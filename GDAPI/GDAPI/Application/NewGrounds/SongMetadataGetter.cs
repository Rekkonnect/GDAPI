using GDAPI.Objects.GeometryDash.General;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAPI.Application.Newgrounds
{
    // TODO: Rework this class to support multiple song metadata retrievals.
    /// <summary>Handles <seealso cref="SongMetadata"/> retrieval for the specified song ID.</summary>
    public class SongMetadataGetter : IDisposable
    {
        private readonly HttpClient client = new HttpClient();

        private Task<SongMetadata> getSongMetadataTask;

        /// <summary>Gets the status of the song metadata retrieval task.</summary>
        public TaskStatus Status => getSongMetadataTask?.Status ?? (TaskStatus)(-1);
        /// <summary>The <seealso cref="SongMetadata"/> that was retrieved.</summary>
        public SongMetadata Result => getSongMetadataTask?.Result ?? SongMetadata.Unknown;

        /// <summary>The delegate to execute after finishing the song metadata's retrieval.</summary>
        public event Action<SongMetadata> FinishedRetrieval;

        /// <summary>Initializes a new instance of the <seealso cref="SongMetadataGetter"/> class and immediately initializes the retrieval operation for the requested song ID.</summary>
        /// <param name="id">The song ID of the song on Newgrounds.</param>
        /// <param name="finishedRetrievalDelegate">The delegate to execute after finishing the song metadata's retrieval.</param>
        public SongMetadataGetter(int id, Action<SongMetadata> finishedRetrievalDelegate = null)
        {
            FinishedRetrieval = finishedRetrievalDelegate;
            getSongMetadataTask = GetSongMetadataAsync(id);
            getSongMetadataTask.ContinueWith(a => FinishedRetrieval?.Invoke(a.Result));
        }
        ~SongMetadataGetter()
        {
            Dispose();
        }

        /// <summary>Disposes this instance's unmanaged resources.</summary>
        public void Dispose() => client.Dispose();

        private async Task<SongMetadata> GetSongMetadataAsync(int id)
        {
            //Getting raw song metadata from Boomlings website
            var content = GetFormContentForRequest(id);
            var res = await client.PostAsync("http://www.boomlings.com/database/getGJSongInfo.php", content);
            var rawMetadata = await res.Content.ReadAsStringAsync();

            return SongMetadata.ParseWebsiteData(rawMetadata);
        }

        /// <summary>Gets the <seealso cref="FormUrlEncodedContent"/> that should be used when requesting the song ID information.</summary>
        /// <param name="id">The song ID that will be requested.</param>
        /// <returns>The <seealso cref="FormUrlEncodedContent"/> that was generated.</returns>
        public static FormUrlEncodedContent GetFormContentForRequest(int id)
        {
            return new FormUrlEncodedContent(new Dictionary<string, string> { { "songID", id.ToString() }, { "secret", "Wmfd2893gb7" } });
        }
    }
}
