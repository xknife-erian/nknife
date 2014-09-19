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
        /// 存储已创建过的VSTreeNode的一个静态Dictionary
        /// Dictionary的Key是VSTreeNode的Name属性,
        /// Dictionary的Value是一个键值对，键是不小于0的整型数字，用于工具组的排序（暂时未实现）;值是VSTreeNode
        /// 切换不同的窗体应Dictionary中先获取，如已创建过，将无需再创建
        /// </summary>
        static Dictionary<string, KeyValuePair<int, ToolBox.VSTreeNode>> ThisVSTreeNodeDic =
            new Dictionary<string, KeyValuePair<int, ToolBox.VSTreeNode>>();

        #region 类成员变量

        private ToolBox _toolBox = new ToolBox();
        private ImageList _imageList = new ImageList();
        private XmlDocument _toolsBoxItemsDoc = new XmlDocument();
        private Point _prevMouseLocation;
        private bool _snipItemIsMouseDown;

        #endregion

        #region 构造函数

        public ToolsBoxPad()
        {
            this.Text = StringParserService.Parse("${res:Pad.Wizard.text}");
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("MainMenu.view.wizard").GetHicon());

            //TODO: ZhengHao Img 临时的
            {
                _imageList.ImageSize = new Size(14, 14);
                _imageList.ColorDepth = ColorDepth.Depth32Bit;
                //_imageList.Images.Add(Image.FromFile(Path.Combine(System.Windows.Forms.Application.StartupPath,
                //    @"icon\mytoolsPadItemDefaultImg.png")));
                System.Drawing.Icon icon = Icon.FromHandle(ResourceService.GetResourceImage("table").GetHicon());
                _imageList.Images.Add(icon);
            }

            //载入ToolBox的配置文件，该文件中存放各设计器的基本项目
            _toolsBoxItemsDoc.Load(PathService.CL_ToolsBoxItems);

            #region ToolBox基础设置

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

        #region 事件

        /// <summary>
        /// 当主窗口的当前激活子窗口发生变化时发生
        /// </summary>
        void WorkbenchService_ActiveWorkDocumentChanged(object sender, EventArgs<Form> e)
        {
            //关闭项目的时候不应该有任何处理
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
                ///检查鼠标移动的位置有没有达到拖拽的最小值
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
                    //TODO:需要修改，luakn,2008年2月20日13时44分
                    if (_activeMdiForm != null)
                    {
                        if (e.Node.Tag is SnipPartAttribute)   //判断是否为Attribute型的Part
                        {
                            _activeMdiForm.SnipPageDesigner.AddSnipPart(SnipPartType.Attribute, (SnipPartAttribute)e.Node.Tag);
                        }
                    }
                    break;
            }
        }

        #endregion

        #region 内部方法

        /// <summary>
        /// 初始化工具箱中的工具组与工具
        /// </summary>
        private void InitToolBox()
        {
            this.InitToolBox(true, WorkDocumentType.None);
            _toolBox.ExpandAll();
        }

        /// <summary>
        /// 初始化工具箱中的工具组与工具
        /// </summary>
        private void InitToolBox(WorkDocumentType type)
        {
            this.InitToolBox(false, type);
            _toolBox.ExpandAll();
        }

        /// <summary>
        /// 初始化工具箱中的工具组与工具
        /// </summary>
        /// <param name="isOnlyHasBase">是否仅载入基础的工具组</param>
        /// <param name="type">当前WorkDocument的类型</param>
        private void InitToolBox(bool isOnlyHasBase, WorkDocumentType type)
        {
            if (this.IsDisposed)
            {
                return;
            }
            _toolBox.Nodes.Clear();
            //_toolBox.Nodes.Add(this.BuildBaseTreeNode());//"常规"工具组

            if (isOnlyHasBase)
            {
                return;//仅载入基础的工具组
            }
            switch (Service.Workbench.ActiveWorkDocumentType)
            {
                case WorkDocumentType.SnipDesigner:
                    #region 创建页面片的基本工具组和扩展工具组
                    {
                        MdiSnipDesignerForm form = WorkbenchForm.MainForm.ActiveView as MdiSnipDesignerForm;

                        //增加基本的页面片工具箱中的工具
                        _toolBox.Nodes.Add(BuildSnipBaseTreeNode());

                        //根据定制特性增加扩展的页面片工具箱中的工具
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
                    #region 创建模板下的工具集
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
        /// 创建基本的工具箱（"常规"工具组）
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
        /// 创建（"常规"工具组）的工具TreeNode
        /// </summary>
        private ToolBox.VSTreeNode BuildBaseTreeNodeSubMethod()
        {
            return null;//TODO: lukan, 创建（"常规"工具组）的工具TreeNode未实现
        }

        /// <summary>
        /// 增加基本的页面片工具组
        /// 从外部XML的配置文件中读取各个工具配置生成工具TreeNode
        /// lukan,2008年2月20日11时35分做简单优化
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

                //从配置文件获取工具组的相关属性
                treeNode.Name = name;
                treeNode.Text = ele.GetAttribute("text");

                //遍历子节点，将每个子节点转换成一个工具
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
                    item.ImageIndex = 0;//TODO: 工具箱中的工具的图片未设置
                    treeNode.Nodes.Add(item);
                }
                pair = new KeyValuePair<int, ToolBox.VSTreeNode>(1, treeNode);
                ThisVSTreeNodeDic.Add(treeNode.Name, pair);
            }
            return pair.Value;
        }

        /// <summary>
        /// 根据定制特性增加扩展的页面片工具箱中的工具
        /// Lukan, 2008年2月20日11时29分
        /// </summary>
        private ToolBox.VSTreeNode BuildSnipAttributeTreeNode(PropertyInfo[] propertyInfos, Type type)
        {
            string name = "SnipAttributeToolsGroup" + type.FullName;
            KeyValuePair<int, ToolBox.VSTreeNode> pair = new KeyValuePair<int, ToolBox.VSTreeNode>();
            if (!ThisVSTreeNodeDic.TryGetValue(name, out pair))
            {
                //定义ToolBox的一个新的节点
                ToolBox.VSTreeNode treeNode = new ToolBox.VSTreeNode();
                treeNode.Name = name;
                treeNode.Text = StringParserService.Parse("${res:Pad.Wizard.snipText}");
                treeNode.Nodes.Clear();

                //遍历属性集合,propertyInfos含有当前模板类型所对应的页面类型的所有属性
                foreach (PropertyInfo info in propertyInfos)
                {
                    object[] snipAttrs = info.GetCustomAttributes(typeof(SnipPartAttribute), false);
                    if (snipAttrs.Length <= 0)
                    {
                        continue;//无定制属性
                    }
                    //遍历定制特性
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
