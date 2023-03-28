using System;
using FluentAssertions;
using Xunit;

namespace NKnife.Bytes.UnitTests
{
    public class UtilByteToBytesTest
    {
        [Fact]
        public void ConvertToBytesTest1()
        {
            var bs = UtilByte.ConvertToBytes("FF", string.Empty);
            bs.Length.Should().Be(1);
            bs[0].Should().Be(Convert.ToByte(255));
        }

        [Fact]
        public void ConvertToBytesTest2()
        {
            var bs = UtilByte.ConvertToBytes("FFFFFFFF", string.Empty);
            bs.Length.Should().Be(4);
            bs[0].Should().Be(Convert.ToByte(255));
            bs[1].Should().Be(Convert.ToByte(255));
            bs[2].Should().Be(Convert.ToByte(255));
            bs[3].Should().Be(Convert.ToByte(255));
        }
        [Fact]
        public void ConvertToBytesTest3()
        {
            var bs = UtilByte.ConvertToBytes("0xFF", string.Empty);
            bs.Length.Should().Be(1);
            bs[0].Should().Be(Convert.ToByte(255));
        }

        [Fact]
        public void ConvertToBytesTest4()
        {
            var bs = UtilByte.ConvertToBytes("0xFF0xFF0xFF0xFF", string.Empty);
            bs.Length.Should().Be(4);
            bs[0].Should().Be(Convert.ToByte(255));
            bs[1].Should().Be(Convert.ToByte(255));
            bs[2].Should().Be(Convert.ToByte(255));
            bs[3].Should().Be(Convert.ToByte(255));
        }

        [Fact]
        public void ConvertToBytesTest5()
        {
            var bs = UtilByte.ConvertToBytes("FF FF FF FF");
            bs.Length.Should().Be(4);
            bs[0].Should().Be(Convert.ToByte(255));
            bs[1].Should().Be(Convert.ToByte(255));
            bs[2].Should().Be(Convert.ToByte(255));
            bs[3].Should().Be(Convert.ToByte(255));
        }

        [Fact]
        public void ConvertToBytesTest6()
        {
            var bs = UtilByte.ConvertToBytes("0xFF 0xFF 0xFF 0xFF");
            bs.Length.Should().Be(4);
            bs[0].Should().Be(Convert.ToByte(255));
            bs[1].Should().Be(Convert.ToByte(255));
            bs[2].Should().Be(Convert.ToByte(255));
            bs[3].Should().Be(Convert.ToByte(255));
        }

        [Fact]
        public void ConvertToBytesTest7()
        {
            var bs = UtilByte.ConvertToBytes("0xFF/0xFF/0xFF/0xFF","/");
            bs.Length.Should().Be(4);
            bs[0].Should().Be(Convert.ToByte(255));
            bs[1].Should().Be(Convert.ToByte(255));
            bs[2].Should().Be(Convert.ToByte(255));
            bs[3].Should().Be(Convert.ToByte(255));
        }

        [Fact]
        public void ConvertToBytesTest8()
        {
            Assert.Throws<FormatException>(() => UtilByte.ConvertToBytes("0xKK/0xFF/0xFF/0xFF", "/"));
        }

        [Fact]
        public void ConvertToBytesTest9()
        {
            Assert.Throws<ArgumentException>(() => UtilByte.ConvertToBytes("FFF", string.Empty));
        }
    }
}


