using GDAPI.Enumerations;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Functions.Extensions;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;
using GDAPI.Objects.IDMigration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using static GDAPI.Objects.General.SourceTargetRange;

namespace GDAPI.Application.Editors
{
    /// <summary>Provides tools to migrate a level's IDs, for example Group IDs, Color IDs, etc.</summary>
    public class IDMigrationEditor : BasePluggableEditor
    {
        /// <summary>Initializes a new instance of the <seealso cref="IDMigrationEditor"/> class.</summary>
        /// <param name="master">The <seealso cref="Bindable{T}"/> containing the master <seealso cref="Editor"/> that this editor is bound to.</param>
        public IDMigrationEditor(Bindable<Editor> master)
            : base(master) { }

        #region ID Migration
        /// <summary>The ID migration info of this editor instance that will be used when performing ID migration operations.</summary>
        public readonly IDMigrationInfo IDMigrationInfo = new IDMigrationInfo();

        public event Action IDMigrationOperationInitialized;
        public event ProgressReporter IDMigrationProgressReported;
        public event Action IDMigrationOperationCompleted;

        /// <summary>The steps of the Group ID migration mode.</summary>
        public List<SourceTargetRange> GroupSteps => GetIDMigrationSteps(LevelObjectIDType.Group);
        /// <summary>The steps of the Color ID migration mode.</summary>
        public List<SourceTargetRange> ColorSteps => GetIDMigrationSteps(LevelObjectIDType.Color);
        /// <summary>The steps of the Item ID migration mode.</summary>
        public List<SourceTargetRange> ItemSteps => GetIDMigrationSteps(LevelObjectIDType.Item);
        /// <summary>The steps of the Block ID migration mode.</summary>
        public List<SourceTargetRange> BlockSteps => GetIDMigrationSteps(LevelObjectIDType.Block);

        /// <summary>Gets or sets the currently selected ID migration mode.</summary>
        public LevelObjectIDType SelectedIDMigrationMode
        {
            get => IDMigrationInfo.SelectedIDMigrationMode;
            set => IDMigrationInfo.SelectedIDMigrationMode = value;
        }

        /// <summary>Gets the currently selected mode's <seealso cref="IDMigrationModeInfo"/> object.</summary>
        public IDMigrationModeInfo CurrentlySelectedIDMigrationModeInfo => IDMigrationInfo.CurrentlySelectedIDMigrationModeInfo;
        /// <summary>Gets or sets the currently selected ID migration steps.</summary>
        public List<SourceTargetRange> CurrentlySelectedIDMigrationSteps
        {
            get => IDMigrationInfo.CurrentlySelectedIDMigrationSteps;
            set => IDMigrationInfo.CurrentlySelectedIDMigrationSteps = value;
        }

        // TODO: Migrate this into a separate class and rework the way delegate propagation occurs for consistency
        /// <summary>Performs the ID migration for the currently selected mode.</summary>
        public void PerformMigration() => GetIDMigrationDelegate()();
        /// <summary>Performs the ID migration for the currently selected mode using custom steps.</summary>
        /// <param name="steps">The custom steps to use in the ID migration operation.</param>
        public void PerformMigration(List<SourceTargetRange> steps) => GetIDMigrationCustomStepsDelegate()(steps);
        /// <summary>Performs the ID migration for a specified mode using the respective steps of the mode.</summary>
        /// <param name="mode">The mode of the ID migration to perform.</param>
        public void PerformMigration(LevelObjectIDType mode) => PerformMigration(mode, GetIDMigrationSteps(mode));
        /// <summary>Performs the ID migration for a specified mode using custom steps.</summary>
        /// <param name="mode">The mode of the ID migration to perform.</param>
        /// <param name="steps">The custom steps to use in the ID migration operation.</param>
        public void PerformMigration(LevelObjectIDType mode, List<SourceTargetRange> steps) => GetIDMigrationCustomStepsDelegate(mode)(steps);

