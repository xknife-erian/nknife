namespace ScpiKnife
{
    public class ScpiCommand
    {
        public ScpiCommand()
            : this(true)
        {
        }

        public ScpiCommand(bool isScpi)
        {
            IsScpi = isScpi;
            Interval = 500;
        }

        public ScpiCommand Next { get; set; }

        public bool IsScpi { get; set; }
        public string Content { get; set; }
        public string Command { get; set; }
        public long Interval { get; set; }

        public object Tag { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}