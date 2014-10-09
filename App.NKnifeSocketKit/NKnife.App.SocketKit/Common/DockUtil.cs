using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

namespace NKnife.App.SocketKit.Common
{
    class DockUtil
    {
        private DockingManager _DockingManager;
        private LayoutAnchorGroup _Buttom;
        private LayoutAnchorGroup _Top;
        private LayoutAnchorGroup _Left;
        private LayoutAnchorGroup _Right;

        public void Init(DockingManager dockingManager)
        {
            _DockingManager = dockingManager;

            DocumentPane = _DockingManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
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
                if (_Buttom == null)
                {
                    _Buttom = _DockingManager.Layout.BottomSide.Children.FirstOrDefault();
                    if (_Buttom == null)
                    {
                        _Buttom = new LayoutAnchorGroup();
                        _DockingManager.Layout.BottomSide.Children.Add(_Buttom);
                    }
                }
                return _Buttom;
            }
        }
        public LayoutAnchorGroup Top
        {
            get
            {
                if (_Top == null)
                {
                    _Top = _DockingManager.Layout.TopSide.Children.FirstOrDefault();
                    if (_Top == null)
                    {
                        _Top = new LayoutAnchorGroup();
                        _DockingManager.Layout.BottomSide.Children.Add(_Top);
                    }
                }
                return _Top;
            }
        }
        public LayoutAnchorGroup Left
        {
            get
            {
                if (_Left == null)
                {
                    _Left = _DockingManager.Layout.LeftSide.Children.FirstOrDefault();
                    if (_Buttom == null)
                    {
                        _Left = new LayoutAnchorGroup();
                        _DockingManager.Layout.BottomSide.Children.Add(_Left);
                    }
                }
                return _Left;
            }
        }
        public LayoutAnchorGroup Right
        {
            get
            {
                if (_Right == null)
                {
                    _Right = _DockingManager.Layout.RightSide.Children.FirstOrDefault();
                    if (_Right == null)
                    {
                        _Right = new LayoutAnchorGroup();
                        _DockingManager.Layout.BottomSide.Children.Add(_Right);
                    }
                }
                return _Right;
            }
        }
    }
}
