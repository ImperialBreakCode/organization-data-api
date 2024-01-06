using OrganizationData.Data.Abstractions.Repository.RepoBase;
using OrganizationData.Data.Entities.Auth;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IUserRepository : IRepoDelete<User>, IRepoInsert<User>
    {
        User? GetByUsername(string username);
    }
}
