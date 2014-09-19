using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 模板绘制时的框架
    /// </summary>
    public partial class TmpltDrawFrame : DrawFrame
    {
        #region 构造函数

        internal TmpltDrawFrame(DesignPanel tD, int width, int height,Image backImage)
            : base(tD, width, height,backImage)
        {
            InitializeComponent();
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 模板文档
        /// </summary>
        public TmpltXmlDocument TmpltDoc { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 创建画板
        /// </summary>
        /// <param name="tD"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="backImage"></param>
        /// <returns></returns>
        protected override DrawPanel CreateDrawPanel(DesignPanel tD, int width, int height, Image backImage)
        {
            TmpltDrawPanel drawPanel = new TmpltDrawPanel(tD, width, height,backImage);
            drawPanel.TmpltDoc = TmpltDoc;
            return drawPanel;
        }

        #endregion
    }
}
