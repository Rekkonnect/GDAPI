using System;

namespace GDAPI.Attributes
{
    // This might not be necessary to expand further, but feel free to do so if need be
    /// <summary>Provides a generic type argument to an <seealso cref="Attribute"/>.</summary>
    /// <typeparam name="T">The type argument of the <seealso cref="Attribute"/>.</typeparam>
    public interface IGenericAttribute<T> { }
    /// <summary>Provides 2 generic type arguments to an <seealso cref="Attribute"/>.</summary>
    /// <typeparam name="T1">The first type argument of the <seealso cref="Attribute"/>.</typeparam>
    /// <typeparam name="T2">The second type argument of the <seealso cref="Attribute"/>.</typeparam>
    public interface IGenericAttribute<T1, T2> { }
}
