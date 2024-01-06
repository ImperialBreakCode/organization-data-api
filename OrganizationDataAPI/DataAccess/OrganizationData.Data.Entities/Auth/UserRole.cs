using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Entities.Auth
{
    public class UserRole : BaseEntity
    {
        public string RoleName { get; set; }
    }
}
