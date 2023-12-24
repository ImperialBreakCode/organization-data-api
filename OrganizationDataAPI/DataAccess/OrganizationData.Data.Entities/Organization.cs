using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Entities
{
    public class Organization : BaseEntity
    {
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string CountryId { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public int NumberOfEmployees { get; set; }
    }
}
