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
            int normalizedLetterCode = AsciiManipulator.GetNormalizedLetterCode(letter);
            int encryptedLetterCode = E(normalizedLetterCode);
            char encryptedLetter = AsciiManipulator.GetLetterByNormalizedCode(encryptedLetterCode, isLetterUpper);

            return encryptedLetter;
        }

        private int E(int letterId)
        {
            int encryptedLetterId = (letterId + _key) % LetterCount;

            if (encryptedLetterId < 0)
            {
                encryptedLetterId += 26;
            }

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
            int normalizedLetterCode = AsciiManipulator.GetNormalizedLetterCode(letter);
            int decryptedLetterCode = D(normalizedLetterCode);
            char decryptedLetter = AsciiManipulator.GetLetterByNormalizedCode(decryptedLetterCode, isLetterUpper);

            return decryptedLetter;
        }

        private int D(int letterId)
        {
            int decryptedLetterId = (letterId - _key) % LetterCount;

            if (decryptedLetterId < 0)
            {
                decryptedLetterId += 26;
            }

            return decryptedLetterId;
        }
    }
}
