using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 导航型页面片组成部分
    /// </summary>

    [Serializable]
    public class NavigationPart : SnipPagePart
    {
        #region 属性

        /// <summary>
        /// 获取或设置 包含的频道的GUID
        /// </summary>
        [PropertyPad(0, 1, "channelID", MainControlType= MainControlType.TextBox,IsReadOnly=true)]
        public string ChannelID { get; set; }

        /// <summary>
        /// edit by zhenghao at 10:13
        /// 获取或设置频道的标题
        /// </summary>
        public string ChannelTitle { get; set; }


        private string _pageText;
        public string PageText
        {
            get { return _pageText; }
            set
            {
                if (_pageText != value)
                {
                    Designer.CmdManager.AddExecSetPropertyPartCommand<string>(this,
                        PageText, value, SetPageTextCore);
                }
            }
        }
        private void SetPageTextCore(string value)
        {
            _pageText = value;
        }

        #endregion

        #region 构造函数

        public NavigationPart(SnipPageDesigner designer)
            :base(designer)
        {
            ChannelID = "";
        }

        #endregion
    }
}
