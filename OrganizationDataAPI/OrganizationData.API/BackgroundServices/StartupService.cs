using OrganizationData.Application.Abstractions.Data;

namespace OrganizationData.API.BackgroundServices
{
    public class StartupService : BackgroundService
    {
        private readonly IOrganizationData _organizationData;

        public StartupService(IOrganizationData organizationData)
        {
            _organizationData = organizationData;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _organizationData.EnsureDatabase();
            _organizationData.Dispose();

            return Task.CompletedTask;
        }
    }
}
