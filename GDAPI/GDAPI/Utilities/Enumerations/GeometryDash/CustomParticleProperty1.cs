using GDAPI.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>Provides values for custom particle property 1.</summary>
    [FutureProofing("2.2")]
    public enum CustomParticleProperty1 : byte
    {
        /// <summary>The Gravity property 1 type of the custom particle.</summary>
        Gravity = 0,
        /// <summary>The Radius property 1 type of the custom particle.</summary>
        Radius = 1,
    }
}
