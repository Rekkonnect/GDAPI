using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Triggers.ColorTriggers
{
    /// <summary>Represents a 3DL Color trigger.</summary>
    [ObjectID(TriggerType.ThreeDL)]
    public class ThreeDLColorTrigger : SpecialColorTrigger
    {
        /// <summary>The Object ID of the 3DL Color trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.ThreeDL;
        
        /// <summary>The target Color ID of the trigger.</summary>
        public override int ConstantTargetColorID => (int)SpecialColorID.ThreeDL;

        /// <summary>Initializes a new instance of the <seealso cref="ThreeDLColorTrigger"/> class.</summary>
        public ThreeDLColorTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="ThreeDLColorTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="copyOpacity">The Copy Opacity property of the trigger.</param>
        /// <param name="tintGround">The Tint Ground property of the trigger.</param>
        public ThreeDLColorTrigger(float duration, bool copyOpacity = false, bool tintGround = false)
            : base(duration, copyOpacity, tintGround) { }

        /// <summary>Returns a clone of this <seealso cref="ThreeDLColorTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ThreeDLColorTrigger());
    }
}
