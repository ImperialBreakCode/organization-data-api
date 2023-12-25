using OrganizationData.Data.Abstractions.DbContext;

namespace OrganizationData.Application.Abstractions.Data
{
    public interface IOrganizationData
    {
        IOrganizationDbContext DbContext { get; }

        void EnsureDatabase();
        void SeedData();
    }
}
