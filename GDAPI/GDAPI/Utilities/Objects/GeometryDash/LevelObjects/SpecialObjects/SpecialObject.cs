using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Objects.General.DataStructures;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a special object.</summary>
    public abstract class SpecialObject : GeneralObject
    {
        /// <summary>The <seealso cref="bool"/>s of the special objects.</summary>
        protected BitArray8 SpecialObjectBools = new BitArray8();
        /// <summary>The valid object IDs of the special object. Only override if the class does not represent a single object.</summary>
        protected virtual int[] ValidObjectIDs => null;
        /// <summary>The name as a string of the special object. Only override if the class does not represent a single object.</summary>
        protected virtual string SpecialObjectTypeName => "special object";

        /// <summary>Initializes a new instance of the <seealso cref="SpecialObject"/> class.</summary>
        public SpecialObject() { }
        /// <summary>Initializes a new instance of the <seealso cref="SpecialObject"/> class.</summary>
        /// <param name="objectID">The object ID of the special object.</param>
        public SpecialObject(int objectID) : this(objectID, 0, 0) { }
        /// <summary>Initializes a new instance of the <seealso cref="SpecialObject"/> class.</summary>
        /// <param name="objectID">The object ID of the special object.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public SpecialObject(int objectID, double x, double y)
            : base(objectID, x, y)
        {
            // Since this constructor must be only used by general object classes that may have different object IDs, assume the property is overriden
            if (!ValidObjectIDs.Contains(objectID))
                throw new InvalidCastException($"This object ID does not represent a valid {SpecialObjectTypeName}.");
        }

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as SpecialObject;
            c.SpecialObjectBools = SpecialObjectBools;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Returns the name of the object type.</summary>
        /// <param name="lowerLastWord">Does not determine anything.</param>
        public override string GetObjectTypeName(bool lowerLastWord) => base.GetObjectTypeName(true);

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            return SpecialObjectBools == (other as SpecialObject).SpecialObjectBools
                && base.EqualsInherited(other);
        }
    }
}
