using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Jeelu.KeywordResonator.Client
{
    public partial class MyContextMenuStrip : ContextMenuStrip
    {
        private MyContextMenuStrip()
        {
        }
        static public MyContextMenuStrip CreateForListCheckBox(AbstractView keywordListView)
        {
            ///创建一个TreeContextMenuStrip
            MyContextMenuStrip menuStrip = new MyContextMenuStrip();

            menuStrip.Items.AddRange(new ToolStripItem[]{
                new CombinItems(keywordListView),
                new SplitItems(keywordListView),
                new EditItems(keywordListView),
                new DeleteItems(keywordListView),
                new AddItems(keywordListView),
                new ModifyFrequency(keywordListView),
                new ToolStripSeparator(),
                new CancelCheckItems(keywordListView)

            });
            return menuStrip;
        }
        protected override void OnOpening(System.ComponentModel.CancelEventArgs e)
        {
            ///调用所有BaseTmpltTreeMenuItem的MenuOpening方法，以让BaseTmpltTreeMenuItem在显示之前改变自己的显示状态
            foreach (var item in this.Items)
            {
                ///处理子菜单
                BaseTmpltTreeMenuItem baseMenuItem = item as BaseTmpltTreeMenuItem;
                if (baseMenuItem != null)
                {
                    baseMenuItem.MenuOpening();

                    if (baseMenuItem.DropDownItems.Count > 0)
                    {
                        foreach (var ditem in baseMenuItem.DropDownItems)
                        {
                            if (ditem is BaseTmpltTreeMenuItem)
                                ((BaseTmpltTreeMenuItem)ditem).MenuOpening();
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
