using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions.DbManager;
using OrganizationData.Data.SqlScripts;
using System.Data;

namespace OrganizationData.Data.DbManager
{
    internal class OrganizationTableCreator : IOrganizationTableCreator
    {
        public SqlConnection SqlConnection { get; set; }

        public void CreateTable<T>() where T : class
        {
            SqlCommand sqlCommand = SqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = TableCreationScripts.CreationScripts[nameof(T)];
            sqlCommand.ExecuteNonQuery();
        }
    }
}
