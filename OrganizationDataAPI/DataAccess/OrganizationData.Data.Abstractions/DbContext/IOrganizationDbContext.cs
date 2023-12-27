using OrganizationData.Data.Abstractions.Repository;

namespace OrganizationData.Data.Abstractions.DbContext
{
    public interface IOrganizationDbContext : IDisposable
    {
        IOrganizationRepository Organization { get; }
        ICountryRepository Country { get; }
        IIndustryRepository Industry { get; }

        void Setup(string connectionString);

        void SaveChanges();
        void DiscardChanges();
    }
}
