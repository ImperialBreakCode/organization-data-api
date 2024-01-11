
using OrganizationData.Data.Entities.Attributes;

namespace OrganizationData.Data.Entities.Base
{
    public abstract class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
            DeletedAt = null;
        }

        [Order(0)]
        public string Id { get; set; }

        [Order(1)]
        public DateTime CreatedAt { get; set; }

        [Order(2)]
        public DateTime? DeletedAt { get; set; }
    }
}
