﻿using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Abstractions.Repository.RepoCommon;
using OrganizationData.Data.DbConnectionWrapper;
using OrganizationData.Data.Repositories.RepoCommon;

namespace OrganizationData.Data.DbContext
{
    internal class OrganizationDbContext : IOrganizationDbContext
    {
        private readonly ISqlConnectionWrapper _sqlConnectionWrapper;

        private IRepositoryFactory _repositoryFactory;

        private IOrganizationRepository _organizationRepository;
        private ICountryRepository _countryRepository;
        private IIndustryRepository _industryRepository;
        private IStatsRepository _statsRepository;

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

        public IIndustryRepository Industry 
            => _industryRepository ??= _repositoryFactory.CreateIndustryRepository();

        public IStatsRepository Stats 
            => _statsRepository ??= _repositoryFactory.CreateStatsRepository();

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
