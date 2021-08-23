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
    public class UsualPackageParseToolTestMultithreading
    {
        private static readonly Random _Random = new Random((int)DateTime.Now.Ticks);
        private ICRCProvider _crcProvider;

        private void InitialiseCRCProvider()
        {
            if (_crcProvider == null)
            {
                var factory = new CRCFactory();
                _crcProvider = factory.CreateProvider(CRCProviderMode.CRC16_Modbus);
            }
        }

        #region multithreading

        /// <summary>
        /// 多线程的操作
        /// </summary>
        [Fact]
        public void Test_Multithreading_01()
        {
            InitialiseCRCProvider();
            const int COUNT = 100;
            var tool = new UsePackageToolExample();
            for (int i = 0; i < COUNT; i++)
            {
                var msg = GetAnIndexInTheDataFieldForMessage(i);
                tool.Run(msg);
                Thread.Sleep(_Random.Next(3, 8));
            }

            tool.Results.Count.Should().Be(COUNT);
            for (int i = 0; i < COUNT; i++)
            {
                var n = BitConverter.ToInt32(tool.Results[i].Item2);
                n.Should().Be(i);
            }
        }

        /// <summary>
        /// 获取一条索引号在数据域的消息，通过命令字进行单元测试比较
        /// </summary>
        /// <param name="index">索引号</param>
        private byte[] GetAnIndexInTheDataFieldForMessage(int index)
        {
            var data = BitConverter.GetBytes(index);
            var bs = new byte[11];
            bs[0] = 0xAA;
            bs[1] = 0xFE;
            bs[2] = 0x04;
            bs[3] = 0x00;
            Buffer.BlockCopy(data, 0, bs, 4, 4);
            var crc = _crcProvider.CRCheck(new[] {bs[0], bs[1], bs[2], bs[3], bs[4], bs[5], bs[6], bs[7]});
            bs[8] = crc[0];
            bs[9] = crc[1];
            bs[10] = 0xCC;
            return bs;
        }

        #endregion
    }
}