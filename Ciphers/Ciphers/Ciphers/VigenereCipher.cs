using Ciphers.Interfaces;
using Ciphers.Utility;
using System.Text;

namespace Ciphers.Ciphers
{
    public class VigenereCipher : ISymmetricCipher
    {
        private const int LETTER_COUNT = 'z' - 'a' + 1;

        private readonly string _key;

        public VigenereCipher(string key)
        {
            _key = key;
        }

        public string Encrypt(string plainText)
        {
            string generatedKey = GenerateKey(plainText.Length);
            StringBuilder cipherText = new StringBuilder(plainText.Length);

            for (int i = 0; i < plainText.Length; i++)
            {
                char encryptedLetter = plainText[i];
                if (char.IsLetter(plainText[i]))
                {
                    encryptedLetter = EncryptLetter(plainText[i], generatedKey[i]);
                    
                }

                cipherText.Append(encryptedLetter);
            }

            return cipherText.ToString();
        }

        private string GenerateKey(int neededKeyLength)
        {
            int keyIndex = 0;
            StringBuilder generatedKey = new StringBuilder();

            while (generatedKey.Length < neededKeyLength)
            {
                generatedKey.Append(_key[keyIndex]);
                keyIndex = (keyIndex + 1) % _key.Length;
            }

            return generatedKey.ToString();
        }

        private char EncryptLetter(char plainTextLetter, char keyLetter)
        {
            bool isLetterUpper = char.IsUpper(plainTextLetter);
            int normalizedLetterCode = AsciiManipulator.NormalizeLetter(plainTextLetter);
            int normalizedKeyCode = AsciiManipulator.NormalizeLetter(keyLetter);
            int encryptedLetterCode = E(normalizedLetterCode, normalizedKeyCode);
            char encryptedLetter = AsciiManipulator.ReverseToLetter(encryptedLetterCode, isLetterUpper);

            return encryptedLetter;
        }

        private int E(int letterId, int keyId)
        {
            int encryptedLetterId = (letterId + keyId) % LETTER_COUNT;

            return encryptedLetterId;
        }

        public string Decrypt(string cipherText)
        {
            string generatedKey = GenerateKey(cipherText.Length);
            StringBuilder plainText = new StringBuilder(cipherText.Length);

            for (int i = 0; i < cipherText.Length; i++)
            {
                char decryptedLetter = cipherText[i];
                if (char.IsLetter(cipherText[i]))
                {
                    decryptedLetter = DecryptLetter(cipherText[i], generatedKey[i]);
                }
                
                plainText.Append(decryptedLetter);
            }

            return plainText.ToString();
        }

        private char DecryptLetter(char letter, char keyLetter)
        {
            bool isLetterUpper = char.IsUpper(letter);
            int normalizedLetterCode = AsciiManipulator.NormalizeLetter(letter);
            int normalizedKeyCode = AsciiManipulator.NormalizeLetter(keyLetter);
            int decryptedLetterCode = D(normalizedLetterCode, normalizedKeyCode);
            char decryptedLetter = AsciiManipulator.ReverseToLetter(decryptedLetterCode, isLetterUpper);

            return decryptedLetter;
        }

        private int D(int letterId, int keyId)
        {
            int decryptedLetterId = (letterId - keyId + LETTER_COUNT) % LETTER_COUNT;

            return decryptedLetterId;
        }
    }
}
