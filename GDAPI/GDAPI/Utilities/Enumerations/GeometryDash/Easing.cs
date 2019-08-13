using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>This enumeration provides values for the Easing of a movement command of a trigger.</summary>
    public enum Easing : byte
    {
        /// <summary>Represents no easing.</summary>
        None,
        /// <summary>Represents the Ease In Out easing.</summary>
        EaseInOut,
        /// <summary>Represents the Ease In easing.</summary>
        EaseIn,
        /// <summary>Represents the Ease Out easing.</summary>
        EaseOut,
        /// <summary>Represents the Elastic In Out easing.</summary>
        ElasticInOut,
        /// <summary>Represents the Elastic In easing.</summary>
        ElasticIn,
        /// <summary>Represents the Elastic Out easing.</summary>
        ElasticOut,
        /// <summary>Represents the Bounce In Out easing.</summary>
        BounceInOut,
        /// <summary>Represents the Bounce In easing.</summary>
        BounceIn,
        /// <summary>Represents the Bounce Out easing.</summary>
        BounceOut,
        /// <summary>Represents the Exponential In Out easing.</summary>
        ExponentialInOut,
        /// <summary>Represents the Exponential In easing.</summary>
        ExponentialIn,
        /// <summary>Represents the Exponential Out easing.</summary>
        ExponentialOut,
        /// <summary>Represents the Sine In Out easing.</summary>
        SineInOut,
        /// <summary>Represents the Sine In easing.</summary>
        SineIn,
        /// <summary>Represents the Sine Out easing.</summary>
        SineOut,
        /// <summary>Represents the Back In Out easing.</summary>
        BackInOut,
        /// <summary>Represents the Back In easing.</summary>
        BackIn,
        /// <summary>Represents the Back Out easing.</summary>
        BackOut
    }
}
