using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Jeelu.Win
{
    public class StripTabPage : Panel
    {
        public StripTabControl StripTabControl { get; internal set; }

        private string _text;
        public override string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;

                OnTextChanged(EventArgs.Empty);
            }
        }

        private bool _tabPageVisible;
        /// <summary>
        /// 整个TabPage是否可见（包括左边的按钮）
        /// </summary>
        public bool TabPageVisible
        {
            get { return _tabPageVisible; }
            set
            {
                _tabPageVisible = value;

                if (this.TabStripButton != null)
                {
                    if (!_tabPageVisible)
                    {
                        ///处理TabControl里TabPage的选中状态
                        if (this.TabStripButton.Selected)
                        {
                            if (this.StripTabControl.TabPages[0] != this)
                            {
                                this.StripTabControl.TabPages[0].TabStripButton.PerformClick();
                            }
                            else
                            {
                                this.StripTabControl.TabPages[1].TabStripButton.PerformClick();
                            }
                        }
                    }

                    ///隐藏/显示左边的按钮
                    this.TabStripButton.Visible = value;
                }
            }
        }

        public bool Selected
        {
            get { return TabStripButton.Selected; }
        }

        internal TabStripButton TabStripButton { get; set; }
    }
}
