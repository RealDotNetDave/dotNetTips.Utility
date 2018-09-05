using System;
using System.Security.Cryptography;
using dotNetTips.Utility.Standard.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dotNetTips.Tips.Utility.Standard.Tests.Security
{
    [TestClass]
    public class EncryptionHelperTester
    {
        [TestMethod]
        public void EncryptDecryptStringTest()
        {
            var raw = "We the People of the United States, in Order to form a more perfect Union, establish Justice, insure domestic Tranquility, provide for the common defense, promote the general Welfare, and secure the Blessings of Liberty to ourselves and our Posterity, do ordain and establish this Constitution for the United States of America.";

            try
            {
                // Create 3DES that generates a new key and initialization vector (IV).  
                // Same key must be used in encryption and decryption  
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    // Encrypt string  
                    var encrypted = EncryptionHelper.TripleDESEncrypt(raw, tdes.Key, tdes.IV);

                    // Decrypt the bytes to a string.  
                    var decrypted = EncryptionHelper.TripleDESDecrypt(encrypted, tdes.Key, tdes.IV);

                    Assert.AreEqual(raw, decrypted);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail($"Encryption/ Description test failed. {ex.Message}");
            }
        }
    }
}
