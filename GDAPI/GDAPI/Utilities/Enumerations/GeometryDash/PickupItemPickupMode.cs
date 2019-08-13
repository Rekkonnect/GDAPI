using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>This enumeration provides values for the Pickup Mode of a Pickup item.</summary>
    public enum PickupItemPickupMode : byte
    {
        /// <summary>Represents the value of no selected mode.</summary>
        None = 0,
        /// <summary>Represents the value of the Pickup Item mode.</summary>
        PickupItemMode = 1,
        /// <summary>Represents the value of the Toggle Trigger mode.</summary>
        ToggleTriggerMode = 2
    }
}
