using GDAPI.Attributes;
using GDAPI.Objects.General;
using System.Globalization;
using static GDAPI.Functions.General.Parsing;

namespace GDAPI.Objects.GeometryDash.General
{
    /// <summary>Contains the metadata of a song.</summary>
    public class SongMetadata
    {
        private const int UnknownID = -1;
        private const int OfficialSongID = -2;

        /// <summary>Returns a <seealso cref="SongMetadata"/> that indicates an unknown song.</summary>
        public static SongMetadata Unknown => new()
        {
            ID = UnknownID,
            Title = "Unknown",
            Artist = "Unknown",
        };

        /// <summary>The ID of the song.</summary>
        [SongMetadataStringMappable(1)]
        public int ID { get; set; }
        /// <summary>The title of the song.</summary>
        [SongMetadataStringMappable(2)]
        public string Title { get; set; }
        /// <summary>The ID of the artist.</summary>
        [SongMetadataStringMappable(3)]
        public int ArtistID { get; set; }
        /// <summary>The artist of the song.</summary>
        [SongMetadataStringMappable(4)]
        public string Artist { get; set; }
        /// <summary>The size of the song in MB.</summary>
        [SongMetadataStringMappable(5)]
        public double SongSizeMB { get; set; }
        /// <summary>The ID of the YouTube video that is linked to this song.</summary>
        [SongMetadataStringMappable(6)]
        public string YouTubeSongVideoID { get; set; }
        /// <summary>The ID of the YouTube channel of the artist.</summary>
        [SongMetadataStringMappable(7)]
        public string YouTubeArtistChannelID { get; set; }
        /// <summary>The download link of the song.</summary>
        [SongMetadataStringMappable(10)]
        public string DownloadLink { get; set; }

        /// <summary>The value of the unknown key 9.</summary>
        [SongMetadataStringMappable(9)]
        public int UnknownKey9 { get; set; }

        /// <summary>The URL to the song on Newgrounds.</summary>
        public string NewgroundsSongURL => $"https://www.newgrounds.com/audio/listen/{ID}";
        /// <summary>The URL to the artist on Newgrounds.</summary>
        public string NewgroundsArtistURL => $"https://{Artist}.newgrounds.com/";
        /// <summary>The URL to the song video on YouTube.</summary>
        public string YouTubeSongURL => $"https://www.youtube.com/watch?v={YouTubeSongVideoID}";
        /// <summary>The URL to the artist channel on YouTube.</summary>
        public string YouTubeChannelURL => $"https://www.youtube.com/channel/{YouTubeArtistChannelID}";

        /// <summary>Returns whether the metadata reflects an official song.</summary>
        public bool IsOfficial => ID == OfficialSongID;

        /// <summary>Initializes a new instance of the <seealso cref="SongMetadata"/> class.</summary>
        public SongMetadata() { }

        /// <summary>Initializes a new instance of the <seealso cref="SongMetadata"/> class.</summary>
        /// <param name="data">The data of the <seealso cref="SongMetadata"/>.</param>
        public SongMetadata(string data)
        {
            new XMLAnalyzer(data).AnalyzeXMLInformation(GetSongMetadataParameterInformation);
        }

        /// <summary>Sets the value of a keyed property of this object.</summary>
        /// <param name="key">The key of the property to set.</param>
        /// <param name="value">The value to set to the property.</param>
        public void SetValue(string key, string value)
        {
            switch (key)
            {
                case "1":
                    ID = ParseInt32(value);
                    break;
                case "2":
                    Title = value;
                    break;
                case "3":
                    ArtistID = ParseInt32(value);
                    break;
                case "4":
                    Artist = value;
                    break;
                case "5":
                    SongSizeMB = ParseDouble(value);
                    break;
                case "6":
                    YouTubeSongVideoID = value;
                    break;
                case "7":
                    YouTubeArtistChannelID = value;
                    break;
                case "9":
                    UnknownKey9 = ParseInt32(value);
                    break;
                case "10":
                    DownloadLink = value;
                    break;
                default: // Not something we care about
                    break;
            }
        }

        /// <summary>Creates a new <seealso cref="SongMetadata"/> instance for an official song.</summary>
        /// <param name="artist">The artist of the official song.</param>
        /// <param name="title">The title of the official song.</param>
        /// <returns>A new <seealso cref="SongMetadata"/> instance reflecting an official song.</returns>
        public static SongMetadata CreateOfficialSongMetadata(string artist, string title) => new()
        {
            ID = OfficialSongID,
            Artist = artist,
            Title = title,
        };

        /// <summary>Parses the data into a <seealso cref="SongMetadata"/> instance.</summary>
        /// <param name="data">The data to parse into a <seealso cref="SongMetadata"/> instance.</param>
        public static SongMetadata Parse(string data) => new(data);

        /// <summary>Parses <seealso cref="SongMetadata"/> information rendered as a website response.</summary>
        /// <param name="data">The website response rendered data to parse.</param>
        /// <returns>The parsed <seealso cref="SongMetadata"/> instance.</returns>
        public static SongMetadata ParseWebsiteData(string data)
        {
            var split = data.Split("~|~");
            // some error code probably, definitely not a valid response
            if (split.Length < 2)
                return Unknown;
            
            var metadata = new SongMetadata();
            for (int i = 0; i < split.Length; i += 2)
                metadata.SetValue(split[i], split[i + 1]);

            return metadata;
        }

        private void GetSongMetadataParameterInformation(string key, string value, string valueType) => SetValue(key, value);

        /// <summary>Returns the equivalent <seealso cref="string"/> value of this <seealso cref="SongMetadata"/> instance.</summary>
        public override string ToString() => $"<k>kCEK</k><i>6</i><k>1</k><i>{ID}</i><k>2</k><s>{Title}</s><k>3</k><i>{ArtistID}</i><k>4</k><s>{Artist}</s><k>5</k><r>{SongSizeMB.ToString(CultureInfo.InvariantCulture)}</r><k>6</k><s>{YouTubeSongVideoID}</s><k>7</k><s>{YouTubeArtistChannelID}</s><k>9</k><i>{UnknownKey9}</i><k>10</k><s>{DownloadLink}</s>";
    }
}
