using GDAPI.Application;
using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Functions.Extensions;
using GDAPI.Functions.GeometryDash;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.ColorChannels;
using GDAPI.Objects.GeometryDash.GamesaveStrings;
using GDAPI.Objects.GeometryDash.LevelObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.SpeedPortals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GDAPI.Functions.GeometryDash.Gamesave;
using static GDAPI.Information.GeometryDash.SongInformation;
using static GDAPI.Objects.GeometryDash.LevelObjects.LevelObjectCollection;
using static System.Convert;

namespace GDAPI.Objects.GeometryDash.General
{
    /// <summary>Represents a level in the game.</summary>
    public class Level
    {
        private Task loadLS;

        private LevelString unprocessedLevelString = (LevelString)DefaultLevelString; // Initialize in case there is no k4 property in the raw level
        private LevelString cachedLevelString;
        private bool canLoadLevelString;

        /// <summary>Indicates if the entire level has been successfully loaded.</summary>
        public bool IsFullyLoaded => loadLS?.Status >= TaskStatus.RanToCompletion;

        /// <summary>Raised upon having finished analyzing the newly set level string.</summary>
        public event Action LevelStringLoaded;

        #region Properties
        // Metadata
        /// <summary>Returns the name of the level followed by its revision if needed.</summary>
        public string LevelNameWithRevision => $"{Name}{(Revision > 0 ? $" (Rev. {Revision})" : "")}";
        /// <summary>The name of the level.</summary>
        [LevelStringMappable("k2")]
        public string Name { get; set; }
        /// <summary>The description of the level.</summary>
        [LevelStringMappable("k3")]
        public string Description { get; set; } = "";
        /// <summary>The name of the creator.</summary>
        [LevelStringMappable("k5")]
        public string CreatorName { get; set; }
        /// <summary>The revision of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k46")]
        public int Revision { get; set; }
        /// <summary>The attempts made in the level.</summary>
        [LevelStringMappable("k18")]
        public int Attempts { get; set; }
        /// <summary>The ID of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k1")]
        public int ID { get; set; }
        /// <summary>The version of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k16")]
        public int Version { get; set; }
        /// <summary>The folder of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k84")]
        public int Folder { get; set; }
        /// <summary>The password of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k41")]
        public int Password { get; set; }
        /// <summary>The copied level ID of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k42")]
        public int CopiedLevelID { get; set; }
        /// <summary>The binary version of the game the level was created on.</summary>
        [LevelStringMappable("k50")]
        public int BinaryVersion { get; set; }
        /// <summary>The time spent in the editor building the level in seconds.</summary>
        [LevelStringMappable("k80")]
        public int BuildTime { get; set; }
        /// <summary>The time spent in the editor building the level.</summary>
        public TimeSpan TotalBuildTime
        {
            get => new TimeSpan(0, 0, BuildTime);
            set => BuildTime = (int)value.TotalSeconds;
        }
        /// <summary>Determines whether the level has been verified or not.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k14")]
        public bool VerifiedStatus { get; set; }
        /// <summary>Determines whether the level has been uploaded or not.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k15")]
        public bool UploadedStatus { get; set; }
        /// <summary>Determines whether the level is unlisted or not.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k79")]
        public bool Unlisted { get; set; }
        /// <summary>The length of the level.</summary>
        [LevelStringMappable("k23")]
        public LevelLength Length => EnumConverters.GetLevelLength(TimeLength.TotalSeconds);
        /// <summary>The length of the level as a <seealso cref="TimeSpan"/> object.</summary>
        public TimeSpan TimeLength => TimeSpan.FromSeconds(SpeedSegments.ConvertXToTime(LevelObjects.Max(o => o.X)));

        // Level properties
        /// <summary>The official song ID used in the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k8")]
        public int OfficialSongID { get; set; }
        /// <summary>The custom song ID used in the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("k45")]
        public int CustomSongID { get; set; }
        /// <summary>The song offset of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA13")]
        public float SongOffset { get; set; }
        /// <summary>The fade in property of the song of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA15")]
        public bool FadeIn { get; set; }
        /// <summary>The fade out property of the song of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA16")]
        public bool FadeOut { get; set; }

