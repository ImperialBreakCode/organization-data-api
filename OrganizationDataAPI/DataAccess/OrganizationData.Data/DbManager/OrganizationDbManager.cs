using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions.DbManager;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.DbManager
{
    internal class OrganizationDbManager : IOrganizationDbManager
    {
        private readonly SqlConnection _connection;
        private readonly IOrganizationTableExistenceChecker _existenceChecker;
        private readonly IOrganizationTableCreator _tableCreator;

        public OrganizationDbManager(IOrganizationTableExistenceChecker existenceChecker, IOrganizationTableCreator tableCreator)
        {
            _connection = new SqlConnection();

            _existenceChecker = existenceChecker;
            _existenceChecker.SqlConnection = _connection;

            _tableCreator = tableCreator;
            _tableCreator.SqlConnection = _connection;
        }

        public void EnsureDatabaseTables(string connectionString)
        {
            _connection.ConnectionString = connectionString;
            _connection.Open();

            CreateTableIfItDoesntExist<Country>();
            CreateTableIfItDoesntExist<Organization>();
            CreateTableIfItDoesntExist<Industry>();
            CreateTableIfItDoesntExist<IndustryOrganization>();
        }

        private void CreateTableIfItDoesntExist<T>() where T : class 
        {
            if (!_existenceChecker.CheckIfTableExists<T>())
            {
                _tableCreator.CreateTable<T>();
            }
        }
    }
}
