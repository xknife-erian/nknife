using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public class TmpltViewPad : PadContent
    {
        private MyExTmpltTreeViewPad _tree;
        public MyExTmpltTreeViewPad tmpltTreeViewExPad
        {
            get { return _tree; }
        }

        public TmpltViewPad()
        {
            //todo:更改标题和图标
            this.Text = StringParserService.Parse("${res:Pad.TmpltView.text}");
            this.Icon = Icon.FromHandle(ResourceService.GetResourceImage("MainMenu.view.siteManager").GetHicon());

            this._tree = new MyExTmpltTreeViewPad();
            this._tree.Dock = DockStyle.Fill;

            this.Controls.Add(this._tree);
        }
        static TmpltViewPad _tmoltViewPad = null;
        static public TmpltViewPad TreePadSingle()
        {
            if (_tmoltViewPad == null)
            {
                _tmoltViewPad = new TmpltViewPad();
            }
            return _tmoltViewPad;
        }

        internal void InitializeSiteTreeData()
        {
            _tree.LoadTreeData();
        }
        internal void UnloadTreeData()
        {
            _tree.UnloadTreeData();
        }
    }
}
