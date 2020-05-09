namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents an object which contains a property that can be considered the primary ID, for any ID type (Group, Color, Item, Block).</summary>
    public interface IHasPrimaryID
    {
        /// <summary>The value of the primary ID. Should only be overriden once in an interface, once in an object class that implements this interface more than once, and nowhere else.</summary>
        int PrimaryID { get; set; }
    }
}