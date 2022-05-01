using Ciphers.Ciphers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ciphers.Tests.Ciphers
{
    [TestClass]
    public class PlayfairCipherTests
    {
        [DataRow("MONARCHY", "INSTRUMENTS", "GATLMZCLRQTX")]
        [DataRow("monarchy", "instruments", "GATLMZCLRQTX")]
        [DataRow("MONarCHY", "INSTrumENTS", "GATLMZCLRQTX")]
        [DataRow("MONarCHY", "hello", "CFSUPM")]
        [DataRow("MONarCHY", "helloe", "CFSUPMKU")]
        [DataRow("japonija", "jonas", "ANIPUX")]
        [DataTestMethod]
        public void Encrypt_ReturnEncryptedMessage_WhenPlainTextIsGiven(string key, string plainText, string expectedEncryption)
        {
            PlayfairCipher cipher = new PlayfairCipher(key);

            string actual = cipher.Encrypt(plainText);

            Assert.AreEqual(expectedEncryption, actual);
        }

        [DataRow("MONARCHY", "GATLMZCLRQTX", "INSTRUMENTS")]
        [DataRow("monarchy", "gatlmzclrqtx", "INSTRUMENTS")]
        [DataRow("MONarCHY", "GATLmzcLRQTX", "INSTRUMENTS")]
        [DataRow("MONarCHY", "CFSUPM", "HELLO")]
        [DataRow("MONarCHY", "CFSUPMKU", "HELLOE")]
        [DataRow("japonija", "ANIPUX", "IONAS")]
        [DataRow("MONARCHY", "MC", "UM")]
        [DataTestMethod]
        public void Decrypt_ReturnDecryptedMessage_WhenCipherTextIsGiven(string key, string cipherText, string expectedDecryption)
        {
            PlayfairCipher cipher = new PlayfairCipher(key);

            string actual = cipher.Decrypt(cipherText);

            Assert.AreEqual(expectedDecryption, actual);
        }
    }
}
