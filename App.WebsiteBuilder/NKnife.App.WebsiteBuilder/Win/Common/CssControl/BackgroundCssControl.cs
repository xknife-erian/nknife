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
    public partial class BackgroundCssControl : CssMainControl
    {
        const string COLOR = "background-color";
        const string IMAGE = "background-image";
        const string REPEAT = "background-repeat";
        const string ATTACHMENT = "background-attachment";
        const string POSTION = "background-position";

        public BackgroundCssControl()
        {
            InitializeComponent();
            Init();
        }
        
        #region 重写事件
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
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

        public override void EnterLoad()
        {
            InitControl();
            base.EnterLoad();
        }

        #endregion

        #region 控件事件
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            string imageId = CssUtility.SelectImageResource();
            if (!string.IsNullOrEmpty(imageId))
            {
                string imageUrl = Utility.Regex.FormatResourceId(imageId);
                this.cbxBkImage.Text = imageUrl;
              //  string url = "url(" + imageUrl + ")";
                //       SetBackGroundImage(url);
            }

        }

        private void ColorBtnBackground_ColorTextChanged(object sender, EventArgs e)
        {
            string colorVal = this.ColorBtnBackground.ColorText;
            if (!this.ColorBtnBackground.CheckColor)
            {
                CssUtility.ShowNotStandardColorMsg(colorVal);
                this.ColorBtnBackground.Focus();
                this.ColorBtnBackground.ColorText = "";
            }
            else
            {
                 Value.Properties[COLOR] = colorVal;
            }

        }

        #endregion

        #region 内部方法
        private void Init()
        {
            CssResourceList list;
            list = CssResources.Resources[IMAGE];
            foreach (string text in list.Texts)
            {
                cbxBkImage.Items.Add(text);
            }
            list = CssResources.Resources[REPEAT];
            foreach (string text in list.Texts)
            {
                cbxRepeat.Items.Add(text);
            }
            list = CssResources.Resources[ATTACHMENT];
            foreach (string text in list.Texts)
            {
                cbxAttachment.Items.Add(text);
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            if (Value == null)
            {
                return;
            }
            string ctrlValue = "";

            //颜色
            this.ColorBtnBackground.ColorText = Value.Properties[COLOR];
            
            //图像
            if (Value.Properties.TryGetValue(IMAGE, out ctrlValue))
            {
                string text = ctrlValue;
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
                cbxBkImage.Text = _value;
            }
            else { cbxBkImage.Text = ""; }

            //重复
            if (Value.Properties.TryGetValue(REPEAT, out ctrlValue))
            {
                string _value = CssResources.Resources[REPEAT].ValueToText(ctrlValue);
                cbxRepeat.Text = string.IsNullOrEmpty(_value) ? ctrlValue : _value;
            }
            else { cbxRepeat.Text = ""; }

            //附件
            if (Value.Properties.TryGetValue(ATTACHMENT, out ctrlValue))
            {
                string _value = CssResources.Resources[ATTACHMENT].ValueToText(ctrlValue);
                cbxAttachment.Text = string.IsNullOrEmpty(_value) ? ctrlValue : _value;
            }
            else { cbxAttachment.Text = ""; }

            //位置
            if (Value.Properties.TryGetValue(POSTION, out ctrlValue))
            {
                KeyValuePair<string, string> pair = CssUtility.ParseBackgroundPosition(ctrlValue);
                string level = pair.Key;
                string vertical = pair.Value;

                this.cssFieldUnitLevel.Value = level;
                this.cssFieldUnitVertical.Value = vertical;
            }
            else
            {
                this.cssFieldUnitLevel.Value = "";
                this.cssFieldUnitVertical.Value = "";
            }

        }

        /// <summary>
        /// 设置背景位置
        /// </summary>
        private void SetBackGroundPostion()
        {
            string level = this.cssFieldUnitLevel.Value;
            string vertical = this.cssFieldUnitVertical.Value;

            string postion = level + " " + vertical;

            Value.Properties[POSTION] = postion;
        }

        /// <summary>
        /// 将控件的值写入到css里
        /// </summary>
        private void WriteValueToCss()
        {
         // Value.Properties[COLOR] = ReturnValue(COLOR, ColorBtnBackground.ColorText); //控件内部已处理
            Value.Properties[IMAGE] = GetImageValue();
            Value.Properties[REPEAT] = ReturnValue(REPEAT, cbxRepeat.Text);
            Value.Properties[ATTACHMENT] = ReturnValue(ATTACHMENT, cbxAttachment.Text);
            SetBackGroundPostion();
        }

        /// <summary>
        /// 返回合理的属性值，如位置中的"内"返回inside
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ReturnValue(string property, string value)
        {
            CssResourceList list = CssResources.Resources[property];
            string text = value;
            if (list.HasValue(text))
            {
                return list.GetValue(text);
            }
            return text;
        }
        /// <summary>
        /// 得到背景图像的css值
        /// </summary>
        /// <returns></returns>
        private string GetImageValue()
        {
            string _value = "";
            string _text = this.cbxBkImage.Text;
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
        private bool CheckInputValidate()
        {
            bool isError = false;
            foreach (Control control in this.Controls)
            {
                isError = false;
                switch (control.Name)
                {
                    case "cbxRepeat":
                        isError = CssResources.CheckValue(REPEAT, ((ComboBox)control).Text, false);
                        if (isError)
                        {
                            control.Focus();
                            bool isUse = CssUtility.ShowNotStandard(control.Text);
                            if (!isUse)
                            {
                                return isError;
                            }
                        }
                        break;
                    case "cbxAttachment":

                        isError = CssResources.CheckValue(ATTACHMENT, ((ComboBox)control).Text, false);
                        if (isError)
                        {
                            control.Focus();
                            bool isUse = CssUtility.ShowNotStandard(control.Text);
                            if (!isUse)
                            {
                                return isError;
                            }
                        }
                        break;
                    case "cssFieldUnitLevel":
                        isError = !this.cssFieldUnitLevel.CheckValue();
                        if (isError)
                        {
                            cssFieldUnitLevel.SelectFirst();
                            return true;
                        }
                        break;
                    case "cssFieldUnitVertical":
                        isError = !this.cssFieldUnitVertical.CheckValue();
                        if (isError)
                        {
                            cssFieldUnitVertical.SelectFirst();
                            return true;
                        }
                        break;
                    default:
                        break;
                }

            }
            return false;
        }
        #endregion

    }
}
