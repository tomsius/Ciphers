namespace Ciphers.Interfaces
{
    public interface ISymmetricCipher
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}