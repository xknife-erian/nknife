using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NKnife.Serials.ParseTools;
using NKnife.Serials.UnitTests.Stub;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTestEmpty
    {
        /// <summary>
        /// 一堆无效数据
        /// </summary>
        [Fact]
        public void Test_Empty_01()
        {
            var pt = new UsualVoucherToolStub();
            var msg = Encoding.Default.GetBytes("这样得到的一定是一堆无效数据。");
            var v = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref v);
            success.Should().BeFalse();
            pt.SegmentSource.Should().BeNull(); //无半包
        }

        /// <summary>
        /// 空数据
        /// </summary>
        [Fact]
        public void Test_Empty_02()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                //空数据
            };

            var v = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref v);
            success.Should().BeFalse();
            pt.SegmentSource.Should().BeNull(); //无半包
        }

        /// <summary>
        /// 空数据
        /// </summary>
        [Fact]
        public void Test_Empty_03()
        {
            var pt = new UsualVoucherToolStub();
            //空数据
            var v = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(), ref v);
            success.Should().BeFalse();
            pt.SegmentSource.Should().BeNull(); //无半包
        }

    }
}