using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonitorKnife.Tunnels.Common;

namespace NKnife.Socket.UnitTest.CareOne
{
    [TestClass]
    public class CareOneDatagramDecoderTests
    {
        [TestMethod]
        public void ExecuteTest1() //单条简单数据
        {
            var data = GetSingleBytes();
            int index;
            var decoder = new CareOneDatagramDecoder();
            var ps = decoder.Execute(data, out index);
            Assert.AreEqual(index, data.Length);
            Assert.AreEqual(1, ps.Length);
            for (var i = 0; i < data.Length; i++)
            {
                Assert.AreEqual(data[i], ps[0][i]);
            }
        }

        [TestMethod]
        public void ExecuteTest2() //多条简单数据
        {
            var d = GetSingleBytes();
            const int COUNT = 5;
            var data = GetMultiBytes(COUNT);
            int index;
            var decoder = new CareOneDatagramDecoder();
            var ps = decoder.Execute(data, out index);
            Assert.AreEqual(index, data.Length);
            Assert.AreEqual(COUNT, ps.Length);
            for (var i = 0; i < COUNT; i++)
            {
                for (var j = 0; j < d.Length; j++)
                {
                    Assert.AreEqual(d[j], ps[i][j], string.Format("i={0},j={1};d[i]={2},ps[i][j]={3}", i, j, d[i], ps[i][j]));
                }
            }
        }

        [TestMethod]
        public void ExecuteTest3() //前导无效数据
        {
            var single = GetSingleBytes();
            var data = new byte[single.Length + 100];
            for (var i = 0; i < 100; i++)
            {
                if (i%2 == 0)
                    data[i] = 0xFF;
                else
                    data[i] = 0xFE;
            }
            Buffer.BlockCopy(single, 0, data, 100, single.Length);
            //以上准备测试数据
            int index;
            var decoder = new CareOneDatagramDecoder();
            var ps = decoder.Execute(data, out index);
            Assert.AreEqual(index, data.Length);
            Assert.AreEqual(1, ps.Length);
            for (var i = 0; i < single.Length; i++)
            {
                Assert.AreEqual(single[i], ps[0][i]);
            }
        }

        [TestMethod]
        public void ExecuteTest4() //尾部无效数据
        {
            var single = GetSingleBytes();
            var data = new byte[single.Length + 100];
            for (var i = single.Length; i < data.Length; i++)
            {
                if (i%2 == 0)
                    data[i] = 0xFF;
                else
                    data[i] = 0xFE;
            }
            Buffer.BlockCopy(single, 0, data, 0, single.Length);
            //以上准备测试数据
            int index;
            var decoder = new CareOneDatagramDecoder();
            var ps = decoder.Execute(data, out index);
            Assert.AreEqual(index, data.Length);
            Assert.AreEqual(1, ps.Length);
            for (var i = 0; i < single.Length; i++)
            {
                Assert.AreEqual(single[i], ps[0][i]);
            }
        }

        [TestMethod]
        public void ExecuteTest5() //中间无效数据(多条数据)
        {
            const int SAMPLE = 3;
            const int COUNT = 4;
            var single = GetSingleBytes();
            var data = new byte[single.Length*COUNT + SAMPLE*(COUNT - 1 + 2)];
            for (var k = 0; k < COUNT; k++)
            {
                for (var i = 0; i < SAMPLE; i++)
                    data[k*SAMPLE + k*single.Length + i] = 0x01;
                Buffer.BlockCopy(single, 0, data, (k + 1)*SAMPLE + k*single.Length, single.Length);
            }

            //以上准备测试数据
            int index;
            var decoder = new CareOneDatagramDecoder();
            var ps = decoder.Execute(data, out index);
            Assert.AreEqual(index, data.Length);
            Assert.AreEqual(COUNT, ps.Length);
            for (var i = 0; i < COUNT; i++)
            {
                for (var j = 0; j < single.Length; j++)
                {
                    Assert.AreEqual(single[j], ps[i][j], string.Format("i={0},j={1};d[i]={2},ps[i][j]={3}", i, j, single[i], ps[i][j]));
                }
            }
        }

        [TestMethod]
        public void ExecuteTest6() //待接包
        {
            const int SAMPLE = 3;
            const int COUNT = 4;
            var single = GetSingleBytes();
            var data = new byte[single.Length*COUNT + SAMPLE*(COUNT - 1 + 2) + 1]; //最后的1位是待接包
            for (var k = 0; k < COUNT; k++)
            {
                for (var i = 0; i < SAMPLE; i++)
                    data[k*SAMPLE + k*single.Length + i] = 0x01;
                Buffer.BlockCopy(single, 0, data, (k + 1)*SAMPLE + k*single.Length, single.Length);
            }

            //以上准备测试数据
            int index;
            var decoder = new CareOneDatagramDecoder();
            var ps = decoder.Execute(data, out index);
            Assert.AreEqual(index, data.Length - 1);
            Assert.AreEqual(COUNT, ps.Length);
            for (var i = 0; i < COUNT; i++)
            {
                for (var j = 0; j < single.Length; j++)
                {
                    Assert.AreEqual(single[j], ps[i][j], string.Format("i={0},j={1};d[i]={2},ps[i][j]={3}", i, j, single[i], ps[i][j]));
                }
            }
        }

        //********************************************************************************************
        private static byte[] GetSingleBytes()
        {
            return new Byte[] {0x09, 0x00, 0x06, 0xA0, 0x01, 0x63, 0x61, 0x72, 0x65};
        }

        private static byte[] GetMultiBytes(int count)
        {
            var d = GetSingleBytes();
            var data = new byte[d.Length*count];
            for (var i = 0; i < count; i++)
            {
                Buffer.BlockCopy(d, 0, data, d.Length*i, d.Length);
            }
            return data;
        }
    }
}