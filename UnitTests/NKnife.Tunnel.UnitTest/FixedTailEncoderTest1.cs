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
            const string replay = "ABCDE";

            var encoder = new FixedTailEncoder();
            var actual = encoder.Execute(replay);
            var expected = Encoding.UTF8.GetBytes(replay).Concat(encoder.Tail).ToArray();

            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
