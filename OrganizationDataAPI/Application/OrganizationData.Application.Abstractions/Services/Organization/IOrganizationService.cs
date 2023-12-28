using OrganizationData.Application.DTO.Organization;

namespace OrganizationData.Application.Abstractions.Services.Organization
{
    public interface IOrganizationService
    {
        ServiceGetResult<GetOrganizationResponseDTO> GetOrganizationByOrganizationId(string organizationId);
        string UpdateOrganization(string organizationId, UpdateOrganizationRequestDTO updateDTO);
        string DeleteOrganization(string organizationId);
        string CreateOrganization(CreateOrganizationRequestDTO createDTO);
        string AddIndustry(AddIndustryRequestDTO addIndustryDTO);
        string RemoveIndustry(RemoveIndustryRequestDTO removeIndustryDTO);
    }
}
