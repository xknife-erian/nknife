using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.Win;
using System.Reflection;
using System.IO;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class DesignStyleForm : BaseForm
    {
        public DesignStyleForm(StyleType type)
        {            
            InitializeComponent();
            this._styleType = type;
            if (!Service.Util.DesignMode)
            {
                #region 左侧树
                _toolTipText = this.GetText("nodeToolTipText");

                _genaralText = this.GetText("genaralNodeText");
                genaralNode = new TreeNode(_genaralText);

                _specialText = this.GetText("specialNodeText");
                specialNode = new TreeNode(_specialText);

                _staticText = this.GetText("staticNodeText");
                staticNode = new TreeNode(_staticText);
                staticNode.Name = "insertStaticPart";
                staticNode.ToolTipText = _toolTipText;

                _boxText = this.GetText("boxNodeText");
                boxNode = new TreeNode(_boxText);
                boxNode.Name = "insertBoxPart";
                boxNode.ToolTipText = _toolTipText;

                genaralNode.Nodes.Clear();
                genaralNode.Nodes.Add(staticNode);
                genaralNode.Nodes.Add(boxNode);

                AddSpecialNodes();

                treeViewParts.Nodes.Clear();
                treeViewParts.Nodes.Add(genaralNode);
                treeViewParts.Nodes.Add(specialNode);

                treeViewParts.ExpandAll();

                #endregion                

                #region 页面片设计区

                this._partDesignerPanel.BackColor = SoftwareOption.SnipDesigner.FormBackColor;// System.Drawing.Color.DarkGray;
                this._partDesignerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
                this._partDesignerPanel.Location = new System.Drawing.Point(0, 0);
                this._partDesignerPanel.Name = "workPanel";
                this._partDesignerPanel.TabIndex = 0;
                this._partDesignerPanel.Text = "partDesignerPanel";

                Designer = this._partDesignerPanel.Designer;

                Designer.MouseDoubleClick += new MouseEventHandler(SnipPageDesigner_MouseDoubleClick);
                
                this.panelDisplayStyle.Controls.Add(this._partDesignerPanel);

                #endregion

                #region 样式下拉菜单

                InitComboBox();

                comboBoxSelectStyle.SelectedIndexChanged += new EventHandler(comboBoxSelectStyle_SelectedIndexChanged);

                comboBoxSelectStyle.Leave += new EventHandler(comboBoxSelectStyle_Leave);

                if (comboBoxSelectStyle.Items.Count < 1)
                {
                    comboBoxSelectStyle.Text = this.GetText("newName");
                    CreateNewStyle(this.GetText("newName"));
                    DisplayStyle();
                }
                else
                    comboBoxSelectStyle.SelectedIndex = comboBoxSelectStyle.Items.Count - 1;
        
                #endregion 
            }
        }

        #region 内部变量

        string _toolTipText = "双击添加对应的块";

        string _genaralText = "通用块";
        string _specialText = "专有块";
        string _staticText = "静态块";
        string _boxText = "容器块";        

        /// <summary>
        /// edit by zhenghao at 2008-06-19 10:00
        /// 声明一个通用块 节点
        /// </summary>
        private TreeNode genaralNode =new TreeNode();

        /// <summary>
        /// edit by zhenghao at 2008-06-19 10:00
        /// 声明一个静态块 节点
        /// </summary>
        private TreeNode staticNode = new TreeNode();

        /// <summary>
        /// edit by zhenghao at 2008-06-19 10:00
        /// 声明一个容器块 节点
        /// </summary>
        private TreeNode boxNode = new TreeNode();

        /// <summary>
        /// edit by zhenghao at 2008-06-19 10:00
        /// 声明一个专有块 节点
        /// </summary>
        private TreeNode specialNode = new TreeNode();

        /// <summary>
        /// edit by zhenghao at 2008-06-18 17:30
        /// 声明一个页面片设计器面板
        /// </summary>
        DesignerPanel _partDesignerPanel = new DesignerPanel(false);

        SnipPageDesigner Designer;

        /// <summary>
        /// edit by zhenghao at 2008-06-18 17:30
        /// 声明样式类型
        /// </summary>
        StyleType _styleType = StyleType.GeneralPageListPart;

        /// <summary>
        /// 当前的样式文件
        /// </summary>
        StyleXmlDocument _currenDoc;
       

        /// <summary>
        /// 设计器是否不可用
        /// </summary>
        bool _designerIsNull = true;

        #endregion

        #region 公共属性

        /// <summary>
        /// edit by zhenghao at 2008-06-20 11:10
        /// 获取当前的样式名称
        /// </summary>
        public string CurrentStyleName { get; private set; }

        #endregion

        #region 公共方法

        /// <summary>
        /// edit by zhenghao at 2008-06-19 11:00
        /// 静态方法：获取指定样式类型的所有样式名
        /// </summary>
        /// <returns></returns>
        public static string[] GetStyles(StyleType type)
        {
            List<string> _styles = new List<string>();
            string _stylePath = StyleXmlDocument.GetDirectoryPath(type);
            DirectoryInfo _directoryInfo = new DirectoryInfo(_stylePath);
            if (!_directoryInfo.Exists)
            {
                Directory.CreateDirectory(_stylePath);
                _directoryInfo = new DirectoryInfo(_stylePath);
                return _styles.ToArray();
            }
            FileInfo[] _fileInfos = _directoryInfo.GetFiles();
            foreach (FileInfo info in _fileInfos)
            {
                if (info.Extension.ToLower() == ".sdstyle")
                {
                    _styles.Add(Path.GetFileNameWithoutExtension(info.Name));
                }
            }
            return _styles.ToArray();
        }

        /// <summary>
        /// 获取样式节点
        /// </summary>
        private StyleXmlElement GetStyle(StyleType type, string name)
        {
            if (_currenDoc != null)
            {
                Designer.SaveToElement(_currenDoc.StyleElement);
                _currenDoc.Save(_currenDoc.StyleFilePath);
            }
            _currenDoc = StyleXmlDocument.GetStyleDocument(type, name);
            if (_currenDoc == null)
            {
                return null;
            }
            return _currenDoc.StyleElement;
        }

        #endregion

        #region 内部方法

        void InitComboBox()
        {
            comboBoxSelectStyle.Items.Clear();

            comboBoxSelectStyle.Items.AddRange(GetStyles(_styleType));

        }
        
        /// <summary>
        /// edit by zhenhao at 2008-06-19 11:00
        /// 添加所有的专有类（定制特性）块的节点
        /// </summary>
        private void AddSpecialNodes()
        {
            specialNode.Nodes.Clear();

            Type pageType = null;
            switch (_styleType)
            {
                #region
                case StyleType.GeneralPageListPart:
                case StyleType.GeneralPageContent:
                    pageType = typeof(GeneralPageXmlDocument);
                    break;
                case StyleType.ProductPageListPart:
                case StyleType.ProductPageContent:
                    pageType = typeof(ProductXmlDocument);
                    break;
                case StyleType.ProjectPageListPart:
                case StyleType.ProjectPageContent:
                    pageType = typeof(ProjectXmlDocument);
                    break;
                case StyleType.InviteBiddingPageListPart:
                case StyleType.InviteBiddingPageContent:
                    pageType = typeof(InviteBiddingXmlDocument);
                    break;
                case StyleType.KnowledgePageListPart:
                case StyleType.KnowledgePageContent:
                    pageType = typeof(KnowledgeXmlDocument);
                    break;
                case StyleType.HrPageListPart:
                case StyleType.HrPageContent:
                    pageType = typeof(HrXmlDocument);
                    break;
                case StyleType.HomePageListPart:
                    pageType = typeof(PageXmlDocument);
                    break;
                default:
                    break;
                #endregion
            }
            PropertyInfo[] pInfos = pageType.GetProperties();
            foreach (PropertyInfo info in pInfos)
            {
                object[] snipAttrs = info.GetCustomAttributes(typeof(SnipPartAttribute), false);
                if (snipAttrs.Length <= 0)
                {
                    continue;//无定制属性
                }
                //遍历定制特性
                foreach (SnipPartAttribute snipAttr in snipAttrs)
                {
                    TreeNode subNode = new TreeNode();
                    subNode.Name = snipAttr.Text;
                    subNode.Text = AutoLayoutPanel.GetLanguageText(snipAttr.Text);
                    subNode.ToolTipText = _toolTipText;
                    subNode.ImageIndex = snipAttr.Index;
                    subNode.Tag = snipAttr;
                    specialNode.Nodes.Add(subNode);
                }
            }
        }

        /// <summary>
        /// 根据样式名显示当前的样式
        /// </summary>
        /// <param name="p"></param>
        private void DisplayStyle()
        {
            if (_currenDoc == null)
            {
                return;
            }
            StyleXmlElement styleEle = _currenDoc.StyleElement;
            if (styleEle == null)
            {
                _designerIsNull = true;
                return ;
            }
            Designer.Load(styleEle);
            _designerIsNull = false;
        }

        /// <summary>
        /// 创建新的样式
        /// </summary>
        /// <param name="_newName"></param>
        private void CreateNewStyle(string _newName)
        {
            _currenDoc = StyleXmlDocument.CreatNewStyleDocument(_styleType, _newName);
        }

        #endregion
        
        #region 事件响应

        void comboBoxSelectStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectStyle.SelectedIndex < 0)
            {
                buttonDelete.Enabled = false;
                return;
            }
            buttonDelete.Enabled = true;
            if (_currenDoc != null)
            {
                Designer.SaveToElement(_currenDoc.StyleElement);
                _currenDoc.Save(_currenDoc.StyleFilePath);
            }
            _currenDoc = StyleXmlDocument.GetStyleDocument(_styleType, comboBoxSelectStyle.Text);
            DisplayStyle();
        }

        /// <summary>
        /// 鼠标双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SnipPageDesigner_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys != Keys.Control)
            {
                SnipPagePart part = Designer.GetPartAt(Designer.PointToClient(Control.MousePosition), true);
                if (part != null)
                {
                    Designer.EditPart(part, false);
                }
            }
        }

        void comboBoxSelectStyle_Leave(object sender, EventArgs e)
        {
            bool _inItems = false;
            string _newName = this.GetText("newName");
            if (!string.IsNullOrEmpty(comboBoxSelectStyle.Text))
                _newName = comboBoxSelectStyle.Text;
            else
                comboBoxSelectStyle.Text = _newName;
            foreach (string fileName in comboBoxSelectStyle.Items)
            {
                if (fileName == _newName)
                {
                    _inItems = true;
                    break;
                }
            }
            if (_inItems)
            {
                buttonDelete.Enabled = true;
                if (_currenDoc != null)
                {
                    Designer.SaveToElement(_currenDoc.StyleElement);
                    _currenDoc.Save(_currenDoc.StyleFilePath);
                }
                _currenDoc = StyleXmlDocument.GetStyleDocument(_styleType, _newName);
            }
            else
            {
                buttonDelete.Enabled = false;

                if (_currenDoc!= null)
                {
                    Designer.SaveToElement(_currenDoc.StyleElement);
                    _currenDoc.Save(_currenDoc.StyleFilePath);
                }
                CreateNewStyle(_newName);
                InitComboBox();
            }
            DisplayStyle();
        }
        
        /// <summary>
        /// 左侧树的节点双击事件
        /// </summary>
        private void treeViewParts_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (_designerIsNull)
            {
                return;
            }
            if (e.Node == this.boxNode)
            {
                Designer.AddSnipPart(SnipPartType.Box);
            }
            if (e.Node == this.staticNode)
            {
                Designer.AddSnipPart(SnipPartType.Static);
            }
            if (e.Node.Tag is SnipPartAttribute)   //判断是否为Attribute型的Part
            {
                Designer.AddSnipPart(SnipPartType.Attribute, (SnipPartAttribute)e.Node.Tag);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (_currenDoc != null)
            {
                Designer.SaveToElement(_currenDoc.StyleElement);
                _currenDoc.Save(_currenDoc.StyleFilePath);
                CurrentStyleName = comboBoxSelectStyle.Text;
                DialogResult = DialogResult.OK;
            }
            DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region 自定义事件

        #endregion

    }
}
