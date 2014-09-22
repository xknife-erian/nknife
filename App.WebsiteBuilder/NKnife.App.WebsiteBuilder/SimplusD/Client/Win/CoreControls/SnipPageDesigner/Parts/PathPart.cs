using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{

    [Serializable]
    public class PathPart : SnipPagePart
    {
        #region 构造函数

        public PathPart(SnipPageDesigner designer)
            : base(designer)
        {
            LinkDisplayType = DisplayType.Default;
            SeparatorCode = ">>";
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// edit by zhenghao at 2008-06-17 10:20
        /// 获取或设置链接部分的显示方式
        /// </summary>
        public DisplayType LinkDisplayType { get; set; }

        /// <summary>
        /// edit by zhenghao at 2008-06-17 10:20
        /// 获取或设置间隔部分的符号内容
        /// </summary>
        public string SeparatorCode { get; set; }

        #endregion
    }
}
