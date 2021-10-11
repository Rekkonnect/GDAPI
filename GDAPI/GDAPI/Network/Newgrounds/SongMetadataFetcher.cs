using System;
using System.Threading.Tasks;
using GDAPI.Objects.GeometryDash.General;

namespace GDAPI.Network.Newgrounds
{
    // TODO: Rework this class to support multiple song metadata retrievals.
    /// <summary>Handles <seealso cref="SongMetadata"/> retrieval for the specified song ID.</summary>
    public class SongMetadataFetcher : IDisposable
    {
        private readonly NetworkClient client = new();

        private Task<SongMetadata> getSongMetadataTask;

        /// <summary>Gets the status of the song metadata retrieval task.</summary>
        public TaskStatus Status => getSongMetadataTask?.Status ?? (TaskStatus)(-1);
        /// <summary>The <seealso cref="SongMetadata"/> that was retrieved.</summary>
        public SongMetadata Result => getSongMetadataTask?.Result ?? SongMetadata.Unknown;

        /// <summary>The delegate to execute after finishing the song metadata's retrieval.</summary>
        public event Action<SongMetadata>? FinishedRetrieval;
        
        /// <summary>Initializes a new instance of the <seealso cref="SongMetadataFetcher"/> class and immediately initializes the retrieval operation for the requested song ID.</summary>
        /// <param name="id">The song ID of the song on Newgrounds.</param>
        /// <param name="finishedRetrievalDelegate">The delegate to execute after finishing the song metadata's retrieval.</param>
        public SongMetadataFetcher(int id, Action<SongMetadata>? finishedRetrievalDelegate = null)
        {
            FinishedRetrieval = finishedRetrievalDelegate;
            getSongMetadataTask = GetSongMetadataAsync(id);
            getSongMetadataTask.ContinueWith(a => FinishedRetrieval?.Invoke(a.Result));
        }

        private async Task<SongMetadata> GetSongMetadataAsync(int id)
        {
            var request = new SongInfoWebRequest(id);

            return request.MakeRequest<SongInfoWebResult>(client).Metadata;
        }

        #region Disposal
        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                client.Dispose();
            }
        }

        ~SongMetadataFetcher()
        {
            Dispose(false);
        }
        #endregion

        private class SongInfoWebRequest : BoomlingsWebRequest
        {
            public SongInfoWebRequest(int songId)
                : base(new { songID = songId })
            {
            }

            protected override string Path => "getGJSongInfo";
        }

        private class SongInfoWebResult : WebResult
        {
            public SongMetadata Metadata { get; private set; }

            public override void ParseWebResponse(string response)
            {
                Metadata = SongMetadata.ParseWebsiteData(response);
            }
        }
    }
}