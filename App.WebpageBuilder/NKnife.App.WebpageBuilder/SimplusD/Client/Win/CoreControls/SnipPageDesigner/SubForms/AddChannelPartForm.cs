using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class AddChannelPartForm : BaseForm
    {
        public AddChannelPartForm(bool isAddNewPart, SnipPageDesigner designer)
        {
            InitializeComponent();
            SnipPageDesigner = designer;
            _isAddNewPart = isAddNewPart;
            Init();
        }

        #region 内部变量

        Dictionary<string, NavigationPart> _navigations = new Dictionary<string, NavigationPart>();

        const string DEFAULTWIDTH = "100px";

        bool _isAddNewPart = false;

        #endregion

        #region 公共属性

        /// <summary>
        /// 获取所属的设计器
        /// </summary>
        public SnipPageDesigner SnipPageDesigner { get; private set; }

        /// <summary>
        /// 获取或设置被选中的频道
        /// </summary>
        public List<KeyValuePair<string,string>> CheckedChannels { get; set; }

        /// <summary>
        /// 获取所有的块（part）
        /// </summary>
        public Dictionary<string,NavigationPart> NavigationParts 
        {
            get { return _navigations; }
        }

        /// <summary>
        /// 获取间隔符块的文本内容
        /// </summary>
        public string SeparatorPartText { get; private set; }

        /// <summary>
        /// 获取是否使用间隔符
        /// </summary>
        public bool IsUsingSeparator { get; private set; }

        /// <summary>
        /// 获取是否使用相同宽度
        /// </summary>
        public bool IsSameWidth { get; private set; }

        /// <summary>
        /// 获取已经设置好的宽度
        /// </summary>
        public string SameWidth { get; private set; }

        #endregion

        #region 公共方法

        #endregion

        #region 内部方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            textBoxSeparator.Text = "|";
            IsUsingSeparator = true;
            cssFieldUnitWidth.Value = DEFAULTWIDTH;
            GetChannelNames();
            treeViewChannels.ExpandAll();
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
                    treeNode.ToolTipText = GetText("treeViewToolTipText");
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

        /// <summary>
        /// 找到所有被选定的频道节点
        /// </summary>
        /// <returns></returns>
        private List<TreeNode> GetCheckedNodes()
        {
            if (treeViewChannels.Nodes.Count >= 1)
            {
                List<TreeNode> treeNodes = new List<TreeNode>();
                GetCheckNodes(treeNodes, treeViewChannels.Nodes);
                return treeNodes;
            }
            else
                return null;
        }

        /// <summary>
        /// 递归查找指定节点下被选定的频道节点
        /// </summary>
        /// <param name="nodes">找到的节点放在这里</param>
        /// <param name="treeNodes">指定的节点集</param>
        private void GetCheckNodes(List<TreeNode> nodes, TreeNodeCollection treeNodes)
        {
            foreach (TreeNode node in treeNodes)
            {
                if (node.Checked)
                {
                    nodes.Add(node);
                    CheckedChannels.Add(new KeyValuePair<string, string>((string)node.Tag, node.Text));
                }
                if (node.Nodes.Count > 0)
                {
                    GetCheckNodes(nodes, node.Nodes);
                }
            }
        }

        #endregion

        #region 事件响应
        
        private void usedSeparator_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUsedSeparator.Checked)
            {
                IsUsingSeparator = true;
                textBoxSeparator.Enabled = true;
            }
            else
            {
                IsUsingSeparator = false;
                textBoxSeparator.Enabled = false;
            }
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            
        }

        private void treeViewChannels_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag == null)
            {
                return;
            }
            string channelId = e.Node.Tag.ToString();
            if (string.IsNullOrEmpty(channelId))
            {
                return;
            }
            if (e.Node.Checked)
            {
                NavigationPart nvPart = _navigations[channelId];

                PartSetupForm form = new PartSetupForm(nvPart);
                form.Owner = this;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.WidthChanged)
                    {
                        checkBoxWidth.Checked = false;
                    }
                }
            }
        }
        
        private void textBoxSeparator_TextChanged(object sender, EventArgs e)
        {
            SeparatorPartText = textBoxSeparator.Text;
        }

        private void checkBoxWidth_CheckedChanged(object sender, EventArgs e)
        {
            IsSameWidth = checkBoxWidth.Checked;
            cssFieldUnitWidth.Enabled = IsSameWidth;
            if (IsSameWidth)
            {
                SameWidth = cssFieldUnitWidth.Value;
            }
        }

        /// <summary>
        /// 改变节点选择状态后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewChannels_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            string channelId = node.Tag.ToString();
            if (node.Checked)
            {
                NavigationPart nvPart = (NavigationPart)SnipPagePart.Create(SnipPageDesigner, SnipPartType.Navigation);
                nvPart.ChannelID = channelId;
                _navigations[channelId] = nvPart;
                nvPart.Width_Css = DEFAULTWIDTH;
            }
            else
            {
                _navigations.Remove(channelId);
            }
            if (_navigations.Values.Count <= 1)
            {
                checkBoxUsedSeparator.Checked = false;
                checkBoxUsedSeparator.Enabled = false;
            }
            else
                checkBoxUsedSeparator.Enabled = true;
        }

        private void cssFieldUnitWidth_ValueChanged(object sender, EventArgs e)
        {
            SameWidth = cssFieldUnitWidth.Value;
        }

        #endregion

        #region 自定义事件

        #endregion
    }
}
