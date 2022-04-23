using Ciphers.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ciphers.Tests.Utility
{
    [TestClass]
    public class RandomNumberGeneratorHelperTests
    {
        [DataRow(26)]
        [DataRow(3)]
        [DataRow(1)]
        [DataTestMethod]
        public void ConvertTextToVector_ReturnLetterCodeVector_WhenInputIsText(int maximumValue)
        {
            RandomNumberGeneratorHelper helper = new();

            for (int i = 0; i < 10000; i++)
            {
                int actual = helper.Next(maximumValue);

                Assert.IsTrue(actual >= 0, $"Not true: {actual} >= 0");
                Assert.IsTrue(actual < maximumValue, $"Not true: {actual} < {maximumValue}");
            }
        }
    }
}
