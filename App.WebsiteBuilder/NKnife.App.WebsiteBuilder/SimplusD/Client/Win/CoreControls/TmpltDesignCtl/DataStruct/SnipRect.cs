using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.Win;


namespace Jeelu.SimplusD.Client.Win
{

    public class SnipRect : Rect
    {
        #region 属性成员定义

        /// <summary>
        /// 获取或设置页面片名
        /// </summary>
        [PropertyPad(0,2,"snipName",MainControlType= MainControlType.TextBox,IsReadOnly=true)]
        public string SnipName { get; set; }

        

        #endregion

        #region 构造函数

        public SnipRect(int x, int y, int width, int height, string snipID)
            : base(x, y, width, height)
        {
            SnipID = snipID;
            SnipName = snipID;
            SnipData = null;
        }

        #endregion

        /// <summary>
        /// 复制页面片数据
        /// </summary>
        internal void CopyToClipboard()
        {
            SnipData.CopyToClipboard();
        }
    }
}
