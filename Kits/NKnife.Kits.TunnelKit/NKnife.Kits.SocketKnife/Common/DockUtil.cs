using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

namespace NKnife.Kits.SocketKnife.Common
{
    class DockUtil
    {
        private DockingManager _dockingManager;
        private LayoutAnchorGroup _buttom;
        private LayoutAnchorGroup _top;
        private LayoutAnchorGroup _left;
        private LayoutAnchorGroup _right;

        public void Init(DockingManager dockingManager)
        {
            _dockingManager = dockingManager;

            DocumentPane = _dockingManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (DocumentPane != null)
                Documents = DocumentPane.Children;
            else
                Debug.Fail("未找到文档面板。");
        }

        public LayoutDocumentPane DocumentPane { get; private set; }

        public ObservableCollection<LayoutContent> Documents { get; private set; }

        public LayoutAnchorGroup Buttom
        {
            get
            {
                if (_buttom == null)
                {
                    _buttom = _dockingManager.Layout.BottomSide.Children.FirstOrDefault();
                    if (_buttom == null)
                    {
                        _buttom = new LayoutAnchorGroup();
                        _dockingManager.Layout.BottomSide.Children.Add(_buttom);
                    }
                }
                return _buttom;
            }
        }
        public LayoutAnchorGroup Top
        {
            get
            {
                if (_top == null)
                {
                    _top = _dockingManager.Layout.TopSide.Children.FirstOrDefault();
                    if (_top == null)
                    {
                        _top = new LayoutAnchorGroup();
                        _dockingManager.Layout.BottomSide.Children.Add(_top);
                    }
                }
                return _top;
            }
        }
        public LayoutAnchorGroup Left
        {
            get
            {
                if (_left == null)
                {
                    _left = _dockingManager.Layout.LeftSide.Children.FirstOrDefault();
                    if (_buttom == null)
                    {
                        _left = new LayoutAnchorGroup();
                        _dockingManager.Layout.BottomSide.Children.Add(_left);
                    }
                }
                return _left;
            }
        }
        public LayoutAnchorGroup Right
        {
            get
            {
                if (_right == null)
                {
                    _right = _dockingManager.Layout.RightSide.Children.FirstOrDefault();
                    if (_right == null)
                    {
                        _right = new LayoutAnchorGroup();
                        _dockingManager.Layout.RightSide.Children.Add(_right);
                    }
                }
                return _right;
            }
        }
    }
}
