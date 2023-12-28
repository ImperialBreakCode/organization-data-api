namespace OrganizationData.Application.DTO.Organization
{
    //public record GetOrganizationResponseDTO(
    //    string Id,
    //    string CreatedAt,
    //    string OrganizationId,
    //    string Name,
    //    string Website,
    //    string Country,
    //    string Description,
    //    int Founded,
    //    ICollection<string> Industries,
    //    int NumberOfEmployees);

    public class GetOrganizationResponseDTO
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public ICollection<string> Industries { get; set; }
        public int NumberOfEmployees { get; set; }
    }
}
