using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.Kits.SocketKnife.StressTest.Codec;

namespace NKnife.Kits.SocketKnife.StressTest.UnitTest
{
    [TestClass]
    public class NangleDatagramDecoderTest
    {
        [TestMethod]
        public void ExecuteTestMethod0() //一条完整的测试
        {
            var decoder = new NangleDatagramDecoder();
            const int COUNT = 1;
            var src = new List<byte>();
            for (int i = 0; i < COUNT; i++)
            {
                src.AddRange(GetAnyBytes());
                src.AddRange(decoder.Tail);
            }
            int index;
            var protocols = decoder.Execute(src.ToArray(), out index);
            Assert.AreEqual(COUNT, protocols.Length);
            Assert.AreEqual(src.Count, index);
            var bs = Encoding.Default.GetString(GetAnyBytes());
            foreach (var protocol in protocols)
            {
                Assert.AreEqual(bs, protocol);
            }
        }
    }
}
