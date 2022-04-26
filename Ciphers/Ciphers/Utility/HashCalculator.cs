using System;
using System.Security.Cryptography;
using System.Text;

namespace Ciphers.Utility
{
    public static class HashCalculator
    {
        public static string CalculateHash(string input)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(input));

                foreach (Byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }

            return sb.ToString();
        }
    }
}
