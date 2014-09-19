using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public abstract class ElementNode : DataNode
    {
        public ElementNode(SimpleExIndexXmlElement element, bool isFolder)
            :base(element.AbsoluteFilePath,isFolder)
        {
            this.Element = element;
        }

        public override void LoadData()
        {
            this.Text = this.Element.Title;

            LoadChildNodes();
        }

        public SimpleExIndexXmlElement Element { get; protected set; }

        /// <summary>
        /// 获取此节点的状态。(3种状态:未上传、已上传且未修改、已上传且已修改)
        /// </summary>
        public ServerState ServerState
        {
            get
            {
                if (!Element.IsAlreadyPublished)
                {
                    return ServerState.InClientOnly;
                }
                if (Element.IsModified)
                {
                    return ServerState.InServerNoModified;
                }
                return ServerState.InServerHasModified;
            }
        }
    }
}
