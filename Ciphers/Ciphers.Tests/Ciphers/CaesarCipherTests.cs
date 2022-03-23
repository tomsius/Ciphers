using Ciphers.Caesar;
using Ciphers.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ciphers.Tests.Ciphers
{
    [TestClass]
    public class CaesarCipherTests
    {
        [DataRow(3, "ABC", "DEF")]
        [DataRow(3, "abc", "def")]
        [DataRow(3, "XYZ", "ABC")]
        [DataRow(3, "xyz", "abc")]
        [DataRow(3, "aBc", "dEf")]
        [DataRow(3, "xYz", "aBc")]
        [DataRow(-3, "DEF", "ABC")]
        [DataRow(-3, "def", "abc")]
        [DataRow(-3, "ABC", "XYZ")]
        [DataRow(-3, "abc", "xyz")]
        [DataRow(-3, "dEf", "aBc")]
        [DataRow(-3, "aBc", "xYz")]
        [DataRow(29, "ABC", "DEF")]
        [DataRow(29, "abc", "def")]
        [DataRow(29, "XYZ", "ABC")]
        [DataRow(29, "xyz", "abc")]
        [DataRow(29, "aBc", "dEf")]
        [DataRow(29, "xYz", "aBc")]
        [DataRow(-29, "DEF", "ABC")]
        [DataRow(-29, "def", "abc")]
        [DataRow(-29, "ABC", "XYZ")]
        [DataRow(-29, "abc", "xyz")]
        [DataRow(-29, "dEf", "aBc")]
        [DataRow(-29, "aBc", "xYz")]
        [DataRow(3, "a123c", "d123f")]
        [DataRow(-3, "D321F", "A321C")]
        [DataTestMethod]
        public void Encrypt_ReturnEncryptedMessage_WhenPlainTextIsGiven(int key, string plainText, string expectedEncryption)
        {
            CaesarCipher cipher = new CaesarCipher(key);

            string actual = cipher.Encrypt(plainText);

            Assert.AreEqual(expectedEncryption, actual);
        }

        [DataRow(3, "DEF", "ABC")]
        [DataRow(3, "def", "abc")]
        [DataRow(3, "ABC", "XYZ")]
        [DataRow(3, "abc", "xyz")]
        [DataRow(3, "dEf", "aBc")]
        [DataRow(3, "aBc", "xYz")]
        [DataRow(-3, "ABC", "DEF")]
        [DataRow(-3, "abc", "def")]
        [DataRow(-3, "XYZ", "ABC")]
        [DataRow(-3, "xyz", "abc")]
        [DataRow(-3, "aBc", "dEf")]
        [DataRow(-3, "xYz", "aBc")]
        [DataRow(29, "DEF", "ABC")]
        [DataRow(29, "def", "abc")]
        [DataRow(29, "ABC", "XYZ")]
        [DataRow(29, "abc", "xyz")]
        [DataRow(29, "dEf", "aBc")]
        [DataRow(29, "aBc", "xYz")]
        [DataRow(-29, "ABC", "DEF")]
        [DataRow(-29, "abc", "def")]
        [DataRow(-29, "XYZ", "ABC")]
        [DataRow(-29, "xyz", "abc")]
        [DataRow(-29, "aBc", "dEf")]
        [DataRow(-29, "xYz", "aBc")]
        [DataRow(3, "D321F", "A321C")]
        [DataRow(-3, "a123c", "d123f")]
        [DataTestMethod]
        public void Decrypt_ReturnDecryptedMessage_WhenCipherTextIsGiven(int key, string cipherText, string expectedDecryption)
        {
            CaesarCipher cipher = new CaesarCipher(key);

            string actual = cipher.Decrypt(cipherText);

            Assert.AreEqual(expectedDecryption, actual);
        }
    }
}