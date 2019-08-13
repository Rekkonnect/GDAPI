using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using GDAPI.Utilities.Functions.Extensions;
using GDAPI.Utilities.Functions.General;
using GDAPI.Utilities.Functions.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash;
using static System.Convert;
using static System.Environment;
using static GDAPI.Utilities.Functions.GeometryDash.Gamesave;
using System.Threading;

namespace GDAPI.Application
{
    /// <summary>Contains information about a database for the game.</summary>
    public class Database
    {
        private static readonly int Cores = ProcessorCount;

        private string decryptedGamesave;
        private string decryptedLevelData;

        private Task[] threadTasks;
        private CancellationTokenSource[] tokens;
        private List<int> levelIndicesToLoad;
        private List<int> currentlyCancelledIndices;
        private int nextAvailableLevelIndex;
        private object lockObject = new object();

        private Task setDecryptedGamesave;
        private Task setDecryptedLevelData;
        private Task getFolderNames;
        private Task getPlayerName;
        private Task getCustomObjects;
        private Task getSongMetadata;
        private Task getLevels;
        private Task<(bool, string)> decryptGamesave;
        private Task<(bool, string)> decryptLevelData;

        #region Database Status
        public TaskStatus SetDecryptedGamesaveStatus => setDecryptedGamesave?.Status ?? (TaskStatus)(-1);
        public TaskStatus SetDecryptedLevelDataStatus => setDecryptedLevelData?.Status ?? (TaskStatus)(-1);
        public TaskStatus GetFolderNamesStatus => getFolderNames?.Status ?? (TaskStatus)(-1);
        public TaskStatus GetPlayerNameStatus => getPlayerName?.Status ?? (TaskStatus)(-1);
        public TaskStatus GetCustomObjectsStatus => getCustomObjects?.Status ?? (TaskStatus)(-1);
        public TaskStatus GetSongMetadataStatus => getSongMetadata?.Status ?? (TaskStatus)(-1);
        public TaskStatus GetLevelsStatus => getLevels?.Status ?? (TaskStatus)(-1);
        public TaskStatus DecryptGamesaveStatus => decryptGamesave?.Status ?? (TaskStatus)(-1);
        public TaskStatus DecryptLevelDataStatus => decryptLevelData?.Status ?? (TaskStatus)(-1);
        #endregion

        #region Constants
        /// <summary>The default local data folder path of the game.</summary>
        //TODO: Check platform with RuntimeInformation.IsOSPlatform() after mono implements such
        public static readonly string GDLocalData = $@"{GetFolderPath(SpecialFolder.LocalApplicationData)}{(OSVersion.Platform == PlatformID.Unix ? "/Steam/steamapps/compatdata/322170/pfx/drive_c/users/steamuser/Local Settings/Application Data/" : @"\")}GeometryDash";
        /// <summary>The default game manager file path of the game.</summary>
        public static readonly string GDGameManager = $@"{GDLocalData}{Path.DirectorySeparatorChar}CCGameManager.dat";
        /// <summary>The default local levels file path of the game.</summary>
        public static readonly string GDLocalLevels = $@"{GDLocalData}{Path.DirectorySeparatorChar}CCLocalLevels.dat";
        #endregion

        #region Information
        /// <summary>The path for the game manager file.</summary>
        public string GameManagerPath { get; private set; }
        /// <summary>The path for the local levels file.</summary>
        public string LocalLevelsPath { get; private set; }

        // TODO: Split into LevelInfo and GameInfo

        /// <summary>The level key start indices of the database in the local levels file.</summary>
        public List<int> LevelKeyStartIndices { get; private set; }

        /// <summary>The user name of the player as found in the game manager file.</summary>
        public string UserName { get; set; }
        /// <summary>The user levels in the database.</summary>
        public LevelCollection UserLevels { get; set; }
        /// <summary>The names of the folders.</summary>
        public FolderNameCollection FolderNames { get; set; }
        /// <summary>The stored metadata information of the songs.</summary>
        public SongMetadataCollection SongMetadataInformation { get; set; }

