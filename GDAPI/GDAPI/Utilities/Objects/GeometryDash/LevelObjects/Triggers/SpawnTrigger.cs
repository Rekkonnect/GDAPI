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
    /// <summary>Represents a Spawn trigger.</summary>
    [ObjectID(TriggerType.Spawn)]
    public class SpawnTrigger : Trigger, IHasTargetGroupID
    {
        private short targetGroupID;
        private float delay;

        /// <summary>The Object ID of the Spawn trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Spawn;

        /// <summary>The target Group ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.TargetGroupID, 0)]
        public int TargetGroupID
        {
            get => targetGroupID;
            set => targetGroupID = (short)value;
        }
        /// <summary>The Delay property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.SpawnDelay, 0d)]
        public double Delay
        {
            get => delay;
            set => delay = (float)value;
        }
        /// <summary>The Editor Disable property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.EditorDisable, false)]
        public bool EditorDisable
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="SpawnTrigger"/> class.</summary>
        public SpawnTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="SpawnTrigger"/> class.</summary>
        /// <param name="targetGroupID">The target Group ID of the trigger.</param>
        /// <param name="delay">The Delay property of the trigger.</param>
        /// <param name="editorDisable">The Editor Disable property of the trigger.</param>
        public SpawnTrigger(int targetGroupID, double delay, bool editorDisable = false)
            : base()
        {
            TargetGroupID = targetGroupID;
            Delay = delay;
            EditorDisable = editorDisable;
        }

        /// <summary>Returns a clone of this <seealso cref="SpawnTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new SpawnTrigger());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as SpawnTrigger;
            c.targetGroupID = targetGroupID;
            c.delay = delay;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as SpawnTrigger;
            return base.EqualsInherited(other)
                && targetGroupID == z.targetGroupID
                && delay == z.delay;
        }
    }
}
