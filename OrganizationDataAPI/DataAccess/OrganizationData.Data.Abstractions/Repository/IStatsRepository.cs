using OrganizationData.Data.Entities;
using OrganizationData.Data.Entities.QueryStatsResults;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IStatsRepository
    {
        ICollection<OrganizationCountByCountry> GetOrganizationCountByCountries();
        ICollection<OrganizationCountByIndustry> GetOrganizationCountByIndustries();
        ICollection<Organization> GetTopTenOrganizationsWithMostWorkers();
        ICollection<Organization> GetTopTenYoungestOrganizations();
    }
}
