using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using FluentAssertions;
using NKnife.CRC;
using NKnife.Interface;
using NKnife.Serials.ParseTools;
using NKnife.Serials.UnitTests.Examples;
using NKnife.Serials.UnitTests.Stub;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTestImpurity
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

        #region Impurity

        /// <summary>
        /// 两包中间有杂质
        /// </summary>
        [Fact]
        public void Test_Impurity_InBetweenTheTwoBags()
        {
            var pt = new UsualVoucherToolStub();

            var bytes = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC,
            };

            var list = new List<byte>();
            list.AddRange(bytes);
            list.AddRange(Encoding.Default.GetBytes("两包中间有杂质"));
            list.AddRange(bytes);

            var vs = new List<IVoucher>();
            pt.TryParse(new ArraySegment<byte>(list.ToArray()), ref vs);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            vs.Count.Should().Be(2);
            pt.SegmentSource.Should().BeNull(); //无未解析数据，应当为空。
        }

        /// <summary>
        /// 前中后均有杂质
        /// </summary>
        [Fact]
        public void Test_Impurities_InTheFrontMiddleAndBack_1()
        {
            var pt = new UsualVoucherToolStub();

            var bytes = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC,
            };

            var list = new List<byte>();
            list.AddRange(Encoding.Default.GetBytes("前面有杂质"));
            list.AddRange(bytes);
            list.AddRange(Encoding.Default.GetBytes("中间有杂质"));
            list.AddRange(bytes);
            list.AddRange(Encoding.Default.GetBytes("后面有杂质"));

            var vs = new List<IVoucher>();
            pt.TryParse(new ArraySegment<byte>(list.ToArray()), ref vs);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            vs.Count.Should().Be(2);

            pt.SegmentSource.Should().BeNull(); //无未解析数据，应当为空。
        }

        /// <summary>
        /// 一包数据分成三包发送，但前后有杂质
        /// </summary>
        [Fact]
        public void Test_Impurities_InTheFrontAndBack_2()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();

            var msg1 = new byte[] {0xAA}; //只有起始字符
            var msg2 = new byte[] {0xEE, 0x04, 0x00, 0x01, 0x02};
            var msg3 = new byte[] {0x03, 0x04, 0x64, 0x89, 0xCC};

            tool.Run(Guid.NewGuid().ToByteArray());//杂质
            tool.Results.Count.Should().Be(0);

            tool.Run(msg1);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg2);
            tool.Results.Count.Should().Be(0);

            tool.Run(msg3);
            tool.Results.Count.Should().Be(1);
            tool.TrueCount.Should().Be(1);
            tool.TrueAtLastast.Should().BeTrue();
            
            tool.Run(Guid.NewGuid().ToByteArray());//杂质
            tool.Results.Count.Should().Be(1);
            tool.Results[0].Item1.Should().Be(0xEE);
            tool.Results[0].Item2.Count.Should().Be(4);
            for (int i = 0; i < tool.Results[0].Item2.Count; i++)
            {
                tool.Results[0].Item2[i].Should().Be((byte) (i + 1));
            }
        }

        
        [Fact]
        public void Test_Impurities_InsideTheBag_InTheFrontAndBack()
        {
            var tool = new UsualVoucherToolStub();
            var list = new List<byte>();
            var correct = new byte[]
            {
                0xAA, //起始
                0xEE, //命令字
                0x06, 0x00, //长度
                0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, //数据域
                0x20, 0xB7, //CRC
                0xCC //结尾
            };
            list.AddRange(Guid.NewGuid().ToByteArray());
            list.AddRange(correct);
            list.AddRange(Guid.NewGuid().ToByteArray());

            var vs = new List<IVoucher>(1);
            var success = tool.TryParse(list.ToArray(), ref vs);
            success.Should().BeTrue();
            // 无有效数据可供解析，第三包数据也将丢弃
            tool.SegmentSource.Should().BeNull();
            vs.Count.Should().Be(1);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xEE);
                for (int i = 0; i < v.DataField.Count; i++)
                {
                    v.DataField.At(i).Should().Be(correct[i + 4]);
                }
            }
        }

        #endregion
    }
}