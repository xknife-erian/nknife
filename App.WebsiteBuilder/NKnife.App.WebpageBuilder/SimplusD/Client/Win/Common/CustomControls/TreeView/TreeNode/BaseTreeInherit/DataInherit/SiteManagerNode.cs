using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = false, CanRename = false, IsBranch = true)]
    public class SiteManagerNode : DataNode
    {
        public SiteManagerNode(string filePath)
            :base(filePath,true)
        { 
        }

        public FavoriteRootNode FavoriteRootNode { get; private set; }

        public RootChannelNode RootChannelNode { get; private set; }

        public override void LoadData()
        {
            this.Text = Path.GetFileNameWithoutExtension(Service.Sdsite.CurrentDocument.AbsoluteFilePath);
            LoadChildNodes();
        }

        protected override void LoadChildNodes()
        {
            if (TreeView.TreeMode == TreeMode.General)
            {
                FavoriteRootNode = new FavoriteRootNode();
                this.AddChildNode(FavoriteRootNode);
                FavoriteRootNode.LoadData();
            }

            RootChannelNode = new RootChannelNode(Service.Sdsite.CurrentDocument.RootChannel);
            this.AddChildNode(RootChannelNode);
            RootChannelNode.LoadData();
        }

        public override string CollapseImageKey
        {
            get { return "MainMenu.view.siteManager"; }
        }

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.SiteManager;
            }
        }
    }
}
