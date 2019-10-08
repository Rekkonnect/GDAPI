using System;
using System.Collections.Generic;
using System.Text;

namespace GDAPI.Application.Editors.Delegates
{
    /// <summary>Represents a function that transforms a value.</summary>
    /// <typeparam name="TValue">The type of the value to transform.</typeparam>
    /// <param name="value">The value to transform.</param>
    public delegate TValue TransformationDelegate<TValue>(TValue value);
}
