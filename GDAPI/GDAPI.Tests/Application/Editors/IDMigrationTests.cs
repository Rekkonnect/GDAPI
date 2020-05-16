using GDAPI.Application.Editors;
using GDAPI.Enumerations;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.Triggers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GDAPI.Tests.Application.Editors
{
    public class IDMigrationTests
    {
        private GeneralObject[] normalBlocks = new GeneralObject[5];
        private MoveTrigger[] moveTriggers = new MoveTrigger[5];
        private PickupItem[] pickupItems = new PickupItem[5];
        private PickupTrigger[] pickupTriggers = new PickupTrigger[5];
        private ColorTrigger[] colorTriggers = new ColorTrigger[5];
        private InstantCountTrigger[] instantCountTriggers = new InstantCountTrigger[5];
        private CollisionBlock[] collisionBlocks = new CollisionBlock[5];
        private CollisionTrigger[] collisionTriggers = new CollisionTrigger[5];

        private LevelObjectCollection allObjects;
        private Level level;
        private Editor editor;

        [SetUp]
        public void Initialize()
        {
            InitializeLevelEditorInstances();
        }

        private void InitializeLevelEditorInstances()
        {
            level = new Level();
            editor = new Editor(level);
            ReinitializeLevelObjects();
        }

        private void ReinitializeLevelStuff()
        {
            ReinitializeLevelObjects();
            InitializeColorChannels();
        }
        private void ReinitializeLevelObjects()
        {
            InitializeObjects();
            level.LevelObjects = allObjects;
        }
        private void InitializeColorChannels()
        {
            for (int i = 1; i < 1000; i++)
                level.ColorChannels[i].CopiedColorID = i; // an indicator for the color channels
        }
        private void InitializeObjects()
        {
            for (int i = 0; i < 5; i++)
                normalBlocks[i] = new GeneralObject(1)
                {
                    GroupIDs = new int[] { i + 1 },
                    Color1ID = i + 1,
                    Color2ID = i + 21,
                };
            for (int i = 0; i < 5; i++)
                moveTriggers[i] = new MoveTrigger(0, i + 1);
            for (int i = 0; i < 5; i++)
                pickupItems[i] = new PickupItem(1275)
                {
                    TargetItemID = i + 1,
                };
            for (int i = 0; i < 5; i++)
                pickupTriggers[i] = new PickupTrigger(i + 1, 0);
            for (int i = 0; i < 5; i++)
                colorTriggers[i] = new ColorTrigger(i + 1)
                {
                    CopiedColorID = i + 21,
                };
            for (int i = 0; i < 5; i++)
                instantCountTriggers[i] = new InstantCountTrigger(i + 1, i + 1, 0);
            for (int i = 0; i < 5; i++)
                collisionBlocks[i] = new CollisionBlock
                {
                    BlockID = i + 1,
                };
            for (int i = 0; i < 5; i++)
                collisionTriggers[i] = new CollisionTrigger(i + 1, i + 21, i + 1);

            allObjects = new LevelObjectCollection(normalBlocks.Concat(moveTriggers).Concat(pickupItems).Concat(pickupTriggers).Concat(colorTriggers).Concat(instantCountTriggers).Concat(collisionBlocks).Concat(collisionTriggers));
        }

        #region ID Migration
        [Test]
        public void IDMigration()
        {
            var step0 = new SourceTargetRange(1, 5, 6);    //  1 -  5 >  6 - 10
            var step1 = new SourceTargetRange(21, 25, 46); // 21 - 25 > 46 - 50
            var step2 = new SourceTargetRange(1, 25, 26);  //  1 - 25 > 26 - 50
            var step3 = new SourceTargetRange(8, 8, 10);   //       8 >      10

            ReinitializeLevelStuff();

            // TODO: Move assertions to within the verification functions
            editor.PerformGroupIDMigration(new List<SourceTargetRange> { step0 });
            Assert.IsTrue(VerifyGroupIDMigration(step0.TargetFrom));
            editor.PerformColorIDMigration(new List<SourceTargetRange> { step2 });
            Assert.IsTrue(VerifyColorIDMigration(step2.TargetFrom, step1.TargetFrom));
            editor.PerformItemIDMigration(new List<SourceTargetRange> { step0 });
            Assert.IsTrue(VerifyItemIDMigration(step0.TargetFrom));
            editor.PerformBlockIDMigration(new List<SourceTargetRange> { step1 });
            Assert.IsTrue(VerifyBlockIDMigration(step0.SourceFrom, step1.TargetFrom));
            // TODO: Add more test steps with multiple step ID migrations

            bool VerifyGroupIDMigration(int offset)
            {
                for (int i = 0; i < 5; i++)
                    if (normalBlocks[i].GetGroupID(0) != i + offset)
                        return false;
                for (int i = 0; i < 5; i++)
                    if (moveTriggers[i].TargetGroupID != i + offset)
                        return false;
                for (int i = 0; i < 5; i++)
                    if (instantCountTriggers[i].TargetGroupID != i + offset)
                        return false;
                for (int i = 0; i < 5; i++)
                    if (collisionTriggers[i].TargetGroupID != i + offset)
                        return false;
                return true;
            }
            bool VerifyColorIDMigration(int offsetA, int offsetB)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (normalBlocks[i].Color1ID != i + offsetA)
                        return false;
                    if (normalBlocks[i].Color2ID != i + offsetB)
                        return false;
                }
                for (int i = 0; i < 5; i++)
                {
                    if (colorTriggers[i].TargetColorID != i + offsetA)
                        return false;
                    if (colorTriggers[i].CopiedColorID != i + offsetB)
                        return false;
                }
                for (int i = 26; i <= 50; i++)
                {
                    int origin = i - 25;
                    if (level.ColorChannels[i].CopiedColorID != i)
                        return false;
                    if (level.ColorChannels[i].ColorChannelID != i)
                        return false;
                    if (level.ColorChannels[origin].ColorChannelID != origin)
                        return false;
                }
                return true;
            }
            bool VerifyItemIDMigration(int offset)
            {
                for (int i = 0; i < 5; i++)
                    if (pickupItems[i].TargetItemID != i + offset)
                        return false;
                for (int i = 0; i < 5; i++)
                    if (pickupTriggers[i].TargetItemID != i + offset)
                        return false;
                return true;
            }
            bool VerifyBlockIDMigration(int offsetA, int offsetB)
            {
                for (int i = 0; i < 5; i++)
                    if (collisionBlocks[i].BlockID != i + offsetA)
                        return false;
                for (int i = 0; i < 5; i++)
                {
                    if (collisionTriggers[i].PrimaryBlockID != i + offsetA)
                        return false;
                    if (collisionTriggers[i].SecondaryBlockID != i + offsetB)
                        return false;
                }
                return true;
            }
        }
        #endregion

        #region ID Reallocation
        private static readonly int[] primaryTargetIDs = { 1, 2, 3, 4, 5 };
        private static readonly int[] secondaryTargetIDs = { 6, 7, 8, 9, 10 };
        private static readonly int[] primaryTargetIDsWithIgnored = { 30, 40, 1, 2, 3 };
        private static readonly int[] secondaryTargetIDsWithIgnored = { 901, 902, 4, 5, 6 };

        [Test]
        public void GroupIDReallocation()
        {
            RunIDReallocationTest(IDMigrationMode.Groups, AssertGroupIDReallocation);
        }
        [Test]
        public void ColorIDReallocation()
        {
            RunIDReallocationTest(IDMigrationMode.Colors, AssertColorIDReallocation, AssertExtraColorIDReallication);
        }
        [Test]
        public void ItemIDReallocation()
        {
            RunIDReallocationTest(IDMigrationMode.Items, AssertItemIDReallocation);
        }
        [Test]
        public void BlockIDReallocation()
        {
            RunIDReallocationTest(IDMigrationMode.Blocks, AssertBlockIDReallocation);
        }

        private void AssertGroupIDReallocation(int i, int id1, int id2)
        {
            Assert.AreEqual(id1, normalBlocks[i].GroupIDs[0]);
            Assert.AreEqual(id1, moveTriggers[i].TargetGroupID);
            Assert.AreEqual(id1, instantCountTriggers[i].TargetGroupID);
            Assert.AreEqual(id1, collisionTriggers[i].TargetGroupID);
        }
        private void AssertColorIDReallocation(int i, int id1, int id2)
        {
            Assert.AreEqual(id1, normalBlocks[i].Color1ID);
            Assert.AreEqual(id2, normalBlocks[i].Color2ID);
            Assert.AreEqual(id1, colorTriggers[i].TargetColorID);
            Assert.AreEqual(id2, colorTriggers[i].CopiedColorID);
        }
        private void AssertItemIDReallocation(int i, int id1, int id2)
        {
            Assert.AreEqual(id1, pickupItems[i].TargetItemID);
            Assert.AreEqual(id1, pickupTriggers[i].TargetItemID);
            Assert.AreEqual(id1, instantCountTriggers[i].ItemID);
        }
        private void AssertBlockIDReallocation(int i, int id1, int id2)
        {
            Assert.AreEqual(id1, collisionBlocks[i].BlockID);
            Assert.AreEqual(id1, collisionTriggers[i].PrimaryBlockID);
            Assert.AreEqual(id2, collisionTriggers[i].SecondaryBlockID);
        }

        private void AssertExtraColorIDReallication(List<SourceTargetRange> steps, List<Range> ignoredRanges)
        {

        }

        private void RunIDReallocationTest(IDMigrationMode mode, PostReallocationAssertion assertion, PostReallocationExtra extra = null)
        {
            ReinitializeLevelStuff();

            var steps = new List<SourceTargetRange>();
            for (int i = 4; i >= 0; i--)
            {
                steps.Add(new SourceTargetRange(i + 1, i + 1, (i + 3) * 10));
                steps.Add(new SourceTargetRange(i + 21, i + 21, i + 901));
            }

            editor.PerformMigration(mode, steps);
            editor.CompactlyReallocateIDs((LevelObjectIDType)mode);
            for (int i = 0; i < 5; i++)
            {
                int id1 = primaryTargetIDs[i];
                int id2 = secondaryTargetIDs[i];
                assertion(i, id1, id2);
            }
            extra?.Invoke(steps, null);

            // Reset object IDs
            ReinitializeLevelStuff();
            editor.PerformMigration(mode, steps);

            // Current ranges are 30, 40, 50, 60, 70 and 901, 902, 903, 904, 905
            // Reallocation should ignore 30, 40 and 901, 902 meaning
            // 50 > 1
            // 60 > 2
            // 70 > 3
            // 903 > 4
            // 904 > 5
            // 905 > 6
            var ignoredRanges = new List<Range> { 27..31, 22..24, 899..903, 78..700, 35..45 };
            editor.CompactlyReallocateIDs((LevelObjectIDType)mode, ignoredRanges);
            for (int i = 0; i < 5; i++)
            {
                int id1 = primaryTargetIDsWithIgnored[i];
                int id2 = secondaryTargetIDsWithIgnored[i];
                assertion(i, id1, id2);
            }
            extra?.Invoke(steps, ignoredRanges);
        }

        private delegate void PostReallocationAssertion(int i, int id1, int id2 = default);
        private delegate void PostReallocationExtra(List<SourceTargetRange> steps, List<Range> ignoredRanges);
        #endregion
    }
}
