using GDAPI.Application.Editors.Actions.LevelActions;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Functions.Extensions;
using GDAPI.Information.GeometryDash;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.Triggers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System.Collections.Generic;

namespace GDAPI.Tests.Objects.GeometryDash
{
    public class LevelObjectIDUsageTests
    {
        private int[] groupIDs = new int[] { 4, 1, 412, 3 };

        [Test]
        public void GetUsedIDs()
        {
            // Part 0
            var moveTrigger = new MoveTrigger();
            moveTrigger.GroupIDs = groupIDs;
            moveTrigger.TargetGroupID = 163;
            moveTrigger.TargetPosGroupID = 321;

            var used = moveTrigger.UsedGroupIDs;
            Assert.IsTrue(used.AssignedIDs.Contains(4));
            Assert.IsTrue(used.AssignedIDs.Contains(1));
            Assert.IsTrue(used.AssignedIDs.Contains(412));
            Assert.IsTrue(used.AssignedIDs.Contains(3));
            Assert.AreEqual(163, used.PrimaryID);
            Assert.AreEqual(321, used.SecondaryID);

            var block = new GeneralObject();
            block.GroupIDs = groupIDs;

            used = block.UsedGroupIDs;
            Assert.IsTrue(used.AssignedIDs.Contains(4));
            Assert.IsTrue(used.AssignedIDs.Contains(1));
            Assert.IsTrue(used.AssignedIDs.Contains(412));
            Assert.IsTrue(used.AssignedIDs.Contains(3));
            Assert.AreEqual(0, used.PrimaryID);
            Assert.AreEqual(0, used.SecondaryID);

            // Part 1
            var pulse = new PulseTrigger();
            var pickupItem = new PickupItem(ObjectLists.PickupItemList[0]);
            var pickup = new PickupTrigger();
            var collision = new CollisionTrigger();

            pulse.PulseTargetType = PulseTargetType.Group;
            pulse.TargetGroupID = 3;
            var groups = pulse.UsedGroupIDs;

            pulse.PulseTargetType = PulseTargetType.ColorChannel;
            pulse.TargetColorID = 4;
            pulse.CopiedColorID = 5;
            var colors = pulse.UsedColorIDs;

            pickupItem.TargetItemID = 41;
            pickup.TargetItemID = 42;
            var items0 = pickupItem.UsedItemIDs;
            var items1 = pickup.UsedItemIDs;

            collision.PrimaryBlockID = 50;
            collision.SecondaryBlockID = 51;
            var blocks = collision.UsedBlockIDs;

            Assert.AreEqual(3, groups.PrimaryID);
            Assert.AreEqual(0, groups.SecondaryID);
            Assert.AreEqual(4, colors.PrimaryID);
            Assert.AreEqual(5, colors.SecondaryID);
            Assert.AreEqual(41, items0.PrimaryID);
            Assert.AreEqual(0, items0.SecondaryID);
            Assert.AreEqual(42, items1.PrimaryID);
            Assert.AreEqual(0, items1.SecondaryID);
            Assert.AreEqual(50, blocks.PrimaryID);
            Assert.AreEqual(51, blocks.SecondaryID);
        }

        [Test]
        public void EnumerateUsedIDs()
        {
            AssertEnumeration(1, 2, 1, 2);
            AssertEnumeration(0, 2, 2);
            AssertEnumeration(1, 0, 1);
            AssertEnumeration(0, 0);

            static void AssertEnumeration(int primary, int secondary, params int[] expected)
            {
                var used = new LevelObjectIDUsage(primary, secondary);

                var ids = new List<int>();
                foreach (var id in used)
                    ids.Add(id);

                Assert.AreEqual(expected.Length, ids.Count);
                for (int i = 0; i < expected.Length; i++)
                    Assert.AreEqual(expected[i], ids[i]);
            }
        }

        [Test]
        public void EnumerateUsedGroupIDs()
        {
            AssertEnumeration(0, 0);
            AssertEnumeration(4, 0, 4);
            AssertEnumeration(4, 54, 4, 54);
            AssertEnumeration(0, 54, 54);

            void AssertEnumeration(int primary, int secondary, params int[] expected)
            {
                var used = new LevelObjectGroupIDUsage(primary, secondary, groupIDs);

                var ids = new List<int>();
                foreach (var id in used)
                    ids.Add(id);

                Assert.AreEqual(expected.Length + groupIDs.Length, ids.Count);
                for (int i = 0; i < expected.Length; i++)
                    Assert.AreEqual(expected[i], ids[i]);
                for (int i = 0; i < groupIDs.Length; i++)
                    Assert.AreEqual(groupIDs[i], ids[i + expected.Length]);
            }
        }

        [Test]
        public void EnumerateUsedColorIDs()
        {
            // Imagine writing 16 lines for each single permutation
            AssertEnumeration(1, 2, 3, 4, 1, 2, 3, 4);
            AssertEnumeration(0, 2, 3, 4, 2, 3, 4);
            AssertEnumeration(1, 0, 3, 4, 1, 3, 4);
            AssertEnumeration(1, 2, 0, 4, 1, 2, 4);
            AssertEnumeration(1, 2, 3, 0, 1, 2, 3);
            // ... fuck off, let's just assume it works
            AssertEnumeration(1, 0, 0, 0, 1);
            AssertEnumeration(0, 2, 0, 0, 2);
            AssertEnumeration(0, 0, 3, 0, 3);
            AssertEnumeration(0, 0, 0, 4, 4);
            AssertEnumeration(0, 0, 0, 0);

            void AssertEnumeration(int primary, int secondary, int main, int detail, params int[] expected)
            {
                var used = new LevelObjectColorIDUsage(primary, secondary, main, detail);

                var ids = new List<int>();
                foreach (var id in used)
                    ids.Add(id);

                Assert.AreEqual(expected.Length, ids.Count);
                for (int i = 0; i < expected.Length; i++)
                    Assert.AreEqual(expected[i], ids[i]);
            }
        }
    }
}
