using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocketKnife.Generic;

namespace NKnife.Socket.UnitTest
{
    [TestClass]
    public class KnifeSocketFilterTest2
    {
        #region 附加测试特性
        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext { get; set; }

        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CompareTestMethod01()
        {
            var data = new byte[] { 0xAB };
            var toCompare = new byte[] { 0xAB };
            var filter = new TestKnifeSocketFilter();
            Assert.AreEqual(true, filter.TestCompare(ref data, toCompare));
            Assert.AreEqual(1, data.Length);
            Assert.AreEqual(0xAB, data[0]);
        }

        [TestMethod]
        public void CompareTestMethod02()
        {
            var data = new byte[] { 0xAB };
            var toCompare = new byte[] { 0x00 };
            var filter = new TestKnifeSocketFilter();
            Assert.AreEqual(false, filter.TestCompare(ref data, toCompare));
            Assert.AreEqual(1, data.Length);
            Assert.AreEqual(0xAB, data[0]);
        }

        [TestMethod]
        public void CompareTestMethod03()
        {
            var data = new byte[2048];
            var toCompare = new byte[2048];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte) i;
                toCompare[i] = (byte) i;
            }
            var filter = new TestKnifeSocketFilter();
            Assert.AreEqual(true, filter.TestCompare(ref data, toCompare));
            Assert.AreEqual(data.Length, toCompare.Length);
        }

        [TestMethod]
        public void CompareTestMethod1()
        {
            var data = new byte[] { 0x00, 0x11, 0x12, 0x13, 0x14, 0x15 };
            var toCompare = new byte[] { 0x00, 0x11, 0x12, 0x13, 0x14, 0x15 };
            var filter = new TestKnifeSocketFilter();
            Assert.AreEqual(true, filter.TestCompare(ref data, toCompare));
            Assert.AreEqual(6, data.Length);
            for (int i = 0; i < toCompare.Length; i++)
            {
                Assert.AreEqual(toCompare[i], data[i], data.ToHexString());
            }
        }

        [TestMethod]
        public void CompareTestMethod2()//待比较数据前置
        {
            var data = new byte[] { 0x00, 0x11, 0x12, 0x13, 0x14, 0x15 };
            var toCompare = new byte[] { 0x00, 0x11 };
            var filter = new TestKnifeSocketFilter();
            Assert.AreEqual(true, filter.TestCompare(ref data, toCompare));
            Assert.AreEqual(4, data.Length);
            Assert.AreEqual(0x12, data[0], data.ToHexString());
            Assert.AreEqual(0x13, data[1], data.ToHexString());
            Assert.AreEqual(0x14, data[2], data.ToHexString());
            Assert.AreEqual(0x15, data[3], data.ToHexString());
        }

        [TestMethod]
        public void CompareTestMethod3()//待比较数据后置
        {
            var data = new byte[] { 0x00, 0x11, 0x12, 0x13, 0x14, 0x15 };
            var toCompare = new byte[] { 0x14, 0x15 };
            var filter = new TestKnifeSocketFilter();
            Assert.AreEqual(true, filter.TestCompare(ref data, toCompare));
            Assert.AreEqual(4, data.Length);
            Assert.AreEqual(0x00, data[0], data.ToHexString());
            Assert.AreEqual(0x11, data[1], data.ToHexString());
            Assert.AreEqual(0x12, data[2], data.ToHexString());
            Assert.AreEqual(0x13, data[3], data.ToHexString());
        }

        [TestMethod]
        public void CompareTestMethod4()//待比较数据在中间部份
        {
            var data = new byte[] { 0x00, 0x11, 0x12, 0x13, 0x14, 0x15 };
            var toCompare = new byte[] { 0x12, 0x13 };
            var filter = new TestKnifeSocketFilter();
            Assert.AreEqual(true, filter.TestCompare(ref data, toCompare));
            Assert.AreEqual(4, data.Length);
            Assert.AreEqual(0x00, data[0], data.ToHexString());
            Assert.AreEqual(0x11, data[1], data.ToHexString());
            Assert.AreEqual(0x14, data[2], data.ToHexString());
            Assert.AreEqual(0x15, data[3], data.ToHexString());
        }

        private class TestKnifeSocketFilter : KnifeSocketFilter
        {
            public override bool ContinueNextFilter
            {
                get { return true; }
            }

            public override void PrcoessReceiveData(KnifeSocketSession session, ref byte[] data)
            {
            }

            public bool TestCompare(ref byte[] data, byte[] toCompare)
            {
                return Compare(ref data, toCompare);
            }
        }
    }
}
