using System;

namespace GDAPI.Attributes
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
