using GDAPI.Functions.Crypto;
using System;

namespace GDAPI.Objects.GeometryDash.GamesaveStrings
{
    /// <summary>Represents a .dat file gamesave string.</summary>
    public abstract class DATFileGamesaveString : BaseGamesaveString
    {
        /// <summary>Initializes a new instance of the <seealso cref="DATFileGamesaveString"/>.</summary>
        /// <param name="rawString">The raw string to contain in this instance.</param>
        public DATFileGamesaveString(string rawString) : base(rawString) { }

        /// <inheritdoc/>
        protected sealed override string Decrypt() => GeometryDashCrypto.GDGamesaveDecrypt(RawString);
        /// <inheritdoc/>
        protected sealed override string Encrypt() => GeometryDashCrypto.GDGamesaveEncrypt(RawString);
    }
}
