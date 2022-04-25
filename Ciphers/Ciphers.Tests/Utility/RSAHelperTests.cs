using Ciphers.Ciphers;
using Ciphers.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ciphers.Tests.Utility
{
    [TestClass]
    public class RSAHelperTests
    {
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(100)]
        [DataTestMethod]
        public void GeneratePrimeNumberUpTo_ReturnRandomPrimeNumber_WhenInitialMaximumRandomNumberIsGiven(int maximumNumber)
        {
            for (int k = 0; k < 1000; k++)
            {
                int actual = RSAHelper.GeneratePrimeNumberUpTo(maximumNumber);

                for (int i = 2; i * i < actual; i++)
                {
                    Assert.IsFalse(actual % i == 0);
                } 
            }
        }

        [DataRow(6, 21, 42)]
        [DataRow(21, 6, 42)]
        [DataRow(48, 180, 720)]
        [DataRow(180, 48, 720)]
        [DataRow(0, 0, 0)]
        [DataTestMethod]
        public void CalculateLeastCommonMultiple_ReturnLeastCommonMultiple_WhenPairOfIntegerValuesIsGiven(int a, int b, int expectedLCM)
        {
            int actual = RSAHelper.CalculateLeastCommonMultiple(a, b);

            Assert.AreEqual(expectedLCM, actual);
        }

        [DataRow(54, 24, 6)]
        [DataRow(24, 54, 6)]
        [DataRow(48, 180, 12)]
        [DataRow(180, 48, 12)]
        [DataRow(48, 18, 6)]
        [DataRow(18, 48, 6)]
        [DataTestMethod]
        public void CalculateGreatestCommonDivisor_ReturnGreatestCommonDivisor_WhenPairOfIntegerValuesIsGiven(int a, int b, int expectedGCD)
        {
            int actual = RSAHelper.CalculateGreatestCommonDivisor(a, b);

            Assert.AreEqual(expectedGCD, actual);
        }

        [DataRow(17, 780, 413)]
        [DataRow(3, 10, 7)]
        [DataTestMethod]
        public void FindModularMultiplicativeInverse_ReturnModularMultiplicativeInverse_WhenExponentAndLeastCommonMultipleAreGiven(int e, int lcm, int expectedResult)
        {
            int actual = RSAHelper.FindModularMultiplicativeInverse(e, lcm);

            Assert.AreEqual(expectedResult, actual);
        }
    }
}
