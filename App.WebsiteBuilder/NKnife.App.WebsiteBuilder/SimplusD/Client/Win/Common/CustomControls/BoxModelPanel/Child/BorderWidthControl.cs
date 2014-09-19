using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Jeelu.SimplusD.Client.Win
{
    internal class BorderWidthControl : Control
    {
        private ComboBox _comboBox;
        private NumericUpDown _numericUpDown;
        private ComboBox _unitComboBox;

        private CssSection _cssSection = new CssSection();


        private string _getBorderWidth = "";
        private string _getUnit = "";

        /// <summary>
        /// 获取Css
        /// </summary>
        public string ToCss()
        {
            return _getBorderWidth +_getUnit;
        }


        public BorderWidthControl()
        {
            InitControls();
        }

        bool _isFirstChanged = true;
        private void InitControls()
        {
            this._comboBox = new ComboBox();
            this._comboBox.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            if(!Service.Util.DesignMode)
                this._comboBox.Items.AddRange(BorderSourceFile.BorderWidth);
            this._comboBox.TextChanged += new EventHandler(_comboBox_TextChanged);
            //this._comboBox.SelectedIndexChanged += new EventHandler(_comboBox_SelectedIndexChanged);

            this._numericUpDown = new NumericUpDown ();
            this._numericUpDown.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this._numericUpDown.Minimum = 0;
            this._numericUpDown.Maximum = 10000;
            this._numericUpDown.Click +=new EventHandler(_numericUpDown_Click);


            this._unitComboBox = new ComboBox();
            this._unitComboBox.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            if(!Service.Util.DesignMode)
                this._unitComboBox.Items.AddRange(BorderSourceFile.BorderWidthUnit);
            this._unitComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this._unitComboBox.SelectedValueChanged += new EventHandler(_unitComboBox_SelectedValueChanged);

            this.Controls.Add(_comboBox);
            this.Controls.Add(_numericUpDown);
            this.Controls.Add(_unitComboBox);
        }

        /// <summary>
        /// 组合框中选择值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //void _comboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this._unitComboBox.Text = "";
        //    //this._unitComboBox.Enabled = false;
        //    this._numericUpDown.Value = Decimal.Parse("0");
        //    _getBorderWidth = BorderSourceFile.borderWidthDic[_comboBox.Text];
        //    OnCssChanged(EventArgs.Empty);
        //}

        void _unitComboBox_SelectedValueChanged(object sender, EventArgs e)
        {

            if (Regex.IsMatch(_comboBox.Text, @"^\d*$"))
            {
                if (this._unitComboBox.Text == "" && !String.IsNullOrEmpty(_comboBox.Text))
                {
                    _getUnit = "px";
                }
                else if (this._unitComboBox.Text == "")
                {
                    _getUnit = "";
                }
                else
                {
                    _getUnit = BorderSourceFile.borderUnitDic[_unitComboBox.Text];
                }
            }
            else
                _getUnit = "";
            OnCssChanged(EventArgs.Empty);
        }


       
        /// <summary>
        /// 显示宽度值的控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _comboBox_TextChanged(object sender, EventArgs e)
        {
            if (_isFirstChanged && this._comboBox.Text !="")
            {
                OnValueInited(EventArgs.Empty);
                _isFirstChanged = false;
            }

            if (Regex.IsMatch(_comboBox.Text, @"^\d*$"))
            {
                if (this._unitComboBox.Text == "" && !String.IsNullOrEmpty(_comboBox.Text))
                {
                    _getUnit = "px";
                }
                else if (this._unitComboBox.Text == "")
                {
                    _getUnit = "";
                }
                else
                {
                    _getUnit = BorderSourceFile.borderUnitDic[_unitComboBox.Text];
                }
            }
            else
                _getUnit = "";

            ///如果手动输入数字，则将输入的数字赋值给numericUpDown的value
            decimal d = 0;
            if (decimal.TryParse(this._comboBox.Text, out d))
            {
                if (d < 0)
                    this._numericUpDown.Value = 0;
                else if (d <= this._numericUpDown.Maximum)
                    this._numericUpDown.Value = d;
                else
                    this._numericUpDown.Value = _numericUpDown.Maximum;
                _getBorderWidth = this._comboBox.Text;
            }
            //else
            //{
            //    ///重新复值一下
            //    _comboBox.Text = "";// textPrev;
            //}
            else
            {
                if (!BorderSourceFile.borderWidthDic.TryGetValue(this._comboBox.Text,out _getBorderWidth))
                {
                    _getBorderWidth = "";
                }
                else
                    _getBorderWidth = BorderSourceFile.borderWidthDic[this._comboBox.Text];
            }

            OnCssChanged(EventArgs.Empty);
        }
        
        void _numericUpDown_Click(object sender, EventArgs e)
        {
            //一，为空时 二，是控件的另种选择 三，是本身的的加减 四，手动输入时的值，五，输入错误时
            if (this._unitComboBox.Text == "")
            {
                this._unitComboBox.Enabled = true;
                this._unitComboBox.SelectedIndex = 0;
            }
            else
            {
                ///此时，有一种情况是combobox里是其它的已选值
                if (BorderSourceFile.borderWidthDic.ContainsKey ( this._comboBox.Text ))
                {
                    this._unitComboBox.Enabled = true;
                    this._unitComboBox.SelectedIndex = 0;
                }
            }

            this._comboBox.Text = this._numericUpDown.Value.ToString();
            OnCssChanged(EventArgs.Empty);
        }

        protected override void OnCreateControl()
        {
            this._comboBox.Location     = new Point(0, 0);
            this._comboBox.Size         = new Size(100, 20);
            this._numericUpDown.Location = new Point(97, 0);
            this._numericUpDown.Size    = new Size(20, 21);
            this._unitComboBox.Location = new Point(120, 0);
            this._unitComboBox.Size     = new Size(50,20);
            this.Height = 20;

            base.OnCreateControl();
        }


        #region 事件的定义与声明

        ///没有定义委托，直接使用系统默认的委托类型
        
        /// <summary>
        /// 定义一个事件成员
        /// </summary>
        public event EventHandler ValueInited;
        /// <summary>
        /// 定义一个受保护的虚方法，负责通知事件的登记对象
        /// </summary>
        /// <param name="e"></param>
        protected void OnValueInited(EventArgs e)
        {
            ///检查是否有对象登记了事件，如果有，则触发事件。
            if (ValueInited != null)
            {
                ValueInited(this, e);
            }
        }

        public event EventHandler CssChanged;
        /// <summary>
        /// 当Css字符串发生改变时
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCssChanged(EventArgs e)
        {
            if (CssChanged != null)
            {
                CssChanged(this, e);
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 初始化现有的值
        /// </summary>
        public void InitValue(string cssStr)
        {
            if (Service.Util.StringIsAllNumber(cssStr))
            {
                _comboBox.Text = cssStr;
                _unitComboBox.SelectedIndex = 0;
            }
            else if (!Utility.Regex.HasAnyNumber(cssStr))
            {
                switch (cssStr)
                {
                    case "thin":
                        _comboBox.Text = StringParserService.Parse("${res:cssDesign.border.thin}");
                        break;
                    case "auto":
                        _comboBox.Text = StringParserService.Parse("${res:cssDesign.border.auto}");
                        break;
                    case "medium":
                        _comboBox.Text = StringParserService.Parse("${res:cssDesign.border.medium}");
                        break;
                    case "thick":
                        _comboBox.Text = StringParserService.Parse("${res:cssDesign.border.thick}");
                        break;
                    default:
                        break;
                }
                _unitComboBox.Text = "";
            }
            else
            {
                KeyValuePair<string, int> uintValue = Service.Util.GetSizeAndUnit(cssStr);
                _comboBox.Text = uintValue.Key;
                _unitComboBox.SelectedIndex = uintValue.Value;
            }
        }

        #endregion
    }
}