        /// <summary>The decrypted form of the game manager.</summary>
        public string DecryptedGamesave
        {
            get
            {
                if (decryptedGamesave == null)
                {
                    TryDecryptGamesave(File.ReadAllText(GameManagerPath), out decryptedGamesave);
                    DecryptedGamesave = decryptedGamesave;
                }
                SetFolderNamesInGamesave();
                SetCustomObjectsInGamesave();
                SetSongMetadataInGamesave();
                return decryptedGamesave;
            }
            set => Task.Run(() => setDecryptedGamesave = SetDecryptedGamesave(value));
        }
        /// <summary>The decrypted form of the level data.</summary>
        public string DecryptedLevelData
        {
            get
            {
                if (decryptedLevelData == null)
                {
                    LevelKeyStartIndices = new List<int>();
                    StringBuilder lvlDat = new StringBuilder(LevelDataStart);
                    for (int i = 0; i < UserLevelCount; i++)
                    {
                        lvlDat = lvlDat.Append($"<k>k_{i}</k><d>");
                        LevelKeyStartIndices.Add(lvlDat.Length);
                        lvlDat = lvlDat.Append(UserLevels[i].RawLevel).Append("</d>");
                    }
                    lvlDat = lvlDat.Append(LevelDataEnd);
                    decryptedLevelData = lvlDat.ToString();
                }
                return decryptedLevelData;
            }
            set => Task.Run(() => setDecryptedLevelData = SetDecryptedLevelData(value));
        }
        /// <summary>The custom objects.</summary>
        public CustomLevelObjectCollection CustomObjects { get; set; }

        /// <summary>The number of local levels in the level data file.</summary>
        public int UserLevelCount => UserLevels.Count;
        #endregion

        #region Contsructors
        /// <summary>Initializes a new instance of the <seealso cref="Database"/> class from the default database file paths.</summary>
        public Database() : this(GDGameManager, GDLocalLevels) { }
        /// <summary>Initializes a new instance of the <seealso cref="Database"/> class from custom database file paths.</summary>
        /// <param name="gameManagerPath">The file path of the game manager file of the game.</param>
        /// <param name="localLevelsPath">The file path of the local levels file of the game.</param>
        public Database(string gameManagerPath, string localLevelsPath)
        {
            Task.Run(() => SetDecryptedGamesave(File.ReadAllText(GameManagerPath = gameManagerPath)));
            Task.Run(() => SetDecryptedLevelData(File.ReadAllText(LocalLevelsPath = localLevelsPath)));
        }
        #endregion

        // TODO: Order these appropriately
        #region Functions
        /// <summary>Opens the first level that matches the specified name for editing, ordered from top to bottom in the list. The level's cached level string data is cleared.</summary>
        /// <param name="name">The name of the level to open for editing.</param>
        public Level OpenLevelForEditing(string name) => OpenLevelForEditing(UserLevels.FindIndex(l => l.Name == name));
        /// <summary>Opens the first level that matches the specified name and revision for editing, ordered from top to bottom in the list. The level's cached level string data is cleared.</summary>
        /// <param name="name">The name of the level to open for editing.</param>
        /// <param name="revision">The revision of the level to open for editing.</param>
        public Level OpenLevelForEditing(string name, int revision) => OpenLevelForEditing(UserLevels.FindIndex(l => l.Name == name && l.Revision == revision));
        /// <summary>Opens the level at the specified index in the list for editing. The level's cached level string data is cleared.</summary>
        /// <param name="index">The index of the level to open for editing.</param>
        public Level OpenLevelForEditing(int index)
        {
            var level = index > -1 ? UserLevels[index] : null;
            level?.ClearCachedLevelStringData();
            return level;
        }

        /// <summary>Forces a level at the specified index to be loaded, if there is at least one currently running task to load a non-forced level. If there is no more space left, the level is not force loaded.</summary>
        /// <param name="index">The index of the level to force loading.</param>
        public void ForceLevelLoad(int index)
        {
            if (currentlyCancelledIndices.Count == currentlyCancelledIndices.Capacity)
                return;

            for (int i = 0; i < currentlyCancelledIndices.Capacity; i++)
                if (!currentlyCancelledIndices.Contains(i))
                {
                    currentlyCancelledIndices.Add(i);
                    tokens[i].Cancel();
                    LoadLevelString(index).ContinueWith(t => AddLevelLoadingTask(i));
                    break;
                }
        }

