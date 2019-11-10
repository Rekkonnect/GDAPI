using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;

namespace GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects.Portals
{
    /// <summary>Represents a blue teleportation portal.</summary>
    [ObjectID(PortalType.BlueTeleportation)]
    public class BlueTeleportationPortal : Portal
    {
        private YellowTeleportationPortal linkedYellowTeleportationPortal;

        /// <summary>The <seealso cref="YellowTeleportationPortal"/> that this <seealso cref="BlueTeleportationPortal"/> is linked to.</summary>
        public YellowTeleportationPortal LinkedYellowTeleportationPortal
        {
            get
            {
                if (linkedYellowTeleportationPortal == null)
                    linkedYellowTeleportationPortal = new YellowTeleportationPortal(this);
                return linkedYellowTeleportationPortal;
            }
            private set => linkedYellowTeleportationPortal = value;
        }

        /// <summary>The object ID of the blue teleportation portal.</summary>
        [ObjectStringMappable(ObjectProperty.ObjectID)]
        public override int ObjectID => (int)PortalType.BlueTeleportation;

        /// <summary>The distance of the Y location between the yellow and this teleportation portals.</summary>
        [ObjectStringMappable(ObjectProperty.YellowTeleportationPortalDistance)]
        public double YellowTeleportationPortalDistance { get; set; }

        /// <summary>Initializes a new instance of the <seealso cref="BlueTeleportationPortal"/> class.</summary>
        public BlueTeleportationPortal() : base() { }

        /// <summary>Returns a clone of this <seealso cref="BlueTeleportationPortal"/>.</summary>
        public override GeneralObject Clone() => AddClonedInstanceInformation(new BlueTeleportationPortal());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected override GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            var c = cloned as BlueTeleportationPortal;
            c.LinkedYellowTeleportationPortal = LinkedYellowTeleportationPortal;
            c.YellowTeleportationPortalDistance = YellowTeleportationPortalDistance;
            return base.AddClonedInstanceInformation(c);
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected override bool EqualsInherited(GeneralObject other)
        {
            var z = other as BlueTeleportationPortal;
            return base.EqualsInherited(other)
                && YellowTeleportationPortalDistance == z.YellowTeleportationPortalDistance
                && LinkedYellowTeleportationPortal == z.LinkedYellowTeleportationPortal;
        }
    }
}
