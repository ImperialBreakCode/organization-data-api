using OrganizationData.Application.Abstractions.Services.Factories;
using OrganizationData.Application.DTO.User;

namespace OrganizationData.Application.Services.Factories
{
    internal class UserDTOFactory : IUserDTOFactory
    {
        public LoginResponseDTO CreateLoginResponseDTO(string? token, string message)
        {
            return new LoginResponseDTO() { Token = token, Message = message };
        }
    }
}
