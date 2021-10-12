using GDAPI.Objects.GeometryDash.LevelObjects;
using System;

namespace GDAPI.Attributes
{
    /// <summary>Marks an object property as only useful to avoid handling too many exceptions when initializing a new <seealso cref="GeneralObject"/>.</summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ExceptionReductorAttribute : Attribute
    {
    }
}