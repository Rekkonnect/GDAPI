using GDAPI.Objects.GeometryDash.General;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAPI.Application.Newgrounds
{
    /// <summary>Handles downloading a Newgrounds song.</summary>
    public class SongDownloader
    {
        private readonly HttpClient client = new();

        private Task download;

        /// <summary>Gets the status of the song downloading task.</summary>
        public TaskStatus DownloadStatus => download?.Status ?? (TaskStatus)(-1);

        /// <summary>Initializes a new instance of the <seealso cref="SongDownloader"/> and immediately begins to download the requested song.</summary>
        /// <param name="song">The <seealso cref="SongMetadata"/> containing the song information to download.</param>
        public SongDownloader(SongMetadata song)
        {
            download = DownloadSong(song);
        }

        /// <summary>Initializes a new instance of the <seealso cref="SongDownloader"/> and immediately begins to download the requested song.</summary>
        /// <param name="id">The song ID of the song to download.</param>
        public SongDownloader(int id)
        {
            using var getter = new SongMetadataGetter(id, a => download = DownloadSong(a));
        }

        private async Task DownloadSong(SongMetadata song)
        {
            if (song == SongMetadata.Unknown)
                return;

            var filepath = Database.GetCustomSongLocation(song.ID);
            var res = await client.GetAsync(song.DownloadLink);
            var bytes = await res.Content.ReadAsByteArrayAsync();

            await File.WriteAllBytesAsync(filepath, bytes);
        }
    }
}
