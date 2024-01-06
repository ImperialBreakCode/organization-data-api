using OrganizationData.Data.Abstractions.DbContext;

namespace OrganizationData.Application.Abstractions.Data
{
    public interface IOrganizationData : IDisposable
    {
        IOrganizationDbContext DbContext { get; }
        void EnsureDatabase();
        void EnsureAdminAccountAndRoles();
    }
}
