using GDAPI.Functions.Extensions;
using System;
using System.Buffers.Text;
using System.Text;
using System.Text.Encodings.Web;

namespace GDAPI.Functions.Crypto
{
    /// <summary>Contains functions related to encryption within Geometry Dash.</summary>
    public static class GeometryDashCrypto
    {
        public static readonly Encoding GeometryDashStringEncoding = Encoding.UTF8;

        /// <summary>Decrypts the encrypted gamesave data (the raw data found in the .dat files) and returns the decrypted data.</summary>
        /// <param name="data">The encrypted gamesave data to decrypt.</param>
        /// <returns>The decrypted version of the provided gamesave data.</returns>
        public static string GDGamesaveDecrypt(string data)
        {
            string xored = data.XORStringUTF8(11); // Decrypt XOR ^ 11
            string replaced = xored.ConvertBase64URLToNormal();
            byte[] gzipb64 = GZipCompression.Decompress(Convert.FromBase64String(replaced)); // Decompress
            return GeometryDashStringEncoding.GetString(gzipb64); // Change to string
        }
        /// <summary>Decrypts the encrypted level string and returns its decrypted version.</summary>
        /// <param name="ls">The encrypted level string to decrypt.</param>
        /// <returns>The decrypted version of the provided level string.</returns>
        public static string GDLevelStringDecrypt(string ls)
        {
            string replaced = ls.ConvertBase64URLToNormal();

            if (!replaced.StartsWith("H4sIAAAAAAAA", StringComparison.InvariantCulture))
                throw new ArgumentException("The level string is not valid.");

            byte[] gzipb64 = Convert.FromBase64String(replaced);
            gzipb64 = GZipCompression.Decompress(gzipb64); // Decompress
            return GeometryDashStringEncoding.GetString(gzipb64); // Change to string
        }

        /// <summary>Encrypts the decrypted gamesave data (the raw data found in the .dat files) and returns the encrypted data.</summary>
        /// <param name="data">The decrypted gamesave data to encrypt.</param>
        /// <returns>The encrypted version of the provided gamesave data.</returns>
        public static string GDGamesaveEncrypt(string data)
        {
            byte[] compressed = GZipCompression.Compress(GeometryDashStringEncoding.GetBytes(data)); // Compress
            string base64 = Convert.ToBase64String(compressed).ConvertBase64ToBase64URL();
            return base64.XORStringUTF8(11); // Encrypt XOR ^ 11
        }
        /// <summary>Encrypts the decrypted level string and returns its encrypted version.</summary>
        /// <param name="ls">The decrypted level string to encrypt.</param>
        /// <returns>The encrypted version of the provided level string.</returns>
        public static string GDLevelStringEncrypt(string ls)
        {
            byte[] compressed = GZipCompression.Compress(GeometryDashStringEncoding.GetBytes(ls)); // Compress
            return Convert.ToBase64String(compressed).ConvertBase64ToBase64URL();
        }
    }
}
