namespace GDAPI.Objects.GeometryDash.GamesaveStrings
{
    /// <summary>Represents a level data string.</summary>
    public class LevelDataString : DATFileGamesaveString
    {
        private static readonly string[] samples = new string[] { "<?xml version=\"1.0\"?><plist version=\"1.0\" gjver=\"2.0\">" };

        /// <summary>Initializes a new instance of the <seealso cref="LevelDataString"/>.</summary>
        /// <param name="rawString">The raw string to contain in this instance.</param>
        public LevelDataString(string rawString) : base(rawString) { }

        /// <inheritdoc/>
        protected override string[] GetUnencryptedSamples() => samples;

        public static implicit operator string(LevelDataString s) => s.RawString;
        public static explicit operator LevelDataString(string s) => new(s);
    }
}
