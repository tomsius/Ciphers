using Ciphers.Ciphers;

namespace Ciphers.Interfaces
{
    public interface IAsymmetricCipher
    {
        public string Encrypt(string plainText, Key publicKey);
        public string Decrypt(string cipherText);
    }
}
