using GDAPI.Attributes;

namespace GDAPI.Enumerations.GeometryDash
{
    /// <summary>Provides values for custom particle grouping types.</summary>
    [FutureProofing("2.2")]
    public enum CustomParticleGrouping : byte
    {
        /// <summary>The Free grouping type of the custom particle.</summary>
        Free = 0,
        /// <summary>The Relative grouping type of the custom particle.</summary>
        Relative = 1,
        /// <summary>The Grouped grouping type of the custom particle.</summary>
        Grouped = 2,
    }
}
