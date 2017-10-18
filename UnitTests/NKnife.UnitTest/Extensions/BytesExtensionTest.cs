using System;
using NKnife.Extensions;
using NUnit.Framework;

namespace NKnife.UnitTest.Extensions
{
    /// <summary>
    ///     BytesExtensionTest 的摘要说明
    /// </summary>
    [TestFixture]
    public class BytesExtensionTest
    {
        [Test]
        public void IndexOfTestMethod1()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {0x02, 0x03};
            int actual = source.Find(target);
            Assert.AreEqual(2, actual);
        }

        [Test]
        public void IndexOfTestMethod2()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {0x02, 0x04};
            int actual = source.Find(target);
            Assert.AreEqual(-1, actual);
        }

        [Test]
        public void IndexOfTestMethod3()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {};
            int actual = source.Find(target);
            Assert.AreEqual(-1, actual);
        }

        [Test]
        public void IndexOfTestMethod4()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target1 = new byte[] {0x00};
            int actual = source.Find(target1);
            Assert.AreEqual(0, actual);

            var target2 = new byte[] {0x09};
            actual = source.Find(target2);
            Assert.AreEqual(9, actual);
        }

        [Test]
        public void IndexOfTestMethod5()
        {
            var source = new byte[] {0x02, 0x03};
            var target = new byte[] {0x02, 0x03};
            int actual = source.Find(target);
            Assert.AreEqual(0, actual);
            actual = source.Find(source);
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void IndexOfTestMethod6()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {0x08, 0x09};
            int actual = source.Find(target);
            Assert.AreEqual(8, actual);
        }

        [Test]
        public void IndexOfTestMethod7()
        {
            var source = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            var target = new byte[] { 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            int actual = source.Find(target);
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void IndexOfTestMethod8()
        {
            var source = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            var target = new byte[] { 0x03, 0x04, 0x05, 0x06, 0x07, 0x09 };
            int actual = source.Find(target);
            Assert.AreEqual(-1, actual);
        }

        [Test]
        public void IndexOfTestMethod10()
        {
            var source = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x00, 0x01, 0x02, 0x03, 0x00, 0x01, 0x02, 0x03 };
            var target = new byte[] { 0x02, 0x03 };

            int actual = source.Find(target);
            Assert.AreEqual(2, actual);

            actual = source.Find(target, 2);
            Assert.AreEqual(2, actual);

            actual = source.Find(target, 3);
            Assert.AreEqual(6, actual);

            actual = source.Find(target, 6);
            Assert.AreEqual(6, actual);

            actual = source.Find(target, 7);
            Assert.AreEqual(10, actual);

            actual = source.Find(target, 10);
            Assert.AreEqual(10, actual);

            actual = source.Find(target, 11);
            Assert.AreEqual(-1, actual);
        }

    }
}