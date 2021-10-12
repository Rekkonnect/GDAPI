using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a Reverse trigger.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.Reverse)]
    public class ReverseTrigger : Trigger
    {
        public override int ConstantObjectID => (int)TriggerType.Reverse;

        /// <summary>The Reverse property of the trigger.</summary>
        [ObjectStringMappable(ObjectProperty.Reversed, false)]
        public bool Reverse
        {
            get => TriggerBools[3];
            set => TriggerBools[3] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="ReverseTrigger"/> class.</summary>
        public ReverseTrigger() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="ReverseTrigger"/> class.</summary>
        /// <param name="reverse">The Reverse property of the trigger.</param>
        public ReverseTrigger(bool reverse = false)
             : base()
        {
            Reverse = reverse;
        }

        /// <summary>Returns a clone of this <seealso cref="ReverseTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new ReverseTrigger());
    }
}
