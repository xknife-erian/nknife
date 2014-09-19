using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.SimplusD;
using System.Xml;
using Jeelu.Win;
namespace Jeelu.SimplusD.Client.Win
{
    public partial class ProductPropertyForm : Form
    {
        public List<KeyValuePair<string,bool>>productList;
        public List<string>notUserProperty;
        public List<string> userProperty;
        private XmlElement _ele;
        public string GroupName;
        private string _isAdd;
        public string isAdd
        {
            get { return _isAdd; }
        }
        public ProductPropertyForm()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            _isAdd = "add";
            userProperty = new List<string>();
            notUserProperty = new List<string>();
        }
        public ProductPropertyForm(XmlElement ele)
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            _isAdd = "modify";
            userProperty = new List<string>();
            notUserProperty = new List<string>();
            _ele = ele;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //显示固定的属性
            showGroup();
            ShowValue();
        }
        private void showGroup()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathService.CL_DS_ProductProperty);
            if (doc == null)
            {
                return;
            }
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                SelectGroupItem item = new SelectGroupItem(node.Attributes["value"].Value, node.Attributes["text"].Value);
                this.conProductPropGroup.Items.Add(item);
                notUserProperty.Add(item.Text);
            }
        }
        private void ShowValue()
        {  
            List<string> selectValueList = new List<string>();
            if (_isAdd == "modify")
            {
               if (_ele != null)
               {
                   this.txtPropGroupName.Text = _ele.GetAttribute("groupName");
                   XmlNodeList nodes = _ele.SelectNodes("child::item");
                 foreach (XmlElement node in nodes)
                 {
                     if (node.GetAttribute("isUserAdd")=="True")
                     {
                         SelectGroupItem item = new SelectGroupItem(node.GetAttribute("name"), node.GetAttribute("name"));
                         this.conProductPropGroup.Items.Add(item);
                         userProperty.Add(node.GetAttribute("name"));
                     }
                    string name= node.GetAttribute("name");
                    selectValueList.Add(name);
                 }
                 this.conProductPropGroup.SelectedStringValues= selectValueList.ToArray();
               }
            }
        }
        private void btnYes_Click(object sender, EventArgs e)
        {
            string propName = this.txtAutoDefineProp.Text;
            if (string.IsNullOrEmpty(propName))
                return;
            List<string> list = SelectStart();
            if (list.Contains(propName))
                return;
            SelectGroupItem item = new SelectGroupItem(propName, propName);
            this.conProductPropGroup.Items.Add(item);
            userProperty.Add(propName);
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
           GetExistedProperties();
           if (!string.IsNullOrEmpty(this.txtPropGroupName.Text))
           {
               GroupName = this.txtPropGroupName.Text;
               this.DialogResult = DialogResult.OK;
               this.Close();
           }
           else
               Jeelu.MessageService.Show("未设置组名");
        }
        /// <summary>
        /// 得到固定的属性名称
        /// </summary>
        /// <returns></returns>
        private void GetExistedProperties()
        {
            productList = new List<KeyValuePair<string, bool>>();
           
            foreach (string value in this.conProductPropGroup.SelectedValues)
            {
                foreach (string item in userProperty)
                {
                    if (value == item)
                    {
                        KeyValuePair<string, bool> key = new KeyValuePair<string, bool>(value,true);
                        productList.Add(key);
                    }
                }
                foreach (string itemu in notUserProperty)
                {
                    if (value == itemu)
                    {
                        KeyValuePair<string, bool> key = new KeyValuePair<string, bool>(value, false);
                        productList.Add(key);
                    }
                }
            }
        }
        private List<string> SelectStart()
        {
            List<string> list = new List<string>();
            foreach (Control control in this.conProductPropGroup.Controls)
            {
                if (control is CheckBox)
                {
                    list.Add(control.Text);
                }
            }
            return list;
        }
    }
}

