using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using TaskHouseApi.Model;

namespace TaskHouseApi.Logic
{

    public class SecurityHandler
    {
        public static (string saltText, string saltechashedPassword) GenerateNewPassword(User user) 
        {
            // generate a random salt 
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            string saltText = Convert.ToBase64String(saltBytes);

            return (saltText, GenerateSaltedHashedPassword(user.Password, saltText));
        }

        public static string GenerateSaltedHashedPassword(string password, string saltText) 
        {
            // generate the salted and hashed password 
            var sha = SHA256.Create();
            var saltedPassword = password + saltText;
            var saltedhashedPassword = Convert.ToBase64String(
            sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));

            return saltedhashedPassword;
        }
    }
}
