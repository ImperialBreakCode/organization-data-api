using Microsoft.Extensions.Options;
using OrganizationData.Application.Abstractions.Settings;
using OrganizationData.Application.Abstractions.Settings.Options;
using OrganizationData.Application.Abstractions.Settings.Options.OrganizationApi;

namespace OrganizationData.API.Implementations
{
    public class OrganizationSettings : IOrganizationSettings
    {
        private readonly IOptions<ConnectionStringsOptions> _connectionStringOptions;
        private readonly IOptionsMonitor<OrganizationApiOptions> _organizationApiOptions;

        public OrganizationSettings(IOptions<ConnectionStringsOptions> connectionStringOptions, IOptionsMonitor<OrganizationApiOptions> organizationApiOptions)
        {
            _connectionStringOptions = connectionStringOptions;
            _organizationApiOptions = organizationApiOptions;
        }

        public string ConnectionString 
            => _connectionStringOptions.Value.OrganizationDbContextConnection;

        public string FileReaderDir 
            => _organizationApiOptions.CurrentValue.FileReaderDir;

        public string ProcessedFilesDir 
            => _organizationApiOptions.CurrentValue.ProcessedFilesDir;

        public AuthSettings AuthSettings 
            => _organizationApiOptions.CurrentValue.AuthSettings;
    }
}
