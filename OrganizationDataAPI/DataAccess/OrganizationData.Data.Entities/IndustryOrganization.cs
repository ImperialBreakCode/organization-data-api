using OrganizationData.Data.Entities.Attributes;

namespace OrganizationData.Data.Entities
{
    public class IndustryOrganization
    {
        [Order(0)]
        public string OrganizationId { get; set; }
        [Order(1)]
        public string IndustryId { get; set; }
    }
}
