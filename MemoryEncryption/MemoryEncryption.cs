using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Text.Encoding;

namespace VunvuleaR.MemoryEncryption
{
    /// <summary>
    /// Extensin methods for string padding that can be used for memory encryption.
    /// </summary>
    public static class StringPaddingExtensions
    {
        /// <summary>
        /// Get byte array of the original string, applying a padding with block size 16.
        /// </summary>
        public static byte[] ToByteArrayWithPadding(this String str)
        {
            const int BlockingSize = 16;
            int byteLength = ((str.Length / BlockingSize) + 1) * BlockingSize;
            byte[] toEncrypt = new byte[byteLength];
            ASCII.GetBytes(str).CopyTo(toEncrypt, 0);
            return toEncrypt;
        }

        /// <summary>
        /// Remove padding of a string 
        /// </summary>
        /// <remarks>Padding at the end of the string, char '\0'</remarks>
        public static string RemovePadding(this String str)
        {
            char paddingChar = '\0';
            int indexOfFirstPadding = str.IndexOf(paddingChar);
            string cleanString = str.Remove(indexOfFirstPadding);
            return cleanString;
        }
    }

    [TestClass]
    public class MemoryEncryptionUnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string contentToEncrypt = "Hello Word!";

            byte[] toEncrypt = contentToEncrypt.ToByteArrayWithPadding();
            ProtectedMemory.Protect(toEncrypt, MemoryProtectionScope.SameProcess);

            ProtectedMemory.Unprotect(toEncrypt, MemoryProtectionScope.SameProcess);
            string decryptedContent = ASCII.GetString(toEncrypt).RemovePadding();

            Assert.AreEqual(contentToEncrypt,decryptedContent);
        }

      
    }
}
