namespace OrganizationData.API.Extensions
{
    public static class MiddlewareConfiguration
    {
        public static void AddOrganizationCorsPolicies(this IServiceCollection services) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy("OrganizationAPICorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin().WithExposedHeaders("x-api");
                });
            });
        }

        public static void UseOrganizationMiddlewares(this WebApplication webApplication)
        {
            webApplication.Use(async (context, next) =>
            {
                context.Response.Headers.Add("x-api", "organization data api");
                await next();
            });
        }
    }
}
