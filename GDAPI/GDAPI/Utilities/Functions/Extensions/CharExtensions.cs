using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Functions.Extensions
{
    public static class CharExtensions
    {
        #region Letters & Numbers
        /// <summary>Checks whether the character is a lower case latin letter.</summary>
        /// <param name="c">The character to check whether it is a lower case latin letter.</param>
        public static bool IsLowerCaseLetter(this char c) => c >= 'a' && c <= 'z';
        /// <summary>Checks whether the character is a upper case latin letter.</summary>
        /// <param name="c">The character to check whether it is a upper case latin letter.</param>
        public static bool IsUpperCaseLetter(this char c) => c >= 'A' && c <= 'Z';
        /// <summary>Checks whether the character is a number character.</summary>
        /// <param name="c">The character to check whether it is a number character.</param>
        public static bool IsNumber(this char c) => c >= '0' && c <= '9';
        /// <summary>Checks whether the character is a latin letter character.</summary>
        /// <param name="c">The character to check whether it is a latin letter character.</param>
        public static bool IsLetter(this char c) => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        /// <summary>Checks whether the character is a latin letter or a number character.</summary>
        /// <param name="c">The character to check whether it is a latin letter or a number character.</param>
        public static bool IsLetterOrNumber(this char c) => IsLetter(c) || IsNumber(c);
        #endregion

        #region Base 64
        /// <summary>Checks whether the character is a valid Base 64 character.</summary>
        /// <param name="c">The character to check whether it is a valid Base 64 character.</param>
        public static bool IsBase64Character(this char c) => IsNumber(c) || IsLetter(c) || c == '/' || c == '+' || c == '=';
        #endregion
    }
}
