using OrganizationData.Data.Abstractions.DbContext;

namespace OrganizationData.Application.Abstractions.Data
{
    public interface IOrganizationDataSeeder
    {
        void SeedData(IOrganizationDbContext context);
    }
}
