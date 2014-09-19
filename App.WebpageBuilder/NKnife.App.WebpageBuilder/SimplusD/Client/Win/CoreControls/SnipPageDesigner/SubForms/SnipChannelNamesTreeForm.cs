using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using System.IO;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class SnipChannelNamesTreeForm : BaseForm
    {
        #region 属性

        /// <summary>
        /// 获取所有被选择的频道节点
        /// </summary>
        public List<TreeNode> CheckedNodes { get; private set; }

       
        /// <summary>
        /// 获取是否使用间隔符
        /// </summary>
        public bool IsUsedSeparator 
        {
            get { return UsedSeparator.Checked; }
            set { UsedSeparator.Checked = value; }
        }
        /// <summary>
        /// 获取频道链接的宽度
        /// </summary>
        public string ChannelLinkWidth { get; set; }

        /// <summary>
        /// 获取间隔符
        /// </summary>
        public string Separator
        {
            get { return textBoxDefaultSeparator.Text; }
            set { textBoxDefaultSeparator.Text = value; }
        }

        public List<KeyValuePair<string, string>> CheckedChannels { get; set; }

        #endregion

        #region 构造函数

        public SnipChannelNamesTreeForm()
        {
            InitializeComponent();
            CheckedNodes = new List<TreeNode>();
            Separator = "|";
            ChannelLinkWidth = "60px";
            IsUsedSeparator = true;
            CheckedChannels = new List<KeyValuePair<string, string>>();
            if (!Service.Util.DesignMode)
            {
                this.comboBoxSizeUnit.Items.AddRange(new string[]{
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.px}"),
            //StringParserService.Parse("${res:TextpropertyPanel.sizeunit.pt}"),
            //StringParserService.Parse("${res:TextpropertyPanel.sizeunit.in}"),
            //StringParserService.Parse("${res:TextpropertyPanel.sizeunit.cm}"),
            //StringParserService.Parse("${res:TextpropertyPanel.sizeunit.mm}"),
            //StringParserService.Parse("${res:TextpropertyPanel.sizeunit.pc}"),
            //StringParserService.Parse("${res:TextpropertyPanel.sizeunit.em}"),
            //StringParserService.Parse("${res:TextpropertyPanel.sizeunit.ex}"),
            StringParserService.Parse("${res:TextpropertyPanel.sizeunit.per}")});
                comboBoxSizeUnit.SelectedIndex = 0;
            }

            GetChannelNames();
        }

        #endregion

        #region 内部函数

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
            this.ChannelNamestreeView.Nodes.Clear();
            this.ChannelNamestreeView.Nodes.AddRange(new TreeNode[] { rootNode });
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

        /// <summary>
        /// 找到所有被选定的频道节点
        /// </summary>
        /// <returns></returns>
        private List<TreeNode> GetCheckedNodes()
        {
            if (ChannelNamestreeView.Nodes.Count >= 1)
            {
                List<TreeNode> treeNodes = new List<TreeNode>();
                GetCheckNodes(treeNodes, ChannelNamestreeView.Nodes);
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


        private void SetCheckedNodes(KeyValuePair<string, string> kv, TreeNodeCollection treeNodes)
        {
            foreach (TreeNode node in treeNodes)
            {
                string tag = (string)node.Tag;
                if (tag == kv.Key)
                {
                    node.Checked = true;
                }
                else if (node.Nodes.Count > 0)
                {
                    SetCheckedNodes(kv, node.Nodes);
                }
            }
        }

        #endregion

        #region 控件事件

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void enterBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            CheckedNodes = GetCheckedNodes();
            string sizeUint = "px";
            switch (comboBoxSizeUnit.SelectedIndex)
            {
                case 0:
                    sizeUint = "px";
                    break;
                case 1:
                //    sizeUint = "pt";
                //    break;
                //case 2:
                //    sizeUint = "in";
                //    break;
                //case 3:
                //    sizeUint = "cm";
                //    break;
                //case 4:
                //    sizeUint = "mm";
                //    break;
                //case 5:
                //    sizeUint = "pc";
                //    break;
                //case 6:
                //    sizeUint = "em";
                //    break;
                //case 7:
                //    sizeUint = "ex";
                //    break;
                //case 8:
                    sizeUint = @"%";
                    break;
                default:
                    break;
            }
            if (!String.IsNullOrEmpty(textBoxWidth.Text))
                ChannelLinkWidth = textBoxWidth.Text + sizeUint;
            else
                ChannelLinkWidth = "80px";
            IsUsedSeparator = UsedSeparator.Checked;

            if (UsedSeparator.Checked)
            {
                Separator = textBoxDefaultSeparator.Text;
            }
            this.Close();
        }

        private void UsedSeparator_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDefaultSeparator.Enabled = UsedSeparator.Checked;
        }

        private void SnipChannelNamesTreeForm_Load(object sender, EventArgs e)
        {
            InitControls();
            ChannelNamestreeView.ExpandAll();
        }

        private void InitControls()
        {
            if (CheckedChannels != null)
            {
                foreach (KeyValuePair<string,string> item in CheckedChannels)
                {
                    SetCheckedNodes(item, ChannelNamestreeView.Nodes);
                }
            }
            if (string.IsNullOrEmpty(ChannelLinkWidth))
            {
                textBoxWidth.Text = "60";
                comboBoxSizeUnit.Text = "px";
            }
            else
            {
                KeyValuePair<string, string> kv = Utility.Convert.SizeToUintAndInt(ChannelLinkWidth);
                textBoxWidth.Text = kv.Value;
                comboBoxSizeUnit.Text = kv.Key;
            }
        }

        #endregion
    }
}