using Microsoft.Data.SqlClient;
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

        private readonly IRepositoryFactory _repositoryFactory;

        private IOrganizationRepository _organizationRepository;
        private IRepository<Country> _countryRepository;
        private IRepositoryWithJunction<Industry, IndustryOrganization> _industryRepository;

        public OrganizationDbContext(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
            _sqlConnection.Open();
            _sqlTransaction = _sqlConnection.BeginTransaction();

            _repositoryFactory = new SqlRepositoryFactory(_sqlConnection, _sqlTransaction);
        }

        public IOrganizationRepository Organization 
            => _organizationRepository ??= _repositoryFactory.CreateOrganizationRepository();

        public IRepository<Country> Country 
            => _countryRepository ??= _repositoryFactory.CreateGenericRepository<Country>();

        public IRepositoryWithJunction<Industry, IndustryOrganization> Industry 
            => _industryRepository ??= _repositoryFactory.CreateGenericRepositoryWithJunction<Industry, IndustryOrganization>();

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
