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
        private readonly IAccountInitializer _accountInitializer;

        public OrganizationDataManager(
            IOrganizationDbManager organizationDbManager,
            IOrganizationDbContext organizationDbContext,
            IOrganizationSettings organizationSettings,
            IAccountInitializer accountInitializer)
        {
            _organizationDbManager = organizationDbManager;
            _organizationSettings = organizationSettings;
            _organizationDbContext = organizationDbContext;
            _accountInitializer = accountInitializer;

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

        public void EnsureAdminAccountAndRoles()
        {
            _accountInitializer.EnsureAdminAccountAndRoles(_organizationDbContext);
        }
    }
}
