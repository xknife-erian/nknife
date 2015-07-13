namespace ScpiKnife
{
    /// <summary>
    /// 针对SCPI的指令的封装。
    /// SCPI，可编程仪器标准命令，是一种建立在现有标准IEEE488.1 和 IEEE 488．2 基础上，
    /// 并遵循了IEEE754 标准中浮点运算规则、ISO646 信息交换7 位编码符号（相当于ASCll编
    /// 程）等多种标准的标准化仪器编程语言。
    /// </summary>
    public class ScpiCommand
    {
        public ScpiCommand()
            : this(true)
        {
        }

        public ScpiCommand(bool isStandard)
        {
            IsStandard = isStandard;
            Interval = 300;
        }

        public ScpiCommand Next { get; set; }

        public bool IsStandard { get; set; }
        public string Description { get; set; }
        public string Command { get; set; }
        public long Interval { get; set; }

        public bool IsReturn { get; set; }

        public string Build()
        {
            return Description;
        }

        public override string ToString()
        {
            return Build();
        }
    }
}