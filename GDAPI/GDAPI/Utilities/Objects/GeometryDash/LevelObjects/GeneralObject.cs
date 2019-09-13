using GDAPI.Utilities.Attributes;
using GDAPI.Utilities.Enumerations.GeometryDash;
using GDAPI.Utilities.Functions.Extensions;
using GDAPI.Utilities.Information.GeometryDash;
using GDAPI.Utilities.Objects.General;
using GDAPI.Utilities.Objects.GeometryDash.General;
using GDAPI.Utilities.Objects.GeometryDash.LevelObjects.SpecialObjects;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using static System.Convert;

namespace GDAPI.Utilities.Objects.GeometryDash.LevelObjects
{
    // TODO: Remove LevelObject entirely once completely migrated to this
    /// <summary>Represents a general object.</summary>
    public class GeneralObject
    {
        private static Type[] objectTypes;
        private static ObjectTypeInfo[] initializableObjectTypes;

        static GeneralObject()
        {
            objectTypes = typeof(GeneralObject).Assembly.GetTypes().Where(t => typeof(GeneralObject).IsAssignableFrom(t)).ToArray();
            initializableObjectTypes = objectTypes.Select(t => ObjectTypeInfo.GetInfo(t)).ToArray();
        }

        private short[] groupIDs = new short[0]; // Create a ComparableArray<T> class and use it instead
        private BitArray16 bools = new BitArray16();
        private short objectID, el1, el2, zLayer, zOrder, color1ID, color2ID;
        private float rotation, scaling = 1, transformationScalingX = 1, transformationScalingY = 1, transformationScalingCenterX, transformationScalingCenterY;

        // No default parameter values for the fundamental parameters of a level object
        /// <summary>The Object ID of this object.</summary>
        [ObjectStringMappable(ObjectParameter.ID)]
        public int ObjectID
        {
            get => objectID;
            set => objectID = (short)value;
        }
        /// <summary>The X position of this object.</summary>
        [ObjectStringMappable(ObjectParameter.X)]
        public double X { get; set; }
        /// <summary>The Y position of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Y)]
        public double Y { get; set; }
        /// <summary>Determines whether this object is flipped horizontally or not.</summary>
        [ObjectStringMappable(ObjectParameter.FlippedHorizontally, false)]
        public bool FlippedHorizontally
        {
            get => bools[0];
            set => bools[0] = value;
        }
        /// <summary>Determines whether this object is flipped vertically or not.</summary>
        [ObjectStringMappable(ObjectParameter.FlippedVertically, false)]
        public bool FlippedVertically
        {
            get => bools[1];
            set => bools[1] = value;
        }
        /// <summary>The rotation of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Rotation, 0)]
        public double Rotation
        {
            get => rotation;
            set
            {
                rotation = (float)value;
                if (rotation > 360)
                    rotation -= 360;
                else if (rotation < -360)
                    rotation += 360;
            }
        }
        /// <summary>The scaling of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Scaling, 1)]
        public double Scaling
        {
            get => scaling;
            set => scaling = (float)value;
        }
        /// <summary>The Editor Layer 1 of this object.</summary>
        [ObjectStringMappable(ObjectParameter.EL1, 0)]
        public int EL1
        {
            get => el1;
            set => el1 = (short)value;
        }
        /// <summary>The Editor Layer 2 of this object.</summary>
        [ObjectStringMappable(ObjectParameter.EL2, 0)]
        public int EL2
        {
            get => el2;
            set => el2 = (short)value;
        }
        /// <summary>The Z Layer of this object.</summary>
        [ObjectStringMappable(ObjectParameter.ZLayer, 0, true)]
        public int ZLayer
        {
            get => zLayer;
            set => zLayer = (short)value;
        }
        /// <summary>The Z Order of this object.</summary>
        [ObjectStringMappable(ObjectParameter.ZOrder, 0, true)]
        public int ZOrder
        {
            get => zOrder;
            set => zOrder = (short)value;
        }
        /// <summary>The Color 1 ID of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Color1, 0, true)]
        public int Color1ID
        {
            get => color1ID;
            set => color1ID = (short)value;
        }
        /// <summary>The Color 2 ID of this object.</summary>
        [ObjectStringMappable(ObjectParameter.Color2, 0, true)]
        public int Color2ID
        {
            get => color2ID;
            set => color2ID = (short)value;
        }
        /// <summary>The Group IDs of this object.</summary>
        [ObjectStringMappable(ObjectParameter.GroupIDs, new int[0])]
        public int[] GroupIDs
        {
            get => groupIDs.CastToInt();
            set => groupIDs = value.CastToShort();
        }
        /// <summary>The linked group ID of this object.</summary>
        [ObjectStringMappable(ObjectParameter.LinkedGroupID, 0)]
        public int LinkedGroupID { get; set; }
        /// <summary>Determines whether this object is the group parent or not.</summary>
        [ObjectStringMappable(ObjectParameter.GroupParent, false)]
        public bool GroupParent
        {
            get => bools[2];
            set => bools[2] = value;
        }
        /// <summary>Determines whether this object is for high detail or not.</summary>
        [ObjectStringMappable(ObjectParameter.HighDetail, false)]
        public bool HighDetail
        {
            get => bools[3];
            set => bools[3] = value;
        }
        /// <summary>Determines whether this object should have an entrance effect or not.</summary>
        [ObjectStringMappable(ObjectParameter.DontEnter, false)]
        public bool DontEnter
        {
            get => bools[4];
            set => bools[4] = value;
        }
        /// <summary>Determines whether this object should have the fade in and out disabled or not.</summary>
        [ObjectStringMappable(ObjectParameter.DontFade, false)]
        public bool DontFade
        {
            get => bools[5];
            set => bools[5] = value;
        }
        /// <summary>Determines whether this object should have its glow disabled or not.</summary>
        [ObjectStringMappable(ObjectParameter.DisableGlow, false)]
        public bool DisableGlow
        {
            get => bools[6];
            set => bools[6] = value;
        }

