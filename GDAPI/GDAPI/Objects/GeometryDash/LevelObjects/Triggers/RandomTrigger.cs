using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Functions.Extensions;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Random trigger.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.Random)]
    public class RandomTrigger : Trigger, IHasTargetGroupID, IHasSecondaryGroupID
    {
        // Hopefully all the features for the Random trigger will be merged into one trigger
        // Right now, according to the sneak peek of 25/05/2019, there might as well be a new Random trigger offering that complex functionality
        // However, the normal trigger that was showcased earlier offers the exact same functionality in perhaps a more confusing way.

        private byte chance = 50;
        private short groupID1, groupID2;
        private ChancePoolInfo chancePool;

        /// <summary>The Object ID of the Random trigger.</summary>
        public override int ConstantObjectID => (int)TriggerType.Random;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => GroupID1;
            set => GroupID1 = (short)value;
        }
        /// <summary>The secondary Group ID of the trigger.</summary>
        public int SecondaryGroupID
        {
            get => GroupID2;
            set => GroupID2 = (short)value;
        }

        /// <summary>The Chance property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.Chance, 50)]
        public int Chance
        {
            get => chance;
            set => chance = (byte)value;
        }
        /// <summary>The Chance Lots property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ChanceLots)] // This implementation is such a wild guess
        public ChancePoolInfo ChancePool => chancePool;
        /// <summary>The Group ID 1 of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.TargetGroupID, 0)]
        public int GroupID1
        {
            get => groupID1;
            set => groupID1 = (short)value;
        }
        /// <summary>The Group ID 2 of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.SecondaryGroupID, 0)]
        public int GroupID2
        {
            get => groupID2;
            set => groupID2 = (short)value;
        }
        /// <summary>The Activate Group property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.ActivateGroup, false)]
        public bool ActivateGroup
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="RandomTrigger"/> class.</summary>
        public RandomTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="RandomTrigger"/> class.</summary>
        /// <param name="groupID1">The Group ID 1 of the trigger.</param>
        /// <param name="groupID2">The Group ID 2 of the trigger.</param>
        /// <param name="chance">The Chance property of the trigger.</param>
        /// <param name="activateGroup">The Activate Group property of the trigger.</param>
        public RandomTrigger(int groupID1, int groupID2, int chance, bool activateGroup = false)
             : base()
        {
            GroupID1 = groupID1;
            GroupID2 = groupID2;
            Chance = chance;
            ActivateGroup = activateGroup;
        }

        /// <summary>Returns a clone of this <seealso cref="RandomTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new RandomTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as RandomTrigger;
            c.groupID1 = groupID1;
            c.groupID2 = groupID2;
            c.chance = chance;
            c.chancePool = chancePool.Clone();
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as RandomTrigger;
            return base.EqualsInherited(other)
                && groupID1 == z.groupID1
                && groupID2 == z.groupID2
                && chance == z.chance;
        }

        /// <summary>Contains information about the chance lot.</summary>
        public struct ChanceLotInfo
        {
            // Presumably lots will not need to be Int32, however if need be, change both to Int32, since the size of the struct will become 8 bytes nonetheless because C#?
            private short groupID, lots;

            /// <summary>The group ID.</summary>
            public int GroupID
            {
                get => groupID;
                set => groupID = (short)value;
            }
            /// <summary>The lots this group ID is assigned.</summary>
            public int Lots
            {
                get => lots;
                set => lots = (short)value;
            }

            /// <summary>Initializes a new instance of the <seealso cref="ChanceLotInfo"/> struct.</summary>
            /// <param name="g">The group ID.</param>
            /// <param name="l">The lots this group ID is assigned.</param>
            public ChanceLotInfo(int g, int l)
            {
                groupID = (short)g;
                lots = (short)l;
            }

            /// <summary>Calculates the chance ratio of this group and returns a value between [0, 1].</summary>
            /// <param name="lotPool">The total lots in the lot pool.</param>
            public double ChanceRatio(int lotPool) => (double)lots / lotPool;
        }

        /// <summary>Contains information about a chance pool.</summary>
        public struct ChancePoolInfo
        {
            /// <summary>The chance lots for each group ID in this pool.</summary>
            public ChanceLotInfo[] ChanceLots { get; private set; }
            /// <summary>Returns the total lots of this pool.</summary>
            public int TotalLots
            {
                get
                {
                    int total = 0;
                    for (int i = 0; i < ChanceLots.Length; i++)
                        total += ChanceLots[i].Lots;
                    return total;
                }
            }

            /// <summary>Initializes a new instance of the <seealso cref="ChancePoolInfo"/> struct.</summary>
            /// <param name="chanceLots">The chance lots to be included in this chance pool.</param>
            public ChancePoolInfo(ChanceLotInfo[] chanceLots)
            {
                ChanceLots = chanceLots;
            }

            /// <summary>Adds the lots of the specified group ID by a specified amount. If the group ID has no lots registered, a record is created.</summary>
            /// <param name="groupID">The group ID to add the lots for.</param>
            /// <param name="lots">The lots of the group ID to add.</param>
            public void AddLots(int groupID, int lots)
            {
                if (FindChanceLotInfo(groupID, out var c))
                    c.Lots += lots;
                else
                    CreateNewEntry(groupID, lots);
            }

            /// <summary>Returns the chance ratio of a specified group in this chance pool and returns a value within [0, 1].</summary>
            /// <param name="groupID">The group ID to calculate the chance ratio of.</param>
            public double ChanceRatio(int groupID)
            {
                if (FindChanceLotInfo(groupID, out var c))
                    return c.ChanceRatio(TotalLots);
                return 0;
            }

            /// <summary>Clones this <seealso cref="ChancePoolInfo"/>.</summary>
            public ChancePoolInfo Clone() => new(ChanceLots.CopyArray());

            private bool FindChanceLotInfo(int groupID, out ChanceLotInfo info)
            {
                for (int i = 0; i < ChanceLots.Length; i++)
                    if (ChanceLots[i].GroupID == groupID)
                    {
                        info = ChanceLots[i];
                        return true;
                    }
                info = default;
                return false;
            }

            private void CreateNewEntry(int groupID, int lots)
            {
                ChanceLots = ChanceLots.Append(new ChanceLotInfo(groupID, lots));
            }
        }
    }
}
