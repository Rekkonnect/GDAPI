using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a gamemode portal (a portal that changes the player's gamemode).</summary>
    public abstract class GamemodePortal : Portal
    {
        /// <summary>The gamemode the gamemode portal transforms the player into.</summary>
        public abstract Gamemode Gamemode { get; }

        /// <summary>Initializes a new instance of the <seealso cref="GamemodePortal"/> class.</summary>
        public GamemodePortal() : base() { }
    }
}
