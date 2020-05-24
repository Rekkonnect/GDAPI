using GDAPI.Objects.General;
using NUnit.Framework;

namespace GDAPI.Tests.Objects.General
{
    public class BindableTests
    {
        [Test]
        public void Bindability()
        {
            var a = new Bindable<int>(2, 1);
            var b = new Bindable<int>(3, 4);

            a.BindTo(b);
            AssertBindableValues(2, 3);

            b.SetDefault();
            AssertBindableValues(4, 4);

            a.SetDefault();
            AssertBindableValues(1, 4);

            a.Value = 412;
            AssertBindableValues(412, 4);

            b.Value = 425;
            AssertBindableValues(425, 425);

            void AssertBindableValues(int va, int vb)
            {
                Assert.AreEqual(va, a.Value);
                Assert.AreEqual(vb, b.Value);
            }
        }
        [Test]
        public void CreateChildBindable()
        {
            var a = new Bindable<int>(2, 1)
            {
                IsBindable = false
            };
            var b = a.CreateChildBindable();

            Assert.AreEqual(a.Value, b.Value);
            Assert.AreEqual(a.DefaultValue, b.DefaultValue);
            Assert.IsFalse(a.IsBoundTo(b));
            Assert.IsTrue(b.IsBoundTo(a));
            Assert.IsFalse(a.IsBindable);
            Assert.IsTrue(b.IsBindable);
        }
        [Test]
        public void Copy()
        {
            var a = new Bindable<int>(2, 1)
            {
                IsBindable = false
            };
            var b = a.Copy();

            Assert.AreEqual(a.Value, b.Value);
            Assert.AreEqual(a.DefaultValue, b.DefaultValue);
            Assert.IsFalse(a.IsBoundTo(b));
            Assert.IsFalse(b.IsBoundTo(a));
            Assert.IsFalse(a.IsBindable);
            Assert.IsFalse(b.IsBindable);
        }
        [Test]
        public void CopyWithAllowedBindability()
        {
            var a = new Bindable<int>(2, 1)
            {
                IsBindable = false
            };
            var b = a.CopyWithAllowedBindability();

            Assert.AreEqual(a.Value, b.Value);
            Assert.AreEqual(a.DefaultValue, b.DefaultValue);
            Assert.IsFalse(a.IsBoundTo(b));
            Assert.IsFalse(b.IsBoundTo(a));
            Assert.IsFalse(a.IsBindable);
            Assert.IsTrue(b.IsBindable);
        }
        [Test]
        public void CopyBindableAndBindToThis()
        {
            AssertBindables(true);
            AssertBindables(false);

            void AssertBindables(bool isBindable)
            {
                var a = new Bindable<int>(2, 1)
                {
                    IsBindable = isBindable
                };
                var b = a.CopyBindableAndBindToThis();

                Assert.AreEqual(a.Value, b.Value);
                Assert.AreEqual(a.DefaultValue, b.DefaultValue);
                Assert.IsFalse(a.IsBoundTo(b));
                Assert.AreEqual(isBindable, b.IsBoundTo(a));
                Assert.AreEqual(a.IsBindable, b.IsBindable);
            }
        }
        [Test]
        public void CreateNewBindableBoundToThis()
        {
            var a = new Bindable<int>(2, 1)
            {
                IsBindable = false
            };
            var b = a.CreateNewBindableBoundToThis();

            // default(int) is necessary because the Assert class does not force object type equality
            Assert.AreEqual(default(int), b.Value);
            Assert.AreEqual(default(int), b.DefaultValue);
            Assert.IsFalse(a.IsBoundTo(b));
            Assert.IsTrue(b.IsBoundTo(a));
            Assert.IsFalse(a.IsBindable);
            Assert.IsTrue(b.IsBindable);
        }
    }
}
