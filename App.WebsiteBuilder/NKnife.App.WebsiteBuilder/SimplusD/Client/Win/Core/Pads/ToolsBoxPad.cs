using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using VS2005ToolBox;
using Jeelu.Win;

namespace Jeelu.SimplusD.Client.Win
{
    public class ToolsBoxPad : PadContent
    {
        /// <summary>
        /// �洢�Ѵ�������VSTreeNode��һ����̬Dictionary
        /// Dictionary��Key��VSTreeNode��Name����,
        /// Dictionary��Value��һ����ֵ�ԣ����ǲ�С��0���������֣����ڹ������������ʱδʵ�֣�;ֵ��VSTreeNode
        /// �л���ͬ�Ĵ���ӦDictionary���Ȼ�ȡ�����Ѵ��������������ٴ���
        /// </summary>
        static Dictionary<string, KeyValuePair<int, ToolBox.VSTreeNode>> ThisVSTreeNodeDic =
            new Dictionary<string, KeyValuePair<int, ToolBox.VSTreeNode>>();

        #region ���Ա����

        private ToolBox _toolBox = new ToolBox();
        private ImageList _imageList = new ImageList();
        private XmlDocument _toolsBoxItemsDoc = new XmlDocument();
        private Point _prevMouseLocation;
        private bool _snipItemIsMouseDown;

        #endregion

        #region ���캯��

        public ToolsBoxPad()
        {
            this.Text = StringParserService.Parse("${res:Pad.Wizard.text}");
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("MainMenu.view.wizard").GetHicon());

            //TODO: ZhengHao Img ��ʱ��
            {
                _imageList.ImageSize = new Size(14, 14);
                _imageList.ColorDepth = ColorDepth.Depth32Bit;
                //_imageList.Images.Add(Image.FromFile(Path.Combine(System.Windows.Forms.Application.StartupPath,
                //    @"icon\mytoolsPadItemDefaultImg.png")));
                System.Drawing.Icon icon = Icon.FromHandle(ResourceService.GetResourceImage("table").GetHicon());
                _imageList.Images.Add(icon);
            }

            //����ToolBox�������ļ������ļ��д�Ÿ�������Ļ�����Ŀ
            _toolsBoxItemsDoc.Load(PathService.CL_ToolsBoxItems);

            #region ToolBox��������

            _toolBox.BackColor = System.Drawing.SystemColors.Control;
            _toolBox.BorderStyle = BorderStyle.None;
            _toolBox.Dock = DockStyle.Fill;
            _toolBox.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            _toolBox.FullRowSelect = true;
            _toolBox.HideSelection = false;
            _toolBox.HotTracking = true;
            _toolBox.ImageIndex = 0;
            _toolBox.ImageList = this._imageList;
            _toolBox.ItemHeight = 20;
            _toolBox.Location = new System.Drawing.Point(12, 12);
            _toolBox.Name = "toolBox";
            _toolBox.SelectedImageIndex = 0;
            _toolBox.ShowLines = false;
            _toolBox.ShowNodeToolTips = true;
            _toolBox.ShowPlusMinus = false;
            _toolBox.ShowRootLines = false;
            _toolBox.Size = new System.Drawing.Size(201, 311);
            _toolBox.TabIndex = 0;

            #endregion

            this.InitToolBox();
            this.Controls.Add(_toolBox);

            _toolBox.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(SnipItem_DoubleClick);
            _toolBox.MouseDown += new MouseEventHandler(SnipItem_MouseDown);
            _toolBox.MouseMove += new MouseEventHandler(SnipItem_MouseMove);
            _toolBox.MouseUp += new MouseEventHandler(SnipItem_MouseUp);

