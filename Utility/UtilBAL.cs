using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UitilityClasses 
{
    public static class UtilBAL
    {
        public static string Encrypt(string originalString)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes("Kr!$hn@0");
            if (!String.IsNullOrEmpty(originalString))
            {
                //    throw new ArgumentNullException
                //      ("The string which needs to be encrypted can not be null.");
                //
#pragma warning disable SCS0010 // Weak cipher algorithm
                System.Security.Cryptography.DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
#pragma warning restore SCS0010 // Weak cipher algorithm
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream,
                  cryptoProvider.CreateEncryptor(bytes, bytes), System.Security.Cryptography.CryptoStreamMode.Write);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(cryptoStream);
                writer.Write(originalString);
                writer.Flush();
                cryptoStream.FlushFinalBlock();
                writer.Flush();
                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }
            else
            {
                return null;

            }
        }
        public static string Decrypt(string cryptedString)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes("Kr!$hn@0");
            if (String.IsNullOrEmpty(cryptedString))
            {
                throw new ArgumentNullException
                  ("The string which needs to be decrypted can not be null.");
            }
#pragma warning disable SCS0010 // Weak cipher algorithm
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
#pragma warning restore SCS0010 // Weak cipher algorithm
            MemoryStream memoryStream = new MemoryStream
              (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
              cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }
    }
}