        /// <summary>The starting speed of the player when the level begins.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA4")]
        public Speed StartingSpeed { get; set; }
        /// <summary>The starting gamemode of the player when the level begins.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA2")]
        public Gamemode StartingGamemode { get; set; }
        /// <summary>The starting size of the player when the level begins.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA3")]
        public PlayerSize StartingSize { get; set; }
        /// <summary>The Dual Mode property of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA8")]
        public bool DualMode { get; set; }
        /// <summary>The 2-Player Mode property of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA10")]
        public bool TwoPlayerMode { get; set; }
        /// <summary>The inversed gravity property of the level (there is no ability to set that from the game itself without hacking the gamesave).</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA11")]
        public bool InversedGravity { get; set; }
        /// <summary>The background texture property of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA6")]
        public int BackgroundTexture { get; set; }
        /// <summary>The ground texture property of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA7")]
        public int GroundTexture { get; set; }
        /// <summary>The ground line property of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA17")]
        public int GroundLine { get; set; }
        /// <summary>The font property of the level.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kA18")]
        public int Font { get; set; }
        /// <summary>The level's guidelines.</summary>
        [LevelStringMappable("kA14")]
        public GuidelineCollection Guidelines { get; set; } = new GuidelineCollection();
        /// <summary>The level's objects.</summary>
        public LevelObjectCollection LevelObjects { get; set; } = new LevelObjectCollection();
        /// <summary>The color channels of the level.</summary>
        [LevelStringMappable("kS38")]
        public LevelColorChannels ColorChannels { get; private set; }

        /// <summary>The absolute level object count (without excluding any object type).</summary>
        public int AbsoluteObjectCount => LevelObjects.Count;
        /// <summary>The level object count (excluding Start Pos objects).</summary>
        public int ObjectCount => AbsoluteObjectCount - ObjectCounts.ValueOrDefault((int)TriggerType.StartPos);
        /// <summary>The level trigger count.</summary>
        public int TriggerCount => LevelObjects.TriggerCount;
        /// <summary>Contains the count of objects per object ID in the collection.</summary>
        public Dictionary<int, int> ObjectCounts => LevelObjects.ObjectCounts;
        /// <summary>Contains the count of groups per object ID in the collection.</summary>
        public Dictionary<int, int> GroupCounts => LevelObjects.GroupCounts;
        /// <summary>The different object IDs in the collection.</summary>
        public int DifferentObjectIDCount => ObjectCounts.Keys.Count;
        /// <summary>The different object IDs in the collection.</summary>
        public int[] DifferentObjectIDs => ObjectCounts.Keys.ToArray();
        /// <summary>The group IDs in the collection.</summary>
        public int[] UsedGroupIDs => GroupCounts.Keys.ToArray();
        /// <summary>The speed segments of the level.</summary>
        public SpeedSegmentCollection SpeedSegments
        {
            get
            {
                var speedSegments = new SpeedSegmentCollection();
                speedSegments.Add(new SpeedSegment(StartingSpeed, 0));
                foreach (var o in LevelObjects)
                    if (o is SpeedPortal s && s.Checked)
                        speedSegments.Add(new SpeedSegment(s.Speed, s.X)); // TODO: Add offset based on hitbox and scaling and rotation
                return speedSegments;
            }
        }
        /// <summary>Gets the location of the custom song that is used in this level.</summary>
        public string CustomSongLocation => Database.GetCustomSongLocation(CustomSongID);

        // Editor stuff
        /// <summary>The X position of the camera.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kI1")]
        public double CameraX { get; set; }
        /// <summary>The Y position of the camera.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kI2")]
        public double CameraY { get; set; }
        /// <summary>The zoom of the camera.</summary>
        [CommonMergedProperty]
        [LevelStringMappable("kI3")]
        public double CameraZoom { get; set; }

        // Strings
        /// <summary>The level string in its decrypted form.</summary>
        [LevelStringMappable("k4")]
        public string LevelString
        {
            get => cachedLevelString ?? (cachedLevelString = (LevelString)GetLevelString());
            set
            {
                unprocessedLevelString = (LevelString)(value ?? DefaultLevelString);
                if (canLoadLevelString)
                    LoadLevelStringData();
            }
        }
        /// <summary>The raw form of the level as found in the gamesave.</summary>
        public string RawLevel
        {
            get => GetRawLevel();
            set => new XMLAnalyzer(value).AnalyzeXMLInformation(GetRawParameterInformation);
        }
        #endregion

