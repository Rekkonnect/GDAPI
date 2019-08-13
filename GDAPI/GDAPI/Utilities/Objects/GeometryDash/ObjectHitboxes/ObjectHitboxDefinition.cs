using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Functions.Extensions;

namespace GDAPI.Utilities.Objects.GeometryDash.ObjectHitboxes
{
    /// <summary>Contains information about the object IDs that use this type of hitbox.</summary>
    public class ObjectHitboxDefinition
    {
        /// <summary>The object IDs that this hitbox is valid for.</summary>
        public List<int> ObjectIDs { get; set; }
        /// <summary>The hitbox that is valid for the object IDs.</summary>
        public Hitbox Hitbox { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="ObjectHitboxDefinition"/> class.</summary>
        /// <param name="objectID">The object ID that this hitbox is valid for.</param>
        /// <param name="hitbox">The hitbox of the object IDs.</param>
        public ObjectHitboxDefinition(int objectID, Hitbox hitbox) : this(new List<int> { objectID }, hitbox) { }
        /// <summary>Initializes a new instance of the <seealso cref="ObjectHitboxDefinition"/> class.</summary>
        /// <param name="objectIDs">The object IDs that this hitbox is valid for.</param>
        /// <param name="hitbox">The hitbox of the object IDs.</param>
        public ObjectHitboxDefinition(List<int> objectIDs, Hitbox hitbox)
        {
            ObjectIDs = objectIDs;
            Hitbox = hitbox;
        }

        /// <summary>Determines whether this equals another object. It is recommended to use the <seealso cref="Equals(ObjectHitboxDefinition)"/> method if the object is certainly an <seealso cref="ObjectHitboxDefinition"/>.</summary>
        /// <param name="obj">The other object to compare this with.</param>
        public override bool Equals(object obj) => (obj is ObjectHitboxDefinition other) && Equals(other);
        /// <summary>Determines whether this <seealso cref="ObjectHitboxDefinition"/> equals another <seealso cref="ObjectHitboxDefinition"/>.</summary>
        /// <param name="obj">The other <seealso cref="ObjectHitboxDefinition"/> to compare this with.</param>
        public bool Equals(ObjectHitboxDefinition other) => other.ObjectIDs.ContainsAll(ObjectIDs) && other.Hitbox == Hitbox;

        public static bool operator ==(ObjectHitboxDefinition left, ObjectHitboxDefinition right) => left.Equals(right);
        public static bool operator !=(ObjectHitboxDefinition left, ObjectHitboxDefinition right) => !left.Equals(right);
    }
}
