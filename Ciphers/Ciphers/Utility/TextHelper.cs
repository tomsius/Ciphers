using System;
using System.Collections.Generic;
using System.Text;

namespace Ciphers.Utility
{
    public static class TextHelper
    {
        public static string NormalizeText(string text)
        {
            StringBuilder normalizedText = new StringBuilder(text.Length);

            foreach (var character in text)
            {
                if (char.IsLetter(character))
                {
                    normalizedText.Append(character);
                }
            }

            return normalizedText.ToString();
        }
    }
}
