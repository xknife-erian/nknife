using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public abstract class OutsideNode : DataNode
    {
        public OutsideNode(string filePath,bool isFolder)
            :base(filePath,isFolder)
        {
            this.Text = Path.GetFileNameWithoutExtension(filePath);
        }
    }
}
