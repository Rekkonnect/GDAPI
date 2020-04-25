using GDAPI.Functions.Crypto;
using System;

namespace GDAPI.Objects.GeometryDash.GamesaveStrings
{
    /// <summary>Represents a gamesave string.</summary>
    public class GamesaveString : DATFileGamesaveString
    {
        private static readonly string[] samples = new string[] { "<k>bgVolume</k>", "<k>sfxVolume</k>", "<k>playerUDID</k>", "<k>playerName</k>", "<k>playerUserID</k>" };

        /// <summary>Initializes a new instance of the <seealso cref="GamesaveString"/>.</summary>
        /// <param name="rawString">The raw string to contain in this instance.</param>
        public GamesaveString(string rawString) : base(rawString) { }

        /// <inheritdoc/>
        protected override string[] GetUnencryptedSamples() => samples;

        public static implicit operator string(GamesaveString s) => s.RawString;
        public static explicit operator GamesaveString(string s) => new GamesaveString(s);
    }
}
