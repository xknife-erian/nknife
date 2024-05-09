using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using FluentAssertions;
using NKnife.CRC;
using NKnife.Serials.ParseTools;
using NKnife.Serials.UnitTests.Examples;
using NKnife.Serials.UnitTests.Stub;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTestFull
    {
        /// <summary>
        /// ������
        /// </summary>
        [Fact]
        public void Test_Full_01()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30, 0x7D, //CRC
                0xCC
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();
            pt.SegmentSource.Should().BeNull();
        }

        /// <summary>
        /// ǰ���ж����ֽڣ�������ʣ���������İ���
        /// </summary>
        [Fact]
        public void Test_Full_02()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0x00, //ǰ���ж����ֽ�
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30, 0x7D, //CRC
                0xCC
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();
            pt.SegmentSource.Should().BeNull();
        }

        /// <summary>
        /// β���ж����ֽڡ����������İ���ͬʱ������ֽ��ṩ�µĺ���ʹ�á�
        /// </summary>
        [Fact]
        public void Test_Full_03()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30, 0x7D, //CRC
                0xCC,
                0x00, 0x01, 0x02, //β���ж����ֽ�, ��Ȼ��Ч�������׳����������������д���
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();
            pt.SegmentSource.Should().BeNull();
        }

        /// <summary>
        /// ���������İ�ճ����һ��
        /// </summary>
        [Fact]
        public void Test_Full_04()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30, 0x7D, //CRC
                0xCC,
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30, 0x7D, //CRC
                0xCC,
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();
            pt.SegmentSource.Should().BeNull(); //��δ�������ݣ�Ӧ��Ϊ�ա�
        }

        /// <summary>
        /// ���������İ�ճ����һ��
        /// </summary>
        [Fact]
        public void Test_Full_05()
        {
            var pt = new UsualVoucherToolStub();

            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30, 0x7D, //CRC
                0xCC,
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30, 0x7D, //CRC
                0xCC,
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x00, 0x00, //������
                0x30, 0x7D, //CRC
                0xCC,
            };

            var vs = new List<IVoucher>();
            var success = pt.TryParse(new ArraySegment<byte>(msg), ref vs);
            foreach (var v in vs)
            {
                v.Command[0].Should().Be(0xFE);
                v.DataField.Count.Should().Be(2);
                foreach (var b in v.DataField)
                    b.Should().Be(0x00);
            }
            success.Should().BeTrue();
            pt.SegmentSource.Should().BeNull(); //��δ�������ݣ�Ӧ��Ϊ�ա�
        }

        /// <summary>
        /// ����������ȷ��CRC����������������
        /// </summary>
        [Fact]
        public void Test_Full_06()
        {
            var msg = new byte[]
            {
                0xAA,
                0xFE, //������
                0x02, 0x00, //�����򳤶�
                0x88, 0x89, //������
                0x97, 0xDB, //CRC
                0xCC,
                //
                0xAA,
                0xCD, //������
                0x02, 0x00, //�����򳤶�
                0x77, 0x78, //������
                0x13, 0xAB, //CRC
                0xCC
            };

            var tool = new UsePackageToolExample();
            tool.Run(msg);
            tool.Results.Count.Should().Be(2);

            var result0 = tool.Results[0];
            result0.Item1.Should().Be(0xFE);
            var pk1 = result0.Item2;
            pk1.Count.Should().Be(2);
            pk1[0].Should().Be(0x88);
            pk1[1].Should().Be(0x89);

            var result1 = tool.Results[1];
            result1.Item1.Should().Be(0xCD);
            var pk2 = result1.Item2;
            pk2.Count.Should().Be(2);
            pk2[0].Should().Be(0x77);
            pk2[1].Should().Be(0x78);
        }

    }
}
