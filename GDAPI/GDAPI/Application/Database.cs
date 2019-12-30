using GDAPI.Application.Editors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GDAPI.Functions.Extensions;
using GDAPI.Objects.GeometryDash;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects;
using static GDAPI.Functions.GeometryDash.Gamesave;
using static System.Environment;

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
        private Task decryptGamesave;
        private Task decryptLevelData;

        #region Database Status
        public TaskStatus SetDecryptedGamesaveStatus => setDecryptedGamesave?.Status ?? (TaskStatus)(-1);
        public TaskStatus SetDecryptedLevelDataStatus => setDecryptedLevelData?.Status ?? (TaskStatus)(-1);
        public TaskStatus GetFolderNamesStatus => getFolderNames?.Status ?? (TaskStatus)(-1);
        public TaskStatus GetPlayerNameStatus => getPlayerName?.Status ?? (TaskStatus)(-1);
        public TaskStatus GetCustomObjectsStatus => getCustomObjects?.Status ?? (TaskStatus)(-1);
        public TaskStatus GetSongMetadataStatus => getSongMetadata?.Status ?? (TaskStatus)(-1);
        public TaskStatus GetLevelsStatus => getLevels?.Status ?? (TaskStatus)(-1);
        public Tas
        /// <summary>Raised upon completion of retrieval of the custom objects as specified in the newly set gamesave string.</summary>
        public event Action CustomObjectsRetrieved;
        /// <summary>Raised upon completion of retrieval of the song metadata as specified in the newly set gamesave string.</summary>
        public event Action SongMetadataRetrieved;
        /// <summary>Raised upon completion of retrieval of the levels as specified in the newly set level data string.</summary>
        public event Action LevelsRetrieved;
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
xt(GameManagerPath), out decryptedGamesave);
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
        /// e.ReadAllText(GameManagerPath = gameManagerPath)));
            Task.Run(() => SetDecryptedLevelData(File.ReadAllText(LocalLevelsPath = localLevelsPath)));
        }
        #endregion

        // TODO: Order these appropriately
        #region Functions
        /// <summary>Opens the first level that matches the specified name for editing, ordered from top to bottom in the list. The level's cached level string data is cleared.</summary>
        /// <param name="name">The name of the level to open for editing.</param>
        public Editor OpenLevelForEditing(string name) => OpenLevelForEditing(UserLevels.FindIndex(l => l.Name == name));
        /// <summary>Opens the first level that matches the specified name and revision for editing, ordered from top to bottom in the list. The level's cached level string data is cleared.</summary>
        /// <param name="name">The name of the level to open for editing.</param>
        /// <param name="revision">The revision of the level to open for editing.</param>
        public Editor OpenLevelForEditing(string name, int revision) => OpenLevelForEditing(UserLevels.FindIndex(l => l.Name == name && l.Revision == revision));
        /// <summary>Opens the level at the specified index in the list for editing. The level's cached level string data is cleared.</summary>
        /// <param name="index">The index of the level to open for editing.</param>
        public Editor OpenLevelForEditing(int index)
        {
            if (index < 0)
                return null;
            var level = UserLevels[index];
            level.ClearCachedLevelStringData();
            return new Editor(level);
        }

        /// <summary>Forces a level at the specified index to be loaded, if there is at least one currently running task to load a non-forced level. If there is no more space left, the level is not force loaded.</summary>
        /// <param name="index">The index of the level to force loading.</param>
        public void ForceLevelLoad(int index)
        {
            if (currentlyCancelledIndices.Count == currentlyCancelledIndices.Capacity)
                return;

            for (int i = 0; i < currentlyCancelledIndices.Capacity; i++)

                throw new ArgumentOutOfRangeException("index", "The argument that is parsed is out of range.");
            UserLevels.Insert(0, UserLevels[index].Clone());
            UpdateLevelData();
        }
        /// <summary>Clones a number of levels and adds them to the start of the level list in their original order.</summary>
        /// <param name="indices">The indices of the levels to clone.</param>
        public void CloneLevels(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            fo create.</param>
        /// <param name="description">The description of the new level to create.</param>
        public Level CreateLevel(string name, string description) => CreateLevel(name, description, DefaultLevelString);
        /// <summary>Creates a new level with a specified name, description and level string and adds it to the start of the level list.</summary>
        /// <param name="name">The name of the new level to create.</param>
        /// <param name="description">The description of the new level to create.</param>
        /// <param name="levelString">The level string of the new level to create.</param>
        public Level CreateLevel(string name, string description, string levelString)
        {
            var newLevel = new Level(name, description, levelString, UserName, GetNextAvailableRevision(name));
            UserLevels.Insert(0, newLevel);
            UpdateLevelData();
            return newLevel;
        }
        /// <summary>Creates a number of new levels with the names "Unnamed {n}" and adds them to the start of the level list.</summary>
        /// <param name="numberOfLevels">The number of new levels to create.</param>
        public Level[] CreateLevels(int numberOfLevels) => CreateLevels(numberOfLevels, new string[numberOfLevels], new string[numberOfLevels]);
        /// <summary>Creates a number of new levels with specified names and adds them to the start of the level list.</summary>
        /// <param name="numberOfLevels">The number of new levels to create.</param>
        /// <param name="name">The names of the new levels to create.</param>
        public Level[] CreateLevels(int numberOfLevels, string[] names) => CreateLevels(numberOfLevels, names, new string[numberOfLevels]);
        /// <summary>Creates a number of new levels with specified names and descriptions and adds them to the start of the level list.</summary>
        /// <param name="numberOfLevels">The number of new levels to create.</param>
        /// <param name="names">The names of the new levels to create.</param>
        /// <param name="descriptions">The descriptions of the new levels to create.</param>
        public Level[] CreateLevels(int numberOfLevels, string[] names, string[] descriptions)
        {
            var levels = new Level[numberOfLevels];
            for (int i = 0; i < numberOfLevels; i++)
                UserLevels.Insert(0, levels[i] = new Level(names[i], descriptions[i], DefaultLevelString, UserName, GetNextAvailableRevision(names[i])));
            UpdateLevelData();
            return levels;
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

        #region Level exporting/importing
        /// <summary>Exports the level at the specified index in the database to a .dat file in the specified folder.</summary>
        /// <param name="index">The index of the level to export.</param>
        /// <param name="folderPath">The path of the folder to export the level at.</param>

                ImportLevel(levels[i], initializeLoading);
            UpdateLevelData();
        }
        /// <summary>Imports a number of levels from the specified file path and adds them to the start of the level list.</summary>
        /// <param name="levelPaths">The paths of the levels to import.</param>
        public void ImportLevelsFromFiles(string[] levelPaths, bool initializeLoading = true)
        {
            string[] levels = new string[levelPaths.Length];
            for (int i = 0; i < levelPaths.Length; i++)
                levels[i] = File.ReadAllText(levelPaths[i]);
            ImportLevels(levels, initializeLoading);
        }
        #endregion

        #region Custom Object exporting/importing
        /// <summary>Exports the custom object at the specified index in the database to a .dat file in the specified folder.</summary>
        /// <param name="index">The index of the custom object to export.</param>
        /// <param name="folderPath">The path of the folder to export the custom object at.</param>
        public void ExportCustomObject(int index, string folderPath) => File.WriteAllText($@"{folderPath}\Custom Object {index}.dat", CustomObjects[index].ToString());
        /// <summary>Exports the custom objects at the specified indices in the database to a .dat file in the specified folder.</summary>
        /// <param name="indices">The indices of the custom objects to export.</param>
        /// <param name="folderPath">The path of the folder to export the custom objects at.</param>
        public void ExportCustomObjects(int[] indices, string folderPath)
        {
            for (int i = 0; i < indices.Length; i++)
                File.WriteAllText($@"{folderPath}\Custom Object {i}.dat", CustomObjects[indices[i]].ToString());
        }
        /// <summary>Imports a custom object into the database and adds it to the start of the custom object list.</summary>
        /// <param name="customObject">The raw custom object to import.</param>
        public void ImportCustomObject(string customObject) => CustomObjects.Insert(0, new CustomLevelObject(GetObjects(customObject)));
        /// <summary>Imports a custom object from the specified file path and adds it to the start of the custom object list.</summary>
        /// <param name="customObjectPath">The path of the custom object to import.</param>
        public void ImportCustomObjectFromFile(string customObjectPath) => ImportCustomObject(File.ReadAllText(customObjectPath));
        /// <summary>Imports a number of custom objects into the database and adds them to the start of the custom object list.</summary>
        /// <param name="customObjects">The raw custom objects to import.</param>
        public void ImportCustomObjects(string[] customObjects)
        {
            for (int i = 0; i < customObjects.Length; i++)
                ImportCustomObject(customObjects[i]);
        }
        /// <summary>Imports a number of custom objects from the specified file path and adds them to the start of the custom object list.</summary>
        /// <param name="customObjectPaths">The paths of the custom objects to import.</param>
        public void ImportCustomObjectsFromFiles(string[] customObjectPaths)
        {
            string[] customObjects = new string[customObjectPaths.Length];
            for (int i = 0; i < customObjectPaths.Length; i++)

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
            await PerformTaskWithInvocableEvent(decryptGamesave = SetDecryptedGamesaveField(gamesave), GamesaveDecrypted);

            await PerformTaskWithInvocableEvent(getFolderNames = GetFolderNames(), FolderNamesRetrieved);
            await PerformTaskWithInvocableEvent(getPlayerName = GetPlayerName(), PlayerNameRetrieved);
            await PerformTaskWithInvocableEvent(getCustomObjects = GetCustomObjects(), CustomObjectsRetrieved);
            await PerformTaskWithInvocableEvent(getSongMetadata = GetSongMetadata(), SongMetadataRetrieved);

            GamesaveSetCompleted?.Invoke();
        }
        private async Task SetDecryptedLevelData(string levelData)
        {
            await PerformTaskWithInvocableEvent(decryptLevelData = SetDecryptedLevelDataField(levelData), LevelDataDecrypted);
            await PerformTaskWithInvocableEvent(getLevels = GetLevels(false), LevelsRetrieved);
            LoadLevelsInOrder();

            LevelDataSetCompleted?.Invoke();
        }

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

            int endInde
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

        private static async Task PerformTaskWithInvocableEvent(Task task, Action invocableEvent)
        {
            task.ContinueWith(_ => invocableEvent?.Invoke());
            await task;
        }
        #endregion he alex guess what, I got a 2.2 copy
    }
}
