using GDAPI.Functions.General;
using GDAPI.Objects.GeometryDash.General;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Application.Newgrounds
{
    class SongMetaGetter
    {
        private readonly HttpClient client = new HttpClient();

        private Task<SongMetadata> getSongMetadataTask;

        public TaskStatus Status => getSongMetadataTask?.Status ?? (TaskStatus)(-1);
        public SongMetadata Result => getSongMetadataTask?.Result ?? SongMetadata.Unknown;

        /// <summary>Get the <seealso cref="SongMetadata"/> of a song specified by the provided <paramref name="id"/></summary>
        /// <param name="id">the id of the song on Newgrounds.com</param>
        public SongMetaGetter(string id)
        {
            Task.Run(() => getSongMetadataTask = GetSongMetadataAsync(id));
        }

        private async Task<SongMetadata> GetSongMetadataAsync(string id)
        {
            //Getting raw song metadata from Boomlings website
            var content = new FormUrlEncodedContent(new Dictionary<string, string> { { "songID", id }, { "secret", "Wmfd2893gb7" } });
            var res = await client.PostAsync("http://www.boomlings.com/database/getGJSongInfo.php", content);
            var rawMeta = await res.Content.ReadAsStringAsync();

            if (rawMeta == "-2") // song "not allowed for use" or not found
                return SongMetadata.Unknown;

            var metaChars = rawMeta.ToCharArray();
            var meta = new SongMetadata();
            for (int i = 0; i < rawMeta.Length; i++)
            {
                int c = 0;
                var value = new StringBuilder();

                if (metaChars[i] == '~' && metaChars[i + 1] == '|')
                {
                    string index = metaChars[i - 1].ToString();
                    if (index == "0") // Homemade little fix cuz i suc lmao
                        index = "10";

                    if (!(i + 3 + c >= rawMeta.Length))
                    {
                        while (metaChars[i + 3 + c] != '~')
                        {
                            value.Append(metaChars[i + 3 + c]);
                            c++;
                        }
                    }

                    i += c + 3;

                    switch (index)
                    {
                        case "1": // SongID
                            meta.ID = int.Parse(value.ToString());
                            break;
                        case "2": // Title
                            meta.Title = value.ToString();
                            break;
                        case "3": // ?
                            meta.UnknownKey3 = int.Parse(value.ToString());
                            break;
                        case "4": // Artist
                            meta.Artist = value.ToString();
                            break;
                        case "5": // Song Size (MB)
                            meta.SongSizeMB = double.Parse(value.ToString(), CultureInfo.InvariantCulture);
                            break;
                        case "7": // ?
                            meta.UnknownKey7 = value.ToString();
                            break;
                        case "9": // ?
                            meta.UnknownKey9 = int.Parse(value.ToString());
                            break;
                        case "10":
                            meta.DownloadLink = UsefulFunctions.ConvertStringWithHexCharsToProperString(value.ToString());
                            break;
                        default: // nope go back
                            break;
                    }
                }
            }
            return meta;
        }
    }
}
