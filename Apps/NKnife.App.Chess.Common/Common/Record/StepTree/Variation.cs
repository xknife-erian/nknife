using IItem = NKnife.Chesses.Common.Interface.IChessItem;

namespace NKnife.Chesses.Common.Record.StepTree
{
    /// <summary>
    /// 变招(变招同样也是一个Step的序列)。
    /// </summary>
    public class Variation : Steps, IItem
    {
        #region IItem 成员

        public string ItemType { get { return "Variation"; } }
        public string Value
        {
            get { return this.ToString(); }
        }

        #endregion
    }
}
