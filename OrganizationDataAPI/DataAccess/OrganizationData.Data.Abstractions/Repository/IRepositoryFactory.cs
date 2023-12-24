using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IRepositoryFactory
    {
        IRepository<T> CreateGenericReposioty<T>() where T : IEntity;
    }
}
