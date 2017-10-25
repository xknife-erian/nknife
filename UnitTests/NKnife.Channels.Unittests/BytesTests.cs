using FluentAssertions;
using NUnit.Framework;

namespace NKnife.Channels.Unittests
{
    [TestFixture]
    public class BytesTests
    {
        [Test]
        public void BuildTest1()
        {
            var bs = new Bytes(0, 0, 0);
            bs.Byte1.Should().Be(0);
            bs.Byte2.Should().Be(0);
            bs.Byte3.Should().Be(0);
            bs.Byte4.Should().Be(0);
        }

        [Test]
        public void BuildTest2()
        {
            var bs = new Bytes(15, 16383, 16383);
            bs.Byte1.Should().Be(255);
            bs.Byte2.Should().Be(255);
            bs.Byte3.Should().Be(255);
            bs.Byte4.Should().Be(255);
        }

        [Test]
        public void BuildTest3()
        {
            var bs = new Bytes(1, 16383, 5461);
            bs.Byte1.Should().Be(31);
            bs.Byte2.Should().Be(255);
            bs.Byte3.Should().Be(213);
            bs.Byte4.Should().Be(85);
        }

        [Test]
        public void BuildTest4()
        {
            var bs = new Bytes(3, 10, 2);
            bs.Byte1.Should().Be(48);
            bs.Byte2.Should().Be(2);
            bs.Byte3.Should().Be(128);
            bs.Byte4.Should().Be(2);
        }
    }
}
