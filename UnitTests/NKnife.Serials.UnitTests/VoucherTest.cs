using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NKnife.Serials.UnitTests.Stub;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class VoucherTest
    {
        [Fact]
        public void TryGetDataFieldLength_Test__01()
        {
            var voucher = new VoucherStub();
            voucher.AppendHead(new ArraySegment<byte>(new byte[]
            {
                0xAA, 0xFE, 0x02, 0x00
            }));
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeTrue();
            length.Should().Be(2);
        }

        [Fact]
        public void TryGetDataFieldLength_Test__02()
        {
            const short n = -4321;
            var voucher = new VoucherStub();
            var head = new List<byte>(new byte[]{0xAA, 0xFE});
            var t = BitConverter.GetBytes(n);
            head.AddRange(t);
            voucher.AppendHead(new ArraySegment<byte>(head.ToArray()));
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeFalse();
            length.Should().Be(-3);
        }

        [Fact]
        public void TryGetDataFieldLength_Test__03()
        {
            const short n = 1024;
            var voucher = new VoucherStub();
            var head = new List<byte>(new byte[] {0xAA, 0xFE});
            var t = BitConverter.GetBytes(n);
            head.AddRange(t);
            voucher.AppendHead(new ArraySegment<byte>(head.ToArray()));
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeTrue();
            length.Should().Be(1024);
        }

        [Fact]
        public void TryGetDataFieldLength_Test__04()
        {
            const short n = 1024 + 1;
            var voucher = new VoucherStub();
            var head = new List<byte>(new byte[] {0xAA, 0xFE});
            var t = BitConverter.GetBytes(n);
            head.AddRange(t);
            voucher.AppendHead(new ArraySegment<byte>(head.ToArray()));
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeFalse();
            length.Should().Be(-2);
        }

        [Fact]
        public void TryGetDataFieldLength_Test__05()
        {
            var voucher = new VoucherStub();
            voucher.AppendHead(new ArraySegment<byte>(new byte[]
            {
                0xAA, 0xFE, 0x02
            }));
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeFalse();
            length.Should().Be(-1);
        }

        [Fact]
        public void TryGetDataFieldLength_Test__06()
        {
            var voucher = new VoucherStub();
            voucher.AppendHead(new ArraySegment<byte>(new byte[] { }));
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeFalse();
            length.Should().Be(-1);
        }

        
        [Fact]
        public void TryGetDataFieldLength_Test__07()
        {
            const short n = 0;
            var voucher = new VoucherStub();
            var head = new List<byte>(new byte[] {0xAA, 0xFE});
            var t = BitConverter.GetBytes(n);
            head.AddRange(t);
            voucher.AppendHead(new ArraySegment<byte>(head.ToArray()));
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeTrue();
            length.Should().Be(0);
        }

                
        [Fact]
        public void TryGetDataFieldLength_Test__08()
        {
            const short n = -1;
            var voucher = new VoucherStub();
            var head = new List<byte>(new byte[] {0xAA, 0xFE});
            var t = BitConverter.GetBytes(n);
            head.AddRange(t);
            voucher.AppendHead(new ArraySegment<byte>(head.ToArray()));
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeFalse();
            length.Should().Be(-3);
        }

                
        [Fact]
        public void TryGetDataFieldLength_Test__09()
        {
            const short n = -2;
            var voucher = new VoucherStub();
            var head = new List<byte>(new byte[] {0xAA, 0xFE});
            var t = BitConverter.GetBytes(n);
            head.AddRange(t);
            voucher.AppendHead(new ArraySegment<byte>(head.ToArray()));
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeFalse();
            length.Should().Be(-3);
        }

                
        [Fact]
        public void TryGetDataFieldLength_Test__10()
        {
            const short n = -3;
            var voucher = new VoucherStub();
            var head = new List<byte>(new byte[] {0xAA, 0xFE});
            var t = BitConverter.GetBytes(n);
            head.AddRange(t);
            voucher.AppendHead(new ArraySegment<byte>(head.ToArray()));
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeFalse();
            length.Should().Be(-3);
        }

        [Fact]
        public void TryGetDataFieldLength_Test__11()
        {
            var voucher = new VoucherStub();
            var success = voucher.TryGetDataFieldLength(out var length);
            success.Should().BeFalse();
            length.Should().Be(-1);
        }
    }
}
