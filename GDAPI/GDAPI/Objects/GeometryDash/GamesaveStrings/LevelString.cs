using GDAPI.Functions.Crypto;

namespace GDAPI.Objects.GeometryDash.GamesaveStrings
{
    /// <summary>Represents a level string.</summary>
    public class LevelString : BaseGamesaveString
    {
        private static readonly string[] samples = new string[] { "kA13,", "kA15,", "kA16,", "kA14,", "kA6," };

        /// <summary>Initializes a new instance of the <seealso cref="LevelString"/>.</summary>
        /// <param name="rawString">The raw string to contain in this instance.</param>
        public LevelString(string rawString) : base(rawString) { }

        /// <inheritdoc/>
        protected override string Decrypt() => GeometryDashCrypto.GDLevelStringDecrypt(RawString);
        /// <inheritdoc/>
        protected override string Encrypt() => GeometryDashCrypto.GDLevelStringEncrypt(RawString);

        /// <inheritdoc/>
        protected override string[] GetUnencryptedSamples() => samples;

        public static implicit operator string(LevelString s) => s.RawString;
        public static explicit operator LevelString(string s) => new LevelString(s);
    }
}
