using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CashMachine.Web.Helpers
{
    public static class HashManager
    {
        public static string HashPassword(string password)
        {
            var salt = Encoding.Unicode.GetBytes("salt");
            string saltedPassword = String.Concat(password, salt);
            using (MD5 md5 = MD5.Create())
            {
                var saltedPasswordHash = md5.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword));
                return Convert.ToBase64String(saltedPasswordHash);
            }
        }
    }
}