using System;
using System.Collections.Generic;
using System.Text;
using NKnife.Interface;

namespace Gean.Module.Chess
{
    /// <summary>
    /// 变招(变招同样也是一个Step的序列)。
    /// </summary>
    public class Variation : Steps, Gean.IItem
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
