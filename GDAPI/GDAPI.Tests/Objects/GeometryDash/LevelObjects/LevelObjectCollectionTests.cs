using GDAPI.Enumerations.GeometryDash;
using GDAPI.Objects.GeometryDash.General;
using GDAPI.Objects.GeometryDash.LevelObjects;
using GDAPI.Objects.GeometryDash.LevelObjects.Triggers;
using NUnit.Framework;
using System.Linq;

namespace GDAPI.Tests.Objects.GeometryDash.LevelObjects
{
    public class LevelObjectCollectionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CommonProperties()
        {
            var objectTypes = typeof(GeneralObject).Assembly.GetTypes().Where(t => typeof(GeneralObject).IsAssignableFrom(t) && !t.IsAbstract).ToArray();
            var objectTypeDictionary = new GeneralObject.ObjectTypeInfoDictionary();
            foreach (var t in objectTypes.Select(t => GeneralObject.ObjectTypeInfo.GetInfo(t)).ToArray())
                if (t != null)
                    objectTypeDictionary.Add(t);

            var objects = new LevelObjectCollection();
            for (int i = 0; i < 45; i++)
                objects.Add(new GeneralObject(i, i * 30 + 15, 75));

            bool hasCommonID = objects.TryGetCommonPropertyWithID((int)ObjectProperty.ID, out int ID);
            bool hasCommonX = objects.TryGetCommonPropertyWithID((int)ObjectProperty.X, out double x);
            bool hasCommonY = objects.TryGetCommonPropertyWithID((int)ObjectProperty.Y, out double y);

            Assert.IsFalse(hasCommonID);
            Assert.IsFalse(hasCommonX);
            Assert.IsTrue(hasCommonY);

            objects.TrySetCommonPropertyWithID((int)ObjectProperty.ID, 4);
            objects.TrySetCommonPropertyWithID((int)ObjectProperty.X, 35.5);
            objects.TrySetCommonPropertyWithID((int)ObjectProperty.Y, 43.3);

            hasCommonID = objects.TryGetCommonPropertyWithID((int)ObjectProperty.ID, out ID);
            hasCommonX = objects.TryGetCommonPropertyWithID((int)ObjectProperty.X, out x);
            hasCommonY = objects.TryGetCommonPropertyWithID((int)ObjectProperty.Y, out y);

            Assert.IsTrue(hasCommonID);
            Assert.IsTrue(hasCommonX);
            Assert.IsTrue(hasCommonY);

            var firstMove = new MoveTrigger(2, 3);
            var secondMove = new MoveTrigger(2, 4);
            var alpha = new AlphaTrigger(3, 4, 0.5);

            objects.AddRange(firstMove, secondMove, alpha);
            
            bool hasCommonDuration = objects.TryGetCommonPropertyWithID((int)ObjectProperty.Duration, out double duration);
            bool hasCommonTargetGroupID = objects.TryGetCommonPropertyWithID((int)ObjectProperty.TargetGroupID, out int targetGroupID);

            Assert.IsFalse(hasCommonDuration);
            Assert.IsFalse(hasCommonTargetGroupID);

            objects.TrySetCommonPropertyWithID((int)ObjectProperty.Duration, 7d);
            objects.TrySetCommonPropertyWithID((int)ObjectProperty.TargetGroupID, 5);

            Assert.IsTrue(firstMove.Duration == 7);
            Assert.IsTrue(secondMove.Duration == 7);
            Assert.IsTrue(alpha.Duration == 7);

            Assert.IsTrue(firstMove.TargetGroupID == 5);
            Assert.IsTrue(secondMove.TargetGroupID == 5);
            Assert.IsTrue(alpha.TargetGroupID == 5);

            hasCommonDuration = objects.TryGetCommonPropertyWithID((int)ObjectProperty.Duration, out duration);
            hasCommonTargetGroupID = objects.TryGetCommonPropertyWithID((int)ObjectProperty.TargetGroupID, out targetGroupID);

            Assert.IsTrue(hasCommonDuration);
            Assert.IsTrue(hasCommonTargetGroupID);
        }
    }
}