using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Information.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a rotating object with rotating properties.</summary>
    public class RotatingObject : SpecialObject
    {
        private float customRotationSpeed;

        /// <summary>The valid object IDs of the special object.</summary>
        public override int[] ValidObjectIDs => ObjectLists.RotatingObjectList;
        /// <summary>The name as a string of the special object.</summary>
        protected override string SpecialObjectTypeName => "rotating object";

        /// <summary>Represents the Disable Rotation property of the rotating object.</summary>
        [ObjectStringMappable(ObjectProperty.DisableRotation, false)]
        public bool DisableRotation
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }
        /// <summary>Represents the Custom Rotation Speed property of the rotating object.</summary>
        [ObjectStringMappable(ObjectProperty.CustomRotationSpeed, 0d)]
        public double CustomRotationSpeed
        {
            get => customRotationSpeed;
            set => customRotationSpeed = (float)value;
        }

        /// <summary>Initializes a new empty instance of the <seealso cref="RotatingObject"/> class. For internal use only.</summary>
        private RotatingObject() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="RotatingObject"/> class.</summary>
        /// <param name="objectID">The object ID of the rotating object.</param>
        public RotatingObject(int objectID) : base(objectID) { }
        /// <summary>Initializes a new instance of the <seealso cref="RotatingObject"/> class.</summary>
        /// <param name="objectID">The object ID of the rotating object.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public RotatingObject(int objectID, double x, double y)
            : base(objectID, x, y) { }

        /// <summary>Returns a clone of this <seealso cref="RotatingObject"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new RotatingObject());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as RotatingObject;
            c.customRotationSpeed = customRotationSpeed;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as RotatingObject;
            return base.EqualsInherited(other)
                && customRotationSpeed == z.customRotationSpeed;
        }
    }
}
