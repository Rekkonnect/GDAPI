using System;

namespace GDAPI.Attributes
{
    /// <summary>Represents that a feature is only supported for future-proofing in the game, as suggested by teasers and trailers and may be subject to changes.</summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
    public sealed class FutureProofingAttribute : Attribute
    {
        /// <summary>The version of the expected version that the feature will be implemented in the game.</summary>
        public string Version;

        public FutureProofingAttribute(string version)
        {
            Version = version;
        }
    }
}