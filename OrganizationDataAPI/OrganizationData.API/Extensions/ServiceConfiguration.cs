using OrganizationData.API.BackgroundServices;
using OrganizationData.API.Implementations;
using OrganizationData.Application;
using OrganizationData.Application.Abstractions.Settings;
using OrganizationData.Application.Options;

namespace OrganizationData.API.Extensions
{
    public static class ServiceConfiguration
    {
        public static void ConfigureOptions(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ConnectionStringsOptions>(
                builder.Configuration.GetSection("ConnectionStrings"));

            builder.Services.Configure<OrganizationApiOptions>(
                builder.Configuration.GetSection(nameof(OrganizationApiOptions)));
        }

        public static IServiceCollection ConfigureOrganizationApplication(this IServiceCollection services)
        {
            services.AddApplication().AddFileDataServices();

            services.AddTransient<IOrganizationSettings, OrganizationSettings>();

            services.AddHostedService<StartupService>();

            return services;
        }
    }
}
