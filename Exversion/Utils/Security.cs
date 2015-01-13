using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Exversion.Utils
{
    public static class Security
    {
        public static string HashSHA1(string text)
        {
            SHA1CryptoServiceProvider sha1Obj =
                new SHA1CryptoServiceProvider();

            byte[] bytesToHash = System.Text.Encoding.ASCII.GetBytes(text);
            string strResult = "";

            bytesToHash = sha1Obj.ComputeHash(bytesToHash);

            foreach (byte b in bytesToHash)
            {
                strResult += b.ToString("x2");
            }

            return strResult;
        }

        public static string Encrypt(string plainText)
        {
            if (plainText == null)
                return null;

            var data = Encoding.Unicode.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string cipher)
        {
            if (cipher == null)
                return null;

            byte[] data = Convert.FromBase64String(cipher);

            byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decrypted);
        }
    }
}
