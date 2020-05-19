using GDAPI.Objects.General;
using System.Threading.Tasks;

namespace GDAPI.Objects.GeometryDash.GamesaveStrings
{
    /// <summary>The base class for a gamesave string representation.</summary>
    public abstract class BaseGamesaveString
    {
        /// <summary>The raw string.</summary>
        public string RawString { get; set; }

        /// <summary>Gets the length of the raw string.</summary>
        public int Length => RawString.Length;

        /// <summary>Gets a value indicating whether the current gamesave string is decrypted or not.</summary>
        public bool IsDecrypted => !IsEncrypted;
        /// <summary>Gets a value indicating whether the current gamesave string is encrypted or not.</summary>
        public bool IsEncrypted
        {
            get
            {
                int checks = 0;
                var samples = GetUnencryptedSamples();
                for (int i = 0; i < samples.Length; i++)
                    if (RawString.Contains(samples[i]))
                        checks++;
                return checks != samples.Length;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="BaseGamesaveString"/>.</summary>
        /// <param name="rawString">The raw string to contain in this instance.</param>
        public BaseGamesaveString(string rawString) => RawString = rawString;

        /// <summary>Decrypts the gamesave after checking whether the gamesave is encrypted or not, and optionally overwrites the raw string with the decrypted version. Returns true if the gamesave is encrypted; otherwise false.</summary>
        /// <param name="decrypted">The string to return the decrypted gamesave.</param>
        /// <param name="overwriteLocalString">Determines whether to overwrite the locally stored raw string or not, replacing it with the decrypted version.</param>
        /// <returns>Returns true if the gamesave is encrypted; otherwise false.</returns>
        public bool TryDecrypt(out string decrypted, bool overwriteLocalString = false)
        {
            bool isEncrypted = IsEncrypted;
            decrypted = RawString;
            if (isEncrypted)
                decrypted = Decrypt();
            if (overwriteLocalString)
                RawString = decrypted;
            return isEncrypted;
        }
        /// <summary>Returns the decrypted version of the gamesave after checking whether the gamesave is encrypted or not asynchronously. Returns a tuple containing the result of the operation.</summary>
        /// <param name="overwriteLocalString">Determines whether to overwrite the locally stored raw string or not, replacing it with the decrypted version.</param>
        public async Task<DecryptionResult> TryDecryptAsync(bool overwriteLocalString = false)
        {
            bool result = TryDecrypt(out string decrypted, overwriteLocalString);
            return new DecryptionResult(result, decrypted);
        }

        /// <summary>Encrypts the gamesave after checking whether the gamesave is encrypted or not, and optionally overwrites the raw string with the encrypted version. Returns true if the gamesave is encrypted; otherwise false.</summary>
        /// <param name="encrypted">The string to return the encrypted gamesave.</param>
        /// <param name="overwriteLocalString">Determines whether to overwrite the locally stored raw string or not, replacing it with the encrypted version.</param>
        /// <returns>Returns true if the gamesave is encrypted; otherwise false.</returns>
        public bool TryEncrypt(out string encrypted, bool overwriteLocalString = false)
        {
            bool isDecrypted = IsDecrypted;
            encrypted = RawString;
            if (isDecrypted)
                encrypted = Encrypt();
            if (overwriteLocalString)
                RawString = encrypted;
            return isDecrypted;
        }
        /// <summary>Returns the encrypted version of the gamesave after checking whether the gamesave is encrypted or not asynchronously. Returns a tuple containing the result of the operation.</summary>
        /// <param name="overwriteLocalString">Determines whether to overwrite the locally stored raw string or not, replacing it with the encrypted version.</param>
        public async Task<EncryptionResult> TryEncryptAsync(bool overwriteLocalString = false)
        {
            bool result = TryEncrypt(out string encrypted, overwriteLocalString);
            return new EncryptionResult(result, encrypted);
        }

        /// <summary>Decrypts the current raw string into its unencrypted form and returns the resulting string. The string must be encrypted; the operation will be performed in all occassions.</summary>
        protected abstract string Decrypt();
        /// <summary>Encrypts the current raw string into its encrypted form and returns the resulting string. The string must be deencrypted; the operation will be performed in all occassions.</summary>
        protected abstract string Encrypt();

        /// <summary>Gets the unencrypted samples that determine whether the gamesave string is encrypted or not. They must be large parts of a typical gamesave string that will always be present in the save.</summary>
        protected abstract string[] GetUnencryptedSamples();

        public static implicit operator string(BaseGamesaveString s) => s.RawString;

        /// <summary>Gets the raw string representation of the gamesave string.</summary>
        /// <returns></returns>
        public override string ToString() => RawString;
    }
}
