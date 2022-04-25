using Ciphers.Interfaces;
using Ciphers.Utility;
using System;
using System.Numerics;
using System.Text;

namespace Ciphers.Ciphers
{
    public class RSACipher : IAsymmetricCipher
    {
        public Key PublicKey { get; private set; }
        private Key _privateKey;

        public RSACipher()
        {
            int p = RSAHelper.GeneratePrimeNumberUpTo(100);
            int q = GenerateDistinctPrimeNumber(p);

            int n = p * q;
            int lcm = RSAHelper.CalculateLeastCommonMultiple(p - 1, q - 1);
            int e = GenerateExponent(lcm);

            int d = RSAHelper.FindModularMultiplicativeInverse(e, lcm);

            PublicKey = new Key(e, n);
            _privateKey = new Key(d, n);
        }

        public RSACipher(int e, int d, int n)
        {
            PublicKey = new Key(e, n);
            _privateKey = new Key(d, n);
        }

        private int GenerateDistinctPrimeNumber(int p)
        {
            int result = p;

            while (result == p)
            {
                result = RSAHelper.GeneratePrimeNumberUpTo(100);
            }

            return result;
        }

        private int GenerateExponent(int lcm)
        {
            int result = 2;

            while (result < lcm)
            {
                if (RSAHelper.CalculateGreatestCommonDivisor(result, lcm) == 1)
                {
                    break;
                }

                result++;
            }

            return result;
        }

        public string Encrypt(string plainText, Key publicKey)
        {
            StringBuilder cipherText = new StringBuilder(plainText.Length);

            for (int i = 0; i < plainText.Length; i++)
            {
                cipherText.Append(EncryptCharacter(plainText[i], publicKey));
            }

            return cipherText.ToString();
        }

        private char EncryptCharacter(char character, Key key)
        {
            int unicode = character;
            int encryptedCode = E(unicode, key);
            char encryptedCharacter = (char)encryptedCode;

            return encryptedCharacter;
        }

        private int E(int value, Key key)
        {
            BigInteger bigValue = value;
            BigInteger exponent = key.Exponent;
            BigInteger modulus = key.Modulus;
            BigInteger result = BigInteger.ModPow(bigValue, exponent, modulus);

            return (int)result;
        }

        public string Decrypt(string cipherText)
        {
            StringBuilder plainText = new StringBuilder(cipherText.Length);

            for (int i = 0; i < cipherText.Length; i++)
{
                plainText.Append(DecryptCharacter(cipherText[i]));
            }

            return plainText.ToString();
        }

        private char DecryptCharacter(char character)
        {
            int unicode = character;
            int decryptedCode = D(unicode);
            char decryptedCharacter = (char)decryptedCode;

            return decryptedCharacter;
        }

        private int D(int value)
        {
            BigInteger bigValue = value;
            BigInteger exponent = _privateKey.Exponent;
            BigInteger modulus = _privateKey.Modulus;
            BigInteger result = BigInteger.ModPow(bigValue, exponent, modulus);

            return (int)result;
        }
    }
}
