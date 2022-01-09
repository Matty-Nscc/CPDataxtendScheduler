using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Configuration;

namespace DBAccess
{
    public class Encryption
    {
        private static string key = "MME(2021)PB_MatthewV";

        public static string ToEncryptMD5(string toEncrypt, string Key, bool useHashing)
        {
            if (Key == null)
                Key = key;

            byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
            byte[] numArray;
            if (useHashing)
            {
                MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
                numArray = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(Key));
                cryptoServiceProvider.Clear();
            }
            else
                numArray = Encoding.UTF8.GetBytes(Key);
            TripleDESCryptoServiceProvider cryptoServiceProvider1 = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider1.Key = numArray;
            cryptoServiceProvider1.Mode = CipherMode.ECB;
            cryptoServiceProvider1.Padding = PaddingMode.PKCS7;
            byte[] inArray = cryptoServiceProvider1.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            cryptoServiceProvider1.Clear();
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        public static string ToDecryptMD5(string cipherString, string Key, bool useHashing)
        {
            if (Key == null)
                Key = key;

            byte[] inputBuffer = Convert.FromBase64String(cipherString);
            byte[] numArray;
            if (useHashing)
            {
                MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
                numArray = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(Key));
                cryptoServiceProvider.Clear();
            }
            else
                numArray = Encoding.UTF8.GetBytes(Key);
            TripleDESCryptoServiceProvider cryptoServiceProvider1 = new TripleDESCryptoServiceProvider();
            cryptoServiceProvider1.Key = numArray;
            cryptoServiceProvider1.Mode = CipherMode.ECB;
            cryptoServiceProvider1.Padding = PaddingMode.PKCS7;
            byte[] bytes = cryptoServiceProvider1.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            cryptoServiceProvider1.Clear();
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
