namespace GDAPI.Enumerations.GeometryDash
{
    /// <summary>This enumeration provides values representing the length of a level.</summary>
    public enum LevelLength
    {
        /// <summary>Represents the Tiny length of a level. Levels with Tiny length are less than 10 seconds long.</summary>
        Tiny,
        /// <summary>Represents the Small length of a level. Levels with Small length are 10 to 29 seconds long.</summary>
        Small,
        /// <summary>Represents the Medium length of a level. Levels with Medium length are 30 to 59 seconds long.</summary>
        Medium,
        /// <summary>Represents the Long length of a level. Levels with Long length are 60 to 119 seconds long.</summary>
        Long,
        /// <summary>Represents the XL length of a level. Levels with XL length are at least 120 seconds long.</summary>
        XL
    }
}
