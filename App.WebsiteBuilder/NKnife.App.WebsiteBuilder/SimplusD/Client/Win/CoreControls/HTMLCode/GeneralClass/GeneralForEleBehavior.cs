using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using mshtml;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    [ComImport(), Guid("3050F429-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IElementBehaviorFactory
    {
        [return: MarshalAs(UnmanagedType.Interface)]
        IElementBehavior FindBehavior([In, MarshalAs(UnmanagedType.BStr)]string bstrBehavior,
            [In, MarshalAs(UnmanagedType.BStr)]	string bstrBehaviorUrl,
            [In, MarshalAs(UnmanagedType.Interface)]IElementBehaviorSite pSite);
    }

    public class ImageBehavior : ElementBehavior
    {
        public new IHTMLImgElement Element
        {
            get
            {
                return (IHTMLImgElement)base.Element;
            }
        }
    }

    public class TableBehavior : ElementBehavior
    {
        public new mshtml.IHTMLTable Element
        {
            get
            {
                return (mshtml.IHTMLTable)base.Element;
            }
        }
    }

    public class TableCellBehavior : ElementBehavior
    {
        public new mshtml.IHTMLTableCell Element
        {
            get
            {
                return (mshtml.IHTMLTableCell)base.Element;
            }
        }
    }

    public class LineBehavior : ElementBehavior
    {
        public new IHTMLHRElement Element
        {
            get
            {
                return (IHTMLHRElement)base.Element;
            }
        }
    }

    public class LINKBehavior : ElementBehavior
    {
        public  new IHTMLAnchorElement Element
        {
            get
            {
                return (IHTMLAnchorElement)base.Element;
            }
        }
    }

    public class PhraseBehavior : ElementBehavior
    {
        public new HTMLPhraseElement Element
        {
            get
            {
                return (HTMLPhraseElement)base.Element;
            }
        }
    }

    public class FlashBehavior : ElementBehavior
    {
        public new IHTMLObjectElement Element
        {
            get
            {
                return (IHTMLObjectElement)base.Element;
            }
        }

        protected override void OnBehaviorNotify(BEHAVIOR_EVENT dwEvent, IntPtr pVar)
        {
            switch (dwEvent)
            {
                case BEHAVIOR_EVENT.BEHAVIOREVENT_DOCUMENTREADY:
                    HTMLObjectElementEvents_Event flashEvent = (HTMLObjectElementEvents_Event)Element;
                    break;
            }
        }
    }

    public class HeaderBehavior : ElementBehavior
    {
        public new IHTMLHeaderElement Element
        {
            get
            {
                return (IHTMLHeaderElement)base.Element;
            }
        }
    }

    public class PBehavior : ElementBehavior
    {
        public new IHTMLParaElement Element
        {
            get
            {
                return (IHTMLParaElement)base.Element;
            }
        }
    }

    public class EmbedBehavior : ElementBehavior
    {
        public new IHTMLEmbedElement Element
        {
            get
            {
                return (IHTMLEmbedElement)base.Element;
            }
        }
    }

    public class FontBehavior : ElementBehavior
    {
        public new IHTMLFontElement Element
        {
            get
            {
                return (IHTMLFontElement)base.Element;
            }
        }
    }
}
