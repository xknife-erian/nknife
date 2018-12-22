using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.Tunnel.Generic;

namespace NKnife.Socket.UnitTest
{
    [TestClass]
    public class FixedTailDecoderTest2
    {
        [TestMethod]
        public void ExecuteTestMethod0()//一条完整的数据
        {
            var decoder = new FixedTailDecoder { Tail = new byte[] { 0xFF } };
            const int count = 1;
            var src = new List<byte>();
            for (int i = 0; i < count; i++)
            {
                src.AddRange(GetAnyBytes());
                src.AddRange(decoder.Tail);
            }
            int index;
            var protocols = decoder.Execute(src.ToArray(), out index);
            Assert.AreEqual(count, protocols.Length);
            Assert.AreEqual(src.Count, index);
            var bs = Encoding.Default.GetString(GetAnyBytes());
            foreach (var protocol in protocols)
            {
                Assert.AreEqual(bs, protocol);
            }
        }

        [TestMethod]
        public void ExecuteTestMethod1()//多条完整的数据
        {
            var decoder = new FixedTailDecoder { Tail = new byte[] { 0xFF } };
            const int count = 5;
            var src = new List<byte>();
            for (int i = 0; i < count; i++)
            {
                src.AddRange(GetAnyBytes());
                src.AddRange(decoder.Tail);
            }
            int index;
            var protocols = decoder.Execute(src.ToArray(), out index);
            Assert.AreEqual(count, protocols.Length);
            Assert.AreEqual(src.Count, index);
            var bs = Encoding.Default.GetString(GetAnyBytes());
            foreach (var protocol in protocols)
            {
                Assert.AreEqual(bs, protocol);
            }
        }

        [TestMethod]
        public void ExecuteTestMethod2()//高数据量数据
        {
            var decoder = new FixedTailDecoder { Tail = new byte[] { 0xFF } };
            const int count = 1000;
            var src = new List<byte>();
            for (int i = 0; i < count; i++)
            {
                src.AddRange(GetAnyBytes());
                src.AddRange(decoder.Tail);
            }
            int index;

            var watch = new Stopwatch();
            watch.Start();
            var protocols = decoder.Execute(src.ToArray(), out index);
            watch.Stop();
            var ms = watch.ElapsedMilliseconds;
            Assert.IsTrue(ms < 2);//解析速度小于5毫秒

            Assert.AreEqual(count, protocols.Length);
            Assert.AreEqual(src.Count, index);
            var bs = Encoding.Default.GetString(GetAnyBytes());
            foreach (var protocol in protocols)
            {
                Assert.AreEqual(bs, protocol);
            }
        }

        [TestMethod]
        public void ExecuteTestMethod3()//有数据但是是不完整的数据
        {
            var decoder = new FixedTailDecoder { Tail = new byte[] { 0xFF } };
            var src = GetAnyBytes();
            int index;
            var protocols = decoder.Execute(src.ToArray(), out index);
            Assert.AreEqual(0, protocols.Length);
            Assert.AreEqual(0, index);
        }

        [TestMethod]
        public void ExecuteTestMethod4() //多条完整的数据，但同时最后有不完整的数据
        {
            var decoder = new FixedTailDecoder { Tail = new byte[] { 0xFF } };
            const int count = 10;
            var src = new List<byte>();
            for (int i = 0; i < count; i++)
            {
                src.AddRange(GetAnyBytes());
                src.AddRange(decoder.Tail);
            }
            src.Add(0xAB);
            int index;
            var protocols = decoder.Execute(src.ToArray(), out index);
            Assert.AreEqual(count, protocols.Length);
            Assert.AreEqual(src.Count - 1, index);
            var bs = Encoding.Default.GetString(GetAnyBytes());
            foreach (var protocol in protocols)
            {
                Assert.AreEqual(bs, protocol);
            }
        }

        protected byte[] GetAnyBytes()
        {
            return Encoding.Default.GetBytes("ABCDEFG");
        }
    }
}
