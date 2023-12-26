using Microsoft.Data.SqlClient;
using OrganizationData.Data.Abstractions;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities;
using OrganizationData.Data.Repositories;

namespace OrganizationData.Data.DbContext
{
    internal class OrganizationDbContext : IOrganizationDbContext
    {
        private readonly SqlConnection _sqlConnection;
        private SqlTransaction _sqlTransaction;

        private IRepositoryFactory _repositoryFactory;

        private IOrganizationRepository _organizationRepository;
        private ICountryRepository _countryRepository;
        private IRepositoryWithJunction<Industry, IndustryOrganization> _industryRepository;

        private bool _isSet;

        public OrganizationDbContext()
        {
            _sqlConnection = new SqlConnection();
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
                _sqlConnection.ConnectionString = connectionString;
                _sqlConnection.Open();
                _sqlTransaction = _sqlConnection.BeginTransaction();

                _repositoryFactory = new SqlRepositoryFactory(_sqlConnection, _sqlTransaction);

                _isSet = true;
            }
        }

        public void SaveChanges()
        {
            _sqlTransaction.Commit();
            _sqlTransaction = _sqlConnection.BeginTransaction();
        }

        public void DiscardChanges()
        {
            _sqlTransaction.Rollback();
            _sqlTransaction = _sqlConnection.BeginTransaction();
        }

        public void Dispose()
        {
            _sqlTransaction?.Dispose();
            _sqlConnection?.Dispose();
        }
    }
}
