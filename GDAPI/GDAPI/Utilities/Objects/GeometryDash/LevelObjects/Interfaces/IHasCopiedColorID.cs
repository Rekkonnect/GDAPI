using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents an object which contains a definition for a copied Color ID.</summary>
    public interface IHasCopiedColorID
    {
        /// <summary>The copied Color ID of the object.</summary>
        [ObjectStringMappable(ObjectParameter.CopiedColorID)]
        int CopiedColorID { get; set; }
    }
}
