using System;
using System.Collections.Generic;
using FluentAssertions;
using NKnife.Serials.ParseTools;
using NKnife.Serials.UnitTests.Stub;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTestOneAndHalf
    {

        #region One_And_A_Half

        /// <summary>
        /// 半包
        /// </summary>
        [Fact]
        public void Test_One_And_A_Half_01()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC, //结束符

                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                //无结束符
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            vs.Count.Should().Be(1);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeTrue();
            length.Should().Be(2);
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(8);
            foreach (var t in pt.SegmentSource.DataField)
            {
                t.Should().Be(0x00);
            }
        }

        /// <summary>
        /// 半包
        /// </summary>
        [Fact]
        public void Test_One_And_A_Half_02()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC, //结束符

                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30 //CRC
                //无结束符
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            vs.Count.Should().Be(1);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeTrue();
            length.Should().Be(2);
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(7);
            foreach (var t in pt.SegmentSource.DataField)
            {
                t.Should().Be(0x00);
            }
        }

        /// <summary>
        /// 半包
        /// </summary>
        [Fact]
        public void Test_One_And_A_Half_03()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC, //结束符

                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                //无CRC
                //无结束符
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            vs.Count.Should().Be(1);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeTrue();
            length.Should().Be(2);
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(6);
            foreach (var t in pt.SegmentSource.DataField)
            {
                t.Should().Be(0x00);
            }
        }


        /// <summary>
        /// 半包
        /// </summary>
        [Fact]
        public void Test_One_And_A_Half_04()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC, //结束符

                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0xBB, //数据域不完整
                //无CRC
                //无结束符
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            vs.Count.Should().Be(1);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeTrue();
            length.Should().Be(2);
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(5);
            pt.SegmentSource.DataField.Count.Should().Be(1);
            foreach (var t in pt.SegmentSource.DataField)
            {
                t.Should().Be(0xBB);
            }
        }

        /// <summary>
        /// 半包
        /// </summary>
        [Fact]
        public void Test_One_And_A_Half_05()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC, //结束符

                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                //数据域不完整
                //无CRC
                //无结束符
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            vs.Count.Should().Be(1);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeTrue();
            length.Should().Be(2);
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(4);
            pt.SegmentSource.DataField.Count.Should().Be(0);
        }

        /// <summary>
        /// 半包
        /// </summary>
        [Fact]
        public void Test_One_And_A_Half_06()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC, //结束符

                0xAA,
                0xFE, //命令字
                0x02, //数据域长度不完整
                //无数据域
                //无CRC
                //无结束符
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            vs.Count.Should().Be(1);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeFalse();
            length.Should().Be(-1);
        }

        /// <summary>
        /// 半包
        /// </summary>
        [Fact]
        public void Test_One_And_A_Half_07()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC, //结束符

                0xAA,
                0xFE, //命令字
                //无数据域长度
                //无数据域
                //无CRC
                //无结束符
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            vs.Count.Should().Be(1);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeFalse();
            length.Should().Be(-1);
        }

        /// <summary>
        /// 半包
        /// </summary>
        [Fact]
        public void Test_One_And_A_Half_08()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //命令字
                0x02, 0x00, //数据域长度
                0x00, 0x00, //数据域
                0x30, 0x7D, //CRC
                0xCC, //结束符

                0xAA,
                //无命令字
                //无数据域长度
                //无数据域
                //无CRC
                //无结束符
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            vs.Count.Should().Be(1);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();
            pt.SegmentSource.Should().NotBeNull();
        }

        #endregion
    }
}