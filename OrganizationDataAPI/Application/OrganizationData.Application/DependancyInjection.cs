using Microsoft.Extensions.DependencyInjection;
using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.FileData;
using OrganizationData.Application.Abstractions.Services;
using OrganizationData.Application.Abstractions.Services.Factories;
using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.Abstractions.Services.Organization;
using OrganizationData.Application.Abstractions.Services.User;
using OrganizationData.Application.Data;
using OrganizationData.Application.FileData;
using OrganizationData.Application.Mapper;
using OrganizationData.Application.QuartzConfig;
using OrganizationData.Application.Services;
using OrganizationData.Application.Services.Factories;
using OrganizationData.Application.Services.OrganizationServices;
using OrganizationData.Application.Services.UserServices;
using OrganizationData.Data;
using Quartz;

namespace OrganizationData.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<OrganizationMappingProfile>();
                cfg.AddProfile<CountryMappingProfile>();
                cfg.AddProfile<IndustryMappingProfile>();
            });

            services.AddOrganizationDataLayer();

            services.AddTransient<IOrganizationData, OrganizationDataManager>();
            services.AddTransient<IAccountInitializer, AccountInitializer>();

            services.AddTransient<IDataFilter, DataFilter>();
            services.AddTransient<IServiceGetResultFactory, ServiceGetResultFactory>();

            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IIndustryService, IndustryService>();
            services.AddTransient<IOrganizationDataHelper, OrganizationDataHelper>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IStatsService, StatsService>();

            services.AddTransient<IPasswordManager, PasswordManager>();
            services.AddTransient<ITokenIssuer, TokenIssuer>();
            services.AddTransient<IUserDTOFactory, UserDTOFactory>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddFileDataServices(this IServiceCollection services)
        {
            services.AddQuartz();

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.ConfigureOptions<QuartzSetup>();

            services.AddTransient<IFileReader, FileReader>();
            services.AddTransient<IFileDataManager, FileDataManager>();
            services.AddTransient<IFileModifier, FileModifier>();
            services.AddTransient<IFileDataInserter, FileDataInserter>();

            return services;
        }
    }
}
