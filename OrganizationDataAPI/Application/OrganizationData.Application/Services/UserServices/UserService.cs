using OrganizationData.Application.Abstractions.Data;
using OrganizationData.Application.Abstractions.Services.Factories;
using OrganizationData.Application.Abstractions.Services.Filter;
using OrganizationData.Application.Abstractions.Services.User;
using OrganizationData.Application.DTO.User;
using OrganizationData.Application.ResponseMessage;
using OrganizationData.Data.Abstractions.DbContext;
using OrganizationData.Data.Abstractions.Factories;

namespace OrganizationData.Application.Services.UserServices
{
    internal class UserService : IUserService
    {
        private readonly IOrganizationDbContext _organizationDbContext;
        private readonly IPasswordManager _passwordManager;
        private readonly ITokenIssuer _tokenIssuer;
        private readonly IDataFilter _dataFilter;
        private readonly IEntityFactory _entityFactory;
        private readonly IUserDTOFactory _userDTOFactory;

        public UserService(
            IOrganizationData organizationData,
            IPasswordManager passwordManager,
            ITokenIssuer tokenIssuer,
            IDataFilter dataFilter,
            IEntityFactory entityFactory,
            IUserDTOFactory userDTOFactory)
        {
            _organizationDbContext = organizationData.DbContext;
            _passwordManager = passwordManager;
            _tokenIssuer = tokenIssuer;
            _dataFilter = dataFilter;
            _entityFactory = entityFactory;
            _userDTOFactory = userDTOFactory;
        }

        public string DeleteByUsername(string username)
        {
            var user = _organizationDbContext.User.GetByUsername(username);
            var filterResult = _dataFilter.CheckSingle(user);
            if (!filterResult.Success)
            {
                return filterResult.ErrorMessage!;
            }

            _organizationDbContext.User.SoftDelete(user!);
            _organizationDbContext.SaveChanges();

            return ServiceMessages.UserDeleted;
        }

        public LoginResponseDTO Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _organizationDbContext.User.GetByUsername(loginRequestDTO.Username);
            var filterResult = _dataFilter.CheckSingle(user);
            if (!filterResult.Success)
            {
                return _userDTOFactory.CreateLoginResponseDTO(null, ServiceMessages.LoginIncorrectCredentials);
            }

            if (!_passwordManager.VerifyPassword(loginRequestDTO.Password, user!.PassHash, user.Salt))
            {
                return _userDTOFactory.CreateLoginResponseDTO(null, ServiceMessages.LoginIncorrectCredentials);
            }

            var role = _organizationDbContext.UserRole.GetById(user.UserRoleId)!.RoleName;
            string token = _tokenIssuer.CreateToken(loginRequestDTO.Username, role, 3600);

            return _userDTOFactory.CreateLoginResponseDTO(token, ServiceMessages.LoginSuccessfull);
        }

        public string Register(RegisterRequestDTO registerRequestDTO)
        {
            var userWithTheSameName = _organizationDbContext.User.GetByUsername(registerRequestDTO.Username);
            var filterResult = _dataFilter.CheckSingle(userWithTheSameName);
            if (filterResult.Success)
            {
                return ServiceMessages.UsernameConflict;
            }

            var user = _entityFactory.CreateUserEntity(registerRequestDTO.Username);
            user.PassHash = _passwordManager.HashPassword(registerRequestDTO.Password, out string salt);
            user.Salt = salt;
            user.UserRoleId = _organizationDbContext.UserRole.GetRoleByName(RoleNames.User)!.Id;

            _organizationDbContext.User.Insert(user);
            _organizationDbContext.SaveChanges();

            return ServiceMessages.UserCreated;
        }
    }
}
