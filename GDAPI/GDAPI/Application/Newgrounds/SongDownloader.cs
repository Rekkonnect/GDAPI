using GDAPI.Objects.GeometryDash.General;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAPI.Application.Newgrounds
{
    public class SongDownloader
    {
        private readonly HttpClient client = new HttpClient();

        private Task download;

        public TaskStatus DownloadStatus => download?.Status ?? (TaskStatus)(-1);

        /// <summary>
        /// Download the matching song from Newgrounds specified by the <seealso cref="SongMetadata"/>
        /// </summary>
        /// <param name="song">The song to download</param>
        public SongDownloader(SongMetadata song)
        {
            Task.Run(() => download = DownloadSong(song));
        }

        /// <summary>
        /// Download the matching song from Newgrounds specified by the <paramref name="id"/>
        /// </summary>
        /// <param name="id">The id of the song to download</param>
        public SongDownloader(string id)
        {
            Task.Run(() =>
            {
                var getter = new SongMetaGetter(id);
                while (true)
                {
                    if (getter.Status >= TaskStatus.RanToCompletion)
                    {
                        Task.Run(() => download = DownloadSong(getter.Result));
                        break;
                    }
                }
            });
        }

        private async Task DownloadSong(SongMetadata song)
        {
            if (song == SongMetadata.Unknown)
                return;
            else
            {
                var filepath = $"{Database.GDLocalData}{Path.DirectorySeparatorChar}{song.ID}.mp3";
                var res = await client.GetAsync(song.DownloadLink);
                var bytes = await res.Content.ReadAsByteArrayAsync();

                File.WriteAllBytes(filepath, bytes);
            }
        }
    }
}
