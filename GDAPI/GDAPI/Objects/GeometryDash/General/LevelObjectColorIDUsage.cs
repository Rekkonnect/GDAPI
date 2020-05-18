using GDAPI.Objects.GeometryDash.LevelObjects;
using System.Collections.Generic;

namespace GDAPI.Objects.GeometryDash.General
{
    /// <summary>Contains the Color IDs that are being used in a <seealso cref="GeneralObject"/>.</summary>
    public class LevelObjectColorIDUsage : LevelObjectIDUsage
    {
        /// <summary>The main Color ID of the object.</summary>
        public int MainColorID { get; set; }
        /// <summary>The detail Color ID of the object.</summary>
        public int DetailColorID { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectColorIDUsage"/> class.</summary>
        public LevelObjectColorIDUsage() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectColorIDUsage"/> class.</summary>
        /// <param name="primaryID">The primary Color ID of the object.</param>
        /// <param name="secondaryID">The secondary Color ID of the object.</param>
        public LevelObjectColorIDUsage(int primaryID, int secondaryID) : base(primaryID, secondaryID) { }
        /// <summary>Initializes a new instance of the <seealso cref="LevelObjectColorIDUsage"/> class.</summary>
        /// <param name="primaryID">The primary Color ID of the object.</param>
        /// <param name="secondaryID">The secondary Color ID of the object.</param>
        /// <param name="mainColorID">The main Color ID of the object.</param>
        /// <param name="detailColorID">The detail Color ID of the object.</param>
        public LevelObjectColorIDUsage(int primaryID, int secondaryID, int mainColorID, int detailColorID) : base(primaryID, secondaryID)
        {
            MainColorID = mainColorID;
            DetailColorID = detailColorID;
        }

        public override IEnumerator<int> GetEnumerator() => new LevelObjectColorIDUsageEnumerator(this);

        /// <summary>Represents an enumerator for all the used IDs in a <seealso cref="LevelObjectColorIDUsage"/> object.</summary>
        protected class LevelObjectColorIDUsageEnumerator : LevelObjectIDUsageEnumerator
        {
            private LevelObjectColorIDUsage ColorUsage => Usage as LevelObjectColorIDUsage;

            protected override int Length => base.Length + 2;

            public LevelObjectColorIDUsageEnumerator(LevelObjectColorIDUsage u)
                : base(u) { }

            /// <summary>Gets the element in the sequence based on the given index.</summary>
            /// <param name="index">The index representing the element to return, with 2 being the <seealso cref="MainColorID"/>, and 3 being the <seealso cref="DetailColorID"/>. The other indices are covered by the base function.</param>
            protected override int GetElementAtIndex(int index)
            {
                return index switch
                {
                    2 => ColorUsage.MainColorID,
                    3 => ColorUsage.DetailColorID,
                    _ => base.GetElementAtIndex(index),
                };
            }
        }
    }
}
