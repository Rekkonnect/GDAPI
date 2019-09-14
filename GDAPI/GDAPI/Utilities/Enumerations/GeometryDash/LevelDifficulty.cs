using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Utilities.Enumerations.GeometryDash
{
    /// <summary>Represents the difficulty of a level.</summary>
    public enum LevelDifficulty
    {
        /// <summary>Represents no difficulty. This value is not used anywhere in the game and perhaps defaults to <seealso cref="NA"/>.</summary>
        None,
        /// <summary>Represents the NA difficulty.</summary>
        NA,
        /// <summary>Represents the Easy difficulty.</summary>
        Easy,
        /// <summary>Represents the Normal difficulty.</summary>
        Normal,
        /// <summary>Represents the Hard difficulty.</summary>
        Hard,
        /// <summary>Represents the Harder difficulty.</summary>
        Harder,
        /// <summary>Represents the Insane difficulty.</summary>
        Insane,
        /// <summary>Represents the Easy Demon difficulty.</summary>
        EasyDemon,
        /// <summary>Represents the Medium Demon difficulty.</summary>
        MediumDemon,
        /// <summary>Represents the Hard Demon difficulty.</summary>
        HardDemon,
        /// <summary>Represents the Insane Demon difficulty.</summary>
        InsaneDemon,
        /// <summary>Represents the Extreme Demon difficulty.</summary>
        ExtremeDemon,
        /// <summary>Represents the Auto difficulty.</summary>
        Auto,
    }
}
