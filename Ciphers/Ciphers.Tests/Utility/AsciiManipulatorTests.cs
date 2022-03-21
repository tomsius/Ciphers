using Ciphers.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ciphers.Tests.Utility
{
    [TestClass]
    public class AsciiManipulatorTests
    {
        [DataRow('A', 0)]
        [DataRow('a', 0)]
        [DataRow('Z', 25)]
        [DataRow('z', 25)]
        [DataRow('B', 1)]
        [DataRow('b', 1)]
        [DataTestMethod]
        public void NormalizeLetter_ReturnNormalizedLetterCode_WhenInputIsLetter(char givenLetter, int expectedNormalizedCode)
        {
            int actual = AsciiManipulator.NormalizeLetter(givenLetter);

            Assert.AreEqual(expectedNormalizedCode, actual);
        }

        [DataRow('%', "Argument has to be a letter.")]
        [DataRow('1', "Argument has to be a letter.")]
        [DataTestMethod]
        public void NormalizeLetter_ThrowArgumentException_WhenArgumentIsNotLetter(char givenInput, string expectedMessage)
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => AsciiManipulator.NormalizeLetter(givenInput));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [DataRow(0, true, 'A')]
        [DataRow(0, false, 'a')]
        [DataRow(25, true, 'Z')]
        [DataRow(25, false, 'z')]
        [DataRow(1, true, 'B')]
        [DataRow(1, false, 'b')]
        [DataTestMethod]
        public void ReverseToLetter_ReturnA_WhenLetterIndexIs0AndLetterIsUpper(int givenNormalizedCode, bool isUpper, char expectedLetter)
        {
            char actual = AsciiManipulator.ReverseToLetter(givenNormalizedCode, isUpper);

            Assert.AreEqual(expectedLetter, actual);
        }
    }
}