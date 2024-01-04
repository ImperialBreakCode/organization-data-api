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

        public OrganizationDataManager(
            IOrganizationDbManager organizationDbManager,
            IOrganizationDbContext organizationDbContext,
            IOrganizationSettings organizationSettings)
        {
            _organizationDbManager = organizationDbManager;
            _organizationSettings = organizationSettings;
            _organizationDbContext = organizationDbContext;

            _organizationDbContext.Setup(_organizationSettings.ConnectionString);
        }

        public IOrganizationDbContext DbContext 
            => _organizationDbContext;

        public void EnsureDatabase()
        {
            _organizationDbManager.EnsureDatabaseTables(_organizationSettings.ConnectionString);
        }

        public void Dispose()
        {
            _organizationDbContext?.Dispose();
        }
    }
}
