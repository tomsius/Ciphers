using Ciphers.Interfaces;
using System;

namespace Ciphers.Utility
{
    public class RandomNumberGeneratorHelper : IRandomHelper
    {
        private readonly Random _random;

        public RandomNumberGeneratorHelper()
        {
            _random = new Random();
        }

        public int Next(int max)
{
            return _random.Next(max);
        }
    }
}
