using System;
using System.Linq;
using System.Text;
using NKnife.CRC;
using Xunit;

namespace NKnife.UnitTests.CRC
{
    /// <summary>
    ///     This is a test class for CRCManagerTest and is intended
    ///     to contain all CRCManagerTest Unit Tests
    /// </summary>
    public class CRCManagerTest
    {
        private readonly CRCFactory _factory = new CRCFactory();

        [Fact]
        public void CRC16CCITT_0x0000_Test()
        {
            var provider = _factory.CreateProvider(CRCProviderMode.CRC16_CCITT_XModem);
            var source = "1234567890";

            provider.Endianness = Endianness.LE;
            var leCRC = "21D3";
            var actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(leCRC, actual.ToHexString());

            provider.Endianness = Endianness.BE;
            var beCRC = "D321";
            actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(beCRC, actual.ToHexString());
        }

        [Fact]
        public void CRC16CCITT_0x1D0F_Test()
        {
            var provider = _factory.CreateProvider(CRCProviderMode.CRC16_CCITT_0x1D0F);
            var source = "1234567890";

            provider.Endianness = Endianness.LE;
            var leCRC = "D857";
            var actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(leCRC, actual.ToHexString());

            provider.Endianness = Endianness.BE;
            var beCRC = "57D8";
            actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(beCRC, actual.ToHexString());
        }

        [Fact]
        public void CRC16CCITT_0xFFFF_Test()
        {
            var provider = _factory.CreateProvider(CRCProviderMode.CRC16_CCITT_0xFFFF);
            var source = "1234567890";

            provider.Endianness = Endianness.LE;
            var leCRC = "1832";
            var actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(leCRC, actual.ToHexString());

            provider.Endianness = Endianness.BE;
            var beCRC = "3218";
            actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(beCRC, actual.ToHexString());
        }

        [Fact]
        public void CRC16Kermit_Test()
        {
            var provider = _factory.CreateProvider(CRCProviderMode.CRC16_Kermit);
            var source = "1234567890";

            provider.Endianness = Endianness.LE;
            var leCRC = "6B28";
            var actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(leCRC, actual.ToHexString());

            provider.Endianness = Endianness.BE;
            var beCRC = "286B";
            actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(beCRC, actual.ToHexString());
        }

        [Fact]
        public void CRC16Modbus_Test()
        {
            var provider = _factory.CreateProvider(CRCProviderMode.CRC16_Modbus);
            var source = "1234567890";

            provider.Endianness = Endianness.LE;
            var leCRC = "0AC2";
            var actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(leCRC, actual.ToHexString());

            provider.Endianness = Endianness.BE;
            var beCRC = "C20A";
            actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(beCRC, actual.ToHexString());
        }

        [Fact]
        public void CRC16_Test()
        {
            var provider = _factory.CreateProvider(CRCProviderMode.CRC16);
            var source = "1234567890";

            provider.Endianness = Endianness.LE;
            var leCRC = "7AC5";
            var actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(leCRC, actual.ToHexString());

            provider.Endianness = Endianness.BE;
            var beCRC = "C57A";
            actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(beCRC, actual.ToHexString());
        }

        [Fact]
        public void CRC32_Test()
        {
            var provider = _factory.CreateProvider(CRCProviderMode.CRC32);
            var source = "1234567890";

            provider.Endianness = Endianness.LE;
            var leCRC = "E5AE1D26";
            var actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(leCRC, actual.ToHexString());

            provider.Endianness = Endianness.BE;
            var beCRC = "261DAEE5";
            actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(beCRC, actual.ToHexString());
        }

        [Fact]
        public void CRC8_Test()
        {
            var provider = _factory.CreateProvider(CRCProviderMode.CRC8);
            var source = "1234567890";
            var expectedCheckSum = "38";
            var actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            Assert.Equal(expectedCheckSum, actual.ToHexString());
        }

        [Fact]
        public void CRC_SourceStringEmpty_Exception_Test()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var provider = _factory.CreateProvider(CRCProviderMode.CRC16_Modbus);
                var source = "";
                var actual = provider.CRCheck(Encoding.ASCII.GetBytes(source));
            });
        }

        [Fact]
        public void CRC_SourceArrayEmpty1_Exception_Test()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var provider = _factory.CreateProvider(CRCProviderMode.CRC16_Modbus);
                var source = new byte[0];
                provider.CRCheck(source);
            });
        }

        [Fact]
        public void CRC_SourceArrayEmpty2_Exception_Test()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var provider = _factory.CreateProvider(CRCProviderMode.CRC16_Modbus);
                byte[] source = null;
                // ReSharper disable once ExpressionIsAlwaysNull
                provider.CRCheck(source);
            });
        }
    }
}