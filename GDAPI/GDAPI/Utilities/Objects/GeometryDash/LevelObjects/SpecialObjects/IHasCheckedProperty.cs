using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects
{
    /// <summary>Represents a special object that has the checked property.</summary>
    public interface IHasCheckedProperty
    {
        /// <summary>Represents the Checked property of the special object.</summary>
        [ObjectStringMappable(ObjectParameter.SpecialObjectChecked)]
        bool Checked { get; set; }
    }
}
