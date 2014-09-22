using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Jeelu.SimplusD;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class ProductProperty : UserControl
    {
        #region 初始化
        protected ItemCollection productItem;
        protected List<string> list;
        private bool IsAdd;
        private string _selectGroupName;
        private string _productGroupName;
        List<KeyValuePair<string, string>> propertyList;
        Dictionary<string, List<KeyValuePair<string, string>>> dic = null;
        public ItemCollection value
        {
            get { Save(); return productItem; }
            set { productItem = value; }
        }
        #endregion
        #region load，构造函数
        public ProductProperty()
        {
            InitializeComponent();
            list = new List<string>();
            propertyList = new List<KeyValuePair<string, string>>();
        }
        private void ProductProperty_Load(object sender, EventArgs e)
        {
            MadeForm();
            //添加商品属性组的名称
            AddProuctNameList(string.Empty);
            //将Element中的值绑定到字典
            dic = new Dictionary<string, List<KeyValuePair<string, string>>>();
            ShowForm();
            //设置当前页面保存的值
            NowPageProperty();
            _selectGroupName = this.conProductProp.Text;
            this.conProductProp.SelectedIndexChanged += new EventHandler(conProductProp_SelectedIndexChanged);
            Changed += new EventHandler(ProductProperty_Changed);
        }

        void ProductProperty_Changed(object sender, EventArgs e)
        {
            if (this.FindForm() != Service.Workbench.ActiveForm)
            {
                //添加商品属性组的名称
                AddProuctNameList(string.Empty);
                //将Element中的值绑定到字典
                dic = new Dictionary<string, List<KeyValuePair<string, string>>>();
                ShowForm();
                this.conProductProp.SelectedValue = -1;
                this.conProductPropPanel.Controls.Clear();
                //设置当前页面保存的值
                NowPageProperty();
                
            }
        }
        #endregion
        #region 产品属性组名称的加载与设置默认值
        private void AddProuctNameList(string id)
        {
            DataTable dt = new DataTable();
            dt.Columns .Add("groupId");
            dt.Columns.Add("groupName");
            XmlElement productEle=null;
            //添加产品的属性名称到Combox中
            if (Service.Sdsite.DesignDataDocument.ProductPropGroupEle != null)
            {
                XmlNodeList nodes=Service.Sdsite.DesignDataDocument.ProductPropGroupEle.SelectNodes("//GroupItem");
                foreach (XmlNode node in nodes)
                {
                    if (node is XmlElement && node != null)
                    {
                        productEle = (XmlElement)node;
                        dt.Rows.Add(productEle.GetAttribute("groupId"), productEle.GetAttribute("groupName"));
                    }   
                }
                this.conProductProp.DataSource = dt;
                this.conProductProp.DisplayMember = "groupName";
                this.conProductProp.ValueMember ="groupId";
                if (!IsAdd)
                {
                    if (this.conProductProp.SelectedValue == null)
                        this.conProductProp.SelectedValue = -1;
                    else
                    {
                        if (string.IsNullOrEmpty(productItem.GroupId))
                            this.conProductProp.SelectedValue = -1;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        this.conProductProp.SelectedValue = id;
                    }
                    IsAdd = false;
                }
            }
        }
        private void ShowForm()
        {
            if (Service.Sdsite.DesignDataDocument.ProductPropGroupEle != null)
            {
                List<KeyValuePair<string, string>> _list = null;
                XmlElement productEle = null;
                XmlNodeList nodes = Service.Sdsite.DesignDataDocument.ProductPropGroupEle.SelectNodes("//GroupItem");
                foreach (XmlNode node in nodes)
                {
                    if (node is XmlElement && node != null)
                    {
                        productEle = (XmlElement)node;
                        _list = new List<KeyValuePair<string, string>>();
                        foreach (XmlElement item in node.ChildNodes)
                        {
                            KeyValuePair<string, string> key = new KeyValuePair<string, string>(item.GetAttribute("name"), "");
                            _list.Add(key);
                        }

                        if (!dic.ContainsKey(productEle.GetAttribute("groupId")))
                        {
                            dic.Add(productEle.GetAttribute("groupId"), _list);
                        }
                    }
                }

            }
        }
        private void NowPageProperty()
        {
            //设置当前页面的产品属性
            if (productItem != null)
            {
                if (string.IsNullOrEmpty(productItem.GroupName.ToString()))
                {
                    MadeButtunStartState();
                    return;
                }
                this.conProductProp.SelectedValue = productItem.GroupId;
                string _value=string.Empty;
                XmlElement propertyEle = null;
                if (productItem.HasChildNodes)
                {
                    propertyList.Clear();
                    foreach (XmlNode propertyValue in productItem.ChildNodes)
                    {
                        if (propertyValue is XmlElement && propertyValue != null)
                        {
                           propertyEle = (XmlElement)propertyValue;
                           KeyValuePair<string, string> key = new KeyValuePair<string, string>(propertyEle.GetAttribute("name"), propertyEle.GetAttribute("value"));
                           propertyList.Add(key);
                          
                        }
                    }
                    List<KeyValuePair<string, string>> _list = new List<KeyValuePair<string, string>>();
                    foreach (KeyValuePair<string, string> keytag in dic[productItem.GroupId])
                    {
                        _value = GetValue(propertyList, keytag.Key);
                        KeyValuePair<string, string> key = new KeyValuePair<string, string>(keytag.Key, _value);
                        _list.Add(key);
                    }
                    dic[productItem.GroupId] = _list;
                    ShowPropertyNameValue(_list);
                    MadeButtunState();
                }
            }
        }
        
        #endregion 
        #region Form资源文件的加载
        private void MadeForm()
        {
            this.lblPropertyGroupChoose.Text = StringParserService.Parse("${res:productPage.choseGroup}");
            this.btnAddProp.Text = StringParserService.Parse("${res:productPage.add}");
            this.btnDelProp.Text = StringParserService.Parse("${res:productPage.del}");
            this.btnModifyProp.Text = StringParserService.Parse("${res:productPage.modify}");
        }
        #endregion 
        #region 切换属性组
        string preId;
        void conProductProp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.conProductProp.Text))
                MadeButtunState();
            else
            {
                MadeButtunStartState();
                return;
            }
            if (preId != "")
            {
                SaveList(preId);
            }
            else 
            {
                preId = productItem.GroupId;
            }
            preId = this.conProductProp.SelectedValue.ToString();
            
            SelectProductGroup();
            OnCheckChanged(EventArgs.Empty);
        }

        private void SelectProductGroup()
        {

           string id= this.conProductProp.SelectedValue.ToString();
           if (dic.ContainsKey(id))
           {
               ShowPropertyNameValue(dic[id]);
           }
        } 
        private void MadeButtunStartState()
        {
            this.btnAddProp.Enabled = true;
            this.btnDelProp.Enabled = false;
            this.btnModifyProp.Enabled = false;
        }
        private void MadeButtunState()
        {
            this.btnAddProp.Enabled = true;
            this.btnDelProp.Enabled = true;
            this.btnModifyProp.Enabled = true;
        }
        #endregion
        #region 添加，修改，删除
        private void btnAddProp_Click(object sender, EventArgs e)
        {
            ProductPropertyForm form = new ProductPropertyForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                IsAdd = true;
                _productGroupName = this.conProductProp.Text;
                ShowPanel(form,"");
                Service.Sdsite.DesignDataDocument.Save();
                if (Changed != null)
                {
                    Changed(null, EventArgs.Empty);
                }
                OnCheckChanged(EventArgs.Empty);
            }
        }
        private void btnModifyProp_Click(object sender, EventArgs e)
        {
            XmlNode node = Service.Sdsite.DesignDataDocument.ProductPropGroupEle.SelectSingleNode("//GroupItem[@groupName='" + this.conProductProp.Text+ "']");
            if (node != null && node is XmlElement)
            {
                XmlElement groupEle = (XmlElement)node;
                ProductPropertyForm form = new ProductPropertyForm(groupEle);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _productGroupName = form.GroupName;
                    ShowPanel(form, groupEle.GetAttribute("groupId"));
                    Service.Sdsite.DesignDataDocument.Save();
                    if (Changed != null)
                    {
                        Changed(null, EventArgs.Empty);
                    }
                    OnCheckChanged(EventArgs.Empty);
                }
            }
        }
        private void btnDelProp_Click(object sender, EventArgs e)
        {
            XmlNode node = Service.Sdsite.DesignDataDocument.ProductPropGroupEle.SelectSingleNode("//GroupItem[@groupName='" + this.conProductProp.Text + "']");
            if (node != null && node is XmlElement)
            {
                XmlElement groupEle = (XmlElement)node;
                if (Utility.Convert.StringToBool(groupEle.GetAttribute("isUsed")))
                {
                    MessageService.Show("该属性组被使用",MessageBoxButtons.OK);
                }
                else
                {
                    IsAdd = true;
                    _selectGroupName = string.Empty;
                    Service.Sdsite.DesignDataDocument.ProductPropGroupEle.RemoveChild(node);
                    Service.Sdsite.DesignDataDocument.Save();
                    if (Changed != null)
                    {
                        Changed(null, EventArgs.Empty);
                    }
                    AddProuctNameList(string.Empty);
                    if (string.IsNullOrEmpty(this.conProductProp.Text))
                    {
                        MadeButtunStartState();
                        this.conProductPropPanel.Controls.Clear();
                    }
                    OnCheckChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// 将修改的值保存到Service.Sdsite.DesignDataDocument.ProductPropGroupEle
        /// </summary>
        /// <param name="form"></param>
        /// <param name="id"></param>
        private void ShowPanel(ProductPropertyForm form,string id)
        {
            XmlElement groupItem;
            if (string.IsNullOrEmpty(id))
            {
                string groupId = Utility.Guid.NewGuid();
                groupItem = Service.Sdsite.DesignDataDocument.ProductPropGroupEle.OwnerDocument.CreateElement("GroupItem");
                Service.Sdsite.DesignDataDocument.ProductPropGroupEle.AppendChild(groupItem);
                groupItem.SetAttribute("groupId", groupId);
                groupItem.SetAttribute("isUsed", false.ToString());
                groupItem.SetAttribute("groupName", form.GroupName);
                List<KeyValuePair<string, string>> _list = new List<KeyValuePair<string, string>>();
                foreach (KeyValuePair<string,bool> item in form.productList)
                {
                    XmlElement itemEle = Service.Sdsite.DesignDataDocument.ProductPropGroupEle.OwnerDocument.CreateElement("item");
                    itemEle.SetAttribute("name", item.Key);
                    itemEle.SetAttribute("isUserAdd", item.Value.ToString());
                    groupItem.AppendChild(itemEle);
                    KeyValuePair<string,string>key=new KeyValuePair<string,string>(item.Key,"");
                    _list.Add(key);
                }
                dic.Add(groupId, _list);
                AddProuctNameList(groupId);
            }
            else
            {
                SaveList(id);
                XmlNode groupNode = Service.Sdsite.DesignDataDocument.ProductPropGroupEle.SelectSingleNode("//GroupItem[@groupId='" + id + "']");
                groupNode.RemoveAll();
                groupItem = (XmlElement)groupNode;
                groupItem.SetAttribute("groupId", id);
                groupItem.SetAttribute("isUsed", true.ToString());
                groupItem.SetAttribute("groupName", form.GroupName);

                List<KeyValuePair<string, string>> _list = new List<KeyValuePair<string, string>>();

                foreach (KeyValuePair<string, bool> item in form.productList)
                {
                    XmlElement itemEle = Service.Sdsite.DesignDataDocument.ProductPropGroupEle.OwnerDocument.CreateElement("item");
                    itemEle.SetAttribute("name", item.Key);
                    itemEle.SetAttribute("isUserAdd", item.Value.ToString());
                    groupItem.AppendChild(itemEle);
                                       
                    string value = GetValue(dic[id],item.Key);
                    KeyValuePair<string, string> vp = new KeyValuePair<string, string>(item.Key, value);
                    _list.Add(vp);
                }
                dic[id] = _list;
                ShowPropertyNameValue(dic[id]);
            }
         
        }
        /// <summary>
        /// 取出dic中的list下的value值
        /// </summary>
        /// <param name="list"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private string GetValue(List<KeyValuePair<string, string>> list, string name)
        {
            string value = "";
            foreach (KeyValuePair<string ,string > item in list)
            {
                if (item.Key == name)
                {
                    value = item.Value;
                }
            }
            return value;
        }

        
        private void SaveList(string id)
        {
            if (!string.IsNullOrEmpty (id))
            {
                if (dic.ContainsKey(id))
                {
                    dic[id].Clear();
                    foreach (Control control in this.conProductPropPanel.Controls)
                    {
                        if (control is TextBox)
                        {
                            KeyValuePair<string, string> vp = new KeyValuePair<string, string>(control.Tag.ToString(), control.Text);
                            dic[id].Add(vp);
                        }
                    }
                }
                else
                {
                    MessageService.Show("id不存在！");
                }
            }
            
        }
        #endregion
        #region 绘制Panel
        private void ShowPropertyNameValue(List<KeyValuePair<string, string>> values)
        {
            int x = 0;
            int y = 0;
            int levelSpace = 10; //水平间距
            int controlWidth = 150; //标签与文本框最大宽度为150
            int maxWidth = this.conProductPropPanel.Width;//最大宽度
            int verticalSapce = 5; //行间距

            int num = maxWidth / controlWidth; //一行可显示的个数;
            int init = 1;//初始值 为显示一个
            conProductPropPanel.Controls.Clear();
            foreach (KeyValuePair<string, string> value in values)
            {
                if (init > num)
                {
                    //则换行显示
                    x = 0;
                    y = y + 21 + verticalSapce;
                    init = 1;
                }

                Label label = new Label();
                label.Width = 60;
                label.Text = value.Key + ":";
                label.Location = new Point(x, y + 5);
                conProductPropPanel.Controls.Add(label);
                x = x + label.Width;

                TextBox textBox = new TextBox();
                textBox.Width = 100;
                textBox.Tag = value.Key;
                textBox.Text = value.Value;
                textBox.Location = new Point(x, y);

                conProductPropPanel.Controls.Add(textBox);

                x = x + textBox.Width + levelSpace;
                init++;
            }
        }
        #endregion
        #region
        private void Save()
        {
            XmlNode node = Service.Sdsite.DesignDataDocument.ProductPropGroupEle.SelectSingleNode("//GroupItem[@groupName='" + this.conProductProp.Text + "']");
            if (node != null && node is XmlElement)
            {  
                XmlElement groupEle = (XmlElement)node;
                if (!productItem.GroupId.Equals(groupEle.GetAttribute("groupId")) && !string.IsNullOrEmpty(productItem.GroupId))
                {
                    XmlNode nodeOld = Service.Sdsite.DesignDataDocument.ProductPropGroupEle.SelectSingleNode("//GroupItem[@groupId='" + productItem.GroupId + "']");
                    XmlElement groupEleOld = (XmlElement)nodeOld;
                    groupEleOld.SetAttribute("isUsed", false.ToString());
                }
                productItem.Clear();
                productItem.GroupName = this.conProductProp.Text;  
                bool isUsed = true;
                groupEle.SetAttribute("isUsed", isUsed.ToString());
                productItem.GroupId = groupEle.GetAttribute("groupId");
                foreach (Control control in this.conProductPropPanel.Controls)
                {
                    if (control is TextBox)
                    { 
                        ProductPropertyData data = new ProductPropertyData();
                        data.Name = control.Tag.ToString();
                        data.Value = control.Text;  
                        productItem.Add(data);    
                    } 
                }
                Service.Sdsite.DesignDataDocument.Save();
            }
            if (Changed != null)
            {
                Changed(null, EventArgs.Empty);
            }

        }
        public event EventHandler SelectChanged;
        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (SelectChanged != null)
            {
                SelectChanged(this, e);
            }
        }

        #endregion
        public static event EventHandler Changed;
    }
}

