using Microsoft.Data.SqlClient;
using OrganizationData.Application.Abstractions.FileData.DataInsertion;
using OrganizationData.Application.Abstractions.Settings;
using OrganizationData.Data.Entities.Attributes;
using System.Data;

namespace OrganizationData.Application.FileData.DataInsertion
{
    internal class FileDataInserter : IFileDataInserter
    {
        private readonly IOrganizationSettings _organizationSettings;

        public FileDataInserter(IOrganizationSettings organizationSettings)
        {
            _organizationSettings = organizationSettings;
        }

        public void SaveData(BulkCollectionWrapper bulkCollectionWrapper)
        {
            ProcessBulkInsert(bulkCollectionWrapper.CountriesBulkInserts);
            ProcessBulkInsert(bulkCollectionWrapper.IndustriesBulkInserts);
            ProcessBulkInsert(bulkCollectionWrapper.OrganizationBulkInserts);
            ProcessBulkInsert(bulkCollectionWrapper.IndOrgBulkInserts);
        }

        private void ProcessBulkInsert<T>(ICollection<T> data) where T : class
        {
            BulkInsert<T>(ConvertToDataTable(data));
        }

        private void BulkInsert<T>(DataTable dataTable) where T : class
        {
            using (var connection = new SqlConnection(_organizationSettings.ConnectionString))
            {
                connection.Open();
                SqlTransaction sqlTransaction = connection.BeginTransaction();

                using (SqlBulkCopy bulkCopy = new(connection, SqlBulkCopyOptions.TableLock, sqlTransaction))
                {
                    bulkCopy.DestinationTableName = typeof(T).Name;
                    bulkCopy.BulkCopyTimeout = 0;

                    bulkCopy.WriteToServer(dataTable);
                }

                sqlTransaction.Commit();
            }
        }

        private DataTable ConvertToDataTable<T>(ICollection<T> data) where T : class 
        {
            DataTable dataTable = new();

            if (data == null || data.Count == 0)
            {
                return dataTable;
            }

            var properties = typeof(T)
                .GetProperties()
                .OrderBy(p => ((OrderAttribute)p.GetCustomAttributes(typeof(OrderAttribute), false).FirstOrDefault())?.Order ?? int.MaxValue)
                .ToArray();

            foreach (var property in properties)
            {
                dataTable.Columns.Add(property.Name);
            }

            foreach (var item in data)
            {
                DataRow row = dataTable.NewRow();

                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(item) ?? DBNull.Value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
