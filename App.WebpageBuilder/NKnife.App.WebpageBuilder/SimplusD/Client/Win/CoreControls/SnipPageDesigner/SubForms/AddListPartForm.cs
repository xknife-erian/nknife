using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;
using System.IO;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class AddListPartForm : BaseForm
    {
        public AddListPartForm(bool isAdd,ListPart part)
        {
            InitializeComponent();
            _isAdd = isAdd;
            _editStyleText = this.GetText("editStyleText");
            GetChannelNames();
            InitSortCmbs();
            InitCmbs();
            if (part != null)
            {
                _part = part;
                _sequenceType = part.SequenceType;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
            if (!_isAdd)
            {
                InitControls();
            }
            else
            {
                checkBoxGeneral.Checked = true;
            }
        }
        
        #region 内部变量

        bool _isAdd = false;

        ListPart _part;

        Dictionary<StyleType, ListBoxPart> _listBoxPartsDic = new Dictionary<StyleType, ListBoxPart>();

        SequenceType _sequenceType = SequenceType.None;

        string _keyWords = "";

        List<string> _channelIds = new List<string>();

        string _editStyleText = "编辑/修改样式";

        #endregion

        #region 公共属性



        #endregion

        #region 公共方法

        #endregion

        #region 内部方法

        /// <summary>
        /// 初始化排序方式的下拉框
        /// </summary>
        private void InitSortCmbs()
        {
            cmbSortType.Items.Clear();
            string[] sortType = Enum.GetNames(typeof(SequenceType));
            for (int i = 0; i < sortType.Length; i++)
            {
                sortType[i] = ResourceService.GetEnumResourceText(typeof(SequenceType), sortType[i]);
            }
            cmbSortType.Items.AddRange(sortType);
        }

        /// <summary>
        /// 初始化排序方式选择栏
        /// </summary>
        private void InitCmbs()
        {            
            #region 类型下拉框

            comboBoxGenaral.Items.Clear();
            comboBoxGenaral.Items.AddRange(DesignStyleForm.GetStyles(StyleType.GeneralPageListPart));
            comboBoxGenaral.Items.Add(_editStyleText);

            comboBoxHome.Items.Clear();
            comboBoxHome.Items.AddRange(DesignStyleForm.GetStyles(StyleType.HomePageListPart));
            comboBoxHome.Items.Add(_editStyleText);

            comboBoxHr.Items.Clear();
            comboBoxHr.Items.AddRange(DesignStyleForm.GetStyles(StyleType.HrPageListPart));
            comboBoxHr.Items.Add(_editStyleText);

            comboBoxInviteBidding.Items.Clear();
            comboBoxInviteBidding.Items.AddRange(DesignStyleForm.GetStyles(StyleType.InviteBiddingPageListPart));
            comboBoxInviteBidding.Items.Add(_editStyleText);

            comboBoxKnowledge.Items.Clear();
            comboBoxKnowledge.Items.AddRange(DesignStyleForm.GetStyles(StyleType.KnowledgePageListPart));
            comboBoxKnowledge.Items.Add(_editStyleText);

            comboBoxProject.Items.Clear();
            comboBoxProject.Items.AddRange(DesignStyleForm.GetStyles(StyleType.ProjectPageListPart));
            comboBoxProject.Items.Add(_editStyleText);

            comboBoxProduct.Items.Clear();
            comboBoxProduct.Items.AddRange(DesignStyleForm.GetStyles(StyleType.ProductPageListPart));
            comboBoxProduct.Items.Add(_editStyleText);

            #endregion
        }

        /// <summary>
        /// 初始化所有控件
        /// </summary>
        /// <param name="isAdd">是否是新建</param>
        private void InitControls()
        {
            _listBoxPartsDic.Clear();
            numericUpDownAmount.Value = _part.MaxDisplayAmount;
            if (_part.ChannelIDs.Count > 0)
            {
                _channelIds = _part.ChannelIDs;
                foreach (string channelId in _channelIds)
                {
                    SetCheckedNodes(channelId,treeViewChannels.Nodes);
                }
            }
            else
                return;
            if (_part.ChildParts.Count > 0)
            {
                #region 设置页面类型复选部分
                foreach (SnipPagePart sPart in _part.ChildParts)
                {
                    if (sPart.GetType() == typeof(ListBoxPart))
                    {
                        ListBoxPart lbPart = sPart as ListBoxPart;
                        _listBoxPartsDic[lbPart.StyleType] = lbPart;
                        switch (lbPart.StyleType)
                        { 
                            case StyleType.GeneralPageListPart:
                                checkBoxGeneral.Checked = true;
                                break;
                            case StyleType.ProductPageListPart:
                                checkBoxProduct.Checked = true;
                                break;
                            case StyleType.ProjectPageListPart:
                                checkBoxProject.Checked = true;
                                break;
                            case StyleType.InviteBiddingPageListPart:
                                checkBoxInviteBidding.Checked = true;
                                break;
                            case StyleType.KnowledgePageListPart:
                                checkBoxKnowledge.Checked = true;
                                break;
                            case StyleType.HrPageListPart:
                                checkBoxHr.Checked = true;
                                break;
                            case StyleType.HomePageListPart:
                                checkBoxHomePage.Checked = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
                #endregion
            }
            else
                return;
            cmbSortType.SelectedIndex = (int)(_part.SequenceType);
            if (_part.SequenceType == SequenceType.CustomKeyWord)
            {
                textBoxKeyWords.Text = _part.CustomKeyWords;
            }
        }

        /// <summary>
        /// 设置所有应该被选择上的节点
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="treeNodes"></param>
        private void SetCheckedNodes(string channelId, TreeNodeCollection treeNodes)
        {
            foreach (TreeNode node in treeNodes)
            {
                string tag = (string)node.Tag;
                if (tag == channelId)
                {
                    node.Checked = true;
                }
                else if (node.Nodes.Count > 0)
                {
                    SetCheckedNodes(channelId, node.Nodes);
                }
            }
        }

        /// <summary>
        /// edit by  zhenghao at 2008-06-17 14:40
        /// 编辑样式
        /// </summary>
        /// <param name="type"></param>
        private void EditListBoxPart(PageType type)
        {
            DesignStyleForm form = new DesignStyleForm(StyleType.GeneralPageListPart);
            switch (type)
            {
                case PageType.None:
                    break;
                case PageType.General:
                    break;
                case PageType.Product:
                    form = new DesignStyleForm(StyleType.ProductPageListPart);
                    break;
                case PageType.Project:
                    form = new DesignStyleForm(StyleType.ProjectPageListPart);
                    break;
                case PageType.InviteBidding:
                    form = new DesignStyleForm(StyleType.InviteBiddingPageListPart);
                    break;
                case PageType.Knowledge:
                    form = new DesignStyleForm(StyleType.KnowledgePageListPart);
                    break;
                case PageType.Hr:
                    form = new DesignStyleForm(StyleType.HrPageListPart);
                    break;
                case PageType.Home:
                    form = new DesignStyleForm(StyleType.HomePageListPart);
                    break;
                default:
                    break;
            }
            if (form != null)
            {
                form.Owner = this;
                form.ShowDialog();
            }
            //ListBoxPart lbPart = _listBoxPartsDic[type];
            //EditListBoxForm form = new EditListBoxForm(lbPart);
            //form.ShowDialog();
        }

        /// <summary>
        /// 获得所有的频道并加入到TreeView中
        /// </summary>
        private void GetChannelNames()
        {
            TreeNode rootNode = new TreeNode(Service.Sdsite.CurrentDocument.RootChannel.GetAttribute("title"));
            rootNode.Tag = Service.Sdsite.CurrentDocument.RootChannel.GetAttribute("id");

            rootNode.Nodes.Clear();
            ChannelSimpleExXmlElement[] channelEles = Service.Sdsite.CurrentDocument.RootChannel.GetChildChannels();
            if (channelEles.Length >= 1)
                ExtractTreeNode(rootNode, channelEles);
            this.treeViewChannels.Nodes.Clear();
            this.treeViewChannels.Nodes.AddRange(new TreeNode[] { rootNode });
            this.treeViewChannels.ExpandAll();
        }

        /// <summary>
        /// 根据父节点找到节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodes"></param>
        private void ExtractTreeNode(TreeNode parentNode, ChannelSimpleExXmlElement[] nodes)
        {
            foreach (ChannelSimpleExXmlElement ele in nodes)
            {
                if (!ele.IsDeleted)
                {
                    TreeNode treeNode = new TreeNode(ele.GetAttribute("title"));
                    treeNode.Tag = ele.GetAttribute("id");
                    ChannelSimpleExXmlElement[] _nodes = ele.GetChildChannels();
                    parentNode.Nodes.Add(treeNode);
                    if (_nodes == null || _nodes.Length <= 0)
                    {
                        continue;
                    }
                    ExtractTreeNode(treeNode, _nodes);
                }
            }
        }

        private TreeNode GetNode(TreeNodeCollection nodes, string channelID)
        {
            foreach (TreeNode node in nodes)
            {
                if (channelID == node.Tag.ToString())
                {
                    return node;
                }
                else if (node.Nodes.Count > 0)
                {
                    TreeNode node2 = GetNode(node.Nodes, channelID);
                    if (node2 != null)
                    {
                        return node2;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// edit by zhenghao at 2008-06-19 15:45
        /// 当下拉框的选择项改变时
        /// </summary>
        /// <param name="styleType"></param>
        private void ComboBoxPageType_SelectdIndexChanged(StyleType styleType,ComboBox comboBox,Label lab)
        {
            int _count = comboBox.Items.Count;
            if (_count < 1 || comboBox.SelectedIndex  >= _count)
            {
                return;
            }
            if (comboBox.SelectedIndex == _count - 1)
            {
                DesignStyleForm form = new DesignStyleForm(styleType);
                form.Owner = this;
                if (form.ShowDialog() == DialogResult.OK)
                {

                }
                InitCmbs();
            }
            else
            {
                string styleFileName = comboBox.Text;
                _listBoxPartsDic[styleType].StyleElement = StyleXmlDocument.GetStyleDocument(styleType, styleFileName).StyleElement;
               
            }
        }

        /// <summary>
        /// 添加listbox的子
        /// </summary>
        /// <param name="listBoxPart"></param>
        /// <param name="styleEle"></param>
        private void AddListBoxPartChildrenParts(ListBoxPart listBoxPart, StyleXmlElement styleEle)
        {
            SnipPartsXmlElement partsEle = styleEle.GetPartsElement();
            listBoxPart.ChildParts.Clear();
            GetParts(listBoxPart.ChildParts, partsEle,listBoxPart.Designer);
        }

        private void GetParts(SnipPagePartCollection snipPagePartCollection, XmlElement partsEle,SnipPageDesigner designer)
        {
            XmlNodeList _nodes = partsEle.SelectNodes("part");

            foreach (XmlNode node in _nodes)
            {
                SnipPartXmlElement partEle = (SnipPartXmlElement)node;
                SnipPagePart part = SnipPagePart.Parse(partEle, designer);

                snipPagePartCollection.Add(part);
                GetParts(part.ChildParts, partEle,designer);
            }
        }

        #endregion
        
        #region 事件响应

        private void checkBoxHomePage_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxHome.Enabled = checkBoxHomePage.Checked;
            labelHome.Enabled = checkBoxHomePage.Checked;
            if (checkBoxHomePage.Checked)
            {
                ListBoxPart lbPart = (ListBoxPart)SnipPagePart.Create(_part.Designer, SnipPartType.ListBox);
                lbPart.StyleType = StyleType.HomePageListPart;
                _listBoxPartsDic[StyleType.HomePageListPart] = lbPart;
            }
            else
            {
                _listBoxPartsDic.Remove(StyleType.HomePageListPart);
            }
        }

        private void checkBoxGeneral_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxGenaral.Enabled = checkBoxGeneral.Checked;
            labelGeneral.Enabled = checkBoxGeneral.Checked;
            if (checkBoxGeneral.Checked)
            {
                ListBoxPart lbPart = (ListBoxPart)SnipPagePart.Create(_part.Designer, SnipPartType.ListBox);
                lbPart.StyleType = StyleType.GeneralPageListPart;
                _listBoxPartsDic[StyleType.GeneralPageListPart] = lbPart;
            }
            else
            {
                _listBoxPartsDic.Remove(StyleType.GeneralPageListPart);
            }
        }

        private void checkBoxProduct_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxProduct.Enabled = checkBoxProduct.Checked;
            labelProduct.Enabled = checkBoxProduct.Checked;
            if (checkBoxProduct.Checked)
            {
                ListBoxPart lbPart = (ListBoxPart)SnipPagePart.Create(_part.Designer, SnipPartType.ListBox);
                lbPart.StyleType = StyleType.ProductPageListPart;
                _listBoxPartsDic[StyleType.ProductPageListPart] = lbPart;
            }
            else
            {
                _listBoxPartsDic.Remove(StyleType.ProductPageListPart);
            }
        }

        private void checkBoxKnowledge_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxKnowledge.Enabled = checkBoxKnowledge.Checked;
            labelKnowledge.Enabled = checkBoxKnowledge.Checked;
            if (checkBoxKnowledge.Checked)
            {
                ListBoxPart lbPart = (ListBoxPart)SnipPagePart.Create(_part.Designer, SnipPartType.ListBox);
                lbPart.StyleType = StyleType.KnowledgePageListPart;
                _listBoxPartsDic[StyleType.KnowledgePageListPart] = lbPart;
            }
            else
            {
                _listBoxPartsDic.Remove(StyleType.KnowledgePageListPart);
            }
        }

        private void checkBoxHr_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxHr.Enabled = checkBoxHr.Checked;
            labelHr.Enabled = checkBoxHr.Checked;
            if (checkBoxHr.Checked)
            {
                ListBoxPart lbPart = (ListBoxPart)SnipPagePart.Create(_part.Designer, SnipPartType.ListBox);
                lbPart.StyleType = StyleType.HrPageListPart;
                _listBoxPartsDic[StyleType.HrPageListPart] = lbPart;
            }
            else
            {
                _listBoxPartsDic.Remove(StyleType.HrPageListPart);
            }
        }

        private void checkBoxInviteBidding_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxInviteBidding.Enabled = checkBoxInviteBidding.Checked;
            labelInviteBidding.Enabled = checkBoxInviteBidding.Checked;
            if (checkBoxInviteBidding.Checked)
            {
                ListBoxPart lbPart = (ListBoxPart)SnipPagePart.Create(_part.Designer, SnipPartType.ListBox);
                lbPart.StyleType = StyleType.InviteBiddingPageListPart;
                _listBoxPartsDic[StyleType.InviteBiddingPageListPart] = lbPart;
            }
            else
            {
                _listBoxPartsDic.Remove(StyleType.InviteBiddingPageListPart);
            }
        }

        private void checkBoxProject_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxProject.Enabled = checkBoxProject.Checked;
            labelProject.Enabled = checkBoxProject.Checked;
            if (checkBoxProject.Checked)
            {
                ListBoxPart lbPart = (ListBoxPart)SnipPagePart.Create(_part.Designer, SnipPartType.ListBox);
                lbPart.StyleType = StyleType.ProjectPageListPart;
                _listBoxPartsDic[StyleType.ProjectPageListPart] = lbPart;
            }
            else
            {
                _listBoxPartsDic.Remove(StyleType.ProjectPageListPart);
            }
        }

        private void cmbSortType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbSortType.SelectedIndex)
            {
                case 0:
                    cmbSortType.SelectedIndex = 1;
                    break;
                case 1:
                    _sequenceType = SequenceType.Recent;
                    textBoxKeyWords.Enabled = false;
                    textBoxKeyWords.Text = "";
                    break;
                case 2:
                    _sequenceType = SequenceType.CustomKeyWord;
                    textBoxKeyWords.Enabled = true;
                    textBoxKeyWords.Text = _keyWords;
                    break;
                case 3:
                    _sequenceType = SequenceType.AutoKeyWord;
                    textBoxKeyWords.Enabled = false;
                    textBoxKeyWords.Text = _keyWords;
                    break;
                default:
                    break;
            }
            
        }

        private void textBoxKeyWords_Leave(object sender, EventArgs e)
        {
            _keyWords = textBoxKeyWords.Text;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (_listBoxPartsDic.Count < 1 || _channelIds.Count < 1)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                _part.MaxDisplayAmount = (int)numericUpDownAmount.Value;
                _part.ChannelIDs = _channelIds;
                _part.SequenceType = _sequenceType;
                if (_sequenceType == SequenceType.CustomKeyWord)
                {
                    _part.CustomKeyWords = _keyWords;
                }
                foreach (ListBoxPart lbPart in _listBoxPartsDic.Values)
                {
                    _part.ChildParts.Add(lbPart);
                }

                DialogResult = DialogResult.OK;
            }
        }
        
        private void treeViewChannels_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode _node = e.Node;
            string _channelId = (string)_node.Tag;
            if (_node.Checked)
            {
                _channelIds.Add(_channelId);
            }
            else
            {
                _channelIds.Remove(_channelId);
            }
        }

        private void comboBoxHome_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxPageType_SelectdIndexChanged(StyleType.HomePageListPart,(ComboBox)sender,labelHome);
        }

        private void comboBoxGenaral_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxPageType_SelectdIndexChanged(StyleType.GeneralPageListPart, (ComboBox)sender,labelGeneral);
        }

        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxPageType_SelectdIndexChanged(StyleType.ProductPageListPart, (ComboBox)sender,labelProduct);
        }

        private void comboBoxKnowledge_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxPageType_SelectdIndexChanged(StyleType.KnowledgePageListPart, (ComboBox)sender,labelKnowledge);
        }

        private void comboBoxHr_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxPageType_SelectdIndexChanged(StyleType.HrPageListPart, (ComboBox)sender,labelHr);
        }

        private void comboBoxInviteBidding_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxPageType_SelectdIndexChanged(StyleType.InviteBiddingPageListPart, (ComboBox)sender,labelInviteBidding);
        }

        private void comboBoxProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxPageType_SelectdIndexChanged(StyleType.ProjectPageListPart, (ComboBox)sender,labelProject);
        }

        #endregion

        #region 自定义事件

        #endregion
    }
}
