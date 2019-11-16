using GDAPI.Objects.GeometryDash.General;
using System;

namespace GDAPI.Attributes
{
    /// <summary>Marks an object property as only useful to avoid handling too many exceptions when initializing a new <seealso cref="LevelObject"/>.</summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class ExceptionReductorAttribute : Attribute
    {
    }
}