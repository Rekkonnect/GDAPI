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
    /// <summary>Represents a Reverse trigger.</summary>
    [FutureProofing("2.2")]
    [ObjectID(TriggerType.Reverse)]
    public class ReverseTrigger : Trigger
    {
        [ObjectStringMappable(ObjectParameter.ID)]
        public override int ObjectID => (int)TriggerType.Reverse;

        /// <summary>The Reverse property of the trigger.</summary>
        [ObjectStringMappable(ObjectParameter.Reversed, false)]
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
