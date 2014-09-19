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
        #region ���Ժ��ֶ�

        /// <summary>
        /// ÿ����ĸ߶ȣ������ʾʱ��
        /// </summary>
        public const int PartHeight = 20;

       // public const int PartSeparator = 5;

        /// <summary>
        /// �Ҽ��˵���ѡ����
        /// </summary>
        ToolStripMenuItem _itemSelect;
        /// <summary>
        /// �Ҽ��˵���ȡ��ѡ����
        /// </summary>
        ToolStripMenuItem _unSelectItem;
        /// <summary>
        /// �Ҽ��˵���ɾ������
        /// </summary>
        ToolStripMenuItem _deleteItem;
        /// <summary>
        /// �Ҽ��˵������ơ���
        /// </summary>
        ToolStripMenuItem _copyItem; 
        /// <summary>
        /// �Ҽ��˵���ճ������
        /// </summary>
        ToolStripMenuItem _affixItem;
        ///// <summary>
        ///// �Ҽ��˵���ճ�����ڲ�����
        ///// </summary>
        //ToolStripMenuItem _affixInPartItem;
        /// <summary>
        /// �Ҽ��˵����༭����
        /// </summary>
        ToolStripMenuItem _editItem;
        /// <summary>
        /// �Ҽ��˵����༭�������ԡ���
        /// </summary>
        ToolStripMenuItem _editLinkItem;
        /// <summary>
        /// �Ҽ��˵���Css���á���
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
        /// ��ʶ�Ƿ������BeginUpdata����Ϊtrue����������²�����Ϣ����EndUpdate��ʱ��ǿ�Ʋ���
        /// </summary>
        internal bool _isUpdateData;

        /// <summary>
        /// �Ƿ����޸�
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// ��ȡ���б�ѡ��Ĳ���
        /// </summary>
        public SelectedSnipPagePartCollection SelectedParts { get; private set; }

        /// <summary>
        /// ��ȡ�������Ƿ����Զ��ؼ�������
        /// </summary>
        public bool HasAutoKeyWordsSequenceType { get; set; }
        /// <summary>
        /// ��ȡ������������������ɫ
        /// </summary>
        public Color DesignerBackColor 
        {
            get { return BackColor; }
            set { BackColor = value; }
        }

        private bool _isStyleDesigner = false;


        #endregion

        #region ���캯��

        public SnipPageDesigner(bool hasAutoKeyWordsSequenceType)
        {            
            ///��ʼ�����Ա
            ChildParts = new SnipPagePartCollection(this);
            SelectedParts = new SelectedSnipPagePartCollection(this);
            CmdManager = new CommandManager(this);
            this.DesignerBackColor = SoftwareOption.SnipDesigner.DesignerBackColor;
            this.ContextMenuStrip = _contextMenu;
            _contextMenu.ShowImageMargin = false;
            this._classPartContainer = new ClassPartContainer(this);

            HasAutoKeyWordsSequenceType = hasAutoKeyWordsSequenceType;

            ///���ÿؼ��ɻ�ȡ����
            SetStyle(ControlStyles.Selectable, true);
            ///���ÿؼ�Ϊ˫����
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            /// ����ڼ���Ҽ��˵�
            _contextMenu.Opened += new EventHandler(_contextMenu_DropDownOpening);
            //��ӡ�ѡ����
            _itemSelect = new ToolStripMenuItem(GetText("selectText"));
            _contextMenu.Items.Add(_itemSelect);
            //��ӡ�ȡ��ѡ����
            _unSelectItem = new ToolStripMenuItem(GetText("unSelectText"), null, UnSelectPart);
            _contextMenu.Items.Add(_unSelectItem);
            //��� �����
            _contextMenu.Items.Add(new ToolStripSeparator());
            //��ӡ����ơ���            
            _copyItem = new ToolStripMenuItem(GetText("copyText"),null,CopyPart);
            _contextMenu.Items.Add(_copyItem);
            //��ӡ�ճ������
            _affixItem = new ToolStripMenuItem(GetText("affixText"),null,AffixPart);
            _contextMenu.Items.Add(_affixItem);
            ////��ӡ�ճ������
            //_affixItem = new ToolStripMenuItem(GetText("affixText"), null, AffixPart);
            //_contextMenu.Items.Add(_affixItem);
            //��ӡ�ɾ������
            _deleteItem = new ToolStripMenuItem(GetText("deleteText"), null, DelectPart);
            _contextMenu.Items.Add(_deleteItem);
            //��� �����
            _contextMenu.Items.Add(new ToolStripSeparator());
            //��ӡ��༭����
            _editItem = new ToolStripMenuItem(GetText("editText"), null, EditPart);
            _contextMenu.Items.Add(_editItem);
            //��ӡ��༭�������ԡ�
            _editLinkItem = new ToolStripMenuItem(GetText("editLinkText"), null, EditLink);
            _contextMenu.Items.Add(_editLinkItem);
            //��ӡ�Css���á���
            _cssSetupItem = new ToolStripMenuItem(GetText("cssSetupText"), null, CssSetupPart);
            _contextMenu.Items.Add(_cssSetupItem);


        }

        #endregion

        #region �Ҽ��˵����¼�

        /// <summary>
        /// �Ҽ��˵���Opening�¼�
        /// </summary>
        void _contextMenu_DropDownOpening(object sender, EventArgs e)
        {
            #region �����Ҽ��˵��С��༭...���͡�Css...��ѡ���״̬

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

            ///���²˵�״̬
            bool isSelectedPart = this.SelectedParts.Count > 0;
            _copyItem.Enabled = isSelectedPart;
            _deleteItem.Enabled = isSelectedPart;
            _unSelectItem.Enabled = isSelectedPart;

            ///�ҵ���괦��ָʾ�ĵ㣬��ӡ�ѡ��������Ӳ˵���
            CmdManager.BeginBatchCommand();
            _itemSelect.DropDownItems.Clear();
            SnipPagePart[] parts = GetPartsAt(this.PointToClient(Control.MousePosition));

            ///������ĵ�����Part�������
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
            ///������ĵ���û��Part������ʾһ�����ա�
            else
            {
                ToolStripMenuItem emptyMenuItem = new ToolStripMenuItem("��");
                emptyMenuItem.Enabled = false;
                _itemSelect.DropDownItems.Add(emptyMenuItem);                
            }
            CmdManager.EndBatchCommand();
        }

        /// <summary>
        /// ɾ��ѡ���Ĳ���
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
        /// ȡ��ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UnSelectPart(object sender, EventArgs e)
        {
            this.SelectedParts.Clear();
        }

        /// <summary>
        /// ճ��
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
        /// ����
        /// </summary>
        /// <param name="sendrer"></param>
        /// <param name="e"></param>
        void CopyPart(object sendrer, EventArgs e)
        {
            CopySelectPart();
        }

        /// <summary>
        /// �༭ѡ���Ĳ���
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
        /// �༭��������
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
        /// ѡ�����ֵ�Css����
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

        #region �ڲ�����

        /// <summary>
        /// ͨ�����ڵ�ļ��ϵõ���Ӧ��ID[]
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
        /// ��element�ﱣ������ݶ�ȡ��parts(�ݹ�)
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
        /// ��childParts���ϱ��浽xmlElement(�ݹ�)
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
        /// ��childParts���ϱ��浽xmlElement(�ݹ�),������ʽʱ
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
        /// �������partת��Element
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
                    Debug.Fail("δ֪��SnipPartType:" + part.PartType);
                    break;
            }
            return partEle;
        }
        /// <summary>
        /// �������partת��Element:�༭��ʽʱ
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
                    Debug.Fail("δ֪��SnipPartType:" + part.PartType);
                    break;
            }
            return partEle;
        }

        /// <summary>
        /// ѡ��ָ��part
        /// </summary>
        /// <param name="part">ѡ�д�part</param>
        /// <param name="isAdd">ָʾ�Ƿ��Ǽ�ѡ��</param>
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
                    ///ȡ����ǰ��ѡ����:��ʼ����ִ��Command
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

                    ///ѡ���������
                    if (part != null)
                    {
                        if (!part.Selected)
                        {
                            this.Designer.CmdManager.AddExecSelectPartCommand(part);
                        }
                    }

                    ///��������ִ��Command
                    this.Designer.CmdManager.EndBatchCommand();
                }
            }
        }

        /// <summary>
        /// ����ѡ��ָ��part add by 2008-06-13
        /// </summary>
        /// <param name="part">ѡ�д�part</param>
        /// <param name="isAdd">ָʾ�Ƿ��Ǽ�ѡ��</param>
        public void SelectPartAndCheck(SnipPagePart part)
        {
            if (part == null || !part.Selected)
            {
                ///ȡ����ǰ��ѡ����:��ʼ����ִ��Command
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

                ///ѡ���������
                if (part != null)
                {
                    if (!part.Selected)
                    {
                        this.Designer.CmdManager.AddExecSelectPartCommand(part);
                    }
                }

                ///��������ִ��Command
                this.Designer.CmdManager.EndBatchCommand();

                //����PART�����¼�
                if (part != null)
                {
                    part.OnClick(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// ѡ������parts
        /// </summary>
        void SelectParts(SnipPagePart[] parts)
        {
            CmdManager.BeginBatchCommand();

            ///��ȡ�����е�ѡ��
            if (SelectedParts.Count > 0)
            {
                SelectedParts.Clear();
            }

            ///�����д����Part��Ϊѡ��
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
            ///�ҵ���ѡ������Part
            SnipPagePart[] parts = _classPartContainer.GetPartsAt(selectRect);

            ///ѡ���ҵ�������Part
            SelectParts(parts);
        }
                
        /// <summary>
        /// ��ʼ�Ϸ�һ��ָ��Part��dragDropForNewָ���Ƿ����ڡ��½��������õ�
        /// </summary>
        private void StartPartDragDrop(SnipPagePart dragDropingStartPart, bool dragDropForNew)
        {
            _isMouseDown = true;
            _isCasingSelect = false;
            _isDrawDropingPart = true;
            _dragDropForNew = dragDropForNew;
            _dragDropingStartPart = dragDropingStartPart;

            ///��ʾ���ٵĿ�
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
        /// ��ʼ�Ϸ�һ��ָ��Part���ڡ��½����е��á�
        /// </summary>
        public void StartPartDragDrop(SnipPagePart drawDropingStartPart)
        {
            StartPartDragDrop(drawDropingStartPart, true);
        }

        private void PartDragDrop()
        {
            Point mouseLocationClient = PointToClient(Control.MousePosition);
            Point prevMouseLocationClient = PointToClient(_prevMouseLocation);

            ///��ʾ���ٵĿ�
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
                    ///��ȡ���λ�õ�SnipPagePart������ʾ��ק��ͣ��ָʾ
                    SnipPagePart[] targetParts = GetPartsAt(mouseLocationClient);
                    bool showBitHalfForm = false;

                    ///������λ��û��Part��������ͣ��ָʾ
                    if (targetParts.Length == 0)
                    {
                        DrawDropDockForm.HideForm();
                        HalfOpacityForm.HideAll();
                    }
                    ///���ֻ��һ��Part������ʾ�������ҵ�ͣ��ָʾ
                    else if (targetParts.Length == 1 && (!targetParts[0].HasChild))
                    {
                        SnipPagePart tempPart = targetParts[0];
                        ///����Part�Ĵ�С�ж��Ƿ��ܹ���ʾ��������(Bot��Into,Itemû��Into,���Դ�С��һ��)
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

                    ///�ð�͸�����ǵ���ʽ��ָʾ
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

            ///��¼���ݣ�Ϊ�´���׼��
            _prevMouseLocation = Control.MousePosition;
        }

        private void ProcessDragDrop(SnipPagePart dropdownPart, DrawDropResult result)
        {
            CmdManager.BeginBatchCommand();
            try
            {
                ///��ק��������ƶ����������ϷŽ���ʱ��λ�ú�ѡ��ġ��������ҡ�����������ק��float
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
                        throw new Exception("(�����ڴ���)�����ܵı�ǣ�" + result);
                }
            }
            finally
            {
                CmdManager.EndBatchCommand();
            }
        }

        /// <summary>
        /// �ƶ�Part
        /// </summary>
        /// <param name="dropdownPart"></param>
        /// <param name="insertIndex"></param>
        /// <param name="floatCss"></param>
        private void MovePart(IPartContainer dropdownPart, int insertIndex, string floatCss)
        {
            ///�����ק�Ƿ���New����New�����ק���Ǹ�Part�Ź���
            if (_dragDropForNew)
            {
                _dragDropingStartPart.MoveTo(dropdownPart, insertIndex);
                _dragDropingStartPart.Float_Css = floatCss;
            }
            ///��ק����New�����SelectedParts�ƶ�����
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
        /// �������������������Part�ƶ�
        /// </summary>
        private void EndPartDragDrop()
        {
            ///���ظ����͸������
            DrawDropFollowForm.HideForm();

            ///��ʾ�������ҵĴ���
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

                        ///������קʱ��һЩ��������
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
                            ///Ϊÿ������µ�SnipPagePart����һ���˵���
                            ContextMenu menu = new ContextMenu();
                            MenuItem item = new MenuItem(dropdownPart.Text);

                            MenuItem topSubItem = new MenuItem("�Ϸ�");
                            MenuItem bottomSubItem = new MenuItem("�·�");
                            MenuItem leftSubItem = new MenuItem("��");
                            MenuItem rightSubItem = new MenuItem("�ҷ�");

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

                            ///���������������Էŵ�����
                            if (dropdownPart.IsBox)
                            {
                                MenuItem intoSubItem = new MenuItem("����");
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

                    ///����DrawDropDockForm
                    DrawDropResult result = DrawDropDockForm.GetDrawDropResult();
                    ProcessDragDrop(dropdownPart, result);

                    CmdManager.EndBatchCommand();
                }
                ///����ʾ͸����Ĵ���(Ȼ����ʾһ���˵���ѡ��)
                else
                {
                    Point mouseLocationClient = this.PointToClient(Control.MousePosition);
                    SnipPagePart[] targetParts = GetPartsAt(mouseLocationClient);

                    if (targetParts.Length > 0)
                    {
                        ///Ϊÿ������µ�SnipPagePart����һ���˵���
                        ContextMenu menu = new ContextMenu();
                        foreach (SnipPagePart part in targetParts)
                        {
                            SnipPagePart tempPart = part;
                            MenuItem item = new MenuItem(part.Text);

                            MenuItem topSubItem = new MenuItem("�Ϸ�");
                            MenuItem bottomSubItem = new MenuItem("�·�");
                            MenuItem leftSubItem = new MenuItem("��");
                            MenuItem rightSubItem = new MenuItem("�ҷ�");

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

                            ///���������������Էŵ�����
                            if (part.IsBox)
                            {
                                MenuItem intoSubItem = new MenuItem("����");
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
            ///������קʱ��һЩ��������
            DrawDropDockForm.HideForm();
            HalfOpacityForm.HideAll();
        }

        /// <summary>
        ///  ɾ��ѡ����Part
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
        /// ����ѡ����part
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
        /// ճ����ָ����part
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
        /// part�Ƿ��Ѿ�����
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

        #region ��������

        /// <summary>
        /// ����ҳ��Ƭ�����ݣ�����SnipPagePart
        /// </summary>
        public void Load(SnipXmlElement snipEle)
        {
            _isStyleDesigner = false;
            ///����һ��SnipElement�ĸ���������ʵʱԤ��
            _snipElementForPreview = (SnipXmlElement)snipEle.Clone();

            this.Width = Utility.Convert.PxStringToInt(snipEle.Width);// +(int)(2 * PartSeparator);
            this.Text = StringParserService.Parse("${res:snipDesign.text.formText}") + snipEle.SnipName;

            ///��������Part
            GetParts(this.ChildParts, snipEle.GetPartsElement());

            //todo:
            this.Width_Css = (new CssProperty("width", snipEle.GetAttribute("width"))).Value;

            ///����Ϊ��ʼ��״̬
            CmdManager.ClearCommand();
            IsModified = false;
        }

        /// <summary>
        /// ����ҳ��Ƭ�����ݣ�����Style
        /// </summary>
        internal void Load(StyleXmlElement styleEle)
        {
            _isStyleDesigner = true;
            ChildParts.Clear();
            this.Width = Utility.Convert.PxStringToInt(styleEle.Width);
            ///��������Part
            GetParts(this.ChildParts, styleEle.GetPartsElement());
            //todo:
            this.Width_Css = (new CssProperty("width", styleEle.GetAttribute("width"))).Value;

            ///����Ϊ��ʼ��״̬
            CmdManager.ClearCommand();
            IsModified = false;
        }

        /// <summary>
        /// ��ʼ�޸�
        /// </summary>
        public void BeginUpdateData()
        {
            _isUpdateData = true;
        }

        /// <summary>
        /// �����޸�
        /// </summary>
        public void EndUpdateData()
        {
            _isUpdateData = false;
        }

        /// <summary>
        /// ����ǰSnip������е�����Part���浽Element��
        /// </summary>
        public void SaveToElement(SnipXmlElement ele)
        {
            ///���Parts�ڵ�
            SnipPartsXmlElement partsEle = ele.GetPartsElement();

            ///���Parts�ڵ�����
            if (partsEle == null)
            {
                partsEle = (SnipPartsXmlElement)ele.OwnerDocument.CreateElement("parts");
                ele.AppendChild(partsEle);
            }
            else
            {
                partsEle.RemoveAll();
            }

            ///����ChildParts��partsEle��ȥ
            SaveParts(ele,partsEle, ChildParts);

            ///��ҳ��Ƭ��Ϊ���޸�
            ele.IsModified = true;
        }

        /// <summary>
        /// ����ǰStyle������е�����Part���浽Element��
        /// </summary>
        public void SaveToElement(StyleXmlElement ele)
        {
            ///���Parts�ڵ�
            XmlElement partsEle = ele.GetPartsElement();

            ///���Parts�ڵ�����
            if (partsEle == null)
            {
                partsEle = ele.OwnerDocument.CreateElement("parts");
                ele.AppendChild(partsEle);
            }
            else
            {
                partsEle.RemoveAll();
            }

            ///����ChildParts��partsEle��ȥ
            SaveParts(ele, partsEle, ChildParts);

            ///��ҳ��Ƭ��Ϊ���޸�
            //ele.IsModified = true;
        }

        /// <summary>
        /// ����ʵʱԤ����Html
        /// </summary>
        public string ToHtmlPreview()
        {
            Debug.Assert(_snipElementForPreview != null);

            SaveToElement(_snipElementForPreview);
            return _snipElementForPreview.ToHtmlPreview();
        }

        /// <summary>
        /// ��Part�ı༭���ڣ���isAddΪtrue������ȡ��ʱ��ɾ��ԭPart
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
                                #region �༭��̬Part
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
                                #region �༭��������Part
                                {
                                    NavigationPart npart = (NavigationPart)part;
                                    AddStaticPartForm form = new AddStaticPartForm(npart.PageText);
                                    form.Owner = this.FindForm();
                                    form.ShowDialog();
                                    break;
                                }
                                #endregion
                            case SnipPartType.List:
                                #region �༭�б�Part
                                {
                                    AddListPartForm form = new AddListPartForm(false, part as ListPart);
                                    form.Owner = this.FindForm();
                                    form.ShowDialog();
                                    break;
                                }
                                #endregion
                            case SnipPartType.ListBox:
                                #region �༭ListBoxPart
                                {
                                    break;
                                }
                                #endregion
                            case SnipPartType.Box:
                                #region �༭Box
                                {
                                    break;
                                }
                                #endregion
                            case SnipPartType.Attribute:
                                #region �༭��������Part
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
                                            //from:ZhengHao 2008.03.03��  
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
                                Debug.Fail("δ֪��SnipPartType:" + part.PartType);
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
        /// ���SnipPagePart�����򿪱༭�Ի���
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

                ///����������ǵ�����Ƶ������ʱ
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

                ///����SnipPagePart
                SnipPagePart part = SnipPagePart.Create(this, type);

                ///����AttributePart���������Attribute����ֵ
                if (type == SnipPartType.Attribute)
                {
                    ((AttributePart)part).AttributeName = attribute.Name;
                    part.Text = attribute.Name;
                    part.Width_Css = attribute.Width.ToString();
                }
                this.ChildParts.Add(part);

                ///�༭�մ�����SnipPagePart
                EditPart(part, true);
            }
            finally
            {
                CmdManager.EndBatchCommand();
            }
        }       

        #endregion

        #region CommandManager�Ķ���ͷ�װ

        /// <summary>
        /// �Դ������ִ�е�����Ĺ���
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

        #region override����

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

            /////���ؼ�����ı���
            //Pen myPen = new Pen(System.Drawing.Color.FromArgb(0,0,0));
            //g.DrawRectangle(myPen, new Rectangle(0,0,this.Width-1,this.Height-1));

            ///��Parts
            PaintChildParts(g);

            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _isMouseDown = true;
            _prevMouseLocation = Control.MousePosition;
            _mouseStartLocation = Control.MousePosition;
            ///��ý���
            if (!this.Focused)
            {
                this.Focus();
            }

            ///��סAlt���϶�������ǿ�ѡ
            if (Control.ModifierKeys == Keys.Alt)
            {
                _isCasingSelect = true;
            }
            else
            {
                ///���ѡ��
                if (e.Button == MouseButtons.Left)
                {
                    bool isAdd = (Control.ModifierKeys == Keys.Control);
                    SnipPagePart part = GetPartAt(this.PointToClient(Control.MousePosition), true);
                    SelectPart(part, isAdd);

                    ///ѡ���������
                    if (part != null)
                    {
                        part.OnClick(EventArgs.Empty);
                    }
                }

                #region �Ҽ����
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
                        ///ѡ���������
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
            #region ����ڰ���״̬
            {
                ///��ѡ����
                if (_isCasingSelect)
                {
                    int x = Math.Min(_mouseStartLocation.X, Control.MousePosition.X);
                    int y = Math.Min(_mouseStartLocation.Y, Control.MousePosition.Y);
                    int width = Math.Abs(Control.MousePosition.X - _mouseStartLocation.X);
                    int height = Math.Abs(Control.MousePosition.Y - _mouseStartLocation.Y);

                    CasingSelectForm.ShowForm(x, y, width, height);
                }
                ///��ק����
                else
                {
                    ///��δ��ʼ��ק
                    if (!_isDrawDropingPart)
                    {
                        if (this.SelectedParts.Count > 0)
                        {
                            ///�������ƶ���λ����û�дﵽ��ק����Сֵ
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

                    ///������ק��
                    if (_isDrawDropingPart)
                    {
                        PartDragDrop();
                    }
                }
            }
            #endregion
            else
            #region ��겻�ڰ���״̬
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

            ///��ѡ����
            if (_isCasingSelect)
            {
                Rectangle selectRect = this.RectangleToClient(CasingSelectForm.Singler.Bounds);
                CasingSelectForm.HideForm();
                _isCasingSelect = false;

                ///��ѡ�����Ĵ�����
                CasingSelectEnd(selectRect);
            }
            ///�ϷŽ���
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
                ///ɾ��ѡ��Part
                DeleteSelectePart();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region IPartContainer��ʵ��

        /// <summary>
        /// ҳ��Ƭ�еĶ�����Part����
        /// </summary>
        public SnipPagePartCollection ChildParts { get; set; }

        public void PaintChildParts(Graphics g)
        {
            _classPartContainer.PaintChildParts(g);
        }

        /// <summary>
        /// ���ָ��λ�ã��㣬������������棩�Ŀ飬
        /// </summary>
        /// <param name="point">��</param>
        /// <param name="isRecursiveChilds">�Ƿ���������Ŀ�</param>
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
        /// add by fenggy 2008-06-13 ����ID�õ�PART
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

            ///ˢ�½���
            this.Invalidate();
           // OnDesignerReseted(EventArgs.Empty);
        }

        private string _width_Css;
        /// <summary>
        /// ��ǰPart�Ŀ��
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
            set { throw new Exception("δ֧��"); }
        }

        public int FactLines
        {
            get { throw new Exception("δ֧�ֵ�����"); }
        }

        public int FactWidth
        {
            get { return WidthPixel; }
        }

        /// <summary>
        /// �ڲ�������
        /// </summary>
        public int InnerLines { get; private set; }

        /// <summary>
        /// ����InterLines����
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

        #region �Զ�����¼�

        public event EventHandler PartsLayouted;
        protected internal void OnPartsLayouted(EventArgs e)
        {
            if (PartsLayouted != null)
            {
                PartsLayouted(this, e);
            }
        }
        /// <summary>
        /// �������ˢ��ʱ
        /// </summary>
        public event EventHandler DesignerReseted;
        /// <summary>
        /// �������ˢ��ʱ
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

        #region INavigatable ��Ա

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

            ///��ֻ����ѡ��һ�������
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
    /// �����ݣ�����ʱ�õ��ࣩ
    /// </summary>
    internal class LineData
    {
        /// <summary>
        /// �洢�Ĵ������Parts����
        /// </summary>
        public List<SnipPagePart> LineParts { get; private set; }

        /// <summary>
        /// ������(���ڼ���)
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// ��һ��Part��Index
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
