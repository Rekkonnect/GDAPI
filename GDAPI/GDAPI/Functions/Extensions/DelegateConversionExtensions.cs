using System;

namespace GDAPI.Functions.Extensions
{
    public static class DelegateConversionExtensions
    {
        public static Func<T, bool> ToFuncDelegate<T>(this Predicate<T> predicate) => new(predicate);
    }
}
