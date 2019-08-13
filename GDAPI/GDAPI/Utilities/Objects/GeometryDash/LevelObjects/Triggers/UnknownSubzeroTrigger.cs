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
    /// <summary>Represents a trigger found in Subzero whose functionality is unknown.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.UnknownSubzero)]
    public class UnknownSubzeroTrigger : Trigger
    {
        /// <summary>The Object ID of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.UnknownSubzero;

        /// <summary>Initializes a new instance of the <seealso cref="UnknownSubzeroTrigger"/> class.</summary>
        public UnknownSubzeroTrigger() : base() { }

        /// <summary>Returns a clone of this <seealso cref="UnknownSubzeroTrigger"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new UnknownSubzeroTrigger());
    }
}
