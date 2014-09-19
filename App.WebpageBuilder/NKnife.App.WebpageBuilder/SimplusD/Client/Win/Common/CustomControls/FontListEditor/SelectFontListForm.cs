using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class SelectFontListForm : BaseForm
    {
        #region 公共属性

        /// <summary>
        /// 获取字体列表
        /// </summary>
        public string FontList { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 选择字体列表
        /// </summary>
        /// <param name="fontLists"></param>
        public SelectFontListForm(List<string> fontLists)
        {
            InitializeComponent();
            InitListBoxFontLists(fontLists);
        }

        #endregion

        #region 事件消息

        /// <summary>
        /// 执行选择后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxFontLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontList = listBoxFontLists.SelectedItem.ToString();
        }

        #endregion

        #region 内部函数

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="fontLists"></param>
        void InitListBoxFontLists(List<string> fontLists)
        {
            listBoxFontLists.Items.Clear();
            foreach (string fontList in fontLists)
            {
                listBoxFontLists.Items.Add(fontList);
            }
        }

        #endregion
    }
}
