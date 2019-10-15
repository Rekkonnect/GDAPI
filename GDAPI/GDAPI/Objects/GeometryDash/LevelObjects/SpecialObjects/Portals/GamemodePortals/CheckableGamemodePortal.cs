using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals.GamemodePortals
{
    /// <summary>Represents a gamemode portal that has a checked property.</summary>
    public abstract class CheckableGamemodePortal : GamemodePortal, IHasCheckedProperty
    {
        /// <summary>The checked property of the ball portal that determines whether the borders of the player's gamemode will be shown or not.</summary>
        [ObjectStringMappable(ObjectProperty.SpecialObjectChecked, false)]
        public bool Checked
        {
            get => SpecialObjectBools[0];
            set => SpecialObjectBools[0] = value;
        }

        /// <summary>Initializes a new instance of the <seealso cref="CheckableGamemodePortal"/> class.</summary>
        public CheckableGamemodePortal() : base() { }
    }
}
