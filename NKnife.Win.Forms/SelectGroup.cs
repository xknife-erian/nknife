using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NKnife.Win.Forms
{
    public class SelectGroup : Control
    {
        private readonly Dictionary<SelectGroupItem, Control> _ControlMap = new Dictionary<SelectGroupItem, Control>();
        private readonly ObservableCollection<SelectGroupItem> _Items = new ObservableCollection<SelectGroupItem>();
        private bool _AutoSize;

        private int _ControlAddIndex;
        private int _HIndent = 5;
        private int _HorizontalCount = 1;

        private int _LineHeight = 20;
        private int _PrevIsChecked = -1;
        private int _RecItemChecked;
        private int _SelectedItemCount;

        private int _VIndent = 5;

        public SelectGroup()
        {
            Items.CollectionChanged += ItemsCollectionChanged;
        }

        /// <summary>
        /// �Ƿ��ѡģʽ
        /// </summary>
        [Description("�Ƿ��ѡģʽ")]
        public bool MultiSelect { get; set; }

        /// <summary>
        /// һ����ʾ�����ӿؼ�
        /// </summary>
        [Description("һ����ʾ�����ӿؼ�")]
        public int HorizontalCount
        {
            get { return _HorizontalCount; }
            set
            {
                if (_HorizontalCount < 1)
                {
                    throw new ArgumentOutOfRangeException("HorizontalCount", "�������쳣");
                }
                _HorizontalCount = value;
            }
        }

        /// <summary>
        /// �и�
        /// </summary>
        [Description("�и�")]
        public int LineHeight
        {
            get { return _LineHeight; }
            set { _LineHeight = value; }
        }

        /// <summary>
        /// ��ֱ��������
        /// </summary>
        [Description("��ֱ��������")]
        public int VIndent
        {
            get { return _VIndent; }
            set { _VIndent = value; }
        }

        /// <summary>
        /// ˮƽ��������
        /// </summary>
        [Description("ˮƽ��������")]
        public int HIndent
        {
            get { return _HIndent; }
            set { _HIndent = value; }
        }

        /// <summary>
        /// SelectGroup�����ѡ�������
        /// </summary>
        [Description("��ѡģʽ��,����ѡ������")]
        public int SelectedItemCount
        {
            set { _SelectedItemCount = value; }
        }

        public object SelectedValue
        {
            get
            {
                //��ѡ��Ӧ�ò�����ֵ��Ӧ����Values
                if (MultiSelect)
                    return null;
                return (from keyvalue in _ControlMap where ((RadioButton) keyvalue.Value).Checked select keyvalue.Key.Value).FirstOrDefault();
            }
            set
            {
                //��ѡ��Ӧ�ò�����ֵ��Ӧ����Values
                if (MultiSelect)
                    return;

                foreach (var keyvalue in _ControlMap)
                {
                    if (Equals(keyvalue.Key.Value, value))
                    {
                        ((RadioButton) keyvalue.Value).Checked = true;
                        return;
                    }
                }
            }
        }

        public string SelectedText
        {
            get
            {
                //��ѡ��Ӧ�ò�����ֵ��Ӧ����Values
                if (MultiSelect)
                    return null;
                return (from keyvalue in _ControlMap where ((RadioButton) keyvalue.Value).Checked select keyvalue.Key.Text).FirstOrDefault();
            }
        }

        public override string Text
        {
            get { return SelectedText; }
        }

        public object[] SelectedValues
        {
            get
            {
                //��ѡ��Ӧ�ò�����ֵ��Ӧ����Value
                if (!MultiSelect)
                    return null;
                return (from keyvalue in _ControlMap where ((CheckBox) keyvalue.Value).Checked select keyvalue.Key.Value).ToArray();
            }
            set
            {
                //��ѡ��Ӧ�ò�����ֵ��Ӧ����Value
                if (!MultiSelect)
                    return;
                foreach (var keyvalue in _ControlMap)
                {
                    ((CheckBox) keyvalue.Value).Checked = (Array.IndexOf(value, keyvalue.Key.Value) != -1);
                }
            }
        }

        public string[] SelectedStringValues
        {
            get
            {
                //��ѡ��Ӧ�ò�����ֵ��Ӧ����Value
                if (!MultiSelect)
                {
                    return null;
                }

                object[] tempValues = SelectedValues;
                var arrTemp = new string[tempValues.Length];
                for (int i = 0; i < arrTemp.Length; i++)
                {
                    arrTemp[i] = Convert.ToString(tempValues[i]);
                }
                return arrTemp;
            }
            set
            {
                //��ѡ��Ӧ�ò�����ֵ��Ӧ����Value
                if (!MultiSelect)
                {
                    return;
                }

                var arrTemp = new object[value.Length];
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
                //��ѡ��Ӧ�ò�����ֵ��Ӧ����Value
                if (!MultiSelect)
                {
                    return null;
                }

                return (from keyvalue in _ControlMap where ((CheckBox) keyvalue.Value).Checked select keyvalue.Key.Text).ToArray();
            }
        }

        public ObservableCollection<SelectGroupItem> Items
        {
            get { return _Items; }
        }

        public override bool AutoSize
        {
            get { return _AutoSize; }
            set
            {
                //��������ҪSelectGroup���һ�����Զ������ڲ��ؼ������Ű�
                _AutoSize = value;
                if (_AutoSize)
                {
                    MarkOwnSize();
                }
            }
        }

        #region ���ݰ�

        public object DataSource { get; set; }

        public string DisplayMember { get; set; }

        public string ValueMember { get; set; }

        public void DataBinding()
        {
            Items.Clear();

            DataView dv = null;
            if (DataSource is DataView)
            {
                dv = (DataView) DataSource;
            }
            else if (DataSource is DataTable)
            {
                dv = ((DataTable) DataSource).DefaultView;
            }
            else if (DataSource is DataSet)
            {
                var ds = (DataSet) DataSource;
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

        public void SetChecked(int index, bool isChecked)
        {
            Control con = _ControlMap[_Items[index]];
            if (con is CheckBox)
            {
                ((CheckBox) con).Checked = isChecked;
            }
            else if (con is RadioButton)
            {
                ((RadioButton) con).Checked = isChecked;
            }
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Control control = CreateSubControl((SelectGroupItem) e.NewItems);
            Debug.Assert(!_ControlMap.ContainsKey((SelectGroupItem) e.NewItems), "��ǰ��ֵ�Ѵ���!");
            _ControlMap.Add((SelectGroupItem) e.NewItems, control);

            if (control is CheckBox)
            {
                var chkbox = (CheckBox) control;
                chkbox.CheckedChanged += ItemCheckedChanged;
            }
            else if (control is RadioButton)
            {
                var rdobtn = (RadioButton) control;
                rdobtn.CheckedChanged += ItemCheckedChanged;
            }
            (control).AutoSize = true;
            control.BringToFront();

            if (_AutoSize)
            {
                MarkOwnSize();
            }
            else
            {
                control.Location = new Point(
                    HIndent + ((Width - HIndent)/HorizontalCount)*(_ControlAddIndex%HorizontalCount),
                    VIndent + LineHeight*(_ControlAddIndex/HorizontalCount));
            }

            _ControlAddIndex++;
        }

        private void ItemCheckedChanged(object sender, EventArgs e)
        {
            //��ѡ��ʱ������һ��ѡ��ᵼ�´��������¼�,�������⴦��
            if (!MultiSelect)
            {
                if (_PrevIsChecked == -1)
                {
                    OnSelectedValueChanged(EventArgs.Empty);
                    _PrevIsChecked = 0;
                }
                if (_PrevIsChecked == 0)
                {
                    _PrevIsChecked++;
                }
                else
                {
                    OnSelectedValueChanged(EventArgs.Empty);
                    _PrevIsChecked = 0;
                }
            }
            else //��ѡ
            {
                //��ѡ״̬��,��¼��ǰѡ�е���ĸ���
                _RecItemChecked = SelectedStringValues.Length;
                if (_SelectedItemCount != 0)
                {
                    //�ؼ�������ѡ��ĸ�����
                    if (_RecItemChecked == _SelectedItemCount)
                    {
                        //�������������
                        foreach (CheckBox item in Controls)
                        {
                            if (!item.Checked)
                            {
                                item.Enabled = false;
                            }
                        }
                    }
                    else if (_RecItemChecked < _SelectedItemCount)
                    {
                        //�����������
                        foreach (CheckBox item in Controls)
                        {
                            item.Enabled = true;
                        }
                    }
                }
                OnSelectedValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// ����һ���ؼ��������MultiSelect��������������CheckBox����RadioButton
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
            Controls.Add(control);

            return control;
        }

        public void ReBuild()
        {
            SelectGroupItem[] tempArr = Items.ToArray();
            _ControlAddIndex = 0;

            Controls.Clear();
            Items.Clear();
            foreach (SelectGroupItem selectGroupItem in tempArr)
            {
                Items.Add(selectGroupItem);
            }
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
        /// �����Զ����ֵĿؼ�״̬,�Լ��ؼ��Ŀ����߶� lukan, 2008��1��30��1ʱ50��
        /// </summary>
        private void MarkOwnSize() // �㷨����֤�����, Ӧ�������Ż�
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

            int num = _ControlMap.Count;
            int rows = num/_HorizontalCount + (((num%_HorizontalCount) == 0) ? 0 : 1);

            //selectGroup�Ŀ��(�ڶ����������õĿ�Ƚ���û�õ�)
            Width = HIndent + _HorizontalCount*GetMaxValue() + HIndent;
            Height = VIndent + rows*LineHeight + VIndent;

            int addIndex = 0;
            foreach (Control control in _ControlMap.Values)
            {
                control.Location = new Point(
                    HIndent + ((Width - HIndent)/HorizontalCount)*(addIndex%HorizontalCount),
                    VIndent + LineHeight*(addIndex/HorizontalCount));
                addIndex++;
            }
        }

        private int GetMaxValue()
        {
            int controlWidth = 0;
            foreach (Control control in _ControlMap.Values)
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
            foreach (Control control in Controls)
            {
                if (control is RadioButton || control is CheckBox)
                {
                    control.Enabled = Enabled;
                    control.Invalidate();
                }
            }
        }
    }

    public class SelectGroupItem
    {
        private readonly string _Text;
        private readonly object _Value;

        /// <summary>
        /// �ؼ�Ҫ��ʾ����
        /// </summary>
        /// <param name="value">ͬComboBox�� value</param>
        /// <param name="text">��ʾ�ı�</param>
        public SelectGroupItem(object value, string text)
        {
            _Value = value;
            _Text = text;
        }

        public object Value
        {
            get { return _Value; }
        }

        public string Text
        {
            get { return _Text; }
        }

        public object Tag { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var compareItem = (SelectGroupItem) obj;

            if (!Equals(Value, compareItem.Value))
            {
                return false;
            }

            if (!Equals(Text, compareItem.Text))
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