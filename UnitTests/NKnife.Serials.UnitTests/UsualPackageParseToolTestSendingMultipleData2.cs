using System;
using System.Threading;
using FluentAssertions;
using NKnife.CRC;
using NKnife.Interface;
using NKnife.Serials.UnitTests.Examples;
using NKnife.Serials.UnitTests.Stub;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTestSendingMultipleData2
    {
        private static readonly Random _Random = new Random((int) DateTime.Now.Ticks);
        private ICRCProvider _crcProvider;

        private void InitialiseCRCProvider()
        {
            if (_crcProvider == null)
            {
                var factory = new CRCFactory();
                _crcProvider = factory.CreateProvider(CRCProviderMode.CRC16_Modbus);
            }
        }

        /// <summary>
        /// 将一条11个字节的数据分成一个一个字节发送。最极端的断包。
        /// </summary>
        [Fact]
        public void Test_01()
        {
            InitialiseCRCProvider();
            var tool = new UsePackageToolExample();
            var msg01 = new byte[] {0xFF, 0xAA};
            var msg02 = new byte[] {0xEE};
            var msg03 = new byte[] {0x04};
            var msg04 = new byte[] {0x00};
            var msg05 = new byte[] {0x01};
            var msg06 = new byte[] {0x02};
            var msg07 = new byte[] {0x03};
            var msg08 = new byte[] {0x04};
            var msg09 = new byte[] {0x64};
            var msg10 = new byte[] {0x89};
            var msg11 = new byte[] {0xCC, 0xBB};

            tool.Run(msg01);
            tool.Results.Count.Should().Be(0);
            
            tool.Run(msg02);
            tool.Results.Count.Should().Be(0);
            
            tool.Run(msg03);
            tool.Results.Count.Should().Be(0);
            
            tool.Run(msg04);
            tool.Results.Count.Should().Be(0);
            
            tool.Run(msg05);
            tool.Results.Count.Should().Be(0);
            
            tool.Run(msg06);
            tool.Results.Count.Should().Be(0);
            
            tool.Run(msg07);
            tool.Results.Count.Should().Be(0);
            
            tool.Run(msg08);
            tool.Results.Count.Should().Be(0);
            
            tool.Run(msg09);
            tool.Results.Count.Should().Be(0);
            
            tool.Run(msg10);
            tool.Results.Count.Should().Be(0);
            
            tool.Run(msg11);
            tool.Results.Count.Should().Be(1);

            tool.TrueCount.Should().Be(1);
            tool.TrueAtLastast.Should().BeTrue();
            tool.Results[0].Item1.Should().Be(0xEE);
            tool.Results[0].Item2.Count.Should().Be(4);
            for (int i = 0; i < tool.Results[0].Item2.Count; i++)
            {
                tool.Results[0].Item2[i].Should().Be((byte) (i + 1));
            }
        }
    }
}