namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>This enumeration provides values for the Easing method of a movement command of a trigger.</summary>
    public enum EasingMethod : byte
    {
        /// <summary>Represents no easing method.</summary>
        None,
        /// <summary>Represents the Ease easing method.</summary>
        Ease,
        /// <summary>Represents the Elastic easing method.</summary>
        Elastic,
        /// <summary>Represents the Bounce easing method.</summary>
        Bounce,
        /// <summary>Represents the Exponential easing method.</summary>
        Exponential,
        /// <summary>Represents the Sine easing method.</summary>
        Sine,
        /// <summary>Represents the Back easing method.</summary>
        Back,
    }
}
