namespace OrganizationData.Application.DTO.Organization
{
    public class CreateOrganizationRequestDTO
    {
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string Description {  get; set; }
        public int Founded { get; set; }
        public ICollection<string> Industries {  get; set; }
        public int NumberOfEmployees {  get; set; }
    };
}
