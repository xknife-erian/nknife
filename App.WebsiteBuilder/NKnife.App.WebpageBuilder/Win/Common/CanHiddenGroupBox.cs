using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public partial class CanHiddenGroupBox : UserControl
    {
        public CanHiddenGroupBox()
        {
            InitializeComponent();
        }

        #region 内部变量

        private int _columnsCount = 1;
        private int _rowsCount = 0;
        private bool _hiddened = false;

        #endregion

        #region 公共属性

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 获取包含在控件内控件的集合
        /// </summary>
        public ControlCollection BoxControls
        {
            get { return groupBoxContent.Controls; }
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 获取或设置控件关联的文本
        /// </summary>
        public new string Text
        {
            get { return labelText.Text; }
            set 
            {
                labelText.Text = value;
                //zhenghao:为了保持布局的视觉效果，在text前边加了2个空格。
                groupBoxContent.Text = "  " + value;
                ResetControlsLocationAndSize();
            }
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 获取或设置控件工作状态是否处于隐藏状态
        /// </summary>
        public bool Hiddened
        {
            get { return _hiddened; }
            set 
            { 
                _hiddened = value;
                ResetGroupBoxState();
            }
        }
       
        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 获取或设置内部子控件的排列的列数，默认为1
        /// </summary>
        public int ColumnsCount 
        {
            get { return _columnsCount; }
            set
            {
                if (value > 0)
                {
                    _columnsCount = value;
                }
            }
        }

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 获取或设置内部子控件的排列的行数，0为自动
        /// </summary>
        public int RowsCount 
        {
            get { return _rowsCount; }
            set
            {
                if (_rowsCount >= 0 )
                {
                    _rowsCount = value;
                }
            }
        }

        #endregion

        #region 公共方法

        #endregion

        #region 重写方法

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 当控件的大小发生变化时......
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            ResetControlsLocationAndSize();
            base.OnSizeChanged(e);
        }

        #endregion

        #region 内部方法

        /// <summary>
        /// zhenghao at 2008-05-28 : 重置子控件的位置和大小
        /// </summary>
        private void ResetControlsLocationAndSize()
        {
            singleLineLine.Location = 
                new Point(labelText.Location.X + labelText.Width, 8);
            singleLineLine.Size = 
                new Size(this.Width - singleLineLine.Location.X - 3, 2);
            groupBoxContent.Size = new Size(this.Width - 3,this.Height-6);
        }

        /// <summary>
        /// zhenghao at 2008-05-28 : 重置控件是否隐藏的状态
        /// </summary>
        private void ResetGroupBoxState()
        {
            if (_hiddened)
            {
                groupBoxContent.Visible = false;
                labelText.Visible = true;
                singleLineLine.Visible = true;
                this.Height = 25;
            }
            else
            {
                groupBoxContent.Visible = true;
                labelText.Visible = false;
                singleLineLine.Visible = false;
                this.Height = groupBoxContent.Height + 6;
            }
        }

        #endregion
        
        #region 事件响应

        /// <summary>
        /// edited by zhenghao at 2008-05-28 10:30
        /// 当点击控件左上角的按钮时......
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHidden_Click(object sender, EventArgs e)
        {
            Hiddened = !Hiddened;
        }
        
        #endregion

        #region 自定义事件

        #endregion
    }
}
