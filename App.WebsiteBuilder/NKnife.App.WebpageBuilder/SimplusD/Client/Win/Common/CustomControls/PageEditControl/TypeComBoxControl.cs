using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class TypeComBoxControl : UserControl
    {
        #region
        private string fileName;
        private int count;
        List<string> list;
        List<string> valueShow;
        Dictionary<string, ComboBox> dicCombox;

        /// <summary>
        /// 内部使用的一个变量，存储现有的数据
        /// </summary>
        List<TypeData> _value = new List<TypeData>();
        /// <summary>
        /// 提供给外界使用的
        /// </summary>
        public TypeData[] Value
        {
            get
            {
                Save();
                return _value.ToArray();
            }
            set
            {
                _value.Clear();
                foreach (TypeData part in value)
                {
                    _value.Add(part);
                }
            }
        }
        public string ResourceFileName { get; set; }
        #endregion
        #region 构造函数 Onload
        public TypeComBoxControl()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ShowPanel();
            AddValue();
            
        }
        #endregion
        #region 绘制Panel上的控件
        private void ShowPanel()
        {
            fileName = ResourceFileName;
            XmlDocument doc = CreateDocument();
            TagCount(doc);
        }

        private XmlDocument CreateDocument()
        {
            string resourceFile = Path.Combine(PathService.CL_DataSources_Folder, fileName);
            XmlDocument doc = new XmlDocument();
            doc.Load(resourceFile);

            return doc;
        }
        /// <summary>
        /// 统计Document的节点级数
        /// </summary>
        /// <param name="doc"></param>
        private void TagCount(XmlDocument doc)
        {
            valueShow = new List<string>();
            if (doc.DocumentElement != null)
            {
                if (doc.DocumentElement.HasChildNodes)
                {
                    foreach (XmlElement ele in doc.DocumentElement.SelectNodes("child::*"))
                    {
                        valueShow.Add( ele.GetAttribute("text")); 
                        count = 1;
                        ReturnValue(ele);
                    }
                   PaintCombox(); 
                }
            }
        }
        //TODO:树的深度有问题（此方法只适用一级节点的子结点为最深的节点）
        private int ReturnValue(XmlElement ele)
        {
            if (ele.HasChildNodes)
            {
                count++;
                //foreach(XmlElement eleChild in ele.ChildNodes)
                //{
                    ReturnValue((XmlElement)ele.ChildNodes[0]);  
                //}
            }
             return count;
        }
       /// <summary>
       /// 根据节点的级数添加ComBox
       /// </summary>
        private void PaintCombox()
        {
            int x = 0;
            int y = 0;
            dicCombox = new Dictionary<string, ComboBox>();
            for (int i = 0; i < count; i++)
            {
               ComboBox control = new ComboBox();
               control.Width = 120;
               control.Location = new Point(x, y + 5);
               x = x + control.Width + 2;
               control.Name = "combox" + i;
               this.conTypePanel.Controls.Add(control);
               control.DropDownStyle = ComboBoxStyle.DropDownList;
               dicCombox.Add(control.Name, control); 
               control.SelectedIndexChanged += new EventHandler(control_SelectedIndexChanged);
            }
            if (this.conTypePanel.Controls[0] is ComboBox)
            {
                ((ComboBox)this.conTypePanel.Controls[0]).DataSource = valueShow;
                ((ComboBox)this.conTypePanel.Controls[0]).SelectedItem = valueShow[0]; 
            }
        }
        void control_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = ((ComboBox)sender).SelectedItem.ToString();
            int x =Convert.ToInt32((((ComboBox)sender).Name).Substring(6))+1;
            ComboBox combox;
            if (dicCombox.TryGetValue("combox"+x.ToString(), out combox))
            {
               BindingComboBox(value, combox); 
            } 
        }
        private void BindingComboBox(string value,ComboBox combox)
        {
            List<string> itemList = new List<string>();
            XmlDocument doc = CreateDocument();
            XmlNode node=doc.SelectSingleNode("//*[@text='" + value + "']");
            if (node != null)
            {
                if (node.ChildNodes != null)
                {
                    foreach (XmlElement nodeItem in node.ChildNodes)
                    {
                        itemList.Add(nodeItem.GetAttribute("text"));
                    }
                    combox.DataSource = itemList;
                    if (combox.Items.Count != 0)
                    {
                        combox.SelectedItem = combox.Items[0];
                    }
                }
            }
        }
        private void AddValue()
        {
            list=new List<string>();
            if (_value.Count!=0)
            {
                foreach (TypeData item in _value)
                {
                    list.Add(item.value);
                }
                AddValueCombox();
            }
        }
        /// <summary>
        /// 设置combox中的值
        /// </summary>
        private void AddValueCombox()
        {
            int i = 0;
            foreach (Control control in this.conTypePanel.Controls)
            {
                if (control is ComboBox)
                {
                    if (((ComboBox)control).Items.Count != 0)
                    {
                        ((ComboBox)control).SelectedItem = list[i];
                        i++;
                    }
                }
            }
        }
        private void Save()
        {
            _value.Clear();
            foreach (Control item in this.conTypePanel.Controls)
            {
                if (item is ComboBox)
                {
                    TypeData data = new TypeData();
                    if (((ComboBox)item).Items.Count != 0)
                    {
                        data.value = ((ComboBox)item).SelectedItem.ToString();
                        _value.Add(data);
                    }
                }

            }
        }
        #endregion
    }
}
