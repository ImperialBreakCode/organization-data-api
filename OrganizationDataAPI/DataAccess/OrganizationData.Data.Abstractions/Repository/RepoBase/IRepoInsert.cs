using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Abstractions.Repository.RepoBase
{
    public interface IRepoInsert<T> where T : IEntity
    {
        void Insert(T entity);
    }
}
