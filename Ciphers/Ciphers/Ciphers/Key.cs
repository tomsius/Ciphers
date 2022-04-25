namespace Ciphers.Ciphers
{
    public class Key
    {
        public int Exponent { get; private set; }
        public int Modulus { get; private set; }

        public Key(int exponent, int modulus)
        {
            Exponent = exponent;
            Modulus = modulus;
        }
    }
}
