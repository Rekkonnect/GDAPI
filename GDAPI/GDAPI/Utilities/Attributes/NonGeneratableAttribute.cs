using System;
using GDAPI.Utilities.Objects.GeometryDash;

namespace GDAPI.Utilities.Attributes
{
    /// <summary>Marks a <seealso cref="LevelObject"/> that may not be explicitly generated in the editor.</summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class NonGeneratableAttribute : Attribute
    {
        /// <summary>The exception message to show upon attempting to generate that <seealso cref="LevelObject"/>.</summary>
        public string ExceptionMessage { get; }

        /// <summary>Creates a new instance of the <seealso cref="NonGeneratableAttribute"/> attribute.</summary>
        /// <param name="exceptionMessage">The exception message to show upon attempting to generate that <seealso cref="LevelObject"/>.</param>
        public NonGeneratableAttribute(string exceptionMessage = null)
        {
            ExceptionMessage = exceptionMessage;
        }
    }
}