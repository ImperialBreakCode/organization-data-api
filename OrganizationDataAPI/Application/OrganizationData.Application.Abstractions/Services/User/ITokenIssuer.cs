namespace OrganizationData.Application.Abstractions.Services.User
{
    public interface ITokenIssuer
    {
        string CreateToken(string username, string role, int secondsValid);
    }
}
