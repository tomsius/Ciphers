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

        [DataRow("C", "సਨӶ੆Y։կం֯੆ంկġਨġġ֯࠰ਨసச੆֯։ਨ։ޥసġ॥੆ంంկ࠺ਨġġӶ॥॥կ࠰ġޥ࠺։࠰ġկ੆࠰࠰Yసంంਨச॥࠰ص։կ")]
        [DataRow("AbC123%", "կկկ࠰ంசޥంկ࠺֯֯ంంਨ։ص։సਨկ࠺ంం։࠰Y࠰࠺։ġ੆࠺ص࠰੆࠺։ޥ੆։॥։ޥ࠰Ӷ࠺֯సਨصਨġص࠰ġ॥ص࠺ص࠺॥ਨం")]
        [DataTestMethod]
        public void Sign_ReturnSignedHashValue_WhenInputIsGiven(string input, string expectedSignedHashValue)
        {
            RSACipher sender = new(17, 413, 3233);

            string actual = sender.Sign(input);

            Assert.AreEqual(expectedSignedHashValue, actual);
        }

        [DataRow("C", "సਨӶ੆Y։կం֯੆ంկġਨġġ֯࠰ਨసச੆֯։ਨ։ޥసġ॥੆ంంկ࠺ਨġġӶ॥॥կ࠰ġޥ࠺։࠰ġկ੆࠰࠰Yసంంਨச॥࠰ص։կ", true)]
        [DataRow("AbC123%", "fake", false)]
        [DataTestMethod]
        public void IsSignatureValid_ReturnSignatureVerificationResult_WhenInputIsGiven(string input, string signature, bool expectedVerificationResult)
        {
            RSACipher sender = new(17, 413, 3233);
            RSACipher receiver = new(17, 413, 3233);

            bool actual = receiver.IsSignatureValid(input, signature, sender.PublicKey);

            Assert.AreEqual(expectedVerificationResult, actual);
        }
    }
}
