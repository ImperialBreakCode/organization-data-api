using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.Settings;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.DbManager;

namespace OrganizationData.Application.Data
{
    internal class OrganizationDataManager : IOrganizationData
    {
        private readonly IOrganizationDbManager _organizationDbManager;
        private readonly IOrganizationDbContext _organizationDbContext;
        private readonly IOrganizationSettings _organizationSettings;
        private readonly IOrganizationDataSeeder _seeder;

        public OrganizationDataManager(
            IOrganizationDbManager organizationDbManager,
            IOrganizationDbContext organizationDbContext,
            IOrganizationSettings organizationSettings,
            IOrganizationDataSeeder seeder)
        {
            _organizationDbManager = organizationDbManager;
            _organizationSettings = organizationSettings;
            _organizationDbContext = organizationDbContext;
            _seeder = seeder;

            _organizationDbContext.Setup(_organizationSettings.ConnectionString);
        }

        public IOrganizationDbContext DbContext 
            => _organizationDbContext;

        public void EnsureDatabase()
        {
            _organizationDbManager.EnsureDatabaseTables(_organizationSettings.ConnectionString);
        }

        public void SeedData()
        {
            _seeder.SeedData(DbContext);
        }

        public void Dispose()
        {
            _organizationDbContext?.Dispose();
        }
    }
}
