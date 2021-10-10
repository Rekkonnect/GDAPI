﻿using GDAPI.Application.Editors;
using GDAPI.Functions.Extensions;
using GDAPI.Objects.GeometryDash.GamesaveStrings;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static GDAPI.Functions.GeometryDash.Gamesave;
using static GDAPI.Functions.General.Parsing;
using static GDAPI.Objects.GeometryDash.LevelObjects.LevelObjectCollection;
using static System.Environment;

namespace GDAPI.Application
{
    /// <summary>Contains information about a database for the game.</summary>
    public class Database
    {
        private static readonly int Cores = ProcessorCount;

        private GamesaveString decryptedGamesave;
        private LevelDataString decryptedLevelData;

        private Task[] threadTasks;
        private CancellationTokenSource[] tokens;
        private List<int> levelIndicesToLoad;
        private int nextAvailableLevelIndex;
        private object lockObject = new object();

        private LevelCollection loadedLevels;

        private Task setDecryptedGamesave;
        private Task setDecryptedLevelData;
        private Task getFolderNames;
        private Task getPlayerName;
        private Task getCustomObjects;
        private Task getSongMetadata;
        private Task getLevels;
        private Task decryptGamesave;
        private Task decryptLevelData;

        #region Functionality
        /// <summary>The threshold of the total object count of the loaded levels for this database. If reached, the first levels that were loaded will be unloaded until the total object count is reduced below the threshold.</summary>
        public int ObjectCountThreshold { get; set; }
        #endregion

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

        #region Database Async Operation Completion Events
        /// <summary>Raised upon having finished analyzing the newly set gamesave string.</summary>
        public event Action GamesaveSetCompleted;
        /// <summary>Raised upon having finished analyzing the newly set level data string.</summary>
        public event Action LevelDataSetCompleted;
        /// <summary>Raised upon completion of the decryption of the gamesave string that was set.</summary>
        public event Action GamesaveDecrypted;
        /// <summary>Raised upon completion of the decryption of the level data string that was set.</summary>
        public event Action LevelDataDecrypted;
        /// <summary>Raised upon completion of retrieval of the folder names as specified in the newly set gamesave string.</summary>
        public event Action FolderNamesRetrieved;
        /// <summary>Raised upon completion of retrieval of the player name as specified in the newly set gamesave string.</summary>
        public event Action PlayerNameRetrieved;
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
        public string UserName { get; set; }
        /// <summary>The user levels in the database.</summary>
        public LevelCollection UserLevels { get; set; }
        /// <summary>The names of the folders.</summary>
        public FolderNameCollection FolderNames { get; set; }
        /// <summary>The stored metadata information of the songs.</summary>
        public SongMetadataCollection SongMetadataInformation { get; set; }

        /// <summary>Gets the raw decrypted gamesave string, as of the time it was last loaded or processed. It may contain outdated data.</summary>
        protected string RawDecryptedGamesaveString
        {
            get => decryptedGamesave?.RawString;
            set
            {
                if (decryptedGamesave != null)
                    decryptedGamesave.RawString = value;
            }
        }
        /// <summary>Gets the raw decrypted level data string, as of the time it was last loaded or processed. It may contain outdated data.</summary>
        protected string RawDecryptedLevelDataString
        {
            get => decryptedLevelData?.RawString;
            set
            {
                if (decryptedLevelData != null)
                    decryptedLevelData.RawString = value;
            }
        }

