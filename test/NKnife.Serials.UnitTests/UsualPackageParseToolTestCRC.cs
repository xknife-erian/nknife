using System;
using System.Collections.Generic;
using FluentAssertions;
using NKnife.Serials.ParseTools;
using NKnife.Serials.UnitTests.Stub;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTestCRC
    {
        #region CRC

        /// <summary>
        /// 正常包，错误的CRC
        /// </summary>
        [Fact]
        public void Test_CRC_01()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC
            };
            var vouchers = new List<IVoucher>(1);
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vouchers);
            vouchers.Count.Should().Be(0);
            success.Should().BeFalse();
            pt.SegmentSource.Should().BeNull();
        }

        /// <summary>
        /// 正常包，错误的CRC
        /// </summary>
        [Fact]
        public void Test_CRC_02()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
            };
            var vouchers = new List<IVoucher>(1);
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vouchers);
            vouchers.Count.Should().Be(0);
            success.Should().BeFalse();
            pt.SegmentSource.Should().BeNull();
        }

        /// <summary>
        /// 正常包，错误的CRC
        /// </summary>
        [Fact]
        public void Test_CRC_03()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
                0xAA,
                0xBB, //命令字
                0x02, 0x00, //数据域长度
                0x44, 0x55, //数据域
                0x0E, 0x8D, //CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
            };
            var vouchers = new List<IVoucher>(1);
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vouchers);
            success.Should().BeTrue();
            vouchers.Count.Should().Be(1);
            vouchers[0].Command[0].Should().Be(0xBB);
            vouchers[0].DataField[0].Should().Be(0x44);
            vouchers[0].DataField[1].Should().Be(0x55);
            pt.SegmentSource.Should().BeNull();
        }

        /// <summary>
        /// 正常包，错误的CRC
        /// </summary>
        [Fact]
        public void Test_CRC_04()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
            };
            var vouchers = new List<IVoucher>(1);
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vouchers);
            vouchers.Count.Should().Be(1);
            success.Should().BeTrue();
            foreach (var voucher in vouchers)
            {
                foreach (var b in voucher.DataField)
                {
                    b.Should().Be(0x00);
                }
            }
            pt.SegmentSource.Should().BeNull();
        }

        /// <summary>
        /// 正常包，错误的CRC
        /// </summary>
        [Fact]
        public void Test_CRC_05()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC,
            };
            var vouchers = new List<IVoucher>(1);
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vouchers);
            vouchers.Count.Should().Be(1);
            success.Should().BeTrue();
            foreach (var voucher in vouchers)
            {
                foreach (var b in voucher.DataField)
                {
                    b.Should().Be(0x00);
                }
            }
            pt.SegmentSource.Should().BeNull();
        }

        /// <summary>
        /// 正常包，错误的CRC
        /// </summary>
        [Fact]
        public void Test_CRC_06()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC,
            };
            var vouchers = new List<IVoucher>(1);
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vouchers);
            vouchers.Count.Should().Be(3);
            success.Should().BeTrue();
            foreach (var voucher in vouchers)
            {
                voucher.Command[0].Should().Be(0xFE);
                foreach (var b in voucher.DataField)
                {
                    b.Should().Be(0x00);
                }
            }
            pt.SegmentSource.Should().BeNull();
        }

        /// <summary>
        /// 正常包，错误的CRC
        /// </summary>
        [Fact]
        public void Test_CRC_07()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
            };
            var vouchers = new List<IVoucher>(1);
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vouchers);
            vouchers.Count.Should().Be(0);
            success.Should().BeFalse();
            pt.SegmentSource.Should().BeNull();
        }

        /// <summary>
        /// 正常包，错误的CRC。跳过CRC校验。
        /// </summary>
        [Fact]
        public void Test_CRC_08()
        {
            var pt = new UsualVoucherToolStub {SkipCRC = true}; //跳过CRC校验

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x11, 0x11, //错误的CRC
                0xCC,
            };
            var vouchers = new List<IVoucher>(1);
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vouchers);
            success.Should().BeTrue();
            vouchers.Count.Should().Be(3);
            foreach (var voucher in vouchers)
            {
                voucher.Command[0].Should().Be(0xFE);
                foreach (var b in voucher.DataField)
                {
                    b.Should().Be(0x00);
                }
            }
            pt.SegmentSource.Should().BeNull();
        }

        #endregion
    }
}