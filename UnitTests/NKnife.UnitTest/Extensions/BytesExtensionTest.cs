﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NKnife.UnitTest.Extensions
{
    /// <summary>
    ///     BytesExtensionTest 的摘要说明
    /// </summary>
    [TestClass]
    public class BytesExtensionTest
    {
        #region 附加测试特性

        //        public BytesExtensionTest()
        //        {
//        }
//
//        private TestContext testContextInstance;
//
//        /// <summary>
//        ///获取或设置测试上下文，该上下文提供
//        ///有关当前测试运行及其功能的信息。
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

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
        public void IndexOfTestMethod1()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {0x02, 0x03};
            int actual = source.IndexOf(target);
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void IndexOfTestMethod2()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {0x02, 0x04};
            int actual = source.IndexOf(target);
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void IndexOfTestMethod3()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {};
            int actual = source.IndexOf(target);
            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void IndexOfTestMethod4()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target1 = new byte[] {0x00};
            int actual = source.IndexOf(target1);
            Assert.AreEqual(0, actual);

            var target2 = new byte[] {0x09};
            actual = source.IndexOf(target2);
            Assert.AreEqual(9, actual);
        }

        [TestMethod]
        public void IndexOfTestMethod5()
        {
            var source = new byte[] {0x02, 0x03};
            var target = new byte[] {0x02, 0x03};
            int actual = source.IndexOf(target);
            Assert.AreEqual(0, actual);
            actual = source.IndexOf(source);
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void IndexOfTestMethod6()
        {
            var source = new byte[] {0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09};
            var target = new byte[] {0x08, 0x09};
            int actual = source.IndexOf(target);
            Assert.AreEqual(8, actual);
        }

        [TestMethod]
        public void IndexOfTestMethod7()
        {
            var source = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            var target = new byte[] { 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            int actual = source.IndexOf(target);
            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void IndexOfTestMethod8()
        {
            var source = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 };
            var target = new byte[] { 0x03, 0x04, 0x05, 0x06, 0x07, 0x09 };
            int actual = source.IndexOf(target);
            Assert.AreEqual(-1, actual);
        }
    }
}