        /// <summary>Gets the <seealso cref="IDMigrationModeInfo"/> object for a specified mode.</summary>
        /// <param name="mode">The mode to get the <seealso cref="IDMigrationModeInfo"/> object for.</param>
        public IDMigrationModeInfo GetIDMigrationModeInfo(LevelObjectIDType mode) => IDMigrationInfo[mode];
        /// <summary>Gets the ID migration steps for a specified mode.</summary>
        /// <param name="mode">The mode to get the steps for.</param>
        public List<SourceTargetRange> GetIDMigrationSteps(LevelObjectIDType mode) => IDMigrationInfo[mode].Steps;

        /// <summary>Adds a new ID migration step to the currently selected mode's ranges.</summary>
        /// <param name="range">The ID migration step to add to the currently selected ID migration.</param>
        public void AddIDMigrationStep(SourceTargetRange range) => IDMigrationInfo.AddIDMigrationStep(range);
        /// <summary>Adds a range of new ID migration steps to the currently selected mode's ranges.</summary>
        /// <param name="ranges">The ID migration steps to add to the currently selected ID migration.</param>
        public void AddIDMigrationSteps(List<SourceTargetRange> ranges) => IDMigrationInfo.AddIDMigrationSteps(ranges);
        /// <summary>Removes a new ID migration step from the currently selected mode's ranges.</summary>
        /// <param name="range">The ID migration step to remove from the currently selected ID migration.</param>
        public void RemoveIDMigrationStep(SourceTargetRange range) => IDMigrationInfo.RemoveIDMigrationStep(range);
        /// <summary>Removes a range of new ID migration steps from the currently selected mode's ranges.</summary>
        /// <param name="ranges">The ID migration steps to remove from the currently selected ID migration.</param>
        public void RemoveIDMigrationSteps(List<SourceTargetRange> ranges) => IDMigrationInfo.RemoveIDMigrationSteps(ranges);
        // TODO: Add cloning method

        /// <summary>Saves the current ID migration steps to the associated file, if not <see langword="null"/> and returns whether the steps were saved.</summary>
        public bool SaveCurrentIDMigrationSteps()
        {
            bool hasAssociatedFile = CurrentlySelectedIDMigrationModeInfo.FileName != null;
            if (hasAssociatedFile)
                SaveCurrentIDMigrationSteps(CurrentlySelectedIDMigrationModeInfo.FileName, false);
            return hasAssociatedFile;
        }
        /// <summary>Saves the current ID migration steps to a specified file.</summary>
        /// <param name="fileName">The name of the file to save the current ID migration steps.</param>
        /// <param name="associateFile">Determines whether the file name will be associated with the currently selected ID migration steps.</param>
        public void SaveCurrentIDMigrationSteps(string fileName, bool associateFile = true)
        {
            SaveIDMigrationSteps(fileName, CurrentlySelectedIDMigrationSteps);
            if (associateFile)
                CurrentlySelectedIDMigrationModeInfo.FileName = fileName;
        }
        /// <summary>Saves the specified ID migration steps to a specified file.</summary>
        /// <param name="fileName">The name of the file to save the specified ID migration steps.</param>
        /// <param name="steps">The steps to save.</param>
        public void SaveIDMigrationSteps(string fileName, List<SourceTargetRange> steps) => File.WriteAllLines(fileName, ConvertRangesToStringArray(steps));
        /// <summary>Loads the ID migration steps from a specified file and replaces the currently selected ID migration steps with the loaded ones.</summary>
        /// <param name="fileName">The name of the file to load the ID migration steps from.</param>
        public void LoadIDMigrationSteps(string fileName)
        {
            CurrentlySelectedIDMigrationSteps = LoadRangesFromStringArray(File.ReadAllLines(fileName));
            IDMigrationInfo.CurrentlySelectedIDMigrationModeInfo.FileName = fileName;
        }