        /// <summary>Determines whether color 1 HSV is enabled.</summary>
        [ObjectStringMappable(ObjectParameter.Color1HSVEnabled, false)]
        public bool Color1HSVEnabled
        {
            get => bools[7];
            set => bools[7] = value;
        }
        /// <summary>The color 1 HSV values of the object (as a string for the gamesave).</summary>
        [ObjectStringMappable(ObjectParameter.Color1HSVValues, HSVAdjustment.DefaultHSVString)]
        public string Color1HSV
        {
            get => Color1HSVAdjustment.ToString();
            set => Color1HSVAdjustment = HSVAdjustment.Parse(value);
        }
        /// <summary>The color 1 HSV adjustment of the color 1 of the object.</summary>
        public HSVAdjustment Color1HSVAdjustment { get; set; } = new HSVAdjustment();

        /// <summary>Determines whether color 2 HSV is enabled.</summary>
        [ObjectStringMappable(ObjectParameter.Color2HSVEnabled, false)]
        public bool Color2HSVEnabled
        {
            get => bools[8];
            set => bools[8] = value;
        }
        /// <summary>The color 2 HSV values of the object (as a string for the gamesave).</summary>
        [ObjectStringMappable(ObjectParameter.Color2HSVValues, HSVAdjustment.DefaultHSVString)]
        public string Color2HSV
        {
            get => Color2HSVAdjustment.ToString();
            set => Color2HSVAdjustment = HSVAdjustment.Parse(value);
        }
        /// <summary>The color 2 HSV adjustment of the color 2 of the object.</summary>
        public HSVAdjustment Color2HSVAdjustment { get; set; } = new HSVAdjustment();

