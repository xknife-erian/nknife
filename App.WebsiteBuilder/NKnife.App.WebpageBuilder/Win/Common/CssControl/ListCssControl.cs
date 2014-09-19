using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    [ResReader(false)]
    public partial class ListCssControl : CssMainControl
    {
        const string TYPE ="list-style-type";
        const string IMAGE = "list-style-image";
        const string POSTION = "list-style-position";
        
        public ListCssControl()
        {
            InitializeComponent();
            Init();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string imageId = CssUtility.SelectImageResource();
            if (!string.IsNullOrEmpty(imageId))
            {
                string imageUrl = Utility.Regex.FormatResourceId(imageId);
                this.cbxProjSignImage.Text = imageUrl;
            }
        }

        #region 重写方法
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public override void EnterLoad()
        {
            InitControl();
            base.EnterLoad();
        }

        public override bool LeaveValidate()
        {
            WriteValueToCss();
            bool isError = CheckInputValidate();
            if (isError)
            {
                return false;
            }
            
            return base.LeaveValidate();
        }

        #endregion

        #region 内部方法
        private void Init()
        {
            CssResourceList list;
            list = CssResources.Resources[TYPE];
            foreach (string text in list.Texts)
            {
                this.cbxStyle.Items.Add(text);
            }

            list = CssResources.Resources[IMAGE];
            foreach (string text in list.Texts)
            {
                this.cbxProjSignImage.Items.Add(text);
            }

            list = CssResources.Resources[POSTION];
            foreach (string text in list.Texts)
            {
                this.cbxPostion.Items.Add(text);
            }
        }

        private void InitControl()
        {
            if (Value == null)
            {
                return;
            }

            string cssValue = "";

            //类型
            if (Value.Properties.TryGetValue(TYPE, out cssValue))
            {
                string _value = CssResources.Resources[TYPE].ValueToText(cssValue);
                this.cbxStyle.Text = string.IsNullOrEmpty(_value) ? cssValue : _value;
            }
            else { this.cbxStyle.Text = ""; }

            //图像
            if (Value.Properties.TryGetValue(IMAGE, out cssValue))
            {
                string text = cssValue;
                string _value = "";


                if (string.IsNullOrEmpty(text))
                {
                    _value = "";
                }
                else if (string.Compare(text, "none") == 0)
                {
                    _value = "无";
                }
                else
                {
                    if (text.StartsWith("url("))
                    {
                        //除掉url()符号
                        string val = text.Remove(0, 4);
                        _value = val.Remove(val.Length - 1, 1);
                    }
                    else
                        _value = text;
                }
                cbxProjSignImage.Text = _value;
            }
            else { cbxProjSignImage.Text = ""; }

            //位置
            if (Value.Properties.TryGetValue(POSTION, out cssValue))
            {
                string _value = CssResources.Resources[POSTION].ValueToText(cssValue);
                this.cbxPostion.Text = string.IsNullOrEmpty(_value) ? cssValue : _value;
            }
            else { this.cbxPostion.Text = ""; }
        }

        /// <summary>
        /// 将控件值写入到css里
        /// </summary>
        private void WriteValueToCss()
        {
            Value.Properties[TYPE] = ReturnValue(TYPE ,cbxStyle.Text);
            Value.Properties[IMAGE] = GetImageValue();
            Value.Properties[POSTION] = ReturnValue (POSTION , cbxPostion.Text);
        }
        
        /// <summary>
        /// 返回合理的属性值，如位置中的"内"返回inside
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ReturnValue(string property,string value)
        {
            CssResourceList list = CssResources.Resources[property];
            string text = value;
            if (list.HasValue(text))
            {
                return list.GetValue(text);
            }
            return text;
        }

        private string GetImageValue()
        {
            string _value = "";
            string _text = cbxProjSignImage.Text;
            if (_text == "")
            {
                _value = "";
            }
            else if (string.Compare(_text, "无") == 0 || string.Compare(_text, "none") == 0)
            {
                _value = "none";
            }
            else 
            {
                _value = "url(" + _text + ")";
            }
            return _value;
        }

        /// <summary>
        /// 检查控件输入的正确性
        /// </summary>
        /// <returns>true,表示有错误</returns>
        bool CheckInputValidate()
        {
            bool isError = false;
            string _styleValue = this.cbxStyle.Text;
            string _posValue = this.cbxPostion.Text;
            isError = CssResources.CheckValue(TYPE, _styleValue, false);
            if (isError)
            {
                cbxStyle.Focus();
                bool isUse = CssUtility.ShowNotStandard(_styleValue);
                if (!isUse)
                {
                    return isError;    
                }
                isError = false;
            }
            isError = CssResources.CheckValue(POSTION, _posValue, false);
            if (isError)
            {
                cbxPostion.Focus();
                bool isUse = CssUtility.ShowNotStandard(_posValue);
                if (!isUse)
                {
                    return isError;    
                }
                isError = false; 
            }
            return isError;
        }
        #endregion
    }
}
