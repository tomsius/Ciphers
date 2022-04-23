using System;
using System.Collections.Generic;
using System.Text;

namespace Ciphers.Utility
{
    public static class HillHelper
    {
        public static int[,] ConvertTextToVector(string text, int neededLength)
        {
            string normalizedText = TextHelper.NormalizeText(text);
            int[,] vector = new int[neededLength, 1];
            int i = 0;

            while (i < normalizedText.Length && i < neededLength)
            {
                vector[i, 0] = EncodeLetter(normalizedText[i]);
                i++;
            }

            while (i < neededLength)
            {
                vector[i, 0] = 'Z' - 'A';
                i++;
            }

            return vector;
        }

        private static int EncodeLetter(char letter)
        {
            if (char.IsUpper(letter))
            {
                return letter - 'A';
            }
            else
            {
                return letter - 'a';
            }
        }

        public static string ConvertVectorToText(int[,] matrix)
        {
            StringBuilder text = new StringBuilder(matrix.GetLength(0));

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    char letter = (char)(matrix[i, j] + 'A');
                    text.Append(letter);
                }
            }

            return text.ToString();
        }
    }
}
