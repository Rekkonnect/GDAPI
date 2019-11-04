using GDAPI.Objects.DataStructures;
using GDAPI.Objects.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GDAPI.Objects.GeometryDash.Reflection
{
    /// <summary>Represents a dictionary of <seealso cref="PropertyAccessInfo"/> objects.</summary>
    public class PropertyAccessInfoDictionary : KeyedPropertyInfoDictionary<int?>
    {
        /// <summary>Initializes a new instance of the <seealso cref="PropertyAccessInfoDictionary"/> class.</summary>
        public PropertyAccessInfoDictionary() : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="PropertyAccessInfoDictionary"/> class.</summary>
        /// <param name="dictionary">The dictionary to initialize this dictionary out of.</param>
        public PropertyAccessInfoDictionary(KeyedPropertyInfoDictionary<int?> dictionary) : base(dictionary) { }
        /// <summary>Initializes a new instance of the <seealso cref="PropertyAccessInfoDictionary"/> class.</summary>
        /// <param name="dictionary">The dictionary to initialize this dictionary out of.</param>
        public PropertyAccessInfoDictionary(PropertyAccessInfoDictionary dictionary) : base(dictionary) { }

        /// <summary>Gets or sets the <seealso cref="PropertyAccessInfo"/> given a specified key.</summary>
        /// <param name="key">The key of the <seealso cref="PropertyAccessInfo"/> to get or set.</param>
        public new PropertyAccessInfo this[int? key]
        {
            get => base[key] as PropertyAccessInfo;
            set => base[key] = value;
        }
    }
}
