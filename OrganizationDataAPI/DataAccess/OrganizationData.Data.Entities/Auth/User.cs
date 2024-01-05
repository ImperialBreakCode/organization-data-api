using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Entities.Auth
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PassHash { get; set; }
        public string Salt { get; set; }
        public string UserRoleId { get; set; }
    }
}
