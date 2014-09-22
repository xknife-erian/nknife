using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace Jeelu.Win
{
    public class ComboBoxGroupControl : UserControl
    {
        #region 私有属性
        private string _labelText;
        private int _comboBoxCount;
        private string[] _values;
        XmlDocument xmlDoc;

        /// <summary>
        /// String意为等级||DataTable意为数据表
        /// </summary>
        Dictionary<string, DataTable> _dic = new Dictionary<string, DataTable>();
        Dictionary<Control, string> _dicControlDataBindId = new Dictionary<Control, string>();
        List<ComboBox> _list = new List<ComboBox>();
        #endregion

        public string[] SelectedValues
        {
            get
            {
                if (isC)
                {
                    ReadValue();
                    isC = false;
                }
                return _values;
            }
            set
            {
                _values = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comboBoxNum">控件的个数</param>
        /// <param name="fileName">绑定的文件</param>
        public ComboBoxGroupControl(string fileName,string text)
        {
            _labelText = text;
            xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            _comboBoxCount = GetLevel(xmlDoc);
            LoadControl();
           
        }
        bool isC;
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            InitControl();
            isC = true;
            
            if (_values.Length == 0)
            {
                return;
            }
            
            for (int i = 0; i < _list.Count; i++)
            {
                _list[i].SelectedValue = _values[i];
            }
        }

        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedValueChanged(EventArgs.Empty);
        }

        public void ReadValue()
        {
            _values = new string[_comboBoxCount];
            int i = 0;
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(ComboBox))
                {
                    if (((ComboBox)control).SelectedValue!=null)
                    _values[i++] = ((ComboBox)control).SelectedValue.ToString(); ///顺序相关
                }
            }
        }

        private int GetLevel(XmlDocument doc)
        {
            int sumLevel = 0;
            XmlElement ele = doc.DocumentElement;
            while (ele.HasChildNodes)
            {
                sumLevel++;
                ele = (XmlElement)ele.FirstChild;
            }
            return sumLevel;
        }

        /// <summary>
        /// 加载控件
        /// </summary>
        private void LoadControl()
        {
            int x = 0, y = 0;
            for (int level = 1; level < _comboBoxCount + 1; level++)
            {
                //创建控件
                ComboBox comboBox = new ComboBox();
                comboBox.Name = "comboBox" + level.ToString();
                comboBox.Location = new System.Drawing.Point(x, y);
                comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
                this.Controls.Add(comboBox);
                //添加标签
                if (string.IsNullOrEmpty(_labelText))
                {
                    x = x + comboBox.Width + 5;
                }
                else
                {
                    if (level != _comboBoxCount)
                    {
                        x = x + comboBox.Width + 2;

                        //存在标签
                        Label label = new Label();
                        label.Text = _labelText;
                        label.TextAlign = ContentAlignment.MiddleCenter;
                        label.AutoSize = true;
                        label.Location = new Point(x, y + 1);
                        this.Controls.Add(label);
                        x = x + label.Width + 2;
                        y = 0;
                    }
                    else
                    {//记录最后的x坐标
                        x = x + comboBox.Width + 2;
                    }

                }
                _dicControlDataBindId.Add(comboBox, level.ToString());
                _list.Add(comboBox);
            }
            this.Size = new Size(x, 20);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            XmlElement xmlEle = xmlDoc.DocumentElement;

            for (int level = 1; level < _comboBoxCount + 1; level++)
            {
                DataTable dt = LoadDataTable(xmlEle, level, null);
                _dic.Add(level.ToString(), dt);
            }

            ///显示控件数据
            for (int i = 0; i < _list.Count; i++)
            {
                if (i != 0)
                {
                    BindFile(_list[i], _list[i - 1], (i + 1).ToString());
                }
                else
                {
                    BindFile(_list[i], null, (i + 1).ToString());
                }
            }
        } 

        /// <summary>
        /// 载入XmlNode的数据到DataTable中去,level为数据在XmlNode中的层数
        /// </summary>
        DataTable LoadDataTable(XmlElement node, int level, string selectValue)
        {
            if (level == 1)
            {
                return LoadDataTableEx(node, selectValue);
            }
            else
            {
                DataTable dtAll = new DataTable();
                foreach (XmlNode n in node.ChildNodes)
                {
                    XmlAttribute attParent = n.Attributes["value"];
                    DataTable dt = LoadDataTable((XmlElement)n, level - 1, attParent == null ? null : attParent.Value);
                    dtAll.Merge(dt);
                }
                return dtAll;
            }
        }

        DataTable LoadDataTableEx(XmlElement node, string selectValue)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            dt.Columns.Add("text");

            if (!string.IsNullOrEmpty(selectValue))
            {
                dt.Columns.Add("selectValue");
            }
            foreach (XmlNode n in node.ChildNodes)
            {
                if (!string.IsNullOrEmpty(selectValue))
                {
                    dt.Rows.Add(n.Attributes["value"].Value, n.Attributes["text"].Value, selectValue);
                }
                else
                {
                    dt.Rows.Add(n.Attributes["value"].Value, n.Attributes["text"].Value);
                }
            }
            return dt;
        }
        /// <summary>
        /// 绑定文件
        /// </summary>
        private void BindFile(ComboBox control, ComboBox selectControl, string id)
        {
            ///载入数据
            DataTable dt = _dic[id];

            if (selectControl != null)
            {
                EventHandler bindingHandler = delegate(object sender, EventArgs e)
                {
                    DataView dv = new DataView(_dic[_dicControlDataBindId[control]]);
                    dv.RowFilter = string.Format("selectValue='{0}'", selectControl.SelectedValue);
                    control.DataSource = dv;
                    control.DisplayMember = "text";
                    control.ValueMember = "value";
                };
                selectControl.SelectedValueChanged += bindingHandler;

                //首先执行一次绑定
                bindingHandler(selectControl, EventArgs.Empty);
            }
            else
            {
                control.DataSource = dt;
                control.DisplayMember = "text";
                control.ValueMember = "value";
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
    }
}
