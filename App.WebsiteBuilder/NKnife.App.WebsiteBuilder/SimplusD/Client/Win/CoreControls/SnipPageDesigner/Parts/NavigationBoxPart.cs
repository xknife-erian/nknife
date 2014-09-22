using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{

    [Serializable]
    public class NavigationBoxPart : SnipPagePart
    {
        #region 构造函数

        public NavigationBoxPart(SnipPageDesigner designer)
            : base(designer)
        {
            
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 是否使用间隔符
        /// </summary>
        public bool IsUsedSeparator { get; set; }

        #endregion

    }
}
