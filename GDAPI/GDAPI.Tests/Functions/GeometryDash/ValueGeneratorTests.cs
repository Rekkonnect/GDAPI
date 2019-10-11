using System;
using GDAPI.Enumerations.GeometryDash;
using GDAPI.Functions.GeometryDash;
using NUnit.Framework;

namespace GDAPI.Tests.Functions.GeometryDash
{
    /// <summary>Provides helpful functions for generating values for several object properties that are enumerated.</summary>
    public class ValueGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region Z Layer
        [Test]
        public void GenerateEasing()
        {
            Assert.AreEqual(Easing.None, ValueGenerator.GenerateEasing(EasingMethod.None, true, true));
            Assert.AreEqual(Easing.EaseInOut, ValueGenerator.GenerateEasing(EasingMethod.Ease, true, true));
            Assert.AreEqual(Easing.EaseIn, ValueGenerator.GenerateEasing(EasingMethod.Ease, true, false));
            Assert.AreEqual(Easing.EaseOut, ValueGenerator.GenerateEasing(EasingMethod.Ease, false, true));
            Assert.AreEqual(Easing.ElasticInOut, ValueGenerator.GenerateEasing(EasingMethod.Elastic, true, true));
            Assert.AreEqual(Easing.ElasticIn, ValueGenerator.GenerateEasing(EasingMethod.Elastic, true, false));
            Assert.AreEqual(Easing.ElasticOut, ValueGenerator.GenerateEasing(EasingMethod.Elastic, false, true));
            Assert.AreEqual(Easing.BounceInOut, ValueGenerator.GenerateEasing(EasingMethod.Bounce, true, true));
            Assert.AreEqual(Easing.BounceIn, ValueGenerator.GenerateEasing(EasingMethod.Bounce, true, false));
            Assert.AreEqual(Easing.BounceOut, ValueGenerator.GenerateEasing(EasingMethod.Bounce, false, true));
            Assert.AreEqual(Easing.ExponentialInOut, ValueGenerator.GenerateEasing(EasingMethod.Exponential, true, true));
            Assert.AreEqual(Easing.ExponentialIn, ValueGenerator.GenerateEasing(EasingMethod.Exponential, true, false));
            Assert.AreEqual(Easing.ExponentialOut, ValueGenerator.GenerateEasing(EasingMethod.Exponential, false, true));
            Assert.AreEqual(Easing.SineInOut, ValueGenerator.GenerateEasing(EasingMethod.Sine, true, true));
            Assert.AreEqual(Easing.SineIn, ValueGenerator.GenerateEasing(EasingMethod.Sine, true, false));
            Assert.AreEqual(Easing.SineOut, ValueGenerator.GenerateEasing(EasingMethod.Sine, false, true));
            Assert.AreEqual(Easing.BackInOut, ValueGenerator.GenerateEasing(EasingMethod.Back, true, true));
            Assert.AreEqual(Easing.BackIn, ValueGenerator.GenerateEasing(EasingMethod.Back, true, false));
            Assert.AreEqual(Easing.BackOut, ValueGenerator.GenerateEasing(EasingMethod.Back, false, true));

            Assert.Catch(() => ValueGenerator.GenerateEasing(EasingMethod.Ease, false, false));
        }
        [Test]
        public void GenerateEasingType()
        {
            Assert.AreEqual(EasingType.None, ValueGenerator.GenerateEasingType(EasingMethod.None, true, true));
            Assert.AreEqual(EasingType.EaseInOut, ValueGenerator.GenerateEasingType(EasingMethod.Ease, true, true));
            Assert.AreEqual(EasingType.EaseIn, ValueGenerator.GenerateEasingType(EasingMethod.Ease, true, false));
            Assert.AreEqual(EasingType.EaseOut, ValueGenerator.GenerateEasingType(EasingMethod.Ease, false, true));
            Assert.AreEqual(EasingType.ElasticInOut, ValueGenerator.GenerateEasingType(EasingMethod.Elastic, true, true));
            Assert.AreEqual(EasingType.ElasticIn, ValueGenerator.GenerateEasingType(EasingMethod.Elastic, true, false));
            Assert.AreEqual(EasingType.ElasticOut, ValueGenerator.GenerateEasingType(EasingMethod.Elastic, false, true));
            Assert.AreEqual(EasingType.BounceInOut, ValueGenerator.GenerateEasingType(EasingMethod.Bounce, true, true));
            Assert.AreEqual(EasingType.BounceIn, ValueGenerator.GenerateEasingType(EasingMethod.Bounce, true, false));
            Assert.AreEqual(EasingType.BounceOut, ValueGenerator.GenerateEasingType(EasingMethod.Bounce, false, true));
            Assert.AreEqual(EasingType.ExponentialInOut, ValueGenerator.GenerateEasingType(EasingMethod.Exponential, true, true));
            Assert.AreEqual(EasingType.ExponentialIn, ValueGenerator.GenerateEasingType(EasingMethod.Exponential, true, false));
            Assert.AreEqual(EasingType.ExponentialOut, ValueGenerator.GenerateEasingType(EasingMethod.Exponential, false, true));
            Assert.AreEqual(EasingType.SineInOut, ValueGenerator.GenerateEasingType(EasingMethod.Sine, true, true));
            Assert.AreEqual(EasingType.SineIn, ValueGenerator.GenerateEasingType(EasingMethod.Sine, true, false));
            Assert.AreEqual(EasingType.SineOut, ValueGenerator.GenerateEasingType(EasingMethod.Sine, false, true));
            Assert.AreEqual(EasingType.BackInOut, ValueGenerator.GenerateEasingType(EasingMethod.Back, true, true));
            Assert.AreEqual(EasingType.BackIn, ValueGenerator.GenerateEasingType(EasingMethod.Back, true, false));
            Assert.AreEqual(EasingType.BackOut, ValueGenerator.GenerateEasingType(EasingMethod.Back, false, true));

            Assert.Catch(() => ValueGenerator.GenerateEasingType(EasingMethod.Ease, false, false));
        }
        [Test]
        public void GetEasingMethod()
        {
            Assert.AreEqual(EasingMethod.None, ValueGenerator.GetEasingMethod(Easing.None));
            Assert.AreEqual(EasingMethod.Ease, ValueGenerator.GetEasingMethod(Easing.EaseInOut));
            Assert.AreEqual(EasingMethod.Ease, ValueGenerator.GetEasingMethod(Easing.EaseIn));
            Assert.AreEqual(EasingMethod.Ease, ValueGenerator.GetEasingMethod(Easing.EaseOut));
            Assert.AreEqual(EasingMethod.Elastic, ValueGenerator.GetEasingMethod(Easing.ElasticInOut));
            Assert.AreEqual(EasingMethod.Elastic, ValueGenerator.GetEasingMethod(Easing.ElasticIn));
            Assert.AreEqual(EasingMethod.Elastic, ValueGenerator.GetEasingMethod(Easing.ElasticOut));
            Assert.AreEqual(EasingMethod.Bounce, ValueGenerator.GetEasingMethod(Easing.BounceInOut));
            Assert.AreEqual(EasingMethod.Bounce, ValueGenerator.GetEasingMethod(Easing.BounceIn));
            Assert.AreEqual(EasingMethod.Bounce, ValueGenerator.GetEasingMethod(Easing.BounceOut));
            Assert.AreEqual(EasingMethod.Exponential, ValueGenerator.GetEasingMethod(Easing.ExponentialInOut));
            Assert.AreEqual(EasingMethod.Exponential, ValueGenerator.GetEasingMethod(Easing.ExponentialIn));
            Assert.AreEqual(EasingMethod.Exponential, ValueGenerator.GetEasingMethod(Easing.ExponentialOut));
            Assert.AreEqual(EasingMethod.Sine, ValueGenerator.GetEasingMethod(Easing.SineInOut));
            Assert.AreEqual(EasingMethod.Sine, ValueGenerator.GetEasingMethod(Easing.SineIn));
            Assert.AreEqual(EasingMethod.Sine, ValueGenerator.GetEasingMethod(Easing.SineOut));
            Assert.AreEqual(EasingMethod.Back, ValueGenerator.GetEasingMethod(Easing.BackInOut));
            Assert.AreEqual(EasingMethod.Back, ValueGenerator.GetEasingMethod(Easing.BackIn));
            Assert.AreEqual(EasingMethod.Back, ValueGenerator.GetEasingMethod(Easing.BackOut));
        }
        [Test]
        public void HasEasingIn()
        {
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.EaseInOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.EaseIn));
            Assert.AreEqual(false, ValueGenerator.HasEasingIn(Easing.EaseOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.ElasticInOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.ElasticIn));
            Assert.AreEqual(false, ValueGenerator.HasEasingIn(Easing.ElasticOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.BounceInOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.BounceIn));
            Assert.AreEqual(false, ValueGenerator.HasEasingIn(Easing.BounceOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.ExponentialInOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.ExponentialIn));
            Assert.AreEqual(false, ValueGenerator.HasEasingIn(Easing.ExponentialOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.SineInOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.SineIn));
            Assert.AreEqual(false, ValueGenerator.HasEasingIn(Easing.SineOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.BackInOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingIn(Easing.BackIn));
            Assert.AreEqual(false, ValueGenerator.HasEasingIn(Easing.BackOut));
        }
        [Test]
        public void HasEasingOut()
        {
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.EaseInOut));
            Assert.AreEqual(false, ValueGenerator.HasEasingOut(Easing.EaseIn));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.EaseOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.ElasticInOut));
            Assert.AreEqual(false, ValueGenerator.HasEasingOut(Easing.ElasticIn));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.ElasticOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.BounceInOut));
            Assert.AreEqual(false, ValueGenerator.HasEasingOut(Easing.BounceIn));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.BounceOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.ExponentialInOut));
            Assert.AreEqual(false, ValueGenerator.HasEasingOut(Easing.ExponentialIn));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.ExponentialOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.SineInOut));
            Assert.AreEqual(false, ValueGenerator.HasEasingOut(Easing.SineIn));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.SineOut));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.BackInOut));
            Assert.AreEqual(false, ValueGenerator.HasEasingOut(Easing.BackIn));
            Assert.AreEqual(true, ValueGenerator.HasEasingOut(Easing.BackOut));
        }
        #endregion

        #region Z Layer
        [Test]
        public void GetAbsoluteZLayer()
        {
            Assert.AreEqual(3, ValueGenerator.GetAbsoluteZLayer(ZLayer.T3));
            Assert.AreEqual(2, ValueGenerator.GetAbsoluteZLayer(ZLayer.T2));
            Assert.AreEqual(1, ValueGenerator.GetAbsoluteZLayer(ZLayer.T1));
            Assert.AreEqual(-1, ValueGenerator.GetAbsoluteZLayer(ZLayer.B1));
            Assert.AreEqual(-2, ValueGenerator.GetAbsoluteZLayer(ZLayer.B2));
            Assert.AreEqual(-3, ValueGenerator.GetAbsoluteZLayer(ZLayer.B3));
            Assert.AreEqual(-4, ValueGenerator.GetAbsoluteZLayer(ZLayer.B4));
        }
        [Test]
        public void GetZLayerPosition()
        {
            Assert.AreEqual(ZLayerPosition.Top, ValueGenerator.GetZLayerPosition(ZLayer.T3));
            Assert.AreEqual(ZLayerPosition.Top, ValueGenerator.GetZLayerPosition(ZLayer.T2));
            Assert.AreEqual(ZLayerPosition.Top, ValueGenerator.GetZLayerPosition(ZLayer.T1));
            Assert.AreEqual(ZLayerPosition.Bottom, ValueGenerator.GetZLayerPosition(ZLayer.B1));
            Assert.AreEqual(ZLayerPosition.Bottom, ValueGenerator.GetZLayerPosition(ZLayer.B2));
            Assert.AreEqual(ZLayerPosition.Bottom, ValueGenerator.GetZLayerPosition(ZLayer.B3));
            Assert.AreEqual(ZLayerPosition.Bottom, ValueGenerator.GetZLayerPosition(ZLayer.B4));
        }
        [Test]
        public void GenerateZLayer()
        {
            Assert.AreEqual(ZLayer.T3, ValueGenerator.GenerateZLayer(ZLayerPosition.Top, 3));
            Assert.AreEqual(ZLayer.T2, ValueGenerator.GenerateZLayer(ZLayerPosition.Top, 2));
            Assert.AreEqual(ZLayer.T1, ValueGenerator.GenerateZLayer(ZLayerPosition.Top, 1));
            Assert.AreEqual(ZLayer.B1, ValueGenerator.GenerateZLayer(ZLayerPosition.Bottom, 1));
            Assert.AreEqual(ZLayer.B2, ValueGenerator.GenerateZLayer(ZLayerPosition.Bottom, 2));
            Assert.AreEqual(ZLayer.B3, ValueGenerator.GenerateZLayer(ZLayerPosition.Bottom, 3));
            Assert.AreEqual(ZLayer.B4, ValueGenerator.GenerateZLayer(ZLayerPosition.Bottom, 4));
        }
        #endregion
    }
}
