using OrganizationData.Application.DTO.Organization;

namespace OrganizationData.Application.Abstractions.Services.Organization
{
    public interface IOrganizationService
    {
        ServiceGetResult<GetOrganizationResponseDTO> GetOrganizationByOrganizationId(string organizationId);
        string UpdateOrganization(UpdateOrganizationRequestDTO updateDTO);
        string DeleteOrganization(string organizationId);
        string CreateOrganization(CreateOrganizationRequestDTO createDTO);
        string AddIndustry(string industryName);
        string RemoveIndustry(string industryName);
    }
}
