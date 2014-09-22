using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public class TreePad : PadContent
    {
        MyExTreeViewPad _tree;
        public MyExTreeViewPad TreeViewExPad
        {
            get { return _tree; }
        }

        protected TreePad()
        {
            this.Text = StringParserService.Parse("${res:Pad.SiteManager.text}");
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("MainMenu.view.siteManager").GetHicon());

            this._tree = new MyExTreeViewPad();
            this.Controls.Add(_tree);
            this._tree.Dock = DockStyle.Fill;
        }

        static TreePad  _treePad = null;
        static public TreePad TreePadSingle()
        {
            if (_treePad == null)
            {
                _treePad = new TreePad();
            }
            return _treePad;
        }

        internal void InitializeSiteTreeData()
        {
            this._tree.LoadTreeData();
        }
    }
}
