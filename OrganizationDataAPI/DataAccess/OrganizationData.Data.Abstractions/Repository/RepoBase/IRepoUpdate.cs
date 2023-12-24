using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Abstractions.Repository.RepoBase
{
    public interface IRepoUpdate<T> where T : IEntity
    {
        void Update(T entity);
    }
}