        // TODO: Change type to GamesaveString?
        /// <summary>The decrypted form of the game manager.</summary>
        public string DecryptedGamesave
        {
            get
            {
                if (decryptedGamesave == null)
                {
                    decryptedGamesave = (GamesaveString)File.ReadAllText(GameManagerPath);
                    decryptedGamesave.TryDecrypt(out _, true);
                    DecryptedGamesave = decryptedGamesave.RawString;
                }
                SetFolderNamesInGamesave();
                SetCustomObjectsInGamesave();
                SetSongMetadataInGamesave();
                return decryptedGamesave.RawString;
            }
            set => Task.Run(() => setDecryptedGamesave = SetDecryptedGamesave(value));
        }
        /// <summary>The decrypted form of the level data.</summary>
        public string DecryptedLevelData
        {
            get
            {
                if (decryptedLevelData.IsEmpty)
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
                    decryptedLevelData = (LevelDataString)lvlDat.ToString();
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

        #region Constructors
        /// <summary>Initializes a new instance of the <seealso cref="Database"/> class from the default database file paths.</summary>
        public Database() : this(GDGameManager, GDLocalLevels) { }
        /// <summary>Initializes a new instance of the <seealso cref="Database"/> class from custom database file paths.</summary>
        /// <param name="gameManagerPath">The file path of the game manager file of the game.</param>
        /// <param name="localLevelsPath">The file path of the local levels file of the game.</param>
        /// <param name="objectCountThreshold">The threshold of the total object count of the loaded levels for this database.</param>
        public Database(string gameManagerPath, string localLevelsPath, int objectCountThreshold = 256 * 1024)
        {
            ObjectCountThreshold = objectCountThreshold;
            Task.Run(() => SetDecryptedGamesave(File.ReadAllText(GameManagerPath = gameManagerPath)));
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

        /// <summary>Loads the level at the specified index.</summary>
        /// <param name="index">The index of the level to load.</param>
        public void LoadLevel(int index)
        {
            if (loadedLevels.Contains(UserLevels[index]))
                return;

            LoadLevelString(index);
        }

        /// <summary>Clones a level and adds it to the start of the list.</summary>
        /// <param name="index">The index of the level to clone.</param>
        public void CloneLevel(int index)
        {
            CloneLevelWithoutUpdatingDatabase(index);
            UpdateLevelData();
        }
        /// <summary>Clones a number of levels and adds them to the start of the level list in their original order.</summary>
        /// <param name="indices">The indices of the levels to clone.</param>
        public void CloneLevels(int[] indices)
        {
            indices = indices.RemoveDuplicates().Sort();
            for (int i = indices.Length - 1; i >= 0; i--)
                CloneLevelWithoutUpdatingDatabase(indices[i]);
            UpdateLevelData();
        }
        /// <summary>Creates a new level with the name "Unnamed {n}" and adds it to the start of the level list.</summary>
        public Level CreateLevel() => CreateLevel($"Unnamed {GetNextUnnamedNumber()}", "", DefaultLevelString);
        /// <summary>Creates a new level with a specified name and adds it to the start of the level list.</summary>
        /// <param name="name">The name of the new level to create.</param>
        public Level CreateLevel(string name) => CreateLevel(name, "", DefaultLevelString);
        /// <summary>Creates a new level with a specified name and description and adds it to the start of the level list.</summary>
        /// <param name="name">The name of the new level to create.</param>
        /// <param name="description">The description of the new level to create.</param>
        public Level CreateLevel(string name, string description) => CreateLevel(name, description, DefaultLevelString);
        /// <summary>Creates a new level with a specified name, description and level string and adds it to the start of the level list.</summary>
        /// <param name="name">The name of the new level to create.</param>
        /// <param name="description">The description of the new level to create.</param>
        /// <param name="levelString">The level string of the new level to create.</param>
        public Level CreateLevel(string name, string description, string levelString)
        {
            var newLevel = CreateLevelWithoutUpdatingDatabase(name, description, levelString);
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
                levels[i] = CreateLevelWithoutUpdatingDatabase(names[i], descriptions[i], DefaultLevelString);
            UpdateLevelData();
            return levels;
        }
        /// <summary>Deletes all levels in the database.</summary>
        public void DeleteAllLevels()
        {
            RawDecryptedLevelDataString = (LevelDataString)DefaultLevelData; // Set the level data to the default
            // Delete all the level info from the prorgam's memory
            UserLevels.Clear();
            LevelKeyStartIndices.Clear();
            UpdateLevelData();
        }
        /// <summary>Deletes the level at the specified index.</summary>
        /// <param name="index">The index of the level to delete.</param>
        public void DeleteLevel(int index)
        {
            DeleteLevelWithoutUpdatingDatabase(index);
            UpdateLevelData();
        }
        /// <summary>Deletes the levels at the specified indices in the database.</summary>
        /// <param name="indices">The indices of the levels to delete from the database.</param>
        public void DeleteLevels(int[] indices)
        {
            indices = indices.RemoveDuplicates();
            indices = indices.Sort();
            for (int i = indices.Length - 1; i >= 0; i--)
                DeleteLevelWithoutUpdatingDatabase(indices[i]);
            UpdateLevelData();
        }

        #region Level exporting/importing
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
        public void ImportLevel(string level, bool initializeLoading = true)
        {
            for (int i = UserLevelCount - 1; i >= 0; i--) // Increase the level indices of all the other levels to insert the cloned level at the start
                RawDecryptedLevelDataString = RawDecryptedLevelDataString.Replace($"<k>k_{i}</k>", $"<k>k_{i + 1}</k>");
            level = RemoveLevelIndexKey(level); // Remove the index key of the level
            RawDecryptedLevelDataString = RawDecryptedLevelDataString.Insert(LevelKeyStartIndices[0] - 10, $"<k>k_0</k>{level}"); // Insert the new level
            int clonedLevelLength = level.Length + 10; // The length of the inserted level
            LevelKeyStartIndices = LevelKeyStartIndices.InsertAtStart(LevelKeyStartIndices[0]); // Add the new key start position in the array
            for (int i = 1; i < LevelKeyStartIndices.Count; i++)
                LevelKeyStartIndices[i] += clonedLevelLength; // Increase the other key indices by the length of the cloned level
            // Insert the imported level and initialize its analysis
            var newLevel = new Level(level);
            if (initializeLoading)
                newLevel.InitializeLoadingLevelString();
            UserLevels.Insert(0, newLevel);
        }
        /// <summary>Imports a level from the specified file path and adds it to the start of the level list.</summary>
        /// <param name="levelPath">The path of the level to import.</param>
        public void ImportLevelFromFile(string levelPath, bool initializeLoading = true) => ImportLevel(File.ReadAllText(levelPath), initializeLoading);
        /// <summary>Imports a number of levels into the database and adds them to the start of the level list.</summary>
        /// <param name="levels">The raw levels to import.</param>
        public void ImportLevels(string[] levels, bool initializeLoading = true)
        {
            for (int i = 0; i < levels.Length; i++)
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
        public void ImportCustomObject(string customObject) => CustomObjects.Insert(0, new CustomLevelObject(ParseObjects(customObject)));
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
                customObjects[i] = File.ReadAllText(customObjectPaths[i]);
            ImportCustomObjects(customObjects);
        }
        #endregion

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
            RawDecryptedLevelDataString = null; // Reset level data and let it be generated later
        }
        /// <summary>Writes the level data to the level data file.</summary>
        public void WriteLevelData()
        {
            UpdateLevelData();
            File.WriteAllText(GDLocalLevels, DecryptedLevelData); // Write the level data
        }
        #endregion

        #region Private Functions
        private Level CloneLevelWithoutUpdatingDatabase(int index)
        {
            var cloned = UserLevels[index].Clone();
            if (loadedLevels.Contains(UserLevels[index]))
                loadedLevels.Add(cloned);
            UserLevels.Insert(0, cloned);
            return cloned;
        }
        private Level CreateLevelWithoutUpdatingDatabase(string name, string description, string levelString)
        {
            var newLevel = new Level(name, description, levelString, UserName, GetNextAvailableRevision(name));
            UserLevels.Insert(0, newLevel);
            loadedLevels.Add(newLevel);
            return newLevel;
        }
        private void DeleteLevelWithoutUpdatingDatabase(int index)
        {
            loadedLevels.Remove(UserLevels[index]);
            UserLevels.RemoveAt(index);
        }

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

            LevelDataSetCompleted?.Invoke();
        }

        private async Task SetDecryptedGamesaveField(string gamesave) => await (decryptedGamesave = (GamesaveString)gamesave).TryDecryptAsync(true);
        private async Task SetDecryptedLevelDataField(string levelData) => await (decryptedLevelData = (LevelDataString)levelData).TryDecryptAsync(true);

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
        }

