using GDAPI.Objects.GeometryDash.General;
using NUnit.Framework;
using System;

namespace GDAPI.Tests.Objects.GeometryDash
{
    public class GuidelineCollectionTests
    {
        private const string guidelineString = "1.5~1~3.2~0.9~4.7~0.8~6~1~7.25~1~8.112~0.9";

        [Test]
        public void Stringification()
        {
            var guidelines = new GuidelineCollection();

            Assert.AreEqual("", guidelines.ToString());

            guidelines.Add(new Guideline(1.5, GuidelineColor.Green));
            Assert.AreEqual("1.5~1", guidelines.ToString());

            guidelines.AddRange(new[]
            {
                new Guideline(3.2, GuidelineColor.Yellow),
                new Guideline(4.7, GuidelineColor.Orange),
                new Guideline(6.0, GuidelineColor.Green),
                new Guideline(7.25, GuidelineColor.Green),
                new Guideline(8.112, GuidelineColor.Yellow),
            });
            Assert.AreEqual(guidelineString, guidelines.ToString());
        }
        [Test]
        public void Parse()
        {
            var expectedGuidelines = new GuidelineCollection(new[]
            {
                new Guideline(1.5, GuidelineColor.Green),
                new Guideline(3.2, GuidelineColor.Yellow),
                new Guideline(4.7, GuidelineColor.Orange),
                new Guideline(6.0, GuidelineColor.Green),
                new Guideline(7.25, GuidelineColor.Green),
                new Guideline(8.112, GuidelineColor.Yellow),
            });

            var resultingGuidelines = GuidelineCollection.Parse(guidelineString);
            Assert.AreEqual(expectedGuidelines, resultingGuidelines);

            resultingGuidelines = GuidelineCollection.Parse($"{guidelineString}~");
            Assert.AreEqual(expectedGuidelines, resultingGuidelines);

            resultingGuidelines = GuidelineCollection.Parse($"~");
            Assert.AreEqual(Array.Empty<Guideline>(), resultingGuidelines);

            resultingGuidelines = GuidelineCollection.Parse($"");
            Assert.AreEqual(Array.Empty<Guideline>(), resultingGuidelines);
        }
    }
}