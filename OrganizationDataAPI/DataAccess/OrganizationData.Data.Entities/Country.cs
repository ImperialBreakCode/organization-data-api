using OrganizationData.Data.Entities.Attributes;
using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Entities
{
    public class Country : BaseEntity
    {
        [Order(3)]
        public string CountryName { get; set; }
    }
}
