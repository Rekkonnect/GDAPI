namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents an object which contains a property that can be considered the secondary ID, for any ID type (Group, Color, Item, Block).</summary>
    public interface IHasSecondaryID
    {
        /// <summary>The value of the secondary ID. Should only be overriden once in an interface, and nowhere else.</summary>
        int SecondaryID { get; set; }
    }
}