using System.Text;
using Ciphers.Interfaces;
using Ciphers.Utility;

namespace Ciphers.Ciphers
{
    public class CaesarCipher : ISymmetricCipher
    {
        private const int LetterCount = 'z' - 'a' + 1;

        private readonly int _key;

        public CaesarCipher(int key)
        {
            _key = key % 26;
        }

        public string Encrypt(string plainText)
        {
            StringBuilder cipherText = new StringBuilder(plainText);

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (char.IsLetter(cipherText[i]))
                {
                    cipherText[i] = EncryptLetter(cipherText[i]);
                }
            }

            return cipherText.ToString();
        }

        private char EncryptLetter(char letter)
        {
            bool isLetterUpper = char.IsUpper(letter);
            int normalizedLetterCode = AsciiManipulator.NormalizeLetter(letter);
            int encryptedLetterCode = E(normalizedLetterCode);
            char encryptedLetter = AsciiManipulator.ReverseToLetter(encryptedLetterCode, isLetterUpper);

            return encryptedLetter;
        }

        private int E(int letterId)
        {
            int encryptedLetterId = (letterId + _key) % LetterCount;
            encryptedLetterId = encryptedLetterId >= 0 ? encryptedLetterId : encryptedLetterId + 26;

            return encryptedLetterId;
        }

        public string Decrypt(string cipherText)
        {
            StringBuilder plainText = new StringBuilder(cipherText);

            for (int i = 0; i < plainText.Length; i++)
            {
                if (char.IsLetter(plainText[i]))
                {
                    plainText[i] = DecryptLetter(plainText[i]);
                }
            }

            return plainText.ToString();
        }

        private char DecryptLetter(char letter)
        {
            bool isLetterUpper = char.IsUpper(letter);
            int normalizedLetterCode = AsciiManipulator.NormalizeLetter(letter);
            int decryptedLetterCode = D(normalizedLetterCode);
            char decryptedLetter = AsciiManipulator.ReverseToLetter(decryptedLetterCode, isLetterUpper);

            return decryptedLetter;
        }

        private int D(int letterId)
        {
            int decryptedLetterId = (letterId - _key) % LetterCount;
            decryptedLetterId = decryptedLetterId >= 0 ? decryptedLetterId : decryptedLetterId + 26;

            return decryptedLetterId;
        }
    }
}
