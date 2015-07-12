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