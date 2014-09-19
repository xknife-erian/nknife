using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public class MyToolStrip : ToolStrip
    {
        WorkspaceType _defaultWorkspaceType;
        public WorkspaceType DefaultWorkspaceType
        {
            get { return _defaultWorkspaceType; }
        }

        public MyToolStrip(string text,WorkspaceType type)
        {
            base.Text = text;
            this._defaultWorkspaceType = type;
            this.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
        }
        public MyToolStrip(string text,WorkspaceType type,params ToolStripItem[] items) : base(items)
        {
            base.Text = text;
            this._defaultWorkspaceType = type;
            this.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
        }
    }
}
