using GDAPI.Attributes;
using GDAPI.Objects.General;
using static System.Convert;

namespace GDAPI.Objects.GeometryDash.General
{
    /// <summary>Contains the metadata of a song.</summary>
    public class SongMetadata
    {
        /// <summary>Returns a <seealso cref="SongMetadata"/> that indicates an unknown song.</summary>
        public static SongMetadata Unknown => new SongMetadata
        {
            ID = -1,
            Title = "Unknown",
            Artist = "Unknown",
        };

        /// <summary>The ID of the song.</summary>
        [SongMetadataStringMappable(1)]
        public int ID { get; set; }
        /// <summary>The title of the song.</summary>
        [SongMetadataStringMappable(2)]
        public string Title { get; set; }
        /// <summary>The artist of the song.</summary>
        [SongMetadataStringMappable(4)]
        public string Artist { get; set; }
        /// <summary>The size of the song in MB.</summary>
        [SongMetadataStringMappable(5)]
        public double SongSizeMB { get; set; }
        /// <summary>The URL to the song on Newgrounds.</summary>
        public string URL => $"https://www.newgrounds.com/audio/listen/{ID}";
        /// <summary>The download link of the song.</summary>
        [SongMetadataStringMappable(10)]
        public string DownloadLink { get; set; }

        /// <summary>The value of the unknown key 3.</summary>
        [SongMetadataStringMappable(3)]
        public int UnknownKey3 { get; set; }
        /// <summary>The value of the unknown key 7.</summary>
        [SongMetadataStringMappable(7)]
        public string UnknownKey7 { get; set; }
        /// <summary>The value of the unknown key 9.</summary>
        [SongMetadataStringMappable(9)]
        public int UnknownKey9 { get; set; }

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
                case "1": // Song ID
                    ID = ToInt32(value);
                    break;
                case "2": // Title
                    Title = value;
                    break;
                case "3": // ?
                    UnknownKey3 = ToInt32(value);
                    break;
                case "4": // Artist
                    Artist = value;
                    break;
                case "5": // Song Size (MB)
                    SongSizeMB = ToDouble(value);
                    break;
                case "7": // ?
                    UnknownKey7 = value;
                    break;
                case "9": // ?
                    UnknownKey9 = ToInt32(value);
                    break;
                case "10": // Download Link
                    DownloadLink = value;
                    break;
                default: // Not something we care about
                    break;
            }
        }

        /// <summary>Parses the data into a <seealso cref="SongMetadata"/> instance.</summary>
        /// <param name="data">The data to parse into a <seealso cref="SongMetadata"/> instance.</param>
        public static SongMetadata Parse(string data) => new SongMetadata(data);

        /// <summary>Parses <seealso cref="SongMetadata"/> information rendered as a website response.</summary>
        /// <param name="data">The website response rendered data to parse.</param>
        /// <returns>The parsed <seealso cref="SongMetadata"/> instance.</returns>
        public static SongMetadata ParseWebsiteData(string data)
        {
            if (data == "-2") // song "not allowed for use" or not found
                return Unknown;

            var split = data.Split("~|~");
            var metadata = new SongMetadata();
            for (int i = 0; i < split.Length; i += 2)
                metadata.SetValue(split[i], split[i + 1]);

            return metadata;
        }

        private void GetSongMetadataParameterInformation(string key, string value, string valueType) => SetValue(key, value);

        /// <summary>Returns the equivalent <seealso cref="string"/> value of this <seealso cref="SongMetadata"/> instance.</summary>
        public override string ToString() => $"<k>kCEK</k><i>6</i><k>1</k><i>{ID}</i><k>2</k><s>{Title}</s><k>3</k><i>{UnknownKey3}</i><k>4</k><s>{Artist}</s><k>5</k><r>{SongSizeMB}</r><k>7</k><s>{UnknownKey7}</s><k>9</k><i>{UnknownKey9}</i><k>10</k><s>{DownloadLink}</s>";
    }
}
