using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    internal class BorderStyleControl : ComboBox
    {
        private string getBorderStyle = "";

        public string GetBorderStyle
        {
            get { return getBorderStyle; }
            set 
            {
                if (BorderSourceFile.borderStyleDic.ContainsKey(value))
                {
                    this.Text = BorderSourceFile.borderStyleDic[value];
                }
                else
                    this.Text = ""; 
            }
        }

        bool _isFirstChanged = true;
        public BorderStyleControl()
        {
            InitializeControl();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (_isFirstChanged)
            {
                OnValueInited(EventArgs.Empty);
            }

            base.OnSelectedIndexChanged(e);
        }

        private void InitializeControl()
        {
            this.Width = 100;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            if(!Service.Util.DesignMode)
                this.Items.AddRange (BorderSourceFile.BorderStyle);
            this.Location = new System.Drawing.Point(0,0);
            this.SelectedIndexChanged += new EventHandler(BorderStyleControl_SelectedIndexChanged);
        }

        void BorderStyleControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBorderStyle = BorderSourceFile.borderStyleDic[this.SelectedItem.ToString()];
        }


        #region 事件的定义与声明
        public event EventHandler ValueInited;

        protected void OnValueInited(EventArgs e)
        {
            if (ValueInited != null)
            {
                ValueInited(this, e);
            }
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 初始化现有的值
        /// </summary>
        public void InitValue(string cssStr)
        {
            switch (cssStr)
            {
                case "dotted":
                    SelectedIndex = 1;
                    break;
                case "dashed":
                    SelectedIndex = 2;
                    break;
                case "solid":
                    SelectedIndex = 3;
                    break;
                case "double":
                    SelectedIndex = 4;
                    break;
                case "groove":
                    SelectedIndex = 5;
                    break;
                case "ridge":
                    SelectedIndex = 6;
                    break;
                case "inset":
                    SelectedIndex = 7;
                    break;
                case "outset":
                    SelectedIndex = 8;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
