using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Information.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents an animated object.</summary>
    public class AnimatedObject : SpecialObject
    {
        /// <summary>The valid object IDs of the special object.</summary>
        protected override int[] ValidObjectIDs => ObjectLists.AnimatedObjectList;
        /// <summary>The name as a string of the special object.</summary>
        protected override string SpecialObjectTypeName => "animated object";

        /// <summary>Initializes a new empty instance of the <seealso cref="AnimatedObject"/> class. For internal use only.</summary>
        private AnimatedObject() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="AnimatedObject"/> class.</summary>
        /// <param name="objectID">The object ID of the animated object.</param>
        public AnimatedObject(int objectID) : base(objectID) { }
        /// <summary>Initializes a new instance of the <seealso cref="AnimatedObject"/> class.</summary>
        /// <param name="objectID">The object ID of the animated object.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public AnimatedObject(int objectID, double x, double y)
            : base(objectID, x, y) { }

        /// <summary>Returns a clone of this <seealso cref="AnimatedObject"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new AnimatedObject());
    }
}