        #region Constructors
        /// <summary>Creates a new empty instance of the <see cref="Level"/> class.</summary>
        public Level()
        {
            ColorChannels = new LevelColorChannels();
        }
        /// <summary>Creates a new instance of the <see cref="Level"/> class from a raw string containing a level and gets its info.</summary>
        /// <param name="level">The raw string containing the level.</param>
        /// <param name="initializeBackgroundLevelStringLoading">Determines whether loading the infromation from the level string will be initialized on the background.</param>
        public Level(string level, bool initializeBackgroundLevelStringLoading = true)
        {
            canLoadLevelString = initializeBackgroundLevelStringLoading;
            RawLevel = level;
        }
        /// <summary>Creates a new instance of the <see cref="Level"/> class from a specified name, description, level string, revision and the creator's name.</summary>
        /// <param name="name">The name of the level.</param>
        /// <param name="description">The description of the level.</param>
        /// <param name="levelString">The level string of the level.</param>
        /// <param name="creatorName">The name of the creator.</param>
        /// <param name="revision">The revision of the level.</param>
        public Level(string name, string description, string levelString, string creatorName, int revision = 0)
            : this(GenerateLevelString(name, description, levelString, creatorName, revision)) { }
        #endregion

        #region Functions
        /// <summary>Initializes the process of loading the level string data. If this has already happened, no task is run.</summary>
        public async Task InitializeLoadingLevelString()
        {
            canLoadLevelString = true;
            if (loadLS == null || loadLS.Status == TaskStatus.Created)
                await LoadLevelStringData();
        }
        /// <summary>Unloads the level string's information, which includes level objects, guidelines and color channels.</summary>
        public void UnloadLevelString()
        {
            loadLS = null;
            LevelObjects.Clear();
            Guidelines.Clear();
            ColorChannels = null;
        }

        /// <summary>Returns the metadata of the song, given the song metadata collection found in the database.</summary>
        /// <param name="metadata">The song metadata collection of the database, based on which the song metadata is retrieved.</param>
        public SongMetadata GetSongMetadata(SongMetadataCollection metadata)
        {
            if (CustomSongID == 0)
            {
                if (OfficialSongID >= 0 && OfficialSongID < OfficialSongMetadata.Length)
                    return OfficialSongMetadata[OfficialSongID];

                return SongMetadata.Unknown;
            }
            return metadata.Find(s => s.ID == CustomSongID);
        }
        /// <summary>Clones this level and returns the cloned result.</summary>
        public Level Clone() => new Level(RawLevel.Substring(0));
        /// <summary>Returns the level string of this <seealso cref="Level"/>.</summary>
        public string GetLevelString() => $"kS38,{ColorChannels},kA13,{SongOffset},kA15,{(FadeIn ? "1" : "0")},kA16,{(FadeOut ? "1" : "0")},kA14,{Guidelines},kA6,{BackgroundTexture},kA7,{GroundTexture},kA17,{GroundLine},kA18,{Font},kS39,0,kA2,{(int)StartingGamemode},kA3,{(int)StartingSize},kA8,{(DualMode ? "1" : "0")},kA4,{(int)StartingSpeed},kA9,0,kA10,{(TwoPlayerMode ? "1" : "0")},kA11,{(InversedGravity ? "1" : "0")};{LevelObjects}";
        /// <summary>Returns the raw level string of this <seealso cref="Level"/>.</summary>
        public string GetRawLevel() => $"<k>kCEK</k><i>4</i><k>k1</k><i>{ID}</i><k>k2</k><s>{Name}</s><k>k4</k><s>{LevelString}</s>{(Description.Length > 0 ? $"<k>k3</k><s>{ToBase64String(Encoding.ASCII.GetBytes(Description))}</s>" : "")}<k>k46</k><i>{Revision}</i><k>k5</k><s>{CreatorName}</s><k>k13</k><t />{GetBoolPropertyString("k14", VerifiedStatus)}{GetBoolPropertyString("k15", UploadedStatus)}{GetBoolPropertyString("k79", Unlisted)}<k>k21</k><i>2</i><k>k16</k><i>{Version}</i><k>k23</k><s>{(int)Length}</s><k>k8</k><i>{OfficialSongID}</i><k>k45</k><i>{CustomSongID}</i><k>k80</k><i>{BuildTime}</i><k>k50</k><i>{BinaryVersion}</i><k>k47</k><t /><k>k84</k><i>{Folder}</i><k>kI1</k><r>{CameraX}</r><k>kI2</k><r>{CameraY}</r><k>kI3</k><r>{CameraZoom}</r>";
        /// <summary>Clears the cached level string data. This has to be manually called upon preparation for changes in the level.</summary>
        public void ClearCachedLevelStringData() => cachedLevelString = null;

