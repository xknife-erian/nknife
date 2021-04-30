using IItem = NKnife.Chesses.Common.Interface.IChessItem;

namespace NKnife.Chesses.Common.Record.StepTree
{
    public class Nag : IItem
    {
        public Nag()
        {

        }
        public Nag(string value)
        {
            this.Value = value;
        }

        public string ItemType { get { return "Nag"; } }
        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
