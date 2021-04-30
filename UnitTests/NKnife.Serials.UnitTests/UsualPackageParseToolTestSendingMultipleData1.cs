using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using FluentAssertions;
using NKnife.CRC;
using NKnife.Interface;
using NKnife.Serials.UnitTests.Examples;
using NKnife.Serials.UnitTests.Stub;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTestSendingMultipleData1
    {
        private static readonly Random _Random = new Random((int)DateTime.Now.Ticks);
        private ICRCProvider _crcProvider;

        private void InitialiseCRCProvider()
        {
            if (_crcProvider == null)
            {
                var factory = new CRCFactory();
                _crcProvider = factory.CreateProvider(CRCProviderMode.CRC16_Modbus);
            }
        }

        #region Sending Multiple Data

        /// <summary>
        /// 分条发送。第一条完整，第二条被拆分。
        /// </summary>
        [Fact]
        public void Test_Sending_Multiple_Data_01()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg = GetSimpleMessage();

            var n = msg.Length / 3;
            var msg1 = new byte[n];
            var msg2 = new byte[n];
            var msg3 = new byte[msg.Length - msg1.Length - msg2.Length];

            Buffer.BlockCopy(msg, 0, msg1, 0, msg1.Length);
            Buffer.BlockCopy(msg, msg1.Length, msg2, 0, msg2.Length);
            Buffer.BlockCopy(msg, msg1.Length + msg2.Length, msg3, 0, msg3.Length);

            tool.Run(msg);
            tool.Results.Count.Should().Be(1);

            tool.Run(msg1);
            tool.Results.Count.Should().Be(1);

            tool.Run(msg2);
            tool.Results.Count.Should().Be(1);

            tool.Run(msg3);
            tool.TrueCount.Should().Be(2);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results.Count.Should().Be(2);
        }

        private byte[] GetSimpleMessage()
        {
            var bs = new List<byte>();
            bs.Add(0xAA);
            bs.Add(0xEE);
            var data = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x01, 0x02, 0x03, 0x04, 0x01, 0x02, 0x03, 0x04 };
            var length = BitConverter.GetBytes((short)data.Length);
            bs.Add(length[0]);
            bs.Add(length[1]);
            bs.AddRange(data);
            var crc = _crcProvider.CRCheck(bs.ToArray());
            bs.Add(crc[0]);
            bs.Add(crc[1]);
            bs.Add(0xCC);
            return bs.ToArray();
        }

        /// <summary>
        /// 分条发送。第一条完整，第二条被拆分。
        /// </summary>
        [Fact]
        public void Test_Sending_Multiple_Data_02()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg = GetLongMessage(0);

            var n = msg.Length / 3;
            var msg1 = new byte[n];
            var msg2 = new byte[n];
            var msg3 = new byte[msg.Length - msg1.Length - msg2.Length];

            Buffer.BlockCopy(msg, 0, msg1, 0, msg1.Length);
            Buffer.BlockCopy(msg, msg1.Length, msg2, 0, msg2.Length);
            Buffer.BlockCopy(msg, msg1.Length + msg2.Length, msg3, 0, msg3.Length);

            tool.Run(msg);
            tool.Results.Count.Should().Be(1);

            tool.Run(msg1);
            tool.Results.Count.Should().Be(1);

            tool.Run(msg2);
            tool.Results.Count.Should().Be(1);

            tool.Run(msg3);
            tool.TrueCount.Should().Be(2);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results.Count.Should().Be(2);
        }

        private byte[] GetLongMessage(short index)
        {
            var bs = new List<byte>();
            bs.Add(0xAA);
            bs.Add(BitConverter.GetBytes(index)[0]);
            var dataSrc = Encoding.Default.GetBytes("这串文字将转成一个字节数组，然后被乱序后使用。");
            var data = Shuffle(dataSrc);
            var length = BitConverter.GetBytes((short)data.Length);
            bs.Add(length[0]);
            bs.Add(length[1]);
            bs.AddRange(data);
            var crc = _crcProvider.CRCheck(bs.ToArray());
            bs.Add(crc[0]);
            bs.Add(crc[1]);
            bs.Add(0xCC);
            return bs.ToArray();
        }

        /// <summary>
        /// 用洗牌算法打乱一个数组内部的顺序
        /// </summary>
        private static byte[] Shuffle(byte[] src)
        {
            for (int i = 0; i < src.Length; i++)
            {
                int x, y;
                x = _Random.Next(0, src.Length);
                do
                {
                    y = _Random.Next(0, src.Length);
                } while (y == x);

                src[x] = (byte) (src[x]^src[y]);
                src[y] = (byte) (src[x]^src[y]);
                src[x] = (byte) (src[x]^src[y]);
            }
            return src;
        }

        /// <summary>
        /// 将一条数据分条发送。将一条拆分成了多条发送。即断包。
        /// </summary>
        [Fact]
        public void Test_Sending_Multiple_Data_03()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg1 = new byte[] {0xAA,}; //只有起始字符
            var msg2 = new byte[] {0xEE, 0x04, 0x00, 0x01, 0x02};
            var msg3 = new byte[] {0x03, 0x04, 0x64, 0x89, 0xCC};

            tool.Run(msg1);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg2);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg3);
            tool.TrueCount.Should().Be(1);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results.Count.Should().Be(1);
            tool.Results[0].Item1.Should().Be(0xEE);
            tool.Results[0].Item2.Count.Should().Be(4);
            for (int i = 0; i < tool.Results[0].Item2.Count; i++)
            {
                tool.Results[0].Item2[i].Should().Be((byte)(i + 1));
            }
        }

        /// <summary>
        /// 将一条数据分条发送。将一条拆分成了多条发送。即断包。
        /// </summary>
        [Fact]
        public void Test_Sending_Multiple_Data_04()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg1 = new byte[] {0xAA, 0xEE}; //只有起始字符和命令字
            var msg2 = new byte[] {0x04, 0x00, 0x01, 0x02};
            var msg3 = new byte[] {0x03, 0x04, 0x64, 0x89, 0xCC};

            tool.Run(msg1);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg2);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg3);
            tool.TrueCount.Should().Be(1);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results.Count.Should().Be(1);
            tool.Results[0].Item1.Should().Be(0xEE);
            tool.Results[0].Item2.Count.Should().Be(4);
            for (int i = 0; i < tool.Results[0].Item2.Count; i++)
            {
                tool.Results[0].Item2[i].Should().Be((byte)(i + 1));
            }
        }

        /// <summary>
        /// 将一条数据分条发送。将一条拆分成了多条发送。即断包。
        /// </summary>
        [Fact]
        public void Test_Sending_Multiple_Data_05()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg1 = new byte[] {0xAA, 0xEE, 0x04}; //长度字节被断掉
            var msg2 = new byte[] {0x00, 0x01, 0x02};
            var msg3 = new byte[] {0x03, 0x04, 0x64, 0x89, 0xCC};

            tool.Run(msg1);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg2);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg3);
            tool.TrueCount.Should().Be(1);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results.Count.Should().Be(1);
            tool.Results[0].Item1.Should().Be(0xEE);
            tool.Results[0].Item2.Count.Should().Be(4);
            for (int i = 0; i < tool.Results[0].Item2.Count; i++)
            {
                tool.Results[0].Item2[i].Should().Be((byte)(i + 1));
            }
        }

        /// <summary>
        /// 将一条数据分条发送。将一条拆分成了多条发送。即断包。
        /// </summary>
        [Fact]
        public void Test_Sending_Multiple_Data_06()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg1 = new byte[] {0xAA, 0xEE, 0x04, 0x00};//正确的头部
            var msg2 = new byte[] {0x01, 0x02, 0x03, 0x04};//正确的数据域
            var msg3 = new byte[] {0x64, 0x89, 0xCC};//正确的尾部

            tool.Run(msg1);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg2);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg3);
            tool.TrueCount.Should().Be(1);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results.Count.Should().Be(1);
            tool.Results[0].Item1.Should().Be(0xEE);
            tool.Results[0].Item2.Count.Should().Be(4);
            for (int i = 0; i < tool.Results[0].Item2.Count; i++)
            {
                tool.Results[0].Item2[i].Should().Be((byte)(i + 1));
            }
        }

        /// <summary>
        /// 将一条数据分条发送。将一条拆分成了多条发送。即断包。
        /// </summary>
        [Fact]
        public void Test_Sending_Multiple_Data_07()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg1 = new byte[] { 0xAA, 0xEE, 0x04, 0x00 }; //头部是正确的
            var msg2 = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x64 };
            var msg3 = new byte[] { 0x89, 0xCC };

            tool.Run(msg1);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg2);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg3);
            tool.TrueCount.Should().Be(1);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results.Count.Should().Be(1);
            tool.Results[0].Item1.Should().Be(0xEE);
            tool.Results[0].Item2.Count.Should().Be(4);
            for (int i = 0; i < tool.Results[0].Item2.Count; i++)
            {
                tool.Results[0].Item2[i].Should().Be((byte)(i + 1));
            }
        }

        /// <summary>
        /// 将一条数据分条发送。将一条拆分成了多条发送。即断包。
        /// </summary>
        [Fact]
        public void Test_Sending_Multiple_Data_08()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg1 = new byte[] {0xAA, 0xEE, 0x04,}; //头部是正确的
            var msg2 = new byte[] {0x00,};
            var msg3 = new byte[] {0x01,};
            var msg4 = new byte[] {0x02, 0x03};
            var msg5 = new byte[] {0x04};
            var msg6 = new byte[] {0x64};
            var msg7 = new byte[] {0x89, 0xCC};

            tool.Run(msg1);
            tool.Results.Count.Should().Be(0);
            tool.Run(msg2);
            tool.Results.Count.Should().Be(0);
            tool.Run(msg3);
            tool.Results.Count.Should().Be(0);
            tool.Run(msg4);
            tool.Results.Count.Should().Be(0);
            tool.Run(msg5);
            tool.Results.Count.Should().Be(0);
            tool.Run(msg6);
            tool.Results.Count.Should().Be(0);
            tool.Run(msg7);
            tool.TrueCount.Should().Be(1);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results.Count.Should().Be(1);
            tool.Results[0].Item1.Should().Be(0xEE);
            tool.Results[0].Item2.Count.Should().Be(4);
            for (int i = 0; i < tool.Results[0].Item2.Count; i++)
            {
                tool.Results[0].Item2[i].Should().Be((byte) (i + 1));
            }
        }

        /// <summary>
        /// 将一条数据分条发送。将一条拆分成了多条发送。即断包。
        /// </summary>
        [Fact]
        public void Test_Sending_Multiple_Data_09()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg1 = new byte[] { 0xAA, 0xEE, 0x04, 0x00 }; //头部是正确的
            var msg2 = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x64, 0x89 };
            var msg3 = new byte[] { 0xCC };

            tool.Run(msg1);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg2);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg3);
            tool.TrueCount.Should().Be(1);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results.Count.Should().Be(1);
            tool.Results[0].Item1.Should().Be(0xEE);
            tool.Results[0].Item2.Count.Should().Be(4);
            for (int i = 0; i < tool.Results[0].Item2.Count; i++)
            {
                tool.Results[0].Item2[i].Should().Be((byte)(i + 1));
            }
        }

        /// <summary>
        /// 将一条11个字节的数据分成一个一个字节发送。最极端的断包。
        /// </summary>
        [Fact]
        public void Test_Sending_Multiple_Data_10()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg01 = new byte[] { 0xAA }; 
            var msg02 = new byte[] { 0xEE }; 
            var msg03 = new byte[] { 0x04 }; 
            var msg04 = new byte[] { 0x00 }; 
            var msg05 = new byte[] { 0x01 };
            var msg06 = new byte[] { 0x02 };
            var msg07 = new byte[] { 0x03 };
            var msg08 = new byte[] { 0x04 };
            var msg09 = new byte[] { 0x64 };
            var msg10 = new byte[] { 0x89 };
            var msg11 = new byte[] { 0xCC };

            tool.Run(msg01);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg02);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg03);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg04);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg05);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg06);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg07);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg08);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg09);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg10);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg11);
            tool.Results.Count.Should().Be(1);

            tool.TrueCount.Should().Be(1);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results[0].Item1.Should().Be(0xEE);
            tool.Results[0].Item2.Count.Should().Be(4);
            for (int i = 0; i < tool.Results[0].Item2.Count; i++)
            {
                tool.Results[0].Item2[i].Should().Be((byte)(i + 1));
            }
        }

        #endregion
    }
}