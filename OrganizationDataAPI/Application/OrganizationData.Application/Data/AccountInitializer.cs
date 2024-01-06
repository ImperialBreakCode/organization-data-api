using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.Abstractions.Services.User;
using OrganizationData.Application.Services.UserServices;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.Factories;

namespace OrganizationData.Application.Data
{
    internal class AccountInitializer : IAccountInitializer
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IPasswordManager _passwordManager;
        private readonly IDataFilter _dataFilter;

        private IOrganizationDbContext _organizationDbContext;

        public AccountInitializer(IEntityFactory entityFactory, IPasswordManager passwordManager, IDataFilter dataFilter)
        {
            _entityFactory = entityFactory;
            _passwordManager = passwordManager;
            _dataFilter = dataFilter;
        }

        public void EnsureAdminAccountAndRoles(IOrganizationDbContext organizationDbContext)
        {
            _organizationDbContext = organizationDbContext;

            CreateRoleIfItDoesntExist(RoleNames.User);
            CreateRoleIfItDoesntExist(RoleNames.Admin);

            CheckAndCreateAdminUser("admin", "pass");

            _organizationDbContext.SaveChanges();
        }

        private void CreateRoleIfItDoesntExist(string name)
        {
            if (!CheckRole(name))
            {
                _organizationDbContext.UserRole.Insert(_entityFactory.CreateUserRoleEntity(name));
            }
        }

        private bool CheckRole(string name)
        {
            var role = _organizationDbContext.UserRole.GetRoleByName(name);
            var result = _dataFilter.CheckSingle(role);

            return result.Success;
        }

        private void CheckAndCreateAdminUser(string username, string password)
        {
            var user = _organizationDbContext.User.GetByUsername(username);
            var result = _dataFilter.CheckSingle(user);
            if (!result.Success)
            {
                user = _entityFactory.CreateUserEntity(username);
                user.PassHash = _passwordManager.HashPassword(password, out string salt);
                user.Salt = salt;
                user.UserRoleId = _organizationDbContext.UserRole.GetRoleByName(RoleNames.Admin)!.Id;

                _organizationDbContext.User.Insert(user);
            }
        }
    }
}