        /// <summary>Clones a level and adds it to the start of the list.</summary>
        /// <param name="index">The index of the level to clone.</param>
        public void CloneLevel(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "The index of the level cannot be a negative number.");
            if (index >= UserLevelCount)
                throw new ArgumentOutOfRangeException("index", "The argument that is parsed is out of range.");
            UserLevels.Insert(0, UserLevels[index].Clone());
            UpdateLevelData();
        }
        /// <summary>Clones a number of levels and adds them to the start of the level list in their original order.</summary>
        /// <param name="indices">The indices of the levels to clone.</param>
        public void CloneLevels(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            for (int i = indices.Length - 1; i >= 0; i--)
                if (indices[i] >= 0 && indices[i] < UserLevelCount)
                    UserLevels.Insert(0, UserLevels[indices[i] + indices.Length - 1 - i].Clone());
            UpdateLevelData();
        }
        /// <summary>Creates a new level with the name "Unnamed {n}" and adds it to the start of the level list.</summary>
        public void CreateLevel() => CreateLevel($"Unnamed {GetNextUnnamedNumber()}", "", DefaultLevelString);
        /// <summary>Creates a new level with a specified name and adds it to the start of the level list.</summary>
        /// <param name="name">The name of the new level to create.</param>
        public void CreateLevel(string name) => CreateLevel(name, "", DefaultLevelString);
        /// <summary>Creates a new level with a specified name and description and adds it to the start of the level list.</summary>
        /// <param name="name">The name of the new level to create.</param>
        /// <param name="desc">The description of the new level to create.</param>
        public void CreateLevel(string name, string desc) => CreateLevel(name, "", DefaultLevelString);
        /// <summary>Creates a new level with a specified name, description and level string and adds it to the start of the level list.</summary>
        /// <param name="name">The name of the new level to create.</param>
        /// <param name="desc">The description of the new level to create.</param>
        /// <param name="levelString">The level string of the new level to create.</param>
        public void CreateLevel(string name, string desc, string levelString)
        {
            UserLevels.Insert(0, new Level(name, desc, levelString, UserName, GetNextAvailableRevision(name)));
            UpdateLevelData();
        }
        /// <summary>Creates a number of new levels with the names "Unnamed {n}" and adds them to the start of the level list.</summary>
        /// <param name="numberOfLevels">The number of new levels to create.</param>
        public void CreateLevels(int numberOfLevels) => CreateLevels(numberOfLevels, new string[numberOfLevels], new string[numberOfLevels]);
        /// <summary>Creates a number of new levels with specified names and adds them to the start of the level list.</summary>
        /// <param name="numberOfLevels">The number of new levels to create.</param>
        /// <param name="name">The names of the new levels to create.</param>
        public void CreateLevels(int numberOfLevels, string[] names) => CreateLevels(numberOfLevels, names, new string[numberOfLevels]);
        /// <summary>Creates a number of new levels with specified names and descriptions and adds them to the start of the level list.</summary>
        /// <param name="numberOfLevels">The number of new levels to create.</param>
        /// <param name="name">The names of the new levels to create.</param>
        /// <param name="desc">The descriptions of the new levels to create.</param>
        public void CreateLevels(int numberOfLevels, string[] names, string[] descs)
        {
            for (int i = 0; i < numberOfLevels; i++)
                UserLevels.Insert(0, new Level(names[i], descs[i], DefaultLevelString, UserName, GetNextAvailableRevision(names[i])));
            UpdateLevelData();
        }
        /// <summary>Deletes all levels in the database.</summary>
        public void DeleteAllLevels()
        {
            decryptedLevelData = DefaultLevelData; // Set the level data to the default
            // Delete all the level info from the prorgam's memory
            UserLevels.Clear();
            LevelKeyStartIndices.Clear();
        }
        /// <summary>Deletes the levels at the specified indices in the database.</summary>
        /// <param name="indices">The indices of the levels to delete from the database.</param>
        public void DeleteLevels(int[] indices)
        {
            indices = indices.RemoveDuplicates();
            indices = indices.Sort();
            for (int i = indices.Length - 1; i >= 0; i--)
                UserLevels.RemoveAt(indices[i]);
            UpdateLevelData();
        }

