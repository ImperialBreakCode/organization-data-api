using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Abstractions.DbContext
{
    public interface IOrganizationDbContext : IDisposable
    {
        IOrganizationRepository Organization { get; }
        IRepository<Country> Country { get; }
        IRepositoryWithJunction<Industry, IndustryOrganization> Industry { get; }

        void SaveChanges();
        void DiscardChanges();
    }
}
