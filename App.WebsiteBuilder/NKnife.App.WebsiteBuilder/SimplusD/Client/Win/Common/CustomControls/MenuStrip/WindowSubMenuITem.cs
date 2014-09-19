//using System;
//using System.Collections.Generic;
//using System.Text;
//using WeifenLuo.WinFormsUI.Docking;
//using System.Windows.Forms;

//namespace Jeelu.SimplusD.Client.Win
//{
//    public class WindowSubMenuITem : MyMenuItem
//    {
//        private IDockContent _dockContent;
//        public IDockContent DockContent
//        {
//            get { return _dockContent; }
//        }

//        public WindowSubMenuITem(IDockContent dockContent)
//            :base(dockContent.DockHandler.TabText,((Form)dockContent).Handle.ToString(),false)
//        {
//            _dockContent = dockContent;
//        }

//        protected override void OnClick(EventArgs e)
//        {
//            _dockContent.DockHandler.Activate();

//            base.OnClick(e);
//        }
//    }
//}
//todo: zhucai