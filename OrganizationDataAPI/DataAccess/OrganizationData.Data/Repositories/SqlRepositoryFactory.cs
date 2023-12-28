using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.Repository;

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

        public ICountryRepository CreateCountryRepository()
        {
            return new CountryRepository(_connectionWrapper);
        }

        public IIndustryRepository CreateIndustryRepository()
        {
            return new IndustryRepository(_connectionWrapper);
        }
    }
}
