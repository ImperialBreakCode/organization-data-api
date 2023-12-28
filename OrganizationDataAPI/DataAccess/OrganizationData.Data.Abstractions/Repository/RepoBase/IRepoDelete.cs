using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Abstractions.Repository.RepoBase
{
    public interface IRepoDelete<T> where T : class, IEntity
    {
        void SoftDelete(T entity);
    }
}
