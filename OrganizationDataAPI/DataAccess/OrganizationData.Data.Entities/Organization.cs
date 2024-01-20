using OrganizationData.Data.Entities.Attributes;
using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Entities
{
    public class Organization : BaseEntity
    {
        [Order(3)]
        public string OrganizationId { get; set; }
        [Order(4)]
        public string Name { get; set; }
        [Order(5)]
        public string Website { get; set; }
        [Order(6)]
        public string Description { get; set; }
        [Order(7)]
        public int Founded { get; set; }
        [Order(8)]
        public int NumberOfEmployees { get; set; }
        [Order(9)]
        public string CountryId { get; set; }
    }
}
