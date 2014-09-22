using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace Jeelu.Win
{
    public class StripTabControl : Control
    {
        /// <summary>
        /// 左侧按钮集(Strip)
        /// </summary>
        protected TabStrip LeftStrip { get; private set; }
        /// <summary>
        /// TabPage集合
        /// </summary>
        public RichList<StripTabPage> TabPages { get; private set; }
        /// <summary>
        /// TabPages的数量
        /// </summary>
        public int TabCount { get { return TabPages.Count; } }


        public StripTabPage CurrentPage { get; private set; }

        private int _leftStripWidth;
        /// <summary>
        /// 左侧按钮的宽度
        /// </summary>
        public int LeftStripWidth
        {
            get
            {
                return _leftStripWidth;
            }
            set
            {
                if (_leftStripWidth != value)
                {
                    _leftStripWidth = value;
                    LeftStrip.Width = value;
                }
            }
        }

        public void SelectPage(int index)
        {
            if (this.TabCount < (index + 1))
            {
                throw new ArgumentOutOfRangeException("index");
            }

            //在最后激活第一个TabStripButton
            this.LeftStrip.Items[index].PerformClick();
        }

        private Panel _mainPanel = new Panel();

        public StripTabControl()
        {
            ///添加左边的选项卡选择按钮
            LeftStrip = new TabStrip();
            LeftStripWidth = 100;            ///左侧按键的高度的显示正常是与此按钮有无文字有关
            LeftStrip.Dock = DockStyle.Left;
            LeftStrip.AutoSize = false;
            LeftStrip.Width = LeftStripWidth;

            _mainPanel.Dock = DockStyle.Fill;

            ///初始化TabPages
            TabPages = new RichList<StripTabPage>();
            TabPages.Inserted += new EventHandler<EventArgs<StripTabPage>>(TabPages_Inserted);
            TabPages.Removed += new EventHandler<EventArgs<StripTabPage>>(TabPages_Removed);

            this.Controls.Add(_mainPanel);
            this.Controls.Add(LeftStrip);
        }

        void TabPages_Inserted(object sender, EventArgs<StripTabPage> e)
        {
            e.Item.StripTabControl = this;

            ///添加JeeluTabPage到当前控件
            e.Item.Dock = DockStyle.Fill;
            e.Item.Visible = false;
            e.Item.TextChanged += new EventHandler(Item_TextChanged);
            _mainPanel.Controls.Add(e.Item);

            ///添加TabPage对应的按钮
            var strip = new TabStripButton(e.Item.Text);
            strip.Tag = e.Item;
            strip.Click += new EventHandler(strip_Click);
            this.LeftStrip.Items.Add(strip);
            e.Item.TabStripButton = strip;
        }

        void strip_Click(object sender, EventArgs e)
        {
            StripTabPage tabPage = ((TabStripButton)sender).Tag as StripTabPage;

            foreach (var item in TabPages)
            {
                if (item == tabPage)
                {
                    item.Visible = true;
                    CurrentPage = item;
                    item.TabStripButton.Select();
                }
                else
                {
                    item.Visible = false;
                }
            }

            OnCurrentPageChanged(EventArgs.Empty);
        }

        protected virtual void OnCurrentPageChanged(EventArgs e)
        {
            if (CurrentPageChanged != null)
            {
                CurrentPageChanged(this, e);
            }
        }
        public event EventHandler CurrentPageChanged;

        void TabPages_Removed(object sender, EventArgs<StripTabPage> e)
        {
            if (e.Item.TabStripButton.Selected)
            {
                this.LeftStrip.Items[0].PerformClick();
            }

            _mainPanel.Controls.Remove(e.Item);
            this.LeftStrip.Items.Remove(e.Item.TabStripButton);
        }

        void Item_TextChanged(object sender, EventArgs e)
        {
            StripTabPage page = (StripTabPage)sender;

            page.TabStripButton.Text = page.Text;
        }
    }
}
