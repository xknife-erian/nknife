using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class SelectChannelsForm : Form
    {
        #region 构造函数

        public SelectChannelsForm()
        {
            InitializeComponent();
            ResourcesReader.SetControlPropertyHelper(this);
            GetChannelNames();
            SetCheckedNodes(Channels, treeChannels.Nodes);
        }

        #endregion

        #region 公共属性

        public List<KeyValuePair<string, string>> Channels { get; set; }

        #endregion

        #region 内部方法

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
            this.treeChannels.Nodes.Clear();
            this.treeChannels.Nodes.AddRange(new TreeNode[] { rootNode });
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
        /// 递归查找指定节点下被选定的频道节点
        /// </summary>
        /// <param name="channels">找到的节点放在这里</param>
        /// <param name="treeNodes">指定的节点集</param>
        private void GetCheckedNodes(List<KeyValuePair<string,string>> channels, TreeNodeCollection treeNodes)
        {
            
            foreach (TreeNode node in treeNodes)
            {
                if (node.Checked)
                {
                    channels.Add(new KeyValuePair<string, string>(node.Text, (string)node.Tag));
                }
                if (node.Nodes.Count > 0)
                {
                    GetCheckedNodes(channels, node.Nodes);
                }
            }
        }

        /// <summary>
        /// 设置已经选定的频道
        /// </summary>
        /// <param name="channels"></param>
        /// <param name="treeNodes"></param>
        private void SetCheckedNodes(List<KeyValuePair<string, string>> channels, TreeNodeCollection treeNodes)
        {
            if (channels == null)
            {
                return;
            }
            foreach (KeyValuePair<string,string> channel in channels)
            {
                foreach (TreeNode node in treeChannels.Nodes)
                {
                    if (node.Text == channel.Key && channel.Value == (string)node.Tag)
                    {
                        node.Checked = true;
                        break;
                    }
                    if (node.Nodes.Count>0)
                    {
                        SetCheckedNodes(channels,node.Nodes);
                    }
                }
            }
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            Channels = new List<KeyValuePair<string, string>>();
            GetCheckedNodes(Channels, treeChannels.Nodes);
        }

    }
}
