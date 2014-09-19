using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public static class ToolbarManager
    {
        static WorkbenchForm _mainForm;
        //static Dictionary<ToolbarType, ToolStrip> _allToolStrip = new Dictionary<ToolbarType, ToolStrip>();
        //static Dictionary<WorkbenchForm, List<ToolStrip>> _defaultToolStrips = new Dictionary<WorkbenchForm, List<ToolStrip>>();
        //static Dictionary<WorkbenchForm, List<ToolStrip>> _customToolStrips = new Dictionary<WorkbenchForm, List<ToolStrip>>();
        //static Dictionary<WorkspaceType, ToolbarType> _defaultToolbarTypes = new Dictionary<WorkspaceType, ToolbarType>();
        //static Dictionary<WorkspaceType, ToolbarType> _customToolbarTypes = new Dictionary<WorkspaceType, ToolbarType>();
        static ToolStripPanel _toolStripPanel = new ToolStripPanel();

        static Dictionary<string, ToolStrip> _toolStripList = new Dictionary<string, ToolStrip>();

        static ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();
        static ToolStripMenuItem[] _contextMenuItems;
        public static ToolStripMenuItem[] ContextMenuItems
        {
            get { return _contextMenuItems; }
        }
        public static event EventHandler ContextMenuItemsCreated;

        public static void Initialize(WorkbenchForm mainForm)
        {
            _mainForm = mainForm;
            _toolStripPanel.AutoSize = true;
            _toolStripPanel.Dock = DockStyle.Top;
            _mainForm.Controls.Add(_toolStripPanel);

            //创建所有Toolbar
            CreateAllToolbar();
            MenuStripManager.Initialized += delegate
            {
                //创建右键菜单
                List<ToolStripMenuItem> menuItemList = new List<ToolStripMenuItem>();
                EventHandler menuClick = delegate(object sender, EventArgs e)
                {
                    ToolStripMenuItem item = (ToolStripMenuItem)sender;
                    ToolStrip tempStrip = (ToolStrip)item.Tag;
                    tempStrip.Visible = !tempStrip.Visible;
                    //item.Checked = tempStrip.Visible;
                };
                foreach (ToolStrip strip in _toolStripList.Values)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(strip.Text, null, menuClick);
                    ToolStripMenuItem itemForTopMenu = new ToolStripMenuItem(strip.Text,null,menuClick);

                    item.Checked = strip.Visible;
                    itemForTopMenu.Checked = strip.Visible;

                    ToolStrip tempForDelegate = strip;
                    strip.VisibleChanged += delegate
                    {
                        item.Checked = tempForDelegate.Visible;
                        itemForTopMenu.Checked = tempForDelegate.Visible;
                    };
                    item.Tag = strip;
                    itemForTopMenu.Tag = strip;
                    _contextMenuStrip.Items.Add(item);
                    menuItemList.Add(itemForTopMenu);
                }
                _toolStripPanel.ContextMenuStrip = _contextMenuStrip;

                _contextMenuItems = menuItemList.ToArray();
                _contextMenuStrip.Items.AddRange(_contextMenuItems);

                if (ContextMenuItemsCreated != null)
                {
                    ContextMenuItemsCreated(null, EventArgs.Empty);
                }
            };

            //注册主窗体的ActiveWorkspaceTypeChanged事件
            _mainForm.ActiveWorkspaceTypeChanged += delegate(object sender, WorkspaceTypeEventArgs e)
            {
                ChangeToolbar(e.ActiveWorkspaceType);
            };
        }

        static void ChangeToolbar(WorkspaceType workspaceType)
        {
            foreach (MyToolStrip strip in _toolStripList.Values)
            {
                bool isShow = false;

                if (workspaceType == WorkspaceType.Default)
                {
                    isShow = (strip.DefaultWorkspaceType == WorkspaceType.All);
                }
                else
                {
                    isShow = ((strip.DefaultWorkspaceType & workspaceType) == workspaceType);
                }
                strip.Visible = isShow;
            }
        }

        /// <summary>
        /// 创建所有工具栏(这儿不创建每一项,每一项在MenuStripManager中创建)
        /// </summary>
        static void CreateAllToolbar()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(PathService.CL_ToolbarList);

            //由于显示的顺序是后添加的显示在上面,所以用一个Stack存储起来再添加
            Stack<string> tempStack = new Stack<string>();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                MyToolStrip tempToolStrip = new MyToolStrip(node.Attributes["text"].Value,
                    (WorkspaceType)Enum.Parse(typeof(WorkspaceType), node.Attributes["defaultWorkspace"].Value));
                tempToolStrip.ImageList = ResourceService.MainImageList;
                tempToolStrip.ImageScalingSize = new System.Drawing.Size(16, 16);

                _toolStripList.Add(node.Name, tempToolStrip);
                tempStack.Push(node.Name);
            }
            //添加进Panel中去
            foreach (string name in tempStack)
            {
                _toolStripPanel.Join(_toolStripList[name]);
            }
        }

        /// <summary>
        /// 供MenuStripManager调用,往容器中加入ToolStripItem
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        static internal void AddToolStripItem(string key,ToolStripItem item)
        {
            _toolStripList[key].Items.Add(item);
        }
        /// <summary>
        /// 是否需要添加分隔线
        /// </summary>
        static internal bool ShoudAddSeparator(string key)
        {
            //如果key对应的ToolStrip中有项,并且最后一项不是ToolStripSeparator,则需要添加
            ToolStrip strip = _toolStripList[key];
            if (strip.Items.Count > 0)
            {
                return !(strip.Items[strip.Items.Count-1] is ToolStripSeparator);
            }
            return false;
        }

        //static ToolStrip GetToolStrip(ToolbarType toolbarType)
        //{
        //    ToolStrip newToolStrip;
        //    if (!_allToolStrip.TryGetValue(toolbarType, out newToolStrip))
        //    {
        //        newToolStrip = new ToolStrip();
        //        newToolStrip.ImageScalingSize = new System.Drawing.Size(100, 100);
        //        switch (toolbarType)
        //        {
        //            case ToolbarType.Standard:
        //                ToolStripButton openButton = new ToolStripButton("打开");
        //                newToolStrip.Items.AddRange(new ToolStripItem[] { openButton });
        //                break;
        //            case ToolbarType.ContentEdit:
        //                break;
        //            default:
        //                throw new Exception("未知参数:" + toolbarType);
        //        }

        //        _mainForm.Controls.Add(newToolStrip);
        //        _allToolStrip.Add(toolbarType, newToolStrip);
        //    }

        //    return newToolStrip;
        //}
    }
}
