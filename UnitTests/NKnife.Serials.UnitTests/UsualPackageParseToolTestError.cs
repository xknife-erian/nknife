using System;
using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using NKnife.CRC;
using NKnife.Serials.ParseTools;
using NKnife.Serials.UnitTests.Stub;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTestError
    {
        private static readonly Random _Random = new Random((int)DateTime.Now.Ticks);

        [Fact]
        public void Test_01()
        {
            var tool = new UsualVoucherToolStub();
            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, 0xDD, //数据域长度不对
                0x30, 0x7D, //CRC
                0xCC
            };
            var vs = new List<IVoucher>(1);
            var success = tool.TryParse(msg, ref vs);
            tool.SegmentSource.Should().BeNull();
            success.Should().BeFalse();
            vs.Count.Should().Be(0);
        }

        [Fact]
        public void Test_02()
        {
            var tool = new UsualVoucherToolStub();
            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, //数据域长度不对
                0x30, 0x7D, //CRC
                0xCC, 0xCC
            };

            var vs = new List<IVoucher>(1);
            var success = tool.TryParse(msg, ref vs);
            tool.SegmentSource.Should().BeNull();
            success.Should().BeFalse();
            vs.Count.Should().Be(0);
        }

        [Fact]
        public void Test_03()
        {
            var tool = new UsualVoucherToolStub();
            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00,//数据域
                0x30, 0x7D, //CRC
                0xCC, 0xCC, //开始出现错误
                0xAA, 0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00,//数据域
                0x30, 0x7D, //CRC
                0xCC, 0xCC
            };
            var vs = new List<IVoucher>(1);
            var success = tool.TryParse(msg, ref vs);
            tool.SegmentSource.Should().NotBeNull();
            success.Should().BeTrue();
            vs.Count.Should().Be(1);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                foreach (var b in v.DataField)
                {
                    b.Should().Be(0x00);
                }
            }
        }
    }
}