        /// <summary>Adds a <seealso cref="GuidelineCollection"/> to this level's <seealso cref="GuidelineCollection"/>.</summary>
        /// <param name="guidelines">The <seealso cref="GuidelineCollection"/> to add.</param>
        /// <param name="appendGuidelines">If <see langword="true"/>, the <seealso cref="GuidelineCollection"/> will be appended to the level and the previous ones will be preserved, otherwise the new ones will replace the old ones.</param>
        public void AddGuidelines(GuidelineCollection guidelines, bool appendGuidelines = true)
        {
            if (appendGuidelines)
                Guidelines.AddRange(guidelines);
            else
                Guidelines = guidelines;
        }
        #endregion

        #region Static Functions
        /// <summary>Merges a number of levels together. All their objects are concatenated, colors are kept the same if equal at respective IDs, otherwise reset, and the rest of the properties (excluding some metadata) are kept the same if respectively equal, otherwise reset.</summary>
        /// <param name="levels">The levels to merge.</param>
        public static Level MergeLevels(params Level[] levels)
        {
            if (levels.Length == 1)
                return levels[0];
            if (levels.Length == 0)
                throw new Exception("Cannot merge 0 levels.");
            Level result = levels[0].Clone();
            for (int i = 1; i < levels.Length; i++)
            {
                result.LevelObjects.AddRange(levels[i].LevelObjects);
                result.Guidelines.AddRange(levels[i].Guidelines);
                result.BuildTime += levels[i].BuildTime;
                if (levels[i].BinaryVersion > result.BinaryVersion)
                    result.BinaryVersion = levels[i].BinaryVersion;
            }
            result.ColorChannels = LevelColorChannels.GetCommonColors(levels);
            result.Guidelines.RemoveDuplicatedGuidelines();
            var properties = typeof(Level).GetProperties();
            foreach (var p in properties)
            {
                if (p.GetCustomAttributes(typeof(CommonMergedPropertyAttribute), false).Length > 0)
                {
                    bool common = true;
                    for (int i = 1; i < levels.Length && common; i++)
                        if (!(common = p.GetValue(result) != p.GetValue(levels[i])))
                            p.SetValue(result, default);
                }
            }
            return result;
        }
        #endregion

        /// <summary>Returns a <seealso cref="string"/> that represents the current object.</summary>
        public override string ToString() => RawLevel;

        #region Private stuff
        private async Task LoadLevelStringData() => await PerformTaskWithInvocableEvent(loadLS = RunLoadLevelStringData(), LevelStringLoaded);
        private async Task RunLoadLevelStringData()
        {
            unprocessedLevelString.TryDecrypt(out var c, false);
            GetLevelStringInformation(cachedLevelString = (LevelString)c);
            unprocessedLevelString = null; // Free some memory; not too bad
        }

