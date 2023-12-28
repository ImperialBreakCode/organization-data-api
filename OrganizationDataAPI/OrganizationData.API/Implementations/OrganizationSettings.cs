using Microsoft.Extensions.Options;
using OrganizationData.Application.Abstractions.Settings;
using OrganizationData.Application.Options;

namespace OrganizationData.API.Implementations
{
    public class OrganizationSettings : IOrganizationSettings
    {
        private readonly IOptions<ConnectionStringsOptions> _connectionStringOptions;

        public OrganizationSettings(IOptions<ConnectionStringsOptions> connectionStringOptions)
        {
            _connectionStringOptions = connectionStringOptions;
        }

        public string ConnectionString 
            => _connectionStringOptions.Value.OrganizationDbContextConnection;
    }
}
