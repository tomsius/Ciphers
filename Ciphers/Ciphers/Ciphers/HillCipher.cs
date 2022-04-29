using Ciphers.Interfaces;
using Ciphers.Utility;

namespace Ciphers.Ciphers
{
    public class HillCipher : ISymmetricCipher
    {
        private const int LetterCount = 'z' - 'a' + 1;
        private readonly IRandomHelper _random;
        private readonly int[,] _encryptionKeyMatrix;
        private readonly int[,] _decryptionKeyMatrix;

        public HillCipher(int messageMaximumLength, IRandomHelper? random = null)
        {
            if (random is null)
            {
                _random = new RandomNumberGeneratorHelper();
            }
            else
            {
                _random = random;
            }
            
            _encryptionKeyMatrix = GenerateEncryptionKeyMatrix(messageMaximumLength);
            _decryptionKeyMatrix = GenerateDecryptionKeyMatrix();
        }

        private int[,] GenerateEncryptionKeyMatrix(int n)
        {
            int[,] keyMatrix = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    keyMatrix[i, j] = _random.Next('z' - 'a' + 1);
                }
            }

            return keyMatrix;
        }

        private int[,] GenerateDecryptionKeyMatrix()
        {
            int determinant = GetDeterminant(_encryptionKeyMatrix, _encryptionKeyMatrix.GetLength(0));
            var adjoint = GetAdjoint(_encryptionKeyMatrix);
            int x = FindFactor(determinant);
            int[,] inverseMatrix = GetInverseMatrix(adjoint, x);

            return inverseMatrix;
        }

        private int GetDeterminant(int[,] matrix, int n)
        {
            int determinant = 0;

            if (n == 1)
            {
                return matrix[0, 0];
            }

            int[,] temp = new int[_encryptionKeyMatrix.GetLength(0), _encryptionKeyMatrix.GetLength(1)];
            int sign = 1;

            for (int i = 0; i < n; i++)
            {
                CalculateCofactors(matrix, temp, 0, i, n);
                determinant += sign * matrix[0, i] * GetDeterminant(temp, n - 1);
                sign *= -1;
            }

            return determinant;
        }

        private void CalculateCofactors(int[,] matrix, int[,] temp, int p, int q, int n)
        {
            int i = 0;
            int j = 0;

            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    if (row != p && col != q)
                    {
                        temp[i, j++] = matrix[row, col];

                        if (j == n - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
        }

        private int[,] GetAdjoint(int[,] matrix)
        {
            int[,] adjoint = new int[matrix.GetLength(0), matrix.GetLength(1)];
            int[,] temp = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    CalculateCofactors(matrix, temp, i, j, matrix.GetLength(0));
                    int sign = (i + j) % 2 == 0 ? 1 : -1;
                    adjoint[i, j] = sign * GetDeterminant(temp, matrix.GetLength(0) - 1);
                }
            }

            adjoint = Transpose(adjoint);

            return adjoint;
        }

        private int[,] Transpose(int[,] adjoint)
        {
            int[,] result = new int[adjoint.GetLength(0), adjoint.GetLength(1)];

            for (int i = 0; i < adjoint.GetLength(0); i++)
            {
                for (int j = 0; j < adjoint.GetLength(1); j++)
                {
                    result[i, j] = adjoint[j, i];
                }
            }

            return result;
        }

        private int FindFactor(int d)
        {
            int i = 1;
            while ((i * d - 1) % LetterCount != 0)
            {
                i++;
            }

            return i;
        }

        private int[,] GetInverseMatrix(int[,] matrix, int x)
        {
            int[,] result = new int[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int digit = x * matrix[i, j] % LetterCount;

                    if (digit < 0)
                    {
                        digit = LetterCount + digit;
                    }

                    result[i, j] = digit;
                }
            }

            return result;
        }

        public string Encrypt(string plainText)
        {
            int[,] plainTextVector = HillHelper.ConvertTextToVector(plainText, _encryptionKeyMatrix.GetLength(0));
            int[,] cipherMatrix = GenerateOutputMatrix(plainTextVector, _encryptionKeyMatrix);
            string cipherText = HillHelper.ConvertVectorToText(cipherMatrix);

            return cipherText;
        }

        private int[,] GenerateOutputMatrix(int[,] inpuVvector, int[,] keyMatrix)
        {
            int[,] cipherMatrix = new int[keyMatrix.GetLength(0), 1];

            for (int i = 0; i < cipherMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < cipherMatrix.GetLength(1); j++)
                {
                    cipherMatrix[i, j] = 0;

                    for (int k = 0; k < cipherMatrix.GetLength(0); k++)
                    {
                        cipherMatrix[i, j] += keyMatrix[i, k] * inpuVvector[k, j];
                    }

                    cipherMatrix[i, j] %= LetterCount;
                }
            }

            return cipherMatrix;
        }

        public string Decrypt(string cipherText)
        {
            int[,] cipherTextVector = HillHelper.ConvertTextToVector(cipherText, _decryptionKeyMatrix.GetLength(0));
            int[,] plainTextMatrix = GenerateOutputMatrix(cipherTextVector, _decryptionKeyMatrix);
            string plainText = HillHelper.ConvertVectorToText(plainTextMatrix);

            return plainText;
        }
    }
}
