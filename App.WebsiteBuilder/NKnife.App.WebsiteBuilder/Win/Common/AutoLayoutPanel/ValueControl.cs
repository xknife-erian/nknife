using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.Data;
using Jeelu.Win;
using System.IO;
using System.Xml;
namespace Jeelu.Win
{
    public partial class ValueControl : Control
    {
        #region ��̬��������

        static int txtWidth = 80; //��70�ĳ�80,�������ñ�ǩ�ı���ʾ�ĸ���Ϊ6
        static int txtHeight = 13;
        static int inpHeignt = 21;

        static int offsetX = 2;
        static int offsetY = 2;
        static int offsetLabelX = -2;
        static int offsetLabelY = 4;

        static int x1 = offsetX + 5; //�����������ؼ���X����
        static int x2 = x1 + txtWidth + offsetX;
        static int y1 = offsetY;
        static int y2 = offsetY + txtHeight + offsetY;

        #endregion

        #region ���Ա��������

        protected Size _txtSize = new Size(txtWidth, txtHeight);
        public Control _mainControl;
        public Control MainControl
        {
            get { return _mainControl; }
            set { _mainControl = value; }
        }

        /// <summary>
        /// ���ؼ�����Ӧ������(�������ԾͶ����ڴ�������)
        /// </summary>
        public MemberInfo MemberInfo { get; protected set; }
        /// <summary>
        /// ���ؼ������ݵĶ�������
        /// </summary>
        public AutoLayoutPanelAttribute AutoAttribute { get; protected set; }
        protected object[] _objects;
        protected object _historyValue;
        /// <summary>
        /// ��ǰ�ؼ���
        /// </summary>
        //�õ����Ƶĵ�ǰ�ؼ��Ŀؼ����� by lisuye on 2008��5��28��
        public MainControlType MainControlType { get { return AutoAttribute.MainControlType; } }

        /// <summary>
        /// ��GroupBoxEx�ؼ�
        /// </summary>
        public GroupBoxEx ParentGroup { get; private set; }

        /// <summary>
        /// ������AutoPanel
        /// </summary>
        public AutoLayoutPanel OwnerAutoPanel { get { return ParentGroup.OwnerAutoPanel; } }
        /// <summary>
        /// �Ƿ�ֵ���޸ġ�ֻ����RealTimeSaveΪFalseʱ��������
        /// </summary>
        public bool IsModified { get; protected set; }

        private bool _different = false;
        /// <summary>
        /// �Ƿ��ǲ�һ����ֵ
        /// </summary>
        public bool Different
        {
            get
            {
                return _different;
            }
        }

        #endregion

        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        protected internal ValueControl(AutoAttributeData data, GroupBoxEx parentGroup)
        {

            this.ParentGroup = parentGroup;
            this.TabStop = false;
            this.MemberInfo = data.Property;
            this.AutoAttribute = data.Attribute;

            /// �ؼ��Ű�(��Ҫ�����Label��Image��־)
            this.LayoutOwnControl();

            /// ����ؼ�ʵʱ������¼�
            this.EventDefine(AutoAttribute.MainControlType);
        } 

        #endregion

        #region �ؼ���ֵ���

