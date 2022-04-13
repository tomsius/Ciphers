using Ciphers.Interfaces;
using Ciphers.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ciphers.Ciphers
{
    public class PlayfairCipher : ISymmetricCipher
    {
        private const int ROW_SIZE = 5;
        private const int COL_SIZE = 5;
        private HashSet<char> _uniqueLetters;
        private readonly string _key;

        // TODO - iskelti keyMatrix i atskira metoda ir generuoti tik viena karta
        public PlayfairCipher(string key)
        {
            _key = key.ToUpper();
            _uniqueLetters = new HashSet<char>(ROW_SIZE * COL_SIZE);
        }

        public string Encrypt(string plainText)
        {
            char[,] keyMatrix = GenerateKeyMatrix();
            List<string> digraphs = GetDigraphs(plainText.ToUpper());
            StringBuilder cipherText = new StringBuilder();

            foreach (var digraph in digraphs)
            {
                char firstLetter = digraph[0];
                (int row, int col) firstPos = PlayfairHelper.GetPositionOfLetter(keyMatrix, firstLetter);

                char secondLetter = digraph[1];
                (int row, int col) secondPos = PlayfairHelper.GetPositionOfLetter(keyMatrix, secondLetter);

                char firstEncryptedLetter;
                char secondEncryptedLetter;

                if (firstPos.col == secondPos.col)
                {
                    firstEncryptedLetter = EncryptLetterInColumn(keyMatrix, firstPos);
                    secondEncryptedLetter = EncryptLetterInColumn(keyMatrix, secondPos);
                }
                else if (firstPos.row == secondPos.row)
                {
                    firstEncryptedLetter = EncryptLetterInRow(keyMatrix, firstPos);
                    secondEncryptedLetter = EncryptLetterInRow(keyMatrix, secondPos);
                }
                else
                {
                    char[] encryptedLetters = GetHorizontallyOppositeLetters(keyMatrix, firstPos, secondPos);

                    firstEncryptedLetter = encryptedLetters[0];
                    secondEncryptedLetter = encryptedLetters[1];
                }

                cipherText.Append(firstEncryptedLetter);
                cipherText.Append(secondEncryptedLetter);
            }

            return cipherText.ToString();
        }

        private char[,] GenerateKeyMatrix()
        {
            char[,] partialKeyMatrix = FillMatrixWithKey();
            char[,] fullKeyMatrix = CompleteKeyMatrix(partialKeyMatrix);

            return fullKeyMatrix;
        }

        private char[,] FillMatrixWithKey()
        {
            char[,] matrix = new char[ROW_SIZE, COL_SIZE];
            int row = 0;
            int col = 0;

            for (int i = 0; i < _key.Length; i++)
            {
                char letter = PlayfairHelper.GetAvailableLetter(_key[i]);

                if (_uniqueLetters.Add(letter))
                {
                    matrix[row, col] = letter;
                    col = (col + 1) % COL_SIZE;

                    if (col == 0)
                    {
                        row++;
                    }
                }
            }

            return matrix;
        }

        private char[,] CompleteKeyMatrix(char[,] partialKeyMatrix)
        {
            char[,] matrix = new char[ROW_SIZE, COL_SIZE];
            Array.Copy(partialKeyMatrix, matrix, ROW_SIZE * COL_SIZE);
            int row = _uniqueLetters.Count / COL_SIZE;
            int col = _uniqueLetters.Count % COL_SIZE;

            for (char c = 'A'; c <= 'Z'; c++)
            {
                char letter = PlayfairHelper.GetAvailableLetter(c);

                if (_uniqueLetters.Add(letter))
                {
                    matrix[row, col] = letter;
                    col = (col + 1) % COL_SIZE;

                    if (col == 0)
                    {
                        row++;
                    }
                }
            }

            return matrix;
        }

        private List<string> GetDigraphs(string plainText)
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
            int newCol = (pos.col + 1) % COL_SIZE;

            return keyMatrix[pos.row, newCol];
        }

        private char EncryptLetterInColumn(char[,] keyMatrix, (int row, int col) pos)
        {
            int newRow = (pos.row + 1) % ROW_SIZE;

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
            char[,] keyMatrix = GenerateKeyMatrix();
            List<string> digraphs = GetDigraphs(cipherText.ToUpper());
            StringBuilder plainText = new StringBuilder();

            foreach (var digraph in digraphs)
            {
                char firstLetter = digraph[0];
                (int row, int col) firstPos = PlayfairHelper.GetPositionOfLetter(keyMatrix, firstLetter);

                char secondLetter = digraph[1];
                (int row, int col) secondPos = PlayfairHelper.GetPositionOfLetter(keyMatrix, secondLetter);

                char firstDecryptedLetter;
                char secondDecryptedLetter;

                if (firstPos.col == secondPos.col)
                {
                    firstDecryptedLetter = DecryptLetterInColumn(keyMatrix, firstPos);
                    secondDecryptedLetter = DecryptLetterInColumn(keyMatrix, secondPos);
                }
                else if (firstPos.row == secondPos.row)
                {
                    firstDecryptedLetter = DecryptLetterInRow(keyMatrix, firstPos);
                    secondDecryptedLetter = DecryptLetterInRow(keyMatrix, secondPos);
                }
                else
                {
                    char[] encryptedLetters = GetHorizontallyOppositeLetters(keyMatrix, firstPos, secondPos);

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
                newCol += COL_SIZE;
            }

            return keyMatrix[pos.row, newCol];
        }

        private char DecryptLetterInColumn(char[,] keyMatrix, (int row, int col) pos)
        {
            int newRow = pos.row - 1;

            if (newRow < 0)
            {
                newRow += ROW_SIZE;
            }

            return keyMatrix[newRow, pos.col];
        }
    }
}
