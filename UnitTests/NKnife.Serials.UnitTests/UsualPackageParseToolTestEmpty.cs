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
        /// һ����Ч����
        /// </summary>
        [Fact]
        public void Test_Empty_01()
        {
            var pt = new UsualVoucherToolStub();
            var msg = Encoding.Default.GetBytes("�����õ���һ����һ����Ч���ݡ�");
            var v = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref v);
            success.Should().BeFalse();
            pt.SegmentSource.Should().BeNull(); //�ް��
        }

        /// <summary>
        /// ������
        /// </summary>
        [Fact]
        public void Test_Empty_02()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                //������
            };

            var v = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref v);
            success.Should().BeFalse();
            pt.SegmentSource.Should().BeNull(); //�ް��
        }

        /// <summary>
        /// ������
        /// </summary>
        [Fact]
        public void Test_Empty_03()
        {
            var pt = new UsualVoucherToolStub();
            //������
            var v = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(), ref v);
            success.Should().BeFalse();
            pt.SegmentSource.Should().BeNull(); //�ް��
        }

    }
}