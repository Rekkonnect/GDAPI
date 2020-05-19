using GDAPI.Functions.Extensions;
using System.Reflection;

namespace GDAPI.Tests
{
    /// <summary>Contains information about the test resources.</summary>
    public static class TestResourceContainer
    {
        public static readonly Assembly TestsAssembly = typeof(TestResourceContainer).Assembly;
        public static readonly string TestsAssemblyName = TestsAssembly.GetName().Name;

        /// <summary>Gets the base resources directory.</summary>
        public static readonly string BaseResourcesDirectory = $@"{TestsAssembly.Location.SubstringUntil(TestsAssemblyName, true)}\Resources\";
        /// <summary>Gets the base resource strings directory.</summary>
        public static readonly string BaseResourceStringsDirectory = $@"{BaseResourcesDirectory}\ResourceStrings\";
    }
}
