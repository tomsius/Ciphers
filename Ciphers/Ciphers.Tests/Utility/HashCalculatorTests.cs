using Ciphers.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ciphers.Tests.Utility
{
    [TestClass]
    public class HashCalculatorTests
    {
        [DataRow("abc", "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad")]
        [DataRow("aBc", "516dd854ec42b5b992888cfa87ae16e260864f5e051e045cd7d7c0b45eacbeb2")]
        [DataRow("aBc123%", "e0db509cec9021820734c77c5b4dba97f0581cfe863e7faf14c151a25e7de491")]
        [DataTestMethod]
        public void NormalizeLetter_ReturnNormalizedLetterCode_WhenInputIsLetter(string input, string expectedHashValue)
        {
            string actual = HashCalculator.CalculateHash(input);

            Assert.AreEqual(expectedHashValue, actual);
        }
    }
}
