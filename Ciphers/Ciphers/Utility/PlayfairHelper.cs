using System;
using System.Text;

namespace Ciphers.Utility
{
    public static class PlayfairHelper
    {
        public static char GetAvailableLetter(char letter)
        {
            if (!char.IsLetter(letter))
            {
                throw new ArgumentException("Argument has to be a letter.");
            }

            char availableLetter = letter == 'J' ? 'I' : letter;

            return availableLetter;
        }

        public static string NormalizeText(string plainText)
        {
            StringBuilder normalizedText = new StringBuilder(plainText.Length);

            foreach (var character in plainText)
            {
                if (char.IsLetter(character))
                {
                    normalizedText.Append(GetAvailableLetter(character));
                }
            }

            return normalizedText.ToString();
        }

        public static (int, int) GetPositionOfLetter(char[,] matrix, char letter)
        {
            int foundRow = -1;
            int foundColumn = -1;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == letter)
                    {
                        foundRow = row;
                        foundColumn = col;
                    }
                }
            }

            return (foundRow, foundColumn);
        }
    }
}
