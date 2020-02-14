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
        /// <summary>Used to convert specifics hexadecimal chars on a given string (<paramref name="str"/>)</summary>
        /// <param name="str">The string to process</param>
        /// <returns>The proper string</returns>
        public static string ConvertStringWithHexCharsToProperString(string str)
        {
            var chars = str.ToCharArray();
            var result = new StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '%')
                {
                    switch (chars[i + 1].ToString() + chars[i + 2].ToString()) // This website for hex codes => https://www.obkb.com/dcljr/charstxt.html
                    {
                        case "3A": // colon 
                            result.Append(':');
                            break;
                        case "2F": // slash
                            result.Append('/');
                            break;
                        case "3F": // Question mark
                            result.Append('?');
                            break;
                        default:
                            break;
                    }
                    i += 2;
                }
                else
                {
                    result.Append(chars[i]);
                }
            }
            return result.ToString();
        }
    }
}