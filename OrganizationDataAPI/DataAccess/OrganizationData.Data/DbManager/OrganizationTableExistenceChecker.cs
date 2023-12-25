using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions.DbManager;
using System.Data;

namespace OrganizationData.Data.DbManager
{
    internal class OrganizationTableExistenceChecker : IOrganizationTableExistenceChecker
    {
        private const string _tableColumnsCountQuery = @"SELECT Count(*)
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = '{0}';";

        public SqlConnection SqlConnection { get; set; }

        public bool CheckIfTableExists<T>() where T : class
        {
            SqlCommand sqlCommand = SqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = string.Format(_tableColumnsCountQuery, typeof(T).Name);

            return (int)sqlCommand.ExecuteScalar() != 0;
        }
    }
}