        // This was copied from a private feature code of EffectSome
        // TODO: Add undo/redo action after reworking the undo/redo system to use classes instead of functions
        /// <summary>Performs a group ID migration with the currently loaded ID migration steps for the group mode.</summary>
        public void PerformGroupIDMigration() => PerformGroupIDMigration(GroupSteps);
        /// <summary>Performs a group ID migration given the provided ID migration steps.</summary>
        /// <param name="ranges">The ID migration steps to execute to perform the group ID migration.</param>
        public void PerformGroupIDMigration(List<SourceTargetRange> ranges) => PerformIDMigration(ranges, AdjustGroups);
        /// <summary>Performs a color ID migration with the currently loaded ID migration steps for the color mode.</summary>
        public void PerformColorIDMigration() => PerformColorIDMigration(ColorSteps);
        /// <summary>Performs a color ID migration given the provided ID migration steps.</summary>
        /// <param name="ranges">The ID migration steps to execute to perform the color ID migration.</param>
        public void PerformColorIDMigration(List<SourceTargetRange> ranges) => PerformIDMigration(ranges, AdjustColors, AdjustColorChannels);
        /// <summary>Performs an item ID migration with the currently loaded ID migration steps for the item mode.</summary>
        public void PerformItemIDMigration() => PerformItemIDMigration(ItemSteps);
        /// <summary>Performs an item ID migration given the provided ID migration steps.</summary>
        /// <param name="ranges">The ID migration steps to execute to perform the item ID migration.</param>
        public void PerformItemIDMigration(List<SourceTargetRange> ranges) => PerformIDMigration(ranges, AdjustItems);
        /// <summary>Performs a block ID migration with the currently loaded ID migration steps for the block mode.</summary>
        public void PerformBlockIDMigration() => PerformBlockIDMigration(BlockSteps);
        /// <summary>Performs a block ID migration given the provided ID migration steps.</summary>
        /// <param name="ranges">The ID migration steps to execute to perform the block ID migration.</param>
        public void PerformBlockIDMigration(List<SourceTargetRange> ranges) => PerformIDMigration(ranges, AdjustBlocks);

        // The argument ordering inconsistency seems a bit annoying, but it's all due to the enforced way default parameters work
        // Between making type default to Group, or reordering every other function's ordering to comply with this one, I choose none of the above
        /// <summary>Performs a compact ID reallocation for the given level object ID type. This ensures that the next unused ID will be equal to the number of total used IDs + 1.</summary>
        /// <param name="type">The type of the level objects' used IDs to compactly reallocate.</param>
        /// <param name="ignoredRanges">The ranges of IDs to ignore. These IDs will remain intact, and will not be reallocated into. If <see langword="null"/>, all IDs will be reallocated.</param>
        public void CompactlyReallocateIDs(LevelObjectIDType type, List<Range>? ignoredRanges = null) => CompactlyReallocateIDs(type, ignoredRanges, GetIDAdjustmentFunction(type), GetLevelIDAdjustmentFunction(type));
        /// <summary>Performs a compact Group ID reallocation, adjusting all Group IDs. This ensures that the next unused Group ID will be equal to the number of total used Group IDs + 1.</summary>
        /// <param name="ignoredRanges">The ranges of Group IDs to ignore. These Group IDs will remain intact, and will not be reallocated into. If <see langword="null"/>, all Group IDs will be reallocated.</param>
        public void CompactlyReallocateGroupIDs(List<Range>? ignoredRanges = null) => CompactlyReallocateIDs(LevelObjectIDType.Group, ignoredRanges);
        /// <summary>Performs a compact Color ID reallocation, adjusting all Color IDs. This ensures that the next unused Color ID will be equal to the number of total used Color IDs + 1.</summary>
        /// <param name="ignoredRanges">The ranges of Color IDs to ignore. These Color IDs will remain intact, and will not be reallocated into. If <see langword="null"/>, all Color IDs will be reallocated.</param>
        public void CompactlyReallocateColorIDs(List<Range>? ignoredRanges = null) => CompactlyReallocateIDs(LevelObjectIDType.Color, ignoredRanges);
        /// <summary>Performs a compact Item ID reallocation, adjusting all Item IDs. This ensures that the next unused Item ID will be equal to the number of total used Item IDs + 1.</summary>
        /// <param name="ignoredRanges">The ranges of Item IDs to ignore. These Item IDs will remain intact, and will not be reallocated into. If <see langword="null"/>, all Item IDs will be reallocated.</param>
        public void CompactlyReallocateItemIDs(List<Range>? ignoredRanges = null) => CompactlyReallocateIDs(LevelObjectIDType.Item, ignoredRanges);
        /// <summary>Performs a compact Block ID reallocation, adjusting all Block IDs. This ensures that the next unused Block ID will be equal to the number of total used Block IDs + 1.</summary>
        /// <param name="ignoredRanges">The ranges of Block IDs to ignore. These Block IDs will remain intact, and will not be reallocated into. If <see langword="null"/>, all Block IDs will be reallocated.</param>
        public void CompactlyReallocateBlockIDs(List<Range>? ignoredRanges = null) => CompactlyReallocateIDs(LevelObjectIDType.Block, ignoredRanges);

