using OrganizationData.Application.DTO.Organization;
using OrganizationData.Application.DTO.Stats;

namespace OrganizationData.Application.Abstractions.Services
{
    public interface IStatsService
    {
        ICollection<GetOrganizationResponseDTO> GetTopTenYoungestOrganizations();
        ICollection<GetOrganizationResponseDTO> GetTopTenOrganizationsWithMostWorkers();
        ICollection<OrganizationCountByCountryDTO> GetOrganizationCountByCountry();
        ICollection<OrganizationCountByIndustryDTO> GetOrganizationCountByIndustry();
    }
}
