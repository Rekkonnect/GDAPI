using System.Linq;
using GDAPI.Functions.Extensions;
using NUnit.Framework;

namespace GDAPI.Tests.Functions.Extensions
{
    public class ObjectExtensionsTests
    {
        private readonly object testObject = new
        {
            field1 = true,
            field2 = false
        };
        
        [Test]
        public void TestGetPropertiesName()
        {
            var properties = testObject.GetPropertiesName();
            
            Assert.AreEqual("field1", properties[0]);
            Assert.AreEqual("field2", properties[1]);
        }

        [Test]
        public void TestToDictionary()
        {
            var dict = testObject.ToDictionary();
            
            Assert.IsTrue(dict.ContainsKey("field1"), "Dictionary did not include \"field1\".");
            Assert.IsTrue(dict.ContainsKey("field2"), "Dictionary did not include \"field2\".");
            
            Assert.AreEqual(true, dict["field1"]);
            Assert.AreEqual(false, dict["field2"]);
        }
    }
}