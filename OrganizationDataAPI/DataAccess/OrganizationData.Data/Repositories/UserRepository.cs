using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities.Auth;
using OrganizationData.Data.Helpers;
using OrganizationData.Data.Repositories.RepoCommon;

namespace OrganizationData.Data.Repositories
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ISqlConnectionWrapper sqlConnectionWrapper) : base(sqlConnectionWrapper)
        {
        }

        public User? GetByUsername(string username)
        {
            var command = CreateCommand($"SELECT * FROM [{nameof(User)}] WHERE {nameof(User.Username)}=@username AND DeletedAt IS NULL");
            command.Parameters.AddWithValue("@username", username);

            return EntityConverterHelper.ToEntityCollection<User>(command).FirstOrDefault();
        }
    }
}
