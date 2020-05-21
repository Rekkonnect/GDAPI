using GDAPI.Objects.GeometryDash.General;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.GeometryDash
{
    public class GuidelineCollectionTests
    {
        [Test]
        public void Stringification()
        {
            var guidelines = new GuidelineCollection();

            Assert.AreEqual("", guidelines.ToString());

            guidelines.Add(new Guideline(1.5, GuidelineColor.Green));
            Assert.AreEqual("1.5~1", guidelines.ToString());

            guidelines.AddRange(new Guideline[]
            {
                new Guideline(3.2, GuidelineColor.Yellow),
                new Guideline(4.7, GuidelineColor.Orange),
                new Guideline(6.0, GuidelineColor.Green),
                new Guideline(7.25, GuidelineColor.Green),
                new Guideline(8.112, GuidelineColor.Yellow),
            });
            Assert.AreEqual("1.5~1~3.2~0.9~4.7~1.1~6~1~7.25~1~8.112~0.9", guidelines.ToString());
        }
        [Test]
        public void Parse()
        {
            var matchedGuidelines = new Guideline[]
            {
                new Guideline(1.5, GuidelineColor.Green),
                new Guideline(3.2, GuidelineColor.Yellow),
                new Guideline(4.7, GuidelineColor.Orange),
                new Guideline(6.0, GuidelineColor.Green),
                new Guideline(7.25, GuidelineColor.Green),
                new Guideline(8.112, GuidelineColor.Yellow),
            };
            var collection = GuidelineCollection.Parse("1.5~1~3.2~0.9~4.7~1.1~6~1~7.25~1~8.112~0.9");

            for (int i = 0; i < matchedGuidelines.Length; i++)
                Assert.IsTrue(collection[i] == matchedGuidelines[i]);
        }
    }
}