using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Cryptography;
using System.Text;

namespace WsPortfolioExpress.Web.Services
{
    public static class Utilities
    {
        public static string ConvertToSHA256(string content)
        {
            string hash = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(content));

                foreach (byte b in hashValue)
                    hash += $"{b:X2}";

            }
            return hash;
        }

        public static string GenerateToken()
        {
            string token = Guid.NewGuid().ToString("N");
            return token;
        }
    }
}
