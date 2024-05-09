using System;
using System.Collections.Generic;
using FluentAssertions;
using NKnife.Serials.ParseTools;
using NKnife.Serials.UnitTests.Stub;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTestHalf
    {

        #region Half

        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_01()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30, 0x7D, //CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

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
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_02()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30 //CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

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
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_03()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x01, 0x02, //������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeTrue();
            length.Should().Be(2);
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(6);
            for (int i = 0; i < pt.SegmentSource.DataField.Count; i++)
            {
                pt.SegmentSource.DataField.At(i).Should().Be((byte)(i+1));
            }
        }


        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_04()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0xBB, //����������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeTrue();
            length.Should().Be(2);
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(5);
            foreach (var t in pt.SegmentSource.DataField)
            {
                t.Should().Be(0xBB);
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_05()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                //����������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeTrue();
            length.Should().Be(2);
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(4);
            foreach (var t in pt.SegmentSource.DataField)
            {
                t.Should().Be(0xBB);
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_06()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, //�����򳤶Ȳ�����
                //��������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeFalse();
            length.Should().Be(-1);
        }

        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_07()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                //�������򳤶�
                //��������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeFalse();
            length.Should().Be(-1);
        }

        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_08()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                //��������
                //�������򳤶�
                //��������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            success.Should().BeFalse();

            pt.SegmentSource.Should().NotBeNull();
        }

        //--------------------------------

        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_09()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xF0, 0xF1, 0xF2,
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30, 0x7D, //CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

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
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_10()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xF0, 0xF1, 0xF2,
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30 //CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

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
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_11()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xF0, 0xF1, 0xF2,
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

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
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_12()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xF0, 0xF1, 0xF2,
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0xBB, //����������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

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
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_13()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xF0, 0xF1, 0xF2,
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                //����������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeTrue();
            length.Should().Be(2);
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(4);
            pt.SegmentSource.DataField.Count.Should().Be(0);
        }

        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_14()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xF0, 0xF1, 0xF2,
                0xAA,
                0xFE, //������
                0x02, //�����򳤶Ȳ�����
                //��������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(3);
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeFalse();
            length.Should().Be(-1);
        }

        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_15()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xF0, 0xF1, 0xF2,
                0xAA,
                0xFE, //������
                //�������򳤶�
                //��������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.Command[0].Should().Be(0xFE);
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(2);
            pt.SegmentSource.TryGetDataFieldLength(out var length).Should().BeFalse();
            length.Should().Be(-1);
        }

        /// <summary>
        /// ���
        /// </summary>
        [Fact]
        public void Test_Half_16()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xF0, 0xF1, 0xF2,
                0xAA,
                //��������
                //�������򳤶�
                //��������
                //��CRC
                //�޽�����
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg),ref vs);
            success.Should().BeFalse();

            pt.SegmentSource.Should().NotBeNull();
            pt.SegmentSource.CountCurrentlyWritten.Should().Be(1);
        }

        #endregion

    }
}