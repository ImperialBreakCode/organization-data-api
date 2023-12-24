using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Abstractions.DbContext
{
    public interface IOrganizationDbContext
    {
        IOrganizationRepository Organization { get; }
        IRepository<Country> Country { get; }
        IRepository<Industry> Industry { get; }

        void SaveChanges();
    }
}
