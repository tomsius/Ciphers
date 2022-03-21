using System;

namespace Ciphers.Utility
{
    public static class AsciiManipulator
    {
        private const int UpperLetterAsciiStart = 'A';
        private const int LowerLetterAsciiStart = 'a';

        public static int NormalizeLetter(char letter)
        {
            if (!char.IsLetter(letter))
            {
                throw new ArgumentException("Argument has to be a letter.");
            }

            if (char.IsUpper(letter))
            {
                return NormalizeUpperLetter(letter);
            }

            return NormalizeLowerLetter(letter);
        }

        private static int NormalizeUpperLetter(char letter)
        {
            return letter - UpperLetterAsciiStart;
        }

        private static int NormalizeLowerLetter(char letter)
        {
            return letter - LowerLetterAsciiStart;
        }

        public static char ReverseToLetter(int letterIndex, bool isUpper)
        {
            if (isUpper)
            {
                return ReverseToUpperLetter(letterIndex);
            }

            return ReverseToLowerLetter(letterIndex);
        }

        private static char ReverseToUpperLetter(int letterIndex)
        {
            return (char)(letterIndex + UpperLetterAsciiStart);
        }

        private static char ReverseToLowerLetter(int letterIndex)
        {
            return (char)(letterIndex + LowerLetterAsciiStart);
        }
    }
}
