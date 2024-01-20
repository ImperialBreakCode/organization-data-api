using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.FileData.DataInsertion;

namespace OrganizationData.API.BackgroundServices
{
    public class StartupService : BackgroundService
    {
        private readonly IOrganizationData _organizationData;
        private readonly IOrganizationIdsSet _organizationIdsSet;

        public StartupService(IOrganizationData organizationData, IOrganizationIdsSet organizationIdsSet)
        {
            _organizationData = organizationData;
            _organizationIdsSet = organizationIdsSet;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _organizationData.EnsureDatabase();
            _organizationData.EnsureAdminAccountAndRoles();

            _organizationIdsSet.LoadData(_organizationData.DbContext);

            _organizationData.Dispose();

            return Task.CompletedTask;
        }
    }
}
