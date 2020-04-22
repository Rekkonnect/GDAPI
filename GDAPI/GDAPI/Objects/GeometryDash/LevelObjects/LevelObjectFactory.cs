using GDAPI.Attributes;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.LevelObjects.SpecialObjects;
using GDAPI.Objects.GeometryDash.Reflection;
using GDAPI.Objects.KeyedObjects;
using GDAPI.Objects.Reflection;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GDAPI.Objects.GeometryDash.LevelObjects
{
    public static class LevelObjectFactory
    {
        private static Type[] objectTypes;
        private static Dictionary<int, Type> propertyTypeInfo;
        private static readonly ObjectTypeInfoDictionary objectTypeDictionary;

        static LevelObjectFactory()
        {
            // Get object property types from the attributes that are assigned to the enum fields
            var fields = typeof(ObjectProperty).GetFields();
            // default(ObjectParameter) is necessary otherwise an exception will be thrown
            // Reflection really sucks dick when it comes to enums
            var info = fields.Select(i => new KeyValuePair<int, Type>((int)i.GetValue(default(ObjectProperty)), i.GetCustomAttribute<ObjectPropertyTypeAttribute>()?.Type));
            propertyTypeInfo = new Dictionary<int, Type>();
            foreach (var i in info)
                propertyTypeInfo.TryAdd(i.Key, i.Value);
            // I added those 5 lines of code only to realize that I actually do not necessarily need them, hopefully they'll turn out any useful in the near future:tm:
            // For the time being there is no reason to change them at all

            objectTypes = typeof(GeneralObject).Assembly.GetTypes().Where(t => typeof(GeneralObject).IsAssignableFrom(t)).ToArray();
            objectTypeDictionary = new ObjectTypeInfoDictionary();
            foreach (var t in objectTypes.Select(t => ObjectTypeInfo.GetInfo(t)))
                if (t != null)
                    objectTypeDictionary.Add((IFirstWideDoubleKeyedObject<int?, Type>)t);
        }

        /// <summary>Returns a new instance of the appropriate class of an object based on its object ID.</summary>
        /// <param name="objectID">The object ID of the new object.</param>
        public static GeneralObject GetNewObjectInstance(int objectID)
        {
            objectTypeDictionary.TryGetValue(objectID, out var t);
            if (t is ObjectTypeInfo i)
            {
                if (i.NonGeneratableAttribute != null)
                    throw new InvalidOperationException(i.NonGeneratableAttribute.ExceptionMessage);
                var instance = i.Constructor.Invoke(null) as GeneralObject;
                instance.ObjectID = (short)objectID; // Ensure the new object has the specified ID in case of a generic case
                return instance;
            }

            return new GeneralObject(objectID);
        }
        /// <summary>Returns a new instance of the appropriate class of an object based on its object ID.</summary>
        /// <param name="objectID">The object ID of the new object.</param>
        public static GeneralObject GetNewObjectInstance(TriggerType objectID) => GetNewObjectInstance((int)objectID);
        /// <summary>Returns a new instance of the appropriate class of an object based on its object ID.</summary>
        /// <param name="objectID">The object ID of the new object.</param>
        public static GeneralObject GetNewObjectInstance(SpecialObjectType objectID) => GetNewObjectInstance((int)objectID);
        /// <summary>Returns a new instance of the appropriate class of an object based on its object ID.</summary>
        /// <param name="objectID">The object ID of the new object.</param>
        public static GeneralObject GetNewObjectInstance(SpecialBlockType objectID) => GetNewObjectInstance((int)objectID);
        /// <summary>Returns a new instance of the appropriate class of an object based on its object ID.</summary>
        /// <param name="objectID">The object ID of the new object.</param>
        public static GeneralObject GetNewObjectInstance(OrbType objectID) => GetNewObjectInstance((int)objectID);
        /// <summary>Returns a new instance of the appropriate class of an object based on its object ID.</summary>
        /// <param name="objectID">The object ID of the new object.</param>
        public static GeneralObject GetNewObjectInstance(PadType objectID) => GetNewObjectInstance((int)objectID);
        /// <summary>Returns a new instance of the appropriate class of an object based on its object ID.</summary>
        /// <param name="objectID">The object ID of the new object.</param>
        public static GeneralObject GetNewObjectInstance(PortalType objectID) => GetNewObjectInstance((int)objectID);

        /// <summary>Gets the <seealso cref="ObjectTypeInfo"/> for the specified object ID.</summary>
        /// <param name="objectID">The object ID whose <seealso cref="ObjectTypeInfo"/> to get.</param>
        public static ObjectTypeInfo GetObjectTypeInfo(int objectID) => objectTypeDictionary[objectID];
        /// <summary>Gets the <seealso cref="ObjectTypeInfo"/> for the specified object type.</summary>
        /// <param name="objectType">The object type whose <seealso cref="ObjectTypeInfo"/> to get.</param>
        public static ObjectTypeInfo GetObjectTypeInfo(Type objectType) => objectTypeDictionary[objectType];

        /// <summary>Attempts to get the <seealso cref="ObjectTypeInfo"/> for the specified object ID.</summary>
        /// <param name="objectID">The object ID whose <seealso cref="ObjectTypeInfo"/> to get.</param>
        public static bool TryGetObjectTypeInfo(int objectID, out ObjectTypeInfo info)
        {
            bool result = objectTypeDictionary.TryGetValue(objectID, out var i);
            info = i as ObjectTypeInfo;
            return result;
        }
        /// <summary>Attempts to get the <seealso cref="ObjectTypeInfo"/> for the specified object type.</summary>
        /// <param name="objectType">The object type whose <seealso cref="ObjectTypeInfo"/> to get.</param>
        public static bool TryGetObjectTypeInfo(Type objectType, out ObjectTypeInfo info)
        {
            bool result = objectTypeDictionary.TryGetValue(objectType, out var i);
            info = i as ObjectTypeInfo;
            return result;
        }

        /// <summary>Returns the common properties found in the specified <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="collection">The collection whose common object properties will be evaluated and returned.</param>
        public static PropertyAccessInfoDictionary GetCommonProperties(IEnumerable<GeneralObject> collection) => GetCommonProperties(collection, null);
        /// <summary>Returns the common properties found in the specified <seealso cref="LevelObjectCollection"/> from a starting list of common properties.</summary>
        /// <param name="collection">The collection whose common object properties will be evaluated and returned.</param>
        /// <param name="startingDictionary">The starting dictionary of common properties that will be merged with the resulting list.</param>
        public static PropertyAccessInfoDictionary GetCommonProperties(IEnumerable<GeneralObject> collection, PropertyAccessInfoDictionary startingDictionary)
        {
            var objectTypes = GetCollectionObjectTypeInfo(collection);

            KeyedPropertyInfoDictionary<int?> lockedDictionary;
            PropertyAccessInfoDictionary result;
            if (collection != null)
                result = new PropertyAccessInfoDictionary(lockedDictionary = startingDictionary);
            else
                result = new PropertyAccessInfoDictionary(lockedDictionary = objectTypes.First().Properties);

            foreach (var t in objectTypes)
                foreach (var p in lockedDictionary)
                    if (!t.Properties.ContainsKey(p.Key))
                        result.Remove(p.Key);

            return result;
        }
        /// <summary>Returns all the available object properties found in the specified <seealso cref="LevelObjectCollection"/>.</summary>
        /// <param name="collection">The collection whose all available object properties will be evaluated and returned.</param>
        public static PropertyAccessInfoDictionary GetAllAvailableProperties(IEnumerable<GeneralObject> collection) => GetAllAvailableProperties(collection, null);
        /// <summary>Returns all the available object properties found in the specified <seealso cref="LevelObjectCollection"/> from a starting hash set of available properties.</summary>
        /// <param name="collection">The collection whose all available object properties will be evaluated and returned.</param>
        /// <param name="startingDictionary">The starting dictionary of available properties that will be merged with the resulting hash set.</param>
        public static PropertyAccessInfoDictionary GetAllAvailableProperties(IEnumerable<GeneralObject> collection, PropertyAccessInfoDictionary startingDictionary)
        {
            var objectTypes = GetCollectionObjectTypeInfo(collection);

            PropertyAccessInfoDictionary result;
            if (collection != null)
                result = new PropertyAccessInfoDictionary(startingDictionary);
            else
                result = new PropertyAccessInfoDictionary();

            foreach (var t in objectTypes)
                foreach (var p in t.Properties)
                    result.Add(p.Value as PropertyAccessInfo);

            return result;
        }
        private static HashSet<ObjectTypeInfo> GetCollectionObjectTypeInfo(IEnumerable<GeneralObject> collection)
        {
            return new HashSet<ObjectTypeInfo>(collection.Select(o => objectTypeDictionary[o.GetType()]));
        }

        public class ObjectTypeInfoDictionary : CachedTypeInfoDictionary<int?, int?>
        {
            public new ObjectTypeInfo this[int? key] => base[key] as ObjectTypeInfo;
            public new ObjectTypeInfo this[Type key] => base[key] as ObjectTypeInfo;
        }
        public class ObjectTypeInfo : FirstWideCachedTypeInfo<int?, int?>
        {
            private static Func<Type, Type, PropertyInfo, PropertyAccessInfo> GetAppropriateGenericA;
            private static Func<Type, PropertyInfo, PropertyAccessInfo> GetAppropriateGenericB;

            public NonGeneratableAttribute NonGeneratableAttribute { get; }

            public int?[] ObjectIDs { get; protected set; }

            public override int?[] Key1 => ObjectIDs;
            public override int? ConvertedKey => Key1 != null && Key1.Length > 0 ? Key1[0] : 0;

            static ObjectTypeInfo()
            {
                //GenerateSelfExecutingCode();
                // Uncomment this when System.CodeDom is referred
            }

            public ObjectTypeInfo(Type objectType)
                : base(objectType)
            {
                NonGeneratableAttribute = objectType.GetCustomAttribute<NonGeneratableAttribute>();
                InitializeObjectIDs();
            }

            protected virtual void InitializeObjectIDs()
            {
                ObjectIDs = ObjectType.GetCustomAttribute<ObjectIDsAttribute>()?.ObjectIDs.Cast<int?>().ToArray() ?? Array.Empty<int?>();
                if (typeof(SpecialObject).IsAssignableFrom(ObjectType) && !ObjectType.IsAbstract)
                {
                    var validObjectIDs = (int[])ObjectTypeProperties.Where(p => p.Name == "ValidObjectIDs").First().GetValue(Constructor.Invoke(null));
                    if (validObjectIDs != null)
                        ObjectIDs = validObjectIDs.Cast<int?>().ToArray();
                }
            }

            protected sealed override KeyedPropertyInfo<int?> CreateProperty(PropertyInfo p) => new PropertyAccessInfo(p);
            // TODO: Use the following line instead
            //protected sealed override PropertyAccessInfo CreateProperty(PropertyInfo p) => GetAppropriateGenericA?.Invoke(p.DeclaringType, p.PropertyType, p);

            protected virtual string GetValidObjectIDs()
            {
                var s = new StringBuilder();
                for (int i = 0; i < ObjectIDs.Length; i++)
                    s.Append($"{ObjectIDs[i]}, ");
                s.Remove(s.Length - 2, 2);
                return s.ToString();
            }

            public static ObjectTypeInfo GetInfo(Type objectType)
            {
                if (objectType.GetCustomAttribute<ObjectIDAttribute>() != null)
                    return new SingleObjectIDTypeInfo(objectType);
                return new ObjectTypeInfo(objectType);
            }

            private static void GenerateSelfExecutingCode()
            {
                const string className = "PropertyAccessInfoGenerator";
                const string methodAName = "GetAppropriateGenericA";
                const string methodBName = "GetAppropriateGenericB";

                var getAppropriateGenericACode = new StringBuilder();
                var getAppropriateGenericBCode = new StringBuilder();

                // With the current indentation, the produced source is badly formatted, but at least this looks better
                // In the following code, nameof and constants have been purposefully selectively used for "obvious" reasons
                var propertyTypes = new HashSet<Type>();
                foreach (var o in objectTypes)
                {
                    var types = o.GetProperties().Select(p => p.PropertyType);
                    foreach (var t in types)
                        propertyTypes.Add(t);
                    var fullName = o.FullName;
                    getAppropriateGenericACode.Append($@"
                    if (objectType == typeof({fullName}))
                        return {methodBName}<{fullName}>(propertyType, info);");
                }
                foreach (var p in propertyTypes)
                {
                    var fullName = p.FullName;
                    getAppropriateGenericBCode.Append($@"
                    if (propertyType == typeof({fullName}))
                        return new {nameof(PropertyAccessInfo)}<TO, {fullName}>(info);");
                }

                var allCode =
$@"
using System;
using System.Reflection;

namespace GDAPI.Special
{{
    public static partial class {className}
    {{
        public static {nameof(PropertyAccessInfo)} {methodAName}(Type objectType, Type propertyType, PropertyInfo info)
        {{
            {getAppropriateGenericACode}
            throw new Exception(); // Don't make compiler sad
        }}
        public static {nameof(PropertyAccessInfo)} {methodBName}<TO>(Type propertyType, PropertyInfo info)
        {{
            {getAppropriateGenericBCode}
            throw new Exception();
        }}
    }}
}}
";
                // Compile all code
                var provider = new CSharpCodeProvider();
                var options = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll", "System.dll", "System.CodeDom.dll" });
                var assembly = provider.CompileAssemblyFromSource(options, allCode).CompiledAssembly;
                var type = assembly.GetType(className);
                GetAppropriateGenericA = type.GetMethod(methodAName).CreateDelegate(typeof(Func<Type, Type, PropertyInfo, PropertyAccessInfo>)) as Func<Type, Type, PropertyInfo, PropertyAccessInfo>;
                GetAppropriateGenericB = type.GetMethod(methodBName).CreateDelegate(typeof(Func<Type, PropertyInfo, PropertyAccessInfo>)) as Func<Type, PropertyInfo, PropertyAccessInfo>; // maybe useless?
            }

            public override string ToString() => $"{GetValidObjectIDs()} - {ObjectType.Name}";
        }
        public class SingleObjectIDTypeInfo : ObjectTypeInfo
        {
            public int? ObjectID
            {
                get => ObjectIDs[0];
                private set => ObjectIDs[0] = value;
            }

            public SingleObjectIDTypeInfo(Type objectType) : base(objectType) { }

            protected override void InitializeObjectIDs()
            {
                ObjectIDs = new int?[1];
                ObjectID = ObjectType.GetCustomAttribute<ObjectIDAttribute>()?.ObjectID;
            }

            protected override string GetValidObjectIDs() => ObjectID.ToString();
        }
    }
}
