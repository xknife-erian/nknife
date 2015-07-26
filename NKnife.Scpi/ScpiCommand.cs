﻿using System;
using System.Data;
using System.Text;
using System.Xml;

namespace ScpiKnife
{
    /// <summary>
    /// 针对SCPI标准命令的封装。
    /// SCPI，可编程仪器标准命令，是一种建立在现有标准IEEE488.1 和 IEEE 488.2 基础上，
    /// 并遵循了IEEE754 标准中浮点运算规则、ISO646 信息交换7位编码符号（相当于Ascii编
    /// 程）等多种标准的标准化仪器编程语言。
    /// </summary>
    public class ScpiCommand
    {
        public static ScpiCommand Parse(XmlElement element)
        {
            //样本
            //<scpi interval="200" hex="true" return="true">
            //  <![CDATA[]]>
            //</scpi>
            var command = new ScpiCommand();
            var cdata = element.GetCDataElement();
            if (cdata != null)
            {
                command.Command = cdata.InnerText;
            }
            int interval;
            if (!int.TryParse(element.GetAttribute("interval"), out interval))
                command.Interval = 200;
            command.Interval = interval;
            bool isHex;
            if (bool.TryParse(element.GetAttribute("hex"), out isHex))
                command.IsHex = isHex;
            bool isReturn;
            if (bool.TryParse(element.GetAttribute("return"), out isReturn))
                command.IsReturn = isReturn;
            return command;
        }

        public static void Build(ScpiCommand command, ref XmlElement element)
        {
        }

        public ScpiCommand()
        {
            Interval = 200;
            IsHex = false;
        }

        /// <summary>
        /// 命令主体
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 命令的等待周期,指等待仪器返回结果的超时时间
        /// </summary>
        public long Interval { get; set; }

        /// <summary>
        /// 命令需要仪器返回数据
        /// </summary>
        public bool IsReturn { get; set; }

        /// <summary>
        /// 命令主体是用原生字符串表达,还是16进制字符串表达
        /// </summary>
        public bool IsHex { get; set; }

        /// <summary>
        /// 命令的解释
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 生成协议字节数组
        /// </summary>
        /// <param name="gpibAddress">协议体里的GPIB地址</param>
        /// <returns>协议字节数组</returns>
        public virtual byte[] GenerateProtocol(int gpibAddress)
        {
            byte mainCommand = IsReturn ? (byte) 0xAA : (byte) 0xAB;
            const byte SUB_COMMAND = 0x00;
            var scpiBytes = Encoding.ASCII.GetBytes(Command);

            var bs = new byte[] {0x08, (byte) gpibAddress, (byte) (scpiBytes.Length + 2), mainCommand, SUB_COMMAND};
            var result = new byte[bs.Length + scpiBytes.Length];
            Buffer.BlockCopy(bs, 0, result, 0, bs.Length);
            Buffer.BlockCopy(scpiBytes, 0, result, bs.Length, scpiBytes.Length);
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0}\r\n{1}\r\n{2}", Command, Interval, Description);
        }
    }
}