        /// <summary>Exports the level at the specified index in the database to a .dat file in the specified folder.</summary>
        /// <param name="index">The index of the level to export.</param>
        /// <param name="folderPath">The path of the folder to export the level at.</param>
        public void ExportLevel(int index, string folderPath) => File.WriteAllText($@"{folderPath}\{UserLevels[index].LevelNameWithRevision}.dat", UserLevels[index].ToString());
        /// <summary>Exports the levels at the specified indices in the database to a .dat file in the specified folder.</summary>
        /// <param name="indices">The indices of the levels to export.</param>
        /// <param name="folderPath">The path of the folder to export the level at.</param>
        public void ExportLevels(int[] indices, string folderPath)
        {
            for (int i = 0; i < indices.Length; i++)
                File.WriteAllText($@"{folderPath}\{UserLevels[indices[i]].LevelNameWithRevision}.dat", UserLevels[indices[i]].ToString());
        }
        /// <summary>Imports a level into the database and adds it to the start of the level list.</summary>
        /// <param name="level">The raw level to import.</param>
        public void ImportLevel(string level)
        {
            for (int i = UserLevelCount - 1; i >= 0; i--) // Increase the level indices of all the other levels to insert the cloned level at the start
                decryptedLevelData = decryptedLevelData.Replace($"<k>k_{i}</k>", $"<k>k_{i + 1}</k>");
            level = RemoveLevelIndexKey(level); // Remove the index key of the level
            decryptedLevelData = decryptedLevelData.Insert(LevelKeyStartIndices[0] - 10, $"<k>k_0</k>{level}"); // Insert the new level
            int clonedLevelLength = level.Length + 10; // The length of the inserted level
            LevelKeyStartIndices = LevelKeyStartIndices.InsertAtStart(LevelKeyStartIndices[0]); // Add the new key start position in the array
            for (int i = 1; i < LevelKeyStartIndices.Count; i++)
                LevelKeyStartIndices[i] += clonedLevelLength; // Increase the other key indices by the length of the cloned level
            // Insert the imported level's parameters
            UserLevels.Insert(0, new Level(level));
        }
        /// <summary>Imports a level from the specified file path and adds it to the start of the level list.</summary>
        /// <param name="levelPath">The path of the level to import.</param>
        public void ImportLevelFromFile(string levelPath) => ImportLevel(File.ReadAllText(levelPath));
        /// <summary>Imports a number of levels into the database and adds them to the start of the level list.</summary>
        /// <param name="lvls">The raw levels to import.</param>
        public void ImportLevels(string[] lvls)
        {
            for (int i = 0; i < lvls.Length; i++)
                ImportLevel(lvls[i]);
            UpdateLevelData();
        }
        /// <summary>Imports a number of levels from the specified file path and adds them to the start of the level list.</summary>
        /// <param name="levelPaths">The paths of the levels to import.</param>
        public void ImportLevelsFromFiles(string[] levelPaths)
        {
            string[] levels = new string[levelPaths.Length];
            for (int i = 0; i < levelPaths.Length; i++)
                levels[i] = File.ReadAllText(levelPaths[i]);
            ImportLevels(levels);
        }

        /// <summary>Moves the selected levels down by one position.</summary>
        /// <param name="indices">The indices of the levels to move down.</param>
        public void MoveLevelsDown(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort().RemoveElementsMatchingIndicesFromEnd(UserLevelCount);
            for (int i = indices.Length - 1; i >= 0; i--)
                if (indices[i] < UserLevelCount - 1) // If the level can be moved further down
                    UserLevels.Swap(indices[i], indices[i] + 1);
            UpdateLevelData();
        }
        /// <summary>Moves the selected levels to the bottom of the level list while preserving their original order.</summary>
        /// <param name="indices">The indices of the levels to move to the bottom of the level list.</param>
        public void MoveLevelsToBottom(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            for (int i = indices.Length - 1; i >= 0; i--)
                UserLevels.MoveElement(indices[i], UserLevelCount - indices.Length + i + 1); // TODO: Figure out why +1
            UpdateLevelData();
        }
        /// <summary>Moves the selected levels to the top of the level list while preserving their original order.</summary>
        /// <param name="indices">The indices of the levels to move to the top of the level list.</param>
        public void MoveLevelsToTop(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            for (int i = 0; i < indices.Length; i++)
                UserLevels.MoveElement(indices[i], i);
            UpdateLevelData();
        }
        /// <summary>Moves the selected levels up by one position.</summary>
        /// <param name="indices">The indices of the levels to move up.</param>
        public void MoveLevelsUp(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort().RemoveElementsMatchingIndices();
            for (int i = 0; i < indices.Length; i++)
                if (indices[i] >= i) // If the level can be moved further up
                    UserLevels.Swap(indices[i], indices[i] - 1);
            UpdateLevelData();
        }
        /// <summary>Swaps the levels at the specified indices.</summary>
        /// <param name="levelIndexA">The index of the first level in the database to swap.</param>
        /// <param name="levelIndexB">The index of the second level in the database to swap.</param>
        public void SwapLevels(int levelIndexA, int levelIndexB)
        {
            UserLevels.Swap(levelIndexA, levelIndexB);
            UpdateLevelData();
        }

