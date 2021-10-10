using System;
using System.Globalization;
using System.Text;

namespace GDAPI.Functions.Extensions
{
    // TODO: Migrate to Garyon
    /// <summary>Provides functions for string processing.</summary>
    public static class EncodedStringProcessing
    {
        /// <summary>XORs an entire UTF7 string and returns the result.</summary>
        /// <param name="text">The text to XOR.</param>
        /// <param name="key">The value to XOR the string's bytes by.</param>
        public static string XORStringUTF7(this string text, int key) => XORString(text, key, Encoding.UTF7);
        /// <summary>XORs an entire UTF8 string and returns the result.</summary>
        /// <param name="text">The text to XOR.</param>
        /// <param name="key">The value to XOR the string's bytes by.</param>
        public static string XORStringUTF8(this string text, int key) => XORString(text, key, Encoding.UTF8);
        /// <summary>XORs an entire UTF32 string and returns the result.</summary>
        /// <param name="text">The text to XOR.</param>
        /// <param name="key">The value to XOR the string's bytes by.</param>
        public static string XORStringUTF32(this string text, int key) => XORString(text, key, Encoding.UTF32);
        /// <summary>XORs an entire Unicode string and returns the result.</summary>
        /// <param name="text">The text to XOR.</param>
        /// <param name="key">The value to XOR the string's bytes by.</param>
        public static string XORStringUnicode(this string text, int key) => XORString(text, key, Encoding.Unicode);
        /// <summary>XORs an entire ASCII string and returns the result.</summary>
        /// <param name="text">The text to XOR.</param>
        /// <param name="key">The value to XOR the string's bytes by.</param>
        public static string XORStringASCII(this string text, int key) => XORString(text, key, Encoding.ASCII);

        /// <summary>Converts the base64url string into a normal base 64 string by replacing its characetrs and adding missing padding characters.</summary>
        /// <param name="original">The original base64url string to convert.</param>
        public static string ConvertBase64URLToNormal(this string original)
        {
            var replaced = original.Replace('-', '+').Replace('_', '/').Replace("\0", string.Empty, StringComparison.InvariantCulture);
            int remaining = replaced.Length % 4;
            if (remaining > 0)
                replaced += new string('=', 4 - remaining);
            return replaced;
        }
        /// <summary>Converts the normal base 64 string into a base64url string by replacing its characetrs. The padding characters are removed.</summary>
        /// <param name="original">The original normal base 64 string to convert.</param>
        /// <returns>The resulting base64url string.</returns>
        public static string ConvertBase64ToBase64URL(this string original)
        {
            return original.Replace('+', '-').Replace('/', '_').Replace("=", string.Empty, StringComparison.InvariantCulture);
        }

        /// <summary>XORs an entire string and returns the result, based on a given encoding.</summary>
        /// <param name="text">The text to XOR.</param>
        /// <param name="key">The value to XOR the string's bytes by.</param>
        /// <param name="encoding">The encoding to use when parsing the string's bytes.</param>
        public static string XORString(this string text, int key, Encoding encoding)
        {
            byte[] result = encoding.GetBytes(text);
            for (int i = 0; i < text.Length; i++)
                result[i] = (byte)(result[i] ^ key);
            return encoding.GetString(result);
        }
    }
}
