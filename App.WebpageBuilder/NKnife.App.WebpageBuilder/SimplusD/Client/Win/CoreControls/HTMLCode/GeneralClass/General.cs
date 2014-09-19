using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using mshtml;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 
    /// </summary>
    public enum BEHAVIOR_EVENT : int
    {
        BEHAVIOREVENT_CONTENTREADY = 0,
        BEHAVIOREVENT_DOCUMENTREADY = 1,
        BEHAVIOREVENT_APPLYSTYLE = 2,
        BEHAVIOREVENT_DOCUMENTCONTEXTCHANGE = 3,
        BEHAVIOREVENT_CONTENTSAVE = 4
    }

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HTML_PAINTER_INFO
    {
        [MarshalAs(UnmanagedType.I4)]
        public int lFlags;
        [MarshalAs(UnmanagedType.I4)]
        public int lZOrder;
        [MarshalAs(UnmanagedType.Struct)]
        public Guid iidDrawObject;
        [MarshalAs(UnmanagedType.Struct)]
        public RECT rcBounds;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
        }

        public static RECT FromXYWH(int x, int y, int width, int height)
        {
            return new RECT(x, y, x + width, y + height);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ComImport(), Guid("3050F425-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElementBehavior
    {
        void Init([In, MarshalAs(UnmanagedType.Interface)]IElementBehaviorSite pBehaviorSite);
        void Notify([In, MarshalAs(UnmanagedType.U4)]BEHAVIOR_EVENT dwEvent, [In]IntPtr pVar);
        void Detach();
    }

    /// <summary>
    /// 
    /// </summary>
    [ComImport(), Guid("3050F6A6-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IHTMLPainter
    {
        void Draw([In, MarshalAs(UnmanagedType.I4)]	int leftBounds, [In, MarshalAs(UnmanagedType.I4)] int topBounds,
            [In, MarshalAs(UnmanagedType.I4)] int rightBounds, [In, MarshalAs(UnmanagedType.I4)] int bottomBounds,
            [In, MarshalAs(UnmanagedType.I4)] int leftUpdate, [In, MarshalAs(UnmanagedType.I4)] int topUpdate,
            [In, MarshalAs(UnmanagedType.I4)] int rightUpdate, [In, MarshalAs(UnmanagedType.I4)]	int bottomUpdate,
             [In, MarshalAs(UnmanagedType.U4)] int lDrawFlags, [In] IntPtr hdc, [In]	IntPtr pvDrawObject);
        void _OnResize([In, MarshalAs(UnmanagedType.I4)] int cx, [In, MarshalAs(UnmanagedType.I4)]int cy);
        void GetPainterInfo([Out] out HTML_PAINTER_INFO htmlPainterInfo);
        void HitTestPoint([In, MarshalAs(UnmanagedType.I4)]	int ptx, [In, MarshalAs(UnmanagedType.I4)] int pty,
             [Out, MarshalAs(UnmanagedType.I4)] out int pbHit, [Out, MarshalAs(UnmanagedType.I4)]	out int plPartID);
    }

    /// <summary>
    /// 
    /// </summary>
    public class ElementBehavior : IElementBehavior, IHTMLPainter
    {
        protected IHTMLDocument2 Document
        {
            get
            {
                return (IHTMLDocument2)this.Element.document;
            }
        }
        protected mshtml.IHTMLElement Element
        {
            get
            {
                return _behaviorSite.GetElement();
            }
        }
        protected IHTMLWindow2 Window
        {
            get
            {
                return this.Document.parentWindow;
            }
        }
        private IElementBehaviorSite _behaviorSite;

        public IElementBehaviorSite ElementSite
        {
            get
            {
                return _behaviorSite;
            }
        }
        public IHTMLPaintSite PaintSite
        {
            get
            {
                return (IHTMLPaintSite)_behaviorSite;
            }
        }

        protected virtual void OnBehaviorInit()
        {
        }
        protected virtual void OnBehaviorNotify(BEHAVIOR_EVENT dwEvent, IntPtr pVar)
        {
        }
        protected virtual void OnBehaviorDetach()
        {
        }
        protected virtual void OnPainterDraw(Rectangle bounds, Rectangle updateRect, Graphics g)
        {
        }
        protected virtual bool OnHitTestPoint(Point pt, ref int plPartID)
        {
            return false;
        }
        protected virtual void OnResize(Size size)
        {
        }
        protected virtual void OnGetPainterInfo(ref HTML_PAINTER_INFO htmlPainterInfo)
        {
        }

        #region IElementBehavior 成员
        void IElementBehavior.Init(IElementBehaviorSite pBehaviorSite)
        {
            _behaviorSite = pBehaviorSite;
            OnBehaviorInit();
        }
        void IElementBehavior.Notify(BEHAVIOR_EVENT dwEvent, IntPtr pVar)
        {
            OnBehaviorNotify(dwEvent, pVar);
        }
        void IElementBehavior.Detach()
        {
            OnBehaviorDetach();
            _behaviorSite = null;
        }
        #endregion

        #region IHTMLPainter 成员
        void IHTMLPainter.Draw(int leftBounds, int topBounds, int rightBounds, int bottomBounds, int leftUpdate, int topUpdate, int rightUpdate, int bottomUpdate, int lDrawFlags, IntPtr hdc, IntPtr pvDrawObject)
        {
            Rectangle bounds = new Rectangle(leftBounds, topBounds, rightBounds - leftBounds, bottomBounds - topBounds);
            Rectangle updateRect = new Rectangle(leftUpdate, topUpdate, rightUpdate - leftUpdate, bottomUpdate - topUpdate);
            Graphics g = Graphics.FromHdc(hdc);
            OnPainterDraw(bounds, updateRect, g);
        }
        void IHTMLPainter._OnResize(int cx, int cy)
        {
            Size size = new Size(cx, cy);
            OnResize(size);
        }
        void IHTMLPainter.GetPainterInfo(out HTML_PAINTER_INFO htmlPainterInfo)
        {
            htmlPainterInfo = new HTML_PAINTER_INFO();
            OnGetPainterInfo(ref htmlPainterInfo);
        }
        void IHTMLPainter.HitTestPoint(int ptx, int pty, out int pbHit, out int plPartID)
        {
            Point pt = new Point(ptx, pty);
            plPartID = 0;
            pbHit = 0;
            if (OnHitTestPoint(pt, ref plPartID))
            {
                pbHit = 1;
            }
        }
        #endregion
    }

}
