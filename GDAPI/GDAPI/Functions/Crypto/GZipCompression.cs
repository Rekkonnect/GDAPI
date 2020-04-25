using System.IO;
using System.IO.Compression;

namespace GDAPI.Functions.Crypto
{
    // TODO: Migrate to Garyon
    /// <summary>Provides functions helpful for GZip compression.</summary>
    public static class GZipCompression
    {
        /// <summary>Compresses a <seealso cref="byte"/>[] and returns the compressed result.</summary>
        /// <param name="data">The <seealso cref="byte"/>[] of data to compress.</param>
        public static byte[] Compress(byte[] data) => Compress(data, CompressionMode.Compress);
        /// <summary>Decompresses a <seealso cref="byte"/>[] and returns the decompressed result.</summary>
        /// <param name="data">The <seealso cref="byte"/>[] of data to decompress.</param>
        public static byte[] Decompress(byte[] data) => Compress(data, CompressionMode.Decompress);

        private static byte[] Compress(byte[] data, CompressionMode compressionMode)
        {
            using var compressedStream = new MemoryStream(data);
            using var zipStream = new GZipStream(compressedStream, compressionMode);
            using var resultStream = new MemoryStream();

            var buffer = new byte[4096];
            int read;

            while ((read = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                resultStream.Write(buffer, 0, read);

            return resultStream.ToArray();
        }
    }
}
