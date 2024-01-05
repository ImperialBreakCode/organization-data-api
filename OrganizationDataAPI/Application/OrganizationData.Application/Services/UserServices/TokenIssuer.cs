using JWT.Algorithms;
using JWT.Builder;
using OrganizationData.Application.Abstractions.Services.User;
using OrganizationData.Application.Abstractions.Settings;

namespace OrganizationData.Application.Services.UserServices
{
    internal class TokenIssuer : ITokenIssuer
    {
        private readonly IOrganizationSettings _organizationSettings;

        public TokenIssuer(IOrganizationSettings organizationSettings)
        {
            _organizationSettings = organizationSettings;
        }

        public string CreateToken(string username, string role, int secondsValid)
        {
            string token = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(_organizationSettings.AuthSettings.SecretKey)
                .AddClaim("user", username)
                .AddClaim("role", role)
                .AddClaim("admin", "true")
                .AddClaim("exp", DateTimeOffset.UtcNow.AddSeconds(secondsValid).ToUnixTimeSeconds())
                .AddClaim("iss", _organizationSettings.AuthSettings.Issuer)
                .AddClaim("aud", _organizationSettings.AuthSettings.Audience)
                .Encode();

            return token;
        }
    }
}