        private void PerformIDMigration(List<SourceTargetRange> ranges, IDAdjustmentFunction adjustmentFunction, LevelIDAdjustmentFunction? levelAdjustmentFunction = null)
        {
            IDMigrationOperationInitialized?.Invoke();
            for (int i = 0; i < ranges.Count; i++)
            {
                var range = ranges[i];
                if (range.Difference > 0)
                {
                    for (int j = 0; j < Level.LevelObjects.Count; j++)
                        adjustmentFunction(Level.LevelObjects[j], range);
                    levelAdjustmentFunction?.Invoke(range);
                }
                IDMigrationProgressReported?.Invoke(i + 1, ranges.Count);
            }
            IDMigrationOperationCompleted?.Invoke();
        }
        private static void PerformIDMigration(LevelObjectCollection objects, int oldID, int newID, IDAdjustmentFunction adjustmentFunction, LevelIDAdjustmentFunction? levelAdjustmentFunction = null)
        {
            var range = new SourceTargetRange(oldID, oldID, newID);
            if (range.Difference == 0)
                return;

            for (int i = 0; i < objects.Count; i++)
                adjustmentFunction(objects[i], range);
            levelAdjustmentFunction?.Invoke(range);
        }
        private void CompactlyReallocateIDs(LevelObjectIDType type, List<Range>? ignoredRanges, IDAdjustmentFunction adjustmentFunction, LevelIDAdjustmentFunction? levelAdjustmentFunction = null)
        {
            IDMigrationOperationInitialized?.Invoke();

            ignoredRanges = ignoredRanges?.SortAndMerge();

            var categorized = Level.LevelObjects.GetObjectsByUsedIDs(type);
            int currentID = 1;
            int currentRangesIndex = 0;
            int incomingIgnoredRangeIndex = 0;
            foreach (var cat in categorized)
            {
                if (ignoredRanges != null)
                {
                    // Do something about index checking, the code feels kinda awful
                    while (currentRangesIndex < ignoredRanges.Count && !ignoredRanges[currentRangesIndex].Contains(cat.Key))
                    {
                        if (!ignoredRanges[currentRangesIndex].AfterStart(cat.Key))
                            break;
                        currentRangesIndex++;
                    }

                    if (currentRangesIndex < ignoredRanges.Count && ignoredRanges[currentRangesIndex].Contains(cat.Key))
                        continue;

                    if (incomingIgnoredRangeIndex < ignoredRanges.Count)
                    {
                        var incoming = ignoredRanges[incomingIgnoredRangeIndex];
                        if (incoming.Contains(currentID))
                        {
                            currentID = incoming.End.Value;
                            incomingIgnoredRangeIndex++;
                        }
                    }
                }

                PerformIDMigration(cat.Value, cat.Key, currentID, adjustmentFunction, levelAdjustmentFunction);
                IDMigrationProgressReported?.Invoke(currentID, categorized.Count);
                currentID++;
            }

            IDMigrationOperationCompleted?.Invoke();
        }

        private Action GetIDMigrationDelegate() => GetIDMigrationDelegate(SelectedIDMigrationMode);
        private Action GetIDMigrationDelegate(LevelObjectIDType mode)
        {
            return mode switch
            {
                LevelObjectIDType.Group => PerformGroupIDMigration,
                LevelObjectIDType.Color => PerformColorIDMigration,
                LevelObjectIDType.Item => PerformItemIDMigration,
                LevelObjectIDType.Block => PerformBlockIDMigration,
                _ => throw new InvalidEnumArgumentException("My disappointment is immeasurable and my day is ruined."),
            };
        }
        private Action<List<SourceTargetRange>> GetIDMigrationCustomStepsDelegate() => GetIDMigrationCustomStepsDelegate(SelectedIDMigrationMode);
        private Action<List<SourceTargetRange>> GetIDMigrationCustomStepsDelegate(LevelObjectIDType mode)
        {
            return mode switch
            {
                LevelObjectIDType.Group => PerformGroupIDMigration,
                LevelObjectIDType.Color => PerformColorIDMigration,
                LevelObjectIDType.Item => PerformItemIDMigration,
                LevelObjectIDType.Block => PerformBlockIDMigration,
                _ => throw new InvalidEnumArgumentException("My disappointment is immeasurable and my day is ruined."),
            };
        }

