using Ciphers.Interfaces;
using Ciphers.Utility;
using System;
using System.Text;

namespace Ciphers.Ciphers
{
    public class VigenereCipher : ISymmetricCipher
    {
        private const int LetterCount = 'z' - 'a' + 1;

        private readonly string _key;

        public VigenereCipher(string key)
        {
            _key = key;
        }

        public string Encrypt(string plainText)
        {
            StringBuilder cipherText = new StringBuilder(plainText.Length);

            for (int i = 0; i < plainText.Length; i++)
            {
                char encryptedLetter = plainText[i];
                if (char.IsLetter(plainText[i]))
                {
                    char keyLetter = GetKeyLetterByIndex(i);
                    encryptedLetter = EncryptLetter(plainText[i], keyLetter);
                    
                }

                cipherText.Append(encryptedLetter);
            }

            return cipherText.ToString();
        }

        private char GetKeyLetterByIndex(int positionInPlaintext)
        {
            int positionInKey = positionInPlaintext % _key.Length;

            return _key[positionInKey];
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
            int encryptedLetterId = (letterId + keyId) % LetterCount;

            return encryptedLetterId;
        }

        public string Decrypt(string cipherText)
        {
            StringBuilder plainText = new StringBuilder(cipherText.Length);

            for (int i = 0; i < cipherText.Length; i++)
            {
                char decryptedLetter = cipherText[i];
                if (char.IsLetter(cipherText[i]))
                {
                    char keyLetter = GetKeyLetterByIndex(i);
                    decryptedLetter = DecryptLetter(cipherText[i], keyLetter);
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
            int decryptedLetterId = (letterId - keyId + LetterCount) % LetterCount;

            return decryptedLetterId;
        }
    }
}