            Service.Workbench.ActiveWorkDocumentChanged += new EventHandler<EventArgs<Form>>(WorkbenchService_ActiveWorkDocumentChanged);
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �������ڵĵ�ǰ�����Ӵ��ڷ����仯ʱ����
        /// </summary>
        void WorkbenchService_ActiveWorkDocumentChanged(object sender, EventArgs<Form> e)
        {
            //�ر���Ŀ��ʱ��Ӧ�����κδ���
            this.InitToolBox(Service.Workbench.ActiveWorkDocumentType);
        }
        void SnipItem_MouseDown(object sender, MouseEventArgs e)
        {
            _snipItemIsMouseDown = true;
            _prevMouseLocation = Control.MousePosition;
        }
        void SnipItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (_snipItemIsMouseDown)
            {
                ///�������ƶ���λ����û�дﵽ��ק����Сֵ
                int minDrawDropValue = 5;
                if ((Math.Abs(Control.MousePosition.X - _prevMouseLocation.X) > minDrawDropValue)
                    || (Math.Abs(Control.MousePosition.Y - _prevMouseLocation.Y) > minDrawDropValue))
                {
                    TreeView _item = (TreeView)sender;
                    MdiSnipDesignerForm _activeMdiForm = WorkbenchForm.MainForm.ActiveView as MdiSnipDesignerForm;

                    SnipPagePart part = null;
                    switch (_item.SelectedNode.Name)
                    {
                        case "_insertBoxItem":
                            if (_activeMdiForm != null)
                            {
                                part = SnipPagePart.Create(_activeMdiForm.SnipPageDesigner, SnipPartType.Box);
                            }
                            break;
                        case "_insertTextItem":
                            if (_activeMdiForm != null)
                            {
                                part = SnipPagePart.Create(_activeMdiForm.SnipPageDesigner, SnipPartType.Static);
                            }
                            break;
                        case "_insertListItem":
                            if (_activeMdiForm != null)
                            {
                                part = SnipPagePart.Create(_activeMdiForm.SnipPageDesigner, SnipPartType.List);
                            }
                            break;
                        case "_insertNavigationItem":
                            if (_activeMdiForm != null)
                            {
                                //part = SnipPagePart.Create(_activeMdiForm.SnipPageDesigner, SnipPartType.NavigationBox);
                            }
                            break;
                        case "_insertChannelItem":
                            if (_activeMdiForm != null)
                            {
                                //part = SnipPagePart.Create(_activeMdiForm.SnipPageDesigner, SnipPartType.Navigation);
                            }
                            break;
                        case "_insertPathItem":
                            if (_activeMdiForm != null)
                            {
                                part = SnipPagePart.Create(_activeMdiForm.SnipPageDesigner, SnipPartType.Path);
                            }
                            break;
                        default:
                            break;
                    }
                    _snipItemIsMouseDown = false;

                    if (part != null)
                    {
                        _activeMdiForm.SnipPageDesigner.StartPartDragDrop(part);
                        _activeMdiForm.SnipPageDesigner.Capture = true;
                        _activeMdiForm.Activate();
                    }
                }
            }
        }
        void SnipItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (_snipItemIsMouseDown)
            {
                _snipItemIsMouseDown = false;
            }
        }
        void SnipItem_DoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MdiSnipDesignerForm _activeMdiForm = WorkbenchForm.MainForm.ActiveView as MdiSnipDesignerForm;
            switch (e.Node.Name)
            {
                case "_insertTextItem":
                    if (_activeMdiForm != null)
                    {
                        _activeMdiForm.SnipPageDesigner.AddSnipPart(SnipPartType.Static);
                    }
                    break;
                case "_insertListItem":
                    if (_activeMdiForm != null)
                    {
                        _activeMdiForm.SnipPageDesigner.AddSnipPart(SnipPartType.List);
                    }
                    break;
                case "_insertNavigationItem":
                    if (_activeMdiForm != null)
                    {
                       // _activeMdiForm.SnipPageDesigner.AddSnipPart(SnipPartType.NavigationBox);
                    }
                    break;
                case "_insertBoxItem":
                    if (_activeMdiForm != null)
                    {
                        _activeMdiForm.SnipPageDesigner.AddSnipPart(SnipPartType.Box);
                    }
                    break;
                
                case "_insertChannelItem":
                    if (_activeMdiForm != null)
                    {
                        _activeMdiForm.SnipPageDesigner.AddSnipPart(SnipPartType.Navigation);
                    }
                    break;
                case "_insertPathItem":
                    if(_activeMdiForm != null)
                    {
                        _activeMdiForm.SnipPageDesigner.AddSnipPart(SnipPartType.Path);
                    }
                    break;
                case "cssSetup":
                    MdiTmpltDesignForm _tmpltForm = WorkbenchForm.MainForm.ActiveView as MdiTmpltDesignForm;
                    if (_tmpltForm != null)
                    {
                        ((TmpltDrawPanel)_tmpltForm.TmpltDesign.DrawPanel).CurRectProperty_Click(null, EventArgs.Empty);
                    }
                    break;
                default:
                    //TODO:��Ҫ�޸ģ�luakn,2008��2��20��13ʱ44��
                    if (_activeMdiForm != null)
                    {
                        if (e.Node.Tag is SnipPartAttribute)   //�ж��Ƿ�ΪAttribute�͵�Part
                        {
                            _activeMdiForm.SnipPageDesigner.AddSnipPart(SnipPartType.Attribute, (SnipPartAttribute)e.Node.Tag);
                        }
                    }
                    break;
            }
        }

        #endregion

        #region �ڲ�����

        /// <summary>
        /// ��ʼ���������еĹ������빤��
        /// </summary>
        private void InitToolBox()
        {
            this.InitToolBox(true, WorkDocumentType.None);
            _toolBox.ExpandAll();
        }

        /// <summary>
        /// ��ʼ���������еĹ������빤��
        /// </summary>
        private void InitToolBox(WorkDocumentType type)
        {
            this.InitToolBox(false, type);
            _toolBox.ExpandAll();
        }

        /// <summary>
        /// ��ʼ���������еĹ������빤��
        /// </summary>
        /// <param name="isOnlyHasBase">�Ƿ����������Ĺ�����</param>
        /// <param name="type">��ǰWorkDocument������</param>
        private void InitToolBox(bool isOnlyHasBase, WorkDocumentType type)
        {
            if (this.IsDisposed)
            {
                return;
            }
            _toolBox.Nodes.Clear();
            //_toolBox.Nodes.Add(this.BuildBaseTreeNode());//"����"������

            if (isOnlyHasBase)
            {
                return;//����������Ĺ�����
            }
            switch (Service.Workbench.ActiveWorkDocumentType)
            {
                case WorkDocumentType.SnipDesigner:
                    #region ����ҳ��Ƭ�Ļ������������չ������
                    {
                        MdiSnipDesignerForm form = WorkbenchForm.MainForm.ActiveView as MdiSnipDesignerForm;

                        //���ӻ�����ҳ��Ƭ�������еĹ���
                        _toolBox.Nodes.Add(BuildSnipBaseTreeNode());

                        //���ݶ�������������չ��ҳ��Ƭ�������еĹ���
                        TmpltXmlDocument tmpltDoc = form.SnipElement.OwnerAnyDocument as TmpltXmlDocument;
                        if(form.SnipElement.SnipType != PageSnipType.Content)
                        {
                            return;
                        }
                        if (tmpltDoc.TmpltType != TmpltType.Home)
                        {
                            Type pageType = null;
                            switch (tmpltDoc.TmpltType)
                            {
                                case TmpltType.General:
                                    pageType = typeof(GeneralPageXmlDocument);
                                    break;
                                case TmpltType.Product:
                                    pageType = typeof(ProductXmlDocument);
                                    break;
                                case TmpltType.Project:
                                    pageType = typeof(ProjectXmlDocument);
                                    break;
                                case TmpltType.InviteBidding:
                                    pageType = typeof(InviteBiddingXmlDocument);
                                    break;
                                case TmpltType.Knowledge:
                                    pageType = typeof(KnowledgeXmlDocument);
                                    break;
                                case TmpltType.Hr:
                                    pageType = typeof(HrXmlDocument);
                                    break;
                                default:
                                    Debug.Fail("TmpltType is Fail!!!!!!");
                                    break;
                            }
                            ToolBox.VSTreeNode node = BuildSnipAttributeTreeNode(pageType.GetProperties(), pageType);
                            _toolBox.Nodes.Add(node);
                        }
                        break;
                    }
                    #endregion
                case WorkDocumentType.TmpltDesigner:
                    #region ����ģ���µĹ��߼�
                    {
                        //MdiTmpltDesignForm form = WorkbenchForm.MainForm.ActiveView as MdiTmpltDesignForm;

                        
                        //_toolBox.Nodes.Add(BuildRectBaseTreeNode());
                        break;
                    }
                #endregion
                default:
                    break;
            }
        }

        private ToolBox.VSTreeNode BuildRectBaseTreeNode()
        {
            ToolBox.VSTreeNode treeNode = new ToolBox.VSTreeNode();

            treeNode.Name = "tmpltTool";
            treeNode.Text = StringParserService.Parse("${res:tmpltDesign.DrawPanel.contextMenu.curRectProperty}");

            return treeNode;
        }

        /// <summary>
        /// ���������Ĺ����䣨"����"�����飩
        /// </summary>
        private ToolBox.VSTreeNode BuildBaseTreeNode()
        {
            string name = "baseToolsGroup";
            KeyValuePair<int, ToolBox.VSTreeNode> pair = new KeyValuePair<int, ToolBox.VSTreeNode>();
            if (!ThisVSTreeNodeDic.TryGetValue(name, out pair))
            {
                ToolBox.VSTreeNode baseNode = new ToolBox.VSTreeNode();
                baseNode.Name = name;
                baseNode.Text = StringParserService.Parse("${res:Pad.Wizard.baseText}");
                //baseNode.Nodes.Add(this.BuildBaseTreeNodeSubMethod());

                pair = new KeyValuePair<int, ToolBox.VSTreeNode>(0, baseNode);
                ThisVSTreeNodeDic.Add(baseNode.Name, pair);
            }
            return pair.Value;
        }
        /// <summary>
        /// ������"����"�����飩�Ĺ���TreeNode
        /// </summary>
        private ToolBox.VSTreeNode BuildBaseTreeNodeSubMethod()
        {
            return null;//TODO: lukan, ������"����"�����飩�Ĺ���TreeNodeδʵ��
        }

        /// <summary>
        /// ���ӻ�����ҳ��Ƭ������
        /// ���ⲿXML�������ļ��ж�ȡ���������������ɹ���TreeNode
        /// lukan,2008��2��20��11ʱ35�������Ż�
        /// </summary>
        private ToolBox.VSTreeNode BuildSnipBaseTreeNode()
        {
            string name = "SnipBaseToolsGroup";
            KeyValuePair<int, ToolBox.VSTreeNode> pair = new KeyValuePair<int, ToolBox.VSTreeNode>();
            if (!ThisVSTreeNodeDic.TryGetValue(name, out pair))
            {
                ToolBox.VSTreeNode treeNode = new ToolBox.VSTreeNode();
                XmlElement ele = (XmlElement)_toolsBoxItemsDoc.DocumentElement.SelectSingleNode("//toolsBox");
                Debug.Assert(ele != null, "XmlElement = Null !!! \r\n ToolBox Configtion File is Bad!!!");

                //�������ļ���ȡ��������������
                treeNode.Name = name;
                treeNode.Text = ele.GetAttribute("text");

                //�����ӽڵ㣬��ÿ���ӽڵ�ת����һ������
                foreach (XmlNode node in ele.ChildNodes)
                {
                    if (node.NodeType != XmlNodeType.Element || node.Name.ToLower() != "item")
                    {
                        continue;
                    }
                    XmlElement subEle = (XmlElement)node;
                    ToolBox.VSTreeNode item = new ToolBox.VSTreeNode();
                    string toolName = subEle.GetAttribute("name");
                    string text = subEle.GetAttribute("text");

                    item.Name = toolName;
                    item.Text = text;
                    item.ToolTipCaption = text;
                    item.ToolTipText = text;
                    item.ImageIndex = 0;//TODO: �������еĹ��ߵ�ͼƬδ����
                    treeNode.Nodes.Add(item);
                }
                pair = new KeyValuePair<int, ToolBox.VSTreeNode>(1, treeNode);
                ThisVSTreeNodeDic.Add(treeNode.Name, pair);
            }
            return pair.Value;
        }

        /// <summary>
        /// ���ݶ�������������չ��ҳ��Ƭ�������еĹ���
        /// Lukan, 2008��2��20��11ʱ29��
        /// </summary>
        private ToolBox.VSTreeNode BuildSnipAttributeTreeNode(PropertyInfo[] propertyInfos, Type type)
        {
            string name = "SnipAttributeToolsGroup" + type.FullName;
            KeyValuePair<int, ToolBox.VSTreeNode> pair = new KeyValuePair<int, ToolBox.VSTreeNode>();
            if (!ThisVSTreeNodeDic.TryGetValue(name, out pair))
            {
                //����ToolBox��һ���µĽڵ�
                ToolBox.VSTreeNode treeNode = new ToolBox.VSTreeNode();
                treeNode.Name = name;
                treeNode.Text = StringParserService.Parse("${res:Pad.Wizard.snipText}");
                treeNode.Nodes.Clear();

                //�������Լ���,propertyInfos���е�ǰģ����������Ӧ��ҳ�����͵���������
                foreach (PropertyInfo info in propertyInfos)
                {
                    object[] snipAttrs = info.GetCustomAttributes(typeof(SnipPartAttribute), false);
                    if (snipAttrs.Length <= 0)
                    {
                        continue;//�޶�������
                    }
                    //������������
                    foreach (SnipPartAttribute snipAttr in snipAttrs)
                    {
                        ToolBox.VSTreeNode subNode = new ToolBox.VSTreeNode();
                        subNode.Name = snipAttr.Text;
                        subNode.Text =AutoLayoutPanel.GetLanguageText( snipAttr.Text);
                        subNode.ToolTipCaption = snipAttr.ToolTipCaption;
                        subNode.ToolTipText = snipAttr.ToolTipText;
                        subNode.ImageIndex = snipAttr.Index;
                        subNode.Tag = snipAttr;
                        treeNode.Nodes.Add(subNode);
                    }
                }
                pair = new KeyValuePair<int, ToolBox.VSTreeNode>(2, treeNode);
                ThisVSTreeNodeDic.Add(treeNode.Name, pair);
            }
            return pair.Value;
        }

        #endregion

    }
}
