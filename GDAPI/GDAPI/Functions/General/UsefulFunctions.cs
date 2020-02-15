using GDAPI.Functions.Extensions;
using System.Collections.Generic;
using System.Text;
using static System.IO.File;

namespace GDAPI.Functions.General
{
    public static class UsefulFunctions
    {
        public static void WriteAllLinesWithoutUselessNewLine(string path, string[] contents)
        {
            var result = new StringBuilder();
            for (int i = 0; i < contents.Length; i++)
                result.Append(contents[i] + "\n");
            result.Remove(result.Length - 1, 1);
            WriteAllText(path, result.ToString());
        }
        public static void WriteAllLinesWithoutUselessNewLine(string path, StringBuilder[] contents)
        {
            var result = new StringBuilder();
            for (int i = 0; i < contents.Length; i++)
                result.Append(contents[i] + "\n");
            result.Remove(result.Length - 1, 1);
            WriteAllText(path, result.ToString());
        }
        public static void WriteAllLinesWithoutUselessNewLine(string path, List<string> contents)
        {
            var result = new StringBuilder();
            for (int i = 0; i < contents.Count; i++)
                result.Append(contents[i] + "\n");
            result.Remove(result.Length - 1, 1);
            WriteAllText(path, result.ToString());
        }
        public static void WriteAllLinesWithoutUselessNewLine(string path, List<StringBuilder> contents)
        {
            var result = new StringBuilder();
            for (int i = 0; i < contents.Count; i++)
                result.Append(contents[i] + "\n");
            result.Remove(result.Length - 1, 1);
            WriteAllText(path, result.ToString());
        }
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