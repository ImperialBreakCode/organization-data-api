using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.DbConnectionWrapper;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Repositories;

namespace OrganizationData.Data.DbContext
{
    internal class OrganizationDbContext : IOrganizationDbContext
    {
        private readonly ISqlConnectionWrapper _sqlConnectionWrapper;

        private IRepositoryFactory _repositoryFactory;

        private IOrganizationRepository _organizationRepository;
        private ICountryRepository _countryRepository;
        private IRepositoryWithJunction<Industry, IndustryOrganization> _industryRepository;

        private bool _isSet;

        public OrganizationDbContext()
        {
            _sqlConnectionWrapper = new SqlConnectionWrapper();
            _isSet = false;
        }

        public IOrganizationRepository Organization 
            => _organizationRepository ??= _repositoryFactory.CreateOrganizationRepository();

        public ICountryRepository Country 
            => _countryRepository ??= _repositoryFactory.CreateCountryRepository();

        public IRepositoryWithJunction<Industry, IndustryOrganization> Industry 
            => _industryRepository ??= _repositoryFactory.CreateGenericRepositoryWithJunction<Industry, IndustryOrganization>();

        public void Setup(string connectionString)
        {
            if (!_isSet)
            {
                _sqlConnectionWrapper.ConnectToDb(connectionString);
                _sqlConnectionWrapper.BeginNewTransaction();
                _repositoryFactory = new SqlRepositoryFactory(_sqlConnectionWrapper);

                _isSet = true;
            }
        }

        public void SaveChanges()
        {
            _sqlConnectionWrapper.SqlTransaction.Commit();
            _sqlConnectionWrapper.BeginNewTransaction();
        }

        public void DiscardChanges()
        {
            _sqlConnectionWrapper.SqlTransaction.Rollback();
            _sqlConnectionWrapper.BeginNewTransaction();
        }

        public void Dispose()
        {
            _sqlConnectionWrapper.SqlTransaction?.Dispose();
            _sqlConnectionWrapper.SqlConnection?.Dispose();
        }
    }
}
