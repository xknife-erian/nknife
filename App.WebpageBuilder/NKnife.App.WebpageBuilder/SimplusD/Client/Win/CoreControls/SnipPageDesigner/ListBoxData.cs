using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 列表型Part中单个组合条件
    /// </summary>
    public class ListBoxData
    {
        #region 构造函数

        public ListBoxData()
        {
            ListBoxText = "新列表";
            Index = 0;
            Channels = new List<KeyValuePair<string, string>>();
            PageType = PageType.None;
            ChildrenCount = 5;
            SequenceType = SequenceType.Recent;
            ChildPartOptions = new List<string>();
        }

        #endregion

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public ListBoxData Clone()
        {
            ListBoxData temp = new ListBoxData();
            temp.ListBoxText = ListBoxText;
            temp.Index = Index;
            temp.Channels.Clear();
            foreach (KeyValuePair<string, string> item in Channels)
            {
                temp.Channels.Add(item);
            }
            temp.PageType = PageType;
            temp.ChildrenCount = ChildrenCount;
            temp.SequenceType = SequenceType;
            temp.ChildPartOptions.Clear();
            foreach (string str in ChildPartOptions)
            {
                temp.ChildPartOptions.Add(str);
            }
            return temp;
        }

        #region 公共属性

        /// <summary>
        /// 获取或设置在树里的显示名
        /// </summary>
        public string ListBoxText { get; set; }

        /// <summary>
        /// 获取或设置位置索引(默认：0)
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 获取或设置要查找的频道集
        /// </summary>
        public List<KeyValuePair<string, string>> Channels { get; set; }

        /// <summary>
        /// 获取或设置要查找的页面类型(默认：None)
        /// </summary>
        public PageType PageType { get; set; }

        /// <summary>
        /// 获取或设置要查找的结果数量（条）(默认：1)
        /// </summary>
        public int ChildrenCount { get; set; }

        /// <summary>
        /// 获取或设置查找/排序方式(默认：None)
        /// </summary>
        public SequenceType SequenceType { get; set; }

        /// <summary>
        /// 子part选项
        /// </summary>
        public List<string> ChildPartOptions { get; set; }

        #endregion
    }
}
