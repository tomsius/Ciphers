using Ciphers.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ciphers.Tests.Utility
{
    [TestClass]
    public class PlayfairHelperTests
    {
        private readonly char[,] _keyMatrix;
        public PlayfairHelperTests()
        {
            _keyMatrix = new char[2, 2];
            _keyMatrix[0, 0] = 'A';
            _keyMatrix[0, 1] = 'B';
            _keyMatrix[1, 0] = 'C';
            _keyMatrix[1, 1] = 'D';
        }

        [DataRow('%', "Argument has to be a letter.")]
        [DataRow('1', "Argument has to be a letter.")]
        [DataTestMethod]
        public void GetAvailableLetter_ThrowArgumentException_WhenArgumentIsNotLetter(char givenInput, string expectedMessage)
        {
            var ex = Assert.ThrowsException<ArgumentException>(() => PlayfairHelper.GetAvailableLetter(givenInput));

            Assert.AreEqual(expectedMessage, ex.Message);
        }

        [DataRow('A', 'A')]
        [DataRow('a', 'a')]
        [DataRow('I', 'I')]
        [DataRow('J', 'I')]
        [DataTestMethod]
        public void GetAvailableLetter_ReturnAvailableLetter_WhenInputIsLetter(char givenLetter, char expectedLetter)
        {
            char actual = PlayfairHelper.GetAvailableLetter(givenLetter);

            Assert.AreEqual(expectedLetter, actual);
        }

        [DataRow("AbC", "AbC")]
        [DataRow("a123b", "ab")]
        [DataRow("a b=1c", "abc")]
        [DataTestMethod]
        public void NormalizeText_ReturnTextWithLettersOnly_WhenInputIsText(string givenText, string expectedNormalizedText)
        {
            string actual = PlayfairHelper.NormalizeText(givenText);

            Assert.AreEqual(expectedNormalizedText, actual);
        }

        [DataRow('A', 0, 0)]
        [DataRow('B', 0, 1)]
        [DataRow('C', 1, 0)]
        [DataRow('D', 1, 1)]
        [DataRow('E', -1, -1)]
        [DataTestMethod]
        public void GetPositionOfLetter_ReturnTupleForPositionOfLetterInMatrix_When(char letterToSearch, int expectedRow, int expectedCol)
        {
            (int actualRow, int actualCol) = PlayfairHelper.GetPositionOfLetter(_keyMatrix, letterToSearch);

            Assert.AreEqual(expectedRow, actualRow);
            Assert.AreEqual(expectedCol, actualCol);
        }
    }
}