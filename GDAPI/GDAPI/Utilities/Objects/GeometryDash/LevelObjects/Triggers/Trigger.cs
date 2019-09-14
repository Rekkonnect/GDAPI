using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a trigger.</summary>
    public abstract class Trigger : ConstantIDObject
    {
        /// <summary>Contains the <seealso cref="bool"/> values of the trigger. Indices 0, 1, 2 are reserved for Touch Triggered, Spawn Triggered and Multi Trigger respectively, which means any inherited class may use indices [3, 7].</summary>
        protected BitArray8 TriggerBools = new BitArray8();
        
        /// <summary>The Touch Triggered property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TouchTriggered, false)]
        public bool TouchTriggered
        {
            get => TriggerBools[0];
            set => TriggerBools[0] = value;
        }
        /// <summary>The Spawn Triggered property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SpawnTriggered, false)]
        public bool SpawnTriggered
        {
            get => TriggerBools[1];
            set => TriggerBools[1] = value;
        }
        /// <summary>The Multi Trigger property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.MultiTrigger, false)]
        public bool MultiTrigger
        {
            get => TriggerBools[2];
            set => TriggerBools[2] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="Trigger"/> class.</summary>
        public Trigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="Trigger"/> class.</summary>
        /// <param name="touchTriggered">The Touch Triggered property of the trigger.</param>
        public Trigger(bool touchTriggered)
            : base()
        {
            TouchTriggered = touchTriggered;
        }
        /// <summary>Initializes a new instance of the <seealso cref="Trigger"/> class.</summary>
        /// <param name="spawnTriggered">The Spawn Triggered property of the trigger.</param>
        /// <param name="multiTrigger">The Multi Trigger property of the trigger.</param>
        public Trigger(bool spawnTriggered, bool multiTrigger)
            : base()
        {
            SpawnTriggered = spawnTriggered;
            MultiTrigger = multiTrigger;
        }
        
        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as Trigger;
            c.TriggerBools = TriggerBools;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Returns the name of the object type.</summary>
        /// <param name="lowerLastWord">Does not determine anything.</param>
        public override string GetObjectTypeName(bool lowerLastWord) => base.GetObjectTypeName(true);

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            return TriggerBools == (other as Trigger).TriggerBools
                && base.EqualsInherited(other);
        }
    }
}
