using System.Data;
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

        public ScpiCommand()
        {
            Interval = 200;
            IsHex = false;
        }

        public string Command { get; set; }

        public long Interval { get; set; }

        public bool IsReturn { get; set; }

        public bool IsHex { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("{0}\r\n{1}\r\n{2}", Command, Interval, Description);
        }
    }
}