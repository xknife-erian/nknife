using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.Tunnel.Generic;

namespace NKnife.Socket.UnitTest
{
    [TestClass]
    public class FixedTailEncoderTest1
    {
        [TestMethod]
        public void ExecuteTestMethod1()
        {
            const string REPLAY = "ABCDE";

            var encoder = new FixedTailEncoder();
            var actual = encoder.Execute(REPLAY);
            var expected = Encoding.UTF8.GetBytes(REPLAY).Concat(encoder.Tail).ToArray();

            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
