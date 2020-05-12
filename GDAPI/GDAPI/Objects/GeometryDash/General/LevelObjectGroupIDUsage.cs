using GDAPI.Objects.GeometryDash.LevelObjects;
using System.Collections.Generic;

namespace GDAPI.Objects.GeometryDash.General
{
    /// <summary>Contains the Group IDs that are being used in a <seealso cref="GeneralObject"/>.</summary>
    public class LevelObjectGroupIDUsage : LevelObjectIDUsage
    {
        /// <summary>The object's assigned Group IDs.</summary>
        public int[] AssignedIDs { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectGroupIDUsage"/> class.</summary>
        public LevelObjectGroupIDUsage() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectGroupIDUsage"/> class.</summary>
        /// <param name="primaryID">The primary Group ID of the object.</param>
        /// <param name="secondaryID">The secondary Group ID of the object.</param>
        public LevelObjectGroupIDUsage(int primaryID, int secondaryID) : base(primaryID, secondaryID) { }
        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectGroupIDUsage"/> class.</summary>
        /// <param name="primaryID">The primary Group ID of the object.</param>
        /// <param name="secondaryID">The secondary Group ID of the object.</param>
        /// <param name="assignedIDs">The assigned Group IDs for the object.</param>
        public LevelObjectGroupIDUsage(int primaryID, int secondaryID, int[] assignedIDs) : base(primaryID, secondaryID)
        {
            AssignedIDs = assignedIDs;
        }

        public override IEnumerator<int> GetEnumerator() => new LevelObjectGroupIDUsageEnumerator(this);

        /// <summary>Represents an enumerator for all the used IDs in a <seealso cref="LevelObjectGroupIDUsage"/> object.</summary>
        protected class LevelObjectGroupIDUsageEnumerator : LevelObjectIDUsageEnumerator
        {
            private LevelObjectGroupIDUsage GroupUsage => Usage as LevelObjectGroupIDUsage;

            protected override int Length => base.Length + (GroupUsage?.AssignedIDs.Length ?? 0);

            public LevelObjectGroupIDUsageEnumerator(LevelObjectGroupIDUsage u)
                : base(u) { }

            /// <summary>Gets the element in the sequence based on the given index.</summary>
            /// <param name="index">The index representing the element to return, with 0 being the <seealso cref="PrimaryID"/>, and 1 being the <seealso cref="SecondaryID"/>, and anything above 1 representing the index within the assigned IDs, offset by -2.</param>
            protected override int GetElementAtIndex(int index)
            {
                if (index < 2)
                    return base.GetElementAtIndex(index);
                return GroupUsage?.AssignedIDs[index - 2] ?? 0;
            }
        }
    }
}
