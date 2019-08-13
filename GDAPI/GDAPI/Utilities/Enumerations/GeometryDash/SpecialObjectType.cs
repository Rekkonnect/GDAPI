using GDAPI.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>Provides the object IDs of the special objects. Also contains values equal to -2 to declare that there are multiple objects of that special object type.</summary>
    public enum SpecialObjectType : short
    {
        /// <summary>The object ID of the collision block.</summary>
        CollisionBlock = 1816,
        /// <summary>The object ID of the count text object.</summary>
        CountTextObject = 1615,
        /// <summary>The object ID of the text object.</summary>
        TextObject = 914,
        /// <summary>The object ID of the custom particle.</summary>
        [FutureProofing("2.2")]
        CustomParticleObject = -10,

        // General special object types
        /// <summary>Represents a rotating object (not a particular one).</summary>
        RotatingObject = -2,
        /// <summary>Represents a pickup item (not a particular one).</summary>
        PickupItem = -2,
        /// <summary>Represents an orb (not a particular one).</summary>
        Orb = -2,
        /// <summary>Represents a pad (not a particular one).</summary>
        Pad = -2,
        /// <summary>Represents a portal (not a particular one).</summary>
        Portal = -2,
    }
}
