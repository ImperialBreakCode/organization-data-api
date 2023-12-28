namespace OrganizationData.Application.DTO.Organization
{
    public record GetOrganizationResponseDTO(
        string Id,
        string CreatedAt,
        string OrganizationId,
        string Name,
        string Website,
        string Country,
        string Description,
        string Founded,
        ICollection<string> Industries,
        string NumberOfEmployees);
}
