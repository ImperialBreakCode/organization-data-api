using OrganizationData.Data.Entities.Attributes;
using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Entities
{
    public class Industry : BaseEntity
    {
        [Order(3)]
        public string IndustryName { get; set; }
    }
}
