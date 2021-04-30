using System;
using System.Collections.Generic;
using FluentAssertions;
using NKnife.CRC;
using NKnife.Interface;
using NKnife.Serials.ParseTools;
using NKnife.Serials.UnitTests.Examples;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTest
    {
        private static UsePackageToolExample GetTool()
        {
            var upte = new UsePackageToolExample();
            //下面是比较复杂的情况。在可能的出现的情况下最完整的协议。
            upte.FieldConfig = new FieldConfig(
                new byte[] {0xFE, 0xEE, 0xFE, 0xEE}, //起始符
                new byte[] {0xCC, 0xDD}, //结尾符
                CRCProviderMode.CRC32,
                Endianness.BigEndian,
                1024 * 8,
                (0, 4), //起始符
                (4, 2), //属性
                (6, 2), //地址
                (8, 2), //命令字
                (10, 4), //数据长度
                4, //CRC    
                2 //结尾符//CRC
            );
            return upte;
        }

        private ICRCProvider _crcProvider;

        private void InitialiseCRCProvider()
        {
            if (_crcProvider == null)
            {
                var factory = new CRCFactory();
                _crcProvider = factory.CreateProvider(CRCProviderMode.CRC32);
            }
        }


        [Fact]
        public void MainTest_1()
        {
            InitialiseCRCProvider();

            var upte = GetTool();
            upte.SetSkipCRC(true);

            var begin = new byte[] {0xFE, 0xEE, 0xFE, 0xEE}; //起始符
            var attribute = new byte[] {0x00, 0x10}; //属性
            var address = new byte[] {0x01, 0x01}; //地址
            var command = new byte[] {0xCC, 0xEE}; //命令字
            var data = Guid.NewGuid().ToByteArray(); //数据
            var length = BitConverter.GetBytes(data.Length); //数据长度
            var end = new byte[] {0xCC, 0xDD}; //结尾符

            var byteList = new List<byte>();
            byteList.AddRange(begin);
            byteList.AddRange(attribute);
            byteList.AddRange(address);
            byteList.AddRange(command);
            byteList.AddRange(length);
            byteList.AddRange(data);
            //计算CRC
            var crc = _crcProvider.CRCheck(byteList.ToArray());
            switch (upte.FieldConfig.Endianness)
            {
                case Endianness.BigEndian:
                    Array.Reverse(crc);
                    break;
            }

            byteList.AddRange(crc);
            byteList.AddRange(end);

            var success = upte.Run(byteList.ToArray());
            success.Should().BeTrue();
            upte.TrueAtLastast.Should().BeTrue();
            upte.TrueCount.Should().Be(1);
            upte.Vouchers.Count.Should().Be(1);

            var v = upte.Vouchers[0];
            v.Begin.Should().BeEquivalentTo(begin);
            v.Attribute.Should().BeEquivalentTo(attribute);
            v.Address.Should().BeEquivalentTo(address);
            v.Command.Should().BeEquivalentTo(command);
            v.DataFieldLength.Should().BeEquivalentTo(length);
            v.DataField.Should().BeEquivalentTo(data);
            v.CRC.Should().BeEquivalentTo(crc);
            v.End.Should().BeEquivalentTo(end);
        }
    }
}