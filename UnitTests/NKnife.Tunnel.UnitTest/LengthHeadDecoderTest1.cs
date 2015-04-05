using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.Tunnel.Generic;
using NKnife.Zip;

namespace NKnife.Socket.UnitTest
{
    [TestClass]
    public class LengthHeadDecoderTest1
    {
        [TestMethod]
        public void ExecuteTestMethod0()//普通单条短数据
        {
            var decoder = new LengthHeadDecoder();
            const string ABCDE = "ABCDE";

            var content = Encoding.Default.GetBytes(ABCDE);
            var lenghtHead = BitConverter.GetBytes(content.Length);

            var data = lenghtHead.Concat(content).ToArray();

            int finishedIndex;
            var result = decoder.Execute(data, out finishedIndex);

            Assert.AreEqual(data.Length, finishedIndex);
            Assert.AreEqual(content.Length, result[0].Length);

            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[0][i]);
            }
        }

        [TestMethod]
        public void ExecuteTestMethod1()//单条大数据
        {
            var decoder = new LengthHeadDecoder();
            const string ABCDE = "ABCDE";

            var sb = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                sb.Append(ABCDE);
            }

            var content = Encoding.Default.GetBytes(sb.ToString());
            var lenghtHead = BitConverter.GetBytes(content.Length);

            var data = lenghtHead.Concat(content).ToArray();

            int finishedIndex;
            var result = decoder.Execute(data, out finishedIndex);

            Assert.AreEqual(data.Length, finishedIndex);
            Assert.AreEqual(content.Length, result[0].Length);

            for (int i = 0; i < sb.Length; i++)
            {
                Assert.AreEqual(sb[i], result[0][i]);
            }
        }

        [TestMethod]
        public void ExecuteTestMethod3()//普通多条短数据
        {
            var decoder = new LengthHeadDecoder();
            const string ABCDE = "ABCDE";

            var content = Encoding.Default.GetBytes(ABCDE);
            var lenghtHead = BitConverter.GetBytes(content.Length);

            var data = lenghtHead.Concat(content)
                .Concat(lenghtHead).Concat(content)
                .Concat(lenghtHead).Concat(content)
                .Concat(lenghtHead).Concat(content)
                .Concat(lenghtHead).Concat(content)
                .ToArray();

            int finishedIndex;
            var result = decoder.Execute(data, out finishedIndex);

            Assert.AreEqual(data.Length, finishedIndex);
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(content.Length, result[0].Length);

            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[0][i]);
            }
            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[1][i]);
            }
            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[2][i]);
            }
            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[3][i]);
            }
            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[4][i]);
            }
        }

        [TestMethod]
        public void ExecuteTestMethod4() //普通多条短数据，最后有未完成数据
        {
            var decoder = new LengthHeadDecoder();
            const string ABCDE = "ABCDE";

            var content = Encoding.Default.GetBytes(ABCDE);
            var lenghtHead = BitConverter.GetBytes(content.Length);

            var data = lenghtHead.Concat(content)
                .Concat(lenghtHead).Concat(content)
                .Concat(lenghtHead).Concat(content)
                .Concat(lenghtHead).Concat(content)
                .Concat(lenghtHead).Concat(content)
                .Concat(lenghtHead).Concat(new Byte[] {0xAA})
                .ToArray();

            int finishedIndex;
            var result = decoder.Execute(data, out finishedIndex);

            Assert.AreEqual(data.Length - 5, finishedIndex);
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(content.Length, result[0].Length);

            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[0][i]);
            }
            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[1][i]);
            }
            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[2][i]);
            }
            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[3][i]);
            }
            for (int i = 0; i < ABCDE.Length; i++)
            {
                Assert.AreEqual(ABCDE[i], result[4][i]);
            }
        }

        [TestMethod]
        public void ExecuteTestMethod5()//单条大数据
        {
            var decoder = new LengthHeadDecoder(){EnabelCompress = true};
            const string ABCDE = "ABCDE";

            var sb = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                sb.Append(ABCDE);
            }

            var content = CompressHelper.Compress(Encoding.Default.GetBytes(sb.ToString()));
            var lenghtHead = BitConverter.GetBytes(content.Length);

            var data = lenghtHead.Concat(content).ToArray();

            int finishedIndex;
            var result = decoder.Execute(data, out finishedIndex);

            Assert.AreEqual(data.Length, finishedIndex);
            Assert.AreEqual(sb.Length, result[0].Length);

            for (int i = 0; i < sb.Length; i++)
            {
                Assert.AreEqual(sb[i], result[0][i]);
            }
        }
    }
}