        /// <summary>Updates the level data in the memory and clears the stored decrypted level data string.</summary>
        public void UpdateLevelData()
        {
            decryptedLevelData = null; // Reset level data and let it be generated later
        }
        /// <summary>Writes the level data to the level data file.</summary>
        public void WriteLevelData()
        {
            UpdateLevelData();
            File.WriteAllText(GDLocalLevels, DecryptedLevelData); // Write the level data
        }
        #endregion

        #region Private Functions
        private async Task SetDecryptedGamesave(string gamesave)
        {
            decryptedGamesave = (await (decryptGamesave = TryDecryptGamesaveAsync(gamesave))).Item2;
            getFolderNames = GetFolderNames();
            getPlayerName = GetPlayerName();
            getCustomObjects = GetCustomObjects();
            getSongMetadata = GetSongMetadata();
        }
        private async Task SetDecryptedLevelData(string levelData)
        {
            decryptedLevelData = (await (decryptLevelData = TryDecryptLevelDataAsync(levelData))).Item2;
            await (getLevels = GetLevels(false));
            LoadLevelsInOrder();
        }

        private void LoadLevelsInOrder()
        {
            // Use 2 less cores to let the computer breathe a little while loading
            int utilizedCores = Math.Max(1, Cores - 2);

            levelIndicesToLoad = new List<int>(UserLevelCount);
            for (int i = UserLevelCount - 1; i >= 0; i--)
                levelIndicesToLoad.Add(i);

            nextAvailableLevelIndex = UserLevelCount;

            tokens = new CancellationTokenSource[utilizedCores];
            threadTasks = new Task[utilizedCores];
            for (int i = 0; i < utilizedCores; i++)
                AddLevelLoadingTask(i);

            currentlyCancelledIndices = new List<int>(utilizedCores);
        }

        private async Task LoadLevelString(int index) => await UserLevels[index].InitializeLoadingLevelString();

        private void AddLevelLoadingTask(int i)
        {
            var t = tokens[i] = new CancellationTokenSource();
            threadTasks[i] = Task.Run(LoadCurrentLevel, t.Token);

            async Task LoadCurrentLevel()
            {
                int index;
                int? levelIndex = null;
                lock (lockObject)
                {
                    index = --nextAvailableLevelIndex;
                    if (index < 0)
                        index = nextAvailableLevelIndex = levelIndicesToLoad.Count - 1;
                    if (index > -1)
                        levelIndex = levelIndicesToLoad[index];
                }
                if (levelIndex.HasValue)
                {
                    await LoadLevelString(levelIndex.Value);
                    levelIndicesToLoad.Remove(levelIndex.Value);
                    await LoadCurrentLevel();
                }
            }
        }

        /// <summary>Gets the next available revision for a level with a specified name.</summary>
        private int GetNextAvailableRevision(string levelName)
        {
            List<int> levelsWithSameName = new List<int>();
            for (int i = 0; i < UserLevels.Count; i++)
                if (levelName == UserLevels[i].Name)
                    levelsWithSameName.Add(i);
            List<int> revs = new List<int>(); // The revisions of the levels with the same name
            for (int i = 0; i < levelsWithSameName.Count; i++) // Add the revisions of the levels with the same name in the list
                revs.Add(UserLevels[levelsWithSameName[i]].Revision);
            return revs.GetNextAvailableNumber();
        }
        /// <summary>Gets the next available number n for a level with name "Unnamed {n}".</summary>
        private int GetNextUnnamedNumber()
        {
            string name;
            string[] split;
            List<int> nums = new List<int>();
            for (int i = 0; i < UserLevelCount; i++)
                if ((name = UserLevels[i].Name).Contains("Unnamed ") && (split = name.Split(' ')).Length == 2 && int.TryParse(split[1], out int k))
                    nums.Add(k);
            return nums.GetNextAvailableNumber();
        }

