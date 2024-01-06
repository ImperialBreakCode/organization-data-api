using OrganizationData.Data.Abstractions.Repository.RepoBase;
using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Abstractions.Repository.RepoCommon
{
    public interface IRepository<T> : IRepoRead<T>, IRepoInsert<T>, IRepoUpdate<T>, IRepoDelete<T>
        where T : class, IEntity
    {
    }
}
