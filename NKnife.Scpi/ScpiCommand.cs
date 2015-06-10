namespace ScpiKnife
{
    /// <summary>
    /// 针对SCPI的指令的封装
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
            Interval = 500;
        }

        public ScpiCommand Next { get; set; }

        public bool IsStandard { get; set; }
        public string Content { get; set; }
        public string Command { get; set; }
        public long Interval { get; set; }

        public bool IsReturn { get; set; }

        public string Build()
        {
            return Content;
        }

        public override string ToString()
        {
            return Build();
        }
    }
}