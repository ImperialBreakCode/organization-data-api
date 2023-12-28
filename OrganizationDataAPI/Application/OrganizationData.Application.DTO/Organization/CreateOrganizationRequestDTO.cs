namespace OrganizationData.Application.DTO.Organization
{
    public record CreateOrganizationRequestDTO(
        string OrganizationId, 
        string Name, 
        string Website, 
        string Country, 
        string Description,
        string Founded,
        ICollection<string> Industries,
        string NumberOfEmployees);
}
