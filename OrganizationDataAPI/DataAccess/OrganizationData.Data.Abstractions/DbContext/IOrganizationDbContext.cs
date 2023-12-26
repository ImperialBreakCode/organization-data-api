using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities;

namespace OrganizationData.Data.Abstractions.DbContext
{
    public interface IOrganizationDbContext : IDisposable
    {
        IOrganizationRepository Organization { get; }
        ICountryRepository Country { get; }
        IRepositoryWithJunction<Industry, IndustryOrganization> Industry { get; }

        void Setup(string connectionString);

        void SaveChanges();
        void DiscardChanges();
    }
}
