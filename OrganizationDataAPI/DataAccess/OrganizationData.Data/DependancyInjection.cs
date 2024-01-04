using Microsoft.Extensions.DependencyInjection;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.DbManager;
using OrganizationData.Data.Abstractions.Factories;
using OrganizationData.Data.DbContext;
using OrganizationData.Data.DbManager;
using OrganizationData.Data.Factories;

namespace OrganizationData.Data
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddOrganizationDataLayer(this IServiceCollection services)
        {
            services.AddTransient<IOrganizationDbContext, OrganizationDbContext>();
            services.AddTransient<IOrganizationDbManager, OrganizationDbManager>();
            services.AddTransient<IOrganizationTableExistenceChecker, OrganizationTableExistenceChecker>();
            services.AddTransient<IOrganizationTableCreator, OrganizationTableCreator>();

            services.AddTransient<IEntityFactory, EntityFactory>();

            return services;
        }
    }
}
