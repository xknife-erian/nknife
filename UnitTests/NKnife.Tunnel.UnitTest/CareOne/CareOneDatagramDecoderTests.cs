using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonitorKnife.Tunnels.Common;

namespace NKnife.Socket.UnitTest.CareOne
{
    [TestClass()]
    public class CareOneDatagramDecoderTests
    {
        [TestMethod()]
        public void ExecuteTest()
        {
            var data = new Byte[] {0x09, 0x00, 0x06, 0xA0, 0x01, 0x63, 0x61, 0x72, 0x65};
            int index = -1;
            var decoder = new CareOneDatagramDecoder();
            var ps = decoder.Execute(data, out index);
            Assert.AreEqual(index, data.Length);
        }
    }
}
