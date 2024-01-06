using OrganizationData.Data.Abstractions.DbConnectionWrapper;
using OrganizationData.Data.Abstractions.Repository;
using OrganizationData.Data.Entities.Auth;
using OrganizationData.Data.Helpers;
using OrganizationData.Data.Repositories.RepoCommon;

namespace OrganizationData.Data.Repositories
{
    internal class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ISqlConnectionWrapper sqlConnectionWrapper) : base(sqlConnectionWrapper)
        {
        }

        public UserRole? GetRoleByName(string name)
        {
            var command = CreateCommand($"SELECT * FROM [{nameof(UserRole)}] WHERE {nameof(UserRole.RoleName)}=@name");
            command.Parameters.AddWithValue("@name", name);

            return EntityConverterHelper.ToEntityCollection<UserRole>(command).FirstOrDefault();
        }

        public override void SoftDelete(UserRole entity)
        {
            var command = CreateCommand($"Update [{nameof(User)}] SET [{nameof(User.UserRoleId)}]=NULL WHERE [{nameof(User.UserRoleId)}]=@roleId");
            command.Parameters.AddWithValue("@roleId", entity.Id);
            command.ExecuteNonQuery();

            base.SoftDelete(entity);
        }
    }
}
