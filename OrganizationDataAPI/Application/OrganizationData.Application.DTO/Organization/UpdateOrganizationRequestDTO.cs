namespace OrganizationData.Application.DTO.Organization
{
    public class UpdateOrganizationRequestDTO
    {
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public int Founded { get; set; }
        public int NumberOfEmployees { get; set; }
    }
}
