using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;


namespace SurveyApp.Handlers
{
    public class PasswordHashHandler
    {
        private static int _iterationCount = 100000;
        private static RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

        public static string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            _randomNumberGenerator.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, _iterationCount, 256 / 8);
            byte[] outputBytes = new byte[128 / 8 + 256 / 8];
            salt.CopyTo(outputBytes, 0);
            subkey.CopyTo(outputBytes, 128 / 8);
            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);
            byte[] salt = decodedHashedPassword.Take(128 / 8).ToArray();
            byte[] subkey = decodedHashedPassword.Skip(128 / 8).ToArray();
            byte[] generatedSubkey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, _iterationCount, 256 / 8);
            return generatedSubkey.SequenceEqual(subkey);
        }

    }
}