        /// <summary>
        /// ����ؼ�ʵʱ������¼�
        /// </summary>
        public virtual void EventDefine(MainControlType type)
        {
            ///����ֵ����¼�����
            EventHandler eventHandler = new EventHandler(
                delegate
                {
                    //�����Ҫʵʱ���������б��� by lisuye on 2008��5��28��
                    if (OwnerAutoPanel.RealTimeSave)
                    {
                        if (AutoAttribute.IsReadOnly ||
                            (MemberInfo.MemberType == MemberTypes.Property && !((PropertyInfo)MemberInfo).CanWrite))
                        {
                        }
                        else
                        {
                            SetValueFromControl();
                        }
                    }
                      //�������Ҫʵʱ��������IsModifiedΪtrue���������Ʊ���İ�ť by lisuye on 2008��5��28��
                    else
                    {
                        IsModified = true;         
                    }
                });
            //���ݿؼ������ͣ��ֱ����ͬ���͵Ŀؼ����¼� by lisuye on 2008��5��28��
            switch (type)
            {
                case MainControlType.SimpleCheckBox:
                    {
                        ((SimpleCheckBox)this._mainControl).CheckedChanged += eventHandler;
                        break;
                    }
                case MainControlType.TextBox:
                case MainControlType.NumericUpDown:
                    {
                        this._mainControl.TextChanged += eventHandler;
                        break;
                    }
                case MainControlType.SelectGroup:
                    {
                        ((SelectGroup)this._mainControl).SelectedValueChanged += eventHandler;
                        break;
                    }
                case MainControlType.ListBox:
                case MainControlType.ComboBox:
                    {
                        ((ListControl)this._mainControl).SelectedValueChanged += eventHandler;
                        break;
                    }
                case MainControlType.ValidateTextBox:
                    {
                        ((ValidateTextBox)this._mainControl).Changed += eventHandler;
                        break;
                    }
                case MainControlType.Button:
                    break;
                case MainControlType.ColorGeneralButton:
                    {
                        ((ColorGeneralButton)this._mainControl).MyColorChanged += eventHandler;
                        break;
                    }
                case MainControlType.FileSelecterControl:
                    {
                        ((FileSelecterControl)this._mainControl).FileSelected += eventHandler;
                        break;
                    }
                case MainControlType.ComboBoxGroupControl:
                    {
                        ((ComboBoxGroupControl)this._mainControl).SelectedValueChanged += eventHandler;
                        break;
                    }
                case MainControlType.DepartmentControl:
                    {
                        ((DepartmentControl)this._mainControl).ValueChanged += eventHandler;
                        break;
                    }
                case MainControlType.CheckBoxExControl:
                    {
                        ((CheckBoxExControl)this._mainControl).CheckChanged += eventHandler;
                        break;
                    }
                case MainControlType.DateTimePicker:
                    {
                        ((DateTimePicker)this._mainControl).ValueChanged += eventHandler;
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Ϊ�ؼ�ȡֵ�����
        /// ����Ƕ�����󣬵��Ƕ�������ֵ��һ���Ļ��������ؿ�ֵ
        /// </summary>
        //���ݷ���õ��ؼ���Ӧ��ֵ�������ֵ by lisuye on 2008��5��28��
        public virtual void FillValue(object[] objs)
        {
            this._objects = objs;
            _different = false;

            if (this.MemberInfo.MemberType != MemberTypes.Property)
            {
                return;
            }
            PropertyInfo propertyInfo = (PropertyInfo)MemberInfo;

            object valueFirst = propertyInfo.GetValue(objs[0], null);

            //�Ƚ϶��ֵ�Ƿ����
            if (objs.Length > 1)
            {
                foreach (object obj in objs)
                {
                    object valueEvery = propertyInfo.GetValue(obj, null);

                    ///��һ����valueFirstzhiwei��Ϊ��,������ʾ
                    if (!object.Equals(valueFirst, valueEvery))
                    {
                        _different = true;
                        valueFirst = null;
                        break;
                    }
                }
            }

            if (Different)
            {
                //lukan,2008��2��26��11ʱ45��,����������ֵ��һ��ʱ����Nullֵʱδ�����û�UI
                this._mainControl.BackColor = SystemColors.Info;
                this._historyValue = null;
                this.Value = null;
            }
            else
            {
                if (this._mainControl is TextBox
                    || this._mainControl is ComboBox
                    || this._mainControl is ListBox
                    || this._mainControl is NumericUpDown)
                {
                    this._mainControl.BackColor = SystemColors.Window;
                }
                else
                {
                    this._mainControl.BackColor = SystemColors.Control;
                }
                this.Value = valueFirst;
                this._historyValue = valueFirst;
                
            }
        }

        public virtual void FillValue()
        {
            if (this.MemberInfo.MemberType != MemberTypes.Property)
            {
                return;
            }
            PropertyInfo propertyInfo = (PropertyInfo)MemberInfo;

            object obj;
            obj = propertyInfo.GetValue(null, null);
            
            this.Value = obj;
            this._historyValue = obj;
        }
        //����ֵ �����ñ����״̬ by lisuye on 2008��5��28��
        public virtual void Save()
        {
            SetValueFromControl();

            IsModified = false;
        }

        /// <summary>
        /// ��ȡ�����ÿؼ���ֵ��GroupBoxEx����, lukan
        /// </summary>
       //�ӿؼ��ϻ�ȡ�����ö�Ӧ��ֵ by lisuye on 2008��5��28��
        public virtual object Value
        {
            get
            {
                switch (MainControlType)
                {
                    case MainControlType.NumericUpDown:
                        return ((NumericUpDown)_mainControl).Value.ToString();
                    case MainControlType.ValidateTextBox:
                    case MainControlType.TextBox:
                        return _mainControl.Text;
                    case MainControlType.DateTimePicker:
                        return ((DateTimePicker)_mainControl).Value;
                    case MainControlType.ListBox:
                    case MainControlType.ComboBox:
                        return ((ListControl)_mainControl).SelectedValue;
                    case MainControlType.ColorGeneralButton:
                        return ((ColorGeneralButton)_mainControl).MyColor;
                    case MainControlType.FileSelecterControl:
                        break;
                    case MainControlType.SelectGroup:
                        if (AutoAttribute.SelectGroupMultiModel)
                        {
                            string[] values = ((SelectGroup)_mainControl).SelectedStringValues;
                            StringBuilder sb = new StringBuilder();
                            foreach (string value in values)
                            {
                                sb.Append(value).Append(":|:");
                            }
                            return sb.ToString();
                        }
                        else
                            return ((SelectGroup)_mainControl).SelectedValue;
                    case MainControlType.SimpleCheckBox:
                        return ((SimpleCheckBox)_mainControl).Value;
                    case MainControlType.DepartmentControl:
                        return ((DepartmentControl)_mainControl).Value;
                    case MainControlType.ColorSelectorButton:
                        return ((ColorSelectorButton)_mainControl).Value;
                    case MainControlType.FontComboBox:
                        return ((FontComboBox)_mainControl).SelectedValue;
                    case MainControlType.ComboBoxGroupControl:
                        return ((ComboBoxGroupControl)_mainControl).SelectedValues;
                    case MainControlType.CheckBoxExControl:
                        return ((CheckBoxExControl)_mainControl).Value;
                    default:
                        break;
                }
                return _mainControl.Text;
            }
            set
            {
                switch (MainControlType)
                {
                    case MainControlType.NumericUpDown:
                    case MainControlType.ValidateTextBox:
                    case MainControlType.TextBox:
                        _mainControl.Text = Convert.ToString(value);
                        break;
                    case MainControlType.DateTimePicker:
                        {
                            DateTimePicker picker = (DateTimePicker)_mainControl;
                            picker.Value = (DateTime)value;
                            break;
                        }
                    case MainControlType.ListBox:
                    case MainControlType.ComboBox:
                        #region
                    
                        //���ݲ�ͬ��form��������combox�ĳ�ʼֵ by lisuye on 2008��5��28��
                        ListControl tempListControl = ((ListControl)_mainControl);
                        tempListControl.SelectedValue = value;
                        Form form = tempListControl.FindForm();
                        // ��formload��ʱ��Կռ���еĸ�ֵ by lisuye on2008��5��28��
                        if (form != null)
                        {
                            form.Load += delegate
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(value)))
                                {
                                    tempListControl.SelectedIndex = 0;
                                }
                                else
                                {
                                    tempListControl.SelectedValue = value;
                                }
                            };
                        }
                        else
                        {
                            // ��formlayout��ʱ��Կռ���еĸ�ֵ by lisuye on2008��5��28��
                            LayoutEventHandler eventHandler = delegate
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(value)))
                                {
                                    if (tempListControl.SelectedIndex == -1)
                                        tempListControl.SelectedIndex = -1;
                                    else
                                        tempListControl.SelectedIndex = 0;
                                }
                                else
                                {
                                    tempListControl.SelectedValue = value;
                                }
                                return;
                            };
                            // �ڲ������¼�����������µ�ʱ��Կռ���еĸ�ֵ by lisuye on2008��5��28��
                            EventHandler eventHandler2 = delegate
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(value)))
                                {
                                    tempListControl.SelectedIndex = 0;
                                }
                                else
                                {
                                    tempListControl.SelectedValue = value;
                                }
                            };
                            this.Layout -= eventHandler;
                            this.Layout += eventHandler;
                            tempListControl.DataSourceChanged -= eventHandler2;
                            tempListControl.DataSourceChanged += eventHandler2;
                        }
                        
                        #endregion
                        break;
                    case MainControlType.Button:
                        break;
                    case MainControlType.ColorGeneralButton:
                        {
                            string str = ColorTranslator.ToHtml((Color)value);
                            _mainControl.Text = str;
                            break;
                        }
                    case MainControlType.FileSelecterControl:
                        {
                            this._mainControl.Text = ReplacePath((string)value);
                            break;
                        }
                    case MainControlType.SelectGroup:
                        {
                            if (AutoAttribute.SelectGroupMultiModel)
                            {
                                //string str = value.ToString();
                                //string[] strArr = (str == null ? new string[0] : str.Split(new[] { ":|:" }, StringSplitOptions.RemoveEmptyEntries));
                                string[] strArr = (string[])value;
                                ((SelectGroup)_mainControl).SelectedStringValues = strArr;
                            }
                            else
                            {
                                ((SelectGroup)_mainControl).SelectedValue = value;

                            }
                            break;
                        }
                    case MainControlType.SimpleCheckBox:
                        if (value == null)
                        {
                            ((SimpleCheckBox)_mainControl).Value = false;
                        }
                        else
                        {
                            ((SimpleCheckBox)_mainControl).Value = (bool)value;
                        }
                        break;
                    case MainControlType.DepartmentControl:
                        ((DepartmentControl)_mainControl).Value = (DepartmentData[])value;
                        break;
                    case MainControlType.ColorSelectorButton:
                        ((ColorSelectorButton)this._mainControl).Value = (Color)value;
                        break;
                    case MainControlType.FontComboBox:
                        ((FontComboBox)_mainControl).SelectedValue = value;
                        break;
                    case MainControlType.ComboBoxGroupControl:
                        ((ComboBoxGroupControl)_mainControl).SelectedValues = (string[])value;
                        break;
                    case MainControlType.CheckBoxExControl:
                        if (value == null)
                        {
                            ((CheckBoxExControl)_mainControl).Value = false;
                        }
                        else
                        {
                            ((CheckBoxExControl)_mainControl).Value = (bool)value;
                        }
                        break;
                    default:
                        break;
                }
                _historyValue = value;
            }
        }

        /// <summary>
        /// �滻SimplusD��վ·��
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        //Ϊ�ļ��ϴ��еĿؼ�����·�� by lisuye on 2008��5��28��
        private string ReplacePath(string path)
        {
            string text = "%:mydocument%";
            if (path.StartsWith(text))
            {
                string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                path = path.Replace(text, documentPath);
            }
            return path;
        }

        /// <summary>
        /// �����ַ������ж�Form�����Size����ֵ���Լ��ں���λ�ò����˻��з���String 
        /// </summary>
        static internal KeyValuePair<string, Size> GetSize(string str, int MaxWidth, Graphics g, Font font)
        {
            KeyValuePair<string, Size> k;
            SizeF strSize = g.MeasureString(str, font);
            StringBuilder sb = new StringBuilder();
            int lineNum = 0;

            char[] c = str.ToCharArray();
            List<string> list = new List<string>();
            foreach (char myChar in c)
            {
                sb.Append(myChar);
                if (g.MeasureString(sb.ToString(), font).Width >= MaxWidth)
                {
                    sb.Append("\r\n");
                    list.Add(sb.ToString());
                    sb = new StringBuilder();
                    lineNum++;
                }
            }
            list.Add(sb.ToString());
            sb = new StringBuilder();
            foreach (string txt in list)
            {
                sb.Append(txt);///�����ں���λ�ò����˻��з���String
            }

            int w;
            int h;
            if (strSize.Width > MaxWidth)
            {
                w = MaxWidth;
                h = (int)strSize.Height * (lineNum + 1);
            }
            else
            {
                w = (int)strSize.Width;
                h = (int)strSize.Height;
            }
            Size mySize = new Size(w + 10, h);
            k = new KeyValuePair<string, Size>(sb.ToString(), mySize);
            return k;
        }

        /// <summary>
        /// ��ȡ�ؼ���ֵ�Ƿ����ı�
        /// </summary>
        //����ʷֵ���бȽϿ��Ƿ�ؼ���ֵ�����˸ı� by lisuye on2008��5��28��
        private string NowControlValue { get;set; }
        public virtual bool IsModifiedFromValue
        {
            get
            {
                if (_historyValue == null)
                {
                    if (!(this._mainControl is SimpleCheckBox))
                    {
                        return this._mainControl.Text != string.Empty;
                    }
                    else
                    {
                        return false;
                    }
                }
                switch (MainControlType)
                {
                    case MainControlType.NumericUpDown:
                    case MainControlType.TextBox:
                    case MainControlType.ValidateTextBox:
                        NowControlValue = this._mainControl.Text;
                        return NowControlValue != _historyValue.ToString();
                    case MainControlType.DateTimePicker:
                        break;
                    case MainControlType.ListBox:
                        break;
                    case MainControlType.ComboBox:
                        if (((ComboBox)_mainControl).SelectedValue != null)
                        {
                            NowControlValue = ((ComboBox)_mainControl).SelectedValue.ToString();
                        }
                        return NowControlValue != _historyValue.ToString();
                    case MainControlType.ColorGeneralButton:
                        break;
                    case MainControlType.FileSelecterControl:
                        break;
                    case MainControlType.SelectGroup:
                        break;
                    // return ((SelectGroup)_mainControl).SelectedStringValues != (string[])_historyValue;
                    case MainControlType.SimpleCheckBox:
                        return ((SimpleCheckBox)_mainControl).Value != (bool)_historyValue;
                    case MainControlType.DepartmentControl:
                        {
                            //DepartmentData[] historyVal =  (DepartmentData[])_historyValue;
                            //return !Utility.IsAllEquals(historyVal, ((DepartmentControl)_mainControl).Value);
                            break;
                        }
                    case MainControlType.ComboBoxGroupControl:
                        {
                            object[] objs = ((ComboBoxGroupControl)_mainControl).SelectedValues;
                            return !Utility.IsAllEquals(objs, (object[])_historyValue);
                        }
                    case MainControlType.CheckBoxExControl:
                        return ((CheckBoxExControl)_mainControl).Value != (bool)_historyValue;
                    default:
                        break;
                }
                return true; ///������Ҫ�ı�
            }
        }

        /// <summary>
        /// �������뿪�ؼ��r���������Ե�ֵ
        /// </summary>
        protected virtual void SetValueFromControl()
        {
            if (this.IsModifiedFromValue)
            {
                if (this._objects != null)
                {
                    foreach (object obj in this._objects)
                    {
                        SetValueFromControlSubMethod(obj);
                        OwnerAutoPanel.OnSaved(new ValueSaveEventArgs(this.Value,obj));
                    }
                }
                else
                {
                    SetValueFromControlSubMethod(null);
                    OwnerAutoPanel.OnSaved(new ValueSaveEventArgs(this.Value, null));
                }
            }
        }

        //���ؼ��ϵ�ֵ���浽�����ļ��� by lisuye on 2008��5��28��
        protected virtual void SetValueFromControlSubMethod(object obj)
        {
            if (this.MemberInfo.MemberType != MemberTypes.Property)
            {
                return;
            }
            PropertyInfo propertyInfo = (PropertyInfo)MemberInfo;

            if (propertyInfo.PropertyType.BaseType == typeof(System.Enum))
            {
                string value = (string)this.Value;
                if (!string.IsNullOrEmpty(value))
                {
                    Enum returnEnum = (Enum)Enum.Parse(propertyInfo.PropertyType, (string)this.Value);
                    propertyInfo.SetValue(obj, returnEnum, null);
                }
            }
            else
            {
                switch (propertyInfo.PropertyType.FullName)
                {
                    case "System.String":
                    case "System.Boolean":
                    case "System.DateTime":
                    case "System.Drawing.Color":
                    case "System.Object[]":
                    case "Jeelu.DepartmentData[]":
                        {
                            propertyInfo.SetValue(obj, this.Value, null);
                            break;
                        }
                    case "System.Int32":
                        {
                            propertyInfo.SetValue(obj, Utility.Convert.StringToInt((string)this.Value), null);
                            break;
                        }
                    case "System.Single":
                        {
                            propertyInfo.SetValue(obj, Utility.Convert.StringToFloat((string)this.Value), null);
                            break;
                        }


                    case "Jeelu.ProjectPart[]":
                        {
                            propertyInfo.SetValue(obj, (ProjectPart[])this.Value, null);
                            break;
                        }
                    case "System.String[]":
                        {
                            if (MainControlType == MainControlType.SelectGroup)
                            {
                                propertyInfo.SetValue(obj, this.Value.ToString().Split(new[] { ":|:" }, StringSplitOptions.RemoveEmptyEntries), null);
                            }
                            if (MainControlType == MainControlType.ComboBoxGroupControl)
                            {
                                propertyInfo.SetValue(obj, this.Value, null);
                            }
                            break;
                        }
                    default:
                        Debug.Fail("design time: Type is not process. " + propertyInfo.PropertyType.FullName);
                        break;
                }
            }
        }

        #endregion

        #region �Ű����

        /* �Ű湲��6������
         * 1. ���ؼ��ϲ���Label��ʾ����
         * 2. ���ؼ�����Label��ʾ���֣���������ͼ���־
         * 3. ���ؼ������в���ֵ����Ч����
         * 4. ���ؼ��Ҳ��Label��ʾ����
         * 5. ���ؼ��²���Label��ʾ����
         * 6. ���ؼ��²���Label�·��İ����ĵ�
        */

        /// <summary>
        /// ���ڲ��ؼ������Ű�
        /// </summary>
        protected virtual void LayoutOwnControl()
        {

            int x = x1;
            int y = y1;

            #region ��6���ؼ������Ű�

            bool flagTop = false;
            Dictionary<LabelPlace, Label> labelDic = this.BuildLabels();
            this._mainControl = this.BuildMainControl(AutoAttribute.MainControlType);
            this._mainControl.TabIndex = 10;

            Label label = null;
            //�ؼ���label������ by lisuye on 2008��5��28��
            #region �Ƿ���ͷ��Label
            if (labelDic.TryGetValue(LabelPlace.Top, out label))
            {
                if (labelDic.ContainsKey(LabelPlace.Left))
                {
                    label.Location = new Point(x2, y);
                    
                }
                else
                {
                    label.Location = new Point(x1, y);
                }
                this.Controls.Add(label);
                flagTop = true;
                IsRed(label);
            }
            #endregion

            #region �Ƿ������Label
            if (labelDic.TryGetValue(LabelPlace.Left, out label))
            {
                //�Ƿ���ͷ��Label
                if (flagTop)
                {
                    label.Location = new Point(x1, y2 + offsetLabelY);
                    x = x2 + offsetX;
                    y = y2;
                }
                else
                {
                    if (string.Compare(label.Text, "#:") == 0)
                    {
                        label.Text = "";
                    }
                    label.Location = new Point(x1, y1 + offsetLabelY);
                    x = x2 + offsetX;
                    y = y1;
                }

                #region �ж�ValueControl�Ƿ��И�־�Ե�ͼƬ�����н�ͼƬ�������Label
                if (!string.IsNullOrEmpty(AutoAttribute.LabelImage))
                {
                    Image image = Image.FromFile(AutoAttribute.LabelImage);
                    if (!string.IsNullOrEmpty(AutoAttribute.LabelLeft))
                    {
                        label.Size = new Size(txtWidth, image.Height);
                    }
                    else
                    {
                        label.Size = image.Size;
                        x = offsetX + offsetX + image.Width + offsetX + offsetX;
                        if (flagTop)
                        {
                            this.Controls[AutoAttribute.LabelTop].Location = new Point(x + offsetLabelX, y1);
                        }
                    }
                    label.Location = new Point(label.Location.X, label.Location.Y - offsetLabelY);
                    label.Image = image;
                    label.ImageAlign = ContentAlignment.MiddleLeft;
                }
                #endregion
                this.Controls.Add(label);
            }
            else
            {
                if (flagTop)
                {
                    y = y2;
                }
            }
            #endregion
            #region �������ؼ�
            this.SetMainControlSize(AutoAttribute.MainControlType);

            this._mainControl.Location = new Point(x, y);
            this.Controls.Add(_mainControl);

            int rightX = _mainControl.Location.X + _mainControl.Width;
            #endregion

            #region �Ƿ����Ҳ�Label
            if (labelDic.TryGetValue(LabelPlace.Right, out label))
            {
                label.Location = new Point(x + _mainControl.Width + offsetX, y + offsetLabelY);
                this.Controls.Add(label);
                rightX = label.Location.X + label.Width + offsetX;
                IsRed(label);
            }

            y = _mainControl.Location.Y + _mainControl.Height;
            #endregion

            #region �Ƿ��еײ�Label
            if (labelDic.TryGetValue(LabelPlace.Footer, out label))
            {
                label.Location = new Point(x + offsetLabelX, y + offsetY);
                this.Controls.Add(label);
                y = y + label.Height;
                IsRed(label);
            }
            x = _mainControl.Location.X;
            #endregion

            #region �Ƿ��а����ı�
            if (labelDic.TryGetValue(LabelPlace.Help, out label))
            {
                int indent = AutoAttribute.LabelHelpLeftIndent;
                int labelWidth;
                if (AutoAttribute.LabelHelpWidth <= 0)
                {
                    labelWidth = _mainControl.Width;
                }
                else
                {
                    labelWidth = AutoAttribute.LabelHelpWidth;
                }
                KeyValuePair<string, Size> pair =
                    GetSize(AutoAttribute.LabelHelpText, labelWidth, label.CreateGraphics(), label.Font);
                label.Size = pair.Value;
                label.Text = pair.Key;
                label.Location = new Point(x + offsetLabelX + indent, y);
                ///�ж����˰����ı����������Ƿ���ڡ��л����Ҳ��ı���ʱ�Ŀ��
                if ((label.Location.X + label.Width) > rightX)
                {
                    x = label.Location.X + label.Width + offsetX;
                }
                else
                {
                    x = rightX;
                }
                y = y + label.Height + offsetY;
                this.Controls.Add(label);
                IsRed(label);
            }
            else
            {
                x = rightX + offsetX;
            }
            #endregion

            this.Size = new Size(x, y);
            #endregion
        }
        protected void IsRed(Label lable)
        {
            if (AutoAttribute.IsRed)
            {
                lable.ForeColor = Color.Red;
            }

        }
        /// <summary>
        /// ���ݶ��������е�5��Label����������Label�ؼ�Dictionary�ļ���
        /// </summary>
        protected virtual Dictionary<LabelPlace, Label> BuildLabels()
        {
            Dictionary<LabelPlace, Label> labelDic = new Dictionary<LabelPlace, Label>(5);

            #region ѭ������5��Label
            for (int i = 0; i < 5; i++)
            {
                bool flag = false;
                string labelText = string.Empty;
                ContentAlignment align = ContentAlignment.MiddleLeft;
                LabelPlace place = LabelPlace.Left;

                switch (i)
                {
                    case 0://Top
                        #region
                        {
                            if (!string.IsNullOrEmpty(AutoAttribute.LabelTop))
                            {
                                place = LabelPlace.Top;
                                labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.LabelTop) + ":";
                                flag = true;
                            }
                            break;
                        }
                        #endregion
                    case 1://Left
                        #region
                        {
                            if (!string.IsNullOrEmpty(AutoAttribute.LabelLeft) || !string.IsNullOrEmpty(AutoAttribute.LabelImage))
                            {
                                place = LabelPlace.Left;
                                labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.LabelLeft) + ":";
                                align = ContentAlignment.TopRight;
                                flag = true;
                            }
                            break;
                        }
                        #endregion
                    case 2://Right
                        #region
                        {
                            if (!string.IsNullOrEmpty(AutoAttribute.LabelRight))
                            {
                                place = LabelPlace.Right;
                                labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.LabelRight);
                                flag = true;
                            }
                            break;
                        }
                        #endregion
                    case 3://Footer
                        #region
                        {
                            if (!string.IsNullOrEmpty(AutoAttribute.LabelFooter))
                            {
                                place = LabelPlace.Footer;
                                labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.LabelFooter);
                                flag = true;
                            }
                            break;
                        }
                        #endregion
                    case 4://Helper
                        #region
                        {
                            if (!string.IsNullOrEmpty(AutoAttribute.LabelHelpText))
                            {

                                place = LabelPlace.Help;
                                labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.LabelHelpText);
                                flag = true;
                            }
                            break;
                        }
                        #endregion
                }

                if (flag)//����Label
                {
                    #region
                    Label label = new Label();
                    label.Name = labelText;
                    label.Text = labelText;
              
                    if (place == LabelPlace.Right || place == LabelPlace.Top)
                    {
                        SizeF strSize = label.CreateGraphics().MeasureString(labelText, label.Font);
                        label.Size = Size.Truncate(strSize);
                        label.Width = label.Width + 3;
                    }
                    else
                    {
                        label.Size = _txtSize;
                    }
                    label.TabIndex = 0;
                    label.TextAlign = align;
                    labelDic.Add(place, label); // ���Label�ؼ�������������ӽ�Dictionary��ȥ
                    #endregion
                }
            }
            #endregion

            return labelDic;
        }

        /// <summary>
        /// ���ݶ��������е����ý����������ݿؼ�
        /// </summary>
        protected virtual Control BuildMainControl(MainControlType type)
        {
            PropertyInfo propertyInfo = MemberInfo as PropertyInfo;

            Control control = null;
            switch (type)
            {
                case MainControlType.TextBox:
                    control = new TextBox();
                    if (propertyInfo.GetSetMethod() == null)
                    {
                        ((TextBox)control).ReadOnly = true;
                    }
                    if (AutoAttribute.TextBoxMultiLine)
                    {
                        ((TextBox)control).Multiline = AutoAttribute.TextBoxMultiLine;
                        ((TextBox)control).ScrollBars = AutoAttribute.TextBoxScrollBars;
                        ((TextBox)control).Height = AutoAttribute.TextBoxHeight;
                    }
                    if (AutoAttribute.TextMaxLength!=0)
                    ((TextBox)control).MaxLength = AutoAttribute.TextMaxLength;
                    break;
                case MainControlType.SimpleCheckBox:
                    control = new SimpleCheckBox();
                    control.Text = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.SimpleCheckBoxText);
                    break;
                case MainControlType.ListBox:
                    control = new ListBox();
                    control.Height = AutoAttribute.ListBoxHeight;
                    ParseBindingState(control);
                    break;
                case MainControlType.ComboBox:
                    ComboBox box = new ComboBox();
                    if (AutoAttribute.ComboBoxStyle == 0)
                    {
                        box.DropDownStyle = ComboBoxStyle.DropDown;
                    }
                    else
                    {
                        box.DropDownStyle = AutoAttribute.ComboBoxStyle;
                    }
                    control = box;
                    ParseBindingState(control);
                    break;
                case MainControlType.Button:
                    control = new Button();
                    if (!string.IsNullOrEmpty (AutoAttribute.ButtonText))
                    {
                        control.Text = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.ButtonText);
                    }
                    control.Click += delegate
                    {
                        ((MethodInfo)MemberInfo).Invoke(null, null);
                    };
                    break;
                case MainControlType.NumericUpDown:
                    control = new NumericUpDown();
                    ((NumericUpDown)control).DecimalPlaces = AutoAttribute.NumericUpDownDecimalPlaces;
                    ((NumericUpDown)control).Maximum = AutoAttribute.NumericUpDownMax;
                    ((NumericUpDown)control).Minimum = AutoAttribute.NumericUpDownMin;
                    ((NumericUpDown)control).Increment = AutoAttribute.NumericUpDownStep;
                    break;
                case MainControlType.ColorGeneralButton:
                    control = new ColorGeneralButton();
                    break;
                case MainControlType.FileSelecterControl:
                    {
                        control = new FileSelecterControl();
                        ((FileSelecterControl)control).SelectFolder = AutoAttribute.SelectFolder;
                        ((FileSelecterControl)control).DialogTitle = AutoAttribute.FileSelecterControlDialogTitle;
                        ((FileSelecterControl)control).FileSelectFilter = AutoAttribute.FileSelecterControlFilter;
                        ((FileSelecterControl)control).InitialDirectory = AutoAttribute.FileSelecterControlInitialDirectory;
                        ((FileSelecterControl)control).MultiSelect = AutoAttribute.FileSelecterControlMultiSelect;
                        ((FileSelecterControl)control).Style = AutoAttribute.FileSelecterControlStyle;
                        break;
                    }
                case MainControlType.SelectGroup:
                    {
                        SelectGroup selectGroup = new SelectGroup();
                        selectGroup.Width = AutoAttribute.MainControlWidth;
                        selectGroup.HIndent = AutoAttribute.SelectGroupHIndent;
                        selectGroup.HorizontalCount = AutoAttribute.SelectGroupHorizontalCount;
                        selectGroup.LineHeight = AutoAttribute.SelectGroupLineHeight;
                        selectGroup.MultiSelect = AutoAttribute.SelectGroupMultiModel;
                        selectGroup.VIndent = AutoAttribute.SelectGroupVIndent;
                        selectGroup.SelectedItemCount = AutoAttribute.SelectedItemCount;
                        control = selectGroup;
                        ParseBindingState(control);
                        break;
                    }
                case MainControlType.DateTimePicker:
                    {
                        control = new DateTimePicker();
                        ((DateTimePicker)control).Checked = AutoAttribute.DateTimePickerChecked;
                        ((DateTimePicker)control).CustomFormat = AutoAttribute.DateTimePickerCustomFormat;
                        ((DateTimePicker)control).ShowCheckBox = AutoAttribute.DateTimePickerShowCheckBox;
                        ((DateTimePicker)control).ShowUpDown = AutoAttribute.DateTimePickerShowUpDown;
                        break;
                    }
                case MainControlType.ValidateTextBox:
                    {
                        control = new ValidateTextBox();
                        ((ValidateTextBox)control).RegexText = AutoAttribute.ValidateTextBoxRegexText;
                        ((ValidateTextBox)control).RegexTextRuntime = AutoAttribute.ValidateTextBoxRegexTextRuntime;
                        if (propertyInfo.GetSetMethod() == null)
                        {
                            ((ValidateTextBox)control).ReadOnly = true;
                        }
                        break;
                    }
                case MainControlType.DepartmentControl:
                    {
                        // string path = Path.Combine(AutoLayoutPanel.ResourcesPath, _autoAttribute.MainControlBindingFile);
                        control = new DepartmentControl();
                        break;
                    }
                case MainControlType.ColorSelectorButton:
                    {
                        control = new ColorSelectorButton();
                        control.Width = AutoAttribute.MainControlWidth;
                        break;
                    }
                case MainControlType.FontComboBox:
                    {
                        control = new FontComboBox();
                        break;
                    }
                case MainControlType.ComboBoxGroupControl:
                    {
                        string path = Path.Combine(AutoLayoutPanel.ResourcesPath, AutoAttribute.MainControlBindingFile);
                        string mark = "";
                        if (!string.IsNullOrEmpty(AutoAttribute.SpaceMark))
                        {
                            mark = AutoLayoutPanel.GetLanguageText(AutoAttribute.SpaceMark);
                        }
                        ComboBoxGroupControl comboBoxGroup = new ComboBoxGroupControl(path, mark);

                        control = comboBoxGroup;
                        break;
                    }
                case MainControlType.CheckBoxExControl:
                    {
                        string labelText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.CheckBoxExLabelText);
                        string checkText = AutoLayoutPanel.GetLanguageText(AutoLayoutPanelXmlDocument.Singler, AutoAttribute.CheckBoxExText); ;
                        control = new CheckBoxExControl(labelText, checkText);
                        break;
                    }
                default:
                    Debug.Fail("\"" + type + "\" cannot prase!");
                    break;
            }

            ///�������Ե�IsReadOnlyΪtrue��������û��set�����򽫿ؼ���Enabled��Ϊfalse
            if (AutoAttribute.IsReadOnly ||
                (MemberInfo.MemberType == MemberTypes.Property && !((PropertyInfo)MemberInfo).CanWrite))
            {
                if (control is TextBox)
                {
                    ((TextBox)control).ReadOnly = true;
                }
                else
                {
                    control.Enabled = false;
                }
            }
      
            return control;
        }

        #region �����
        protected virtual void FileBindingService(Control ctr, string fileName, out DataTable dt)
        {
            dt = DataTableService(fileName);
            FileBindingService(ctr, fileName);
        }
        protected virtual void FileBindingService(Control ctr, string fileName)
        {
            DataTable dt = DataTableService(fileName);
            DtBindingService(ctr, dt);
        }
        protected virtual void EnumBindingService(Control ctr, Type myType)
        {
            //Array texts = Enum.GetValues(myType);

            //DataTable dt = new DataTable();
            //dt.Columns.Add("value");
            //dt.Columns.Add("text");

            //foreach (var item in texts)
            //{
            //    string text = Service.Resource.GetEnumResourceText(item.ToString());
            //    dt.Rows.Add(Enum.Parse(myType, item.ToString()), text);
            //}
            //DtBindingService(ctr, dt);
            Debug.Fail("");
        }
        DataTable DataTableService(string fileName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            dt.Columns.Add("text");

            Debug.Assert(File.Exists(fileName), "Configtion file isn't Exists");
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.GetAttribute("text") != null && reader.GetAttribute("value") != null)
                    {
                        dt.Rows.Add(reader.GetAttribute("value"), reader.GetAttribute("text"));
                    }
                }
            }
            return dt;
        }
        protected void DtBindingService(Control ctr, DataTable dt)
        {
            if (ctr == null)
            {
                return;
            }
            if (ctr is ListControl)
            {
                ListControl myControl = (ListControl)ctr;
                myControl.DataSource = dt;
                myControl.DisplayMember = "text";
                myControl.ValueMember = "value";
            }
            if (ctr is SelectGroup)
            {
                SelectGroup myControl = (SelectGroup)ctr;
                myControl.DataSource = dt;
                myControl.DisplayMember = "text";
                myControl.ValueMember = "value";
                myControl.DataBinding();
            }
        }

        #endregion                        

        private void ParseBindingState(Control control)
        {
            if (this.MemberInfo.MemberType != MemberTypes.Property)
            {
                return;
            }
            PropertyInfo propertyInfo = (PropertyInfo)MemberInfo;

            if (propertyInfo.PropertyType.BaseType == typeof(System.Enum))
            {
                EnumBindingService(control, propertyInfo.PropertyType);
            }
            else
            {
                string file = Path.Combine(AutoLayoutPanel.ResourcesPath, AutoAttribute.MainControlBindingFile);
                FileBindingService(control, file);
            }
        }

        /// <summary>
        /// ���ݶ��������е��������ValueControl�е����ؼ���Size
        /// </summary>
        protected virtual void SetMainControlSize(MainControlType type)
        {
            int width = AutoAttribute.MainControlWidth;
            switch (type)
            {
                case MainControlType.TextBox:
                case MainControlType.ComboBox:
                case MainControlType.NumericUpDown:
                case MainControlType.DateTimePicker:
                case MainControlType.ValidateTextBox:
                    this._mainControl.Size = new Size(width, inpHeignt);
                    break;
                case MainControlType.ListBox:
                    this._mainControl.Size = new Size(width, AutoAttribute.ListBoxHeight);
                    break;
                case MainControlType.Button:
                case MainControlType.ColorGeneralButton:
                    this._mainControl.Size = new Size(width, inpHeignt);
                    break;
                case MainControlType.FileSelecterControl:
                    this._mainControl.Size = new Size(width, inpHeignt + 1);
                    break;
                case MainControlType.SimpleCheckBox:
                    ((CheckBox)_mainControl).AutoSize = true;
                    break;
                case MainControlType.SelectGroup:
                    //����SelectGroup�Ĵ�С(��ʵ���Եõ���ǰselectGroup�Ŀ��),Ŀǰ�����¶�SelectGroup�ڲ��ؼ�������������
                    ((SelectGroup)_mainControl).AutoSize = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Label��ǩ��λ��
        /// </summary>
        protected enum LabelPlace { Top, Left, Right, Footer, Help, }

        #endregion
    }

    public class SimpleCheckBox : CheckBox
    {
        public bool Value
        {
            get { return this.Checked; }
            set { this.Checked = value; }
        }
    }
}
