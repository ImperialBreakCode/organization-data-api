namespace OrganizationData.Application.DTO.Organization
{
    public record CreateOrganizationRequestDTO(
        string OrganizationId, 
        string Name, 
        string Website, 
        string Country, 
        string Description,
        int Founded,
        ICollection<string> Industries,
        int NumberOfEmployees);
}
