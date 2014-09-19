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
    public partial class CSSGeneralLayoutControl : UserControl
    {
        #region 构造函数

        public CSSGeneralLayoutControl()
        {
            InitializeComponent();
            if (!Service.Util.DesignMode)
            {
                this.comboBoxWidthSizeUnit.Items.AddRange(new string[]{
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.px}"),
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.pt}"),
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.in}"),
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.cm}"),
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.mm}"),
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.pc}"),
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.em}"),
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.ex}"),
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.per}")});
                this.comboBoxHeightSizeUnit.Items.AddRange(new string[]{
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

        #region 内部函数

        private string _wUnit = "px";

        private string _hUnit = "px";

        CssSection CssDic = new CssSection();

        #endregion

        #region 属性

        /// <summary>
        /// 获取CSS字符串
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

        private void _comboBoxlayoutFloat_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_comboBoxlayoutFloat.SelectedIndex)
            {
                case 0:
                    CssDic.Properties["float"] = "none";
                    break;
                case 1:
                    CssDic.Properties["float"] = "left";
                    break;
                case 2:
                    CssDic.Properties["float"] = "right";
                    break;
                case 3:
                    CssDic.Properties["float"] = "";
                    break;
                default:
                    break;
            }

            OnCssChanged(EventArgs.Empty);
        }

        private void _textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_textBoxWidth.Text))
                CssDic.Properties["width"] = "";
            else
                CssDic.Properties["width"] = _textBoxWidth.Text + _wUnit;
            OnCssChanged(EventArgs.Empty);
        }

        private void _textBoxHeight_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_textBoxHeight.Text))
                CssDic.Properties["height"] = "";
            else
                CssDic.Properties["height"] = _textBoxHeight.Text + _hUnit;
            OnCssChanged(EventArgs.Empty);
        }

        private void comboBoxWidthSizeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxWidthSizeUnit.SelectedIndex)
            {
                case 0:
                    _wUnit = "px";
                    break;
                case 1:
                    _wUnit = "pt";
                    break;
                case 2:
                    _wUnit = "in";
                    break;
                case 3:
                    _wUnit = "cm";
                    break;
                case 4:
                    _wUnit = "mm";
                    break;
                case 5:
                    _wUnit = "pc";
                    break;
                case 6:
                    _wUnit = "em";
                    break;
                case 7:
                    _wUnit = "ex";
                    break;
                case 8:
                    _wUnit = "%";
                    break;
                default:
                    break;
            }

            if (String.IsNullOrEmpty(_textBoxWidth.Text))
                CssDic.Properties["width"] = "";
            else
                CssDic.Properties["width"] = _textBoxWidth.Text + _wUnit;
            OnCssChanged(EventArgs.Empty);
        }

        private void comboBoxHeightSizeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxHeightSizeUnit.SelectedIndex)
            {
                case 0:
                    _hUnit = "px";
                    break;
                case 1:
                    _hUnit = "pt";
                    break;
                case 2:
                    _hUnit = "in";
                    break;
                case 3:
                    _hUnit = "cm";
                    break;
                case 4:
                    _hUnit = "mm";
                    break;
                case 5:
                    _hUnit = "pc";
                    break;
                case 6:
                    _hUnit = "em";
                    break;
                case 7:
                    _hUnit = "ex";
                    break;
                case 8:
                    _hUnit = "%";
                    break;
                default:
                    break;
            }

            if (String.IsNullOrEmpty(_textBoxHeight.Text))
                CssDic.Properties["height"] = "";
            else
                CssDic.Properties["height"] = _textBoxHeight.Text + _hUnit;
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

            if (CssDic.Properties.TryGetValue("float", out strValue))
            {
                switch (strValue)
                {
                    case "left":
                        _comboBoxlayoutFloat.SelectedIndex = 1;
                        break;
                    case "right":
                        _comboBoxlayoutFloat.SelectedIndex = 2;
                        break;
                    default:
                        _comboBoxlayoutFloat.SelectedIndex = 0;
                        break;
                }
            }
            else
                _comboBoxlayoutFloat.SelectedIndex = 0;

            if (CssDic.Properties.TryGetValue("width", out strValue))
            {
                KeyValuePair<string, int> _kv =Service.Util.GetSizeAndUnit(strValue);
                _textBoxWidth.Text = _kv.Key;
                comboBoxWidthSizeUnit.SelectedIndex = _kv.Value;
                switch (_kv.Value)
                {
                    case 0:
                        _wUnit = "px";
                        break;
                    case 1:
                        _wUnit = "pt";
                        break;
                    case 2:
                        _wUnit = "in";
                        break;
                    case 3:
                        _wUnit = "cm";
                        break;
                    case 4:
                        _wUnit = "mm";
                        break;
                    case 5:
                        _wUnit = "pc";
                        break;
                    case 6:
                        _wUnit = "em";
                        break;
                    case 7:
                        _wUnit = "ex";
                        break;
                    case 8:
                        _wUnit = "%";
                        break;
                    default:
                        break;
                }
            }

            if (CssDic.Properties.TryGetValue("height", out strValue))
            {
                KeyValuePair<string, int> _kv = Service.Util.GetSizeAndUnit(strValue);
                _textBoxHeight.Text = _kv.Key;
                comboBoxHeightSizeUnit.SelectedIndex = _kv.Value;
                switch (_kv.Value)
                {
                    case 0:
                        _hUnit = "px";
                        break;
                    case 1:
                        _hUnit = "pt";
                        break;
                    case 2:
                        _hUnit = "in";
                        break;
                    case 3:
                        _hUnit = "cm";
                        break;
                    case 4:
                        _hUnit = "mm";
                        break;
                    case 5:
                        _hUnit = "pc";
                        break;
                    case 6:
                        _hUnit = "em";
                        break;
                    case 7:
                        _hUnit = "ex";
                        break;
                    case 8:
                        _hUnit = "%";
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        
    }
}