        private async Task LoadLevelString(int index)
        {
            var level = UserLevels[index];
            loadedLevels.Add(level);
            await UserLevels[index].InitializeLoadingLevelString();
            while (loadedLevels.Count > 1 && loadedLevels.TotalLevelObjects > ObjectCountThreshold)
            {
                loadedLevels[0].UnloadLevelString();
                loadedLevels.RemoveAt(0);
            }
        }

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
        private int GetLevelCount() => RawDecryptedLevelDataString.FindAll("<k>k_").Length;
        /// <summary>Gets the custom objects.</summary>
        private async Task GetCustomObjects()
        {
            CustomObjects = new CustomLevelObjectCollection();
            int startIndex = RawDecryptedGamesaveString.Find("<k>customObjectDict</k><d>") + 26;
            if (startIndex < 26)
                return;

            int endIndex = RawDecryptedGamesaveString.Find("</d>", startIndex, RawDecryptedGamesaveString.Length);
            int currentIndex = startIndex;
            while ((currentIndex = RawDecryptedGamesaveString.Find("</k><s>", currentIndex, endIndex) + 7) > 6)
                CustomObjects.Add(new CustomLevelObject(ParseObjects(RawDecryptedGamesaveString.Substring(currentIndex, RawDecryptedGamesaveString.Find("</s>", currentIndex, RawDecryptedGamesaveString.Length) - currentIndex))));
        }
        /// <summary>Gets the level declaration key indices of the level data. For internal use only.</summary>
        private void GetKeyIndices()
        {
            LevelKeyStartIndices = new List<int>();
            int count = GetLevelCount();
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                    LevelKeyStartIndices.Add(RawDecryptedLevelDataString.Find($"<k>k_{i}</k><d>", LevelKeyStartIndices[i - 1], RawDecryptedLevelDataString.Length) + $"<k>k_{i}</k>".Length);
                else
                    LevelKeyStartIndices.Add(RawDecryptedLevelDataString.Find($"<k>k_{i}</k><d>") + $"<k>k_{i}</k>".Length);
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
                    UserLevels.Add(new Level(RawDecryptedLevelDataString.Substring(LevelKeyStartIndices[i], LevelKeyStartIndices[i + 1] - LevelKeyStartIndices[i] - $"<k>k_{i + 1}</k>".Length), initializeBackgroundLevelStringLoading));
                else
                    UserLevels.Add(new Level(RawDecryptedLevelDataString.Substring(LevelKeyStartIndices[i], Math.Max(RawDecryptedLevelDataString.Find("</d></d></d>", LevelKeyStartIndices[i], RawDecryptedLevelDataString.Length) + 8, RawDecryptedLevelDataString.Find("<d /></d></d>", LevelKeyStartIndices[i], RawDecryptedLevelDataString.Length) + 9) - LevelKeyStartIndices[i]), initializeBackgroundLevelStringLoading));
            }
        }

