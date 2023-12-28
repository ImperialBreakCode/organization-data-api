namespace OrganizationData.Application.DTO.Organization
{
    public record UpdateOrganizationRequestDTO(
        string OrganizationId,
        string Name,
        string Website,
        string Description,
        string Country,
        int Founded,
        int NumberOfEmployees);
}
