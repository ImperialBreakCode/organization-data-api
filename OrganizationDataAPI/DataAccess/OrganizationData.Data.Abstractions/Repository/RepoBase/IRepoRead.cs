using OrganizationData.Data.Entities.Base;

namespace OrganizationData.Data.Abstractions.Repository.RepoBase
{
    public interface IRepoRead<T> where T : IEntity
    {
        T? GetById(string id);
        ICollection<T> GetAll();
    }
}
