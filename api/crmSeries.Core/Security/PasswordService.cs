using System;
using System.Security.Cryptography;
using System.Text;

namespace crmSeries.Core.Security
{
    public interface IPasswordService
    {
        string CreateHash(string password, string salt);
    }

    public class PasswordService : IPasswordService
    {
        public string CreateHash(string password, string salt)
        {
            var salted = string.Concat(password, salt);

            using (var hashService = new SHA256CryptoServiceProvider())
            {
                var hash = hashService.ComputeHash(Encoding.ASCII.GetBytes(salted));

                return Convert.ToBase64String(hash);
            }
        }
    }
}
