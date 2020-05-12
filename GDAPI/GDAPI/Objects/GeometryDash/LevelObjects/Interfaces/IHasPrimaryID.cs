using GDAPI.Objects.GeometryDash.IDTypes;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents an object which contains a property that can be considered the primary ID, for any ID type (Group, Color, Item, Block).</summary>
    public interface IHasPrimaryID { }
    /// <summary>Represents an object which contains a property that can be considered the primary ID, for any ID type (Group, Color, Item, Block).</summary>
    /// <typeparam name="T">The type of the ID.</typeparam>
    public interface IHasPrimaryID<T> : IHasPrimaryID
        where T : IID
    {
        /// <summary>The ID value of the primary ID.</summary>
        T PrimaryID { get; set; }
    }
}