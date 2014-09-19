using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public class FolderOutsideNode : OutsideNode
    {
        public FolderOutsideNode(string filePath)
            : base(filePath, true)
        {
            this.Text = Path.GetFileName(filePath);
        }

        public override void LoadData()
        {
        }

        protected override void LoadChildNodes()
        {
            ///加载子目录
            string[] folders = Directory.GetDirectories(FilePath, "*", SearchOption.TopDirectoryOnly);
            foreach (var item in folders)
            {
                FolderOutsideNode node = new FolderOutsideNode(item);
                node.LoadData();
            }


            ///加载子文件
            string[] files = Directory.GetFiles(FilePath, "*", SearchOption.TopDirectoryOnly);
            foreach (var item in files)
            {
                FileOutsideNode node = new FileOutsideNode(item);
                node.LoadData();
            }
        }

        public override string CollapseImageKey
        {
            get {
                return "";
            
            }
        }

        public override TreeNodeType NodeType
        {
            get { return TreeNodeType.FolderOutsite; }
        }
    }
}
