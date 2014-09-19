using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class CSSDesignerDisplayControl : UserControl
    {
        public CSSDesignerDisplayControl()
        {
            InitializeComponent();
        }

        #region 内部函数

        private string _multipleHeight = "1";

        #endregion

        #region 属性

        public NumericUpDown NumericUpDown
        {
            get { return this.numericUpDown1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string MultipleHeight
        {
            get { return _multipleHeight; }
            set { _multipleHeight = value; }
        }

        public string CSSText
        {
            get
            {
                return _multipleHeight;
            }
        }

        #endregion

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _multipleHeight = numericUpDown1.Value.ToString();
            OnColorSelected(EventArgs.Empty);
        }

        private void CSSDesignerDisplayControl_Load(object sender, EventArgs e)
        {
            //numericUpDown1.Value = 1;
        }

        #region 自定义事件

        public event EventHandler ContentSelected;
        protected void OnColorSelected(EventArgs e)
        {
            if (ContentSelected != null)
            {
                ContentSelected(this, e);
            }
        }

        #endregion
    }
}
