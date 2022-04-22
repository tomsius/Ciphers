using Ciphers.Ciphers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ciphers.Tests.Ciphers
{
    [TestClass]
    public class VigenereCipherTests
    {
        [DataRow("CAKE", "ABCABC", "CBMEDC")]
        [DataRow("CAKE", "abcabc", "cbmedc")]
        [DataRow("CAKE", "XYZXYZ", "ZYJBAZ")]
        [DataRow("CAKE", "xyzxyz", "zyjbaz")]
        [DataRow("CAKE", "a1234c", "c1234c")]
        [DataRow("CAKE", "ABcABc", "CBmEDc")]
        [DataTestMethod]
        public void Encrypt_ReturnEncryptedMessage_WhenPlainTextIsGiven(string key, string plainText, string expectedEncryption)
        {
            VigenereCipher cipher = new VigenereCipher(key);

            string actual = cipher.Encrypt(plainText);

            Assert.AreEqual(expectedEncryption, actual);
        }

        [DataRow("CAKE", "CBMEDC", "ABCABC")]
        [DataRow("CAKE", "cbmedc", "abcabc")]
        [DataRow("CAKE", "ZYJBAZ", "XYZXYZ")]
        [DataRow("CAKE", "zyjbaz", "xyzxyz")]
        [DataRow("CAKE", "C1234C", "A1234C")]
        [DataRow("CAKE", "CBmEDc", "ABcABc")]
        [DataTestMethod]
        public void Decrypt_ReturnDecryptedMessage_WhenCipherTextIsGiven(string key, string cipherText, string expectedDecryption)
        {
            VigenereCipher cipher = new VigenereCipher(key);

            string actual = cipher.Decrypt(cipherText);

            Assert.AreEqual(expectedDecryption, actual);
        }
    }
}