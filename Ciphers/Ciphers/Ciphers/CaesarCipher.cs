using System.Text;
using Ciphers.Interfaces;
using Ciphers.Utility;

namespace Ciphers.Caesar
{
    public class CaesarCipher : ISymmetricCipher
    {
        private const int LETTER_COUNT = 'z' - 'a' + 1;

        private readonly int key;

        public CaesarCipher(int key)
        {
            this.key = key % 26;
        }

        public string Encrypt(string plainText)
        {
            StringBuilder sb = new StringBuilder(plainText);

            for (int i = 0; i < sb.Length; i++)
            {
                if (char.IsLetter(sb[i]))
                {
                    sb[i] = EncryptLetter(sb[i]);
                }
            }

            return sb.ToString();
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
            int encryptedLetterId = (letterId + key) % LETTER_COUNT;
            encryptedLetterId = encryptedLetterId >= 0 ? encryptedLetterId : encryptedLetterId + 26;

            return encryptedLetterId;
        }

        public string Decrypt(string cipherText)
        {
            StringBuilder sb = new StringBuilder(cipherText);

            for (int i = 0; i < sb.Length; i++)
            {
                if (char.IsLetter(sb[i]))
                {
                    sb[i] = DecryptLetter(sb[i]);
                }
            }

            return sb.ToString();
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
            int decryptedLetterId = (letterId - key) % LETTER_COUNT;
            decryptedLetterId = decryptedLetterId >= 0 ? decryptedLetterId : decryptedLetterId + 26;

            return decryptedLetterId;
        }
    }
}
