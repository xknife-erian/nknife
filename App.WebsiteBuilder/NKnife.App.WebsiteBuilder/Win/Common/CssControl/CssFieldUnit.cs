using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public enum CssFieldUnitType
    {
        /// <summary>
        /// 行高类型
        /// </summary>
        LineHeight = 0,
        /// <summary>
        /// 字体大小
        /// </summary>
        FontSize = 1,
        /// <summary>
        /// 水平位置
        /// </summary>
        LevelPosition = 2,
        /// <summary>
        /// 垂直位置
        /// </summary>
        VerticalPosition = 3,
        /// <summary>
        /// 文字缩进
        /// </summary>
        TextIndent = 4,
        /// <summary>
        /// 单词间距
        /// </summary>
        WordSpacing = 5,
        /// <summary>
        /// 字母间距
        /// </summary>
        LetterSpacing = 6,
        /// <summary>
        /// 垂直对齐
        /// </summary>
        VerticalAlign = 7,
        /// <summary>
        /// 填充
        /// </summary>
        Padding = 8,
        /// <summary>
        /// 边界、宽度、高度、定位
        /// </summary>
        MarginWidthHeightPosition = 9,
        /// <summary>
        /// 边框宽度
        /// </summary>
        BorderWidth = 10,
        /// <summary>
        /// 剪辑
        /// </summary>
        Clip = 11,
        /// <summary>
        /// Part使用
        /// </summary>
        Part = 12,

        None = 13
    }

    [ResReader(false)]
    public partial class CssFieldUnit : UserControl
    {
        #region 内部变量
            
        private CssFieldUnitType _fieldUnitType = CssFieldUnitType.None;
        private string _value= "";

        #endregion

        #region 公共属性

        /// <summary>
        /// 设置或获取此控件的类型。
        /// </summary>
        public CssFieldUnitType FieldUnitType
        {
            get { return _fieldUnitType; }
            set 
            { 
                _fieldUnitType = value;
                if (!this.DesignMode)
                {
                    Init();
                }
            }
        }

        /// <summary>
        /// 获取或设置Css值
        /// </summary>
        public string Value
        {
            get { return _value; }
            set 
            { 
                _value = value;
                InitControl();
            }
        }

        #endregion

        #region 构造函数

        public CssFieldUnit()
        {
            InitializeComponent();
            if (!this.DesignMode)
            {
                Init();
            }
        }

        #endregion

        #region 重写函数

        protected override void OnLoad(EventArgs e)
        {
            if (_value == null)
            {
                _value = "";
            }
            cbbField.TextChanged += new EventHandler(cbbField_TextChanged);
            cbbUnit.TextChanged += new EventHandler(cbbUnit_TextChanged);
            base.OnLoad(e);
        }

        #endregion

        #region 控件事件

        void cbbUnit_TextChanged(object sender, EventArgs e)
        {
            GetValue();
            OnValueChanged(EventArgs.Empty);
        }

        void cbbField_TextChanged(object sender, EventArgs e)
        {
            double d = 0;
            if (double.TryParse(cbbField.Text, out d))
            {
                cbbUnit.Enabled = true;
                //if (cbbUnit.Items.Count > 0)
                //{
                //    cbbUnit.SelectedIndex = 0;
                //}
            }
            else
            {
                cbbUnit.Text = "";
                cbbUnit.Enabled = false;
            }
            GetValue();
            OnValueChanged(EventArgs.Empty);
        }

        #endregion

        #region 自定义事件

        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        #endregion

        #region 内部方法

        private void GetValue()
        {
            string field = cbbField.Text;
            string unit = cbbUnit.Text;

            if (string.IsNullOrEmpty(_value)) 
            {
                _value = "";
            }
            if (string.IsNullOrEmpty(field))
            {
                _value = "";
                return;
            }

            CssResourceList list = new CssResourceList();
            
            switch (_fieldUnitType)
            {
                case CssFieldUnitType.LineHeight:
                    #region
                    {
                        list = CssResources.Resources["normal"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["lineHeightUnit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.FontSize:
                    #region
                    {
                        list = CssResources.Resources["font-size"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["unit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.LevelPosition:
                    #region
                    {
                        list = CssResources.Resources["levelPostion"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["unit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.VerticalPosition:
                    #region
                    {
                        list = CssResources.Resources["verticalPostion"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["unit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.TextIndent:
                    #region
                    {
                        list = CssResources.Resources["unit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.WordSpacing:
                    #region
                    {
                        list = CssResources.Resources["normal"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["wordSpaceUnit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.LetterSpacing:
                    #region
                    {
                        list = CssResources.Resources["normal"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["letterSpaceUnit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.VerticalAlign:
                    #region
                    {
                        list = CssResources.Resources["vertical-align"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["verticalUnit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Padding:
                    #region
                    {
                        list = CssResources.Resources["value"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["unit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.MarginWidthHeightPosition:
                    #region
                    {
                        list = CssResources.Resources["auto"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["unit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.BorderWidth:
                    #region
                    {
                        list = CssResources.Resources["borderWidth"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["unit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Clip:
                    #region
                    {
                        list = CssResources.Resources["auto"];
                        if (list.HasValue(field))
                        {
                            field = list.GetValue(field);
                        }
                        list = CssResources.Resources["letterSpaceUnit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Part:
                    #region
                    {
                        list = CssResources.Resources["partUnit"];
                        if (list.HasValue(unit) && cbbUnit.Enabled)
                        {
                            unit = list.GetValue(unit);
                        }
                        else
                            unit = "";
                        _value = field + unit;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.None:
                    #region
                    {
                    }
                    break;
                    #endregion
                default:
                    break;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            CssResourceList list = new CssResourceList();
            cbbField.Items.Clear();
            cbbUnit.Items.Clear();
            switch (_fieldUnitType)
            {
                case CssFieldUnitType.LineHeight:
                    #region
                    {                        
                        list = CssResources.Resources["normal"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["lineHeightUnit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion                    
                case CssFieldUnitType.FontSize:
                    #region
                    {
                        list = CssResources.Resources["font-size"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["unit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.LevelPosition:
                    #region
                    {
                        list = CssResources.Resources["levelPostion"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["unit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.VerticalPosition:
                    #region
                    {
                        list = CssResources.Resources["verticalPostion"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["unit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.TextIndent:
                    #region
                    {
                        list = CssResources.Resources["unit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.WordSpacing:
                    #region
                    {
                        list = CssResources.Resources["normal"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["wordSpaceUnit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.LetterSpacing:
                    #region
                    {
                        list = CssResources.Resources["normal"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["letterSpaceUnit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.VerticalAlign:
                    #region
                    {
                        list = CssResources.Resources["vertical-align"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["verticalUnit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Padding:
                    #region
                    {
                        list = CssResources.Resources["value"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["unit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.MarginWidthHeightPosition:
                    #region
                    {
                        list = CssResources.Resources["auto"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["unit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.BorderWidth:
                    #region
                    {
                        list = CssResources.Resources["borderWidth"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["unit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Part:
                    #region
                    {
                        list = CssResources.Resources["partUnit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Clip:
                    #region
                    {
                        list = CssResources.Resources["auto"];
                        foreach (string text in list.Texts)
                        {
                            cbbField.Items.Add(text);
                        }
                        list = CssResources.Resources["letterSpaceUnit"];
                        foreach (string text2 in list.Texts)
                        {
                            cbbUnit.Items.Add(text2);
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.None:
                    #region
                    {
                    }
                    break;
                    #endregion
                default:
                    break;
            }
        }

        /// <summary>
        /// 根据源设置各个控件的值
        /// </summary>
        private void InitControl()
        {
            if (_value ==null)
            {
                _value = "";
            }
            CssResourceList list = new CssResourceList();
            KeyValuePair<string, string> fieldUnit = new KeyValuePair<string, string>();
            bool isGeneralString = CssUtility.ParseFieldUnit(Value, out fieldUnit);
            switch (_fieldUnitType)
            {
                case CssFieldUnitType.LineHeight:
                    #region
                    {
                        if (isGeneralString)
                        {
                            if (string.IsNullOrEmpty(fieldUnit.Value))
                            {
                                cbbField.Text = Value;
                                cbbUnit.Text = "倍行高";
                                cbbUnit.Enabled = true;
                            }
                            else
                            {
                                cbbField.Text = fieldUnit.Key;                                
                                list = CssResources.Resources["lineHeightUnit"];
                                cbbUnit.Text = list.ValueToText(fieldUnit.Value);
                                cbbUnit.Enabled = true;
                            }
                        }
                        else
                        {
                            list = CssResources.Resources["normal"];
                            if (list.HasValue(Value))
                            {
                                cbbField.Text = list.ValueToText(Value);
                            }
                            else
                                cbbField.Text = Value;
                            cbbUnit.Text = "";
                            cbbUnit.Enabled = false;
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.FontSize:
                    #region
                    {
                        InitControls("font-size", "unit", isGeneralString, fieldUnit);
                    }
                    break;
                    #endregion
                case CssFieldUnitType.LevelPosition:
                    #region
                    {
                        InitControls("levelPostion", "unit", isGeneralString, fieldUnit);
                    }
                    break;
                    #endregion
                case CssFieldUnitType.VerticalPosition:
                    #region
                    {
                        InitControls("verticalPostion", "unit", isGeneralString, fieldUnit);
                    }
                    break;
                    #endregion
                case CssFieldUnitType.TextIndent:
                    #region
                    {
                        if (isGeneralString)
                        {
                            if (string.IsNullOrEmpty(fieldUnit.Value))
                            {
                                cbbField.Text = Value;
                                cbbUnit.Text = "";
                                cbbUnit.Enabled = true;
                            }
                            else
                            {
                                cbbField.Text = fieldUnit.Key;
                                list = CssResources.Resources["unit"];
                                cbbUnit.Text = list.ValueToText(fieldUnit.Value);
                                cbbUnit.Enabled = true;
                            }
                        }
                        else
                        {
                            cbbField.Text = Value;
                            cbbUnit.Text = "";
                            cbbUnit.Enabled = false;
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.WordSpacing:
                    #region
                    {
                        InitControls("normal", "wordSpaceUnit", isGeneralString, fieldUnit);
                    }
                    break;
                    #endregion
                case CssFieldUnitType.LetterSpacing:
                    #region
                    {
                        InitControls("normal", "letterSpaceUnit", isGeneralString, fieldUnit);
                    }
                    break;
                    #endregion
                case CssFieldUnitType.VerticalAlign:
                    #region
                    {
                        InitControls("vertical-align", "verticalUnit", isGeneralString, fieldUnit);
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Padding:
                    #region
                    {
                        InitControls("value", "unit", isGeneralString, fieldUnit);
                    }
                    break;
                    #endregion
                case CssFieldUnitType.MarginWidthHeightPosition:
                    #region
                    {
                        InitControls("auto", "unit", isGeneralString, fieldUnit);
                    }
                    break;
                    #endregion
                case CssFieldUnitType.BorderWidth:
                    #region
                    {
                        InitControls("borderWidth", "unit", isGeneralString, fieldUnit);
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Clip:
                    #region
                    {
                        InitControls("auto", "letterSpaceUnit", isGeneralString, fieldUnit);
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Part:
                    #region
                    {
                        if (isGeneralString)
                        {
                            if (string.IsNullOrEmpty(fieldUnit.Value))
                            {
                                cbbField.Text = Value;
                                cbbUnit.Text = "";
                                cbbUnit.Enabled = true;
                            }
                            else
                            {
                                cbbField.Text = fieldUnit.Key;
                                list = CssResources.Resources["partUnit"];
                                cbbUnit.Text = list.ValueToText(fieldUnit.Value);
                                cbbUnit.Enabled = true;
                            }
                        }
                        else
                        {
                            cbbField.Text = Value;
                            cbbUnit.Text = "";
                            cbbUnit.Enabled = false;
                        }
                    }
                    break;
                    #endregion
                case CssFieldUnitType.None:
                    #region
                    {
                    }
                    break;
                    #endregion
                default:
                    break;
            }
        }

        private void InitControls(string before, string after,
            bool isGeneralString,KeyValuePair<string,string> fieldUnit)
        {
            CssResourceList list = new CssResourceList();
            if (isGeneralString)
            {
                if (string.IsNullOrEmpty(fieldUnit.Value))
                {
                    cbbField.Text = Value;
                    cbbUnit.Text = "";
                    cbbUnit.Enabled = true;
                }
                else
                {
                    cbbField.Text = fieldUnit.Key;
                    list = CssResources.Resources[after];
                    cbbUnit.Text = list.ValueToText(fieldUnit.Value);
                    cbbUnit.Enabled = true;
                }
            }
            else
            {
                list = CssResources.Resources[before];
                if (list.HasValue(Value))
                {
                    cbbField.Text = list.ValueToText(Value);
                }
                else
                    cbbField.Text = Value;
                cbbUnit.Text = "";
                cbbUnit.Enabled = false;
            }
        }

        private bool CheckValue(string before,string after)
        {
            KeyValuePair<string, string> fieldUnit = new KeyValuePair<string, string>();
            bool b = CssUtility.ParseFieldUnit(_value, out fieldUnit);
            CssResourceList list = new CssResourceList();
            if (b)
            {
                list = CssResources.Resources[after];
                if (list.HasValue(fieldUnit.Value))
                {
                    return true;
                }
                return CssUtility.ShowNotStandard(cbbField.Text);
            }
            else
            {
                double d = 0;
                if (double.TryParse(cbbField.Text, out d))
                {
                    return true;
                }
                if (cbbField.Text == "(值)")
                {
                    CssUtility.ShowReplaceMsg("(值)");
                    return false;
                }
                list = CssResources.Resources[before];
                if (list.HasValue(cbbField.Text))
                {
                    return true;
                }
                return CssUtility.ShowNotStandard(cbbField.Text);
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取当前值是否合法
        /// </summary>
        public bool CheckValue()
        {
            if(string.IsNullOrEmpty(_value))
            {
                return true;
            }
            switch (_fieldUnitType)
            {
                case CssFieldUnitType.LineHeight:
                    #region
                    {
                        return CheckValue("normal", "lineHeightUnit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.FontSize:
                    #region
                    {
                        return CheckValue("font-size", "unit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.LevelPosition:
                    #region
                    {
                        return CheckValue("levelPostion", "unit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.VerticalPosition:
                    #region
                    {
                        return CheckValue("verticalPostion", "unit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.TextIndent:
                    #region
                    {
                        return true;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.WordSpacing:
                    #region
                    {
                        return CheckValue("normal", "wordSpaceUnit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.LetterSpacing:
                    #region
                    {
                        return CheckValue("normal", "letterSpaceUnit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.VerticalAlign:
                    #region
                    {
                        return CheckValue("vertical-align", "verticalUnit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Padding:
                    #region
                    {
                        return CheckValue("value", "unit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.MarginWidthHeightPosition:
                    #region
                    {
                        return CheckValue("auto", "unit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.BorderWidth:
                    #region
                    {
                        return CheckValue("borderWidth", "unit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Clip:
                    #region
                    {
                        return CheckValue("auto", "letterSpaceUnit");
                    }
                    break;
                    #endregion
                case CssFieldUnitType.Part:
                    #region
                    {
                        return true;
                    }
                    break;
                    #endregion
                case CssFieldUnitType.None:
                    #region
                    {
                    }
                    break;
                    #endregion
                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// 为控件设置输入焦点
        /// </summary>
        public void SelectFirst()
        {
            cbbField.Focus();
        }

        #endregion
    }
}
