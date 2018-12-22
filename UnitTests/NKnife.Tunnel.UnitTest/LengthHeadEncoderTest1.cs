using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.Tunnel.Generic;
using NKnife.Zip;

namespace NKnife.Socket.UnitTest
{
    [TestClass]
    public class LengthHeadEncoderTest1
    {
        [TestMethod]
        public void ExecuteTestMethod1()//单条短数据
        {
            var decoder = new LengthHeadDecoder();
            const string abcde = "ABCDE";

            var encoder = new LengthHeadEncoder();

            var data = encoder.Execute(abcde);

            int finishedIndex;
            var result = decoder.Execute(data, out finishedIndex);

            Assert.AreEqual(data.Length, finishedIndex);

            for (int i = 0; i < abcde.Length; i++)
            {
                Assert.AreEqual(abcde[i], result[0][i]);
            }
        }

        [TestMethod]
        public void ExecuteTestMethod2()//单条长数据
        {
            var decoder = new LengthHeadDecoder();
            const string abcde = "ABCDE";
            var sb = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                sb.Append(abcde);
            }
            var encoder = new LengthHeadEncoder();

            var data = encoder.Execute(sb.ToString());

            int finishedIndex;
            var result = decoder.Execute(data, out finishedIndex);

            Assert.AreEqual(data.Length, finishedIndex);

            for (int i = 0; i < abcde.Length; i++)
            {
                Assert.AreEqual(abcde[i], result[0][i]);
            }
        }

        [TestMethod]
        public void ExecuteTestMethod3()//单条长数据，需压缩
        {
            var decoder = new LengthHeadDecoder();
            decoder.EnabelCompress = true;

            const string abcde = "ABCDE";
            var sb = new StringBuilder();
            for (int i = 0; i < 100000; i++)
            {
                sb.Append(abcde);
            }
            var encoder = new LengthHeadEncoder();
            encoder.EnabelCompress = true;

            var data = encoder.Execute(sb.ToString());

            int finishedIndex;
            var result = decoder.Execute(data, out finishedIndex);

            Assert.AreEqual(sb.Length, result[0].Length);
            for (int i = 0; i < sb.Length; i++)
            {
                Assert.AreEqual(sb[i], result[0][i]);
            }
        }
    }
}
