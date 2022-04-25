using System;

namespace Ciphers.Utility
{
    public static class RSAHelper
    {
        public static int GeneratePrimeNumberUpTo(int maxRandom)
        {
            Random random = new Random();
            int number = random.Next(maxRandom);

            while (!IsPrime(number))
            {
                number++;
            }

            return number;
        }

        private static bool IsPrime(int n)
        {
            if (n == 2 || n == 3)
            {
                return true;
            }

            if (n <= 1 || n % 2 == 0 || n % 3 == 0)
            {
                return false;
            }

            for (int i = 5; i * i <= n; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int CalculateLeastCommonMultiple(int a, int b)
        {
            if (a == 0 && b == 0)
            {
                return 0;
            }

            int gcd = CalculateGreatestCommonDivisor(a, b);
            return Math.Abs(a) * (Math.Abs(b) / gcd);
        }

        public static int CalculateGreatestCommonDivisor(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }

            int gcd = (b == 0) ? a : b;
            return gcd;
        }

        public static int FindModularMultiplicativeInverse(int e, int lcm)
        {
            int result = 1;

            while ((e * result) % lcm != 1)
            {
                result++;
            }

            return result;
        }
    }
}
