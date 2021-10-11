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
            foreach (string line in contents)
                result.Append(line).Append('\n');
            result.RemoveLast();
            WriteAllText(path, result.ToString());
        }
        public static void WriteAllLinesWithoutUselessNewLine(string path, StringBuilder[] contents)
        {
            var result = new StringBuilder();
            foreach (var line in contents)
                result.Append(line).Append('\n');
            result.RemoveLast();
            WriteAllText(path, result.ToString());
        }
        public static void WriteAllLinesWithoutUselessNewLine(string path, List<string> contents)
        {
            var result = new StringBuilder();
            foreach (string line in contents)
                result.Append(line).Append('\n');
            result.RemoveLast();
            WriteAllText(path, result.ToString());
        }
        public static void WriteAllLinesWithoutUselessNewLine(string path, List<StringBuilder> contents)
        {
            var result = new StringBuilder();
            foreach (var line in contents)
                result.Append(line).Append('\n');
            result.RemoveLast();
            WriteAllText(path, result.ToString());
        }
    }
}