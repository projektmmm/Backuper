using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    public class GlobalSettings
    {
        public readonly int AdminId = 1;
        private readonly string DecryptedPassword = "Ab123456";

        public string GetHashedPassword()
        {
            var sha1 = SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(this.DecryptedPassword);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }


    }
}
