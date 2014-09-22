using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.Win
{
    public class SelectGroup : Control
    {
        private bool _multiSelect;
        /// <summary>
        /// 是否多选模式
        /// </summary>
        [Description("是否多选模式")]
        public bool MultiSelect
        {
            get { return _multiSelect; }
            set { _multiSelect = value; }
        }

        private int _horizontalCount = 1;
        /// <summary>
        /// 一行显示几个子控件
        /// </summary>
        [Description("一行显示几个子控件")]
        public int HorizontalCount
        {
            get { return _horizontalCount; }
            set
            {
                if (_horizontalCount < 1)
                {
                    throw new ArgumentOutOfRangeException("HorizontalCount", "开发期异常");
                }
                _horizontalCount = value;
            }
        }

        int _controlAddIndex = 0;

        private int _lineHeight = 20;
        /// <summary>
        /// 行高
        /// </summary>
        [Description("行高")]
        public int LineHeight
        {
            get { return _lineHeight; }
            set { _lineHeight = value; }
        }

        private int _vIndent = 5;
        /// <summary>
        /// 垂直缩进距离
        /// </summary>
        [Description("垂直缩进距离")]
        public int VIndent
        {
            get { return _vIndent; }
            set { _vIndent = value; }
        }

        private int _hIndent = 5;
        /// <summary>
        /// 水平缩进距离
        /// </summary>
        [Description("水平缩进距离")]
        public int HIndent
        {
            get { return _hIndent; }
            set { _hIndent = value; }
        }

        private int _selectedItemCount = 0;
        /// <summary>
        /// SelectGroup最多能选择的项数
        /// </summary>
        [Description("多选模式下,最多可选择项数")]
        public int SelectedItemCount
        {
            set { _selectedItemCount = value; }
        }

        #region 数据绑定

        private object _dataSource;

        public object DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        private string _displayMember;
        public string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value; }
        }

        private string _valueMember;
        public string ValueMember
        {
            get { return _valueMember; }
            set { _valueMember = value; }
        }

        public void DataBinding()
        {
            Items.Clear();

            DataView dv = null;
            if (DataSource is DataView)
            {
                dv = (DataView)DataSource;
            }
            else if (DataSource is DataTable)
            {
                dv = ((DataTable)DataSource).DefaultView;
            }
            else if (DataSource is DataSet)
            {
                DataSet ds = (DataSet)DataSource;
                if (ds.Tables.Count > 0)
                {
                    dv = (ds).Tables[0].DefaultView;
                }
            }

            if (dv != null)
            {
                foreach (DataRowView row in dv)
                {
                    string strText = Convert.ToString(row[DisplayMember]);
                    object objValue = row[ValueMember];

                    Items.Add(new SelectGroupItem(objValue, strText));
                }
            }
        }

        #endregion

        public object SelectedValue
        {
            get
            {
                ///多选不应该操作此值，应该用Values
                if (MultiSelect)
                {
                    return null;
                }

                foreach (KeyValuePair<SelectGroupItem, Control> keyvalue in _dicControl)
                {
                    if (((RadioButton)keyvalue.Value).Checked)
                    {
                        return keyvalue.Key.Value;
                    }
                }

                return null;
            }
            set
            {
                ///多选不应该操作此值，应该用Values
                if (MultiSelect)
                {
                    return;
                }

                foreach (KeyValuePair<SelectGroupItem, Control> keyvalue in _dicControl)
                {
                    if (object.Equals(keyvalue.Key.Value, value))
                    {
                        ((RadioButton)keyvalue.Value).Checked = true;
                        return;
                    }
                }
            }
        }

        public string SelectedText
        {
            get
            {
                ///多选不应该操作此值，应该用Values
                if (MultiSelect)
                {
                    return null;
                }

                foreach (KeyValuePair<SelectGroupItem, Control> keyvalue in _dicControl)
                {
                    if (((RadioButton)keyvalue.Value).Checked)
                    {
                        return keyvalue.Key.Text;
                    }
                }

                return null;
            }
        }

        public override string Text
        {
            get
            {
                return SelectedText;
            }
            set
            {
            }
        }

        public object[] SelectedValues
        {
            get
            {
                ///单选不应该操作此值，应该用Value
                if (!MultiSelect)
                {
                    return null;
                }

                List<object> list = new List<object>();
                foreach (KeyValuePair<SelectGroupItem, Control> keyvalue in _dicControl)
                {
                    if (((CheckBox)keyvalue.Value).Checked)
                    {
                        list.Add(keyvalue.Key.Value);
                    }
                }
                return list.ToArray();
            }
            set
            {
                ///单选不应该操作此值，应该用Value
                if (!MultiSelect)
                {
                    return;
                }

                foreach (KeyValuePair<SelectGroupItem, Control> keyvalue in _dicControl)
                {
                    ((CheckBox)keyvalue.Value).Checked = (Array.IndexOf(value, keyvalue.Key.Value) != -1);
                }
            }
        }

        public string[] SelectedStringValues
        {
            get
            {
                ///单选不应该操作此值，应该用Value
                if (!MultiSelect)
                {
                    return null;
                }

                object[] tempValues = SelectedValues;
                string[] arrTemp = new string[tempValues.Length];
                for (int i = 0; i < arrTemp.Length; i++)
                {
                    arrTemp[i] = Convert.ToString(tempValues[i]);
                }
                return arrTemp;
            }
            set
            {
                ///单选不应该操作此值，应该用Value
                if (!MultiSelect)
                {
                    return;
                }

                object[] arrTemp = new object[value.Length];
                for (int i = 0; i < arrTemp.Length; i++)
                {
                    arrTemp[i] = value[i];
                }
                SelectedValues = arrTemp;
            }
        }

        public string[] SelectedTexts
        {
            get
            {
                ///单选不应该操作此值，应该用Value
                if (!MultiSelect)
                {
                    return null;
                }

                List<string> list = new List<string>();
                foreach (KeyValuePair<SelectGroupItem, Control> keyvalue in _dicControl)
                {
                    if (((CheckBox)keyvalue.Value).Checked)
                    {
                        list.Add(keyvalue.Key.Text);
                    }
                }
                return list.ToArray();
            }
        }

        private RichList<SelectGroupItem> _items = new RichList<SelectGroupItem>();
        public RichList<SelectGroupItem> Items
        {
            get
            {
                return _items;
            }
        }

        Dictionary<SelectGroupItem, Control> _dicControl = new Dictionary<SelectGroupItem, Control>();

        public SelectGroup()
        {
            Items.Inserted += Items_Inserted;
            Items.Removed += Items_Removed;
        }

        public void SetChecked(int index, bool isChecked)
        {
            Control con = _dicControl[_items[index]];
            if (con is CheckBox)
            {
                ((CheckBox)con).Checked = isChecked;
            }
            else if (con is RadioButton)
            {
                ((RadioButton)con).Checked = isChecked;
            }
        }

        void Items_Inserted(object sender, EventArgs<SelectGroupItem> e)
        {
            Control control = CreateSubControl(e.Item);
            Debug.Assert(!_dicControl.ContainsKey(e.Item), "当前键值已存在!");
            _dicControl.Add(e.Item, control);

            if (control is CheckBox)
            {
                CheckBox chkbox = (CheckBox)control;
                chkbox.CheckedChanged += ItemCheckedChanged;
            }
            else if (control is RadioButton)
            {
                RadioButton rdobtn = (RadioButton)control;
                rdobtn.CheckedChanged += ItemCheckedChanged;
            }
            ((ButtonBase)control).AutoSize = true;
            control.BringToFront();

            if (_autoSize)
            {
                MarkOwnSize();
            }
            else
            {
                control.Location = new Point(
                    HIndent + ((this.Width - HIndent) / HorizontalCount) * (_controlAddIndex % HorizontalCount),
                    VIndent + LineHeight * (_controlAddIndex / HorizontalCount));
            }

            _controlAddIndex++;
        }

        void Items_Removed(object sender, EventArgs<SelectGroupItem> e)
        {
            _dicControl.Remove(e.Item);
        }

        private bool _autoSize = false;

        public override bool AutoSize
        {
            get { return _autoSize; }
            set
            {
                //这里是需要SelectGroup最后一次在自动对其内部控件进行排版
                _autoSize = value;
                if (_autoSize)
                {
                    MarkOwnSize();
                }
            }
        }

        private int _recItemChecked = 0;
        private int _prevIsChecked = -1;
        private void ItemCheckedChanged(object sender, EventArgs e)
        {
            ///单选的时候由于一次选择会导致触发两次事件,所以特殊处理
            if (!MultiSelect)
            {
                if (_prevIsChecked == -1)
                {
                    OnSelectedValueChanged(EventArgs.Empty);
                    _prevIsChecked = 0;
                }
                if (_prevIsChecked == 0)
                {
                    _prevIsChecked++;
                }
                else
                {
                    OnSelectedValueChanged(EventArgs.Empty);
                    _prevIsChecked = 0;
                }
            }
            ///多选
            else
            {
                //多选状态下,记录当前选中的项的个数
                _recItemChecked = this.SelectedStringValues.Length;
                if (_selectedItemCount != 0)
                {
                    //控件在有限选择的个数下
                    if (_recItemChecked == _selectedItemCount)
                    {
                        //其它项都不不可用
                        foreach (CheckBox item in this.Controls)
                        {
                            if (!item.Checked)
                            {
                                item.Enabled = false;
                            }
                        }
                    }
                    else if (_recItemChecked < _selectedItemCount)
                    {
                        //其它项都不可用
                        foreach (CheckBox item in this.Controls)
                        {
                            item.Enabled = true;
                        }
                    }
                }
                OnSelectedValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// 生成一个控件，会根据MultiSelect属性来决定生成CheckBox还是RadioButton
        /// </summary>
        public Control CreateSubControl(SelectGroupItem selectGroupItem)
        {
            Control control = null;
            if (MultiSelect)
            {
                control = new CheckBox();
            }
            else
            {
                control = new RadioButton();
            }

            control.Text = selectGroupItem.Text;
            this.Controls.Add(control);

            return control;
        }

        public void ReBuild()
        {
            SelectGroupItem[] tempArr = Items.ToArray();
            _controlAddIndex = 0;

            Controls.Clear();
            Items.Clear();
            Items.AddRange(tempArr);
        }

        public event EventHandler SelectedValueChanged;
        protected virtual void OnSelectedValueChanged(EventArgs e)
        {
            if (SelectedValueChanged != null)
            {
                SelectedValueChanged(this, e);
            }
        }
        /// <summary>
        /// 生成自动布局的控件状态,以及控件的宽度与高度 lukan, 2008年1月30日1时50分
        /// </summary>
        private void MarkOwnSize()// 算法仅保证了完成, 应该再做优化
        {
            #region

            //List<List<ButtonBase>> ctrTabel = new List<List<ButtonBase>>();
            //for (int i = 0; i < HorizontalCount; i++)
            //{
            //    List<ButtonBase> list = new List<ButtonBase>();
            //    ctrTabel.Add(list);
            //}

            //for (int i = this.Controls.Count - 1; i >= 0; i--)
            //{
            //    int m = i % HorizontalCount;

            //    ctrTabel[HorizontalCount - 1 - m].Add((ButtonBase)this.Controls[i]);
            //}

            //int[] maxWidths = new int[HorizontalCount];
            //int maxHeight = 0;
            //for (int i = 0; i < ctrTabel.Count; i++)
            //{
            //    int maxWidth = 0;
            //    foreach (ButtonBase btnBase in ctrTabel[i])
            //    {
            //        if (btnBase.Width > maxWidth)
            //        {
            //            maxWidth = btnBase.Width;
            //        }
            //    }
            //    maxWidths[i] = maxWidth;
            //}
            //for (int i = 0; i < ctrTabel.Count; i++)
            //{
            //    int xWidth = 0;
            //    if (i != 0)
            //    {
            //        xWidth += maxWidths[i - 1];
            //    }
            //    int yHeight = 0;
            //    for (int j = 0; j < ctrTabel[i].Count; j++)
            //    {
            //        ButtonBase btnBase = ctrTabel[i][j];
            //        btnBase.Location = new Point(
            //            HIndent * (i + 1) + xWidth,
            //            VIndent + LineHeight * j);
            //        yHeight = VIndent + LineHeight * (j + 1);
            //    }
            //    if (yHeight > maxHeight)
            //    {
            //        maxHeight = yHeight;
            //    }
            //}
            //int cWidth = 0;
            //foreach (int w in maxWidths)
            //{
            //    cWidth = cWidth + w + HIndent;
            //}
            //this.Size = new Size(cWidth, maxHeight);

            #endregion

            int num = _dicControl.Count;
            int rows = num / _horizontalCount + (((num % _horizontalCount) == 0) ? 0 : 1);

            ///selectGroup的宽度(在定制特性设置的宽度将是没用的)
            this.Width = HIndent + _horizontalCount * GetMaxValue() + HIndent;
            this.Height = VIndent + rows * LineHeight + VIndent;

            int _addIndex = 0;
            foreach (Control control in _dicControl.Values)
            {
                control.Location = new Point(
                    HIndent + ((this.Width - HIndent) / HorizontalCount) * (_addIndex % HorizontalCount),
                    VIndent + LineHeight * (_addIndex / HorizontalCount));
                _addIndex++;
            }
        }

        private int GetMaxValue()
        {
            int controlWidth = 0;
            foreach (Control control in _dicControl.Values)
            {
                int width = control.Width;
                if (width >= controlWidth)
                {
                    controlWidth = width;
                }
            }
            return controlWidth;
        }

        public void Clear()
        {
            Items.Clear();
            ReBuild();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton || control is CheckBox)
                {
                    control.Enabled = this.Enabled;
                    control.Invalidate();
                }
            }
        }

    }

    public class SelectGroupItem
    {
        private object _value;
        public object Value
        {
            get { return _value; }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
        }

        private object _tag;

        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        /// <summary>
        /// 控件要显示的项
        /// </summary>
        /// <param name="value">同ComboBox的 value</param>
        /// <param name="text">显示文本</param>
        public SelectGroupItem(object value, string text)
        {
            _value = value;
            _text = text;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            SelectGroupItem compareItem = (SelectGroupItem)obj;

            if (!object.Equals(Value, compareItem.Value))
            {
                return false;
            }

            if (!object.Equals(Text, compareItem.Text))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }



    }
}
