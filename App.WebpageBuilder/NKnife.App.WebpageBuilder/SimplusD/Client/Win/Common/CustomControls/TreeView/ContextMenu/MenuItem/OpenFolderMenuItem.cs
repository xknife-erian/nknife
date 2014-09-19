using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public class OpenFolderMenuItem : BaseTreeMenuItem
    {
        public OpenFolderMenuItem(MyTreeView treeView)
            :base(treeView)
        {
        }

        private string strFileMenuText = "打开所在的文件夹";
        private string strFolderMenuText = "在资源管理器打开文件夹";

        protected override void OnClick(EventArgs e)
        {
            ///先判断选择的数量，等于1则继续处理
            if (TreeView.SelectedNodes.Count == 1)
            {
                BaseTreeNode selectedNode = TreeView.SelectedNodes[0] as BaseTreeNode;

                ///判断节点是否是DataNode节点，是DataNode节点则继续处理
                if (selectedNode is DataNode)
                {
                    DataNode dataNode = selectedNode as DataNode;

                    ///文件夹，打开文件夹
                    if (dataNode.IsFolder)
                    {
                        //Process.Start(dataNode.FilePath);
                        Process.Start(@"explorer", @"/e,""" + dataNode.FilePath + @"""");
                    }
                    ///文件，打开所在的文件夹
                    else
                    {
                        Process.Start(@"explorer", @"/select,""" + dataNode.FilePath + @"""");
                    }
                }
            }

            base.OnClick(e);
        }

        public override void MenuOpening()
        {
            ///先判断选择的数量，不等于1则不显示
            if (TreeView.SelectedNodes.Count == 1)
            {
                BaseTreeNode selectedNode = TreeView.SelectedNodes[0] as BaseTreeNode;

                ///判断节点是否是DataNode节点，不是则不显示
                if (selectedNode is DataNode)
                {
                    DataNode dataNode = selectedNode as DataNode;

                    ///文件夹，打开文件夹
                    if (dataNode.IsFolder)
                    {
                        this.Text = strFolderMenuText;
                    }
                    ///文件，打开所在的文件夹
                    else
                    {
                        this.Text = strFileMenuText;
                    }
                    this.Visible = true;
                }
                else
                {
                    this.Visible = false;
                }
            }
            else
            {
                this.Visible = false;
            }
        }
    }
}
