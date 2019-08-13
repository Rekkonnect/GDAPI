using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Alpha trigger.</summary>
    [ObjectID(TriggerType.Alpha)]
    public class AlphaTrigger : Trigger, IHasDuration, IHasTargetGroupID
    {
        private short targetGroupID;
        private float duration = 0.5f, opacity = 1;

        /// <summary>The Object ID of the Alpha trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Alpha;

        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectParameter.Duration, 0.5d)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Opacity property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Opacity, 1d)]
        public double Opacity
        {
            get => opacity;
            set => opacity = (float)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="AlphaTrigger"/> class.</summary>
        public AlphaTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="AlphaTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="opacity">The Opacity property of the trigger.</param>
        public AlphaTrigger(double duration, int targetGroupID, double opacity)
            : base()
        {
            Duration = duration;
            TargetGroupID = targetGroupID;
            Opacity = opacity;
        }

        /// <summary>Returns a clone of this <seealso cref="AlphaTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new AlphaTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as AlphaTrigger;
            c.duration = duration;
            c.targetGroupID = targetGroupID;
            c.opacity = opacity;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as AlphaTrigger;
            return base.EqualsInherited(other)
                && duration == z.duration
                && targetGroupID == z.targetGroupID
                && opacity == z.opacity;
        }
    }
}
