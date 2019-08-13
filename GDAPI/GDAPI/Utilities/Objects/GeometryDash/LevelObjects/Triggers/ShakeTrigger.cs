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
    /// <summary>Represents a Shake trigger.</summary>
    [ObjectID(TriggerType.Shake)]
    public class ShakeTrigger : Trigger, IHasDuration
    {
        private float duration = 0.5f, strength, interval;

        /// <summary>The Object ID of the Shake trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Shake;

        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectParameter.Duration)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The Strength property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Strength)]
        public double Strength
        {
            get => strength;
            set => strength = (float)value;
        }
        /// <summary>The Interval property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Interval)]
        public double Interval
        {
            get => interval;
            set => interval = (float)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="ShakeTrigger"/> class.</summary>
        public ShakeTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="ShakeTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="strength">The Strength property of the trigger.</param>
        /// <param name="interval">The Interval property of the trigger.</param>
        public ShakeTrigger(double duration, double strength, double interval)
            : base()
        {
            Duration = duration;
            Strength = strength;
            Interval = interval;
        }

        /// <summary>Returns a clone of this <seealso cref="ShakeTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ShakeTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as ShakeTrigger;
            c.duration = duration;
            c.strength = strength;
            c.interval = interval;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as ShakeTrigger;
            return base.EqualsInherited(other)
                && duration == z.duration
                && strength == z.strength
                && interval == z.interval;
        }
    }
}
