using Ciphers.Ciphers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ciphers.Interfaces;

namespace Ciphers.Tests.Ciphers
{
    [TestClass]
    public  class HillCipherTests
    {
        private readonly Mock<IRandomHelper> _randomHelperMock;

        public HillCipherTests()
        {
            _randomHelperMock = new Mock<IRandomHelper>();
            _randomHelperMock.SetupSequence(x => x.Next(26))
                .Returns(6)
                .Returns(24)
                .Returns(1)
                .Returns(13)
                .Returns(16)
                .Returns(10)
                .Returns(20)
                .Returns(17)
                .Returns(15);
        }

        [DataRow(3, "ACT", "POH")]
        [DataRow(3, "AC", "VWT")]
        [DataRow(3, "ACTS", "POH")]
        [DataRow(3, "AC1", "VWT")]
        [DataTestMethod]
        public void Encrypt_ReturnEncryptedMessage_WhenPlainTextIsGiven(int messageMaxLength, string plainText, string expectedEncryption)
        {
            HillCipher cipher = new HillCipher(messageMaxLength, _randomHelperMock.Object);

            string actual = cipher.Encrypt(plainText);

            Assert.AreEqual(expectedEncryption, actual);
        }

        [DataRow(3, "POH", "ACT")]
        [DataRow(3, "VWT", "ACZ")]
        [DataRow(3, "VWT1", "ACZ")]
        [DataTestMethod]
        public void Decrypt_ReturnDecryptedMessage_WhenCipherTextIsGiven(int messageMaxLength, string cipherText, string expectedDecryption)
        {
            HillCipher cipher = new HillCipher(messageMaxLength, _randomHelperMock.Object);

            string actual = cipher.Decrypt(cipherText);

            Assert.AreEqual(expectedDecryption, actual);
        }
    }
}
