using OrganizationData.Data.Abstractions.Repository.RepoBase;
using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IRepositoryWithJunction<T, TJunctionEntity> : IRepository<T>, IRepoJunction<TJunctionEntity>
        where T : class, IEntity
        where TJunctionEntity : class
    {
    }
}