        private void GetLevelStringInformation(string levelString)
        {
            string infoString = levelString.Substring(0, levelString.IndexOf(';'));
            string[] split = infoString.Split(',');
            for (int i = 0; i < split.Length; i += 2)
                GetLevelStringParameterInformation(split[i], split[i + 1]);
            LevelObjects = ParseObjects(GetObjectString(levelString));
        }
        private void GetLevelStringParameterInformation(string key, string value)
        {
            switch (key)
            {
                case "kA2": // Gamemode
                    StartingGamemode = (Gamemode)ToInt32(value);
                    break;
                case "kA3": // Player Size
                    StartingSize = (PlayerSize)ToInt32(value);
                    break;
                case "kA4": // Speed
                    StartingSpeed = (Speed)ToInt32(value);
                    break;
                case "kA6": // Background
                    BackgroundTexture = ToInt32(value);
                    break;
                case "kA7": // Ground
                    GroundTexture = ToInt32(value);
                    break;
                case "kA8": // Dual Mode
                    DualMode = value == "1"; // Seriously the easiest way to determine whether it's true or not
                    break;
                case "kA10": // 2-Player Mode
                    TwoPlayerMode = value == "1";
                    break;
                case "kA11": // Inversed Gravity
                    InversedGravity = value == "1";
                    break;
                case "kA13": // Song Offset
                    SongOffset = ToSingle(value);
                    break;
                case "kA14": // Guidelines
                    Guidelines = GuidelineCollection.Parse(value);
                    break;
                case "kA15": // Fade In
                    FadeIn = value == "1";
                    break;
                case "kA16": // Fade Out
                    FadeOut = value == "1";
                    break;
                case "kA17": // Ground Line
                    GroundLine = ToInt32(value);
                    break;
                case "kA18": // Font
                    Font = ToInt32(value);
                    break;
                case "kS38": // Color Channel
                    ColorChannels = LevelColorChannels.Parse(value);
                    break;
                case "kA9": // Level/Start Pos
                case "kS39": // Color Page
                    // We don't care
                    break;
                default: // We need to know more about that suspicious new thing so we keep a log of it
                    Directory.CreateDirectory("ulsk");
                    if (!File.Exists($@"ulsk\{key}.key"))
                        File.WriteAllText($@"ulsk\{key}.key", key);
                    break;
            }
        }
        private void GetRawParameterInformation(string key, string value, string valueType)
        {
            switch (key)
            {
                case "k1": // Level ID
                    ID = ToInt32(value);
                    break;
                case "k2": // Level Name
                    Name = value;
                    break;
                case "k3": // Level Description
                    Description = Encoding.UTF8.GetString(Convert.FromBase64String(value.ConvertBase64URLToNormal()));
                    break;
                case "k4": // Level String
                    LevelString = value;
                    break;
                case "k5": // Creator Name
                    CreatorName = value;
                    break;
                case "k8": // Official Song ID
                    OfficialSongID = ToInt32(value);
                    break;
                case "k14": // Level Verified Status
                    VerifiedStatus = valueType == "t /"; // Well that's how it's implemented ¯\_(ツ)_/¯
                    break;
                case "k15": // Level Uploaded Status
                    UploadedStatus = valueType == "t /";
                    break;
                case "k16": // Level Version
                    Version = ToInt32(value);
                    break;
                case "k18": // Level Attempts
                    Attempts = ToInt32(value);
                    break;
                case "k41": // Level Password
                    Password = ToInt32(value) % 1000000; // If it exists, the password is in the form $"1{password}" (example: 1000023 = 0023)
                    break;
                case "k45": // Custom Song ID
                    CustomSongID = ToInt32(value);
                    break;
                case "k46": // Level Revision
                    Revision = ToInt32(value);
                    break;
                case "k50": // Binary Version
                    BinaryVersion = ToInt32(value);
                    break;
                case "k79": // Unlisted
                    Unlisted = valueType == "t /";
                    break;
                case "k80": // Time Spent
                    BuildTime = ToInt32(value);
                    break;
                case "k84": // Level Folder
                    Folder = ToInt32(value);
                    break;
                case "kI1": // Camera X
                    CameraX = ToDouble(value);
                    break;
                case "kI2": // Camera Y
                    CameraY = ToDouble(value);
                    break;
                case "kI3": // Camera Zoom
                    CameraZoom = ToDouble(value);
                    break;
                default: // Not something we care about
                    break;
            }
        }
        private string GetBoolPropertyString(string key, bool value) => value ? $"<k>{key}</k><t />" : "";
        #endregion

        private static string GenerateLevelString(string name, string description, string levelString, string creatorName, int revision)
        {
            return $"<k>kCEK</k><i>4</i><k>k2</k><s>{name}</s><k>k4</k><s>{levelString}</s>{(description.Length > 0 ? $"<k>k3</k><s>{ToBase64String(Encoding.ASCII.GetBytes(description))}</s>" : "")}<k>k46</k><i>{revision}</i><k>k5</k><s>{creatorName}</s><k>k13</k><t /><k>k21</k><i>2</i><k>k16</k><i>1</i><k>k80</k><i>0</i><k>k50</k><i>35</i><k>k47</k><t /><k>kI1</k><r>0</r><k>kI2</k><r>36</r><k>kI3</k><r>1</r>";
        }
        private static async Task PerformTaskWithInvocableEvent(Task task, Action invocableEvent)
        {
            task.ContinueWith(_ => invocableEvent?.Invoke());
            await task;
        }
    }
}