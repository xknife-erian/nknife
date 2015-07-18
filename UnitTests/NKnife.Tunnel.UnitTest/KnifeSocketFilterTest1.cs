using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Generic;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;

namespace NKnife.Socket.UnitTest
{
    /// <summary>
    /// KnifeSocketFilterTest 的摘要说明
    /// </summary>
    [TestClass]
    public class KnifeSocketFilterTest1
    {
        //TODO:整个单元测试类应该不能工作了
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

        private static readonly EndPoint _endPoint = new IPEndPoint(IPAddress.Parse("192.168.0.0"), 8899);

        /*
         以下ProcessDataPacketTestMethod方法，主要测试异常的情况下的，半包的接包，粘包等现象
         */
        [TestMethod]
        public void ProcessDataPacketTestMethod1()
        {
            var filter = new TestKnifeSocketFilter();
            var dataPacket = new byte[] {};
            var unFinished = new byte[] {};
            var result = filter.ProcessDataPacketMethod(dataPacket, unFinished, dataDecoder1, _endPoint);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ProcessDataPacketTestMethod2()
        {
            var filter = new TestKnifeSocketFilter();
            var dataPacket = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            var unFinished = new byte[] { };
            var result = filter.ProcessDataPacketMethod(dataPacket, unFinished, dataDecoder1, _endPoint);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ProcessDataPacketTestMethod3()
        {
            var filter = new TestKnifeSocketFilter();
            var dataPacket = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            var unFinished = new byte[] { };
            var result = filter.ProcessDataPacketMethod(dataPacket, unFinished, dataDecoder2, _endPoint);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0x05, result[0]);
        }

        [TestMethod]
        public void ProcessDataPacketTestMethod4()
        {
            var filter = new TestKnifeSocketFilter();
            var dataPacket = new byte[] {0x01, 0x02, 0x03, 0x04, 0x05};
            var unFinished = new byte[] {0x0a, 0x0b, 0x0c};
            var result = filter.ProcessDataPacketMethod(dataPacket, unFinished, dataDecoder2, _endPoint);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0x05, result[0]);
        }

        [TestMethod]
        public void ProcessDataPacketTestMethod5()
        {
            var filter = new TestKnifeSocketFilter();
            var dataPacket = new byte[] { 0x0a, 0x0b, 0x0c };
            var unFinished = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            var result = filter.ProcessDataPacketMethod(dataPacket, unFinished, dataDecoder2, _endPoint);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(0x0c, result[0]);
        }

        [TestMethod]
        public void ProcessDataPacketTestMethod6()
        {
            //TODO:
//            var filter = new TestKnifeSocketFilter();
//            var dataPacket = new byte[] { 0x0a, 0x0b, 0x0c };
//            var unFinished = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
//            var result = filter.ProcessDataPacketMethod(dataPacket, unFinished, dataDecoder2, _endPoint);
//            Assert.AreEqual(1, result.Count);
//            Assert.AreEqual(0x0c, result[0]);
//            dataPacket = new byte[] { 0x0a, 0x0b, 0x0c, 0x0d };
//            var result1 = filter.ProcessDataPacketMethod(dataPacket, result, dataDecoder2, _endPoint);
//            Assert.AreEqual(1, result1.Count);
//            Assert.AreEqual(0x0d, result1[0]);
        }

        private int dataDecoder1(EndPoint endPoint, byte[] data)
        {
            return data.Length;
        }

        private int dataDecoder2(EndPoint endPoint, byte[] data)
        {
            return data.Length - 1;
        }

        private class TestKnifeSocketFilter : SocketProtocolFilter
        {
            public List<IProtocol<string>> ProcessDataPacketMethod(byte[] dataPacket, byte[] unFinished, Func<EndPoint, byte[], int> dataDecoder, EndPoint endPoint)
            {
                return ProcessDataPacket(dataPacket, ref unFinished).ToList();
            }
        }
    }
}
