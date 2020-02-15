using System.Text;

namespace GDAPI.Functions.General
{
    /// <summary>Provides HTML text handling functions.</summary>
    public static class HTMLTextHandling
    {
        /// <summary>Converts a string's HTML hex coded characters to normal ones.</summary>
        /// <param name="str">The HTML hex coded string to process.</param>
        /// <returns>The converted string.</returns>
        public static string ConvertHTMLHexCharacterCodes(string str)
        {
            var chars = str.ToCharArray();
            var result = new StringBuilder(chars.Length);
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '%')
                {
                    result.Append(int.Parse(chars[(i + 1)..(i + 2)], System.Globalization.NumberStyles.HexNumber));
                    i += 2;
                }
                else
                    result.Append(chars[i]);
            }
            return result.ToString();
        }
    }
}
