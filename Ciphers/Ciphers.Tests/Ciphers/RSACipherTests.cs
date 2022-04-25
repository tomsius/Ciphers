using Ciphers.Ciphers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ciphers.Tests.Ciphers
{
    [TestClass]
    public class RSACipherTests
    {
        [DataRow("A1", "૦୚")]
        [DataTestMethod]
        public void Encrypt_ReturnEncryptedMessage_WhenPlainTextIsGiven(string plainText, string expectedEncryption)
        {
            RSACipher sender = new(17, 413, 3233);
            RSACipher receiver = new(17, 413, 3233);

            string actual = sender.Encrypt(plainText, receiver.PublicKey);

            Assert.AreEqual(expectedEncryption, actual);
        }

        [DataRow("૦୚", "A1")]
        [DataTestMethod]
        public void Decrypt_ReturnDecryptedMessage_WhenCipherTextIsGiven(string cipherText, string expectedDecryption)
        {
            RSACipher receiver = new(17, 413, 3233);

            string actual = receiver.Decrypt(cipherText);

            Assert.AreEqual(expectedDecryption, actual);
        }
    }
}
