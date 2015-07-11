using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.Kits.SocketKnife.StressTest.Codec;

namespace NKnife.Kits.SocketKnife.StressTest.UnitTest
{
    /*
     测试关键说明：
     5.2    帧长度
帧长度为目的地址长度+命令字长度+测试数据长度。
考虑TCP单帧数据长度不超过1024字节，所以协议限定帧最大长度为1024字节。
帧长度的最大取值为1020（目的地址长度4+命令字长度2+数据最大长度1013+校验和1）。当测试数据为0字节时，帧长度取最小值7。

     5.2	帧长度
帧长度为目的地址长度+命令字长度+测试数据长度。
考虑TCP单帧数据长度不超过1024字节，所以协议限定帧最大长度为1024字节。
帧长度的最大取值为1020（目的地址长度4+命令字长度2+数据最大长度1013+校验和1）。当测试数据为0字节时，帧长度取最小值7。
     5.5	校验和
将目的地址，命令字，数据进行加和，得出校验和。

     */

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class NangleDatagramDecoderTest
    {
        [TestMethod]
        public void ExecuteTestMethod0() //一条完整的测试
        {
            var decoder = new NangleDatagramDecoder();
            const int COUNT = 1;
            var datagram = GetOneCorrectDatagram();
            var data = new byte[datagram.Count - 3];
            datagram.CopyTo(2, data, 0, datagram.Count - 3);

            var src = new List<byte>();
            src.AddRange(new Byte[]{decoder.FirstHeadByte,decoder.SecondHeadByte});
            src.AddRange(datagram);
            int index;
            var datagrams = decoder.Execute(src.ToArray(), out index);
            Assert.AreEqual(COUNT, datagrams.Length);
            Assert.AreEqual(src.Count, index);
            Assert.IsTrue(data.Compare(datagrams[0]));
        }

        [TestMethod]
        public void VerifyLenAndChkTestMethod0() //一条长度和校验和正确的数据
        {
            var decoder = new NangleDatagramDecoder();
            Assert.IsTrue(decoder.VerifyLenAndChk(GetOneCorrectDatagram().ToArray()));
        }

        [TestMethod]
        public void VerifyDataGramTestMethod0() //一条正确的数据
        {
            var decoder = new NangleDatagramDecoder();
            var datagram = GetOneCorrectDatagram();
            var data = new byte[datagram.Count - 3];
            datagram.CopyTo(2, data, 0, datagram.Count - 3);
            byte[] item;
            Assert.IsTrue(decoder.VerifyDataGram(GetOneCorrectDatagram(),out item));
            Assert.IsTrue(data.Compare(item));
        }

        /// <summary>
        /// 返回一条正确的Datagram，不包含包头0xAA,0x55
        /// </summary>
        /// <returns></returns>
        protected List<byte> GetOneCorrectDatagram()
        {
            //帧长度的最大取值为1020（目的地址长度4+命令字长度2+数据最大长度1013+校验和1）。
            //当测试数据为0字节时，帧长度取最小值7
            var src = new List<byte>();
            var data = GetAnyBytes();
            var len = 7 + data.Length;

            src.AddRange(GetLengthFromIntToTwoBytes(len)); //长度
            src.AddRange(GetTargetAddress()); //目标地址
            src.AddRange(GetAnyCommand()); //命令
            src.AddRange(data); //数据
            var tempBytes = new byte[len - 1];
            src.CopyTo(2, tempBytes, 0, len - 1);
            src.Add(GetOneByteChk(tempBytes));
            return src;
        }


        /// <summary>
        /// 返回4字节长度的目标地址
        /// </summary>
        /// <returns></returns>
        protected byte[] GetTargetAddress()
        {
            return new byte[]{0x30,0x30,0x30,0x31};
        }

        /// <summary>
        /// 返回2字节长度的任意命令
        /// </summary>
        /// <returns></returns>
        protected byte[] GetAnyCommand()
        {
            return new byte[]{0x01,0x02};
        }

        protected byte[] GetAnyBytes()
        {
            return Encoding.Default.GetBytes("ABCDEFG");
        }

        /// <summary>
        /// 整数长度转换成2字节
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private byte[] GetLengthFromIntToTwoBytes(int length)
        {
            return new[]{(byte)((length/255)%255),(byte)(length%255)};
        }

        /// <summary>
        /// 计算1字节校验和
        /// </summary>
        /// <param name="tempData"></param>
        /// <returns></returns>
        private byte GetOneByteChk(byte[] tempData)
        {
            var sum = tempData.Aggregate(0, (current, t) => current + t);
            return (byte)(sum%255);
        }
    }
}
