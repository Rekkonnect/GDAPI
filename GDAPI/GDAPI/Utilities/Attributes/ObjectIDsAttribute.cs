using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects.Triggers;
using System;

namespace GDAPI.Utilities.Attributes
{
    /// <summary>Contains the object ID of the <seealso cref="LevelObject"/> class.</summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ObjectIDsAttribute : Attribute
    {
        /// <summary>The object IDs of the <seealso cref="LevelObject"/>.</summary>
        public int[] ObjectIDs { get; }

        /// <summary>Creates a new instance of the <seealso cref="ObjectIDsAttribute"/> attribute.</summary>
        /// <param name="objectID">The object ID of the <seealso cref="LevelObject"/>.</param>
        public ObjectIDsAttribute(params int[] objectIDs)
        {
            ObjectIDs = objectIDs;
        }
        /// <summary>Creates a new instance of the <seealso cref="ObjectIDsAttribute"/> attribute.</summary>
        /// <param name="objectID">The object ID of the <seealso cref="SpecialObject"/>.</param>
        public ObjectIDsAttribute(params SpecialObjectType[] objectIDs) : this(ToIntArray(objectIDs)) { }
        /// <summary>Creates a new instance of the <seealso cref="ObjectIDsAttribute"/> attribute.</summary>
        /// <param name="objectID">The object ID of the <seealso cref="Trigger"/>.</param>
        public ObjectIDsAttribute(params TriggerType[] objectIDs) : this(ToIntArray(objectIDs)) { }

        // This does not put a smile in my face.
        private static int[] ToIntArray(params SpecialObjectType[] values)
        {
            int[] result = new int[values.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = (int)values[i];
            return result;
        }
        private static int[] ToIntArray(params TriggerType[] values)
        {
            int[] result = new int[values.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = (int)values[i];
            return result;
        }
    }
}