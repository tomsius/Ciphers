using Ciphers.Ciphers;

namespace Ciphers.Interfaces
{
    public interface ISignature
    {
        public string Sign(string input);
        public bool IsSignatureValid(string input, string signature, Key publicKey);
    }
}
