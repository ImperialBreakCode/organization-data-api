using OrganizationData.Data.Abstractions.DbContext;

namespace OrganizationData.Application.Abstractions.Data
{
    public interface IAccountInitializer
    {
        void EnsureAdminAccountAndRoles(IOrganizationDbContext organizationDbContext);
    }
}
