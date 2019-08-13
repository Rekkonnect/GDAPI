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
    /// <summary>Represents a pulsating object.</summary>
    public class PulsatingObject : SpecialObject
    {
        private float animationSpeed = 1;

        /// <summary>The valid object IDs of the special object.</summary>
        protected override int[] ValidObjectIDs => ObjectLists.PulsatingObjectList;
        /// <summary>The name as a string of the special object.</summary>
        protected override string SpecialObjectTypeName => "pulsating object";

        /// <summary>The animation speed of the pulsating object as a ratio.</summary>
        [ObjectStringMappable(ObjectParameter.AnimationSpeed, 1d)]
        public double AnimationSpeed
        {
            get => animationSpeed;
            set => animationSpeed = (float)value;
        }
        /// <summary>The Randomize Start property of the pulsating object.</summary>
        [ObjectStringMappable(ObjectParameter.RandomizeStart, false)]
        public bool RandomizeStart
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }

        /// <summary>Initializes a new empty instance of the <seealso cref="PulsatingObject"/> class. For internal use only.</summary>
        private PulsatingObject() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="PulsatingObject"/> class.</summary>
        /// <param name="objectID">The object ID of the pulsating object.</param>
        public PulsatingObject(int objectID) : base(objectID) { }
        /// <summary>Initializes a new instance of the <seealso cref="PulsatingObject"/> class.</summary>
        /// <param name="objectID">The object ID of the pulsating object.</param>
        /// <param name="x">The X location of the object.</param>
        /// <param name="y">The Y location of the object.</param>
        public PulsatingObject(int objectID, double x, double y)
            : base(objectID, x, y) { }

        /// <summary>Returns a clone of this <seealso cref="PulsatingObject"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new PulsatingObject());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as PulsatingObject;
            c.animationSpeed = animationSpeed;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as PulsatingObject;
            return base.EqualsInherited(other)
                && animationSpeed == z.animationSpeed;
        }
    }
}
