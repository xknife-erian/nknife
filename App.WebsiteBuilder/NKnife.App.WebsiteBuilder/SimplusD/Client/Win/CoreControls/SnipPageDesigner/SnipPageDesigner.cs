using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Xml;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public class SnipPageDesigner : BaseControl, IPartContainer,INavigatable
    {
        #region 属性和字段

        /// <summary>
        /// 每个块的高度（设计显示时）
        /// </summary>
        public const int PartHeight = 20;

       // public const int PartSeparator = 5;

        /// <summary>
        /// 右键菜单“选择”项
        /// </summary>
        ToolStripMenuItem _itemSelect;
        /// <summary>
        /// 右键菜单“取消选择”项
        /// </summary>
        ToolStripMenuItem _unSelectItem;
        /// <summary>
        /// 右键菜单“删除”项
        /// </summary>
        ToolStripMenuItem _deleteItem;
        /// <summary>
        /// 右键菜单“复制”项
        /// </summary>
        ToolStripMenuItem _copyItem; 
        /// <summary>
        /// 右键菜单“粘贴”项
        /// </summary>
        ToolStripMenuItem _affixItem;
        ///// <summary>
        ///// 右键菜单“粘贴到内部”项
        ///// </summary>
        //ToolStripMenuItem _affixInPartItem;
        /// <summary>
        /// 右键菜单“编辑”项
        /// </summary>
        ToolStripMenuItem _editItem;
        /// <summary>
        /// 右键菜单“编辑链接属性”项
        /// </summary>
        ToolStripMenuItem _editLinkItem;
        /// <summary>
        /// 右键菜单“Css设置”项
        /// </summary>
        ToolStripMenuItem _cssSetupItem;

        ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private bool _dragDropForNew;
        private ClassPartContainer _classPartContainer;

        bool _isMouseDown;
        Point _prevMouseLocation;
        bool _isDrawDropingPart;
        SnipPagePart _dragDropingStartPart;
        bool _isCasingSelect;
        Point _mouseStartLocation;

        SnipXmlElement _snipElementForPreview;
        /// <summary>
        /// 标识是否调用了BeginUpdata，若为true，则忽略重新布局消息，在EndUpdate的时候强制布局
        /// </summary>
        internal bool _isUpdateData;

        /// <summary>
        /// 是否已修改
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// 获取所有被选择的部分
        /// </summary>
        public SelectedSnipPagePartCollection SelectedParts { get; private set; }

        /// <summary>
        /// 获取或设置是否有自动关键自排序
        /// </summary>
        public bool HasAutoKeyWordsSequenceType { get; set; }
        /// <summary>
        /// 获取或设置设计器底面的颜色
        /// </summary>
        public Color DesignerBackColor 
        {
            get { return BackColor; }
            set { BackColor = value; }
        }

        private bool _isStyleDesigner = false;


        #endregion

        #region 构造函数

        public SnipPageDesigner(bool hasAutoKeyWordsSequenceType)
        {            
            ///初始化类成员
            ChildParts = new SnipPagePartCollection(this);
            SelectedParts = new SelectedSnipPagePartCollection(this);
            CmdManager = new CommandManager(this);
            this.DesignerBackColor = SoftwareOption.SnipDesigner.DesignerBackColor;
            this.ContextMenuStrip = _contextMenu;
            _contextMenu.ShowImageMargin = false;
            this._classPartContainer = new ClassPartContainer(this);

            HasAutoKeyWordsSequenceType = hasAutoKeyWordsSequenceType;

            ///设置控件可获取焦点
            SetStyle(ControlStyles.Selectable, true);
            ///设置控件为双缓冲
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            /// 设计期间的右键菜单
            _contextMenu.Opened += new EventHandler(_contextMenu_DropDownOpening);
            //添加“选择”项
            _itemSelect = new ToolStripMenuItem(GetText("selectText"));
            _contextMenu.Items.Add(_itemSelect);
            //添加“取消选择”项
            _unSelectItem = new ToolStripMenuItem(GetText("unSelectText"), null, UnSelectPart);
            _contextMenu.Items.Add(_unSelectItem);
            //添加 间隔符
            _contextMenu.Items.Add(new ToolStripSeparator());
            //添加“复制”项            
            _copyItem = new ToolStripMenuItem(GetText("copyText"),null,CopyPart);
            _contextMenu.Items.Add(_copyItem);
            //添加“粘贴”项
            _affixItem = new ToolStripMenuItem(GetText("affixText"),null,AffixPart);
            _contextMenu.Items.Add(_affixItem);
            ////添加“粘贴”项
            //_affixItem = new ToolStripMenuItem(GetText("affixText"), null, AffixPart);
            //_contextMenu.Items.Add(_affixItem);
            //添加“删除”项
            _deleteItem = new ToolStripMenuItem(GetText("deleteText"), null, DelectPart);
            _contextMenu.Items.Add(_deleteItem);
            //添加 间隔符
            _contextMenu.Items.Add(new ToolStripSeparator());
            //添加“编辑”项
            _editItem = new ToolStripMenuItem(GetText("editText"), null, EditPart);
            _contextMenu.Items.Add(_editItem);
            //添加“编辑链接属性”
            _editLinkItem = new ToolStripMenuItem(GetText("editLinkText"), null, EditLink);
            _contextMenu.Items.Add(_editLinkItem);
            //添加“Css设置”项
            _cssSetupItem = new ToolStripMenuItem(GetText("cssSetupText"), null, CssSetupPart);
            _contextMenu.Items.Add(_cssSetupItem);


        }

        #endregion

        #region 右键菜单的事件

        /// <summary>
        /// 右键菜单的Opening事件
        /// </summary>
        void _contextMenu_DropDownOpening(object sender, EventArgs e)
        {
            #region 设置右键菜单中“编辑...”和“Css...”选项的状态

            if (SelectedParts.Count != 1)
            {
                _cssSetupItem.Enabled = false;
                _editItem.Enabled = false;
            }
            else
            {
                SnipPagePart part = SelectedParts[0];
                if (part.PartType == SnipPartType.Attribute || part.PartType == SnipPartType.ListBox 
                     || part.PartType == SnipPartType.None )
                {
                    _editItem.Enabled = false;
                }
                else
                    _editItem.Enabled = true;
                _cssSetupItem.Enabled = true;
            }

            #endregion

            ///更新菜单状态
            bool isSelectedPart = this.SelectedParts.Count > 0;
            _copyItem.Enabled = isSelectedPart;
            _deleteItem.Enabled = isSelectedPart;
            _unSelectItem.Enabled = isSelectedPart;

            ///找到鼠标处所指示的点，添加“选择”下面的子菜单项
            CmdManager.BeginBatchCommand();
            _itemSelect.DropDownItems.Clear();
            SnipPagePart[] parts = GetPartsAt(this.PointToClient(Control.MousePosition));

            ///鼠标点击的点上有Part，则添加
            if (parts.Length > 0)
            {
                foreach (SnipPagePart part in parts)
                {
                    SnipPagePart tempPart = part;
                    ToolStripMenuItem partMenuItem = new ToolStripMenuItem(part.Title);
                    partMenuItem.Click += delegate
                    {
                        bool isAdd = (Control.ModifierKeys == Keys.Control);
                        SelectPart(tempPart, isAdd);
                    };
                    _itemSelect.DropDownItems.Add(partMenuItem);
                }
            }
            ///鼠标点击的点上没有Part，则显示一个“空”
            else
            {
                ToolStripMenuItem emptyMenuItem = new ToolStripMenuItem("空");
                emptyMenuItem.Enabled = false;
                _itemSelect.DropDownItems.Add(emptyMenuItem);                
            }
            CmdManager.EndBatchCommand();
        }

        /// <summary>
        /// 删除选定的部分
        /// </summary>
        /// <param name="sendrer"></param>
        /// <param name="e"></param>
        void DelectPart(object sendrer, EventArgs e)
        {
            DeleteSelectePart();
            if (!_isStyleDesigner)
                OnDesignerReseted(EventArgs.Empty);
        }

        /// <summary>
        /// 取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UnSelectPart(object sender, EventArgs e)
        {
            this.SelectedParts.Clear();
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sendrer"></param>
        /// <param name="e"></param>
        void AffixPart(object sendrer, EventArgs e)
        {
            AffixSelectPart();
            if (!_isStyleDesigner)
                OnDesignerReseted(EventArgs.Empty);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sendrer"></param>
        /// <param name="e"></param>
        void CopyPart(object sendrer, EventArgs e)
        {
            CopySelectPart();
        }

        /// <summary>
        /// 编辑选定的部分
        /// </summary>
        /// <param name="sendrer"></param>
        /// <param name="e"></param>
        void EditPart(object sendrer, EventArgs e)
        {
            if (SelectedParts.Count != 1)
                return;
            SnipPagePart part = SelectedParts[0];
            EditPart(part, false);
            if (!_isStyleDesigner)
                OnDesignerReseted(EventArgs.Empty);
        }

        /// <summary>
        /// 编辑链接属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EditLink(object sender, EventArgs e)
        {
            if (SelectedParts.Count != 1)
                return;
            SnipPagePart part = SelectedParts[0];

            XmlElement xmlElement = part.AElement;
            if (xmlElement == null)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlElement = xmlDoc.CreateElement("a");
            }
            LinkManagerForm form = new LinkManagerForm(xmlElement);

            form.Owner = this.FindForm();

            form.ShowDialog();
           
        }

        /// <summary>
        /// 选定部分的Css设置
        /// </summary>
        /// <param name="sendrer"></param>
        /// <param name="e"></param>
        void CssSetupPart(object sendrer, EventArgs e)
        {
            BeginBatchCommand();
            if (SelectedParts.Count != 1)
                return;
            SnipPagePart part = SelectedParts[0];
            if (part != null)
            {
                CssSettingForm cssSettingForm = new CssSettingForm(part.Css);
                if (cssSettingForm.ShowDialog() == DialogResult.OK)
                {
                    part.Css = cssSettingForm.CssText;
                    if (!_isStyleDesigner)
                        OnDesignerReseted(EventArgs.Empty);
                }
            }
            
            EndBatchCommand();
        }

        #endregion

        #region 内部函数

        /// <summary>
        /// 通过树节点的集合得到对应的ID[]
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<string> GetChannelIDList(List<TreeNode> list)
        {
            List<string> strs = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                strs.Add(list[i].Tag.ToString());
            }
            return strs;
        }

        /// <summary>
        /// 将element里保存的数据读取到parts(递归)
        /// </summary>
        private void GetParts(SnipPagePartCollection parts, XmlElement element)
        {
            XmlNodeList _nodes = element.SelectNodes("part");

            foreach (XmlNode node in _nodes)
            {
                SnipPartXmlElement partEle = (SnipPartXmlElement)node;
                SnipPagePart part = SnipPagePart.Parse(partEle, this);

                parts.Add(part);
                GetParts(part.ChildParts, partEle);
            }
        }

        /// <summary>
        /// 将childParts集合保存到xmlElement(递归)
        /// </summary>
        private void SaveParts(SnipXmlElement snipElement,XmlElement xmlElement, SnipPagePartCollection childParts)
        {
            foreach (SnipPagePart part in childParts)
            {
                SnipPartXmlElement partEle = GetPartElement(snipElement,part);
                if (part.ChildParts.Count > 0)
                {
                    SaveParts(snipElement,partEle, part.ChildParts);
                }
                xmlElement.AppendChild(partEle);
            }
        }
        /// <summary>
        /// 将childParts集合保存到xmlElement(递归),保存样式时
        /// </summary>
        private void SaveParts(StyleXmlElement styleElement, XmlElement xmlElement, SnipPagePartCollection childParts)
        {
            foreach (SnipPagePart part in childParts)
            {
                SnipPartXmlElement partEle = GetPartElement(styleElement, part);
                if (part.ChildParts.Count > 0)
                {
                    SaveParts(styleElement, partEle, part.ChildParts);
                }
                xmlElement.AppendChild(partEle);
            }
        }        

        /// <summary>
        /// 将输入的part转成Element
        /// </summary>
        private SnipPartXmlElement GetPartElement(SnipXmlElement snipElement, SnipPagePart part)
        {
            SnipPartXmlElement partEle = snipElement.CreatePart();
            partEle.SnipPartType = part.PartType;
            partEle.SnipPartId = part.PartID;
            partEle.SnipPartCss = part.Css;
            partEle.CustomId = part.CustomID;
            if (part.AElement != null)
            {
                partEle.AppendChild(part.AElement);
            }            
            switch (part.PartType)
            {
                case SnipPartType.None:
                    break;
                case SnipPartType.Static:
                    partEle.CDataValue = ((StaticPart)part).PageText;
                    break;
                case SnipPartType.List:
                    ListPart listPart = (ListPart)part;
                    partEle.ListChannelIDs = listPart.ChannelIDs;
                    partEle.MaxListDisplayAmount = listPart.MaxDisplayAmount;
                    partEle.SequenceType = listPart.SequenceType;
                    break;
                case SnipPartType.ListBox:
                    #region
                    ListBoxPart listBoxPart = (ListBoxPart)part;
                    partEle.StyleType = listBoxPart.StyleType;
                    #endregion
                    break;
                case SnipPartType.Navigation:
                    NavigationPart nPart = (NavigationPart)part;
                    partEle.CDataValue = nPart.PageText;
                    partEle.SetAttribute("channelId", nPart.ChannelID);
                    break;
                case SnipPartType.Attribute:
                    AttributePart attPart = (AttributePart)part;
                    partEle.AttributeName = attPart.AttributeName;
                    partEle.CDataValue = attPart.PageText;
                    break;
                case SnipPartType.Path:
                    #region
                    {
                        PathPart pathPart = (PathPart)part;
                        partEle.SetAttribute("linkDisplayType", pathPart.LinkDisplayType.ToString());
                        partEle.SetAttribute("separatorCode", pathPart.SeparatorCode);
                        break;
                    }
                    #endregion
                case SnipPartType.Box:
                    #region
                    {
                        break;
                    }
                    #endregion
                default:
                    Debug.Fail("未知的SnipPartType:" + part.PartType);
                    break;
            }
            return partEle;
        }
        /// <summary>
        /// 将输入的part转成Element:编辑样式时
        /// </summary>
        private SnipPartXmlElement GetPartElement(StyleXmlElement styleElement, SnipPagePart part)
        {
            SnipPartXmlElement partEle = styleElement.CreatePart();
            partEle.SetAttribute("type", part.PartType.ToString());
            partEle.SetAttribute("partId", part.PartID);
            partEle.SetAttribute("css", part.Css);
            switch (part.PartType)
            {
                case SnipPartType.None:
                    break;
                case SnipPartType.Static:
                    partEle.CDataValue = ((StaticPart)part).PageText;
                    break;                
                case SnipPartType.Box:
                    #region
                    {
                        break;
                    }
                    #endregion
                case SnipPartType.Attribute:
                    AttributePart attPart = (AttributePart)part;
                    partEle.AttributeName = attPart.AttributeName;
                    partEle.CDataValue = attPart.PageText;
                    break;
                default:
                    Debug.Fail("未知的SnipPartType:" + part.PartType);
                    break;
            }
            return partEle;
        }

        /// <summary>
        /// 选中指定part
        /// </summary>
        /// <param name="part">选中此part</param>
        /// <param name="isAdd">指示是否是加选。</param>
        void SelectPart(SnipPagePart part, bool isAdd)
        {
            if (isAdd)
            {
                if (part != null)
                {
                    if (!this.SelectedParts.Contains(part))
                    {
                        this.SelectedParts.Add(part);
                    }
                    else
                    {
                        this.SelectedParts.Remove(part);
                    }
                }
            }
            else
            {
                if (part == null || !part.Selected)
                {
                    ///取消以前的选择项:开始批量执行Command
                    this.Designer.CmdManager.BeginBatchCommand();

                    int index = 0;
                    while (SelectedParts.Count > index)
                    {
                        SnipPagePart selectedPart = SelectedParts[index];
                        if (selectedPart == part)
                        {
                            index = 1;
                        }
                        else
                        {
                            this.Designer.CmdManager.AddExecUnSelectPartCommand(selectedPart);
                        }
                    }

                    ///选择鼠标点击项
                    if (part != null)
                    {
                        if (!part.Selected)
                        {
                            this.Designer.CmdManager.AddExecSelectPartCommand(part);
                        }
                    }

                    ///结束批量执行Command
                    this.Designer.CmdManager.EndBatchCommand();
                }
            }
        }

        /// <summary>
        /// 程序选中指定part add by 2008-06-13
        /// </summary>
        /// <param name="part">选中此part</param>
        /// <param name="isAdd">指示是否是加选。</param>
        public void SelectPartAndCheck(SnipPagePart part)
        {
            if (part == null || !part.Selected)
            {
                ///取消以前的选择项:开始批量执行Command
                this.Designer.CmdManager.BeginBatchCommand();

                int index = 0;
                while (SelectedParts.Count > index)
                {
                    SnipPagePart selectedPart = SelectedParts[index];
                    if (selectedPart == part)
                    {
                        index = 1;
                    }
                    else
                    {
                        this.Designer.CmdManager.AddExecUnSelectPartCommand(selectedPart);
                    }
                }

                ///选择鼠标点击项
                if (part != null)
                {
                    if (!part.Selected)
                    {
                        this.Designer.CmdManager.AddExecSelectPartCommand(part);
                    }
                }

                ///结束批量执行Command
                this.Designer.CmdManager.EndBatchCommand();

                //触发PART单击事件
                if (part != null)
                {
                    part.OnClick(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 选中批量parts
        /// </summary>
        void SelectParts(SnipPagePart[] parts)
        {
            CmdManager.BeginBatchCommand();

            ///先取消所有的选择
            if (SelectedParts.Count > 0)
            {
                SelectedParts.Clear();
            }

            ///将所有传入的Part置为选中
            if (parts.Length > 0)
            {
                foreach (SnipPagePart part in parts)
                {
                    part.Selected = true;
                }
            }

            CmdManager.EndBatchCommand();
        }

        private void CasingSelectEnd(Rectangle selectRect)
        {
            ///找到框选的所有Part
            SnipPagePart[] parts = _classPartContainer.GetPartsAt(selectRect);

            ///选择找到的所有Part
            SelectParts(parts);
        }
                
        /// <summary>
        /// 开始拖放一个指定Part。dragDropForNew指定是否是在“新建”被调用的
        /// </summary>
        private void StartPartDragDrop(SnipPagePart dragDropingStartPart, bool dragDropForNew)
        {
            _isMouseDown = true;
            _isCasingSelect = false;
            _isDrawDropingPart = true;
            _dragDropForNew = dragDropForNew;
            _dragDropingStartPart = dragDropingStartPart;

            ///显示跟踪的框
            if (!_dragDropForNew)
            {
                Point partLocationScreen = PointToScreen(dragDropingStartPart.LocationForDesigner);
                DrawDropFollowForm.ShowForm(partLocationScreen.X, partLocationScreen.Y,
                    dragDropingStartPart.Bounds.Width, dragDropingStartPart.Bounds.Height);
            }
            else
            {
                DrawDropFollowForm.ShowForm(Control.MousePosition.X - 50, Control.MousePosition.Y - 10,
                    100, 20);
            }
        }

        /// <summary>
        /// 开始拖放一个指定Part，在“新建”中调用。
        /// </summary>
        public void StartPartDragDrop(SnipPagePart drawDropingStartPart)
        {
            StartPartDragDrop(drawDropingStartPart, true);
        }

        private void PartDragDrop()
        {
            Point mouseLocationClient = PointToClient(Control.MousePosition);
            Point prevMouseLocationClient = PointToClient(_prevMouseLocation);

            ///显示跟踪的框
            Point partLocationScreen = PointToScreen(_dragDropingStartPart.LocationForDesigner);
            int mouseOffsetX = Control.MousePosition.X - _mouseStartLocation.X;
            int mouseOffsetY = Control.MousePosition.Y - _mouseStartLocation.Y;

            if (!_dragDropForNew)
            {
                DrawDropFollowForm.Singler.Location = new Point(partLocationScreen.X + mouseOffsetX, partLocationScreen.Y + mouseOffsetY);
            }
            else
            {
                DrawDropFollowForm.ShowForm(Control.MousePosition.X - 50, Control.MousePosition.Y - 10,
                    100, 20);
            }

            if (this.Parent.Bounds.Contains(this.Parent.PointToClient(Control.MousePosition)))
            {
                if ((!DrawDropDockForm._halfOpacityForm.Visible) || (!DrawDropDockForm._halfOpacityForm.Bounds.Contains(Control.MousePosition)))
                {
                    ///获取鼠标位置的SnipPagePart，以显示拖拽的停靠指示
                    SnipPagePart[] targetParts = GetPartsAt(mouseLocationClient);
                    bool showBitHalfForm = false;

                    ///如果鼠标位置没有Part，则隐藏停靠指示
                    if (targetParts.Length == 0)
                    {
                        DrawDropDockForm.HideForm();
                        HalfOpacityForm.HideAll();
                    }
                    ///如果只有一个Part，则显示上下左右的停靠指示
                    else if (targetParts.Length == 1 && (!targetParts[0].HasChild))
                    {
                        SnipPagePart tempPart = targetParts[0];
                        ///根据Part的大小判断是否能够显示上下左右(Bot有Into,Item没有Into,所以大小不一样)
                        if ((tempPart.IsBox
                            && targetParts[0].Bounds.Width >= (DrawDropDockForm.ImageWidth * 3)
                            && targetParts[0].Bounds.Height >= (DrawDropDockForm.ImageWidth * 3))
                            || ((!tempPart.IsBox)
                            && targetParts[0].Bounds.Width >= (DrawDropDockForm.ImageWidth * 2)
                            && targetParts[0].Bounds.Height >= (DrawDropDockForm.ImageWidth * 2)))
                        {
                            HalfOpacityForm.HideAll();

                            SnipPagePart targetPart = targetParts[0];
                            ///delete:Point targetPartLocationScreen = PointToScreen(targetPart.LocationForDesignerDisplay);
                            Point targetPartLocationScreen = PointToScreen(targetPart.LocationForDesigner);
                            DrawDropDockForm.ShowForm(targetPartLocationScreen.X, targetPartLocationScreen.Y,
                                targetPart.Bounds.Width, targetPart.Bounds.Height, tempPart.IsBox);
                            DrawDropResult result = DrawDropDockForm.GetDrawDropResult();
                            DrawDropDockForm.ShowHalfOpacity(result);
                        }
                        else
                        {
                            showBitHalfForm = true;
                        }
                    }
                    else
                    {
                        showBitHalfForm = true;
                    }

                    ///用半透明覆盖的形式来指示
                    if (showBitHalfForm)
                    {
                        DrawDropDockForm.HideForm();
                        HalfOpacityForm.ShowForm(targetParts);
                    }
                }
            }
            else
            {
                DrawDropDockForm.HideForm();
                HalfOpacityForm.HideAll();
            }

            ///记录数据，为下次做准备
            _prevMouseLocation = Control.MousePosition;
        }

        private void ProcessDragDrop(SnipPagePart dropdownPart, DrawDropResult result)
        {
            CmdManager.BeginBatchCommand();
            try
            {
                ///拖拽结束后的移动处理：根据拖放结束时的位置和选择的“向下左右”来决定被拖拽的float
                switch (result)
                {
                    case DrawDropResult.None:
                        return;

                    case DrawDropResult.Left:
                        if (dropdownPart.Float_Css == "left")
                        {
                            int insertIndex = dropdownPart.Index;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "left");
                        }
                        else if (dropdownPart.Float_Css == "right")
                        {
                            int insertIndex = dropdownPart.Index + 1;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "right");
                        }
                        else
                        {
                            int insertIndex = dropdownPart.Index;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "left");
                        }
                        break;
                    case DrawDropResult.Top:
                        if (dropdownPart.Float_Css == "left")
                        {
                            int insertIndex = dropdownPart.Index;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "none");
                        }
                        else if (dropdownPart.Float_Css == "right")
                        {
                            int insertIndex = dropdownPart.Index;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "none");
                        }
                        else
                        {
                            int insertIndex = dropdownPart.Index;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "none");
                        }
                        break;
                    case DrawDropResult.Right:
                        if (dropdownPart.Float_Css == "left")
                        {
                            int insertIndex = dropdownPart.Index + 1;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "left");
                        }
                        else if (dropdownPart.Float_Css == "right")
                        {
                            int insertIndex = dropdownPart.Index;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "right");
                        }
                        else
                        {
                            int insertIndex = dropdownPart.Index;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "right");
                        }
                        break;
                    case DrawDropResult.Bottom:
                        if (dropdownPart.Float_Css == "left")
                        {
                            int insertIndex = dropdownPart.Index + 1;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "none");
                        }
                        else if (dropdownPart.Float_Css == "right")
                        {
                            int insertIndex = dropdownPart.Index + 1;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "none");
                        }
                        else
                        {
                            int insertIndex = dropdownPart.Index + 1;
                            MovePart(dropdownPart.ParentContainer, insertIndex, "none");
                        }
                        break;
                    case DrawDropResult.Into:
                        {
                            int insertIndex = dropdownPart.ChildParts.Count;
                            MovePart(dropdownPart, insertIndex, "none");
                            break;
                        }
                    default:
                        throw new Exception("(开发期错误)不可能的标记：" + result);
                }
            }
            finally
            {
                CmdManager.EndBatchCommand();
            }
        }

        /// <summary>
        /// 移动Part
        /// </summary>
        /// <param name="dropdownPart"></param>
        /// <param name="insertIndex"></param>
        /// <param name="floatCss"></param>
        private void MovePart(IPartContainer dropdownPart, int insertIndex, string floatCss)
        {
            ///检查拖拽是否是New，是New则把拖拽的那个Part放过来
            if (_dragDropForNew)
            {
                _dragDropingStartPart.MoveTo(dropdownPart, insertIndex);
                _dragDropingStartPart.Float_Css = floatCss;
            }
            ///拖拽不是New，则把SelectedParts移动过来
            else
            {
                foreach (SnipPagePart part in SelectedParts)
                {
                    part.MoveTo(dropdownPart, insertIndex);
                    part.Float_Css = floatCss;
                    insertIndex = part.Index + 1;
                }
            }
        }

        /// <summary>
        /// 检测结果，并处理真正的Part移动
        /// </summary>
        private void EndPartDragDrop()
        {
            ///隐藏跟随的透明窗体
            DrawDropFollowForm.HideForm();

            ///显示上下左右的处理
            if (_isDrawDropingPart)
            {
                SnipPagePart dropdownPart = GetPartAt(PointToClient(Control.MousePosition), true);
                if (dropdownPart == null)
                {
                    return;
                }
                if (_dragDropingStartPart.ParentContainer is ListBoxPart && _dragDropingStartPart.PartType == SnipPartType.Attribute)
                {
                    #region
                    ListBoxPart _part = (ListBoxPart)_dragDropingStartPart.ParentContainer;
                    if (dropdownPart.ParentContainer != _part)
                    {
                        _isMouseDown = false;
                        _isDrawDropingPart = false;

                        ///隐藏拖拽时的一些辅助窗体
                        DrawDropDockForm.HideForm();
                        HalfOpacityForm.HideAll();
                        return;
                    }
                    else
                    {
                        Point mouseLocationClient = this.PointToClient(Control.MousePosition);
                        SnipPagePart[] targetParts = GetPartsAt(mouseLocationClient);

                        if (targetParts.Length > 0)
                        {
                            ///为每个鼠标下的SnipPagePart生成一个菜单项
                            ContextMenu menu = new ContextMenu();
                            MenuItem item = new MenuItem(dropdownPart.Text);

                            MenuItem topSubItem = new MenuItem("上方");
                            MenuItem bottomSubItem = new MenuItem("下方");
                            MenuItem leftSubItem = new MenuItem("左方");
                            MenuItem rightSubItem = new MenuItem("右方");

                            topSubItem.Click += delegate
                            {
                                ProcessDragDrop(dropdownPart, DrawDropResult.Top);
                            };
                            bottomSubItem.Click += delegate
                            {
                                ProcessDragDrop(dropdownPart, DrawDropResult.Bottom);
                            };
                            leftSubItem.Click += delegate
                            {
                                ProcessDragDrop(dropdownPart, DrawDropResult.Left);
                            };
                            rightSubItem.Click += delegate
                            {
                                ProcessDragDrop(dropdownPart, DrawDropResult.Right);
                            };
                            item.MenuItems.AddRange(new MenuItem[] { topSubItem, bottomSubItem, leftSubItem, rightSubItem });

                            ///如果是容器，则可以放到里面
                            if (dropdownPart.IsBox)
                            {
                                MenuItem intoSubItem = new MenuItem("里面");
                                intoSubItem.Click += delegate
                                {
                                    ProcessDragDrop(dropdownPart, DrawDropResult.Into);
                                };
                                item.MenuItems.Add(intoSubItem);
                            }

                            menu.MenuItems.Add(item);


                            menu.Show(this, mouseLocationClient);
                        }
                    }
                    #endregion
                }
                else if (DrawDropDockForm.IsShow)
                {
                    CmdManager.BeginBatchCommand();

                    ///隐藏DrawDropDockForm
                    DrawDropResult result = DrawDropDockForm.GetDrawDropResult();
                    ProcessDragDrop(dropdownPart, result);

                    CmdManager.EndBatchCommand();
                }
                ///仅显示透明框的处理(然后显示一个菜单供选择)
                else
                {
                    Point mouseLocationClient = this.PointToClient(Control.MousePosition);
                    SnipPagePart[] targetParts = GetPartsAt(mouseLocationClient);

                    if (targetParts.Length > 0)
                    {
                        ///为每个鼠标下的SnipPagePart生成一个菜单项
                        ContextMenu menu = new ContextMenu();
                        foreach (SnipPagePart part in targetParts)
                        {
                            SnipPagePart tempPart = part;
                            MenuItem item = new MenuItem(part.Text);

                            MenuItem topSubItem = new MenuItem("上方");
                            MenuItem bottomSubItem = new MenuItem("下方");
                            MenuItem leftSubItem = new MenuItem("左方");
                            MenuItem rightSubItem = new MenuItem("右方");

                            topSubItem.Click += delegate
                            {
                                ProcessDragDrop(tempPart, DrawDropResult.Top);
                            };
                            bottomSubItem.Click += delegate
                            {
                                ProcessDragDrop(tempPart, DrawDropResult.Bottom);
                            };
                            leftSubItem.Click += delegate
                            {
                                ProcessDragDrop(tempPart, DrawDropResult.Left);
                            };
                            rightSubItem.Click += delegate
                            {
                                ProcessDragDrop(tempPart, DrawDropResult.Right);
                            };
                            item.MenuItems.AddRange(new MenuItem[] { topSubItem, bottomSubItem, leftSubItem, rightSubItem });

                            ///如果是容器，则可以放到里面
                            if (part.IsBox)
                            {
                                MenuItem intoSubItem = new MenuItem("里面");
                                intoSubItem.Click += delegate
                                {
                                    ProcessDragDrop(tempPart, DrawDropResult.Into);
                                };
                                item.MenuItems.Add(intoSubItem);
                            }

                            menu.MenuItems.Add(item);
                        }

                        menu.Show(this, mouseLocationClient);
                    }
                    else
                    {
                        if (_dragDropingStartPart.Designer == null || _dragDropingStartPart.Designer != this)
                        {
                            ChildParts.Add(_dragDropingStartPart);
                            EditPart(_dragDropingStartPart, true);
                        }
                        else
                        {
                            _dragDropingStartPart.MoveTo(this,this.ChildParts.Count);
                        }
                    }
                }
            }

            _isMouseDown = false;
            _isDrawDropingPart = false;

            if (!_isStyleDesigner)
                OnDesignerReseted(EventArgs.Empty);
            ///隐藏拖拽时的一些辅助窗体
            DrawDropDockForm.HideForm();
            HalfOpacityForm.HideAll();
        }

        /// <summary>
        ///  删除选定的Part
        /// </summary>
        private void DeleteSelectePart()
        {
            CmdManager.BeginBatchCommand();
            var arrTemp = this.SelectedParts.ToArray();
            this.SelectedParts.Clear();
            foreach (var part in arrTemp)
            {
                ChildParts.Remove(part);
            }
            CmdManager.EndBatchCommand();
        }

        /// <summary>
        /// 复制选定的part
        /// </summary>
        private void CopySelectPart()
        {
            CmdManager.BeginBatchCommand();

            DataFormats.Format format =
                 DataFormats.GetFormat(typeof(SelectedSnipPagePartCollection).FullName);
            
            //now copy to clipboard
            IDataObject dataObj = new DataObject();
            dataObj.SetData(format.Name, false, SelectedParts);
            Clipboard.SetDataObject(dataObj, true);

            CmdManager.EndBatchCommand();
        }

        /// <summary>
        /// 粘贴到指定的part
        /// </summary>
        private void AffixSelectPart()
        {
            CmdManager.BeginBatchCommand();            

            SelectedSnipPagePartCollection parts = null;
            IDataObject dataObj = Clipboard.GetDataObject();
            string format = typeof(SelectedSnipPagePartCollection).FullName;

            if (dataObj.GetDataPresent(format))
            {
                parts = dataObj.GetData(format) as SelectedSnipPagePartCollection;
            }

            if (parts == null)
            {
                return;
            }

            IPartContainer _parentContainer = null;
            if (SelectedParts.Count < 1)
            {
                foreach (SnipPagePart _clipPart in parts)
                {
                    _clipPart.PartID = XmlUtilService.CreateIncreaseId();
                    ChildParts.Add(_clipPart);
                }
            }
            else
            {
                foreach (SnipPagePart _p in SelectedParts)
                {
                    if (_p.IsBox)
                    {
                        if (_parentContainer != null && _parentContainer == _p)
                            continue;
                        else
                            _parentContainer = _p;

                        foreach (SnipPagePart _clipPart in parts)
                        {
                            if (_clipPart.PartType != SnipPartType.ListBox && _p.PartType == SnipPartType.List)
                            {
                                continue;
                            }
                            _clipPart.PartID = XmlUtilService.CreateIncreaseId();
                            _p.ChildParts.Add(_clipPart);
                        }
                    }
                    else
                    {
                        if (_parentContainer != null && _parentContainer == _p.ParentContainer)
                            continue;
                        else
                            _parentContainer = _p.ParentContainer;

                        foreach (SnipPagePart _clipPart in parts)
                        {
                            _clipPart.PartID = XmlUtilService.CreateIncreaseId();
                            _p.ParentContainer.ChildParts.Add(_clipPart);
                        }
                    }
                }
            }

            CmdManager.EndBatchCommand();
        }


        /// <summary>
        /// part是否已经存在
        /// </summary>
        /// <param name="childParts"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        private bool IsContains(SnipPagePartCollection childParts, SnipPagePart part)
        {
            if (childParts.Count <=0)
            {
                return false;
            }
            if (childParts.Contains(part))
            {
                return true;
            }
            else
            {
                foreach (SnipPagePart _part in childParts)
                {
                    if (IsContains(_part.ChildParts,part))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 载入页面片的数据，加载SnipPagePart
        /// </summary>
        public void Load(SnipXmlElement snipEle)
        {
            _isStyleDesigner = false;
            ///保存一个SnipElement的副本用来做实时预览
            _snipElementForPreview = (SnipXmlElement)snipEle.Clone();

            this.Width = Utility.Convert.PxStringToInt(snipEle.Width);// +(int)(2 * PartSeparator);
            this.Text = StringParserService.Parse("${res:snipDesign.text.formText}") + snipEle.SnipName;

            ///载入所有Part
            GetParts(this.ChildParts, snipEle.GetPartsElement());

            //todo:
            this.Width_Css = (new CssProperty("width", snipEle.GetAttribute("width"))).Value;

            ///重置为初始化状态
            CmdManager.ClearCommand();
            IsModified = false;
        }

        /// <summary>
        /// 载入页面片的数据，加载Style
        /// </summary>
        internal void Load(StyleXmlElement styleEle)
        {
            _isStyleDesigner = true;
            ChildParts.Clear();
            this.Width = Utility.Convert.PxStringToInt(styleEle.Width);
            ///载入所有Part
            GetParts(this.ChildParts, styleEle.GetPartsElement());
            //todo:
            this.Width_Css = (new CssProperty("width", styleEle.GetAttribute("width"))).Value;

            ///重置为初始化状态
            CmdManager.ClearCommand();
            IsModified = false;
        }

        /// <summary>
        /// 开始修改
        /// </summary>
        public void BeginUpdateData()
        {
            _isUpdateData = true;
        }

        /// <summary>
        /// 结束修改
        /// </summary>
        public void EndUpdateData()
        {
            _isUpdateData = false;
        }

        /// <summary>
        /// 将当前Snip设计器中的所有Part保存到Element中
        /// </summary>
        public void SaveToElement(SnipXmlElement ele)
        {
            ///获得Parts节点
            SnipPartsXmlElement partsEle = ele.GetPartsElement();

            ///清空Parts节点数据
            if (partsEle == null)
            {
                partsEle = (SnipPartsXmlElement)ele.OwnerDocument.CreateElement("parts");
                ele.AppendChild(partsEle);
            }
            else
            {
                partsEle.RemoveAll();
            }

            ///保存ChildParts到partsEle中去
            SaveParts(ele,partsEle, ChildParts);

            ///将页面片置为已修改
            ele.IsModified = true;
        }

        /// <summary>
        /// 将当前Style设计器中的所有Part保存到Element中
        /// </summary>
        public void SaveToElement(StyleXmlElement ele)
        {
            ///获得Parts节点
            XmlElement partsEle = ele.GetPartsElement();

            ///清空Parts节点数据
            if (partsEle == null)
            {
                partsEle = ele.OwnerDocument.CreateElement("parts");
                ele.AppendChild(partsEle);
            }
            else
            {
                partsEle.RemoveAll();
            }

            ///保存ChildParts到partsEle中去
            SaveParts(ele, partsEle, ChildParts);

            ///将页面片置为已修改
            //ele.IsModified = true;
        }

        /// <summary>
        /// 生成实时预览的Html
        /// </summary>
        public string ToHtmlPreview()
        {
            Debug.Assert(_snipElementForPreview != null);

            SaveToElement(_snipElementForPreview);
            return _snipElementForPreview.ToHtmlPreview();
        }

        /// <summary>
        /// 打开Part的编辑窗口，若isAdd为true，则点击取消时会删除原Part
        /// </summary>
        public void EditPart(SnipPagePart part, bool isAdd)
        {
            try
            {
                BeginBatchCommand();

                if (part != null)
                {
                    if(!isAdd)
                    {
                        switch (part.PartType)
                        {
                            case SnipPartType.Static:
                                #region 编辑静态Part
                                {
                                    StaticPart spart =(StaticPart)part;
                                    AddStaticPartForm form = new AddStaticPartForm(spart.PageText);
                                    if (form.ShowDialog() == DialogResult.OK)
                                    {
                                        spart.PageText = form.PageText;
                                    } 
                                    break;
                                }
                                #endregion
                            case SnipPartType.Navigation:
                                #region 编辑单个导航Part
                                {
                                    NavigationPart npart = (NavigationPart)part;
                                    AddStaticPartForm form = new AddStaticPartForm(npart.PageText);
                                    form.Owner = this.FindForm();
                                    form.ShowDialog();
                                    break;
                                }
                                #endregion
                            case SnipPartType.List:
                                #region 编辑列表Part
                                {
                                    AddListPartForm form = new AddListPartForm(false, part as ListPart);
                                    form.Owner = this.FindForm();
                                    form.ShowDialog();
                                    break;
                                }
                                #endregion
                            case SnipPartType.ListBox:
                                #region 编辑ListBoxPart
                                {
                                    break;
                                }
                                #endregion
                            case SnipPartType.Box:
                                #region 编辑Box
                                {
                                    break;
                                }
                                #endregion
                            case SnipPartType.Attribute:
                                #region 编辑定制特性Part
                                {
                                    AttributePart npart = (AttributePart)part;
                                    AddStaticPartForm form = new AddStaticPartForm(npart.PageText);
                                    form.Owner = this.FindForm();
                                    form.ShowDialog();
                                    break;
                                }
                                #endregion
                            case SnipPartType.Path:
                                #region
                                {
                                    EditPathPartForm form = new EditPathPartForm(part as PathPart);

                                    form.Owner = this.FindForm();

                                    if (form.ShowDialog() == DialogResult.OK)
                                        IsModified = true;
                                   
                                    break;
                                }
                                #endregion
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (part.PartType)
                        {
                            case SnipPartType.Static:
                                #region
                                {
                                    AddStaticPartForm form = new AddStaticPartForm("");
                                    try
                                    {
                                        if (form.ShowDialog() == DialogResult.OK)
                                        {
                                            //from:ZhengHao 2008.03.03早  
                                            //modify:wangmiao
                                            (part as StaticPart).PageText = form.PageText;
                                        }
                                        else
                                            part.ParentContainer.ChildParts.Remove(part);
                                    }
                                    catch { }
                                    break;
                                }
                                #endregion
                            case SnipPartType.Navigation:
                                #region
                                {
                                    break;
                                }
                                #endregion
                            case SnipPartType.List:
                                #region
                                {
                                    AddListPartForm form = new AddListPartForm(true, part as ListPart);
                                    form.Owner = this.FindForm();
                                    if (form.ShowDialog() == DialogResult.OK)
                                    {
                                        break;
                                    }
                                    else 
                                    {
                                        part.ParentContainer.ChildParts.Remove(part);
                                    }
                                    break;
                                }
                                #endregion
                            case SnipPartType.Box:
                                #region
                                {
                                    
                                    break;
                                }
                                #endregion
                            case SnipPartType.Attribute:
                                #region
                                {
                                    break;
                                }
                                #endregion
                            case SnipPartType.Path:
                                #region
                                {
                                    EditPathPartForm form = new EditPathPartForm(part as PathPart);

                                    form.Owner = this.FindForm();

                                    if (form.ShowDialog() != DialogResult.OK)                                    
                                    {
                                        part.ParentContainer.ChildParts.Remove(part);
                                    }
                                    //StaticPart staticPart = (StaticPart)SnipPagePart.Create(this, SnipPartType.Static);
                                    //part.ChildParts.Add(staticPart);
                                    //staticPart.Width_Css = "10px";
                                    //staticPart.Text = ">>";
                                    break;
                                }
                                #endregion
                            default:
                                Debug.Fail("未知的SnipPartType:" + part.PartType);
                                break;
                        }
                    }
                }
            }
            finally
            {
                EndBatchCommand();
            }
        }

        /// <summary>
        /// 添加SnipPagePart，并打开编辑对话框
        /// </summary>
        public void AddSnipPart(SnipPartType type)
        {
            Debug.Assert(type != SnipPartType.Attribute);

            AddSnipPart(type, null);
            if (!_isStyleDesigner)
                OnDesignerReseted(EventArgs.Empty);
        }
        public void AddSnipPart(SnipPartType type, SnipPartAttribute attribute)
        {
            try
            {
                CmdManager.BeginBatchCommand();

                ///当块的类型是导航（频道）型时
                ///zhenghao at 2008-06-16 15:50
                if (type == SnipPartType.Navigation)
                {
                    AddChannelPartForm form = new AddChannelPartForm(true, this);
                    form.Owner = this.FindForm();
                    int _navigationIndex = 0;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.NavigationParts.Count >= 1)
                        {
                            foreach (NavigationPart nvPart in form.NavigationParts.Values)
                            {
                                ChildParts.Add(nvPart);
                                nvPart.Width_Css = nvPart.Width_Css;
                                _navigationIndex ++;
                                if (form.IsUsingSeparator &&  _navigationIndex < form.NavigationParts.Count)
                                {
                                    StaticPart separatorPart = (StaticPart)SnipPagePart.Create(this, SnipPartType.Static);
                                    ChildParts.Add(separatorPart);
                                    separatorPart.Width_Css = "5px";
                                    separatorPart.PageText = form.SeparatorPartText;
                                }
                            }
                        }
                    }
                    return;
                }

                ///创建SnipPagePart
                SnipPagePart part = SnipPagePart.Create(this, type);

                ///若是AttributePart，则须根据Attribute赋初值
                if (type == SnipPartType.Attribute)
                {
                    ((AttributePart)part).AttributeName = attribute.Name;
                    part.Text = attribute.Name;
                    part.Width_Css = attribute.Width.ToString();
                }
                this.ChildParts.Add(part);

                ///编辑刚创建的SnipPagePart
                EditPart(part, true);
            }
            finally
            {
                CmdManager.EndBatchCommand();
            }
        }       

        #endregion

        #region CommandManager的定义和封装

        /// <summary>
        /// 对此设计器执行的命令的管理
        /// </summary>
        internal CommandManager CmdManager { get; private set; }

        public void BeginBatchCommand()
        {
            CmdManager.BeginBatchCommand();
        }

        public void EndBatchCommand()
        {
            CmdManager.EndBatchCommand();
        }

        public bool CanUndo()
        {
            return CmdManager.CanUndo;
        }

        public bool CanRedo()
        {
            return CmdManager.CanRedo;
        }

        public void Redo()
        {
            CmdManager.Redo();
        }

        public void Undo()
        {
            CmdManager.Undo();
        }

        #endregion

        #region override方法

        protected override void OnLocationChanged(EventArgs e)
        {
            //Invalidate();
            //Update();
            //int x = Location.X;
            //int y = Location.Y;
            //Rectangle rowRect = new Rectangle(x + 4, y + Height,Width, 4);
            //Rectangle columnRect = new Rectangle(x + Width, y + 4, 4, Height);
            //Graphics g = CreateGraphics();
            //g.FillRectangles(new SolidBrush(Color.DimGray), new Rectangle[] { rowRect, columnRect });
            //g.Dispose();
            base.OnLocationChanged(e);
        }

        protected override void OnCreateControl()
        {
            if (this.FindForm() != null)
            {
                this.FindForm().Deactivate += delegate
                {
                    EndPartDragDrop();
                };
            }

            base.OnCreateControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            /////画控件本身的边线
            //Pen myPen = new Pen(System.Drawing.Color.FromArgb(0,0,0));
            //g.DrawRectangle(myPen, new Rectangle(0,0,this.Width-1,this.Height-1));

            ///画Parts
            PaintChildParts(g);

            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _isMouseDown = true;
            _prevMouseLocation = Control.MousePosition;
            _mouseStartLocation = Control.MousePosition;
            ///获得焦点
            if (!this.Focused)
            {
                this.Focus();
            }

            ///按住Alt键拖动鼠标则是框选
            if (Control.ModifierKeys == Keys.Alt)
            {
                _isCasingSelect = true;
            }
            else
            {
                ///点击选择
                if (e.Button == MouseButtons.Left)
                {
                    bool isAdd = (Control.ModifierKeys == Keys.Control);
                    SnipPagePart part = GetPartAt(this.PointToClient(Control.MousePosition), true);
                    SelectPart(part, isAdd);

                    ///选择鼠标点击项
                    if (part != null)
                    {
                        part.OnClick(EventArgs.Empty);
                    }
                }

                #region 右键点击
                else if (e.Button == MouseButtons.Right)  // added by zhenghao at  2008-06-20 15:00
                {
                    bool hasSelectPart = false;
                    SnipPagePart[] parts = GetPartsAt(this.PointToClient(Control.MousePosition));
                    foreach (SnipPagePart part in parts)
                    {
                        if (part.Selected)
                        {
                            hasSelectPart = true;
                            break;
                        }
                    }
                    if (!hasSelectPart)
                    {
                        bool isAdd = (Control.ModifierKeys == Keys.Control);
                        SnipPagePart part = GetPartAt(this.PointToClient(Control.MousePosition), true);
                        SelectPart(part, isAdd);
                        ///选择鼠标点击项
                        if (part != null)
                        {
                            part.OnClick(EventArgs.Empty);
                        }
                    }
                }
                #endregion

            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isMouseDown)
            #region 鼠标在按下状态
            {
                ///框选操作
                if (_isCasingSelect)
                {
                    int x = Math.Min(_mouseStartLocation.X, Control.MousePosition.X);
                    int y = Math.Min(_mouseStartLocation.Y, Control.MousePosition.Y);
                    int width = Math.Abs(Control.MousePosition.X - _mouseStartLocation.X);
                    int height = Math.Abs(Control.MousePosition.Y - _mouseStartLocation.Y);

                    CasingSelectForm.ShowForm(x, y, width, height);
                }
                ///拖拽操作
                else
                {
                    ///还未开始拖拽
                    if (!_isDrawDropingPart)
                    {
                        if (this.SelectedParts.Count > 0)
                        {
                            ///检查鼠标移动的位置有没有达到拖拽的最小值
                            int minDrawDropValue = 5;
                            if ((Math.Abs(Control.MousePosition.X - _prevMouseLocation.X) > minDrawDropValue)
                                || (Math.Abs(Control.MousePosition.Y - _prevMouseLocation.Y) > minDrawDropValue))
                            {
                                _dragDropingStartPart = this.GetPartAt(PointToClient(_mouseStartLocation), true);
                                if (_dragDropingStartPart != null)
                                {
                                    StartPartDragDrop(_dragDropingStartPart, false);
                                }
                            }
                        }
                    }

                    ///正在拖拽中
                    if (_isDrawDropingPart)
                    {
                        PartDragDrop();
                    }
                }
            }
            #endregion
            else
            #region 鼠标不在按下状态
            {
                SnipPagePart part = GetPartAt(PointToClient(Control.MousePosition), true);
                if (part != null && part.Selected)
                {
                    Cursor = Cursors.NoMove2D;
                }
                else
                {
                    Cursor = Cursors.Arrow;
                }
            }
            #endregion
            
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isMouseDown = false;

            ///框选结束
            if (_isCasingSelect)
            {
                Rectangle selectRect = this.RectangleToClient(CasingSelectForm.Singler.Bounds);
                CasingSelectForm.HideForm();
                _isCasingSelect = false;

                ///框选结束的处理函数
                CasingSelectEnd(selectRect);
            }
            ///拖放结束
            else if (_isDrawDropingPart)
            {
                EndPartDragDrop();
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys != Keys.Control)
            {
                SnipPagePart part = GetPartAt(PointToClient(Control.MousePosition), true);
                if (part != null)
                {
                    part.OnDoubleClick(EventArgs.Empty);
                }
            }
            base.OnMouseDoubleClick(e);
        }

        public override void Refresh()
        {
            if (_isUpdateData)
            {
                return;
            }

            base.Refresh();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Delete)
            {
                ///删除选中Part
                DeleteSelectePart();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region IPartContainer的实现

        /// <summary>
        /// 页面片中的顶级的Part集合
        /// </summary>
        public SnipPagePartCollection ChildParts { get; set; }

        public void PaintChildParts(Graphics g)
        {
            _classPartContainer.PaintChildParts(g);
        }

        /// <summary>
        /// 获得指定位置（点，最里面或最外面）的块，
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="isRecursiveChilds">是否是最里面的块</param>
        public SnipPagePart GetPartAt(Point point, bool isRecursiveChilds)
        {
            return _classPartContainer.GetPartAt(point, isRecursiveChilds);
        }

        public SnipPagePart[] GetPartsAt(Point point)
        {
            return _classPartContainer.GetPartsAt(point);
        }

        public SnipPagePart[] GetPartsAt(Rectangle rect)
        {
            return _classPartContainer.GetPartsAt(rect);
        }

        /// <summary>
        /// add by fenggy 2008-06-13 根据ID得到PART
        /// </summary>
        /// <param name="point"></param>
        /// <param name="isRecursiveChilds"></param>
        /// <returns></returns>
        public SnipPagePart GetPartByID(string PartID)
        {
            return _classPartContainer.GetPartByID(PartID);
        }
        public void LayoutParts()
        {
            _classPartContainer.LayoutParts();

            ///刷新界面
            this.Invalidate();
           // OnDesignerReseted(EventArgs.Empty);
        }

        private string _width_Css;
        /// <summary>
        /// 当前Part的宽度
        /// </summary>
        public string Width_Css
        {
            get { return _width_Css; }
            set
            {
                this.Designer.CmdManager.AddExecSetPropertyDesignerCommand<string>(this, Width_Css, value,
                    new SetPropertyCore<string>(SetWidth_Css), PartAction.Relayout);
            }
        }
        internal void SetWidth_Css(string value)
        {
            _width_Css = value;
            Width = WidthPixel;
        }

        public int WidthPixel
        {
            get
            {
                return _classPartContainer.CssLongnessToPixel(Width_Css);

            }
            set { throw new Exception("未支持"); }
        }

        public int FactLines
        {
            get { throw new Exception("未支持的属性"); }
        }

        public int FactWidth
        {
            get { return WidthPixel; }
        }

        /// <summary>
        /// 内部的行数
        /// </summary>
        public int InnerLines { get; private set; }

        /// <summary>
        /// 重置InterLines属性
        /// </summary>
        public void ResetInnerLines(int innerLines)
        {
            InnerLines = innerLines;
        }

        public IPartContainer ParentContainer
        {
            get { return null; }
        }

        public SnipPageDesigner Designer
        {
            get { return this; }
        }

        public int Level
        {
            get { return 0; }
        }

        #endregion

        #region 自定义的事件

        public event EventHandler PartsLayouted;
        protected internal void OnPartsLayouted(EventArgs e)
        {
            if (PartsLayouted != null)
            {
                PartsLayouted(this, e);
            }
        }
        /// <summary>
        /// 当设计器刷新时
        /// </summary>
        public event EventHandler DesignerReseted;
        /// <summary>
        /// 当设计器刷新时
        /// </summary>
        /// <param name="e"></param>
        protected internal void OnDesignerReseted(EventArgs e)
        {
            if (DesignerReseted != null)
            {
                DesignerReseted(this, e);
            }
        }

        public event EventHandler SelectedPartsChanged;
        protected internal void OnSelectedPartsChanged(EventArgs e)
        {
            OnLevelNodesChanged(EventArgs.Empty);

            if (SelectedPartsChanged != null)
            {
                SelectedPartsChanged(this, e);
            }
        }

        #endregion

        #region INavigatable 成员

        void INavigatable.Select(INavigateNode node)
        {
            SnipPagePart part = node as SnipPagePart;
            if (Control.ModifierKeys == Keys.Control)
            {
                if (this.SelectedParts.Contains(part))
                {
                    this.SelectedParts.Remove(part);
                }
                else
                {
                    this.SelectedParts.Add(part);
                }
            }
            else
            {
                this.SelectedParts.Clear();
                this.SelectedParts.Add(part);
            }
        }

        IEnumerable<INavigateNode> INavigatable.GetLevelNodes()
        {
            List<INavigateNode> list = new List<INavigateNode>();

            ///先只处理选择一个的情况
            if (this.SelectedParts.Count == 1)
            {
                SnipPagePart tempPart = this.SelectedParts[0];
                while (tempPart != null)
                {
                    list.Insert(0,tempPart);

                    tempPart = tempPart.ParentContainer as SnipPagePart;
                }
            }

            return list;
        }

        public event EventHandler LevelNodesChanged;

        protected void OnLevelNodesChanged(EventArgs e)
        {
            if (LevelNodesChanged != null)
            {
                LevelNodesChanged(this, e);
            }
        }

        #endregion
   
    }

    /// <summary>
    /// 行数据（布局时用的类）
    /// </summary>
    internal class LineData
    {
        /// <summary>
        /// 存储的此行里的Parts集合
        /// </summary>
        public List<SnipPagePart> LineParts { get; private set; }

        /// <summary>
        /// 所在行(即第几行)
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// 第一个Part的Index
        /// </summary>
        public int FirstPartIndex { get; private set; }

        public LineData(int line, int firstPartIndex)
        {
            LineParts = new List<SnipPagePart>();
            this.Line = line;
            this.FirstPartIndex = firstPartIndex;
        }
    }
}
