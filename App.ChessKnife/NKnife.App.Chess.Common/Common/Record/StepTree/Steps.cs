using System.Collections;
using System.Collections.Generic;
using System.Text;
using IItem = NKnife.Chesses.Common.Interface.IChessItem;

namespace NKnife.Chesses.Common.Record.StepTree
{
    /// <summary>
    /// 描述棋局中的回合序列(实现IList接口)。
    /// 它可能描述的是一整局棋，也可能是描述的是一整局棋的一部份，如变招的描述与记录。
    /// </summary>
    public class Steps : IList<IItem>
    {
        protected List<IItem> SequenceItemList { get; private set; }

        public Steps()
        {
            this.SequenceItemList = new List<IItem>();
        }

        public Step Peek()
        {
            return this.SequenceItemList[this.SequenceItemList.Count - 1] as Step;
        }

        public Step[] PeekPair()
        {
            if (this.SequenceItemList.Count < 2)
            {
                return null;
            }
            Step[] steps = new Step[2];
            steps[0] = this.SequenceItemList[this.SequenceItemList.Count - 1] as Step;
            steps[1] = this.SequenceItemList[this.SequenceItemList.Count - 2] as Step;
            return steps;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IItem item in this.SequenceItemList)
            {
                this.GetItemString(item, sb);
            }
            return sb.ToString().Replace("  ", " ");
        }
        /// <summary>
        /// 序列树生成字符串<see>TooString()</see>的递归子方法
        /// </summary>
        private void GetItemString(IItem item, StringBuilder sb)
        {
            sb.Append(item.Value).Append(' ');
//            if (item is ITree)
//            {
//                ITree tree = (ITree)item;
//                if (tree.HasChildren)
//                {
//                    sb.Append(' ').Append('(');
//                    foreach (var subItem in tree.Items)
//                    {
//                        this.GetItemString(subItem, sb);
//                    }
//                    sb.Append(')').Append(' ');
//                }
//            }
        }

        #region IList<IItem> 成员

        public int IndexOf(IItem item)
        {
            return this.SequenceItemList.IndexOf(item);
        }

        public void Insert(int index, IItem item)
        {
            this.SequenceItemList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.SequenceItemList.RemoveAt(index);
        }

        public IItem this[int index]
        {
            get { return this.SequenceItemList[index]; }
            set { this.SequenceItemList[index] = value; }
        }

        #endregion

        #region ICollection<IItem> 成员

        public void Add(IItem item)
        {
            this.SequenceItemList.Add(item);
        }

        public void Clear()
        {
            this.SequenceItemList.Clear();
        }

        public bool Contains(IItem item)
        {
            return this.SequenceItemList.Contains(item);
        }

        public void CopyTo(IItem[] array, int arrayIndex)
        {
            this.SequenceItemList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.SequenceItemList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IItem item)
        {
            return this.SequenceItemList.Remove(item);
        }

        #endregion

        #region IEnumerable<IItem> 成员

        public IEnumerator<IItem> GetEnumerator()
        {
            return this.SequenceItemList.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.SequenceItemList.GetEnumerator();
        }

        #endregion
    }
}