        // Perhaps experiment with creating a class that performs the adjustment using appropriate interfaces

        private static IDAdjustmentFunction GetIDAdjustmentFunction(LevelObjectIDType type)
        {
            return type switch
            {
                LevelObjectIDType.Group => AdjustGroups,
                LevelObjectIDType.Color => AdjustColors,
                LevelObjectIDType.Item => AdjustItems,
                LevelObjectIDType.Block => AdjustBlocks,
                _ => throw new InvalidEnumArgumentException("My disappointment is immeasurable and my day is ruined."),
            };
        }
        private static void AdjustGroups(GeneralObject o, SourceTargetRange r)
        {
            int d = r.Difference;

            var groups = o.GroupIDs;
            if (groups != null)
                for (int g = 0; g < groups.Length; g++)
                    if (r.IsWithinSourceRange(groups[g]))
                        o.AdjustGroupID(g, d);

            if (o is IHasTargetGroupID t && r.IsWithinSourceRange(t.TargetGroupID))
                t.TargetGroupID += d;
            if (o is IHasSecondaryGroupID s && r.IsWithinSourceRange(s.SecondaryGroupID))
                s.SecondaryGroupID += d;
        }
        private static void AdjustColors(GeneralObject o, SourceTargetRange r)
        {
            int d = r.Difference;

            if (r.IsWithinSourceRange(o.Color1ID))
                o.Color1ID += d;
            if (r.IsWithinSourceRange(o.Color2ID))
                o.Color2ID += d;

            if (o is IHasTargetColorID t && r.IsWithinSourceRange(t.TargetColorID))
                t.TargetColorID += d;
            if (o is IHasCopiedColorID c && r.IsWithinSourceRange(c.CopiedColorID))
                c.CopiedColorID += d;
        }
        private static void AdjustItems(GeneralObject o, SourceTargetRange r)
        {
            int d = r.Difference;

            if (o is IHasPrimaryItemID p && r.IsWithinSourceRange(p.PrimaryItemID))
                p.PrimaryItemID += d;
            if (o is IHasTargetItemID t && r.IsWithinSourceRange(t.TargetItemID))
                t.TargetItemID += d;
        }
        private static void AdjustBlocks(GeneralObject o, SourceTargetRange r)
        {
            int d = r.Difference;

            if (o is IHasPrimaryBlockID p && r.IsWithinSourceRange(p.PrimaryBlockID))
                p.PrimaryBlockID += d;
            if (o is IHasSecondaryBlockID s && r.IsWithinSourceRange(s.SecondaryBlockID))
                s.SecondaryBlockID += d;
        }

        private LevelIDAdjustmentFunction? GetLevelIDAdjustmentFunction(LevelObjectIDType type)
        {
            return type switch
            {
                LevelObjectIDType.Color => AdjustColorChannels,
                _ => null,
            };
        }
        private void AdjustColorChannels(SourceTargetRange r)
        {
            // Recalculation on every step might hurt a little bit, but in real-world situations,
            // not nearly many steps will be performed to be severely impacted by this
            var dict = Level.ColorChannels.GetCopiedColorChannelReferenceDictionary();

            for (int i = 0; i <= r.Range; i++)
            {
                var original = Level.ColorChannels[r.SourceFrom + i];
                var target = Level.ColorChannels[r.TargetFrom + i];
                var channels = dict[target];
                target.AssignPropertiesFrom(original);
                target.SetColorChannelID(r.TargetFrom + i, channels);
            }
        }

        private delegate void IDAdjustmentFunction(GeneralObject obj, SourceTargetRange range);
        private delegate void LevelIDAdjustmentFunction(SourceTargetRange range);
        #endregion
    }
}
