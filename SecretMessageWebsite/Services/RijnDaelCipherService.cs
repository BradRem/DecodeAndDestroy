using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace SecretMessageWebsite.Services
{
    public class RijnDaelCipherService : ICipherService
    {
        public string Encrypt(string plainText, string key)
        {
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                myRijndael.GenerateIV();
                var iv = myRijndael.IV;

                var codedKey = CreateKey(key);

                byte[] encrypted = EncryptStringToBytes(plainText, codedKey, iv);

                var ivString = Convert.ToBase64String(iv);
                var result = string.Format("{0:x}{1}{2}", ivString.Length, ivString, Convert.ToBase64String(encrypted));
                return result;
            }
        }

        public string Decrypt(string encrypted, string key)
        {
            using (RijndaelManaged myRijndael = new RijndaelManaged())
            {
                var ivLength = int.Parse(encrypted.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                var iv = encrypted.Substring(2, ivLength);
                var codedText = encrypted.Substring(2 + ivLength);

                var codedKey = RijnDaelCipherService.CreateKey(key);

                // Decrypt the bytes to a string. 
                var decoded = Convert.FromBase64String(codedText);
                string plainText = DecryptStringFromBytes(decoded,
                    codedKey,
                    Convert.FromBase64String(iv));
                return plainText;
            }
        }

        protected static byte[] CreateKey(string password)
        {
            var salt = new byte[] { 5, 3, 247, 31, 32, 33, 101, 54, 108, 18 };

            const int Iterations = 8808;
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, Iterations))
                return rfc2898DeriveBytes.GetBytes(32);
        }

        protected static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream. 
            return encrypted;
        }

        protected static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }
    }
}


