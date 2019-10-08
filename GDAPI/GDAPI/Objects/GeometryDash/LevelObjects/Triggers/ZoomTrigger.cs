using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.Interfaces;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Zoom trigger.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.Zoom)]
    public class ZoomTrigger : Trigger, IHasDuration
    {
        private short zoom;
        private float duration = 0.5f;

        /// <summary>The Object ID of the Zoom trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Zoom;

        /// <summary>The duration of the trigger's effect.</summary>
        [ObjectStringMappable(ObjectParameter.Duration, 0.5d)]
        public double Duration
        {
            get => duration;
            set => duration = (float)value;
        }
        /// <summary>The Zoom property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Zoom, 0)]
        public int Zoom
        {
            get => zoom;
            set => zoom = (short)value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="ZoomTrigger"/> class.</summary>
        public ZoomTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="ZoomTrigger"/> class.</summary>
        /// <param name="duration">The duration of the trigger.</param>
        /// <param name="zoom">The Zoom property of the trigger.</param>
        public ZoomTrigger(double duration, int zoom)
             : base()
        {
            Duration = duration;
            Zoom = zoom;
        }

        /// <summary>Returns a clone of this <seealso cref="ZoomTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ZoomTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as ZoomTrigger;
            c.duration = duration;
            c.zoom = zoom;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as ZoomTrigger;
            return base.EqualsInherited(other)
                && duration == z.duration
                && zoom == z.zoom;
        }
    }
}