        /// <summary>Returns the level count as found in the level data by counting the occurences of the declaration keys.</summary>
        private int GetLevelCount() => decryptedLevelData.FindAll("<k>k_").Length;
        /// <summary>Gets the custom objects.</summary>
        private async Task GetCustomObjects()
        {
            CustomObjects = new CustomLevelObjectCollection();
            int startIndex = decryptedGamesave.Find("<k>customObjectDict</k><d>") + 26;
            if (startIndex < 26)
                return;

            int endIndex = decryptedGamesave.Find("</d>", startIndex, decryptedGamesave.Length);
            int currentIndex = startIndex;
            while ((currentIndex = decryptedGamesave.Find("</k><s>", currentIndex, endIndex) + 7) > 6)
                CustomObjects.Add(new CustomLevelObject(GetObjects(decryptedGamesave.Substring(currentIndex, decryptedGamesave.Find("</s>", currentIndex, decryptedGamesave.Length) - currentIndex))));
        }
        /// <summary>Gets the level declaration key indices of the level data. For internal use only.</summary>
        private void GetKeyIndices()
        {
            LevelKeyStartIndices = new List<int>();
            int count = GetLevelCount();
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                    LevelKeyStartIndices.Add(decryptedLevelData.Find($"<k>k_{i}</k><d>", LevelKeyStartIndices[i - 1], decryptedLevelData.Length) + $"<k>k_{i}</k>".Length);
                else
                    LevelKeyStartIndices.Add(decryptedLevelData.Find($"<k>k_{i}</k><d>") + $"<k>k_{i}</k>".Length);
            }
        }
        /// <summary>Gets the levels from the level data. For internal use only.</summary>
        private async Task GetLevels(bool initializeBackgroundLevelStringLoading = true)
        {
            UserLevels = new LevelCollection();
            GetKeyIndices();
            for (int i = 0; i < LevelKeyStartIndices.Count; i++)
            {
                if (i < LevelKeyStartIndices.Count - 1)
                    UserLevels.Add(new Level(decryptedLevelData.Substring(LevelKeyStartIndices[i], LevelKeyStartIndices[i + 1] - LevelKeyStartIndices[i] - $"<k>k_{i + 1}</k>".Length), initializeBackgroundLevelStringLoading));
                else
                    UserLevels.Add(new Level(decryptedLevelData.Substring(LevelKeyStartIndices[i], Math.Max(decryptedLevelData.Find("</d></d></d>", LevelKeyStartIndices[i], decryptedLevelData.Length) + 8, decryptedLevelData.Find("<d /></d></d>", LevelKeyStartIndices[i], decryptedLevelData.Length) + 9) - LevelKeyStartIndices[i]), initializeBackgroundLevelStringLoading));
            }
        }

        private async Task GetPlayerName()
        {
            int playerNameStartIndex = decryptedGamesave.FindFromEnd("<k>playerName</k><s>") + 20;
            int playerNameEndIndex = decryptedGamesave.FindFromEnd("</s><k>playerUserID</k>");
            int playerNameLength = playerNameEndIndex - playerNameStartIndex;
            UserName = decryptedGamesave.Substring(playerNameStartIndex, playerNameLength);
        }
        // TODO: Decide whether they're staying or not
        private async Task<string> GetUserID()
        {
            int userIDStartIndex = decryptedGamesave.FindFromEnd("<k>playerUserID</k><i>") + 22;
            int userIDEndIndex = decryptedGamesave.FindFromEnd("</i><k>playerFrame</k>");
            int userIDLength = userIDEndIndex - userIDStartIndex;
            return decryptedGamesave.Substring(userIDStartIndex, userIDLength);
        }
        private async Task<string> GetAccountID()
        {
            int accountIDStartIndex = decryptedGamesave.FindFromEnd("<k>GJA_003</k><i>") + 17;
            int accountIDEndIndex = decryptedGamesave.FindFromEnd("</i><k>KBM_001</k>");
            int accountIDLength = accountIDEndIndex - accountIDStartIndex;
            if (accountIDLength > 0)
                return decryptedGamesave.Substring(accountIDStartIndex, accountIDLength);
            else
                return "0";
        }
        private async Task GetFolderNames()
        {
            FolderNames = new FolderNameCollection();
            int foldersStartIndex = decryptedGamesave.FindFromEnd("<k>GLM_19</k><d>") + 16;
            if (foldersStartIndex > 15)
            {
                int foldersEndIndex = decryptedGamesave.Find("</d>", foldersStartIndex, decryptedGamesave.Length);
                int currentIndex = foldersStartIndex;
                while ((currentIndex = decryptedGamesave.Find("<k>", currentIndex, foldersEndIndex) + 3) > 2)
                {
                    int endingIndex = decryptedGamesave.Find("</k>", currentIndex, foldersEndIndex);
                    int folderIndex = int.Parse(decryptedGamesave.Substring(currentIndex, endingIndex - currentIndex));
                    int folderNameStartIndex = endingIndex + 7;
                    int folderNameEndIndex = decryptedGamesave.Find("</s>", folderNameStartIndex, foldersEndIndex);
                    FolderNames.Add(folderIndex, decryptedGamesave.Substring(folderNameStartIndex, folderNameEndIndex - folderNameStartIndex));
                }
            }
        }
        private async Task GetSongMetadata()
        {
            SongMetadataInformation = new SongMetadataCollection();
            int songMetadataStartIndex = decryptedGamesave.FindFromEnd("<k>MDLM_001</k><d>") + 18;
            if (songMetadataStartIndex > 15)
            {
                int nextDictionaryStartIndex = songMetadataStartIndex, songMetadataEndIndex = songMetadataStartIndex;
                do
                {
                    nextDictionaryStartIndex = decryptedGamesave.Find("<d>", nextDictionaryStartIndex + 3, decryptedGamesave.Length);
                    songMetadataEndIndex = decryptedGamesave.Find("</d>", songMetadataEndIndex + 4, decryptedGamesave.Length);
                }
                while (nextDictionaryStartIndex > 2 && nextDictionaryStartIndex < songMetadataEndIndex);
                int currentIndex = songMetadataStartIndex;
                while ((currentIndex = decryptedGamesave.Find("<k>", currentIndex, songMetadataEndIndex) + 3) > 2)
                {
                    int startingIndex = decryptedGamesave.Find("</k><d>", currentIndex, songMetadataEndIndex) + 7;
                    int endingIndex = currentIndex = decryptedGamesave.Find("</d>", startingIndex, songMetadataEndIndex);
                    SongMetadataInformation.Add(SongMetadata.Parse(decryptedGamesave.Substring(startingIndex, endingIndex - startingIndex)));
                }
            }
        }

        private void SetCustomObjectsInGamesave()
        {
            int startIndex = decryptedGamesave.Find("<k>customObjectDict</k><d>") + 26;
            if (startIndex < 26)
                decryptedGamesave += $"<k>customObjectDict</k><d>{CustomObjects}</d>";
            else
            {
                int endIndex = decryptedGamesave.Find("</d>", startIndex, decryptedGamesave.Length);
                decryptedGamesave = decryptedGamesave.Replace(CustomObjects.ToString(), startIndex, endIndex - startIndex);
            }
        }
        private void SetFolderNamesInGamesave()
        {
            int foldersStartIndex = decryptedGamesave.FindFromEnd("<k>GLM_19</k><d>") + 16;
            if (foldersStartIndex > 15)
            {
                int foldersEndIndex = decryptedGamesave.Find("</d>", foldersStartIndex, decryptedGamesave.Length);
                decryptedGamesave = decryptedGamesave.Replace(FolderNames.ToString(), foldersStartIndex, foldersEndIndex - foldersStartIndex);
            }
        }
        private void SetSongMetadataInGamesave()
        {
            int songMetadataStartIndex = decryptedGamesave.FindFromEnd("<k>MDLM_001</k><d>") + 18;
            if (songMetadataStartIndex > 15)
            {
                int nextDictionaryStartIndex = songMetadataStartIndex, songMetadataEndIndex = songMetadataStartIndex;
                do
                {
                    nextDictionaryStartIndex = decryptedGamesave.Find("<d>", nextDictionaryStartIndex + 3, decryptedGamesave.Length);
                    songMetadataEndIndex = decryptedGamesave.Find("</d>", songMetadataEndIndex + 4, decryptedGamesave.Length);
                }
                while (nextDictionaryStartIndex > 2 && nextDictionaryStartIndex < songMetadataEndIndex);
                int currentIndex = songMetadataStartIndex;
                decryptedGamesave = decryptedGamesave.Replace(SongMetadataInformation.ToString(), songMetadataStartIndex, songMetadataEndIndex);
            }
        }
        #endregion

        #region Static Functions
        /// <summary>Retrieves the custom song location of the song with the specified ID.</summary>
        /// <param name="ID">The ID of the song.</param>
        public static string GetCustomSongLocation(int ID) => $@"{GDLocalData}\{ID}.mp3";
        #endregion
    }
}