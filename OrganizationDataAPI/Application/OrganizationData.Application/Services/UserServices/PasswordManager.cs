using OrganizationData.Application.Abstractions.Services.User;
using System.Security.Cryptography;

namespace OrganizationData.Application.Services.UserServices
{
    internal class PasswordManager : IPasswordManager
    {
        private const int _keySize = 64;
        private const int _iterations = 1000;
        private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

        public string HashPassword(string password, out string salt)
        {
            byte[] saltByteArray = RandomNumberGenerator.GetBytes(_keySize);
            salt = Convert.ToHexString(saltByteArray);

            byte[] hash = CreateHash(password, salt);

            return Convert.ToHexString(hash);
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            byte[] hashFromPass = CreateHash(password, salt);

            return CryptographicOperations.FixedTimeEquals(hashFromPass, Convert.FromHexString(hash));
        }

        private byte[] CreateHash(string password, string salt) 
        {
            return Rfc2898DeriveBytes.Pbkdf2(
                password,
                Convert.FromHexString(salt),
                _iterations,
                _hashAlgorithm,
                _keySize);
        }
    }
}
