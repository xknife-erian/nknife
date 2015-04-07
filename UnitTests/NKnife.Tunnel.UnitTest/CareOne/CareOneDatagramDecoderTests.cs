﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonitorKnife.Tunnels.Common;

namespace NKnife.Socket.UnitTest.CareOne
{
    [TestClass()]
    public class CareOneDatagramDecoderTests
    {
        [TestMethod()]
        public void ExecuteTest1()
        {
            var data = new Byte[] {0x09, 0x00, 0x06, 0xA0, 0x01, 0x63, 0x61, 0x72, 0x65};
            int index = -1;
            var decoder = new CareOneDatagramDecoder();
            var ps = decoder.Execute(data, out index);
            Assert.AreEqual(index, data.Length);
            Assert.AreEqual(1, ps.Length);
            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual(data[i], ps[0][i]);
            }
        }

        [TestMethod()]
        public void ExecuteTest2()
        {
            const int COUNT = 5;
            var d = new Byte[] { 0x09, 0x00, 0x06, 0xA0, 0x01, 0x63, 0x61, 0x72, 0x65 };
            var data = new byte[d.Length*COUNT];
            for (int i = 0; i < COUNT; i++)
            {
                Buffer.BlockCopy(d,0,data,d.Length*i,d.Length);
            }
            int index = -1;
            var decoder = new CareOneDatagramDecoder();
            var ps = decoder.Execute(data, out index);
            Assert.AreEqual(index, data.Length);
            Assert.AreEqual(COUNT, ps.Length);
            for (int i = 0; i < COUNT; i++)
            {
                for (int j = 0; j < d.Length; j++)
                {
                    Assert.AreEqual(d[j], ps[i][j],string.Format("i={0},j={1};d[i]={2},ps[i][j]={3}",i,j,d[i],ps[i][j]));
                }
            }
        }
    }
}
