using System;
using System.Security.Cryptography;
using System.Text;

namespace Funzone.UserAccess.Application.Authentication
{
    public class PasswordManager
    {
        public static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string HashPassword(string input, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(input + salt);
            var sha256ManagedString = new SHA256Managed();
            var hash = sha256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyHashedPassword(string plainTextInput, string hashedInput, string salt)
        {
            var newHashedPin = HashPassword(plainTextInput, salt);
            return newHashedPin.Equals(hashedInput);
        }
    }
}
