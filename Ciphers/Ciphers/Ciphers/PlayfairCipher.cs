using Ciphers.Interfaces;
using Ciphers.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciphers.Ciphers
{
    public class PlayfairCipher : ISymmetricCipher
    {
        private const int RowSize = 5;
        private const int ColSize = 5;
        private readonly char[,] _keyMatrix;

        public PlayfairCipher(string key)
        {
            _keyMatrix = GenerateKeyMatrix(key.ToUpper());
        }

        private char[,] GenerateKeyMatrix(string key)
        {
            HashSet<char> uniqueLetters = new HashSet<char>(RowSize * ColSize);
            char[,] partialKeyMatrix = FillMatrixWithKey(key, uniqueLetters);
            char[,] fullKeyMatrix = CompleteKeyMatrix(partialKeyMatrix, uniqueLetters);

            return fullKeyMatrix;
        }

        private char[,] FillMatrixWithKey(string key, HashSet<char> uniqueLetters)
        {
            char[,] matrix = new char[RowSize, ColSize];
            int row = 0;
            int col = 0;

            for (int i = 0; i < key.Length; i++)
            {
                char letter = PlayfairHelper.GetAvailableLetter(key[i]);

                if (uniqueLetters.Add(letter))
                {
                    matrix[row, col] = letter;
                    col = (col + 1) % ColSize;

                    if (col == 0)
                    {
                        row++;
                    }
                }
            }

            return matrix;
        }

        private char[,] CompleteKeyMatrix(char[,] partialKeyMatrix, HashSet<char> uniqueLetters)
        {
            char[,] matrix = new char[RowSize, ColSize];
            Array.Copy(partialKeyMatrix, matrix, RowSize * ColSize);
            int row = uniqueLetters.Count / ColSize;
            int col = uniqueLetters.Count % ColSize;

            for (char c = 'A'; c <= 'Z'; c++)
            {
                char letter = PlayfairHelper.GetAvailableLetter(c);

                if (uniqueLetters.Add(letter))
                {
                    matrix[row, col] = letter;
                    col = (col + 1) % ColSize;

                    if (col == 0)
                    {
                        row++;
                    }
                }
            }

            return matrix;
        }

        public string Encrypt(string plainText)
        {
            List<string> digrams = GetDigrams(plainText.ToUpper());
            StringBuilder cipherText = new StringBuilder();

            foreach (var digram in digrams)
            {
                char firstLetter = digram[0];
                (int row, int col) firstPos = PlayfairHelper.GetPositionOfLetter(_keyMatrix, firstLetter);

                char secondLetter = digram[1];
                (int row, int col) secondPos = PlayfairHelper.GetPositionOfLetter(_keyMatrix, secondLetter);

                char firstEncryptedLetter;
                char secondEncryptedLetter;

                if (firstPos.col == secondPos.col)
                {
                    firstEncryptedLetter = EncryptLetterInColumn(_keyMatrix, firstPos);
                    secondEncryptedLetter = EncryptLetterInColumn(_keyMatrix, secondPos);
                }
                else if (firstPos.row == secondPos.row)
                {
                    firstEncryptedLetter = EncryptLetterInRow(_keyMatrix, firstPos);
                    secondEncryptedLetter = EncryptLetterInRow(_keyMatrix, secondPos);
                }
                else
                {
                    char[] encryptedLetters = GetHorizontallyOppositeLetters(_keyMatrix, firstPos, secondPos);

                    firstEncryptedLetter = encryptedLetters[0];
                    secondEncryptedLetter = encryptedLetters[1];
                }

                cipherText.Append(firstEncryptedLetter);
                cipherText.Append(secondEncryptedLetter);
            }

            return cipherText.ToString();
        }

        private List<string> GetDigrams(string plainText)
        {
            List<string> result = new List<string>();
            string normalizedText = PlayfairHelper.NormalizeText(plainText);
            char[] pair = new char[2];
            int index = 0;

            while (index < normalizedText.Length)
            {
                if (index + 1 == normalizedText.Length)
                {
                    pair[0] = normalizedText[index];
                    pair[1] = 'Z';
                    index++;
                }
                else if (normalizedText[index] != normalizedText[index + 1])
                {
                    pair[0] = normalizedText[index];
                    pair[1] = normalizedText[index + 1];
                    index += 2;
                }
                else
                {
                    pair[0] = normalizedText[index];
                    pair[1] = 'X';
                    index++;
                }
                
                result.Add(new string(pair));
            }

            return result;
        }

        private char EncryptLetterInRow(char[,] keyMatrix, (int row, int col) pos)
        {
            int newCol = (pos.col + 1) % ColSize;

            return keyMatrix[pos.row, newCol];
        }

        private char EncryptLetterInColumn(char[,] keyMatrix, (int row, int col) pos)
        {
            int newRow = (pos.row + 1) % RowSize;

            return keyMatrix[newRow, pos.col];
        }

        private char[] GetHorizontallyOppositeLetters(char[,] keyMatrix, (int row, int col) firstPos, (int row, int col) secondPos)
        {
            char[] encryptedLetters = new char[2];

            encryptedLetters[0] = keyMatrix[firstPos.row, secondPos.col];
            encryptedLetters[1] = keyMatrix[secondPos.row, firstPos.col];

            return encryptedLetters;
        }

        public string Decrypt(string cipherText)
        {
            List<string> digraphs = GetDigrams(cipherText.ToUpper());
            StringBuilder plainText = new StringBuilder();

            foreach (var digraph in digraphs)
            {
                char firstLetter = digraph[0];
                (int row, int col) firstPos = PlayfairHelper.GetPositionOfLetter(_keyMatrix, firstLetter);

                char secondLetter = digraph[1];
                (int row, int col) secondPos = PlayfairHelper.GetPositionOfLetter(_keyMatrix, secondLetter);

                char firstDecryptedLetter;
                char secondDecryptedLetter;

                if (firstPos.col == secondPos.col)
                {
                    firstDecryptedLetter = DecryptLetterInColumn(_keyMatrix, firstPos);
                    secondDecryptedLetter = DecryptLetterInColumn(_keyMatrix, secondPos);
                }
                else if (firstPos.row == secondPos.row)
                {
                    firstDecryptedLetter = DecryptLetterInRow(_keyMatrix, firstPos);
                    secondDecryptedLetter = DecryptLetterInRow(_keyMatrix, secondPos);
                }
                else
                {
                    char[] encryptedLetters = GetHorizontallyOppositeLetters(_keyMatrix, firstPos, secondPos);

                    firstDecryptedLetter = encryptedLetters[0];
                    secondDecryptedLetter = encryptedLetters[1];
                }

                plainText.Append(firstDecryptedLetter);
                plainText.Append(secondDecryptedLetter);
            }

            return plainText.ToString();
        }

        private char DecryptLetterInRow(char[,] keyMatrix, (int row, int col) pos)
        {
            int newCol = pos.col - 1;

            if (newCol < 0)
            {
                newCol += ColSize;
            }

            return keyMatrix[pos.row, newCol];
        }

        private char DecryptLetterInColumn(char[,] keyMatrix, (int row, int col) pos)
        {
            int newRow = pos.row - 1;

            if (newRow < 0)
            {
                newRow += RowSize;
            }

            return keyMatrix[newRow, pos.col];
        }
    }
}
