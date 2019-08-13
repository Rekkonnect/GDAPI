using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Functions.Extensions
{
    /// <summary>Provides extension methods for the <seealso cref="string"/> class.</summary>
    public static class StringExtensions
    {
        #region String
        #region Find
        /// <summary>Finds a substring within the string. Returns the index of the first character where the first match occurred, otherwise -1.</summary>
        /// <param name="s">The string within which the search will be performed.</param>
        /// <param name="match">The substring to match from the original string.</param>
        public static int Find(this string s, string match)
        {
            for (int i = 0; i <= s.Length - match.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < match.Length && found; j++)
                    if (s[i + j] != match[j])
                        found = false;
                if (found)
                    return i;
            }
            return -1;
        }
        /// <summary>Finds a substring within the string. Returns the index of the first character where the match occurred, otherwise -1.</summary>
        /// <param name="s">The string within which the search will be performed.</param>
        /// <param name="match">The substring to match from the original string.</param>
        /// <param name="occurrence">The single-based (starting from 1) index of the occurrence to find.</param>
        public static int Find(this string s, string match, int occurrence)
        {
            if (occurrence <= 0)
                throw new ArgumentException("The occurrence cannot be non-positive.");
            int occurrences = 0;
            for (int i = 0; i <= s.Length - match.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < match.Length && found; j++)
                    if (s[i + j] != match[j])
                        found = false;
                if (found)
                {
                    occurrences++;
                    if (occurrences == occurrence)
                        return i;
                }
            }
            return -1;
        }
        /// <summary>Finds a substring within the string. Returns the index of the first character where the first match occurred, otherwise -1.</summary>
        /// <param name="s">The string within which the search will be performed.</param>
        /// <param name="match">The substring to match from the original string.</param>
        /// <param name="start">The starting index to perform the search.</param>
        /// <param name="end">The ending index to perform the search.</param>
        public static int Find(this string s, string match, int start, int end)
        {
            for (int i = start; i <= end - match.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < match.Length && found; j++)
                    found = s[i + j] == match[j];
                if (found)
                    return i;
            }
            return -1;
        }
        /// <summary>Finds a substring within the string from the end. Returns the index of the first character where the first match occurred, otherwise -1.</summary>
        /// <param name="s">The string within which the search will be performed.</param>
        /// <param name="match">The substring to match from the original string.</param>
        public static int FindFromEnd(this string s, string match)
        {
            for (int i = s.Length - match.Length; i >= 0; i--)
            {
                bool found = true;
                for (int j = 0; j < match.Length && found; j++)
                    if (s[i + j] != match[j])
                        found = false;
                if (found)
                    return i;
            }
            return -1;
        }
        /// <summary>Finds a substring within the string from the end. Returns the index of the first character where the match occurred, otherwise -1.</summary>
        /// <param name="s">The string within which the search will be performed.</param>
        /// <param name="match">The substring to match from the original string.</param>
        /// <param name="occurrence">The single-based (starting from 1) index of the occurrence to find.</param>
        public static int FindFromEnd(this string s, string match, int occurence)
        {
            int occurences = 0;
            for (int i = s.Length - match.Length; i >= 0; i--)
            {
                bool found = true;
                for (int j = 0; j < match.Length && found; j++)
                    if (s[i + j] != match[j])
                        found = false;
                if (found)
                {
                    occurences++;
                    if (occurences == occurence)
                        return i;
                }
            }
            return -1;
        }
        /// <summary>Finds a substring within the string from the end. Returns the index of the first character where the first match occurred, otherwise -1.</summary>
        /// <param name="s">The string within which the search will be performed.</param>
        /// <param name="match">The substring to match from the original string.</param>
        /// <param name="start">The starting index to perform the search.</param>
        /// <param name="end">The ending index to perform the search.</param>
        public static int FindFromEnd(this string s, string match, int start, int end)
        {
            for (int i = s.Length - match.Length; i >= 0; i--)
            {
                bool found = true;
                for (int j = 0; j < match.Length && found; j++)
                    if (s[i + j] != match[j])
                        found = false;
                if (found)
                    return i;
            }
            return -1;
        }
        /// <summary>Finds a substring within the string. Returns the indexes of the first character where all the matches occurred, otherwise -1.</summary>
        /// <param name="s">The string within which the search will be performed.</param>
        /// <param name="match">The substring to match from the original string.</param>
        public static int[] FindAll(this string s, string match)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i <= s.Length - match.Length; i++)
            {
                string sub = s.Substring(i, match.Length);
                if (sub == match)
                    indices.Add(i);
            }
            return indices.ToArray();
        }
        /// <summary>Finds a substring within the string. Returns the indexes of the first character where all the matches occurred, otherwise -1.</summary>
        /// <param name="s">The string within which the search will be performed.</param>
        /// <param name="match">The substring to match from the original string.</param>
        /// <param name="start">The starting index to perform the search.</param>
        /// <param name="end">The ending index to perform the search.</param>
        public static int[] FindAll(this string s, string match, int start, int end)
        {
            List<int> indices = new List<int>();
            for (int i = start; i <= end - match.Length; i++)
            {
                string sub = s.Substring(i, match.Length);
                if (sub == match)
                    indices.Add(i);
            }
            return indices.ToArray();
        }
        #endregion

        #region Number-Related
        /// <summary>Converts the number found in the end of the string into an <seealso cref="int"/>.</summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int GetLastNumber(this string s)
        {
            int i = s.Length;
            while (i > 0 && s[i - 1].IsNumber())
                i--;
            if (i < s.Length)
                return int.Parse(s.Substring(i));
            throw new ArgumentException("The string has no number in the end.");
        }
        /// <summary>Removes the number found in the end of the string.</summary>
        /// <param name="s">The string whose number in the end to remove.</param>
        public static string RemoveLastNumber(this string s)
        {
            int i = s.Length;
            while (i > 0 && s[i - 1].IsNumber())
                i--;
            return s.Substring(0, i);
        }
        #endregion

        #region Base 64
        /// <summary>Fixes the Base 64 string that is provided. Returns an instance referring to the original string instance.</summary>
        /// <param name="s">The Base 64 string to fix.</param>
        public static string FixBase64String(this string s)
        {
            int lastNumber = 0;
            int lastInvalidCharacter = 0;
            bool continueChecking = true;
            while (continueChecking)
            {
                char c = s[s.Length - lastNumber - 1];
                if (continueChecking = c.IsNumber())
                    lastNumber++;
                else if (continueChecking = !c.IsBase64Character())
                    lastInvalidCharacter = ++lastNumber;
            }
            s = s.Substring(0, s.Length - lastInvalidCharacter);
            while (s.Length % 4 != 0)
                s += "=";
            return s;
        }
        #endregion

        #region Manipulation
        /// <summary>Determines whether the specified string matches search criteria with another.</summary>
        /// <param name="s">The searching string.</param>
        /// <param name="target">The target string to search for.</param>
        public static bool MatchesSearchCriteria(this string s, string target)
        {
            if (s.Length == 0)
                return true;
            var splitA = s.ToLower().Split(' ');
            var splitB = target.ToLower().Split(' ');
            int difference = splitB.Length - splitA.Length;
            bool valid = true;
            if (difference >= 0)
            {
                Dictionary<char, int> a, b;
                int end = splitB.Length - difference;
                for (int i = 0; i < end && valid; i++)
                {
                    string wordA = splitA[i];
                    string wordB = splitB[i];
                    a = wordA.GetCharacterOccurences();
                    b = wordB.GetCharacterOccurences();
                    if (valid = a.Count > b.Count)
                        continue;
                    int count = 0;
                    foreach (var kvp in a)
                    {
                        b.TryGetValue(kvp.Key, out int value);
                        if (!(valid = kvp.Value <= value))
                            break;
                        count++;
                    }
                    double ratio = (double)b.Count / count;
                    int d = b.Count - a.Count;
                    // TODO: Tweak?
                    if (valid && (count == a.Count || (1 <= ratio && ratio < 1.75 && d < 5)))
                        return true;
                }
                if (valid)
                    return true;
            }
            //return false;
            return target.RemoveCharacterRepetitions() == s.RemoveCharacterRepetitions();
        }
        /// <summary>Returns a string that removes repetitions of the same character. Example: <code>RemoveCharacterRepetitions("aabcc") = "abc"</code>.</summary>
        /// <param name="s">The string to remove the character repetitions from.</param>
        public static string RemoveCharacterRepetitions(this string s)
        {
            char[] chars = new char[s.Length];
            if (s.Length > 0) // Just to avoid bad cases
                chars[0] = s[0];
            int x = 0;
            for (int i = 1; i < s.Length; i++)
                if (chars[x] != s[i])
                    chars[++x] = s[i];
            return new string(chars);
        }
        /// <summary>Returns a dictionary containing the number of instances per character in the provided string.</summary>
        /// <param name="s">The string to get the character occurences of.</param>
        public static Dictionary<char, int> GetCharacterOccurences(this string s)
        {
            var d = new Dictionary<char, int>();
            foreach (var c in s)
                d.IncrementOrAddKeyValue(c);
            return d;
        }
        /// <summary>Returns a substring that begins with the beginning matched string and ends before the ending matched string.</summary>
        /// <param name="s">The string whose substring to return.</param>
        /// <param name="from">The beginning matching string to begin the substring from.</param>
        /// <param name="to">The ending matching string to end the substring at.</param>
        public static string Substring(this string s, string from, string to)
        {
            int startIndex = s.Find(from) + from.Length;
            int endIndex = s.Find(to);
            int length = endIndex - startIndex;
            return s.Substring(startIndex, length);
        }
        /// <summary>Replaces a part of the string with a new one.</summary>
        /// <param name="originalString">The original string.</param>
        /// <param name="stringToReplaceWith">The new string to replace to the part of the original one.</param>
        /// <param name="startIndex">The starting index of the substring to replace.</param>
        /// <param name="length">The length of the substring to replace.</param>
        public static string Replace(this string originalString, string stringToReplaceWith, int startIndex, int length)
        {
            string result = originalString;
            result = result.Remove(startIndex, length);
            result = result.Insert(startIndex, stringToReplaceWith);
            return result;
        }
        /// <summary>Replaces a whole word of the original string and returns the new one.</summary>
        /// <param name="originalString">The original string which will be replaced.</param>
        /// <param name="oldString">The old part of the string in the original string to replace.</param>
        /// <param name="newString">The new part of the string which will be contained in the returned string.</param>
        public static string ReplaceWholeWord(this string originalString, string oldString, string newString)
        {
            for (int i = originalString.Length - oldString.Length; i >= 0; i--)
            {
                if (originalString.Substring(i, oldString.Length) == oldString)
                    if ((i > 0 ? (!originalString[i - 1].IsLetterOrNumber()) : true) && (i < originalString.Length - oldString.Length ? (!originalString[i + oldString.Length].IsLetterOrNumber()) : true))
                    {
                        originalString = originalString.Replace(newString, i, oldString.Length);
                        i -= oldString.Length;
                    }
            }
            return originalString;
        }
        #endregion

        #region Split
        /// <summary>Splits this string based on a separator.</summary>
        /// <param name="s">The string to split.</param>
        /// <param name="separator">The separator between the splitted strings.</param>
        public static string[] Split(this string s, string separator)
        {
            int[] occurences = s.FindAll(separator);
            string[] result = new string[occurences.Length + 1];
            for (int i = 0; i < result.Length; i++)
            {
                int startIndex = i == 0 ? 0 : occurences[i - 1] + separator.Length;
                int endIndex = i >= occurences.Length ? s.Length : occurences[i];
                result[i] = s.Substring(startIndex, endIndex - startIndex);
            }
            return result;
        }
        #endregion

        #region Checks
        /// <summary>Checks whether at least one of the provided characters are contained in the string.</summary>
        /// <param name="s">The string to check whether it contains the specified characters.</param>
        /// <param name="c">The characters to check whether they are contained in the string.</param>
        public static bool Contains(this string s, params char[] c)
        {
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < c.Length; j++)
                    if (s[i] == c[j])
                        return true;
            return false;
        }
        /// <summary>Checks whether the string contains the provided substring as a whole word.</summary>
        /// <param name="s">The string to check whether it contains the specified substring as a whole word.</param>
        /// <param name="c">The substring to check whether it is contained in the original string as a whole word.</param>
        public static bool ContainsWholeWord(this string s, string match)
        {
            for (int i = s.Length - match.Length; i >= 0; i--)
                if (s.Substring(i, match.Length) == match)
                    if ((i > 0 ? (!s[i - 1].IsLetterOrNumber()) : true) && (i < s.Length - match.Length ? (!s[i + match.Length].IsLetterOrNumber()) : true))
                        return true;
            return false;
        }
        /// <summary>Checks whether this string matches another string regardless of its character casing.</summary>
        /// <param name="s">The first string.</param>
        /// <param name="c">The second string.</param>
        public static bool MatchesStringCaseFree(this string s, string match)
        {
            if (s.Length != match.Length)
                return false;
            return s.ToLower() == match.ToLower();
        }
        #endregion
        #endregion

        #region String[]
        /// <summary>Finds the occurrences of a string in a string array and returns an array containing the indices of each occurrence in the original array.</summary>
        /// <param name="a">The array containing the strings which will be evaluated.</param>
        /// <param name="match">The string to match.</param>
        public static int[] FindOccurrences(this string[] a, string match)
        {
            if (a != null)
            {
                List<int> occurrences = new List<int>();
                for (int i = 0; i < a.Length; i++)
                    if (a[i] == match)
                        occurrences.Add(i);
                return occurrences.ToArray();
            }
            else return new int[0];
        }
        /// <summary>Replaces the characters of an array of strings and returns the new array.</summary>
        /// <param name="a">The array containing the strings which will be replaced.</param>
        /// <param name="oldChar">The old character.</param>
        /// <param name="newChar">The new character.</param>
        public static string[] Replace(this string[] a, char oldChar, char newChar)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] = a[i].Replace(oldChar, newChar);
            return a;
        }
        /// <summary>Replaces the strings of an array of strings and returns the new array.</summary>
        /// <param name="a">The array containing the strings which will be replaced.</param>
        /// <param name="oldString">The old string.</param>
        /// <param name="newString">The new string.</param>
        public static string[] Replace(this string[] a, string oldString, string newString)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] = a[i].Replace(oldString, newString);
            return a;
        }
        /// <summary>Replaces whole words of the strings of an array of strings and returns the new array.</summary>
        /// <param name="a">The array containing the strings which will be replaced.</param>
        /// <param name="oldString">The old string.</param>
        /// <param name="newString">The new string.</param>
        public static string[] ReplaceWholeWord(this string[] a, string oldString, string newString)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] = a[i].ReplaceWholeWord(oldString, newString);
            return a;
        }
        /// <summary>Removes the empty elements of a string array and returns the new array.</summary>
        /// <param name="a">The array of strings.</param>
        public static string[] RemoveEmptyElements(this string[] a) => RemoveEmptyElements(a.ToList()).ToArray();
        /// <summary>Determines whether there is at least one occurrence of a string in a string of the string array.</summary>
        /// <param name="a">The array of strings.</param>
        /// <param name="match">The string to match.</param>
        public static bool ContainsAtLeast(this string[] a, string match)
        {
            if (a == null)
                return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i].Contains(match))
                    return true;
            return false;
        }
        /// <summary>Determines whether there is at least one occurrence of a string as a whole word in a string of the string array.</summary>
        /// <param name="a">The array of strings.</param>
        /// <param name="match">The string to match.</param>
        public static bool ContainsAtLeastWholeWord(this string[] a, string match)
        {
            if (a != null)
                return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i].ContainsWholeWord(match))
                    return true;
            return false;
        }
        /// <summary>Combines the strings of a string array and returns the new string.</summary>
        /// <param name="s">The array of strings.</param>
        public static string Combine(this string[] s)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
                str = str.Append(s[i]);
            return str.ToString();
        }
        /// <summary>Combines the strings of a string array with a separator and returns the new string.</summary>
        /// <param name="s">The array of strings.</param>
        /// <param name="separator">The separator of the strings.</param>
        public static string Combine(this string[] s, string separator)
        {
            if (s.Length == 0)
                return "";
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
                str = str.Append(s[i] + separator);
            str = str.Remove(str.Length - separator.Length, separator.Length);
            return str.ToString();
        }
        /// <summary>Splits an array of strings and returns a new two-dimensiional array containing the split strings. With indexing [i, j], i is the index of the string in the original array and j is the index of the split string.</summary>
        /// <param name="s">The array of strings.</param>
        /// <param name="separator">The separator of the strings.</param>
        public static string[,] Split(this string[] s, char separator)
        {
            List<string[]> separated = new List<string[]>();
            for (int i = 0; i < s.Length; i++)
                separated.Add(s[i].Split(separator));
            return separated.ToTwoDimensionalArray();
        }
        #endregion

        #region List<string>
        /// <summary>Removes the empty elements of a string list and returns the new list.</summary>
        /// <param name="a">The list of strings.</param>
        public static List<string> RemoveEmptyElements(this List<string> a)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < a.Count; i++)
                if (a[i].Length > 0)
                    result.Add(a[i]);
            return result;
        }
        #endregion

        #region IEnumerable<string>
        /// <summary>Aggregates the provided items with the provided aggregator function if the items' count is greater than 0, otherwise returns an empty string.</summary>
        /// <param name="strings">The strings to aggregate.</param>
        /// <param name="aggregator">The aggregator function to use when aggregating the strings.</param>
        public static string AggregateIfContains(this IEnumerable<string> strings, Func<string, string, string> aggregator) => strings.Count() > 0 ? strings.Aggregate(aggregator) : "";
        #endregion
    }
}
