// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard
// Author           : David McCarter
// Created          : 09-05-2018
//
// Last Modified By : David McCarter
// Last Modified On : 09-05-2018
// ***********************************************************************
// <copyright file="EncryptionHelper.cs" company="dotNetTips.com - David McCarter">
//     McCarter Consulting (David McCarter)
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IO;
using System.Security.Cryptography;

namespace dotNetTips.Utility.Standard.Security
{
    /// <summary>
    /// Class EncryptionHelper.
    /// </summary>
    public static class EncryptionHelper
    {

        /// <summary>
        /// Triples the DES decrypt.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="Key">The key.</param>
        /// <param name="IV">The iv.</param>
        /// <remarks>Original code by: Mahesh Chand</remarks>
        /// <returns>System.String.</returns>
        public static string TripleDESDecrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;

            // Create TripleDESCryptoServiceProvider  
            using (var tdes = new TripleDESCryptoServiceProvider())
            {
                // Create a decryptor  
                var decryptor = tdes.CreateDecryptor(Key, IV);

                // Create the streams used for decryption.  
                using (var ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream  
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream  
                        using (var reader = new StreamReader(cs))
                        {
                            plaintext = reader.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
        /// <summary>
        /// Triples the DES encrypt.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="Key">The key.</param>
        /// <param name="IV">The iv.</param>
        /// <remarks>Original code by: Mahesh Chand</remarks>
        /// <returns>System.Byte[].</returns>
        public static byte[] TripleDESEncrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            // Create a new TripleDESCryptoServiceProvider.  
            using (var tdes = new TripleDESCryptoServiceProvider())
            {
                // Create encryptor  
                var encryptor = tdes.CreateEncryptor(Key, IV);

                // Create MemoryStream  
                using (var ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption  
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream  
                    // to encrypt  
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream  
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        encrypted = ms.ToArray();
                    }
                }
            }

            return encrypted;
        }
    }
}
