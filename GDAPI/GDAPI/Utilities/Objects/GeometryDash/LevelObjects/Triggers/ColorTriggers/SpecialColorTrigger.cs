using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a Color trigger that has a specific target Color ID.</summary>
    public abstract class SpecialColorTrigger : ColorTrigger
    {
        /// <summary>Determines whether this color trigger has a constant target Color ID.</summary>
        public override bool HasConstantTargetColorID => true;
        /// <summary>The target Color ID of the trigger.</summary>
        public abstract int ConstantTargetColorID { get; }

        /// <summary>Initializes a new instance of the <seealso cref="SpecialColorTrigger"/> class.</summary>
        public SpecialColorTrigger()
            : base()
        {
            SetTargetColorID(ConstantTargetColorID);
        }
        /// <summary>Initializes a new instance of the <seealso cref="SpecialColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public SpecialColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, 0, copyOpacity, tintGround)
        {
            SetTargetColorID(ConstantTargetColorID);
        }
    }
}
