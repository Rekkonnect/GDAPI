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
        public static byte[] Compress(byte[] data)
        {
            using var uncompressedStream = new MemoryStream(data);
            using var resultStream = new MemoryStream();
            using var zipStream = new GZipStream(resultStream, CompressionMode.Compress);

            uncompressedStream.CopyTo(zipStream);
            zipStream.Close();
            return resultStream.ToArray();
        }

        /// <summary>Decompresses a <seealso cref="byte"/>[] and returns the decompressed result.</summary>
        /// <param name="data">The <seealso cref="byte"/>[] of data to decompress.</param>
        public static byte[] Decompress(byte[] data)
        {
            using var compressedStream = new MemoryStream(data);
            using var resultStream = new MemoryStream();
            using var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress);

            zipStream.CopyTo(resultStream);
            return resultStream.ToArray();
        }
    }
}