        /// <summary>Determines whether this object will have its effects disabled or not.</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.NoEffects, false)]
        public bool NoEffects
        {
            get => bools[9];
            set => bools[9] = value;
        }
        /// <summary>The Ice Block property of this object (probably for adventure mode).</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.IceBlock, false)]
        public bool IceBlock
        {
            get => bools[10];
            set => bools[10] = value;
        }
        /// <summary>The Non-Stick property of this object (probably for adventure mode).</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.NonStick, false)]
        public bool NonStick
        {
            get => bools[11];
            set => bools[11] = value;
        }
        /// <summary>The Unstuckable(?) property of this object (probably for adventure mode).</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.Unstuckable, false)]
        public bool Unstuckable
        {
            get => bools[12];
            set => bools[12] = value;
        }
        /// <summary>The [unreadable text 1] property of this object (probably for adventure mode).</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.UnreadableProperty1, false)]
        public bool UnreadableProperty1
        {
            get => bools[13];
            set => bools[13] = value;
        }
        /// <summary>The [unreadable text 2] property of this object (probably for adventure mode).</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.UnreadableProperty2, false)]
        public bool UnreadableProperty2
        {
            get => bools[14];
            set => bools[14] = value;
        }
        /// <summary>Unknown feawture with ID 36. Its only purpose is to avoid throwing exceptions when encountering this parameter, causing infinite performance costs.</summary>
        [ObjectStringMappable(ObjectParameter.UnknownFeature36, false)]
        public bool UnknownFeature36
        {
            get => bools[13];
            set => bools[13] = value;
        }
        /// <summary>The transformation scaling X property of this object.</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.TransformationScalingX, 1d)]
        public double TransformationScalingX
        {
            get => transformationScalingX;
            set => transformationScalingX = (float)value;
        }
        /// <summary>The transformation scaling Y property of this object.</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.TransformationScalingY, 1d)]
        public double TransformationScalingY
        {
            get => transformationScalingY;
            set => transformationScalingY = (float)value;
        }
        /// <summary>The transformation scaling center X property of this object.</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.TransformationScalingCenterX, 0d)] // Assumption
        public double TransformationScalingCenterX
        {
            get => transformationScalingCenterX;
            set => transformationScalingCenterX = (float)value;
        }
        /// <summary>The transformation scaling center Y property of this object.</summary>
        [FutureProofing("2.2")]
        [ObjectStringMappable(ObjectParameter.TransformationScalingCenterY, 0d)] // Assumption
        public double TransformationScalingCenterY
        {
            get => transformationScalingCenterY;
            set => transformationScalingCenterY = (float)value;
        }

