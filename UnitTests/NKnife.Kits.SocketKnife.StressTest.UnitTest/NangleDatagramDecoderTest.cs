using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NKnife.Converts;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Utility;

namespace NKnife.Kits.SocketKnife.StressTest.UnitTest
{
    /*
     测试关键说明：
     5.2	帧长度
帧长度为目的地址长度+命令字长度+测试数据长度+校验和。
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
        public void ExecuteTestMethod0() //一条完整的数据
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
        public void ExecuteTestMethod1() //n条一样的完整的数据（连续）
        {
            var decoder = new NangleDatagramDecoder();
            const int COUNT = 2;
            var datagram = GetOneCorrectDatagram();
            var data = new byte[datagram.Count - 3];
            datagram.CopyTo(2, data, 0, datagram.Count - 3);

            var src = new List<byte>();
            for (int i = 0; i < COUNT; i++)
            {
                src.AddRange(new Byte[] { decoder.FirstHeadByte, decoder.SecondHeadByte });
                src.AddRange(datagram);
            }
            int index;
            var datagrams = decoder.Execute(src.ToArray(), out index);
            Assert.AreEqual(COUNT, datagrams.Length);
            Assert.AreEqual(src.Count, index);
            for (int i = 0; i < COUNT; i++)
            {
                Assert.IsTrue(data.Compare(datagrams[i]));
            }
        }

        [TestMethod]
        public void ExecuteTestMethod2() //一条完整的数据，前面有错误的数据
        {
            var decoder = new NangleDatagramDecoder();
            const int COUNT = 1;
            var datagram = GetOneCorrectDatagram();
            var data = new byte[datagram.Count - 3];
            datagram.CopyTo(2, data, 0, datagram.Count - 3);

            var src = new List<byte>();
            src.AddRange(GetNoiseBytes());
            src.AddRange(new Byte[] { decoder.FirstHeadByte, decoder.SecondHeadByte });
            src.AddRange(datagram);
            int index;
            var datagrams = decoder.Execute(src.ToArray(), out index);
            Assert.AreEqual(COUNT, datagrams.Length);
            Assert.AreEqual(src.Count, index);
            Assert.IsTrue(data.Compare(datagrams[0]));
        }

        [TestMethod]
        public void ExecuteTestMethod3() //一条完整的数据，后面有错误的数据
        {
            var decoder = new NangleDatagramDecoder();
            const int COUNT = 1;
            var datagram = GetOneCorrectDatagram();
            var data = new byte[datagram.Count - 3];
            datagram.CopyTo(2, data, 0, datagram.Count - 3);

            var src = new List<byte>();
            src.AddRange(new Byte[] { decoder.FirstHeadByte, decoder.SecondHeadByte });
            src.AddRange(datagram);
            src.AddRange(GetNoiseBytes());
            int index;
            var datagrams = decoder.Execute(src.ToArray(), out index);
            Assert.AreEqual(COUNT, datagrams.Length);
            Assert.AreEqual(src.Count - GetNoiseBytes().Length, index);
            Assert.IsTrue(data.Compare(datagrams[0]));
        }

        [TestMethod]
        public void ExecuteTestMethod4() //一条完整的数据，前面后面都有错误的数据
        {
            var decoder = new NangleDatagramDecoder();
            const int COUNT = 1;
            var datagram = GetOneCorrectDatagram();
            var data = new byte[datagram.Count - 3];
            datagram.CopyTo(2, data, 0, datagram.Count - 3);

            var src = new List<byte>();
            src.AddRange(GetNoiseBytes());
            src.AddRange(new Byte[] { decoder.FirstHeadByte, decoder.SecondHeadByte });
            src.AddRange(datagram);
            src.AddRange(GetNoiseBytes());
            int index;
            var datagrams = decoder.Execute(src.ToArray(), out index);
            Assert.AreEqual(COUNT, datagrams.Length);
            Assert.AreEqual(src.Count - GetNoiseBytes().Length, index);
            Assert.IsTrue(data.Compare(datagrams[0]));
        }

        [TestMethod]
        public void ExecuteTestMethod5() //一条完整的数据
        {
            var decoder = new NangleDatagramDecoder();
            const int COUNT = 1;
            var data = UtilityConvert.HexToBytes("AA 55 00 19 00 00 00 00 00 07 00 01 00 00 02 29 00 00 00 00 00 00 00 00 FF FF FF FF 2F");
            int index;
            var datagrams = decoder.Execute(data, out index);
            Assert.AreEqual(COUNT, datagrams.Length);
            Assert.AreEqual(data.Length, index);
        }


        [TestMethod]
        public void ExecuteTestMethod6() //一条完整的数据,分成两次发送
        {
            var decoder = new NangleDatagramDecoder();
            var data1 = UtilityConvert.HexToBytes("AA 55 00 19 00 00 00 00 00 07 00 01 00 00 02 29");
            var data2 = UtilityConvert.HexToBytes("00 00 00 00 00 00 00 00 FF FF FF FF 2F");
            int done;
            var datagrams = decoder.Execute(data1, out done);
            Assert.AreEqual(0, datagrams.Length);
            Assert.AreEqual(0, done);

            var unFinished = new byte[data1.Length - done];
            Buffer.BlockCopy(data1, done, unFinished, 0, unFinished.Length);

            if (!UtilityCollection.IsNullOrEmpty(unFinished))
            {
                // 当有半包数据时，进行接包操作
                int srcLen = data2.Length;
                data2 = unFinished.Concat(data2).ToArray();
            }

            datagrams = decoder.Execute(data2, out done);
            Assert.AreEqual(1, datagrams.Length);
            Assert.AreEqual(data2.Length, done);
        }

        [TestMethod]
        public void ExecuteTestMethod7() //一条完整的数据(很大),分成多次发送
        {
            var decoder = new NangleDatagramDecoder();
            var dataList = new List<byte[]>();
            dataList.Add(UtilityConvert.HexToBytes("AA 55 03 FC"));
            dataList.Add(UtilityConvert.HexToBytes("00 00 00 00 00 08 7C 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10 11 12 13 14 15 16 17 18 19 1A 1B 1C 1D 1E 1F 20 21 22 23 24 25 26 27 28 29 2A 2B 2C 2D 2E 2F 30 31 32 33 34 35 36 37 38 39 3A 3B 3C 3D 3E 3F 40 41 42 43 44 45 46 47 48 49 4A 4B 4C 4D 4E 4F 50 51 52 53 54 55 56 57 58 59 5A 5B 5C 5D 5E 5F 60 61 62 63 64 65 66 67 68"));
            dataList.Add(UtilityConvert.HexToBytes("69 6A 6B 6C 6D 6E 6F 70 71 72 73 74 75 76 77 78 79 7A 7B 7C 7D 7E 7F 80 81 82 83 84 85 86 87 88 89 8A 8B 8C 8D 8E 8F 90 91 92 93 94 95 96 97 98 99 9A 9B 9C 9D 9E 9F A0 A1 A2 A3 A4 A5 A6 A7 A8 A9 AA AB AC AD AE AF B0 B1 B2 B3 B4 B5 B6 B7 B8 B9 BA BB BC BD BE BF C0 C1 C2 C3 C4 C5 C6 C7 C8 C9 CA CB CC CD CE CF D0 D1 D2 D3 D4 D5 D6 D7 D8 D9 DA DB DC DD DE DF E0 E1 E2 E3 E4 E5 E6 E7 E8"));
            dataList.Add(UtilityConvert.HexToBytes("E9 EA EB EC ED EE EF F0 F1 F2 F3 F4 F5 F6 F7 F8 F9 FA FB FC FD FE FF 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10 11 12 13 14 15 16 17 18 19 1A 1B 1C 1D 1E 1F 20 21 22 23 24 25 26 27 28 29 2A 2B 2C 2D 2E 2F 30 31 32 33 34 35 36 37 38 39 3A 3B 3C 3D 3E 3F 40 41 42 43 44 45 46 47 48 49 4A 4B 4C 4D 4E 4F 50 51 52 53 54 55 56 57 58 59 5A 5B 5C 5D 5E 5F 60 61 62 63 64 65 66 67 68 69 6A 6B 6C 6D 6E 6F 70 71 72 73 74 75 76 77 78 79 7A 7B 7C 7D 7E 7F 80 81 82 83 84 85 86 87 88 89 8A 8B 8C 8D 8E 8F 90 91 92 93 94 95 96 97 98 99 9A 9B 9C 9D 9E 9F A0 A1 A2 A3 A4 A5 A6 A7 A8 A9 AA AB AC AD AE AF B0 B1 B2 B3 B4 B5 B6 B7 B8 B9 BA BB BC BD BE BF C0 C1 C2 C3 C4 C5 C6 C7 C8 C9 CA CB CC CD CE CF D0 D1 D2 D3 D4 D5 D6 D7 D8 D9 DA DB DC DD DE DF E0 E1 E2 E3 E4 E5 E6 E7 E8 E9 EA EB EC ED EE EF F0 F1 F2 F3 F4 F5 F6 F7 F8 F9 FA FB FC FD FE FF 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10 11 12 13 14 15 16 17 18 19 1A 1B 1C 1D 1E 1F 20 21 22 23 24 25 26 27 28 29 2A 2B 2C 2D 2E 2F 30 31 32 33 34 35 36 37 38 39 3A 3B 3C 3D 3E 3F 40 41 42 43 44 45 46 47 48 49 4A 4B 4C 4D 4E 4F 50 51 52 53 54 55 56 57 58 59 5A 5B 5C 5D 5E 5F 60 61 62 63 64 65 66 67 68 69 6A 6B 6C 6D 6E 6F 70 71 72 73 74 75 76 77 78"));
            dataList.Add(UtilityConvert.HexToBytes("79 7A 7B 7C 7D 7E 7F 80 81 82 83 84 85 86 87 88 89 8A 8B 8C 8D 8E 8F 90 91 92 93 94 95 96 97 98 99 9A 9B 9C 9D 9E 9F A0 A1 A2 A3 A4 A5 A6 A7 A8 A9 AA AB AC AD AE AF B0 B1 B2 B3 B4 B5 B6 B7 B8 B9 BA BB BC BD BE BF C0 C1 C2 C3 C4 C5 C6 C7 C8 C9 CA CB CC CD CE CF D0 D1 D2 D3 D4 D5 D6 D7 D8 D9 DA DB DC DD DE DF E0 E1 E2 E3 E4 E5 E6 E7 E8 E9 EA EB EC ED EE EF F0 F1 F2 F3 F4 F5 F6 F7 F8"));
            dataList.Add(UtilityConvert.HexToBytes("F9 FA FB FC FD FE FF 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10 11 12 13 14 15 16 17 18 19 1A 1B 1C 1D 1E 1F 20 21 22 23 24 25 26 27 28 29 2A 2B 2C 2D 2E 2F 30 31 32 33 34 35 36 37 38 39 3A 3B 3C 3D 3E 3F 40 41 42 43 44 45 46 47 48 49 4A 4B 4C 4D 4E 4F 50 51 52 53 54 55 56 57 58 59 5A 5B 5C 5D 5E 5F 60 61 62 63 64 65 66 67 68 69 6A 6B 6C 6D 6E 6F 70 71 72 73 74 75 76 77 78"));
            dataList.Add(UtilityConvert.HexToBytes("79 7A 7B 7C 7D 7E 7F 80 81 82 83 84 85 86 87 88 89 8A 8B 8C 8D 8E 8F 90 91 92 93 94 95 96 97 98 99 9A 9B 9C 9D 9E 9F A0 A1 A2 A3 A4 A5 A6 A7 A8 A9 AA AB AC AD AE AF B0 B1 B2 B3 B4 B5 B6 B7 B8 B9 BA BB BC BD BE BF C0 C1 C2 C3 C4 C5 C6 C7 C8 C9 CA CB CC CD CE CF D0 D1 D2 D3 D4 D5 D6 D7 D8 D9 DA DB DC DD DE DF E0 E1 E2 E3 E4 E5 E6 E7 E8 E9 EA EB EC ED EE EF F0 F1 F2 F3 D2"));


            int done;
            byte[][] datagrams;
            byte[] unFinished = {};
            for (int i = 0; i < dataList.Count; i++)
            {
                var data = dataList[i];
                if (!UtilityCollection.IsNullOrEmpty(unFinished))
                {
                    // 当有半包数据时，进行接包操作
                    data = unFinished.Concat(data).ToArray();
                }
                
                Trace.WriteLine(string.Format("当前数据长度：{0}",data.Length));

                datagrams = decoder.Execute(data, out done);
                if (i < dataList.Count - 1) //没到最后一个
                {
                    Assert.AreEqual(0, datagrams.Length);
                    Assert.AreEqual(0, done);

                    unFinished = new byte[data.Length - done];
                    Buffer.BlockCopy(data, done, unFinished, 0, unFinished.Length);
                }
                else //最后一个
                {
                    Assert.AreEqual(1, datagrams.Length);
                    Assert.AreEqual(data.Length, done);
                }
            }
        }

        [TestMethod]
        public void ExecuteTestMethod8() //一条完整的数据(很大),分成多次发送,头部有错误，尾部也有1个字节错误 
        {
            var decoder = new NangleDatagramDecoder();
            var dataList = new List<byte[]>();
            dataList.Add(UtilityConvert.HexToBytes("AA CC AA 55 03 FC"));
            dataList.Add(UtilityConvert.HexToBytes("00 00 00 00 00 08 7C 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10 11 12 13 14 15 16 17 18 19 1A 1B 1C 1D 1E 1F 20 21 22 23 24 25 26 27 28 29 2A 2B 2C 2D 2E 2F 30 31 32 33 34 35 36 37 38 39 3A 3B 3C 3D 3E 3F 40 41 42 43 44 45 46 47 48 49 4A 4B 4C 4D 4E 4F 50 51 52 53 54 55 56 57 58 59 5A 5B 5C 5D 5E 5F 60 61 62 63 64 65 66 67 68"));
            dataList.Add(UtilityConvert.HexToBytes("69 6A 6B 6C 6D 6E 6F 70 71 72 73 74 75 76 77 78 79 7A 7B 7C 7D 7E 7F 80 81 82 83 84 85 86 87 88 89 8A 8B 8C 8D 8E 8F 90 91 92 93 94 95 96 97 98 99 9A 9B 9C 9D 9E 9F A0 A1 A2 A3 A4 A5 A6 A7 A8 A9 AA AB AC AD AE AF B0 B1 B2 B3 B4 B5 B6 B7 B8 B9 BA BB BC BD BE BF C0 C1 C2 C3 C4 C5 C6 C7 C8 C9 CA CB CC CD CE CF D0 D1 D2 D3 D4 D5 D6 D7 D8 D9 DA DB DC DD DE DF E0 E1 E2 E3 E4 E5 E6 E7 E8"));
            dataList.Add(UtilityConvert.HexToBytes("E9 EA EB EC ED EE EF F0 F1 F2 F3 F4 F5 F6 F7 F8 F9 FA FB FC FD FE FF 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10 11 12 13 14 15 16 17 18 19 1A 1B 1C 1D 1E 1F 20 21 22 23 24 25 26 27 28 29 2A 2B 2C 2D 2E 2F 30 31 32 33 34 35 36 37 38 39 3A 3B 3C 3D 3E 3F 40 41 42 43 44 45 46 47 48 49 4A 4B 4C 4D 4E 4F 50 51 52 53 54 55 56 57 58 59 5A 5B 5C 5D 5E 5F 60 61 62 63 64 65 66 67 68 69 6A 6B 6C 6D 6E 6F 70 71 72 73 74 75 76 77 78 79 7A 7B 7C 7D 7E 7F 80 81 82 83 84 85 86 87 88 89 8A 8B 8C 8D 8E 8F 90 91 92 93 94 95 96 97 98 99 9A 9B 9C 9D 9E 9F A0 A1 A2 A3 A4 A5 A6 A7 A8 A9 AA AB AC AD AE AF B0 B1 B2 B3 B4 B5 B6 B7 B8 B9 BA BB BC BD BE BF C0 C1 C2 C3 C4 C5 C6 C7 C8 C9 CA CB CC CD CE CF D0 D1 D2 D3 D4 D5 D6 D7 D8 D9 DA DB DC DD DE DF E0 E1 E2 E3 E4 E5 E6 E7 E8 E9 EA EB EC ED EE EF F0 F1 F2 F3 F4 F5 F6 F7 F8 F9 FA FB FC FD FE FF 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10 11 12 13 14 15 16 17 18 19 1A 1B 1C 1D 1E 1F 20 21 22 23 24 25 26 27 28 29 2A 2B 2C 2D 2E 2F 30 31 32 33 34 35 36 37 38 39 3A 3B 3C 3D 3E 3F 40 41 42 43 44 45 46 47 48 49 4A 4B 4C 4D 4E 4F 50 51 52 53 54 55 56 57 58 59 5A 5B 5C 5D 5E 5F 60 61 62 63 64 65 66 67 68 69 6A 6B 6C 6D 6E 6F 70 71 72 73 74 75 76 77 78"));
            dataList.Add(UtilityConvert.HexToBytes("79 7A 7B 7C 7D 7E 7F 80 81 82 83 84 85 86 87 88 89 8A 8B 8C 8D 8E 8F 90 91 92 93 94 95 96 97 98 99 9A 9B 9C 9D 9E 9F A0 A1 A2 A3 A4 A5 A6 A7 A8 A9 AA AB AC AD AE AF B0 B1 B2 B3 B4 B5 B6 B7 B8 B9 BA BB BC BD BE BF C0 C1 C2 C3 C4 C5 C6 C7 C8 C9 CA CB CC CD CE CF D0 D1 D2 D3 D4 D5 D6 D7 D8 D9 DA DB DC DD DE DF E0 E1 E2 E3 E4 E5 E6 E7 E8 E9 EA EB EC ED EE EF F0 F1 F2 F3 F4 F5 F6 F7 F8"));
            dataList.Add(UtilityConvert.HexToBytes("F9 FA FB FC FD FE FF 00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F 10 11 12 13 14 15 16 17 18 19 1A 1B 1C 1D 1E 1F 20 21 22 23 24 25 26 27 28 29 2A 2B 2C 2D 2E 2F 30 31 32 33 34 35 36 37 38 39 3A 3B 3C 3D 3E 3F 40 41 42 43 44 45 46 47 48 49 4A 4B 4C 4D 4E 4F 50 51 52 53 54 55 56 57 58 59 5A 5B 5C 5D 5E 5F 60 61 62 63 64 65 66 67 68 69 6A 6B 6C 6D 6E 6F 70 71 72 73 74 75 76 77 78"));
            dataList.Add(UtilityConvert.HexToBytes("79 7A 7B 7C 7D 7E 7F 80 81 82 83 84 85 86 87 88 89 8A 8B 8C 8D 8E 8F 90 91 92 93 94 95 96 97 98 99 9A 9B 9C 9D 9E 9F A0 A1 A2 A3 A4 A5 A6 A7 A8 A9 AA AB AC AD AE AF B0 B1 B2 B3 B4 B5 B6 B7 B8 B9 BA BB BC BD BE BF C0 C1 C2 C3 C4 C5 C6 C7 C8 C9 CA CB CC CD CE CF D0 D1 D2 D3 D4 D5 D6 D7 D8 D9 DA DB DC DD DE DF E0 E1 E2 E3 E4 E5 E6 E7 E8 E9 EA EB EC ED EE EF F0 F1 F2 F3 D2 AA"));


            int done;
            byte[][] datagrams;
            byte[] unFinished = { };
            for (int i = 0; i < dataList.Count; i++)
            {
                var data = dataList[i];
                if (!UtilityCollection.IsNullOrEmpty(unFinished))
                {
                    // 当有半包数据时，进行接包操作
                    data = unFinished.Concat(data).ToArray();
                }

                Trace.WriteLine(string.Format("当前数据长度：{0}", data.Length));

                datagrams = decoder.Execute(data, out done);
                if (i < dataList.Count - 1) //没到最后一个
                {
                    Assert.AreEqual(0, datagrams.Length);
                    Assert.AreEqual(0, done);

                    unFinished = new byte[data.Length - done];
                    Buffer.BlockCopy(data, done, unFinished, 0, unFinished.Length);
                }
                else //最后一个
                {
                    Assert.AreEqual(1, datagrams.Length);
                    Assert.AreEqual(data.Length-1, done);
                }
            }
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

            src.AddRange(NangleCodecUtility.ConvertFromIntToTwoBytes(len)); //长度
            src.AddRange(GetTargetAddress()); //目标地址
            src.AddRange(GetAnyCommand()); //命令
            src.AddRange(data); //数据
            var tempBytes = new byte[len - 1];
            src.CopyTo(2, tempBytes, 0, len - 1);
            src.Add(NangleCodecUtility.GetOneByteChk(tempBytes));
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

        protected byte[] GetNoiseBytes()
        {
            return Encoding.Default.GetBytes("noise");
        }


    }
}
