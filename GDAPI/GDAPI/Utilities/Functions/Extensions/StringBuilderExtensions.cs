using System.Text;

namespace GDAPI.Utilities.Functions.Extensions
{
    /// <summary>Provides extension methods for the <seealso cref="StringBuilder"/> class.</summary>
    public static class StringBuilderExtensions
    {
        /// <summary>Removes a range of characters from the specified starting index to the end of the string.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> whose range of characters to remove.</param>
        /// <param name="startingIndex">The starting index from which to remove all characters.</param>
        public static StringBuilder Remove(this StringBuilder s, int startingIndex) => s.Remove(startingIndex, s.Length - startingIndex);
        /// <summary>Removes a number of characters from the end of the string.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> whose last characters to remove.</param>
        /// <param name="characterCount">The count of characters to remove from the end of the string.</param>
        public static StringBuilder RemoveLast(this StringBuilder s, int characterCount) => s.Remove(s.Length - characterCount, characterCount);
        /// <summary>Removes the last character of the string.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to remove.</param>
        public static StringBuilder RemoveLast(this StringBuilder s) => s.RemoveLast(1);
        /// <summary>Removes the last character of the string if it has any characters, otherwise none.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to remove, if any.</param>
        public static StringBuilder RemoveLastOrNone(this StringBuilder s) => s.RemoveLast(s.Length > 0 ? 1 : 0);
        /// <summary>Removes the last character of the string if it ends with the requested character, otherwise none.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to remove, if the condition is met.</param>
        /// <param name="lastCharacter">The character that should be the last character of the string to remove the last character.</param>
        public static StringBuilder RemoveLastIfEndsWith(this StringBuilder s, char lastCharacter) => s.RemoveLast(s.EndsWith(lastCharacter) ? 1 : 0);
        /// <summary>Removes the last character of the string if it ends with the requested character, otherwise none.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to remove, if the condition is met.</param>
        /// <param name="lastCharacter">The character that should be the last character of the string to remove the last character.</param>
        public static StringBuilder RemoveLastIfEndsWith(this StringBuilder s, string lastCharacters) => s.RemoveLast(s.EndsWith(lastCharacters) ? lastCharacters.Length : 0);

        /// <summary>Retrieves the last character of the string.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to retrieve.</param>
        public static char Last(this StringBuilder s) => s[s.Length - 1];
        /// <summary>Retrieves the last character of the string if the string has any characters, otherwise returns <see langword="null"/> if the <seealso cref="StringBuilder"/> is <see langword="null"/> or if it has no characters.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> whose last character to retrieve.</param>
        public static char? LastOrNull(this StringBuilder s) => s?.Length > 0 ? s[s.Length - 1] : (char?)null;

        /// <summary>Determines whether the string ends with the specified character.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> to check whether it ends with the specified character.</param>
        public static bool EndsWith(this StringBuilder s, char lastCharacter) => s[s.Length - 1] == lastCharacter;
        /// <summary>Determines whether the string ends with the specified substring.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> to check whether it ends with the specified substring.</param>
        public static bool EndsWith(this StringBuilder s, string lastCharacters) => s.SubstringLast(lastCharacters.Length) == lastCharacters;

        /// <summary>Returns a substring of this string with the specified number of characters from a starting index.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
        /// <param name="startingIndex">The index of the original string that the new substring will start from.</param>
        /// <param name="length">The number of characters of the substring.</param>
        public static string Substring(this StringBuilder s, int startingIndex, int length) => s.SubstringBuilder(startingIndex, length).ToString();
        /// <summary>Returns a substring of this string with the specified number of characters from a starting index as a <seealso cref="StringBuilder"/>.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
        /// <param name="startingIndex">The index of the original string that the new substring will start from.</param>
        /// <param name="length">The number of characters of the substring.</param>
        public static StringBuilder SubstringBuilder(this StringBuilder s, int startingIndex, int length)
        {
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                result[i] = s[i + startingIndex];
            return result;
        }
        /// <summary>Returns a substring of this string with the specified number of characters from its start.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
        /// <param name="length">The number of characters of the substring.</param>
        public static string SubstringStart(this StringBuilder s, int length) => s.SubstringBuilder(0, length).ToString();
        /// <summary>Returns a substring of this string with the specified number of characters from its start as a <seealso cref="StringBuilder"/>.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
        /// <param name="length">The number of characters of the substring.</param>
        public static StringBuilder SubstringStartBuilder(this StringBuilder s, int length) => s.SubstringBuilder(0, length);
        /// <summary>Returns a substring of this string with the specified number of characters from its end.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
        /// <param name="length">The number of characters of the substring.</param>
        public static string SubstringLast(this StringBuilder s, int length) => s.SubstringBuilder(s.Length - length, length).ToString();
        /// <summary>Returns a substring of this string with the specified number of characters from its end as a <seealso cref="StringBuilder"/>.</summary>
        /// <param name="s">The <seealso cref="StringBuilder"/> to get the subtsring of.</param>
        /// <param name="length">The number of characters of the substring.</param>
        public static StringBuilder SubstringLastBuilder(this StringBuilder s, int length) => s.SubstringBuilder(s.Length - length, length);
    }
}
