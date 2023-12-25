using Microsoft.Extensions.DependencyInjection;
using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Data;
using OrganizationData.Data;

namespace OrganizationData.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddOrganizationDataLayer();

            services.AddTransient<IOrganizationData, OrganizationDataManager>();
            services.AddTransient<IOrganizationDataSeeder, OrganizationDataSeeder>();

            return services;
        }
    }
}
