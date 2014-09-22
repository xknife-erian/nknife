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
    public partial class EditChannelForm : BaseForm
    {
        #region 构造函数

        public EditChannelForm(NavigationPart part)
        {
            InitializeComponent();
            Part = part;
            SelectedChannel = part.ChannelID;
            DisplayType = part.DisplayType;
            ChannelTitle = part.ChannelTitle;
            GetChannelNames();
        }

        #endregion

        #region 内部变量

        string _picSrc = "";

        #endregion

        #region 公共属性

        /// <summary>
        /// edit by zhenghao at 2008-06-17 09:50
        /// 获取导航（频道）块
        /// </summary>
        public NavigationPart Part { get; private set; }

        /// <summary>
        /// edit by zhenghao at 2008-06-17 10:00
        /// 获取导航（频道）块的显示方式
        /// </summary>
        public DisplayType DisplayType { get; private set; }

        /// <summary>
        /// 获取或设置被选定的频道ID
        /// </summary>
        public string SelectedChannel { get; set; }

        /// <summary>
        /// 获取或设置被选定的频道Title
        /// </summary>
        public string ChannelTitle { get; set; }

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
            this.ChannelNamestreeView.Nodes.Clear();
            this.ChannelNamestreeView.Nodes.AddRange(new TreeNode[] { rootNode });
            this.ChannelNamestreeView.ExpandAll();
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
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            if (!string.IsNullOrEmpty(SelectedChannel))
            {
                TreeNode node = GetNode(ChannelNamestreeView.Nodes, SelectedChannel);
                if(node!= null)
                {
                    ChannelNamestreeView.SelectedNode = node;
                    ChannelNamestreeView.Select();
                }
            }
        }

        #endregion

        #region 公共方法

        #endregion

        #region 控件事件

        private void SnipAddSingleChannelForm_Load(object sender, EventArgs e)
        {
            InitControls();
            ChannelNamestreeView.ExpandAll();
        }

        private void enterBtn_Click(object sender, EventArgs e)
        {
            Part.ChannelID = SelectedChannel;
            Part.DisplayType = DisplayType;
            Part.ChannelTitle = ChannelTitle;
            Part.PictrueSrc = _picSrc;
        }

        private void radioButtonDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDefault.Checked)
            {
                buttonBrowsePic.Enabled = false;
                textBoxText.Enabled = false;
                textBoxTitle.Enabled = false;
                DisplayType = DisplayType.Default;
            }
        }

        private void radioButtonTitle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTitle.Checked)
            {
                buttonBrowsePic.Enabled = false;
                textBoxText.Enabled = false;
                DisplayType = DisplayType.Title;
                textBoxTitle.Text = Part.Text;
            }
        }

        private void radioButtonPic_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPic.Checked)
            {
                buttonBrowsePic.Enabled = true;
                textBoxText.Enabled = false;
                DisplayType = DisplayType.Pictrue;
            }
        }

        private void radioButtonText_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonText.Checked)
            {
                buttonBrowsePic.Enabled = false;
                textBoxText.Enabled = true;
                DisplayType = DisplayType.AppointedText;
            }
        }

        private void buttonBrowsePic_Click(object sender, EventArgs e)
        {
            
        }

        private void textBoxText_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxText.Text))
            {
                textBoxText.Text = Part.Text;
                Part.AppointedText = textBoxText.Text;
            }
            else
            {
                Part.AppointedText = textBoxText.Text;
            }
        }

        private void ChannelNamestreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ChannelNamestreeView.SelectedNode != null)
            {
                SelectedChannel = ChannelNamestreeView.SelectedNode.Tag.ToString();
                ChannelTitle = ChannelNamestreeView.SelectedNode.Text;
            }
            else
            {
                SelectedChannel = Part.ChannelID;
                ChannelTitle = Part.ChannelTitle;
            }
        }

        #endregion

    }
}