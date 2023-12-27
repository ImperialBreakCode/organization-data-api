using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Repositories
{
    internal class SqlRepositoryFactory : IRepositoryFactory
    {
        private readonly SqlConnection _sqlConnection;
        private readonly SqlTransaction _sqlTransaction;

        public SqlRepositoryFactory(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            _sqlConnection = sqlConnection;
            _sqlTransaction = sqlTransaction;
        }

        public IOrganizationRepository CreateOrganizationRepository()
        {
            return new OrganizationRepository(_sqlConnection, _sqlTransaction);
        }

        public IRepositoryWithJunction<T, TJunction> CreateGenericRepositoryWithJunction<T, TJunction>()
            where T : class, IEntity
            where TJunction : class
        {
            return new RepositoryWithJunction<T, TJunction>(_sqlConnection, _sqlTransaction);
        }

        public ICountryRepository CreateCountryRepository()
        {
            return new CountryRepository(_sqlConnection, _sqlTransaction);
        }
    }
}
