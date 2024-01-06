using OrganizationData.Data.Abstractions.Repository.RepoBase;
using OrganizationData.Data.Entities.Auth;

namespace OrganizationData.Data.Abstractions.Repository
{
    public interface IUserRoleRepository : IRepoInsert<UserRole>, IRepoRead<UserRole>
    {
        UserRole? GetRoleByName(string name); 
    }
}
