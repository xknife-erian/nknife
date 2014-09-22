using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class CSSGeneralContectControl : UserControl
    {
        #region 构造函数

        public CSSGeneralContectControl()
        {
            InitializeComponent();
            if (!Service.Util.DesignMode)
            {
                this.comboBoxSizeUnit.Items.AddRange(new string[]{
                    StringParserService.Parse("${res:TextpropertyPanel.sizeunit.px}"),
                    StringParserService.Parse("${res:TextpropertyPanel.sizeunit.pt}"),
                    StringParserService.Parse("${res:TextpropertyPanel.sizeunit.in}"),
                    StringParserService.Parse("${res:TextpropertyPanel.sizeunit.cm}"),
                    StringParserService.Parse("${res:TextpropertyPanel.sizeunit.mm}"),
                    StringParserService.Parse("${res:TextpropertyPanel.sizeunit.pc}"),
                    StringParserService.Parse("${res:TextpropertyPanel.sizeunit.em}"),
                    StringParserService.Parse("${res:TextpropertyPanel.sizeunit.ex}"),
                    StringParserService.Parse("${res:TextpropertyPanel.sizeunit.per}")});
            }
        }

        #endregion

        #region 内部变量

        private CssSection CssDic = new CssSection();

        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置Css
        /// </summary>
        public string Css
        {
            get
            {
                return CssDic.ToString();
            }
        }

        #endregion

        #region 控件事件

        private void _comboBoxTextAlign_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_comboBoxTextAlign.SelectedIndex)
            {
                case 0 :
                    CssDic.Properties["text-align"] = "center";
                    break;
                case 1:
                    CssDic.Properties["text-align"] = "left";
                    break;
                case 2:
                    CssDic.Properties["text-align"] = "right";
                    break;
                case 3:
                    CssDic.Properties["text-align"] = "";
                    break;
                default:
                    break;
            }
            OnCssChanged(EventArgs.Empty);
        }

        private void _comboBoxOverFlow_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_comboBoxOverFlow.SelectedIndex)
            {
                case 0:
                    CssDic.Properties["overflow"] = "auto";
                    break;
                case 1:
                    CssDic.Properties["overflow"] = "visible";
                    break;
                case 2:
                    CssDic.Properties["overflow"] = "hidden";
                    break;
                case 3:
                    CssDic.Properties["overflow"] = "scroll";
                    break;
                case 4:
                    CssDic.Properties["overflow"] = "";
                    break;
                default:
                    break;
            }
            OnCssChanged(EventArgs.Empty);
        }

        private void textBoxHeight_TextChanged(object sender, EventArgs e)
        {
            string _uint = "px";
            switch (comboBoxSizeUnit.SelectedIndex)
            {
                case 0:
                    _uint = "px";
                    break;
                case 1:
                    _uint = "pt";
                    break;
                case 2:
                    _uint = "in";
                    break;
                case 3:
                    _uint = "cm";
                    break;
                case 4:
                    _uint = "mm";
                    break;
                case 5:
                    _uint = "pc";
                    break;
                case 6:
                    _uint = "em";
                    break;
                case 7:
                    _uint = "ex";
                    break;
                case 8:
                    _uint = "%";
                    break;
                default:
                    break;
            }
            if(String.IsNullOrEmpty(_textBoxHeight.Text))
                CssDic.Properties["line-height"] = "";
            else
                CssDic.Properties["line-height"] = _textBoxHeight.Text + _uint;
            OnCssChanged(EventArgs.Empty);
        }

        private void comboBoxClear_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_comboBoxClear.SelectedIndex)
            {
                case 0:
                    CssDic.Properties["clear"] = "both";
                    break;
                case 1:
                    CssDic.Properties["clear"] = "left";
                    break;
                case 2:
                    CssDic.Properties["clear"] = "right";
                    break;
                case 3:
                    CssDic.Properties["clear"] = "none";
                    break;
                default:
                    CssDic.Properties["clear"] = "";
                    break;
            }
            OnCssChanged(EventArgs.Empty);
        }

        private void _comboBoxVerticalAlign_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_comboBoxVerticalAlign.SelectedIndex)
            {
                case 0:
                    CssDic.Properties["vertical-align"] = "middle";
                    break;
                case 1:
                    CssDic.Properties["vertical-align"] = "top";
                    break;
                case 2:
                    CssDic.Properties["vertical-align"] = "bottom";
                    break;
                case 3:
                    CssDic.Properties["vertical-align"] = "text-bottom";
                    break;
                case 4:
                    CssDic.Properties["vertical-align"] = "text-top";
                    break;
                case 5:
                    CssDic.Properties["vertical-align"] = "sub";
                    break;
                case 6:
                    CssDic.Properties["vertical-align"] = "super";
                    break;
                case 7:
                    CssDic.Properties["vertical-align"] = "baseline";
                    break;
                default:
                    break;
            }
            OnCssChanged(EventArgs.Empty);
        }        

        #endregion

        #region 自定义事件

        /// <summary>
        /// 当Css属性变化时
        /// </summary>
        public event EventHandler CssChanged;

        /// <summary>
        /// 当Css属性变化时
        /// </summary>
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
        public void InitValue(CssSection cssDic)
        {
            CssDic = cssDic;
            string strValue = "";
            if (CssDic.Properties.TryGetValue("overflow", out strValue))
            {
                switch (strValue)
                {
                    case "auto":
                        _comboBoxOverFlow.SelectedIndex = 0;
                        break;
                    case "visible":
                        _comboBoxOverFlow.SelectedIndex = 1;
                        break;
                    case "hidden":
                        _comboBoxOverFlow.SelectedIndex = 2;
                        break;
                    case "scroll":
                        _comboBoxOverFlow.SelectedIndex = 3;
                        break;
                    default:
                        break;
                }
            }
            
            if (CssDic.Properties.TryGetValue("clear", out strValue))
            {
                switch (strValue)
                {
                    case "both":
                        _comboBoxClear.SelectedIndex = 0;
                        break;
                    case "left":
                        _comboBoxClear.SelectedIndex = 1;
                        break;
                    case "right":
                        _comboBoxClear.SelectedIndex = 2;
                        break;
                    case "none":
                        _comboBoxClear.SelectedIndex = 3;
                        break;                    
                    default:
                        break;
                }
            }
            
            if (CssDic.Properties.TryGetValue("vertical-align", out strValue))
            {
                switch (strValue)
                {
                    case "middle":
                        _comboBoxVerticalAlign.SelectedIndex = 0;
                        break;
                    case "top":
                        _comboBoxVerticalAlign.SelectedIndex = 1;
                        break;
                    case "bottom":
                        _comboBoxVerticalAlign.SelectedIndex = 2;
                        break;
                    case "text-bottom":
                        _comboBoxVerticalAlign.SelectedIndex = 3;
                        break;
                    case "text-top":
                        _comboBoxVerticalAlign.SelectedIndex = 4;
                        break;
                    case "sub":
                        _comboBoxVerticalAlign.SelectedIndex = 5;
                        break;
                    case "super":
                        _comboBoxVerticalAlign.SelectedIndex = 6;
                        break;
                    case "baseline":
                        _comboBoxVerticalAlign.SelectedIndex = 7;
                        break;
                    default:
                        break;
                }
            }
            
            if (CssDic.Properties.TryGetValue("text-align", out strValue))
            {
                switch (strValue)
                {
                    case "left":
                        _comboBoxTextAlign.SelectedIndex = 1;
                        break;
                    case "right":
                        _comboBoxTextAlign.SelectedIndex = 2;
                        break;
                    case "center":
                        _comboBoxTextAlign.SelectedIndex = 0;
                        break;
                    default:
                        break;
                }
            }
            
            if (CssDic.Properties.TryGetValue("line-height", out strValue))
            {
                KeyValuePair<string, int> _kv = Jeelu.SimplusD.Client.Win.Service.Util.GetSizeAndUnit(strValue);
                _textBoxHeight.Text = _kv.Key;
                comboBoxSizeUnit.SelectedIndex = _kv.Value;
            }
        }

        #endregion

        //#region 内部方法

        ///// <summary>
        ///// 解析长度值为 数值+单位
        ///// </summary>
        ///// <param name="size"></param>
        ///// <returns></returns>
        //private KeyValuePair<string, int> GetSizeAndUnit(string size)
        //{
        //    string _key = "";
        //    int _value = 0;
        //    if (string.IsNullOrEmpty(size))
        //    {
        //        return new KeyValuePair<string, int>(_key, _value);
        //    }
        //    if (size.Length == 1 && int.TryParse(size,out _value))
        //    {
        //        return new KeyValuePair<string, int>(_value.ToString(), 0);
        //    }
        //    string _unit = size.Substring(size.Length - 2, 2);
        //    if (size[size.Length - 1] == '%')
        //    {
        //        _key = size.Remove(size.Length - 1);
        //        _value = 8;
        //    }
        //    else
        //    {
        //        switch (_unit)
        //        {
        //            case "px":
        //                _key = size.Remove(size.Length - 2);
        //                _value = 0;
        //                break;
        //            case "pt":
        //                _key = size.Remove(size.Length - 2);
        //                _value = 1;
        //                break;
        //            case "in":
        //                _key = size.Remove(size.Length - 2);
        //                _value = 2;
        //                break;
        //            case "cm":
        //                _key = size.Remove(size.Length - 2);
        //                _value = 3;
        //                break;
        //            case "mm":
        //                _key = size.Remove(size.Length - 2);
        //                _value = 4;
        //                break;
        //            case "pc":
        //                _key = size.Remove(size.Length - 2);
        //                _value = 5;
        //                break;
        //            case "em":
        //                _key = size.Remove(size.Length - 2);
        //                _value = 6;
        //                break;
        //            case "ex":
        //                _key = size.Remove(size.Length - 2);
        //                _value = 7;
        //                break;
        //            default:
        //                _key = size;
        //                break;
        //        }
        //    }
        //    return new KeyValuePair<string, int>(_key, _value);
        //}

        //#endregion

    }
}
