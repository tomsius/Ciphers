using Ciphers.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Ciphers.Tests.Utility
{
    [TestClass]
    public class HillHelperTests
    {
        [DynamicData(nameof(GetConvertTextToVectorData), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void ConvertTextToVector_ReturnLetterCodeVector_WhenInputIsText(string inputText, int maxLength, int[,] expectedVector)
        {
            int[,] actual = HillHelper.ConvertTextToVector(inputText, maxLength);

            Assert.AreEqual(expectedVector.Length, actual.Length);
            for (int i = 0; i < actual.GetLength(0); i++)
            {
                for (int j = 0; j < actual.GetLength(1); j++)
                {
                    Assert.AreEqual(expectedVector[i, j], actual[i, j]);
                }
            }
        }

        private static IEnumerable<object[]> GetConvertTextToVectorData()
        {
            return new List<object[]>()
                {
                    new object[] { "ABC", 3, new int[,] { { 0 }, { 1 }, { 2 } } },
                    new object[] { "AB", 3, new int[,] { { 0 }, { 1 }, { 25 } } },
                    new object[] { "AbC", 3, new int[,] { { 0 }, { 1 }, { 2 } } },
                    new object[] { "Ab1", 3, new int[,] { { 0 }, { 1 }, { 25 } } }
                };
        }

        [DynamicData(nameof(GetConvertVectorToTextData), DynamicDataSourceType.Method)]
        [DataTestMethod]
        public void ConvertVectorToText_ReturnText_WhenInputIsLetterCodeVector(int[,] inputVector, string expectedText)
        {
            string actual = HillHelper.ConvertVectorToText(inputVector);

            Assert.AreEqual(expectedText, actual);
        }

        private static IEnumerable<object[]> GetConvertVectorToTextData()
        {
            return new List<object[]>()
                {
                    new object[] { new int[,] { { 0 }, { 1 }, { 2 } }, "ABC" },
                    new object[] { new int[,] { { 0 }, { 1 }, { 25 } }, "ABZ" },
                    new object[] { new int[,] { }, "" }
                };
        }
    }
}
