using OrganizationData.Application.DTO.User;

namespace OrganizationData.Application.Abstractions.Services.User
{
    public interface IUserService
    {
        LoginResponseDTO Login(LoginRequestDTO loginRequestDTO);
        string Register(RegisterRequestDTO registerRequestDTO);
        string DeleteByUsername(string username);
    }
}
