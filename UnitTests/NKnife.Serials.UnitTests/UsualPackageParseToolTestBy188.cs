using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using NKnife.CRC;
using NKnife.Interface;
using NKnife.Serials.ParseTools;
using NKnife.Serials.UnitTests.Examples;
using Xunit;

namespace NKnife.Serials.UnitTests
{
    public class UsualPackageParseToolTestBy188
    {
        //https://www.cnblogs.com/myzony/p/10897895.html
        /*
         *  FE FE FE FE 68 10 44 33 22 11 00 33 78 81 16 1F 90 00 00 77 66 55 2C 00 77 66 55 2C 31 01 22 11 05 15 20 21 84 08 16
         *  说明如下：
         *      FE FE FE FE >--->协议头（1-4组）。
         *      68 >------------>帧起始符。
         *      10 >------------>仪表类型。
         *      44 33 22 11 00 >>倒序为0011223344（以BCD码形式看待），表示表号。
         *      33 78 >--------->倒序为7833 （以BCD码形式看待），表示厂家代码。
         *      81 >------------>实际为控制码+80，我们可以简单认为只有81正确，非81均为异常，不进行解析。
         *      16 >------------>数据域长度，为十进制22，表示后面有22个有效数据。
         *      1F 90 >--------->数据标识 （固定）。
         *      00 >------------>序列号（固定）。
         *      00 77 66 55 >--->倒序为556677.00 （以BCD码形式看待），表示 累计用量。
         *      2C >------------>立方米，其它单位见附1。
         *      00 77 66 55 >--->倒序为556677.00 （以BCD码形式看待），表示 本月用量。
         *      2C >------------>立方米，其它单位见附1。    
         *      31 01 22 11 05 15 20：2015-05-11 22:01:31，表示实时时间。
         *      21 84 >--------->状态，两字节，第1字节定义如下，第2字节由厂家自定义。
         *      68：累加和，68+10+44+33+22+11+00+33+78+81+16+1F+90+00+00+77+66+55+2C+00+77+66+55+2C+31+01+22+11+05+15+20+21+84=08
         *      16；结束符。
         */

        private static UsePackageToolExample GetTool()
        {
            var upte = new UsePackageToolExample();
            //下面是比较复杂的情况。在可能的出现的情况下最完整的协议。
            upte.FieldConfig = new FieldConfig(
                new byte[] {0xFE, 0xEE, 0xFE, 0xEE, 0x68}, //起始符
                new byte[] {0x16}, //结尾符
                CRCProviderMode.CRC32,
                Endianness.BigEndian,
                1024 * 8,
                (0, 5), //起始符
                (5, 1), //属性
                (6, 7), //地址
                (13, 1), //命令字
                (14, 1), //数据长度
                1, //CRC    
                1 //结尾符//CRC
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
            false.Should().BeTrue();
        }
    }
}
