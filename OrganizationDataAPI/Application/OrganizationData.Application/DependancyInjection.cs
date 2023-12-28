using Microsoft.Extensions.DependencyInjection;
using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.Services;
using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.Abstractions.Services.Organization;
using OrganizationData.Application.Data;
using OrganizationData.Application.Mapper;
using OrganizationData.Application.Services;
using OrganizationData.Application.Services.OrganizationServices;
using OrganizationData.Data;

namespace OrganizationData.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DTOMappingProfile>();
            });

            services.AddOrganizationDataLayer();

            services.AddTransient<IOrganizationData, OrganizationDataManager>();
            services.AddTransient<IOrganizationDataSeeder, OrganizationDataSeeder>();

            services.AddTransient<IDataFilter, DataFilter>();

            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IIndustryService, IndustryService>();
            services.AddTransient<IOrganizationDataHelper, OrganizationDataHelper>();
            services.AddTransient<IOrganizationService, OrganizationService>();

            return services;
        }
    }
}