        private async Task GetPlayerName()
        {
            int playerNameStartIndex = RawDecryptedGamesaveString.FindFromEnd("<k>playerName</k><s>") + 20;
            int playerNameEndIndex = RawDecryptedGamesaveString.FindFromEnd("</s><k>playerUserID</k>");
            int playerNameLength = playerNameEndIndex - playerNameStartIndex;
            UserName = RawDecryptedGamesaveString.Substring(playerNameStartIndex, playerNameLength);
        }
        // TODO: Decide whether they're staying or not
        private async Task<string> GetUserID()
        {
            int userIDStartIndex = RawDecryptedGamesaveString.FindFromEnd("<k>playerUserID</k><i>") + 22;
            int userIDEndIndex = RawDecryptedGamesaveString.FindFromEnd("</i><k>playerFrame</k>");
            int userIDLength = userIDEndIndex - userIDStartIndex;
            return RawDecryptedGamesaveString.Substring(userIDStartIndex, userIDLength);
        }
        private async Task<string> GetAccountID()
        {
            int accountIDStartIndex = RawDecryptedGamesaveString.FindFromEnd("<k>GJA_003</k><i>") + 17;
            int accountIDEndIndex = RawDecryptedGamesaveString.FindFromEnd("</i><k>KBM_001</k>");
            int accountIDLength = accountIDEndIndex - accountIDStartIndex;
            if (accountIDLength > 0)
                return RawDecryptedGamesaveString.Substring(accountIDStartIndex, accountIDLength);
            else
                return "0";
        }
        private async Task GetFolderNames()
        {
            FolderNames = new FolderNameCollection();
            int foldersStartIndex = RawDecryptedGamesaveString.FindFromEnd("<k>GLM_19</k><d>") + 16;
            if (foldersStartIndex > 15)
            {
                int foldersEndIndex = RawDecryptedGamesaveString.Find("</d>", foldersStartIndex, RawDecryptedGamesaveString.Length);
                int currentIndex = foldersStartIndex;
                while ((currentIndex = RawDecryptedGamesaveString.Find("<k>", currentIndex, foldersEndIndex) + 3) > 2)
                {
                    int endingIndex = RawDecryptedGamesaveString.Find("</k>", currentIndex, foldersEndIndex);
                    int folderIndex = ParseInt32(RawDecryptedGamesaveString.Substring(currentIndex, endingIndex - currentIndex));
                    int folderNameStartIndex = endingIndex + 7;
                    int folderNameEndIndex = RawDecryptedGamesaveString.Find("</s>", folderNameStartIndex, foldersEndIndex);
                    FolderNames.Add(folderIndex, RawDecryptedGamesaveString.Substring(folderNameStartIndex, folderNameEndIndex - folderNameStartIndex));
                }
            }
        }
        private async Task GetSongMetadata()
        {
            SongMetadataInformation = new SongMetadataCollection();
            int songMetadataStartIndex = RawDecryptedGamesaveString.FindFromEnd("<k>MDLM_001</k><d>") + 18;
            if (songMetadataStartIndex > 15)
            {
                int nextDictionaryStartIndex = songMetadataStartIndex, songMetadataEndIndex = songMetadataStartIndex;
                do
                {
                    nextDictionaryStartIndex = RawDecryptedGamesaveString.Find("<d>", nextDictionaryStartIndex + 3, RawDecryptedGamesaveString.Length);
                    songMetadataEndIndex = RawDecryptedGamesaveString.Find("</d>", songMetadataEndIndex + 4, RawDecryptedGamesaveString.Length);
                }
                while (nextDictionaryStartIndex > 2 && nextDictionaryStartIndex < songMetadataEndIndex);
                int currentIndex = songMetadataStartIndex;
                while ((currentIndex = RawDecryptedGamesaveString.Find("<k>", currentIndex, songMetadataEndIndex) + 3) > 2)
                {
                    int startingIndex = RawDecryptedGamesaveString.Find("</k><d>", currentIndex, songMetadataEndIndex) + 7;
                    int endingIndex = currentIndex = RawDecryptedGamesaveString.Find("</d>", startingIndex, songMetadataEndIndex);
                    SongMetadataInformation.Add(SongMetadata.Parse(RawDecryptedGamesaveString.Substring(startingIndex, endingIndex - startingIndex)));
                }
            }
        }

