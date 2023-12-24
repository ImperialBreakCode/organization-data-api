﻿
namespace OrganizationData.Data.Entities.Base
{
    public abstract class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? DeletedAt { get; set; }
    }
}
