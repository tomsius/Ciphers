namespace Ciphers.Interfaces
{
    public interface ISymmetricCipher
    {
        public string Encrypt(string plainText);
        public string Decrypt(string cipherText);
    }
}