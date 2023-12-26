using Microsoft.Extensions.DependencyInjection;
using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.Services;
using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.Data;
using OrganizationData.Application.Services;
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

            services.AddTransient<IDataFilter, DataFilter>();

            services.AddCountryService();

            return services;
        }

        public static IServiceCollection AddCountryService(this IServiceCollection services)
        {
            services.AddTransient<ICountryService, CountryService>();
            return services;
        }
    }
}
