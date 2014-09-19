using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public class FileOutsideNode : OutsideNode
    {
        public FileOutsideNode(string filePath)
            : base(filePath, false)
        {
            this.Text = Path.GetFileNameWithoutExtension(filePath);
        }

        public override void LoadData()
        {
            
        }

        protected override void LoadChildNodes()
        {
            
        }

        public override string CollapseImageKey
        {
            get { return ""; }
        }

        public override TreeNodeType NodeType
        {
            get
            {
                return TreeNodeType.FileOutside;
            }
        }
    }
}
