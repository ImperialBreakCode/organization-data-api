using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OrganizationData.API.Constants;
using OrganizationData.Application.Abstractions.Settings;
using OrganizationData.Application.Services.UserServices;
using System.Text;

namespace OrganizationData.API.Extensions
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddOrganizationAuthentication(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var settingsManager = serviceProvider.GetRequiredService<IOrganizationSettings>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = settingsManager.AuthSettings.Issuer,
                        ValidAudience = settingsManager.AuthSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settingsManager.AuthSettings.SecretKey)),
                    };
                });

            return services;
        }

        public static IServiceCollection AddOrganizationAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(ApiScopes.WriteScope, policy =>
                {
                    policy.RequireClaim(RoleNames.User, "true");
                })
                .AddPolicy(ApiScopes.FullScope, policy =>
                {
                    policy.RequireClaim(RoleNames.Admin, "true");
                });

            return services;
        }
    }
}
