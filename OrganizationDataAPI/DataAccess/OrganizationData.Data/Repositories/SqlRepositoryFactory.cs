using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Repositories
{
    internal class SqlRepositoryFactory : IRepositoryFactory
    {
        private readonly ISqlConnectionWrapper _connectionWrapper;

        public SqlRepositoryFactory(ISqlConnectionWrapper sqlConnectionWrapper)
        {
            _connectionWrapper = sqlConnectionWrapper;
        }

        public IOrganizationRepository CreateOrganizationRepository()
        {
            return new OrganizationRepository(_connectionWrapper);
        }

        public IRepositoryWithJunction<T, TJunction> CreateGenericRepositoryWithJunction<T, TJunction>()
            where T : class, IEntity
            where TJunction : class
        {
            return new RepositoryWithJunction<T, TJunction>(_connectionWrapper);
        }

        public ICountryRepository CreateCountryRepository()
        {
            return new CountryRepository(_connectionWrapper);
        }
    }
}
