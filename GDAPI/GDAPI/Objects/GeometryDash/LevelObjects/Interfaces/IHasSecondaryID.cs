using GDAPI.Objects.GeometryDash.IDTypes;

namespace GDAPI.Objects.GeometryDash.LevelObjects.Interfaces
{
    /// <summary>Represents an object which contains a property that can be considered the secondary ID, for any ID type (Group, Color, Item, Block).</summary>
    public interface IHasSecondaryID { }
    /// <summary>Represents an object which contains a property that can be considered the secondary ID, for any ID type (Group, Color, Item, Block).</summary>
    /// <typeparam name="T">The type of the ID.</typeparam>
    public interface IHasSecondaryID<T> : IHasSecondaryID
        where T : IID
    {
        /// <summary>The ID value of the secondary ID.</summary>
        T SecondaryID { get; set; }
    }
}