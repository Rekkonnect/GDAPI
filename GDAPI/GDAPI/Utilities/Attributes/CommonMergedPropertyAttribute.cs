using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAPI.Utilities.Attributes
{
    /// <summary>Represents that this property is going to be evaluated during level merging and will either be the value that's common among all levels, or the default.</summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class CommonMergedPropertyAttribute : Attribute
    {
        public object DefaultValue;

        public CommonMergedPropertyAttribute(object defaultValue = default)
        {
            DefaultValue = defaultValue;
        }
    }
}
