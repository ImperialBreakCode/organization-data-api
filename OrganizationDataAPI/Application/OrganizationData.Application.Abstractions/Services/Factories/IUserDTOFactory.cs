using OrganizationData.Application.DTO.User;

namespace OrganizationData.Application.Abstractions.Services.Factories
{
    public interface IUserDTOFactory
    {
        LoginResponseDTO CreateLoginResponseDTO(string? token, string message);
    }
}
