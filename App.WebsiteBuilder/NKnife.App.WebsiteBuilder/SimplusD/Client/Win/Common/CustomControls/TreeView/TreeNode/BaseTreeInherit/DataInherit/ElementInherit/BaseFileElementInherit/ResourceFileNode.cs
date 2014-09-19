using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    [TreeNode(CanDragDrop = true, CanRename = true, IsBranch = false,CanDelete = true,
    AcceptDragDropType = TreeNodeType.None)]
    public class ResourceFileNode : BaseFileElementNode
    {
        const string NoExtension = "?noextension";

        public ResourceFileNode(FileSimpleExXmlElement element)
            :base(element)
        {
        }

        public new FileSimpleExXmlElement Element
        {
            get
            {
                return base.Element as FileSimpleExXmlElement;
            }
        }

        public override void LoadData()
        {
            InsureIcon();

            base.LoadData();
        }

        protected override void LoadChildNodes()
        {
            
        }

        public override string CollapseImageKey
        {
            get
            {
                string extension = Path.GetExtension(Element.RelativeFilePath);
                if (string.IsNullOrEmpty(extension))
                {
                    return NoExtension;
                }
                else
                    return extension;
            }
        }

        public override TreeNodeType NodeType
        {
            get { return  TreeNodeType.ResourceFile; }
        }

        public void InsureIcon()
        {
            if (!this.TreeView.ImageList.Images.ContainsKey(CollapseImageKey))
            {
                Image img = GetImageIndex(CollapseImageKey);
                ResourceService.MainImageList.Images.Add(CollapseImageKey, img);
            }
        }

        #region 获得系统图标
        static Dictionary<string, Image> _dicSystemFileIcon = new Dictionary<string, Image>();
        /// <summary>
        /// 通过扩展名获得系统的图标
        /// </summary>
        static Image GetImageIndex(string extensionName)
        {
            //无扩展名时特殊处理
            if (string.IsNullOrEmpty(extensionName))
            {
                string strSign = NoExtension;

                if (_dicSystemFileIcon.ContainsKey(strSign))
                {
                    return _dicSystemFileIcon[strSign];
                }
                else
                {
                    Icon ico = Service.Icon.GetSystemIcon(strSign, GetSystemIconType.ExtensionSmall);
                    if (ico != null)
                    {
                        Image img = ico.ToBitmap();
                        _dicSystemFileIcon.Add(strSign, img);
                        return img;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                if (_dicSystemFileIcon.ContainsKey(extensionName))
                {
                    return _dicSystemFileIcon[extensionName];
                }
                else
                {
                    Icon ico = Service.Icon.GetSystemIcon(extensionName, GetSystemIconType.ExtensionSmall);
                    if (ico != null)
                    {
                        Image img = ico.ToBitmap();
                        _dicSystemFileIcon.Add(extensionName, img);
                        return img;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        #endregion
    }
}
