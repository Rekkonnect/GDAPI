using GDAPI.Application.Editors;
using GDAPI.Objects.General;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.Triggers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GDAPI.Tests.Application.Editors
{
    public class EditorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IDMigration()
        {
            var normalBlocks = new GeneralObject[5];
            var moveTriggers = new MoveTrigger[5];
            var pickupItems = new PickupItem[5];
            var pickupTriggers = new PickupTrigger[5];
            var colorTriggers = new ColorTrigger[5];
            var instantCountTriggers = new InstantCountTrigger[5];
            var collisionBlocks = new CollisionBlock[5];
            var collisionTriggers = new CollisionTrigger[5];

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

            var allObjects = new LevelObjectCollection(normalBlocks.Concat(moveTriggers).Concat(pickupItems).Concat(pickupTriggers).Concat(colorTriggers).Concat(instantCountTriggers).Concat(collisionBlocks).Concat(collisionTriggers).ToList());

            var level = new Level
            {
                Name = "ID Migration Test Level",
                Description = "This is a test level to help testing the ID migration feature in the backend",
                LevelObjects = allObjects,
            };

            var editor = new Editor(level);

            var step0 = new SourceTargetRange(1, 5, 6);
            var step1 = new SourceTargetRange(21, 25, 46);
            var step2 = new SourceTargetRange(1, 25, 26);
            var step3 = new SourceTargetRange(8, 8, 10);

            editor.PerformGroupIDMigration(new List<SourceTargetRange> { step0 });
            Assert.IsTrue(VerifyGroupIDMigration(step0.TargetFrom));
            editor.PerformColorIDMigration(new List<SourceTargetRange> { step2 });
            Assert.IsTrue(VerifyColorIDMigration(step2.TargetFrom, step1.TargetFrom));
            editor.PerformItemIDMigration(new List<SourceTargetRange> { step0 });
            Assert.IsTrue(VerifyItemIDMigration(step0.TargetFrom));
            editor.PerformBlockIDMigration(new List<SourceTargetRange> { step1 });
            Assert.IsTrue(VerifyBlockIDMigration(step0.SourceFrom, step1.TargetFrom));
            // TODO: Add more test steps with multiple step ID migrations

            bool VerifyGroupIDMigration(int targetFrom)
            {
                for (int i = 0; i < 5; i++)
                    if (normalBlocks[i].GetGroupID(0) != i + targetFrom)
                        return false;
                for (int i = 0; i < 5; i++)
                    if (moveTriggers[i].TargetGroupID != i + targetFrom)
                        return false;
                for (int i = 0; i < 5; i++)
                    if (instantCountTriggers[i].TargetGroupID != i + targetFrom)
                        return false;
                for (int i = 0; i < 5; i++)
                    if (collisionTriggers[i].TargetGroupID != i + targetFrom)
                        return false;
                return true;
            }
            bool VerifyColorIDMigration(int targetFromA, int targetFromB)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (normalBlocks[i].Color1ID != i + targetFromA)
                        return false;
                    if (normalBlocks[i].Color2ID != i + targetFromB)
                        return false;
                }
                for (int i = 0; i < 5; i++)
                {
                    if (colorTriggers[i].TargetColorID != i + targetFromA)
                        return false;
                    if (colorTriggers[i].CopiedColorID != i + targetFromB)
                        return false;
                }
                return true;
            }
            bool VerifyItemIDMigration(int targetFrom)
            {
                for (int i = 0; i < 5; i++)
                    if (pickupItems[i].TargetItemID != i + targetFrom)
                        return false;
                for (int i = 0; i < 5; i++)
                    if (pickupTriggers[i].TargetItemID != i + targetFrom)
                        return false;
                return true;
            }
            bool VerifyBlockIDMigration(int targetFromA, int targetFromB)
            {
                for (int i = 0; i < 5; i++)
                    if (collisionBlocks[i].BlockID != i + targetFromA)
                        return false;
                for (int i = 0; i < 5; i++)
                {
                    if (collisionTriggers[i].PrimaryBlockID != i + targetFromA)
                        return false;
                    if (collisionTriggers[i].SecondaryBlockID != i + targetFromB)
                        return false;
                }
                return true;
            }
        }
    }
}
