using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Triggers
{
    /// <summary>Represents a trigger found in Subzero whose functionality is unknown.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.UnknownSubzero)]
    public class UnknownSubzeroTrigger : Trigger
    {
        /// <summary>The Object ID of the trigger.</summary>
        public override int ConstantObjectID => (int)TriggerType.UnknownSubzero;

        /// <summary>Initializes a new instance of the <seealso cref="UnknownSubzeroTrigger"/> class.</summary>
        public UnknownSubzeroTrigger() : base() { }

        /// <summary>Returns a clone of this <seealso cref="UnknownSubzeroTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new UnknownSubzeroTrigger());
    }
}