        private void SetCustomObjectsInGamesave()
        {
            int startIndex = RawDecryptedGamesaveString.Find("<k>customObjectDict</k><d>") + 26;
            if (startIndex < 26)
                RawDecryptedGamesaveString += $"<k>customObjectDict</k><d>{CustomObjects}</d>";
            else
            {
                int endIndex = RawDecryptedGamesaveString.Find("</d>", startIndex, RawDecryptedGamesaveString.Length);
                RawDecryptedGamesaveString = RawDecryptedGamesaveString.Replace(CustomObjects.ToString(), startIndex, endIndex - startIndex);
            }
        }
        private void SetFolderNamesInGamesave()
        {
            int foldersStartIndex = RawDecryptedGamesaveString.FindFromEnd("<k>GLM_19</k><d>") + 16;
            if (foldersStartIndex > 15)
            {
                int foldersEndIndex = RawDecryptedGamesaveString.Find("</d>", foldersStartIndex, RawDecryptedGamesaveString.Length);
                RawDecryptedGamesaveString = RawDecryptedGamesaveString.Replace(FolderNames.ToString(), foldersStartIndex, foldersEndIndex - foldersStartIndex);
            }
        }
        private void SetSongMetadataInGamesave()
        {
            int songMetadataStartIndex = RawDecryptedGamesaveString.FindFromEnd("<k>MDLM_001</k><d>") + 18;
            if (songMetadataStartIndex > 15)
            {
                int nextDictionaryStartIndex = songMetadataStartIndex, songMetadataEndIndex = songMetadataStartIndex;
                do
                {
                    nextDictionaryStartIndex = RawDecryptedGamesaveString.Find("<d>", nextDictionaryStartIndex + 3, RawDecryptedGamesaveString.Length);
                    songMetadataEndIndex = RawDecryptedGamesaveString.Find("</d>", songMetadataEndIndex + 4, RawDecryptedGamesaveString.Length);
                }
                while (nextDictionaryStartIndex > 2 && nextDictionaryStartIndex < songMetadataEndIndex);
                int currentIndex = songMetadataStartIndex;
                RawDecryptedGamesaveString = RawDecryptedGamesaveString.Replace(SongMetadataInformation.ToString(), songMetadataStartIndex, songMetadataEndIndex);
            }
        }
        #endregion

        #region Static Functions
        /// <summary>Retrieves the custom song location of the song with the specified ID.</summary>
        /// <param name="ID">The ID of the song.</param>
        public static string GetCustomSongLocation(int ID) => $@"{GDLocalData}{Path.DirectorySeparatorChar}{ID}.mp3";

        private static async Task PerformTaskWithInvocableEvent(Task task, Action invocableEvent)
        {
            task.ContinueWith(_ => invocableEvent?.Invoke());
            await task;
        }
        #endregion
    }
}
