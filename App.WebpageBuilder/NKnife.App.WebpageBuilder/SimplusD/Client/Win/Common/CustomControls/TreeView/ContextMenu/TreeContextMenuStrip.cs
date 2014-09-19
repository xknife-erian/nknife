using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Jeelu.SimplusD.Client.Win
{
    public class TreeContextMenuStrip : ContextMenuStrip
    {
        private TreeContextMenuStrip()
        {
        }

        static public TreeContextMenuStrip CreateForTreeView(MyTreeView treeView)
        {
            ///创建一个TreeContextMenuStrip
            TreeContextMenuStrip menuStrip = new TreeContextMenuStrip();
            NewMenuItem newItem = new NewMenuItem(treeView);

            menuStrip.Items.AddRange(new ToolStripItem[]{
                newItem,
                new ClearPublishInfoMenuItem(treeView),
                new ClearFavoriteMenuItem(treeView),
                new OpenMenuItem(treeView),
                new GotoTargetNode(treeView),
                new ToolStripSeparator(),

                new AddToFavoriteMenuItem(treeView),
                new RemoveFromFavoriteMenuItem(treeView),
                new ToolStripSeparator(),

                new SelectTmpltMenuItem(treeView),
                new GotoCorelationTmpltMenuItem(treeView),
                //new SetAsDefaultTmpltMenuItem(treeView),
                new SetAsIndexPageMenuItem(treeView),
                new TmpltCopyAsMenuItem(treeView),
                new ToolStripSeparator(),

                //deleted by zhucai:"排除"和"包含"这个功能暂时屏蔽掉
                //new ExcludeMenuItem(treeView),
                //new IncludeMenuItem(treeView),
                //new ToolStripSeparator(),

                new CutMenuItem(treeView),
                new CopyMenuItem(treeView),
                new PasteMenuItem(treeView),
                new DeleteMenuItem(treeView),
                new RenameMenuItem(treeView),
                new ToolStripSeparator(),

                new OpenFolderMenuItem(treeView)
            });

            newItem.DropDownItems.AddRange(new ToolStripItem[]{
                new NewFolderMenuItem(treeView),
                new NewChannelMenuItem(treeView),
                new ToolStripSeparator(),

                new ImportResourceMenuItem(treeView),
                new NewGeneralPageMenuItem(treeView),
                new NewHomePageMenuItem(treeView),
                new NewProductPageMenuItem(treeView),
                new NewKnowledgePageMenuItem(treeView),
                new NewHRPageMenuItem(treeView),
                new NewInviteBidPageMenuItem(treeView),
                new NewProjectPageMenuItem(treeView),

                new NewGeneralTmpltMenuItem(treeView),
                new NewHomeTmpltMenuItem(treeView),
                new NewProductTmpltMenuItem(treeView),
                new NewKnowledgeTmpltMenuItem(treeView),
                new NewHRTmpltMenuItem(treeView),
                new NewInviteBidTmpltMenuItem(treeView),
                new NewProjectTmpltMenuItem(treeView)});

            /////找到当前程序集所有的类型
            //Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            /////遍历所有类型
            //foreach (var item in types)
            //{
            //    ///获得TreeMenuAttribute定制特性
            //    //object[] objs = item.GetCustomAttributes(typeof(TreeMenuAttribute), false);

            //    ///处理有TreeMenuAttribute定制特性且继承自ToolStripMenuItem的类
            //    if (Utility.Type.IsInherit(typeof(BaseTreeMenuItem),item))
            //    {
            //        ///创建一个ToolStripMenuItem并添加到TreeContextMenuStrip
            //        ToolStripMenuItem menuItem = (BaseTreeMenuItem)Activator.CreateInstance(item, treeView);
            //        menuStrip.Items.Add(menuItem);
            //    }
            //}

            return menuStrip;
        }

        protected override void OnOpening(System.ComponentModel.CancelEventArgs e)
        {
            ///调用所有BaseMenuItem的MenuOpening方法，以让BaseMenuItem在显示之前改变自己的显示状态
            foreach (var item in this.Items)
            {
                ///处理子菜单
                BaseTreeMenuItem baseMenuItem = item as BaseTreeMenuItem;
                if (baseMenuItem != null)
                {
                    baseMenuItem.MenuOpening();

                    if (baseMenuItem.DropDownItems.Count > 0)
                    {
                        foreach (var ditem in baseMenuItem.DropDownItems)
                        {
                            if (ditem is BaseTreeMenuItem)
                                ((BaseTreeMenuItem)ditem).MenuOpening();
                        }
                    }
                }
            }

            base.OnOpening(e);
        }

        protected override void OnOpened(EventArgs e)
        {
            bool prevIsSeparator = false;
            int visibleIndex = -1;   //可见项的索引
            ToolStripSeparator lastVisibleSeparator = null;   //最后一个可见分隔符
            foreach (ToolStripItem item in this.Items)
            {
                if (item.Visible || item is ToolStripSeparator)
                {
                    visibleIndex++;

                    ///处理分隔符
                    ToolStripSeparator separator = item as ToolStripSeparator;

                    ///是分隔符:
                    if (separator != null)
                    {
                        ///若分隔符是第一个可见项或上一个可见项也是分隔符，则将之隐藏
                        if (visibleIndex == 0
                            || prevIsSeparator)
                        {
                            separator.Visible = false;
                            visibleIndex--;
                        }
                        ///若不是，则显示
                        else
                        {
                            separator.Visible = true;
                            lastVisibleSeparator = separator;
                        }
                        prevIsSeparator = true;
                        continue;
                    }
                    ///不是分隔符:
                    else
                    {
                        prevIsSeparator = false;
                        lastVisibleSeparator = null;
                    }
                }
            }

            if (lastVisibleSeparator != null)
            {
                lastVisibleSeparator.Visible = false;
            }

            base.OnOpened(e);
        }
    }
}