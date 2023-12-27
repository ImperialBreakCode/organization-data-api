using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions.DbConnectionWrapper;

namespace OrganizationData.Data.DbConnectionWrapper
{
    internal class SqlConnectionWrapper : ISqlConnectionWrapper
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public SqlConnectionWrapper()
        {
            _connection = new SqlConnection();
        }

        public SqlConnection SqlConnection => _connection;
        public SqlTransaction SqlTransaction => _transaction;

        public void BeginNewTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void ConnectToDb(string connectionString)
        {
            _connection.ConnectionString = connectionString;
            _connection.Open();
        }
    }
}