        /// <summary>Gets or sets a <seealso cref="Point"/> instance with the location of the object.</summary>
        public Point Location
        {
            get => new Point(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        /// <summary>The rotation of this object in degrees according to math.</summary>
        public double MathRotationDegrees
        {
            get => -Rotation;
            set => Rotation = -value;
        }
        /// <summary>The rotation of this object in radians according to math.</summary>
        public double MathRotationRadians
        {
            get => MathRotationDegrees * Math.PI / 180;
            set => MathRotationDegrees = value * 180 / Math.PI;
        }
        
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class with the object ID parameter set to 1.</summary>
        public GeneralObject()
        {
            ObjectID = 1;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID)
        {
            ObjectID = objectID;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="x">The X position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="y">The Y position of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID, double x, double y)
        {
            ObjectID = objectID;
            X = x;
            Y = y;
        }
        /// <summary>Creates a new instance of the <seealso cref="GeneralObject"/> class.</summary>
        /// <param name="objectID">The object ID of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="x">The X position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="y">The Y position of this <seealso cref="GeneralObject"/>.</param>
        /// <param name="rotation">The rotation of this <seealso cref="GeneralObject"/>.</param>
        public GeneralObject(int objectID, double x, double y, double rotation)
        {
            ObjectID = objectID;
            X = x;
            Y = y;
            Rotation = rotation;
        }

        /// <summary>Returns a clone of this <seealso cref="GeneralObject"/>.</summary>
        public virtual GeneralObject Clone() => AddClonedInstanceInformation(new GeneralObject());

        /// <summary>Adds the cloned instance information and returns the cloned instance.</summary>
        /// <param name="cloned">The cloned instance to add the information to.</param>
        protected virtual GeneralObject AddClonedInstanceInformation(GeneralObject cloned)
        {
            cloned.objectID = objectID;
            cloned.X = X;
            cloned.Y = Y;
            cloned.bools = bools; // Prefer one assignment, compared to 7 property assignments
            cloned.rotation = rotation;
            cloned.scaling = scaling;
            cloned.el1 = el1;
            cloned.el2 = el2;
            cloned.zLayer = zLayer;
            cloned.zOrder = zOrder;
            cloned.color1ID = color1ID;
            cloned.color2ID = color2ID;
            cloned.groupIDs = groupIDs.CopyArray();
            cloned.LinkedGroupID = LinkedGroupID;
            cloned.transformationScalingX = transformationScalingX;
            cloned.transformationScalingY = transformationScalingY;
            cloned.transformationScalingCenterX = transformationScalingCenterX;
            cloned.transformationScalingCenterY = transformationScalingCenterY;
            cloned.Color1HSVAdjustment = Color1HSVAdjustment;
            cloned.Color2HSVAdjustment = Color2HSVAdjustment;
            return cloned;
        }

        // Reflection is FUN
        public T GetParameterWithID<T>(int ID)
        {
            var type = GetType();
            foreach (var t in initializableObjectTypes)
                if (t.ObjectType == type)
                    foreach (var p in t.Properties)
                        if (p.Key == ID)
                            return (T)p.Get(this);
            throw new KeyNotFoundException($"The parameter ID {ID} was not found in {type.Name} (ID: {ObjectID})");
        }
        public void SetParameterWithID<T>(int ID, T newValue)
        {
            var type = GetType();
            foreach (var t in initializableObjectTypes)
                if (t.ObjectType == type)
                    foreach (var p in t.Properties)
                        if (p.Key == ID)
                        {
                            p.Set(this, newValue);
                            return;
                        }
            throw new KeyNotFoundException($"The parameter ID {ID} was not found in {type.Name} (ID: {ObjectID}) / Value : {newValue}");
        }

        /// <summary>Adds a Group ID to the object's Group IDs if it does not already exist.</summary>
        /// <param name="ID">The Group ID to add.</param>
        public void AddGroupID(int ID)
        {
            if (groupIDs.Length < 10 && !groupIDs.Contains((short)ID))
                groupIDs = groupIDs.Append((short)ID);
        }
        /// <summary>Adds the specified Group IDs to the object's Group IDs. Only ones that do not already exist are added.</summary>
        /// <param name="IDs">The Group IDs to add.</param>
        public void AddGroupIDs(IEnumerable<short> IDs)
        {
            var l = IDs.ToList();
            for (int i = l.Count - 1; i >= 0; i--)
                if (groupIDs.Contains(l[i]))
                    l.RemoveAt(i);
            if (groupIDs.Length < 11 - l.Count)
                groupIDs = groupIDs.AppendRange(l.ToArray());
        }
        /// <summary>Removes a Group ID from the object's Group IDs.</summary>
        /// <param name="ID">The Group ID to remove.</param>
        public void RemoveGroupID(int ID)
        {
            int index = -1;
            bool found = false;
            for (int i = 0; i < groupIDs.Length && !found; i++)
                if (found = groupIDs[i] == ID)
                    index = i;
            if (index > -1)
                groupIDs = groupIDs.RemoveAt(index);
        }
        /// <summary>Removes the specified Group IDs from the object's Group IDs.</summary>
        /// <param name="IDs">The Group IDs to remove.</param>
        public void RemoveGroupIDs(IEnumerable<short> IDs)
        {
            var arrayIDs = IDs.ToArray();
            bool found = false;
            int newLength = groupIDs.Length;
            for (int i = 0; i < groupIDs.Length; i++, found = false)
                for (int j = 0; j < arrayIDs.Length && !found; j++)
                    if (found = groupIDs[i] == arrayIDs[j])
                        newLength += groupIDs[i] = -1;
            var result = new short[newLength];
            int a = 0;
            for (int i = 0; i < groupIDs.Length; i++)
                if (groupIDs[i] > -1)
                    result[a++] = groupIDs[i];
            groupIDs = result;
        }
        /// <summary>Returns the Group IDs of this object that match those from a provided <seealso cref="IEnumerable{T}"/>.</summary>
        /// <param name="IDs">The Group IDs to search in this object's Group IDs.</param>
        public IEnumerable<short> GetCommonGroupIDs(IEnumerable<short> IDs)
        {
            var list = new List<short>();
            var arrayIDs = IDs.ToArray();
            bool found = false;
            for (int i = 0; i < groupIDs.Length; i++, found = false)
                for (int j = 0; j < arrayIDs.Length && !found; j++)
                    if (found = groupIDs[i] == arrayIDs[j])
                        list.Add(arrayIDs[i]);
            return list;
        }
        /// <summary>Returns the Group IDs that are not in this object's Group IDs from a provided <seealso cref="IEnumerable{T}"/>.</summary>
        /// <param name="IDs">The Group IDs that will be filtered out.</param>
        public IEnumerable<short> ExcludeExistingGroupIDs(IEnumerable<short> IDs)
        {
            var list = IDs.ToList();
            bool found = false;
            for (int i = 0; i < groupIDs.Length; i++, found = false)
                for (int j = 0; j < list.Count && !found; j++)
                    if (found = groupIDs[i] == list[j])
                        list.RemoveAt(j);
            return list;
        }
        /// <summary>Gets a group ID of this object. This function exists because `GroupIDs[index]` is inefficient at a large scale.</summary>
        /// <param name="index">The index of the group ID to get.</param>
        public int GetGroupID(int index) => groupIDs[index];
        /// <summary>Sets a group ID of this object. This function exists because `GroupIDs[index] = groupID` will not work.</summary>
        /// <param name="index">The index of the group ID to set.</param>
        /// <param name="groupID">The group ID to set.</param>
        public void SetGroupID(int index, int groupID) => groupIDs[index] = (short)groupID;
        /// <summary>Adjusts a group ID of this object. This function exists because `GroupIDs[index] += groupIDAdjustment` will not work.</summary>
        /// <param name="index">The index of the group ID to adjust.</param>
        /// <param name="groupIDAdjustment">The group ID adjustment to apply.</param>
        public void AdjustGroupID(int index, int groupIDAdjustment) => groupIDs[index] += (short)groupIDAdjustment;

        /// <summary>Determines whether the object's location is within a rectangle.</summary>
        /// <param name="startingX">The starting X position of the rectangle.</param>
        /// <param name="startingY">The starting Y position of the rectangle.</param>
        /// <param name="endingX">The ending X position of the rectangle.</param>
        /// <param name="endingY">The ending Y position of the rectangle.</param>
        public bool IsWithinRange(double startingX, double startingY, double endingX, double endingY) => startingX <= X && endingX >= X && startingY <= Y && endingY >= Y;

        /// <summary>Returns the name of the object type.</summary>
        /// <param name="lowerLastWord">Determines whether the last word will be converted to lower case.</param>
        public virtual string GetObjectTypeName(bool lowerLastWord)
        {
            // TODO: Consider caching the object type names?
            var words = GetType().Name.GetPascalCaseWords();
            if (lowerLastWord)
                words[words.Length - 1] = words[words.Length - 1].ToLower();
            return words.Combine(" ");
        }

        /// <summary>Determines whether this object equals another object's properties; has to be <see langword="override"/>n in every object and every <see langword="override"/> should call its parent function first before determining its own <see langword="override"/>n result. That means an <see langword="override"/> should look like <see langword="return"/> <see langword="base"/>.EqualsInherited(<paramref name="other"/>) &amp;&amp; ...;.</summary>
        /// <param name="other">The other object to check whether it equals this object's properties.</param>
        protected virtual bool EqualsInherited(GeneralObject other)
        {
            // Seriously I do not like how this looks, but at least there is some symmetry, which is the most I could think of
            return objectID == other.objectID
                && bools == other.bools
                && color1ID == other.color1ID
                && color2ID == other.color2ID
                && el1 == other.el1
                && el2 == other.el2
                && groupIDs.EqualsUnordered(other.groupIDs)
                && X == other.X
                && Y == other.Y
                && rotation == other.rotation
                && scaling == other.scaling
                && LinkedGroupID == other.LinkedGroupID
                && zLayer == other.zLayer
                && ZOrder == other.ZOrder
                && Color1HSVAdjustment == other.Color1HSVAdjustment
                && Color2HSVAdjustment == other.Color2HSVAdjustment;
        }
        /// <summary>Determines whether this <seealso cref="GeneralObject"/> equals another <seealso cref="GeneralObject"/>.</summary>
        /// <param name="other">The other <seealso cref="GeneralObject"/> to check equality against.</param>
        public bool Equals(GeneralObject other) => EqualsInherited(other);
        /// <summary>Determines whether this object equals another object. Not recommended using at all due to performance issues and overgeneralization of the implementation.</summary>
        /// <param name="obj">The other object to check equality against.</param>
        public override bool Equals(object obj) => Equals(obj as GeneralObject);

        public static bool operator ==(GeneralObject left, GeneralObject right) => left.Equals(right);
        public static bool operator !=(GeneralObject left, GeneralObject right) => !left.Equals(right);

        /// <summary>Returns a new instance of the appropriate class of an object based on its object ID.</summary>
        /// <param name="objectID">The object ID of the new object.</param>
        public static GeneralObject GetNewObjectInstance(int objectID)
        {
            foreach (var t in initializableObjectTypes)
                if (t.IsValidID(objectID))
                {
                    if (t.NonGeneratableAttribute != null)
                        throw new InvalidOperationException(t.NonGeneratableAttribute.ExceptionMessage);
                    return t.Constructor.Invoke(null) as GeneralObject;
                }

            if (ObjectLists.RotatingObjectList.Contains(objectID))
                return new RotatingObject(objectID);
            if (ObjectLists.AnimatedObjectList.Contains(objectID))
                return new AnimatedObject(objectID);
            if (ObjectLists.PickupItemList.Contains(objectID))
                return new PickupItem(objectID);
            if (ObjectLists.PulsatingObjectList.Contains(objectID))
                return new PulsatingObject(objectID);

            return new GeneralObject(objectID);
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            var properties = GetType().GetProperties();
            foreach (var p in properties)
            {
                // TODO: Optimize this shit
                var mappableAttribute = (ObjectStringMappableAttribute)p.GetCustomAttributes(typeof(ObjectStringMappableAttribute), false).FirstOrDefault();
                int? key = mappableAttribute?.Key;
                object defaultValue = mappableAttribute?.DefaultValue;
                object value = p.GetValue(this);
                if (key != null && key > 0 && value != defaultValue)
                    s.Append($"{key},{GetAppropriateStringRepresentation(p.GetValue(this))},");
            }
            s.Remove(s.Length - 1, 1); // Remove last comma
            return s.ToString();
        }

        // I swear to fucking goodness I made this at 4:30 AM, this will be redone somewhere else it's just a temporary fix so that we release please have mercy
        // During the writing of this function a lot of WHEEZEs dropped
        private string GetAppropriateStringRepresentation(object thing)
        {
            switch (thing)
            {
                case bool b:
                    return ToInt32(b).ToString();
                case int[] a:
                    var s = new StringBuilder();
                    for (int i = 0; i < a.Length; i++)
                        s.Append($"{a[i]}.");
                    if (s.Length > 0)
                        s.Remove(s.Length - 1, 1);
                    return s.ToString();
                case Enum _:
                    return ToInt32(thing).ToString();
                // Please tell me there are no more things that break
            }
            return thing.ToString();
        }

        private abstract class ObjectTypeInfo
        {
            private static Func<Type, Type, PropertyInfo, PropertyAccessInfo> GetAppropriateGenericA;
            private static Func<Type, PropertyInfo, PropertyAccessInfo> GetAppropriateGenericB;

            public Type ObjectType { get; }

            public ConstructorInfo Constructor { get; }
            public NonGeneratableAttribute NonGeneratableAttribute { get; }
            public PropertyAccessInfo[] Properties { get; }

            static ObjectTypeInfo()
            {
                //GenerateSelfExecutingCode();
                // Uncomment this when System.CodeDom is referred
            }

            public ObjectTypeInfo(Type objectType)
            {
                ObjectType = objectType;
                Constructor = objectType.GetConstructor(Type.EmptyTypes);
                NonGeneratableAttribute = objectType.GetCustomAttribute<NonGeneratableAttribute>();
                var properties = objectType.GetProperties();
                Properties = new PropertyAccessInfo[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    var p = properties[i];
                    Properties[i] = new PropertyAccessInfo(p);
                    // TODO: Use the following line instad
                    //Properties[i] = GetAppropriateGenericA?.Invoke(p.DeclaringType, p.PropertyType, p);
                }
            }

            public abstract bool IsValidID(int objectID);

            protected abstract string GetValidObjectIDs();

            public static ObjectTypeInfo GetInfo(Type objectType)
            {
                if (objectType.GetCustomAttribute<ObjectIDsAttribute>() != null)
                    return new MultiObjectIDTypeInfo(objectType);
                return new SingleObjectIDTypeInfo(objectType);
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
        private class SingleObjectIDTypeInfo : ObjectTypeInfo
        {
            public int? ObjectID { get; }

            public SingleObjectIDTypeInfo(Type objectType)
                : base(objectType)
            {
                ObjectID = objectType.GetCustomAttribute<ObjectIDAttribute>()?.ObjectID;
            }

            public override bool IsValidID(int objectID) => objectID == ObjectID;

            protected override string GetValidObjectIDs() => ObjectID.ToString();
        }
        private class MultiObjectIDTypeInfo : ObjectTypeInfo
        {
            public int[] ObjectIDs { get; }

            public MultiObjectIDTypeInfo(Type objectType)
                : base(objectType)
            {
                ObjectIDs = objectType.GetCustomAttribute<ObjectIDsAttribute>()?.ObjectIDs;
            }

            public override bool IsValidID(int objectID) => ObjectIDs?.Contains(objectID) ?? false;

            protected override string GetValidObjectIDs()
            {
                var s = new StringBuilder();
                for (int i = 0; i < ObjectIDs.Length; i++)
                    s.Append($"{ObjectIDs[i]}, ");
                s.Remove(s.Length - 2, 2);
                return s.ToString();
            }
        }

        // The following TODOs contain instructions to be executed when self-compiling code can be made

        // TODO: Make this abstract
        private class PropertyAccessInfo
        {
            public PropertyInfo PropertyInfo { get; }

            protected Type GenericFunc, GenericAction;

            // TODO: Remove these
            public Delegate GetMethodDelegate { get; }
            public Delegate SetMethodDelegate { get; }

            public ObjectStringMappableAttribute ObjectStringMappableAttribute { get; }
            public int? Key => ObjectStringMappableAttribute?.Key;
            
            public PropertyAccessInfo(PropertyInfo info)
            {
                PropertyInfo = info;
                // You have to be kidding me right now
                var propertyType = info.PropertyType;
                var objectType = info.DeclaringType;
                GenericFunc = typeof(Func<,>).MakeGenericType(objectType, propertyType);
                GenericAction = typeof(Action<,>).MakeGenericType(objectType, propertyType);
                ObjectStringMappableAttribute = info.GetCustomAttribute<ObjectStringMappableAttribute>();

                // TODO: Remove these
                GetMethodDelegate = info.GetGetMethod().CreateDelegate(GenericFunc);
                SetMethodDelegate = info.GetSetMethod()?.CreateDelegate(GenericAction);
            }

            // TODO: Make these abstract
            public virtual object Get(object instance) => GetMethodDelegate?.DynamicInvoke(instance);
            public virtual void Set(object instance, object newValue) => SetMethodDelegate?.DynamicInvoke(instance, newValue);
        }
        private class PropertyAccessInfo<TObject, TProperty> : PropertyAccessInfo
        {
            public Func<TObject, TProperty> GetMethod { get; }
            public Action<TObject, TProperty> SetMethod { get; }

            public PropertyAccessInfo(PropertyInfo info)
                : base(info)
            {
                GetMethod = info.GetGetMethod().CreateDelegate(GenericFunc) as Func<TObject, TProperty>;
                SetMethod = info.GetSetMethod()?.CreateDelegate(GenericAction) as Action<TObject, TProperty>;
            }

            public override object Get(object instance) => GetMethod((TObject)instance);
            public override void Set(object instance, object newValue) => SetMethod?.Invoke((TObject)instance, (TProperty)newValue);
        }
    }
}
