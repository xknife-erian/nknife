using System;
using System.Collections.Generic;
using System.Text;


namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// List型页面片组成部分
    /// </summary>
    /// 
    [Serializable]
    public class ListPart : SnipPagePart
    {
        #region 属性

        /// <summary>
        /// edit by zhenghao at 2008-06-17 15:25
        /// 获取或设置排序方式
        /// </summary>
        public SequenceType SequenceType { get; set; }

        /// <summary>
        /// edit by zhenghao at 2008-06-17 15:25
        /// 获取或设置频道ID集
        /// </summary>
        public List<string> ChannelIDs { get; set; }

        /// <summary>
        /// edit by zhenghao at 2008-06-17 15:25
        /// 获取或设置自定义关键词
        /// </summary>
        public string CustomKeyWords { get; set; }

        /// <summary>
        /// 获取或设置显示数量
        /// </summary>
        public int MaxDisplayAmount { get; set; }

        #endregion

        #region 构造函数

        protected  ListPart(SnipPageDesigner designer)
            :base(designer)
        {
            Width_Css = "100%";
            SequenceType = SequenceType.None;
            ChannelIDs = new List<string>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="designer"></param>
        /// <returns></returns>
        static internal ListPart Create(SnipPageDesigner designer)
        {
            return new ListPart(designer);
        }

        #endregion
    }
}
