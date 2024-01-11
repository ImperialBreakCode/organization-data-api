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

        private SqlConnection _sqlConnection;

        public FileDataInserter(IOrganizationSettings organizationSettings)
        {
            _organizationSettings = organizationSettings;

            _sqlConnection = new SqlConnection(_organizationSettings.ConnectionString);
        }

        public void SaveData(BulkCollectionWrapper bulkCollectionWrapper)
        {
            _sqlConnection.Open();

            ProcessBulkInsert(bulkCollectionWrapper.CountriesBulkInserts);
            ProcessBulkInsert(bulkCollectionWrapper.IndustriesBulkInserts);
            ProcessBulkInsert(bulkCollectionWrapper.OrganizationBulkInserts);
            ProcessBulkInsert(bulkCollectionWrapper.IndOrgBulkInserts);

            _sqlConnection.Close();
        }

        private void ProcessBulkInsert<T>(ICollection<T> data) where T : class
        {
            BulkInsert(ConvertToDataTable(data), typeof(T).Name);
        }

        private void BulkInsert(DataTable dataTable, string destinationTableName)
        {
            using (SqlBulkCopy bulkCopy = new(_sqlConnection))
            {
                bulkCopy.DestinationTableName = destinationTableName;
                bulkCopy.WriteToServer(dataTable);
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
