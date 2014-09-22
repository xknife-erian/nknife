using System;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Collections;
using System.Text;
using mshtml;

namespace Jeelu.SimplusD.Client.Win
{
    public enum HTMLEventDispIds : int
    {
        ID_ONMOUSEOVER = unchecked((int)(0x80010000 + 8)),
        ID_ONMOUSEOUT = unchecked((int)(0x80010000 + 9)),
        ID_ONMOUSEDOWN = (-605),
        ID_ONMOUSEUP = (-607),
        ID_ONMOUSEMOVE = (-606),
        ID_ONKEYDOWN = (-602),
        ID_ONKEYUP = (-604),
        ID_ONKEYPRESS = (-603),
        ID_ONCLICK = (-600),
        ID_ONDBLCLICK = (-601),
        ID_ONSELECT = (1006), //DISPID_NORMAL_FIRST = 1000 + 6 
        ID_ONSUBMIT = (1007),
        ID_ONRESET = (1015),
        ID_ONHELP = unchecked((int)(0x80010000 + 10)),
        ID_ONFOCUS = unchecked((int)(0x80010000 + 1)),
        ID_ONBLUR = unchecked((int)(0x80010000)),
        ID_ONROWEXIT = unchecked((int)(0x80010000 + 6)),
        ID_ONROWENTER = unchecked((int)(0x80010000 + 7)),
        ID_ONBOUNCE = unchecked((int)(0x80010000 + 9)),
        ID_ONBEFOREUPDATE = unchecked((int)(0x80010000 + 4)),
        ID_ONAFTERUPDATE = unchecked((int)(0x80010000 + 5)),
        //ID_ONBEFOREDRAGOVER      = EVENTID_CommonCtrlEvent_BeforeDragOver,
        //ID_ONBEFOREDROPORPASTE   = EVENTID_CommonCtrlEvent_BeforeDropOrPaste,
        ID_ONREADYSTATECHANGE = (-609),
        ID_ONFINISH = (1010),
        ID_ONSTART = (1011),
        ID_ONABORT = (1000),
        ID_ONERROR = (1002),
        ID_ONCHANGE = (1001),
        ID_ONSCROLL = (1014),
        ID_ONLOAD = (1003),
        ID_ONUNLOAD = (1008),
        ID_ONLAYOUT = (1013),
        ID_ONDRAGSTART = unchecked((int)(0x80010000 + 11)),
        ID_ONRESIZE = (1016),
        ID_ONSELECTSTART = unchecked((int)(0x80010000 + 12)),
        ID_ONERRORUPDATE = unchecked((int)(0x80010000 + 13)),
        ID_ONBEFOREUNLOAD = (1017),
        ID_ONDATASETCHANGED = unchecked((int)(0x80010000 + 14)),
        ID_ONDATAAVAILABLE = unchecked((int)(0x80010000 + 15)),
        ID_ONDATASETCOMPLETE = unchecked((int)(0x80010000 + 16)),
        ID_ONFILTER = unchecked((int)(0x80010000 + 17)),
        ID_ONCHANGEFOCUS = (1018),
        ID_ONCHANGEBLUR = (1019),
        ID_ONLOSECAPTURE = unchecked((int)(0x80010000 + 18)),
        ID_ONPROPERTYCHANGE = unchecked((int)(0x80010000 + 19)),
        ID_ONPERSISTSAVE = (1021),
        ID_ONDRAG = unchecked((int)(0x80010000 + 20)),
        ID_ONDRAGEND = unchecked((int)(0x80010000 + 21)),
        ID_ONDRAGENTER = unchecked((int)(0x80010000 + 22)),
        ID_ONDRAGOVER = unchecked((int)(0x80010000 + 23)),
        ID_ONDRAGLEAVE = unchecked((int)(0x80010000 + 24)),
        ID_ONDROP = unchecked((int)(0x80010000 + 25)),
        ID_ONCUT = unchecked((int)(0x80010000 + 26)),
        ID_ONCOPY = unchecked((int)(0x80010000 + 27)),
        ID_ONPASTE = unchecked((int)(0x80010000 + 28)),
        ID_ONBEFORECUT = unchecked((int)(0x80010000 + 29)),
        ID_ONBEFORECOPY = unchecked((int)(0x80010000 + 30)),
        ID_ONBEFOREPASTE = unchecked((int)(0x80010000 + 31)),
        ID_ONPERSISTLOAD = (1022),
        ID_ONROWSDELETE = unchecked((int)(0x80010000 + 32)),
        ID_ONROWSINSERTED = unchecked((int)(0x80010000 + 33)),
        ID_ONCELLCHANGE = unchecked((int)(0x80010000 + 34)),
        ID_ONCONTEXTMENU = (1023),
        ID_ONBEFOREPRINT = (1024),
        ID_ONAFTERPRINT = (1025),
        ID_ONSTOP = (1026),
        ID_ONBEFOREEDITFOCUS = (1027),
        ID_ONMOUSEHOVER = (1028),
        ID_ONCONTENTREADY = (1029),
        ID_ONLAYOUTCOMPLETE = (1030),
        ID_ONPAGE = (1031),
        ID_ONLINKEDOVERFLOW = (1032),
        ID_ONMOUSEWHEEL = (1033),
        ID_ONBEFOREDEACTIVATE = (1034),
        ID_ONMOVE = (1035),
        ID_ONCONTROLSELECT = (1036),
        ID_ONSELECTIONCHANGE = (1037),
        ID_ONMOVESTART = (1038),
        ID_ONMOVEEND = (1039),
        ID_ONRESIZESTART = (1040),
        ID_ONRESIZEEND = (1041),
        ID_ONMOUSEENTER = (1042),
        ID_ONMOUSELEAVE = (1043),
        ID_ONACTIVATE = (1044),
        ID_ONDEACTIVATE = (1045),
        ID_ONMULTILAYOUTCLEANUP = (1046),
        ID_ONBEFOREACTIVATE = (1047),
        ID_ONFOCUSIN = (1048),
        ID_ONFOCUSOUT = (1049),
        ID_HTMLOBJECTELEMENTEVENTS2_ONERROR = unchecked((int)(0x80010000 + 19)),
        ID_HTMLOBJECTELEMENTEVENTS2_ONREADYSTATECHANGE = unchecked((int)(0x80010000 + 20))
    }

    public enum HTMLEventType
    {
        HTMLEventNone = 0,
        HTMLDocumentEvent = 1,
        HTMLWindowEvent = 2,
        HTMLElementEvent = 3
    }

    public enum OLECMDID
    {
        OLECMDID_OPEN = 1,
        OLECMDID_NEW = 2,
        OLECMDID_SAVE = 3,
        OLECMDID_SAVEAS = 4,
        OLECMDID_SAVECOPYAS = 5,
        OLECMDID_PRINT = 6,
        OLECMDID_PRINTPREVIEW = 7,
        OLECMDID_PAGESETUP = 8,
        OLECMDID_SPELL = 9,
        OLECMDID_PROPERTIES = 10,
        OLECMDID_CUT = 11,
        OLECMDID_COPY = 12,
        OLECMDID_PASTE = 13,
        OLECMDID_PASTESPECIAL = 14,
        OLECMDID_UNDO = 15,
        OLECMDID_REDO = 16,
        OLECMDID_SELECTALL = 17,
        OLECMDID_CLEARSELECTION = 18,
        OLECMDID_ZOOM = 19,
        OLECMDID_GETZOOMRANGE = 20,
        OLECMDID_UPDATECOMMANDS = 21,
        OLECMDID_REFRESH = 22,
        OLECMDID_STOP = 23,
        OLECMDID_HIDETOOLBARS = 24,
        OLECMDID_SETPROGRESSMAX = 25,
        OLECMDID_SETPROGRESSPOS = 26,
        OLECMDID_SETPROGRESSTEXT = 27,
        OLECMDID_SETTITLE = 28,
        OLECMDID_SETDOWNLOADSTATE = 29,
        OLECMDID_STOPDOWNLOAD = 30,
        OLECMDID_ONTOOLBARACTIVATED = 31,
        OLECMDID_FIND = 32,
        OLECMDID_DELETE = 33,
        OLECMDID_HTTPEQUIV = 34,
        OLECMDID_HTTPEQUIV_DONE = 35,
        OLECMDID_ENABLE_INTERACTION = 36,
        OLECMDID_ONUNLOAD = 37,
        OLECMDID_PROPERTYBAG2 = 38,
        OLECMDID_PREREFRESH = 39,
        OLECMDID_SHOWSCRIPTERROR = 40,
        OLECMDID_SHOWMESSAGE = 41,
        OLECMDID_SHOWFIND = 42,
        OLECMDID_SHOWPAGESETUP = 43,
        OLECMDID_SHOWPRINT = 44,
        OLECMDID_CLOSE = 45,
        OLECMDID_ALLOWUILESSSAVEAS = 46,
        OLECMDID_DONTDOWNLOADCSS = 47,
        OLECMDID_UPDATEPAGESTATUS = 48,
        OLECMDID_PRINT2 = 49,
        OLECMDID_PRINTPREVIEW2 = 50,
        OLECMDID_SETPRINTTEMPLATE = 51,
        OLECMDID_GETPRINTTEMPLATE = 52,
        OLECMDID_PAGEACTIONBLOCKED = 55,
        OLECMDID_PAGEACTIONUIQUERY = 56,
        OLECMDID_FOCUSVIEWCONTROLS = 57,
        OLECMDID_FOCUSVIEWCONTROLSQUERY = 58,
        OLECMDID_SHOWPAGEACTIONMENU = 59,
        OLECMDID_ADDTRAVELENTRY = 60,
        OLECMDID_UPDATETRAVELENTRY = 61,
        OLECMDID_UPDATEBACKFORWARDSTATE = 62,
        OLECMDID_OPTICAL_ZOOM = 63,
        OLECMDID_OPTICAL_GETZOOMRANGE = 64,
        OLECMDID_WINDOWSTATECHANGED = 65

    }

    public enum OLECMDF
    {
        OLECMDF_SUPPORTED = 1,
        OLECMDF_ENABLED = 2,
        OLECMDF_LATCHED = 4,
        OLECMDF_NINCHED = 8,
        OLECMDF_INVISIBLE = 16,
        OLECMDF_DEFHIDEONCTXTMENU = 32
    }

    public enum OLECMDEXECOPT
    {
        OLECMDEXECOPT_DODEFAULT = 0,
        OLECMDEXECOPT_PROMPTUSER = 1,
        OLECMDEXECOPT_DONTPROMPTUSER = 2,
        OLECMDEXECOPT_SHOWHELP = 3
    }

    public enum tagREADYSTATE
    {
        READYSTATE_UNINITIALIZED = 0,
        READYSTATE_LOADING = 1,
        READYSTATE_LOADED = 2,
        READYSTATE_INTERACTIVE = 3,
        READYSTATE_COMPLETE = 4
    }

    public sealed class Iid_Clsids
    {
        //SID_STopWindow = {49e1b500-4636-11d3-97f7-00c04f45d0b3}
        public static Guid IID_IUnknown = new Guid("00000000-0000-0000-C000-000000000046");
        public static Guid IID_IViewObject = new Guid("0000010d-0000-0000-C000-000000000046");
        public static Guid IID_IAuthenticate = new Guid("79eac9d0-baf9-11ce-8c82-00aa004ba90b");
        public static Guid IID_IWindowForBindingUI = new Guid("79eac9d5-bafa-11ce-8c82-00aa004ba90b");
        public static Guid IID_IHttpSecurity = new Guid("79eac9d7-bafa-11ce-8c82-00aa004ba90b");
        //SID_SNewWindowManager same as IID_INewWindowManager
        public static Guid IID_INewWindowManager = new Guid("D2BC4C84-3F72-4a52-A604-7BCBF3982CBB");
        public static Guid IID_IOleClientSite = new Guid("00000118-0000-0000-C000-000000000046");
        public static Guid IID_IDispatch = new Guid("{00020400-0000-0000-C000-000000000046}");
        public static Guid IID_TopLevelBrowser = new Guid("4C96BE40-915C-11CF-99D3-00AA004AE837");
        public static Guid IID_WebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
        public static Guid IID_IBinding = new Guid("79EAC9C0-BAF9-11CE-8C82-00AA004BA90B");
        public static Guid IID_IBindStatusCallBack = new Guid("79EAC9C1-BAF9-11CE-8C82-00AA004BA90B");
        public static Guid IID_IOleObject = new Guid("00000112-0000-0000-C000-000000000046");
        public static Guid IID_IOleWindow = new Guid("00000114-0000-0000-C000-000000000046");
        public static Guid IID_IServiceProvider = new Guid("6d5140c1-7436-11ce-8034-00aa006009fa");
        public static Guid IID_IWebBrowser = new Guid("EAB22AC1-30C1-11CF-A7EB-0000C05BAE0B");
        public static Guid IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");
        public static Guid CLSID_WebBrowser = new Guid("8856F961-340A-11D0-A96B-00C04FD705A2");
        public static Guid CLSID_CGI_IWebBrowser = new Guid("ED016940-BD5B-11CF-BA4E-00C04FD70816");
        public static Guid CLSID_CGID_DocHostCommandHandler = new Guid("F38BC242-B950-11D1-8918-00C04FC2C836");
        public static Guid CLSID_ShellUIHelper = new Guid("64AB4BB7-111E-11D1-8F79-00C04FC2FBE1");
        public static Guid CLSID_SecurityManager = new Guid("7b8a2d94-0ac9-11d1-896c-00c04fb6bfc4");
        public static Guid IID_IShellUIHelper = new Guid("729FE2F8-1EA8-11d1-8F85-00C04FC2FBE1");
        public static Guid Guid_MSHTML = new Guid("DE4BA900-59CA-11CF-9592-444553540000");
        public static Guid CLSID_InternetSecurityManager = new Guid("7b8a2d94-0ac9-11d1-896c-00c04fB6bfc4");
        public static Guid IID_IInternetSecurityManager = new Guid("79EAC9EE-BAF9-11CE-8C82-00AA004BA90B");
        public static Guid CLSID_InternetZoneManager = new Guid("7B8A2D95-0AC9-11D1-896C-00C04FB6BFC4");
        public static Guid CGID_ShellDocView = new Guid("000214D1-0000-0000-C000-000000000046");
        //SID_SDownloadManager same as IID
        public static Guid SID_SDownloadManager = new Guid("988934A4-064B-11D3-BB80-00104B35E7F9");
        public static Guid IID_IDownloadManager = new Guid("988934A4-064B-11D3-BB80-00104B35E7F9");
        public static Guid IID_IHttpNegotiate = new Guid("79eac9d2-baf9-11ce-8c82-00aa004ba90b");
        public static Guid IID_IStream = new Guid("0000000c-0000-0000-C000-000000000046");
        public static Guid DIID_HTMLDocumentEvents2 = new Guid("3050f613-98b5-11cf-bb82-00aa00bdce0b");
        public static Guid DIID_HTMLWindowEvents2 = new Guid("3050f625-98b5-11cf-bb82-00aa00bdce0b");
        public static Guid DIID_HTMLElementEvents2 = new Guid("3050f60f-98b5-11cf-bb82-00aa00bdce0b");
        public static Guid IID_IDataObject = new Guid("0000010e-0000-0000-C000-000000000046");
        public static Guid CLSID_InternetShortcut = new Guid("FBF23B40-E3F0-101B-8488-00AA003E56F8");
        public static Guid IID_IUniformResourceLocatorA = new Guid("FBF23B80-E3F0-101B-8488-00AA003E56F8");
        public static Guid IID_IUniformResourceLocatorW = new Guid("CABB0DA0-DA57-11CF-9974-0020AFD79762");
        public static Guid IID_IHTMLBodyElement = new Guid("3050F1D8-98B5-11CF-BB82-00AA00BDCE0B");
        public static Guid CLSID_CUrlHistory = new Guid("3C374A40-BAE4-11CF-BF7D-00AA006946EE");
        public static Guid CLSID_HTMLDocument = new Guid("25336920-03F9-11cf-8FD0-00AA00686F13");
        public static Guid IID_IPropertyNotifySink = new Guid("9BFBBC02-EFF1-101A-84ED-00AA00341D07");
        public static Guid IID_IProtectFocus = new Guid("D81F90A3-8156-44F7-AD28-5ABB87003274");
        public static Guid IID_IHTMLOMWindowServices = new Guid("3050f5fc-98b5-11cf-bb82-00aa00bdce0b");
    }

    public sealed class HTMLDispIDs
    {
        //useful DISPIDs
        public const int DISPID_UNKNOWN = -1;

        //The original value -2147418111 was incorrect
        //0x80010000 = -2147418112 = &H80010000
        public const int DISPID_XOBJ_MIN = -2147418112;

        //0x8001FFFF
        public const int DISPID_XOBJ_MAX = -2147352577;
        public const int DISPID_XOBJ_BASE = DISPID_XOBJ_MIN;
        public const int DISPID_HTMLOBJECT = (DISPID_XOBJ_BASE + 500);
        public const int DISPID_ELEMENT = (DISPID_HTMLOBJECT + 500);
        public const int DISPID_SITE = (DISPID_ELEMENT + 1000);
        public const int DISPID_OBJECT = (DISPID_SITE + 1000);
        public const int DISPID_STYLE = (DISPID_OBJECT + 1000);
        public const int DISPID_ATTRS = (DISPID_STYLE + 1000);
        public const int DISPID_EVENTS = (DISPID_ATTRS + 1000);
        public const int DISPID_XOBJ_EXPANDO = (DISPID_EVENTS + 1000);
        public const int DISPID_XOBJ_ORDINAL = (DISPID_XOBJ_EXPANDO + 1000);

        public const int DISPID_AMBIENT_DLCONTROL = -5512;

        public const int STDDISPID_XOBJ_ONBLUR = (DISPID_XOBJ_BASE);
        public const int STDDISPID_XOBJ_ONFOCUS = (DISPID_XOBJ_BASE + 1);
        public const int STDDISPID_XOBJ_BEFOREUPDATE = (DISPID_XOBJ_BASE + 4);
        public const int STDDISPID_XOBJ_AFTERUPDATE = (DISPID_XOBJ_BASE + 5);
        public const int STDDISPID_XOBJ_ONROWEXIT = (DISPID_XOBJ_BASE + 6);
        public const int STDDISPID_XOBJ_ONROWENTER = (DISPID_XOBJ_BASE + 7);
        public const int STDDISPID_XOBJ_ONMOUSEOVER = (DISPID_XOBJ_BASE + 8);
        public const int STDDISPID_XOBJ_ONMOUSEOUT = (DISPID_XOBJ_BASE + 9);
        public const int STDDISPID_XOBJ_ONHELP = (DISPID_XOBJ_BASE + 10);
        public const int STDDISPID_XOBJ_ONDRAGSTART = (DISPID_XOBJ_BASE + 11);
        public const int STDDISPID_XOBJ_ONSELECTSTART = (DISPID_XOBJ_BASE + 12);
        public const int STDDISPID_XOBJ_ERRORUPDATE = (DISPID_XOBJ_BASE + 13);
        public const int STDDISPID_XOBJ_ONDATASETCHANGED = (DISPID_XOBJ_BASE + 14);
        public const int STDDISPID_XOBJ_ONDATAAVAILABLE = (DISPID_XOBJ_BASE + 15);
        public const int STDDISPID_XOBJ_ONDATASETCOMPLETE = (DISPID_XOBJ_BASE + 16);
        public const int STDDISPID_XOBJ_ONFILTER = (DISPID_XOBJ_BASE + 17);
        public const int STDDISPID_XOBJ_ONLOSECAPTURE = (DISPID_XOBJ_BASE + 18);
        public const int STDDISPID_XOBJ_ONPROPERTYCHANGE = (DISPID_XOBJ_BASE + 19);
        public const int STDDISPID_XOBJ_ONDRAG = (DISPID_XOBJ_BASE + 20);
        public const int STDDISPID_XOBJ_ONDRAGEND = (DISPID_XOBJ_BASE + 21);
        public const int STDDISPID_XOBJ_ONDRAGENTER = (DISPID_XOBJ_BASE + 22);
        public const int STDDISPID_XOBJ_ONDRAGOVER = (DISPID_XOBJ_BASE + 23);
        public const int STDDISPID_XOBJ_ONDRAGLEAVE = (DISPID_XOBJ_BASE + 24);
        public const int STDDISPID_XOBJ_ONDROP = (DISPID_XOBJ_BASE + 25);
        public const int STDDISPID_XOBJ_ONCUT = (DISPID_XOBJ_BASE + 26);
        public const int STDDISPID_XOBJ_ONCOPY = (DISPID_XOBJ_BASE + 27);
        public const int STDDISPID_XOBJ_ONPASTE = (DISPID_XOBJ_BASE + 28);
        public const int STDDISPID_XOBJ_ONBEFORECUT = (DISPID_XOBJ_BASE + 29);
        public const int STDDISPID_XOBJ_ONBEFORECOPY = (DISPID_XOBJ_BASE + 30);
        public const int STDDISPID_XOBJ_ONBEFOREPASTE = (DISPID_XOBJ_BASE + 31);
        public const int STDDISPID_XOBJ_ONROWSDELETE = (DISPID_XOBJ_BASE + 32);
        public const int STDDISPID_XOBJ_ONROWSINSERTED = (DISPID_XOBJ_BASE + 33);
        public const int STDDISPID_XOBJ_ONCELLCHANGE = (DISPID_XOBJ_BASE + 34);
        public const int STDPROPID_XOBJ_DISABLED = (DISPID_XOBJ_BASE + 0x4C); //+76
        public const int DISPID_DEFAULTVALUE = (DISPID_A_FIRST + 83);

        public const int DISPID_CLICK = (-600);
        public const int DISPID_DBLCLICK = (-601);
        public const int DISPID_KEYDOWN = (-602);
        public const int DISPID_KEYPRESS = (-603);
        public const int DISPID_KEYUP = (-604);
        public const int DISPID_MOUSEDOWN = (-605);
        public const int DISPID_MOUSEMOVE = (-606);
        public const int DISPID_MOUSEUP = (-607);
        public const int DISPID_ERROREVENT = (-608);
        public const int DISPID_READYSTATECHANGE = (-609);
        public const int DISPID_CLICK_VALUE = (-610);
        public const int DISPID_RIGHTTOLEFT = (-611);
        public const int DISPID_TOPTOBOTTOM = (-612);
        public const int DISPID_THIS = (-613);

        //  Standard dispatch ID constants

        public const int DISPID_AUTOSIZE = (-500);
        public const int DISPID_BACKCOLOR = (-501);
        public const int DISPID_BACKSTYLE = (-502);
        public const int DISPID_BORDERCOLOR = (-503);
        public const int DISPID_BORDERSTYLE = (-504);
        public const int DISPID_BORDERWIDTH = (-505);
        public const int DISPID_DRAWMODE = (-507);
        public const int DISPID_DRAWSTYLE = (-508);
        public const int DISPID_DRAWWIDTH = (-509);
        public const int DISPID_FILLCOLOR = (-510);
        public const int DISPID_FILLSTYLE = (-511);
        public const int DISPID_FONT = (-512);
        public const int DISPID_FORECOLOR = (-513);
        public const int DISPID_ENABLED = (-514);
        public const int DISPID_HWND = (-515);
        public const int DISPID_TABSTOP = (-516);
        public const int DISPID_TEXT = (-517);
        public const int DISPID_CAPTION = (-518);
        public const int DISPID_BORDERVISIBLE = (-519);
        public const int DISPID_APPEARANCE = (-520);
        public const int DISPID_MOUSEPOINTER = (-521);
        public const int DISPID_MOUSEICON = (-522);
        public const int DISPID_PICTURE = (-523);
        public const int DISPID_VALID = (-524);
        public const int DISPID_READYSTATE = (-525);
        public const int DISPID_LISTINDEX = (-526);
        public const int DISPID_SELECTED = (-527);
        public const int DISPID_LIST = (-528);
        public const int DISPID_COLUMN = (-529);
        public const int DISPID_LISTCOUNT = (-531);
        public const int DISPID_MULTISELECT = (-532);
        public const int DISPID_MAXLENGTH = (-533);
        public const int DISPID_PASSWORDCHAR = (-534);
        public const int DISPID_SCROLLBARS = (-535);
        public const int DISPID_WORDWRAP = (-536);
        public const int DISPID_MULTILINE = (-537);
        public const int DISPID_NUMBEROFROWS = (-538);
        public const int DISPID_NUMBEROFCOLUMNS = (-539);
        public const int DISPID_DISPLAYSTYLE = (-540);
        public const int DISPID_GROUPNAME = (-541);
        public const int DISPID_IMEMODE = (-542);
        public const int DISPID_ACCELERATOR = (-543);
        public const int DISPID_ENTERKEYBEHAVIOR = (-544);
        public const int DISPID_TABKEYBEHAVIOR = (-545);
        public const int DISPID_SELTEXT = (-546);
        public const int DISPID_SELSTART = (-547);
        public const int DISPID_SELLENGTH = (-548);

        public const int DISPID_REFRESH = (-550);
        public const int DISPID_DOCLICK = (-551);
        public const int DISPID_ABOUTBOX = (-552);
        public const int DISPID_ADDITEM = (-553);
        public const int DISPID_CLEAR = (-554);
        public const int DISPID_REMOVEITEM = (-555);
        public const int DISPID_NORMAL_FIRST = 1000;

        public const int DISPID_ONABORT = (DISPID_NORMAL_FIRST);
        public const int DISPID_ONCHANGE = (DISPID_NORMAL_FIRST + 1);
        public const int DISPID_ONERROR = (DISPID_NORMAL_FIRST + 2);
        public const int DISPID_ONLOAD = (DISPID_NORMAL_FIRST + 3);
        public const int DISPID_ONSELECT = (DISPID_NORMAL_FIRST + 6);
        public const int DISPID_ONSUBMIT = (DISPID_NORMAL_FIRST + 7);
        public const int DISPID_ONUNLOAD = (DISPID_NORMAL_FIRST + 8);
        public const int DISPID_ONBOUNCE = (DISPID_NORMAL_FIRST + 9);
        public const int DISPID_ONFINISH = (DISPID_NORMAL_FIRST + 10);
        public const int DISPID_ONSTART = (DISPID_NORMAL_FIRST + 11);
        public const int DISPID_ONLAYOUT = (DISPID_NORMAL_FIRST + 13);
        public const int DISPID_ONSCROLL = (DISPID_NORMAL_FIRST + 14);
        public const int DISPID_ONRESET = (DISPID_NORMAL_FIRST + 15);
        public const int DISPID_ONRESIZE = (DISPID_NORMAL_FIRST + 16);
        public const int DISPID_ONBEFOREUNLOAD = (DISPID_NORMAL_FIRST + 17);
        public const int DISPID_ONCHANGEFOCUS = (DISPID_NORMAL_FIRST + 18);
        public const int DISPID_ONCHANGEBLUR = (DISPID_NORMAL_FIRST + 19);
        public const int DISPID_ONPERSIST = (DISPID_NORMAL_FIRST + 20);
        public const int DISPID_ONPERSISTSAVE = (DISPID_NORMAL_FIRST + 21);
        public const int DISPID_ONPERSISTLOAD = (DISPID_NORMAL_FIRST + 22);
        public const int DISPID_ONCONTEXTMENU = (DISPID_NORMAL_FIRST + 23);
        public const int DISPID_ONBEFOREPRINT = (DISPID_NORMAL_FIRST + 24);
        public const int DISPID_ONAFTERPRINT = (DISPID_NORMAL_FIRST + 25);
        public const int DISPID_ONSTOP = (DISPID_NORMAL_FIRST + 26);
        public const int DISPID_ONBEFOREEDITFOCUS = (DISPID_NORMAL_FIRST + 27);
        public const int DISPID_ONMOUSEHOVER = (DISPID_NORMAL_FIRST + 28);
        public const int DISPID_ONCONTENTREADY = (DISPID_NORMAL_FIRST + 29);
        public const int DISPID_ONLAYOUTCOMPLETE = (DISPID_NORMAL_FIRST + 30);
        public const int DISPID_ONPAGE = (DISPID_NORMAL_FIRST + 31);
        public const int DISPID_ONLINKEDOVERFLOW = (DISPID_NORMAL_FIRST + 32);
        public const int DISPID_ONMOUSEWHEEL = (DISPID_NORMAL_FIRST + 33);
        public const int DISPID_ONBEFOREDEACTIVATE = (DISPID_NORMAL_FIRST + 34);
        public const int DISPID_ONMOVE = (DISPID_NORMAL_FIRST + 35);
        public const int DISPID_ONCONTROLSELECT = (DISPID_NORMAL_FIRST + 36);
        public const int DISPID_ONSELECTIONCHANGE = (DISPID_NORMAL_FIRST + 37);
        public const int DISPID_ONMOVESTART = (DISPID_NORMAL_FIRST + 38);
        public const int DISPID_ONMOVEEND = (DISPID_NORMAL_FIRST + 39);
        public const int DISPID_ONRESIZESTART = (DISPID_NORMAL_FIRST + 40);
        public const int DISPID_ONRESIZEEND = (DISPID_NORMAL_FIRST + 41);
        public const int DISPID_ONMOUSEENTER = (DISPID_NORMAL_FIRST + 42);
        public const int DISPID_ONMOUSELEAVE = (DISPID_NORMAL_FIRST + 43);
        public const int DISPID_ONACTIVATE = (DISPID_NORMAL_FIRST + 44);
        public const int DISPID_ONDEACTIVATE = (DISPID_NORMAL_FIRST + 45);
        public const int DISPID_ONMULTILAYOUTCLEANUP = (DISPID_NORMAL_FIRST + 46);
        public const int DISPID_ONBEFOREACTIVATE = (DISPID_NORMAL_FIRST + 47);
        public const int DISPID_ONFOCUSIN = (DISPID_NORMAL_FIRST + 48);
        public const int DISPID_ONFOCUSOUT = (DISPID_NORMAL_FIRST + 49);

        public const int DISPID_A_UNICODEBIDI = (DISPID_A_FIRST + 118); // Complex Text support for CSS2 unicode-bidi
        public const int DISPID_A_DIRECTION = (DISPID_A_FIRST + 119); // Complex Text support for CSS2 direction

        public const int DISPID_EVPROP_ONMOUSEOVER = (DISPID_EVENTS + 0);
        public const int DISPID_EVMETH_ONMOUSEOVER = STDDISPID_XOBJ_ONMOUSEOVER;
        public const int DISPID_EVPROP_ONMOUSEOUT = (DISPID_EVENTS + 1);
        public const int DISPID_EVMETH_ONMOUSEOUT = STDDISPID_XOBJ_ONMOUSEOUT;
        public const int DISPID_EVPROP_ONMOUSEDOWN = (DISPID_EVENTS + 2);
        public const int DISPID_EVMETH_ONMOUSEDOWN = DISPID_MOUSEDOWN;
        public const int DISPID_EVPROP_ONMOUSEUP = (DISPID_EVENTS + 3);
        public const int DISPID_EVMETH_ONMOUSEUP = DISPID_MOUSEUP;
        public const int DISPID_EVPROP_ONMOUSEMOVE = (DISPID_EVENTS + 4);
        public const int DISPID_EVMETH_ONMOUSEMOVE = DISPID_MOUSEMOVE;
        public const int DISPID_EVPROP_ONKEYDOWN = (DISPID_EVENTS + 5);
        public const int DISPID_EVMETH_ONKEYDOWN = DISPID_KEYDOWN;
        public const int DISPID_EVPROP_ONKEYUP = (DISPID_EVENTS + 6);
        public const int DISPID_EVMETH_ONKEYUP = DISPID_KEYUP;
        public const int DISPID_EVPROP_ONKEYPRESS = (DISPID_EVENTS + 7);
        public const int DISPID_EVMETH_ONKEYPRESS = DISPID_KEYPRESS;
        public const int DISPID_EVPROP_ONCLICK = (DISPID_EVENTS + 8);
        public const int DISPID_EVMETH_ONCLICK = DISPID_CLICK;
        public const int DISPID_EVPROP_ONDBLCLICK = (DISPID_EVENTS + 9);
        public const int DISPID_EVMETH_ONDBLCLICK = DISPID_DBLCLICK;
        public const int DISPID_EVPROP_ONSELECT = (DISPID_EVENTS + 10);
        public const int DISPID_EVMETH_ONSELECT = DISPID_ONSELECT;
        public const int DISPID_EVPROP_ONSUBMIT = (DISPID_EVENTS + 11);
        public const int DISPID_EVMETH_ONSUBMIT = DISPID_ONSUBMIT;
        public const int DISPID_EVPROP_ONRESET = (DISPID_EVENTS + 12);
        public const int DISPID_EVMETH_ONRESET = DISPID_ONRESET;
        public const int DISPID_EVPROP_ONHELP = (DISPID_EVENTS + 13);
        public const int DISPID_EVMETH_ONHELP = STDDISPID_XOBJ_ONHELP;
        public const int DISPID_EVPROP_ONFOCUS = (DISPID_EVENTS + 14);
        public const int DISPID_EVMETH_ONFOCUS = STDDISPID_XOBJ_ONFOCUS;
        public const int DISPID_EVPROP_ONBLUR = (DISPID_EVENTS + 15);
        public const int DISPID_EVMETH_ONBLUR = STDDISPID_XOBJ_ONBLUR;
        public const int DISPID_EVPROP_ONROWEXIT = (DISPID_EVENTS + 18);
        public const int DISPID_EVMETH_ONROWEXIT = STDDISPID_XOBJ_ONROWEXIT;
        public const int DISPID_EVPROP_ONROWENTER = (DISPID_EVENTS + 19);
        public const int DISPID_EVMETH_ONROWENTER = STDDISPID_XOBJ_ONROWENTER;
        public const int DISPID_EVPROP_ONBOUNCE = (DISPID_EVENTS + 20);
        public const int DISPID_EVMETH_ONBOUNCE = DISPID_ONBOUNCE;
        public const int DISPID_EVPROP_ONBEFOREUPDATE = (DISPID_EVENTS + 21);
        public const int DISPID_EVMETH_ONBEFOREUPDATE = STDDISPID_XOBJ_BEFOREUPDATE;
        public const int DISPID_EVPROP_ONAFTERUPDATE = (DISPID_EVENTS + 22);
        public const int DISPID_EVMETH_ONAFTERUPDATE = STDDISPID_XOBJ_AFTERUPDATE;
        public const int DISPID_EVPROP_ONBEFOREDRAGOVER = (DISPID_EVENTS + 23);
        //public const int  DISPID_EVMETH_ONBEFOREDRAGOVER =  EVENTID_CommonCtrlEvent_BeforeDragOver;
        public const int DISPID_EVPROP_ONBEFOREDROPORPASTE = (DISPID_EVENTS + 24);
        //public const int  DISPID_EVMETH_ONBEFOREDROPORPASTE = EVENTID_CommonCtrlEvent_BeforeDropOrPaste;
        public const int DISPID_EVPROP_ONREADYSTATECHANGE = (DISPID_EVENTS + 25);
        public const int DISPID_EVMETH_ONREADYSTATECHANGE = DISPID_READYSTATECHANGE;
        public const int DISPID_EVPROP_ONFINISH = (DISPID_EVENTS + 26);
        public const int DISPID_EVMETH_ONFINISH = DISPID_ONFINISH;
        public const int DISPID_EVPROP_ONSTART = (DISPID_EVENTS + 27);
        public const int DISPID_EVMETH_ONSTART = DISPID_ONSTART;
        public const int DISPID_EVPROP_ONABORT = (DISPID_EVENTS + 28);
        public const int DISPID_EVMETH_ONABORT = DISPID_ONABORT;
        public const int DISPID_EVPROP_ONERROR = (DISPID_EVENTS + 29);
        public const int DISPID_EVMETH_ONERROR = DISPID_ONERROR;
        public const int DISPID_EVPROP_ONCHANGE = (DISPID_EVENTS + 30);
        public const int DISPID_EVMETH_ONCHANGE = DISPID_ONCHANGE;
        public const int DISPID_EVPROP_ONSCROLL = (DISPID_EVENTS + 31);
        public const int DISPID_EVMETH_ONSCROLL = DISPID_ONSCROLL;
        public const int DISPID_EVPROP_ONLOAD = (DISPID_EVENTS + 32);
        public const int DISPID_EVMETH_ONLOAD = DISPID_ONLOAD;
        public const int DISPID_EVPROP_ONUNLOAD = (DISPID_EVENTS + 33);
        public const int DISPID_EVMETH_ONUNLOAD = DISPID_ONUNLOAD;
        public const int DISPID_EVPROP_ONLAYOUT = (DISPID_EVENTS + 34);
        public const int DISPID_EVMETH_ONLAYOUT = DISPID_ONLAYOUT;
        public const int DISPID_EVPROP_ONDRAGSTART = (DISPID_EVENTS + 35);
        public const int DISPID_EVMETH_ONDRAGSTART = STDDISPID_XOBJ_ONDRAGSTART;
        public const int DISPID_EVPROP_ONRESIZE = (DISPID_EVENTS + 36);
        public const int DISPID_EVMETH_ONRESIZE = DISPID_ONRESIZE;
        public const int DISPID_EVPROP_ONSELECTSTART = (DISPID_EVENTS + 37);
        public const int DISPID_EVMETH_ONSELECTSTART = STDDISPID_XOBJ_ONSELECTSTART;
        public const int DISPID_EVPROP_ONERRORUPDATE = (DISPID_EVENTS + 38);
        public const int DISPID_EVMETH_ONERRORUPDATE = STDDISPID_XOBJ_ERRORUPDATE;
        public const int DISPID_EVPROP_ONBEFOREUNLOAD = (DISPID_EVENTS + 39);
        // <summary>
        /// 
        /// </summary>
        //public const int  DISPID_EVMETH_ONBEFOREUNLOAD  = DISPID_ONBEFOREUNLOAD;
        public const int DISPID_EVPROP_ONDATASETCHANGED = (DISPID_EVENTS + 40);
        public const int DISPID_EVMETH_ONDATASETCHANGED = STDDISPID_XOBJ_ONDATASETCHANGED;
        public const int DISPID_EVPROP_ONDATAAVAILABLE = (DISPID_EVENTS + 41);
        public const int DISPID_EVMETH_ONDATAAVAILABLE = STDDISPID_XOBJ_ONDATAAVAILABLE;
        public const int DISPID_EVPROP_ONDATASETCOMPLETE = (DISPID_EVENTS + 42);
        public const int DISPID_EVMETH_ONDATASETCOMPLETE = STDDISPID_XOBJ_ONDATASETCOMPLETE;
        public const int DISPID_EVPROP_ONFILTER = (DISPID_EVENTS + 43);
        public const int DISPID_EVMETH_ONFILTER = STDDISPID_XOBJ_ONFILTER;
        public const int DISPID_EVPROP_ONCHANGEFOCUS = (DISPID_EVENTS + 44);
        public const int DISPID_EVMETH_ONCHANGEFOCUS = DISPID_ONCHANGEFOCUS;
        public const int DISPID_EVPROP_ONCHANGEBLUR = (DISPID_EVENTS + 45);
        public const int DISPID_EVMETH_ONCHANGEBLUR = DISPID_ONCHANGEBLUR;
        public const int DISPID_EVPROP_ONLOSECAPTURE = (DISPID_EVENTS + 46);
        public const int DISPID_EVMETH_ONLOSECAPTURE = STDDISPID_XOBJ_ONLOSECAPTURE;
        public const int DISPID_EVPROP_ONPROPERTYCHANGE = (DISPID_EVENTS + 47);
        public const int DISPID_EVMETH_ONPROPERTYCHANGE = STDDISPID_XOBJ_ONPROPERTYCHANGE;
        public const int DISPID_EVPROP_ONPERSISTSAVE = (DISPID_EVENTS + 48);
        public const int DISPID_EVMETH_ONPERSISTSAVE = DISPID_ONPERSISTSAVE;
        public const int DISPID_EVPROP_ONDRAG = (DISPID_EVENTS + 49);
        public const int DISPID_EVMETH_ONDRAG = STDDISPID_XOBJ_ONDRAG;
        public const int DISPID_EVPROP_ONDRAGEND = (DISPID_EVENTS + 50);
        public const int DISPID_EVMETH_ONDRAGEND = STDDISPID_XOBJ_ONDRAGEND;
        public const int DISPID_EVPROP_ONDRAGENTER = (DISPID_EVENTS + 51);
        public const int DISPID_EVMETH_ONDRAGENTER = STDDISPID_XOBJ_ONDRAGENTER;
        public const int DISPID_EVPROP_ONDRAGOVER = (DISPID_EVENTS + 52);
        public const int DISPID_EVMETH_ONDRAGOVER = STDDISPID_XOBJ_ONDRAGOVER;
        public const int DISPID_EVPROP_ONDRAGLEAVE = (DISPID_EVENTS + 53);
        public const int DISPID_EVMETH_ONDRAGLEAVE = STDDISPID_XOBJ_ONDRAGLEAVE;
        public const int DISPID_EVPROP_ONDROP = (DISPID_EVENTS + 54);
        public const int DISPID_EVMETH_ONDROP = STDDISPID_XOBJ_ONDROP;
        public const int DISPID_EVPROP_ONCUT = (DISPID_EVENTS + 55);
        public const int DISPID_EVMETH_ONCUT = STDDISPID_XOBJ_ONCUT;
        public const int DISPID_EVPROP_ONCOPY = (DISPID_EVENTS + 56);
        public const int DISPID_EVMETH_ONCOPY = STDDISPID_XOBJ_ONCOPY;
        public const int DISPID_EVPROP_ONPASTE = (DISPID_EVENTS + 57);
        public const int DISPID_EVMETH_ONPASTE = STDDISPID_XOBJ_ONPASTE;
        public const int DISPID_EVPROP_ONBEFORECUT = (DISPID_EVENTS + 58);
        public const int DISPID_EVMETH_ONBEFORECUT = STDDISPID_XOBJ_ONBEFORECUT;
        public const int DISPID_EVPROP_ONBEFORECOPY = (DISPID_EVENTS + 59);
        public const int DISPID_EVMETH_ONBEFORECOPY = STDDISPID_XOBJ_ONBEFORECOPY;
        public const int DISPID_EVPROP_ONBEFOREPASTE = (DISPID_EVENTS + 60);
        public const int DISPID_EVMETH_ONBEFOREPASTE = STDDISPID_XOBJ_ONBEFOREPASTE;
        public const int DISPID_EVPROP_ONPERSISTLOAD = (DISPID_EVENTS + 61);
        public const int DISPID_EVMETH_ONPERSISTLOAD = DISPID_ONPERSISTLOAD;
        public const int DISPID_EVPROP_ONROWSDELETE = (DISPID_EVENTS + 62);
        public const int DISPID_EVMETH_ONROWSDELETE = STDDISPID_XOBJ_ONROWSDELETE;
        public const int DISPID_EVPROP_ONROWSINSERTED = (DISPID_EVENTS + 63);
        public const int DISPID_EVMETH_ONROWSINSERTED = STDDISPID_XOBJ_ONROWSINSERTED;
        public const int DISPID_EVPROP_ONCELLCHANGE = (DISPID_EVENTS + 64);
        public const int DISPID_EVMETH_ONCELLCHANGE = STDDISPID_XOBJ_ONCELLCHANGE;
        public const int DISPID_EVPROP_ONCONTEXTMENU = (DISPID_EVENTS + 65);
        public const int DISPID_EVMETH_ONCONTEXTMENU = DISPID_ONCONTEXTMENU;
        public const int DISPID_EVPROP_ONBEFOREPRINT = (DISPID_EVENTS + 66);
        public const int DISPID_EVMETH_ONBEFOREPRINT = DISPID_ONBEFOREPRINT;
        public const int DISPID_EVPROP_ONAFTERPRINT = (DISPID_EVENTS + 67);
        public const int DISPID_EVMETH_ONAFTERPRINT = DISPID_ONAFTERPRINT;
        public const int DISPID_EVPROP_ONSTOP = (DISPID_EVENTS + 68);
        public const int DISPID_EVMETH_ONSTOP = DISPID_ONSTOP;
        public const int DISPID_EVPROP_ONBEFOREEDITFOCUS = (DISPID_EVENTS + 69);
        public const int DISPID_EVMETH_ONBEFOREEDITFOCUS = DISPID_ONBEFOREEDITFOCUS;
        public const int DISPID_EVPROP_ONATTACHEVENT = (DISPID_EVENTS + 70);
        public const int DISPID_EVPROP_ONMOUSEHOVER = (DISPID_EVENTS + 71);
        public const int DISPID_EVMETH_ONMOUSEHOVER = DISPID_ONMOUSEHOVER;
        public const int DISPID_EVPROP_ONCONTENTREADY = (DISPID_EVENTS + 72);
        public const int DISPID_EVMETH_ONCONTENTREADY = DISPID_ONCONTENTREADY;
        public const int DISPID_EVPROP_ONLAYOUTCOMPLETE = (DISPID_EVENTS + 73);
        public const int DISPID_EVMETH_ONLAYOUTCOMPLETE = DISPID_ONLAYOUTCOMPLETE;
        public const int DISPID_EVPROP_ONPAGE = (DISPID_EVENTS + 74);
        public const int DISPID_EVMETH_ONPAGE = DISPID_ONPAGE;
        public const int DISPID_EVPROP_ONLINKEDOVERFLOW = (DISPID_EVENTS + 75);
        public const int DISPID_EVMETH_ONLINKEDOVERFLOW = DISPID_ONLINKEDOVERFLOW;
        public const int DISPID_EVPROP_ONMOUSEWHEEL = (DISPID_EVENTS + 76);
        public const int DISPID_EVMETH_ONMOUSEWHEEL = DISPID_ONMOUSEWHEEL;
        public const int DISPID_EVPROP_ONBEFOREDEACTIVATE = (DISPID_EVENTS + 77);
        public const int DISPID_EVMETH_ONBEFOREDEACTIVATE = DISPID_ONBEFOREDEACTIVATE;
        public const int DISPID_EVPROP_ONMOVE = (DISPID_EVENTS + 78);
        public const int DISPID_EVMETH_ONMOVE = DISPID_ONMOVE;
        public const int DISPID_EVPROP_ONCONTROLSELECT = (DISPID_EVENTS + 79);
        public const int DISPID_EVMETH_ONCONTROLSELECT = DISPID_ONCONTROLSELECT;
        public const int DISPID_EVPROP_ONSELECTIONCHANGE = (DISPID_EVENTS + 80);
        public const int DISPID_EVMETH_ONSELECTIONCHANGE = DISPID_ONSELECTIONCHANGE;
        public const int DISPID_EVPROP_ONMOVESTART = (DISPID_EVENTS + 81);
        public const int DISPID_EVMETH_ONMOVESTART = DISPID_ONMOVESTART;
        public const int DISPID_EVPROP_ONMOVEEND = (DISPID_EVENTS + 82);
        public const int DISPID_EVMETH_ONMOVEEND = DISPID_ONMOVEEND;
        public const int DISPID_EVPROP_ONRESIZESTART = (DISPID_EVENTS + 83);
        public const int DISPID_EVMETH_ONRESIZESTART = DISPID_ONRESIZESTART;
        public const int DISPID_EVPROP_ONRESIZEEND = (DISPID_EVENTS + 84);
        public const int DISPID_EVMETH_ONRESIZEEND = DISPID_ONRESIZEEND;
        public const int DISPID_EVPROP_ONMOUSEENTER = (DISPID_EVENTS + 85);
        public const int DISPID_EVMETH_ONMOUSEENTER = DISPID_ONMOUSEENTER;
        public const int DISPID_EVPROP_ONMOUSELEAVE = (DISPID_EVENTS + 86);
        public const int DISPID_EVMETH_ONMOUSELEAVE = DISPID_ONMOUSELEAVE;
        public const int DISPID_EVPROP_ONACTIVATE = (DISPID_EVENTS + 87);
        public const int DISPID_EVMETH_ONACTIVATE = DISPID_ONACTIVATE;
        public const int DISPID_EVPROP_ONDEACTIVATE = (DISPID_EVENTS + 88);
        public const int DISPID_EVMETH_ONDEACTIVATE = DISPID_ONDEACTIVATE;
        public const int DISPID_EVPROP_ONMULTILAYOUTCLEANUP = (DISPID_EVENTS + 89);
        public const int DISPID_EVMETH_ONMULTILAYOUTCLEANUP = DISPID_ONMULTILAYOUTCLEANUP;
        public const int DISPID_EVPROP_ONBEFOREACTIVATE = (DISPID_EVENTS + 90);
        public const int DISPID_EVMETH_ONBEFOREACTIVATE = DISPID_ONBEFOREACTIVATE;
        public const int DISPID_EVPROP_ONFOCUSIN = (DISPID_EVENTS + 91);
        public const int DISPID_EVMETH_ONFOCUSIN = DISPID_ONFOCUSIN;
        public const int DISPID_EVPROP_ONFOCUSOUT = (DISPID_EVENTS + 92);
        public const int DISPID_EVMETH_ONFOCUSOUT = DISPID_ONFOCUSOUT;

        public const int DISPID_EVMETH_ONBEFOREUNLOAD = DISPID_ONBEFOREUNLOAD;

        public const int STDPROPID_XOBJ_CONTROLTIPTEXT = (DISPID_XOBJ_BASE + 0x45);

        public const int DISPID_A_LANGUAGE = (DISPID_A_FIRST + 100);
        public const int DISPID_A_LANG = (DISPID_A_FIRST + 9);
        public const int STDPROPID_XOBJ_PARENT = (DISPID_XOBJ_BASE + 0x8);
        public const int STDPROPID_XOBJ_STYLE = (DISPID_XOBJ_BASE + 0x4A);

        //    DISPIDs for interface IHTMLEventObj4
        public const int DISPID_IHTMLEVENTOBJ4_WHEELDELTA = DISPID_EVENTOBJ + 51;

        public const int DISPID_IHTMLELEMENT_SETATTRIBUTE = DISPID_HTMLOBJECT + 1;
        public const int DISPID_IHTMLELEMENT_GETATTRIBUTE = DISPID_HTMLOBJECT + 2;
        public const int DISPID_IHTMLELEMENT_REMOVEATTRIBUTE = DISPID_HTMLOBJECT + 3;
        public const int DISPID_IHTMLELEMENT_CLASSNAME = DISPID_ELEMENT + 1;
        public const int DISPID_IHTMLELEMENT_ID = DISPID_ELEMENT + 2;
        public const int DISPID_IHTMLELEMENT_TAGNAME = DISPID_ELEMENT + 4;
        public const int DISPID_IHTMLELEMENT_PARENTELEMENT = STDPROPID_XOBJ_PARENT;
        public const int DISPID_IHTMLELEMENT_STYLE = STDPROPID_XOBJ_STYLE;
        public const int DISPID_IHTMLELEMENT_ONHELP = DISPID_EVPROP_ONHELP; //-2147412098
        public const int DISPID_IHTMLELEMENT_ONCLICK = DISPID_EVPROP_ONCLICK; //-2147412103
        public const int DISPID_IHTMLELEMENT_ONDBLCLICK = DISPID_EVPROP_ONDBLCLICK;//-2147412102
        public const int DISPID_IHTMLELEMENT_ONKEYDOWN = DISPID_EVPROP_ONKEYDOWN; //-2147412106
        public const int DISPID_IHTMLELEMENT_ONKEYUP = DISPID_EVPROP_ONKEYUP;
        public const int DISPID_IHTMLELEMENT_ONKEYPRESS = DISPID_EVPROP_ONKEYPRESS; //-2147412104
        public const int DISPID_IHTMLELEMENT_ONMOUSEOUT = DISPID_EVPROP_ONMOUSEOUT; //-2147412110
        public const int DISPID_IHTMLELEMENT_ONMOUSEOVER = DISPID_EVPROP_ONMOUSEOVER; //-2147412111
        public const int DISPID_IHTMLELEMENT_ONMOUSEMOVE = DISPID_EVPROP_ONMOUSEMOVE; // -2147412107
        public const int DISPID_IHTMLELEMENT_ONMOUSEDOWN = DISPID_EVPROP_ONMOUSEDOWN;
        public const int DISPID_IHTMLELEMENT_ONMOUSEUP = DISPID_EVPROP_ONMOUSEUP;
        public const int DISPID_IHTMLELEMENT_DOCUMENT = DISPID_ELEMENT + 18;
        public const int DISPID_IHTMLELEMENT_TITLE = STDPROPID_XOBJ_CONTROLTIPTEXT;
        public const int DISPID_IHTMLELEMENT_LANGUAGE = DISPID_A_LANGUAGE;
        public const int DISPID_IHTMLELEMENT_ONSELECTSTART = DISPID_EVPROP_ONSELECTSTART;
        public const int DISPID_IHTMLELEMENT_SCROLLINTOVIEW = DISPID_ELEMENT + 19;
        public const int DISPID_IHTMLELEMENT_CONTAINS = DISPID_ELEMENT + 20;
        public const int DISPID_IHTMLELEMENT_SOURCEINDEX = DISPID_ELEMENT + 24;
        public const int DISPID_IHTMLELEMENT_RECORDNUMBER = DISPID_ELEMENT + 25;
        public const int DISPID_IHTMLELEMENT_LANG = DISPID_A_LANG;
        public const int DISPID_IHTMLELEMENT_OFFSETLEFT = DISPID_ELEMENT + 8;
        public const int DISPID_IHTMLELEMENT_OFFSETTOP = DISPID_ELEMENT + 9;
        public const int DISPID_IHTMLELEMENT_OFFSETWIDTH = DISPID_ELEMENT + 10;
        public const int DISPID_IHTMLELEMENT_OFFSETHEIGHT = DISPID_ELEMENT + 11;
        public const int DISPID_IHTMLELEMENT_OFFSETPARENT = DISPID_ELEMENT + 12;
        public const int DISPID_IHTMLELEMENT_INNERHTML = DISPID_ELEMENT + 26;
        public const int DISPID_IHTMLELEMENT_INNERTEXT = DISPID_ELEMENT + 27;
        public const int DISPID_IHTMLELEMENT_OUTERHTML = DISPID_ELEMENT + 28;
        public const int DISPID_IHTMLELEMENT_OUTERTEXT = DISPID_ELEMENT + 29;
        public const int DISPID_IHTMLELEMENT_INSERTADJACENTHTML = DISPID_ELEMENT + 30;
        public const int DISPID_IHTMLELEMENT_INSERTADJACENTTEXT = DISPID_ELEMENT + 31;
        public const int DISPID_IHTMLELEMENT_PARENTTEXTEDIT = DISPID_ELEMENT + 32;
        public const int DISPID_IHTMLELEMENT_ISTEXTEDIT = DISPID_ELEMENT + 34;
        public const int DISPID_IHTMLELEMENT_CLICK = DISPID_ELEMENT + 33;
        public const int DISPID_IHTMLELEMENT_FILTERS = DISPID_ELEMENT + 35;
        public const int DISPID_IHTMLELEMENT_ONDRAGSTART = DISPID_EVPROP_ONDRAGSTART;
        public const int DISPID_IHTMLELEMENT_TOSTRING = DISPID_ELEMENT + 36;
        public const int DISPID_IHTMLELEMENT_ONBEFOREUPDATE = DISPID_EVPROP_ONBEFOREUPDATE;
        public const int DISPID_IHTMLELEMENT_ONAFTERUPDATE = DISPID_EVPROP_ONAFTERUPDATE;
        public const int DISPID_IHTMLELEMENT_ONERRORUPDATE = DISPID_EVPROP_ONERRORUPDATE;
        public const int DISPID_IHTMLELEMENT_ONROWEXIT = DISPID_EVPROP_ONROWEXIT;
        public const int DISPID_IHTMLELEMENT_ONROWENTER = DISPID_EVPROP_ONROWENTER;
        public const int DISPID_IHTMLELEMENT_ONDATASETCHANGED = DISPID_EVPROP_ONDATASETCHANGED;
        public const int DISPID_IHTMLELEMENT_ONDATAAVAILABLE = DISPID_EVPROP_ONDATAAVAILABLE;
        public const int DISPID_IHTMLELEMENT_ONDATASETCOMPLETE = DISPID_EVPROP_ONDATASETCOMPLETE;
        public const int DISPID_IHTMLELEMENT_ONFILTERCHANGE = DISPID_EVPROP_ONFILTER;
        public const int DISPID_IHTMLELEMENT_CHILDREN = DISPID_ELEMENT + 37;
        public const int DISPID_IHTMLELEMENT_ALL = DISPID_ELEMENT + 38;

        //  DISPIDs for interface IHTMLElement2

        public const int DISPID_IHTMLELEMENT2_SCOPENAME = DISPID_ELEMENT + 39;
        public const int DISPID_IHTMLELEMENT2_SETCAPTURE = DISPID_ELEMENT + 40;
        public const int DISPID_IHTMLELEMENT2_RELEASECAPTURE = DISPID_ELEMENT + 41;
        public const int DISPID_IHTMLELEMENT2_ONLOSECAPTURE = DISPID_EVPROP_ONLOSECAPTURE;
        public const int DISPID_IHTMLELEMENT2_COMPONENTFROMPOINT = DISPID_ELEMENT + 42;
        public const int DISPID_IHTMLELEMENT2_DOSCROLL = DISPID_ELEMENT + 43;
        public const int DISPID_IHTMLELEMENT2_ONSCROLL = DISPID_EVPROP_ONSCROLL;
        public const int DISPID_IHTMLELEMENT2_ONDRAG = DISPID_EVPROP_ONDRAG;
        public const int DISPID_IHTMLELEMENT2_ONDRAGEND = DISPID_EVPROP_ONDRAGEND;
        public const int DISPID_IHTMLELEMENT2_ONDRAGENTER = DISPID_EVPROP_ONDRAGENTER;
        public const int DISPID_IHTMLELEMENT2_ONDRAGOVER = DISPID_EVPROP_ONDRAGOVER;
        public const int DISPID_IHTMLELEMENT2_ONDRAGLEAVE = DISPID_EVPROP_ONDRAGLEAVE;
        public const int DISPID_IHTMLELEMENT2_ONDROP = DISPID_EVPROP_ONDROP;
        public const int DISPID_IHTMLELEMENT2_ONBEFORECUT = DISPID_EVPROP_ONBEFORECUT;
        public const int DISPID_IHTMLELEMENT2_ONCUT = DISPID_EVPROP_ONCUT;
        public const int DISPID_IHTMLELEMENT2_ONBEFORECOPY = DISPID_EVPROP_ONBEFORECOPY;
        public const int DISPID_IHTMLELEMENT2_ONCOPY = DISPID_EVPROP_ONCOPY;
        public const int DISPID_IHTMLELEMENT2_ONBEFOREPASTE = DISPID_EVPROP_ONBEFOREPASTE;
        public const int DISPID_IHTMLELEMENT2_ONPASTE = DISPID_EVPROP_ONPASTE;
        public const int DISPID_IHTMLELEMENT2_CURRENTSTYLE = DISPID_ELEMENT + 7;
        public const int DISPID_IHTMLELEMENT2_ONPROPERTYCHANGE = DISPID_EVPROP_ONPROPERTYCHANGE;
        public const int DISPID_IHTMLELEMENT2_GETCLIENTRECTS = DISPID_ELEMENT + 44;
        public const int DISPID_IHTMLELEMENT2_GETBOUNDINGCLIENTRECT = DISPID_ELEMENT + 45;
        public const int DISPID_IHTMLELEMENT2_SETEXPRESSION = DISPID_HTMLOBJECT + 4;
        public const int DISPID_IHTMLELEMENT2_GETEXPRESSION = DISPID_HTMLOBJECT + 5;
        public const int DISPID_IHTMLELEMENT2_REMOVEEXPRESSION = DISPID_HTMLOBJECT + 6;
        //public const int  DISPID_IHTMLELEMENT2_TABINDEX  = STDPROPID_XOBJ_TABINDEX;
        public const int DISPID_IHTMLELEMENT2_FOCUS = DISPID_SITE + 0;
        public const int DISPID_IHTMLELEMENT2_ACCESSKEY = DISPID_SITE + 5;
        public const int DISPID_IHTMLELEMENT2_ONBLUR = DISPID_EVPROP_ONBLUR;
        public const int DISPID_IHTMLELEMENT2_ONFOCUS = DISPID_EVPROP_ONFOCUS;
        public const int DISPID_IHTMLELEMENT2_ONRESIZE = DISPID_EVPROP_ONRESIZE;
        public const int DISPID_IHTMLELEMENT2_BLUR = DISPID_SITE + 2;
        public const int DISPID_IHTMLELEMENT2_ADDFILTER = DISPID_SITE + 17;
        public const int DISPID_IHTMLELEMENT2_REMOVEFILTER = DISPID_SITE + 18;
        public const int DISPID_IHTMLELEMENT2_CLIENTHEIGHT = DISPID_SITE + 19;
        public const int DISPID_IHTMLELEMENT2_CLIENTWIDTH = DISPID_SITE + 20;
        public const int DISPID_IHTMLELEMENT2_CLIENTTOP = DISPID_SITE + 21;
        public const int DISPID_IHTMLELEMENT2_CLIENTLEFT = DISPID_SITE + 22;
        public const int DISPID_IHTMLELEMENT2_ATTACHEVENT = DISPID_HTMLOBJECT + 7;
        public const int DISPID_IHTMLELEMENT2_DETACHEVENT = DISPID_HTMLOBJECT + 8;
        //public const int  DISPID_IHTMLELEMENT2_READYSTATE  = DISPID_A_READYSTATE;
        public const int DISPID_IHTMLELEMENT2_ONREADYSTATECHANGE = DISPID_EVPROP_ONREADYSTATECHANGE;
        public const int DISPID_IHTMLELEMENT2_ONROWSDELETE = DISPID_EVPROP_ONROWSDELETE;
        public const int DISPID_IHTMLELEMENT2_ONROWSINSERTED = DISPID_EVPROP_ONROWSINSERTED;
        public const int DISPID_IHTMLELEMENT2_ONCELLCHANGE = DISPID_EVPROP_ONCELLCHANGE;
        //public const int  DISPID_IHTMLELEMENT2_DIR = DISPID_A_DIR;
        public const int DISPID_IHTMLELEMENT2_CREATECONTROLRANGE = DISPID_ELEMENT + 56;
        public const int DISPID_IHTMLELEMENT2_SCROLLHEIGHT = DISPID_ELEMENT + 57;
        public const int DISPID_IHTMLELEMENT2_SCROLLWIDTH = DISPID_ELEMENT + 58;
        public const int DISPID_IHTMLELEMENT2_SCROLLTOP = DISPID_ELEMENT + 59;
        public const int DISPID_IHTMLELEMENT2_SCROLLLEFT = DISPID_ELEMENT + 60;
        public const int DISPID_IHTMLELEMENT2_CLEARATTRIBUTES = DISPID_ELEMENT + 62;
        public const int DISPID_IHTMLELEMENT2_MERGEATTRIBUTES = DISPID_ELEMENT + 63;
        public const int DISPID_IHTMLELEMENT2_ONCONTEXTMENU = DISPID_EVPROP_ONCONTEXTMENU;
        public const int DISPID_IHTMLELEMENT2_INSERTADJACENTELEMENT = DISPID_ELEMENT + 69;
        public const int DISPID_IHTMLELEMENT2_APPLYELEMENT = DISPID_ELEMENT + 65;
        public const int DISPID_IHTMLELEMENT2_GETADJACENTTEXT = DISPID_ELEMENT + 70;
        public const int DISPID_IHTMLELEMENT2_REPLACEADJACENTTEXT = DISPID_ELEMENT + 71;
        public const int DISPID_IHTMLELEMENT2_CANHAVECHILDREN = DISPID_ELEMENT + 72;
        public const int DISPID_IHTMLELEMENT2_ADDBEHAVIOR = DISPID_ELEMENT + 80;
        public const int DISPID_IHTMLELEMENT2_REMOVEBEHAVIOR = DISPID_ELEMENT + 81;
        public const int DISPID_IHTMLELEMENT2_RUNTIMESTYLE = DISPID_ELEMENT + 64;
        public const int DISPID_IHTMLELEMENT2_BEHAVIORURNS = DISPID_ELEMENT + 82;
        public const int DISPID_IHTMLELEMENT2_TAGURN = DISPID_ELEMENT + 83;
        public const int DISPID_IHTMLELEMENT2_ONBEFOREEDITFOCUS = DISPID_EVPROP_ONBEFOREEDITFOCUS;
        public const int DISPID_IHTMLELEMENT2_READYSTATEVALUE = DISPID_ELEMENT + 84;
        public const int DISPID_IHTMLELEMENT2_GETELEMENTSBYTAGNAME = DISPID_ELEMENT + 85;

        //    DISPIDs for interface IHTMLElementCollection

        public const int DISPID_IHTMLELEMENTCOLLECTION_TOSTRING = DISPID_COLLECTION + 1;
        public const int DISPID_IHTMLELEMENTCOLLECTION_LENGTH = DISPID_COLLECTION;
        public const int DISPID_IHTMLELEMENTCOLLECTION__NEWENUM = DISPID_NEWENUM;
        public const int DISPID_IHTMLELEMENTCOLLECTION_ITEM = DISPID_VALUE;
        public const int DISPID_IHTMLELEMENTCOLLECTION_TAGS = DISPID_COLLECTION + 2;


        //    DISPIDs for interface IHTMLEventObj

        public const int DISPID_EVENTOBJ = DISPID_NORMAL_FIRST;

        public const int DISPID_IHTMLEVENTOBJ_SRCELEMENT = DISPID_EVENTOBJ + 1;
        public const int DISPID_IHTMLEVENTOBJ_ALTKEY = DISPID_EVENTOBJ + 2;
        public const int DISPID_IHTMLEVENTOBJ_CTRLKEY = DISPID_EVENTOBJ + 3;
        public const int DISPID_IHTMLEVENTOBJ_SHIFTKEY = DISPID_EVENTOBJ + 4;
        public const int DISPID_IHTMLEVENTOBJ_RETURNVALUE = DISPID_EVENTOBJ + 7;
        public const int DISPID_IHTMLEVENTOBJ_CANCELBUBBLE = DISPID_EVENTOBJ + 8;
        public const int DISPID_IHTMLEVENTOBJ_FROMELEMENT = DISPID_EVENTOBJ + 9;
        public const int DISPID_IHTMLEVENTOBJ_TOELEMENT = DISPID_EVENTOBJ + 10;
        public const int DISPID_IHTMLEVENTOBJ_KEYCODE = DISPID_EVENTOBJ + 11;
        public const int DISPID_IHTMLEVENTOBJ_BUTTON = DISPID_EVENTOBJ + 12;
        public const int DISPID_IHTMLEVENTOBJ_TYPE = DISPID_EVENTOBJ + 13;
        public const int DISPID_IHTMLEVENTOBJ_QUALIFIER = DISPID_EVENTOBJ + 14;
        public const int DISPID_IHTMLEVENTOBJ_REASON = DISPID_EVENTOBJ + 15;
        public const int DISPID_IHTMLEVENTOBJ_X = DISPID_EVENTOBJ + 5;
        public const int DISPID_IHTMLEVENTOBJ_Y = DISPID_EVENTOBJ + 6;
        public const int DISPID_IHTMLEVENTOBJ_CLIENTX = DISPID_EVENTOBJ + 20;
        public const int DISPID_IHTMLEVENTOBJ_CLIENTY = DISPID_EVENTOBJ + 21;
        public const int DISPID_IHTMLEVENTOBJ_OFFSETX = DISPID_EVENTOBJ + 22;
        public const int DISPID_IHTMLEVENTOBJ_OFFSETY = DISPID_EVENTOBJ + 23;
        public const int DISPID_IHTMLEVENTOBJ_SCREENX = DISPID_EVENTOBJ + 24;
        public const int DISPID_IHTMLEVENTOBJ_SCREENY = DISPID_EVENTOBJ + 25;
        public const int DISPID_IHTMLEVENTOBJ_SRCFILTER = DISPID_EVENTOBJ + 26;

        //    DISPIDs for interface IHTMLEventObj2

        public const int DISPID_IHTMLEVENTOBJ2_SETATTRIBUTE = DISPID_HTMLOBJECT + 1;
        public const int DISPID_IHTMLEVENTOBJ2_GETATTRIBUTE = DISPID_HTMLOBJECT + 2;
        public const int DISPID_IHTMLEVENTOBJ2_REMOVEATTRIBUTE = DISPID_HTMLOBJECT + 3;
        public const int DISPID_IHTMLEVENTOBJ2_PROPERTYNAME = DISPID_EVENTOBJ + 27;
        public const int DISPID_IHTMLEVENTOBJ2_BOOKMARKS = DISPID_EVENTOBJ + 31;
        public const int DISPID_IHTMLEVENTOBJ2_RECORDSET = DISPID_EVENTOBJ + 32;
        public const int DISPID_IHTMLEVENTOBJ2_DATAFLD = DISPID_EVENTOBJ + 33;
        public const int DISPID_IHTMLEVENTOBJ2_BOUNDELEMENTS = DISPID_EVENTOBJ + 34;
        public const int DISPID_IHTMLEVENTOBJ2_REPEAT = DISPID_EVENTOBJ + 35;
        public const int DISPID_IHTMLEVENTOBJ2_SRCURN = DISPID_EVENTOBJ + 36;
        public const int DISPID_IHTMLEVENTOBJ2_SRCELEMENT = DISPID_EVENTOBJ + 1;
        public const int DISPID_IHTMLEVENTOBJ2_ALTKEY = DISPID_EVENTOBJ + 2;
        public const int DISPID_IHTMLEVENTOBJ2_CTRLKEY = DISPID_EVENTOBJ + 3;
        public const int DISPID_IHTMLEVENTOBJ2_SHIFTKEY = DISPID_EVENTOBJ + 4;
        public const int DISPID_IHTMLEVENTOBJ2_FROMELEMENT = DISPID_EVENTOBJ + 9;
        public const int DISPID_IHTMLEVENTOBJ2_TOELEMENT = DISPID_EVENTOBJ + 10;
        public const int DISPID_IHTMLEVENTOBJ2_BUTTON = DISPID_EVENTOBJ + 12;
        public const int DISPID_IHTMLEVENTOBJ2_TYPE = DISPID_EVENTOBJ + 13;
        public const int DISPID_IHTMLEVENTOBJ2_QUALIFIER = DISPID_EVENTOBJ + 14;
        public const int DISPID_IHTMLEVENTOBJ2_REASON = DISPID_EVENTOBJ + 15;
        public const int DISPID_IHTMLEVENTOBJ2_X = DISPID_EVENTOBJ + 5;
        public const int DISPID_IHTMLEVENTOBJ2_Y = DISPID_EVENTOBJ + 6;
        public const int DISPID_IHTMLEVENTOBJ2_CLIENTX = DISPID_EVENTOBJ + 20;
        public const int DISPID_IHTMLEVENTOBJ2_CLIENTY = DISPID_EVENTOBJ + 21;
        public const int DISPID_IHTMLEVENTOBJ2_OFFSETX = DISPID_EVENTOBJ + 22;
        public const int DISPID_IHTMLEVENTOBJ2_OFFSETY = DISPID_EVENTOBJ + 23;
        public const int DISPID_IHTMLEVENTOBJ2_SCREENX = DISPID_EVENTOBJ + 24;
        public const int DISPID_IHTMLEVENTOBJ2_SCREENY = DISPID_EVENTOBJ + 25;
        public const int DISPID_IHTMLEVENTOBJ2_SRCFILTER = DISPID_EVENTOBJ + 26;
        public const int DISPID_IHTMLEVENTOBJ2_DATATRANSFER = DISPID_EVENTOBJ + 37;

        //    DISPIDs for interface IHTMLEventObj3

        public const int DISPID_IHTMLEVENTOBJ3_CONTENTOVERFLOW = DISPID_EVENTOBJ + 38;
        public const int DISPID_IHTMLEVENTOBJ3_SHIFTLEFT = DISPID_EVENTOBJ + 39;
        public const int DISPID_IHTMLEVENTOBJ3_ALTLEFT = DISPID_EVENTOBJ + 40;
        public const int DISPID_IHTMLEVENTOBJ3_CTRLLEFT = DISPID_EVENTOBJ + 41;
        public const int DISPID_IHTMLEVENTOBJ3_IMECOMPOSITIONCHANGE = DISPID_EVENTOBJ + 42;
        public const int DISPID_IHTMLEVENTOBJ3_IMENOTIFYCOMMAND = DISPID_EVENTOBJ + 43;
        public const int DISPID_IHTMLEVENTOBJ3_IMENOTIFYDATA = DISPID_EVENTOBJ + 44;
        public const int DISPID_IHTMLEVENTOBJ3_IMEREQUEST = DISPID_EVENTOBJ + 46;
        public const int DISPID_IHTMLEVENTOBJ3_IMEREQUESTDATA = DISPID_EVENTOBJ + 47;
        public const int DISPID_IHTMLEVENTOBJ3_KEYBOARDLAYOUT = DISPID_EVENTOBJ + 45;
        public const int DISPID_IHTMLEVENTOBJ3_BEHAVIORCOOKIE = DISPID_EVENTOBJ + 48;
        public const int DISPID_IHTMLEVENTOBJ3_BEHAVIORPART = DISPID_EVENTOBJ + 49;
        public const int DISPID_IHTMLEVENTOBJ3_NEXTPAGE = DISPID_EVENTOBJ + 50;


        public const int DISPID_A_FIRST = DISPID_ATTRS;
        public const int DISPID_A_DIR = DISPID_A_FIRST + 117;

        // DISPIDs for interface IHTMLDocument3
        public const int DISPID_IHTMLDOCUMENT3_RELEASECAPTURE = DISPID_OMDOCUMENT + 72;
        public const int DISPID_IHTMLDOCUMENT3_RECALC = DISPID_OMDOCUMENT + 73;
        public const int DISPID_IHTMLDOCUMENT3_CREATETEXTNODE = DISPID_OMDOCUMENT + 74;
        public const int DISPID_IHTMLDOCUMENT3_DOCUMENTELEMENT = DISPID_OMDOCUMENT + 75;
        public const int DISPID_IHTMLDOCUMENT3_UNIQUEID = DISPID_OMDOCUMENT + 77;
        public const int DISPID_IHTMLDOCUMENT3_ATTACHEVENT = DISPID_HTMLOBJECT + 7;
        public const int DISPID_IHTMLDOCUMENT3_DETACHEVENT = DISPID_HTMLOBJECT + 8;
        public const int DISPID_IHTMLDOCUMENT3_ONROWSDELETE = DISPID_EVPROP_ONROWSDELETE;
        public const int DISPID_IHTMLDOCUMENT3_ONROWSINSERTED = DISPID_EVPROP_ONROWSINSERTED;
        public const int DISPID_IHTMLDOCUMENT3_ONCELLCHANGE = DISPID_EVPROP_ONCELLCHANGE;
        public const int DISPID_IHTMLDOCUMENT3_ONDATASETCHANGED = DISPID_EVPROP_ONDATASETCHANGED;
        public const int DISPID_IHTMLDOCUMENT3_ONDATAAVAILABLE = DISPID_EVPROP_ONDATAAVAILABLE;
        public const int DISPID_IHTMLDOCUMENT3_ONDATASETCOMPLETE = DISPID_EVPROP_ONDATASETCOMPLETE;
        public const int DISPID_IHTMLDOCUMENT3_ONPROPERTYCHANGE = DISPID_EVPROP_ONPROPERTYCHANGE;
        public const int DISPID_IHTMLDOCUMENT3_DIR = DISPID_A_DIR;
        public const int DISPID_IHTMLDOCUMENT3_ONCONTEXTMENU = DISPID_EVPROP_ONCONTEXTMENU;
        public const int DISPID_IHTMLDOCUMENT3_ONSTOP = DISPID_EVPROP_ONSTOP;
        public const int DISPID_IHTMLDOCUMENT3_CREATEDOCUMENTFRAGMENT = DISPID_OMDOCUMENT + 76;
        public const int DISPID_IHTMLDOCUMENT3_PARENTDOCUMENT = DISPID_OMDOCUMENT + 78;
        public const int DISPID_IHTMLDOCUMENT3_ENABLEDOWNLOAD = DISPID_OMDOCUMENT + 79;
        public const int DISPID_IHTMLDOCUMENT3_BASEURL = DISPID_OMDOCUMENT + 80;
        public const int DISPID_IHTMLDOCUMENT3_CHILDNODES = DISPID_ELEMENT + 49;
        public const int DISPID_IHTMLDOCUMENT3_INHERITSTYLESHEETS = DISPID_OMDOCUMENT + 82;
        public const int DISPID_IHTMLDOCUMENT3_ONBEFOREEDITFOCUS = DISPID_EVPROP_ONBEFOREEDITFOCUS;
        public const int DISPID_IHTMLDOCUMENT3_GETELEMENTSBYNAME = DISPID_OMDOCUMENT + 86;
        public const int DISPID_IHTMLDOCUMENT3_GETELEMENTBYID = DISPID_OMDOCUMENT + 88;
        public const int DISPID_IHTMLDOCUMENT3_GETELEMENTSBYTAGNAME = DISPID_OMDOCUMENT + 87;

        //    DISPIDs for interface IHTMLDocument4
        public const int DISPID_OMDOCUMENT = DISPID_NORMAL_FIRST;

        public const int DISPID_IHTMLDOCUMENT4_FOCUS = DISPID_OMDOCUMENT + 89;
        public const int DISPID_IHTMLDOCUMENT4_HASFOCUS = DISPID_OMDOCUMENT + 90;
        public const int DISPID_IHTMLDOCUMENT4_ONSELECTIONCHANGE = DISPID_EVPROP_ONSELECTIONCHANGE;
        public const int DISPID_IHTMLDOCUMENT4_NAMESPACES = DISPID_OMDOCUMENT + 91;
        public const int DISPID_IHTMLDOCUMENT4_CREATEDOCUMENTFROMURL = DISPID_OMDOCUMENT + 92;
        public const int DISPID_IHTMLDOCUMENT4_MEDIA = DISPID_OMDOCUMENT + 93;
        public const int DISPID_IHTMLDOCUMENT4_CREATEEVENTOBJECT = DISPID_OMDOCUMENT + 94;
        public const int DISPID_IHTMLDOCUMENT4_FIREEVENT = DISPID_OMDOCUMENT + 95;
        public const int DISPID_IHTMLDOCUMENT4_CREATERENDERSTYLE = DISPID_OMDOCUMENT + 96;
        public const int DISPID_IHTMLDOCUMENT4_ONCONTROLSELECT = DISPID_EVPROP_ONCONTROLSELECT;
        public const int DISPID_IHTMLDOCUMENT4_URLUNENCODED = DISPID_OMDOCUMENT + 97;

        //    DISPIDs for interface IHTMLDocument5

        public const int DISPID_IHTMLDOCUMENT5_ONMOUSEWHEEL = DISPID_EVPROP_ONMOUSEWHEEL;
        public const int DISPID_IHTMLDOCUMENT5_DOCTYPE = DISPID_OMDOCUMENT + 98;
        public const int DISPID_IHTMLDOCUMENT5_IMPLEMENTATION = DISPID_OMDOCUMENT + 99;
        public const int DISPID_IHTMLDOCUMENT5_CREATEATTRIBUTE = DISPID_OMDOCUMENT + 100;
        public const int DISPID_IHTMLDOCUMENT5_CREATECOMMENT = DISPID_OMDOCUMENT + 101;
        public const int DISPID_IHTMLDOCUMENT5_ONFOCUSIN = DISPID_EVPROP_ONFOCUSIN;
        public const int DISPID_IHTMLDOCUMENT5_ONFOCUSOUT = DISPID_EVPROP_ONFOCUSOUT;
        public const int DISPID_IHTMLDOCUMENT5_ONACTIVATE = DISPID_EVPROP_ONACTIVATE;
        public const int DISPID_IHTMLDOCUMENT5_ONDEACTIVATE = DISPID_EVPROP_ONDEACTIVATE;
        public const int DISPID_IHTMLDOCUMENT5_ONBEFOREACTIVATE = DISPID_EVPROP_ONBEFOREACTIVATE;
        public const int DISPID_IHTMLDOCUMENT5_ONBEFOREDEACTIVATE = DISPID_EVPROP_ONBEFOREDEACTIVATE;
        public const int DISPID_IHTMLDOCUMENT5_COMPATMODE = DISPID_OMDOCUMENT + 102;


        //DISPIDS for interface IHTMLDocumentEvents2
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONHELP = DISPID_EVMETH_ONHELP;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONCLICK = DISPID_EVMETH_ONCLICK;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONDBLCLICK = DISPID_EVMETH_ONDBLCLICK;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONKEYDOWN = DISPID_EVMETH_ONKEYDOWN;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONKEYUP = DISPID_EVMETH_ONKEYUP;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONKEYPRESS = DISPID_EVMETH_ONKEYPRESS;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONMOUSEDOWN = DISPID_EVMETH_ONMOUSEDOWN;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONMOUSEMOVE = DISPID_EVMETH_ONMOUSEMOVE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONMOUSEUP = DISPID_EVMETH_ONMOUSEUP;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONMOUSEOUT = DISPID_EVMETH_ONMOUSEOUT;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONMOUSEOVER = DISPID_EVMETH_ONMOUSEOVER;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONREADYSTATECHANGE = DISPID_EVMETH_ONREADYSTATECHANGE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONBEFOREUPDATE = DISPID_EVMETH_ONBEFOREUPDATE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONAFTERUPDATE = DISPID_EVMETH_ONAFTERUPDATE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONROWEXIT = DISPID_EVMETH_ONROWEXIT;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONROWENTER = DISPID_EVMETH_ONROWENTER;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONDRAGSTART = DISPID_EVMETH_ONDRAGSTART;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONSELECTSTART = DISPID_EVMETH_ONSELECTSTART;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONERRORUPDATE = DISPID_EVMETH_ONERRORUPDATE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONCONTEXTMENU = DISPID_EVMETH_ONCONTEXTMENU;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONSTOP = DISPID_EVMETH_ONSTOP;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONROWSDELETE = DISPID_EVMETH_ONROWSDELETE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONROWSINSERTED = DISPID_EVMETH_ONROWSINSERTED;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONCELLCHANGE = DISPID_EVMETH_ONCELLCHANGE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONPROPERTYCHANGE = DISPID_EVMETH_ONPROPERTYCHANGE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONDATASETCHANGED = DISPID_EVMETH_ONDATASETCHANGED;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONDATAAVAILABLE = DISPID_EVMETH_ONDATAAVAILABLE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONDATASETCOMPLETE = DISPID_EVMETH_ONDATASETCOMPLETE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONBEFOREEDITFOCUS = DISPID_EVMETH_ONBEFOREEDITFOCUS;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONSELECTIONCHANGE = DISPID_EVMETH_ONSELECTIONCHANGE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONCONTROLSELECT = DISPID_EVMETH_ONCONTROLSELECT;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONMOUSEWHEEL = DISPID_EVMETH_ONMOUSEWHEEL;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONFOCUSIN = DISPID_EVMETH_ONFOCUSIN;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONFOCUSOUT = DISPID_EVMETH_ONFOCUSOUT;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONACTIVATE = DISPID_EVMETH_ONACTIVATE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONDEACTIVATE = DISPID_EVMETH_ONDEACTIVATE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONBEFOREACTIVATE = DISPID_EVMETH_ONBEFOREACTIVATE;
        public const int DISPID_HTMLDOCUMENTEVENTS2_ONBEFOREDEACTIVATE = DISPID_EVMETH_ONBEFOREDEACTIVATE;

        //    DISPIDs for event set HTMLWindowEvents2

        public const int DISPID_HTMLWINDOWEVENTS2_ONLOAD = DISPID_EVMETH_ONLOAD;
        public const int DISPID_HTMLWINDOWEVENTS2_ONUNLOAD = DISPID_EVMETH_ONUNLOAD;
        public const int DISPID_HTMLWINDOWEVENTS2_ONHELP = DISPID_EVMETH_ONHELP;
        public const int DISPID_HTMLWINDOWEVENTS2_ONFOCUS = DISPID_EVMETH_ONFOCUS;
        public const int DISPID_HTMLWINDOWEVENTS2_ONBLUR = DISPID_EVMETH_ONBLUR;
        public const int DISPID_HTMLWINDOWEVENTS2_ONERROR = DISPID_EVMETH_ONERROR;
        public const int DISPID_HTMLWINDOWEVENTS2_ONRESIZE = DISPID_EVMETH_ONRESIZE;
        public const int DISPID_HTMLWINDOWEVENTS2_ONSCROLL = DISPID_EVMETH_ONSCROLL;
        public const int DISPID_HTMLWINDOWEVENTS2_ONBEFOREUNLOAD = DISPID_EVMETH_ONBEFOREUNLOAD;
        public const int DISPID_HTMLWINDOWEVENTS2_ONBEFOREPRINT = DISPID_EVMETH_ONBEFOREPRINT;
        public const int DISPID_HTMLWINDOWEVENTS2_ONAFTERPRINT = DISPID_EVMETH_ONAFTERPRINT;

        //    DISPIDs for interface IHTMLDOMNode

        public const int DISPID_IHTMLDOMNODE_NODETYPE = DISPID_ELEMENT + 46;
        public const int DISPID_IHTMLDOMNODE_PARENTNODE = DISPID_ELEMENT + 47;
        public const int DISPID_IHTMLDOMNODE_HASCHILDNODES = DISPID_ELEMENT + 48;
        public const int DISPID_IHTMLDOMNODE_CHILDNODES = DISPID_ELEMENT + 49;
        public const int DISPID_IHTMLDOMNODE_ATTRIBUTES = DISPID_ELEMENT + 50;
        public const int DISPID_IHTMLDOMNODE_INSERTBEFORE = DISPID_ELEMENT + 51;
        public const int DISPID_IHTMLDOMNODE_REMOVECHILD = DISPID_ELEMENT + 52;
        public const int DISPID_IHTMLDOMNODE_REPLACECHILD = DISPID_ELEMENT + 53;
        public const int DISPID_IHTMLDOMNODE_CLONENODE = DISPID_ELEMENT + 61;
        public const int DISPID_IHTMLDOMNODE_REMOVENODE = DISPID_ELEMENT + 66;
        public const int DISPID_IHTMLDOMNODE_SWAPNODE = DISPID_ELEMENT + 68;
        public const int DISPID_IHTMLDOMNODE_REPLACENODE = DISPID_ELEMENT + 67;
        public const int DISPID_IHTMLDOMNODE_APPENDCHILD = DISPID_ELEMENT + 73;
        public const int DISPID_IHTMLDOMNODE_NODENAME = DISPID_ELEMENT + 74;
        public const int DISPID_IHTMLDOMNODE_NODEVALUE = DISPID_ELEMENT + 75;
        public const int DISPID_IHTMLDOMNODE_FIRSTCHILD = DISPID_ELEMENT + 76;
        public const int DISPID_IHTMLDOMNODE_LASTCHILD = DISPID_ELEMENT + 77;
        public const int DISPID_IHTMLDOMNODE_PREVIOUSSIBLING = DISPID_ELEMENT + 78;
        public const int DISPID_IHTMLDOMNODE_NEXTSIBLING = DISPID_ELEMENT + 79;

        public const int DISPID_COLLECTION_MIN = 1000000;
        public const int DISPID_COLLECTION_MAX = 2999999;
        public const int DISPID_COLLECTION = (DISPID_NORMAL_FIRST + 500);
        public const int DISPID_VALUE = 0;

        /* The following DISPID is reserved to indicate the param
         * that is the right-hand-side (or "put" value) of a PropertyPut
         */
        public const int DISPID_PROPERTYPUT = -3;

        /* DISPID reserved for the standard "NewEnum" method */
        public const int DISPID_NEWENUM = -4;

        //    DISPIDs for interface IHTMLDOMChildrenCollection

        public const int DISPID_IHTMLDOMCHILDRENCOLLECTION_LENGTH = DISPID_COLLECTION;
        public const int DISPID_IHTMLDOMCHILDRENCOLLECTION__NEWENUM = DISPID_NEWENUM;
        public const int DISPID_IHTMLDOMCHILDRENCOLLECTION_ITEM = DISPID_VALUE;

        //    DISPIDs for interface IHTMLFramesCollection2

        public const int DISPID_IHTMLFRAMESCOLLECTION2_ITEM = 0;
        public const int DISPID_IHTMLFRAMESCOLLECTION2_LENGTH = 1001;

        //    DISPIDs for interface IHTMLWindow2

        public const int DISPID_IHTMLWINDOW2_FRAMES = 1100;
        public const int DISPID_IHTMLWINDOW2_DEFAULTSTATUS = 1101;
        public const int DISPID_IHTMLWINDOW2_STATUS = 1102;
        public const int DISPID_IHTMLWINDOW2_SETTIMEOUT = 1172;
        public const int DISPID_IHTMLWINDOW2_CLEARTIMEOUT = 1104;
        public const int DISPID_IHTMLWINDOW2_ALERT = 1105;
        public const int DISPID_IHTMLWINDOW2_CONFIRM = 1110;
        public const int DISPID_IHTMLWINDOW2_PROMPT = 1111;
        public const int DISPID_IHTMLWINDOW2_IMAGE = 1125;
        public const int DISPID_IHTMLWINDOW2_LOCATION = 14;
        public const int DISPID_IHTMLWINDOW2_HISTORY = 2;
        public const int DISPID_IHTMLWINDOW2_CLOSE = 3;
        public const int DISPID_IHTMLWINDOW2_OPENER = 4;
        public const int DISPID_IHTMLWINDOW2_NAVIGATOR = 5;
        public const int DISPID_IHTMLWINDOW2_NAME = 11;
        public const int DISPID_IHTMLWINDOW2_PARENT = 12;
        public const int DISPID_IHTMLWINDOW2_OPEN = 13;
        public const int DISPID_IHTMLWINDOW2_SELF = 20;
        public const int DISPID_IHTMLWINDOW2_TOP = 21;
        public const int DISPID_IHTMLWINDOW2_WINDOW = 22;
        public const int DISPID_IHTMLWINDOW2_NAVIGATE = 25;
        public const int DISPID_IHTMLWINDOW2_ONFOCUS = DISPID_EVPROP_ONFOCUS;
        public const int DISPID_IHTMLWINDOW2_ONBLUR = DISPID_EVPROP_ONBLUR;
        public const int DISPID_IHTMLWINDOW2_ONLOAD = DISPID_EVPROP_ONLOAD;
        public const int DISPID_IHTMLWINDOW2_ONBEFOREUNLOAD = DISPID_EVPROP_ONBEFOREUNLOAD;
        public const int DISPID_IHTMLWINDOW2_ONUNLOAD = DISPID_EVPROP_ONUNLOAD;
        public const int DISPID_IHTMLWINDOW2_ONHELP = DISPID_EVPROP_ONHELP;
        public const int DISPID_IHTMLWINDOW2_ONERROR = DISPID_EVPROP_ONERROR;
        public const int DISPID_IHTMLWINDOW2_ONRESIZE = DISPID_EVPROP_ONRESIZE;
        public const int DISPID_IHTMLWINDOW2_ONSCROLL = DISPID_EVPROP_ONSCROLL;
        public const int DISPID_IHTMLWINDOW2_DOCUMENT = 1151;
        public const int DISPID_IHTMLWINDOW2_EVENT = 1152;
        public const int DISPID_IHTMLWINDOW2__NEWENUM = 1153;
        public const int DISPID_IHTMLWINDOW2_SHOWMODALDIALOG = 1154;
        public const int DISPID_IHTMLWINDOW2_SHOWHELP = 1155;
        public const int DISPID_IHTMLWINDOW2_SCREEN = 1156;
        public const int DISPID_IHTMLWINDOW2_OPTION = 1157;
        public const int DISPID_IHTMLWINDOW2_FOCUS = 1158;
        public const int DISPID_IHTMLWINDOW2_CLOSED = 23;
        public const int DISPID_IHTMLWINDOW2_BLUR = 1159;
        public const int DISPID_IHTMLWINDOW2_SCROLL = 1160;
        public const int DISPID_IHTMLWINDOW2_CLIENTINFORMATION = 1161;
        public const int DISPID_IHTMLWINDOW2_SETINTERVAL = 1173;
        public const int DISPID_IHTMLWINDOW2_CLEARINTERVAL = 1163;
        public const int DISPID_IHTMLWINDOW2_OFFSCREENBUFFERING = 1164;
        public const int DISPID_IHTMLWINDOW2_EXECSCRIPT = 1165;
        public const int DISPID_IHTMLWINDOW2_TOSTRING = 1166;
        public const int DISPID_IHTMLWINDOW2_SCROLLBY = 1167;
        public const int DISPID_IHTMLWINDOW2_SCROLLTO = 1168;
        public const int DISPID_IHTMLWINDOW2_MOVETO = 6;
        public const int DISPID_IHTMLWINDOW2_MOVEBY = 7;
        public const int DISPID_IHTMLWINDOW2_RESIZETO = 9;
        public const int DISPID_IHTMLWINDOW2_RESIZEBY = 8;
        public const int DISPID_IHTMLWINDOW2_EXTERNAL = 1169;

        //    DISPIDS for interface IHtmlAncher
        public const int DISPID_ANCHOR = DISPID_NORMAL_FIRST;
        public const int STDPROPID_XOBJ_TABINDEX = DISPID_XOBJ_BASE + 0xF;

        public const int DISPID_IHTMLANCHORELEMENT_HREF = DISPID_VALUE;
        public const int DISPID_IHTMLANCHORELEMENT_TARGET = DISPID_ANCHOR + 3;
        public const int DISPID_IHTMLANCHORELEMENT_REL = DISPID_ANCHOR + 5;
        public const int DISPID_IHTMLANCHORELEMENT_REV = DISPID_ANCHOR + 6;
        public const int DISPID_IHTMLANCHORELEMENT_URN = DISPID_ANCHOR + 7;
        public const int DISPID_IHTMLANCHORELEMENT_METHODS = DISPID_ANCHOR + 8;
        public const int DISPID_IHTMLANCHORELEMENT_NAME = STDPROPID_XOBJ_NAME;
        public const int DISPID_IHTMLANCHORELEMENT_HOST = DISPID_ANCHOR + 12;
        public const int DISPID_IHTMLANCHORELEMENT_HOSTNAME = DISPID_ANCHOR + 13;
        public const int DISPID_IHTMLANCHORELEMENT_PATHNAME = DISPID_ANCHOR + 14;
        public const int DISPID_IHTMLANCHORELEMENT_PORT = DISPID_ANCHOR + 15;
        public const int DISPID_IHTMLANCHORELEMENT_PROTOCOL = DISPID_ANCHOR + 16;
        public const int DISPID_IHTMLANCHORELEMENT_SEARCH = DISPID_ANCHOR + 17;
        public const int DISPID_IHTMLANCHORELEMENT_HASH = DISPID_ANCHOR + 18;
        public const int DISPID_IHTMLANCHORELEMENT_ONBLUR = DISPID_EVPROP_ONBLUR;
        public const int DISPID_IHTMLANCHORELEMENT_ONFOCUS = DISPID_EVPROP_ONFOCUS;
        public const int DISPID_IHTMLANCHORELEMENT_ACCESSKEY = DISPID_SITE + 5;
        public const int DISPID_IHTMLANCHORELEMENT_PROTOCOLLONG = DISPID_ANCHOR + 31;
        public const int DISPID_IHTMLANCHORELEMENT_MIMETYPE = DISPID_ANCHOR + 30;
        public const int DISPID_IHTMLANCHORELEMENT_NAMEPROP = DISPID_ANCHOR + 32;
        public const int DISPID_IHTMLANCHORELEMENT_TABINDEX = STDPROPID_XOBJ_TABINDEX;
        public const int DISPID_IHTMLANCHORELEMENT_FOCUS = DISPID_SITE + 0;
        public const int DISPID_IHTMLANCHORELEMENT_BLUR = DISPID_SITE + 2;

        //    DISPIDs for interface IHTMLImgElement

        public const int DISPID_IMGBASE = DISPID_NORMAL_FIRST;
        public const int DISPID_IMG = (DISPID_IMGBASE + 1000);
        public const int DISPID_INPUTIMAGE = (DISPID_IMGBASE + 1000);
        public const int DISPID_INPUT = (DISPID_TEXTSITE + 1000);
        public const int DISPID_INPUTTEXTBASE = (DISPID_INPUT + 1000);
        public const int DISPID_INPUTTEXT = (DISPID_INPUTTEXTBASE + 1000);
        public const int DISPID_SELECT = DISPID_NORMAL_FIRST;

        public const int DISPID_A_READYSTATE = (DISPID_A_FIRST + 116); // ready state
        public const int STDPROPID_XOBJ_CONTROLALIGN = (DISPID_XOBJ_BASE + 0x49);
        public const int STDPROPID_XOBJ_NAME = (DISPID_XOBJ_BASE + 0x0);
        public const int STDPROPID_XOBJ_WIDTH = (DISPID_XOBJ_BASE + 0x5);
        public const int STDPROPID_XOBJ_HEIGHT = (DISPID_XOBJ_BASE + 0x6);

        public const int DISPID_IHTMLIMGELEMENT_ISMAP = DISPID_IMG + 2;
        public const int DISPID_IHTMLIMGELEMENT_USEMAP = DISPID_IMG + 8;
        public const int DISPID_IHTMLIMGELEMENT_MIMETYPE = DISPID_IMG + 10;
        public const int DISPID_IHTMLIMGELEMENT_FILESIZE = DISPID_IMG + 11;
        public const int DISPID_IHTMLIMGELEMENT_FILECREATEDDATE = DISPID_IMG + 12;
        public const int DISPID_IHTMLIMGELEMENT_FILEMODIFIEDDATE = DISPID_IMG + 13;
        public const int DISPID_IHTMLIMGELEMENT_FILEUPDATEDDATE = DISPID_IMG + 14;
        public const int DISPID_IHTMLIMGELEMENT_PROTOCOL = DISPID_IMG + 15;
        public const int DISPID_IHTMLIMGELEMENT_HREF = DISPID_IMG + 16;
        public const int DISPID_IHTMLIMGELEMENT_NAMEPROP = DISPID_IMG + 17;
        public const int DISPID_IHTMLIMGELEMENT_BORDER = DISPID_IMGBASE + 4;
        public const int DISPID_IHTMLIMGELEMENT_VSPACE = DISPID_IMGBASE + 5;
        public const int DISPID_IHTMLIMGELEMENT_HSPACE = DISPID_IMGBASE + 6;
        public const int DISPID_IHTMLIMGELEMENT_ALT = DISPID_IMGBASE + 2;
        public const int DISPID_IHTMLIMGELEMENT_SRC = DISPID_IMGBASE + 3;
        public const int DISPID_IHTMLIMGELEMENT_LOWSRC = DISPID_IMGBASE + 7;
        public const int DISPID_IHTMLIMGELEMENT_VRML = DISPID_IMGBASE + 8;
        public const int DISPID_IHTMLIMGELEMENT_DYNSRC = DISPID_IMGBASE + 9;
        public const int DISPID_IHTMLIMGELEMENT_READYSTATE = DISPID_A_READYSTATE;
        public const int DISPID_IHTMLIMGELEMENT_COMPLETE = DISPID_IMGBASE + 10;
        public const int DISPID_IHTMLIMGELEMENT_LOOP = DISPID_IMGBASE + 11;
        public const int DISPID_IHTMLIMGELEMENT_ALIGN = STDPROPID_XOBJ_CONTROLALIGN;
        public const int DISPID_IHTMLIMGELEMENT_ONLOAD = DISPID_EVPROP_ONLOAD;
        public const int DISPID_IHTMLIMGELEMENT_ONERROR = DISPID_EVPROP_ONERROR;
        public const int DISPID_IHTMLIMGELEMENT_ONABORT = DISPID_EVPROP_ONABORT;
        public const int DISPID_IHTMLIMGELEMENT_NAME = STDPROPID_XOBJ_NAME;
        public const int DISPID_IHTMLIMGELEMENT_WIDTH = STDPROPID_XOBJ_WIDTH;
        public const int DISPID_IHTMLIMGELEMENT_HEIGHT = STDPROPID_XOBJ_HEIGHT;
        public const int DISPID_IHTMLIMGELEMENT_START = DISPID_IMGBASE + 13;

        //    DISPIDs for interface IHTMLTxtRange
        public const int DISPID_RANGE = DISPID_NORMAL_FIRST;

        public const int DISPID_IHTMLTXTRANGE_HTMLTEXT = DISPID_RANGE + 3;
        public const int DISPID_IHTMLTXTRANGE_TEXT = DISPID_RANGE + 4;
        public const int DISPID_IHTMLTXTRANGE_PARENTELEMENT = DISPID_RANGE + 6;
        public const int DISPID_IHTMLTXTRANGE_DUPLICATE = DISPID_RANGE + 8;
        public const int DISPID_IHTMLTXTRANGE_INRANGE = DISPID_RANGE + 10;
        public const int DISPID_IHTMLTXTRANGE_ISEQUAL = DISPID_RANGE + 11;
        public const int DISPID_IHTMLTXTRANGE_SCROLLINTOVIEW = DISPID_RANGE + 12;
        public const int DISPID_IHTMLTXTRANGE_COLLAPSE = DISPID_RANGE + 13;
        public const int DISPID_IHTMLTXTRANGE_EXPAND = DISPID_RANGE + 14;
        public const int DISPID_IHTMLTXTRANGE_MOVE = DISPID_RANGE + 15;
        public const int DISPID_IHTMLTXTRANGE_MOVESTART = DISPID_RANGE + 16;
        public const int DISPID_IHTMLTXTRANGE_MOVEEND = DISPID_RANGE + 17;
        public const int DISPID_IHTMLTXTRANGE_SELECT = DISPID_RANGE + 24;
        public const int DISPID_IHTMLTXTRANGE_PASTEHTML = DISPID_RANGE + 26;
        public const int DISPID_IHTMLTXTRANGE_MOVETOELEMENTTEXT = DISPID_RANGE + 1;
        public const int DISPID_IHTMLTXTRANGE_SETENDPOINT = DISPID_RANGE + 25;
        public const int DISPID_IHTMLTXTRANGE_COMPAREENDPOINTS = DISPID_RANGE + 18;
        public const int DISPID_IHTMLTXTRANGE_FINDTEXT = DISPID_RANGE + 19;
        public const int DISPID_IHTMLTXTRANGE_MOVETOPOINT = DISPID_RANGE + 20;
        public const int DISPID_IHTMLTXTRANGE_GETBOOKMARK = DISPID_RANGE + 21;
        public const int DISPID_IHTMLTXTRANGE_MOVETOBOOKMARK = DISPID_RANGE + 9;
        public const int DISPID_IHTMLTXTRANGE_QUERYCOMMANDSUPPORTED = DISPID_RANGE + 27;
        public const int DISPID_IHTMLTXTRANGE_QUERYCOMMANDENABLED = DISPID_RANGE + 28;
        public const int DISPID_IHTMLTXTRANGE_QUERYCOMMANDSTATE = DISPID_RANGE + 29;
        public const int DISPID_IHTMLTXTRANGE_QUERYCOMMANDINDETERM = DISPID_RANGE + 30;
        public const int DISPID_IHTMLTXTRANGE_QUERYCOMMANDTEXT = DISPID_RANGE + 31;
        public const int DISPID_IHTMLTXTRANGE_QUERYCOMMANDVALUE = DISPID_RANGE + 32;
        public const int DISPID_IHTMLTXTRANGE_EXECCOMMAND = DISPID_RANGE + 33;
        public const int DISPID_IHTMLTXTRANGE_EXECCOMMANDSHOWHELP = DISPID_RANGE + 34;

        //    DISPIDs for interface IHTMLDOMAttribute

        public const int DISPID_DOMATTRIBUTE = DISPID_NORMAL_FIRST;

        public const int DISPID_IHTMLDOMATTRIBUTE_NODENAME = DISPID_DOMATTRIBUTE;
        public const int DISPID_IHTMLDOMATTRIBUTE_NODEVALUE = DISPID_DOMATTRIBUTE + 2;
        public const int DISPID_IHTMLDOMATTRIBUTE_SPECIFIED = DISPID_DOMATTRIBUTE + 1;

        //    DISPIDs for interface IHTMLAttributeCollection

        public const int DISPID_IHTMLATTRIBUTECOLLECTION_LENGTH = DISPID_COLLECTION;
        public const int DISPID_IHTMLATTRIBUTECOLLECTION__NEWENUM = DISPID_NEWENUM;
        public const int DISPID_IHTMLATTRIBUTECOLLECTION_ITEM = DISPID_VALUE;

        //    DISPIDs for interface IHTMLStyleSheetsCollection

        public const int DISPID_STYLESHEETS_COL = DISPID_NORMAL_FIRST;

        public const int DISPID_IHTMLSTYLESHEETSCOLLECTION_LENGTH = DISPID_STYLESHEETS_COL + 1;
        public const int DISPID_IHTMLSTYLESHEETSCOLLECTION__NEWENUM = DISPID_NEWENUM;
        public const int DISPID_IHTMLSTYLESHEETSCOLLECTION_ITEM = DISPID_VALUE;

        //    DISPIDs for interface IHTMLSelectionObject

        public const int DISPID_SELECTOBJ = DISPID_NORMAL_FIRST;

        public const int DISPID_IHTMLSELECTIONOBJECT_CREATERANGE = DISPID_SELECTOBJ + 1;
        public const int DISPID_IHTMLSELECTIONOBJECT_EMPTY = DISPID_SELECTOBJ + 2;
        public const int DISPID_IHTMLSELECTIONOBJECT_CLEAR = DISPID_SELECTOBJ + 3;
        public const int DISPID_IHTMLSELECTIONOBJECT_TYPE = DISPID_SELECTOBJ + 4;

        // DISPIDS for interface IHTMLBodyElement
        public const int DISPID_TEXTSITE = DISPID_NORMAL_FIRST;
        public const int DISPID_BODY = (DISPID_TEXTSITE + 1000);
        public const int DISPID_IHTMLBODYELEMENT_CREATETEXTRANGE = DISPID_BODY + 13;

        //    DISPIDs for interface IHTMLDOMTextNode
        public const int DISPID_DOMTEXTNODE = DISPID_NORMAL_FIRST;

        public const int DISPID_IHTMLDOMTEXTNODE_DATA = DISPID_DOMTEXTNODE;
        public const int DISPID_IHTMLDOMTEXTNODE_TOSTRING = DISPID_DOMTEXTNODE + 1;
        public const int DISPID_IHTMLDOMTEXTNODE_LENGTH = DISPID_DOMTEXTNODE + 2;
        public const int DISPID_IHTMLDOMTEXTNODE_SPLITTEXT = DISPID_DOMTEXTNODE + 3;

        //    DISPIDs for interface IHTMLDOMTextNode2
        public const int DISPID_IHTMLDOMTEXTNODE2_SUBSTRINGDATA = DISPID_DOMTEXTNODE + 4;
        public const int DISPID_IHTMLDOMTEXTNODE2_APPENDDATA = DISPID_DOMTEXTNODE + 5;
        public const int DISPID_IHTMLDOMTEXTNODE2_INSERTDATA = DISPID_DOMTEXTNODE + 6;
        public const int DISPID_IHTMLDOMTEXTNODE2_DELETEDATA = DISPID_DOMTEXTNODE + 7;
        public const int DISPID_IHTMLDOMTEXTNODE2_REPLACEDATA = DISPID_DOMTEXTNODE + 8;

        //    DISPIDs for interface IHTMLDOMAttribute2
        public const int DISPID_IHTMLDOMATTRIBUTE2_NAME = DISPID_DOMATTRIBUTE + 3;
        public const int DISPID_IHTMLDOMATTRIBUTE2_VALUE = DISPID_DOMATTRIBUTE + 4;
        public const int DISPID_IHTMLDOMATTRIBUTE2_EXPANDO = DISPID_DOMATTRIBUTE + 5;
        public const int DISPID_IHTMLDOMATTRIBUTE2_NODETYPE = DISPID_DOMATTRIBUTE + 6;
        public const int DISPID_IHTMLDOMATTRIBUTE2_PARENTNODE = DISPID_DOMATTRIBUTE + 7;
        public const int DISPID_IHTMLDOMATTRIBUTE2_CHILDNODES = DISPID_DOMATTRIBUTE + 8;
        public const int DISPID_IHTMLDOMATTRIBUTE2_FIRSTCHILD = DISPID_DOMATTRIBUTE + 9;
        public const int DISPID_IHTMLDOMATTRIBUTE2_LASTCHILD = DISPID_DOMATTRIBUTE + 10;
        public const int DISPID_IHTMLDOMATTRIBUTE2_PREVIOUSSIBLING = DISPID_DOMATTRIBUTE + 11;
        public const int DISPID_IHTMLDOMATTRIBUTE2_NEXTSIBLING = DISPID_DOMATTRIBUTE + 12;
        public const int DISPID_IHTMLDOMATTRIBUTE2_ATTRIBUTES = DISPID_DOMATTRIBUTE + 13;
        public const int DISPID_IHTMLDOMATTRIBUTE2_OWNERDOCUMENT = DISPID_DOMATTRIBUTE + 14;
        public const int DISPID_IHTMLDOMATTRIBUTE2_INSERTBEFORE = DISPID_DOMATTRIBUTE + 15;
        public const int DISPID_IHTMLDOMATTRIBUTE2_REPLACECHILD = DISPID_DOMATTRIBUTE + 16;
        public const int DISPID_IHTMLDOMATTRIBUTE2_REMOVECHILD = DISPID_DOMATTRIBUTE + 17;
        public const int DISPID_IHTMLDOMATTRIBUTE2_APPENDCHILD = DISPID_DOMATTRIBUTE + 18;
        public const int DISPID_IHTMLDOMATTRIBUTE2_HASCHILDNODES = DISPID_DOMATTRIBUTE + 19;
        public const int DISPID_IHTMLDOMATTRIBUTE2_CLONENODE = DISPID_DOMATTRIBUTE + 20;

        public const int DISPID_HEDELEMS = DISPID_NORMAL_FIRST;

        //    DISPIDs for interface IHTMLHeadElement
        public const int DISPID_IHTMLHEADELEMENT_PROFILE = DISPID_HEDELEMS + 1;

        public const int DISPID_A_VALUE = DISPID_A_FIRST + 101;
        //    DISPIDs for interface IHTMLTitleElement
        public const int DISPID_IHTMLTITLEELEMENT_TEXT = DISPID_A_VALUE;

        //    DISPIDs for interface IHTMLMetaElement
        public const int DISPID_IHTMLMETAELEMENT_HTTPEQUIV = DISPID_HEDELEMS + 1;
        public const int DISPID_IHTMLMETAELEMENT_CONTENT = DISPID_HEDELEMS + 2;
        public const int DISPID_IHTMLMETAELEMENT_NAME = STDPROPID_XOBJ_NAME;
        public const int DISPID_IHTMLMETAELEMENT_URL = DISPID_HEDELEMS + 3;
        public const int DISPID_IHTMLMETAELEMENT_CHARSET = DISPID_HEDELEMS + 13;

        //    DISPIDs for interface IHTMLMetaElement2
        public const int DISPID_IHTMLMETAELEMENT2_SCHEME = DISPID_HEDELEMS + 20;

        //    DISPIDs for interface IHTMLBaseElement
        public const int DISPID_IHTMLBASEELEMENT_HREF = DISPID_HEDELEMS + 3;
        public const int DISPID_IHTMLBASEELEMENT_TARGET = DISPID_HEDELEMS + 4;

        //    DISPIDs for interface IHTMLNextIdElement
        public const int DISPID_IHTMLNEXTIDELEMENT_N = DISPID_HEDELEMS + 12;

        public const int DISPID_A_COLOR = DISPID_A_FIRST + 2;
        public const int DISPID_A_FONTFACE = DISPID_A_FIRST + 18;
        public const int DISPID_A_FONTSIZE = DISPID_A_FIRST + 19;
        public const int DISPID_A_FONTSTYLE = DISPID_A_FIRST + 24;
        public const int DISPID_A_FONTVARIANT = DISPID_A_FIRST + 25;
        public const int DISPID_A_BASEFONT = DISPID_A_FIRST + 26;
        public const int DISPID_A_FONTWEIGHT = DISPID_A_FIRST + 27;

        //    DISPIDs for interface IHTMLBaseFontElement
        public const int DISPID_IHTMLBASEFONTELEMENT_COLOR = DISPID_A_COLOR;
        public const int DISPID_IHTMLBASEFONTELEMENT_FACE = DISPID_A_FONTFACE;
        public const int DISPID_IHTMLBASEFONTELEMENT_SIZE = DISPID_A_BASEFONT;

        public const int DISPID_SCRIPT = DISPID_NORMAL_FIRST;
        //    DISPIDs for interface IHTMLScriptElement
        public const int DISPID_IHTMLSCRIPTELEMENT_SRC = DISPID_SCRIPT + 1;
        public const int DISPID_IHTMLSCRIPTELEMENT_HTMLFOR = DISPID_SCRIPT + 4;
        public const int DISPID_IHTMLSCRIPTELEMENT_EVENT = DISPID_SCRIPT + 5;
        public const int DISPID_IHTMLSCRIPTELEMENT_TEXT = DISPID_SCRIPT + 6;
        public const int DISPID_IHTMLSCRIPTELEMENT_DEFER = DISPID_SCRIPT + 7;
        public const int DISPID_IHTMLSCRIPTELEMENT_READYSTATE = DISPID_A_READYSTATE;
        public const int DISPID_IHTMLSCRIPTELEMENT_ONERROR = DISPID_EVPROP_ONERROR;
        public const int DISPID_IHTMLSCRIPTELEMENT_TYPE = DISPID_SCRIPT + 9;

        private const int DISPID_COMMENTPDL = DISPID_NORMAL_FIRST;
        //    DISPIDs for interface IHTMLCommentElement
        public const int DISPID_IHTMLCOMMENTELEMENT_TEXT = DISPID_COMMENTPDL + 1;
        public const int DISPID_IHTMLCOMMENTELEMENT_ATOMIC = DISPID_COMMENTPDL + 2;

        public const int DISPID_TABLE = DISPID_NORMAL_FIRST;
        public const int DISPID_TABLESECTION = DISPID_NORMAL_FIRST;
        public const int DISPID_TABLEROW = DISPID_NORMAL_FIRST;
        public const int DISPID_TABLECOL = DISPID_NORMAL_FIRST;
        public const int DISPID_A_BACKGROUNDIMAGE = DISPID_A_FIRST + 1;
        public const int DISPID_A_TABLEBORDERCOLOR = DISPID_A_FIRST + 28;
        public const int DISPID_A_TABLEBORDERCOLORLIGHT = DISPID_A_FIRST + 29;
        public const int DISPID_A_TABLEBORDERCOLORDARK = DISPID_A_FIRST + 30;
        public const int DISPID_A_TABLEVALIGN = DISPID_A_FIRST + 31;
        //unchecked((int)0x48)
        public const int STDPROPID_XOBJ_BLOCKALIGN = DISPID_XOBJ_BASE + 0x48;
        public const int DISPID_TABLECELL = DISPID_TEXTSITE + 1000;
        public const int DISPID_A_NOWRAP = DISPID_A_FIRST + 5;

        //    DISPIDs for interface IHTMLTable
        public const int DISPID_IHTMLTABLE_COLS = DISPID_TABLE + 1;
        public const int DISPID_IHTMLTABLE_BORDER = DISPID_TABLE + 2;
        public const int DISPID_IHTMLTABLE_FRAME = DISPID_TABLE + 4;
        public const int DISPID_IHTMLTABLE_RULES = DISPID_TABLE + 3;
        public const int DISPID_IHTMLTABLE_CELLSPACING = DISPID_TABLE + 5;
        public const int DISPID_IHTMLTABLE_CELLPADDING = DISPID_TABLE + 6;
        public const int DISPID_IHTMLTABLE_BACKGROUND = DISPID_A_BACKGROUNDIMAGE;
        public const int DISPID_IHTMLTABLE_BGCOLOR = DISPID_BACKCOLOR;
        public const int DISPID_IHTMLTABLE_BORDERCOLOR = DISPID_A_TABLEBORDERCOLOR;
        public const int DISPID_IHTMLTABLE_BORDERCOLORLIGHT = DISPID_A_TABLEBORDERCOLORLIGHT;
        public const int DISPID_IHTMLTABLE_BORDERCOLORDARK = DISPID_A_TABLEBORDERCOLORDARK;
        public const int DISPID_IHTMLTABLE_ALIGN = STDPROPID_XOBJ_CONTROLALIGN;
        public const int DISPID_IHTMLTABLE_REFRESH = DISPID_TABLE + 15;
        public const int DISPID_IHTMLTABLE_ROWS = DISPID_TABLE + 16;
        public const int DISPID_IHTMLTABLE_WIDTH = STDPROPID_XOBJ_WIDTH;
        public const int DISPID_IHTMLTABLE_HEIGHT = STDPROPID_XOBJ_HEIGHT;
        public const int DISPID_IHTMLTABLE_DATAPAGESIZE = DISPID_TABLE + 17;
        public const int DISPID_IHTMLTABLE_NEXTPAGE = DISPID_TABLE + 18;
        public const int DISPID_IHTMLTABLE_PREVIOUSPAGE = DISPID_TABLE + 19;
        public const int DISPID_IHTMLTABLE_THEAD = DISPID_TABLE + 20;
        public const int DISPID_IHTMLTABLE_TFOOT = DISPID_TABLE + 21;
        public const int DISPID_IHTMLTABLE_TBODIES = DISPID_TABLE + 24;
        public const int DISPID_IHTMLTABLE_CAPTION = DISPID_TABLE + 25;
        public const int DISPID_IHTMLTABLE_CREATETHEAD = DISPID_TABLE + 26;
        public const int DISPID_IHTMLTABLE_DELETETHEAD = DISPID_TABLE + 27;
        public const int DISPID_IHTMLTABLE_CREATETFOOT = DISPID_TABLE + 28;
        public const int DISPID_IHTMLTABLE_DELETETFOOT = DISPID_TABLE + 29;
        public const int DISPID_IHTMLTABLE_CREATECAPTION = DISPID_TABLE + 30;
        public const int DISPID_IHTMLTABLE_DELETECAPTION = DISPID_TABLE + 31;
        public const int DISPID_IHTMLTABLE_INSERTROW = DISPID_TABLE + 32;
        public const int DISPID_IHTMLTABLE_DELETEROW = DISPID_TABLE + 33;
        public const int DISPID_IHTMLTABLE_READYSTATE = DISPID_A_READYSTATE;
        public const int DISPID_IHTMLTABLE_ONREADYSTATECHANGE = DISPID_EVPROP_ONREADYSTATECHANGE;

        //    DISPIDs for interface IHTMLTable2
        public const int DISPID_IHTMLTABLE2_FIRSTPAGE = DISPID_TABLE + 35;
        public const int DISPID_IHTMLTABLE2_LASTPAGE = DISPID_TABLE + 36;
        public const int DISPID_IHTMLTABLE2_CELLS = DISPID_TABLE + 37;
        public const int DISPID_IHTMLTABLE2_MOVEROW = DISPID_TABLE + 38;

        //    DISPIDs for interface IHTMLTable3
        public const int DISPID_IHTMLTABLE3_SUMMARY = DISPID_TABLE + 39;

        //    DISPIDs for interface IHTMLTableCol
        public const int DISPID_IHTMLTABLECOL_SPAN = DISPID_TABLECOL + 1;
        public const int DISPID_IHTMLTABLECOL_WIDTH = STDPROPID_XOBJ_WIDTH;
        public const int DISPID_IHTMLTABLECOL_ALIGN = STDPROPID_XOBJ_BLOCKALIGN;
        public const int DISPID_IHTMLTABLECOL_VALIGN = DISPID_A_TABLEVALIGN;

        //    DISPIDs for interface IHTMLTableCol2
        public const int DISPID_IHTMLTABLECOL2_CH = DISPID_TABLECOL + 2;
        public const int DISPID_IHTMLTABLECOL2_CHOFF = DISPID_TABLECOL + 3;

        //    DISPIDs for interface IHTMLTableSection
        public const int DISPID_IHTMLTABLESECTION_ALIGN = STDPROPID_XOBJ_BLOCKALIGN;
        public const int DISPID_IHTMLTABLESECTION_VALIGN = DISPID_A_TABLEVALIGN;
        public const int DISPID_IHTMLTABLESECTION_BGCOLOR = DISPID_BACKCOLOR;
        public const int DISPID_IHTMLTABLESECTION_ROWS = DISPID_TABLESECTION;
        public const int DISPID_IHTMLTABLESECTION_INSERTROW = DISPID_TABLESECTION + 1;
        public const int DISPID_IHTMLTABLESECTION_DELETEROW = DISPID_TABLESECTION + 2;

        //    DISPIDs for interface IHTMLTableSection2
        public const int DISPID_IHTMLTABLESECTION2_MOVEROW = DISPID_TABLESECTION + 3;

        //    DISPIDs for interface IHTMLTableSection3
        public const int DISPID_IHTMLTABLESECTION3_CH = DISPID_TABLESECTION + 4;
        public const int DISPID_IHTMLTABLESECTION3_CHOFF = DISPID_TABLESECTION + 5;

        //    DISPIDs for interface IHTMLTableRow
        public const int DISPID_IHTMLTABLEROW_ALIGN = STDPROPID_XOBJ_BLOCKALIGN;
        public const int DISPID_IHTMLTABLEROW_VALIGN = DISPID_A_TABLEVALIGN;
        public const int DISPID_IHTMLTABLEROW_BGCOLOR = DISPID_BACKCOLOR;
        public const int DISPID_IHTMLTABLEROW_BORDERCOLOR = DISPID_A_TABLEBORDERCOLOR;
        public const int DISPID_IHTMLTABLEROW_BORDERCOLORLIGHT = DISPID_A_TABLEBORDERCOLORLIGHT;
        public const int DISPID_IHTMLTABLEROW_BORDERCOLORDARK = DISPID_A_TABLEBORDERCOLORDARK;
        public const int DISPID_IHTMLTABLEROW_ROWINDEX = DISPID_TABLEROW;
        public const int DISPID_IHTMLTABLEROW_SECTIONROWINDEX = DISPID_TABLEROW + 1;
        public const int DISPID_IHTMLTABLEROW_CELLS = DISPID_TABLEROW + 2;
        public const int DISPID_IHTMLTABLEROW_INSERTCELL = DISPID_TABLEROW + 3;
        public const int DISPID_IHTMLTABLEROW_DELETECELL = DISPID_TABLEROW + 4;

        //    DISPIDs for interface IHTMLTableRow2
        public const int DISPID_IHTMLTABLEROW2_HEIGHT = STDPROPID_XOBJ_HEIGHT;

        //    DISPIDs for interface IHTMLTableRow3
        public const int DISPID_IHTMLTABLEROW3_CH = DISPID_TABLEROW + 9;
        public const int DISPID_IHTMLTABLEROW3_CHOFF = DISPID_TABLEROW + 10;

        //    DISPIDs for interface IHTMLTableRowMetrics
        public const int DISPID_IHTMLTABLEROWMETRICS_CLIENTHEIGHT = DISPID_SITE + 19;
        public const int DISPID_IHTMLTABLEROWMETRICS_CLIENTWIDTH = DISPID_SITE + 20;
        public const int DISPID_IHTMLTABLEROWMETRICS_CLIENTTOP = DISPID_SITE + 21;
        public const int DISPID_IHTMLTABLEROWMETRICS_CLIENTLEFT = DISPID_SITE + 22;

        //    DISPIDs for interface IHTMLTableCell
        public const int DISPID_IHTMLTABLECELL_ROWSPAN = DISPID_TABLECELL + 1;
        public const int DISPID_IHTMLTABLECELL_COLSPAN = DISPID_TABLECELL + 2;
        public const int DISPID_IHTMLTABLECELL_ALIGN = STDPROPID_XOBJ_BLOCKALIGN;
        public const int DISPID_IHTMLTABLECELL_VALIGN = DISPID_A_TABLEVALIGN;
        public const int DISPID_IHTMLTABLECELL_BGCOLOR = DISPID_BACKCOLOR;
        public const int DISPID_IHTMLTABLECELL_NOWRAP = DISPID_A_NOWRAP;
        public const int DISPID_IHTMLTABLECELL_BACKGROUND = DISPID_A_BACKGROUNDIMAGE;
        public const int DISPID_IHTMLTABLECELL_BORDERCOLOR = DISPID_A_TABLEBORDERCOLOR;
        public const int DISPID_IHTMLTABLECELL_BORDERCOLORLIGHT = DISPID_A_TABLEBORDERCOLORLIGHT;
        public const int DISPID_IHTMLTABLECELL_BORDERCOLORDARK = DISPID_A_TABLEBORDERCOLORDARK;
        public const int DISPID_IHTMLTABLECELL_WIDTH = STDPROPID_XOBJ_WIDTH;
        public const int DISPID_IHTMLTABLECELL_HEIGHT = STDPROPID_XOBJ_HEIGHT;
        public const int DISPID_IHTMLTABLECELL_CELLINDEX = DISPID_TABLECELL + 3;

        //    DISPIDs for interface IHTMLTableCell
        public const int DISPID_IHTMLTABLECELL2_ABBR = DISPID_TABLECELL + 4;
        public const int DISPID_IHTMLTABLECELL2_AXIS = DISPID_TABLECELL + 5;
        public const int DISPID_IHTMLTABLECELL2_CH = DISPID_TABLECELL + 6;
        public const int DISPID_IHTMLTABLECELL2_CHOFF = DISPID_TABLECELL + 7;
        public const int DISPID_IHTMLTABLECELL2_HEADERS = DISPID_TABLECELL + 8;
        public const int DISPID_IHTMLTABLECELL2_SCOPE = DISPID_TABLECELL + 9;

        //    DISPIDs for event set HTMLElementEvents2

        public const int DISPID_HTMLELEMENTEVENTS2_ONHELP = DISPID_EVMETH_ONHELP;
        public const int DISPID_HTMLELEMENTEVENTS2_ONCLICK = DISPID_EVMETH_ONCLICK;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDBLCLICK = DISPID_EVMETH_ONDBLCLICK;
        public const int DISPID_HTMLELEMENTEVENTS2_ONKEYPRESS = DISPID_EVMETH_ONKEYPRESS;
        public const int DISPID_HTMLELEMENTEVENTS2_ONKEYDOWN = DISPID_EVMETH_ONKEYDOWN;
        public const int DISPID_HTMLELEMENTEVENTS2_ONKEYUP = DISPID_EVMETH_ONKEYUP;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOUSEOUT = DISPID_EVMETH_ONMOUSEOUT;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOUSEOVER = DISPID_EVMETH_ONMOUSEOVER;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOUSEMOVE = DISPID_EVMETH_ONMOUSEMOVE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOUSEDOWN = DISPID_EVMETH_ONMOUSEDOWN;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOUSEUP = DISPID_EVMETH_ONMOUSEUP;
        public const int DISPID_HTMLELEMENTEVENTS2_ONSELECTSTART = DISPID_EVMETH_ONSELECTSTART;
        public const int DISPID_HTMLELEMENTEVENTS2_ONFILTERCHANGE = DISPID_EVMETH_ONFILTER;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDRAGSTART = DISPID_EVMETH_ONDRAGSTART;
        public const int DISPID_HTMLELEMENTEVENTS2_ONBEFOREUPDATE = DISPID_EVMETH_ONBEFOREUPDATE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONAFTERUPDATE = DISPID_EVMETH_ONAFTERUPDATE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONERRORUPDATE = DISPID_EVMETH_ONERRORUPDATE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONROWEXIT = DISPID_EVMETH_ONROWEXIT;
        public const int DISPID_HTMLELEMENTEVENTS2_ONROWENTER = DISPID_EVMETH_ONROWENTER;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDATASETCHANGED = DISPID_EVMETH_ONDATASETCHANGED;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDATAAVAILABLE = DISPID_EVMETH_ONDATAAVAILABLE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDATASETCOMPLETE = DISPID_EVMETH_ONDATASETCOMPLETE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONLOSECAPTURE = DISPID_EVMETH_ONLOSECAPTURE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONPROPERTYCHANGE = DISPID_EVMETH_ONPROPERTYCHANGE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONSCROLL = DISPID_EVMETH_ONSCROLL;
        public const int DISPID_HTMLELEMENTEVENTS2_ONFOCUS = DISPID_EVMETH_ONFOCUS;
        public const int DISPID_HTMLELEMENTEVENTS2_ONBLUR = DISPID_EVMETH_ONBLUR;
        public const int DISPID_HTMLELEMENTEVENTS2_ONRESIZE = DISPID_EVMETH_ONRESIZE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDRAG = DISPID_EVMETH_ONDRAG;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDRAGEND = DISPID_EVMETH_ONDRAGEND;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDRAGENTER = DISPID_EVMETH_ONDRAGENTER;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDRAGOVER = DISPID_EVMETH_ONDRAGOVER;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDRAGLEAVE = DISPID_EVMETH_ONDRAGLEAVE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDROP = DISPID_EVMETH_ONDROP;
        public const int DISPID_HTMLELEMENTEVENTS2_ONBEFORECUT = DISPID_EVMETH_ONBEFORECUT;
        public const int DISPID_HTMLELEMENTEVENTS2_ONCUT = DISPID_EVMETH_ONCUT;
        public const int DISPID_HTMLELEMENTEVENTS2_ONBEFORECOPY = DISPID_EVMETH_ONBEFORECOPY;
        public const int DISPID_HTMLELEMENTEVENTS2_ONCOPY = DISPID_EVMETH_ONCOPY;
        public const int DISPID_HTMLELEMENTEVENTS2_ONBEFOREPASTE = DISPID_EVMETH_ONBEFOREPASTE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONPASTE = DISPID_EVMETH_ONPASTE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONCONTEXTMENU = DISPID_EVMETH_ONCONTEXTMENU;
        public const int DISPID_HTMLELEMENTEVENTS2_ONROWSDELETE = DISPID_EVMETH_ONROWSDELETE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONROWSINSERTED = DISPID_EVMETH_ONROWSINSERTED;
        public const int DISPID_HTMLELEMENTEVENTS2_ONCELLCHANGE = DISPID_EVMETH_ONCELLCHANGE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONREADYSTATECHANGE = DISPID_EVMETH_ONREADYSTATECHANGE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONLAYOUTCOMPLETE = DISPID_EVMETH_ONLAYOUTCOMPLETE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONPAGE = DISPID_EVMETH_ONPAGE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOUSEENTER = DISPID_EVMETH_ONMOUSEENTER;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOUSELEAVE = DISPID_EVMETH_ONMOUSELEAVE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONACTIVATE = DISPID_EVMETH_ONACTIVATE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONDEACTIVATE = DISPID_EVMETH_ONDEACTIVATE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONBEFOREDEACTIVATE = DISPID_EVMETH_ONBEFOREDEACTIVATE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONBEFOREACTIVATE = DISPID_EVMETH_ONBEFOREACTIVATE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONFOCUSIN = DISPID_EVMETH_ONFOCUSIN;
        public const int DISPID_HTMLELEMENTEVENTS2_ONFOCUSOUT = DISPID_EVMETH_ONFOCUSOUT;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOVE = DISPID_EVMETH_ONMOVE;
        public const int DISPID_HTMLELEMENTEVENTS2_ONCONTROLSELECT = DISPID_EVMETH_ONCONTROLSELECT;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOVESTART = DISPID_EVMETH_ONMOVESTART;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOVEEND = DISPID_EVMETH_ONMOVEEND;
        public const int DISPID_HTMLELEMENTEVENTS2_ONRESIZESTART = DISPID_EVMETH_ONRESIZESTART;
        public const int DISPID_HTMLELEMENTEVENTS2_ONRESIZEEND = DISPID_EVMETH_ONRESIZEEND;
        public const int DISPID_HTMLELEMENTEVENTS2_ONMOUSEWHEEL = DISPID_EVMETH_ONMOUSEWHEEL;

        public const int DISPID_HR = DISPID_NORMAL_FIRST;
        public const int DISPID_IHTMLHRELEMENT_ALIGN = STDPROPID_XOBJ_BLOCKALIGN;
        public const int DISPID_IHTMLHRELEMENT_COLOR = DISPID_A_COLOR;
        public const int DISPID_IHTMLHRELEMENT_NOSHADE = DISPID_HR + 1;
        public const int DISPID_IHTMLHRELEMENT_WIDTH = STDPROPID_XOBJ_WIDTH;
        public const int DISPID_IHTMLHRELEMENT_SIZE = STDPROPID_XOBJ_HEIGHT;


        //    DISPIDs for interface IHTMLInputElement

        public const int DISPID_IHTMLINPUTELEMENT_TYPE = DISPID_INPUT;
        public const int DISPID_IHTMLINPUTELEMENT_VALUE = DISPID_A_VALUE;
        public const int DISPID_IHTMLINPUTELEMENT_NAME = STDPROPID_XOBJ_NAME;
        public const int DISPID_IHTMLINPUTELEMENT_STATUS = DISPID_INPUT + 1;
        public const int DISPID_IHTMLINPUTELEMENT_DISABLED = STDPROPID_XOBJ_DISABLED;
        public const int DISPID_IHTMLINPUTELEMENT_FORM = DISPID_SITE + 4;
        public const int DISPID_IHTMLINPUTELEMENT_SIZE = DISPID_INPUT + 2;
        public const int DISPID_IHTMLINPUTELEMENT_MAXLENGTH = DISPID_INPUT + 3;
        public const int DISPID_IHTMLINPUTELEMENT_SELECT = DISPID_INPUT + 4;
        public const int DISPID_IHTMLINPUTELEMENT_ONCHANGE = DISPID_EVPROP_ONCHANGE;
        public const int DISPID_IHTMLINPUTELEMENT_ONSELECT = DISPID_EVPROP_ONSELECT;
        public const int DISPID_IHTMLINPUTELEMENT_DEFAULTVALUE = DISPID_DEFAULTVALUE;
        public const int DISPID_IHTMLINPUTELEMENT_READONLY = DISPID_INPUT + 5;
        public const int DISPID_IHTMLINPUTELEMENT_CREATETEXTRANGE = DISPID_INPUT + 6;
        public const int DISPID_IHTMLINPUTELEMENT_INDETERMINATE = DISPID_INPUT + 7;
        public const int DISPID_IHTMLINPUTELEMENT_DEFAULTCHECKED = DISPID_INPUT + 8;
        public const int DISPID_IHTMLINPUTELEMENT_CHECKED = DISPID_INPUT + 9;
        public const int DISPID_IHTMLINPUTELEMENT_BORDER = DISPID_INPUT + 12;
        public const int DISPID_IHTMLINPUTELEMENT_VSPACE = DISPID_INPUT + 13;
        public const int DISPID_IHTMLINPUTELEMENT_HSPACE = DISPID_INPUT + 14;
        public const int DISPID_IHTMLINPUTELEMENT_ALT = DISPID_INPUT + 10;
        public const int DISPID_IHTMLINPUTELEMENT_SRC = DISPID_INPUT + 11;
        public const int DISPID_IHTMLINPUTELEMENT_LOWSRC = DISPID_INPUT + 15;
        public const int DISPID_IHTMLINPUTELEMENT_VRML = DISPID_INPUT + 16;
        public const int DISPID_IHTMLINPUTELEMENT_DYNSRC = DISPID_INPUT + 17;
        public const int DISPID_IHTMLINPUTELEMENT_READYSTATE = DISPID_A_READYSTATE;
        public const int DISPID_IHTMLINPUTELEMENT_COMPLETE = DISPID_INPUT + 18;
        public const int DISPID_IHTMLINPUTELEMENT_LOOP = DISPID_INPUT + 19;
        public const int DISPID_IHTMLINPUTELEMENT_ALIGN = STDPROPID_XOBJ_CONTROLALIGN;
        public const int DISPID_IHTMLINPUTELEMENT_ONLOAD = DISPID_EVPROP_ONLOAD;
        public const int DISPID_IHTMLINPUTELEMENT_ONERROR = DISPID_EVPROP_ONERROR;
        public const int DISPID_IHTMLINPUTELEMENT_ONABORT = DISPID_EVPROP_ONABORT;
        public const int DISPID_IHTMLINPUTELEMENT_WIDTH = STDPROPID_XOBJ_WIDTH;
        public const int DISPID_IHTMLINPUTELEMENT_HEIGHT = STDPROPID_XOBJ_HEIGHT;
        public const int DISPID_IHTMLINPUTELEMENT_START = DISPID_INPUT + 20;

        //    DISPIDs for interface IHTMLSelectElement

        public const int DISPID_IHTMLSELECTELEMENT_SIZE = DISPID_SELECT + 2;
        public const int DISPID_IHTMLSELECTELEMENT_MULTIPLE = DISPID_SELECT + 3;
        public const int DISPID_IHTMLSELECTELEMENT_NAME = STDPROPID_XOBJ_NAME;
        public const int DISPID_IHTMLSELECTELEMENT_OPTIONS = DISPID_SELECT + 5;
        public const int DISPID_IHTMLSELECTELEMENT_ONCHANGE = DISPID_EVPROP_ONCHANGE;
        public const int DISPID_IHTMLSELECTELEMENT_SELECTEDINDEX = DISPID_SELECT + 10;
        public const int DISPID_IHTMLSELECTELEMENT_TYPE = DISPID_SELECT + 12;
        public const int DISPID_IHTMLSELECTELEMENT_VALUE = DISPID_SELECT + 11;
        public const int DISPID_IHTMLSELECTELEMENT_DISABLED = STDPROPID_XOBJ_DISABLED;
        public const int DISPID_IHTMLSELECTELEMENT_FORM = DISPID_SITE + 4;
        public const int DISPID_IHTMLSELECTELEMENT_ADD = DISPID_COLLECTION + 3;
        public const int DISPID_IHTMLSELECTELEMENT_REMOVE = DISPID_COLLECTION + 4;
        public const int DISPID_IHTMLSELECTELEMENT_LENGTH = DISPID_COLLECTION;
        public const int DISPID_IHTMLSELECTELEMENT__NEWENUM = DISPID_NEWENUM;
        public const int DISPID_IHTMLSELECTELEMENT_ITEM = DISPID_VALUE;
        public const int DISPID_IHTMLSELECTELEMENT_TAGS = DISPID_COLLECTION + 2;

        //    DISPIDs for interface IHTMLTextAreaElement
        public const int DISPID_TEXTAREA = (DISPID_INPUTTEXT + 1000);
        public const int DISPID_MARQUEE = (DISPID_TEXTAREA + 1000);
        public const int DISPID_RICHTEXT = (DISPID_MARQUEE + 1000);

        public const int DISPID_IHTMLTEXTAREAELEMENT_TYPE = DISPID_INPUT;
        public const int DISPID_IHTMLTEXTAREAELEMENT_VALUE = DISPID_A_VALUE;
        public const int DISPID_IHTMLTEXTAREAELEMENT_NAME = STDPROPID_XOBJ_NAME;
        public const int DISPID_IHTMLTEXTAREAELEMENT_STATUS = DISPID_INPUT + 1;
        public const int DISPID_IHTMLTEXTAREAELEMENT_DISABLED = STDPROPID_XOBJ_DISABLED;
        public const int DISPID_IHTMLTEXTAREAELEMENT_FORM = DISPID_SITE + 4;
        public const int DISPID_IHTMLTEXTAREAELEMENT_DEFAULTVALUE = DISPID_DEFAULTVALUE;
        public const int DISPID_IHTMLTEXTAREAELEMENT_SELECT = DISPID_RICHTEXT + 5;
        public const int DISPID_IHTMLTEXTAREAELEMENT_ONCHANGE = DISPID_EVPROP_ONCHANGE;
        public const int DISPID_IHTMLTEXTAREAELEMENT_ONSELECT = DISPID_EVPROP_ONSELECT;
        public const int DISPID_IHTMLTEXTAREAELEMENT_READONLY = DISPID_RICHTEXT + 4;
        public const int DISPID_IHTMLTEXTAREAELEMENT_ROWS = DISPID_RICHTEXT + 1;
        public const int DISPID_IHTMLTEXTAREAELEMENT_COLS = DISPID_RICHTEXT + 2;
        public const int DISPID_IHTMLTEXTAREAELEMENT_WRAP = DISPID_RICHTEXT + 3;
        public const int DISPID_IHTMLTEXTAREAELEMENT_CREATETEXTRANGE = DISPID_RICHTEXT + 6;

        //    DISPIDs for interface IHTMLFormElement

        public const int DISPID_FORM = DISPID_NORMAL_FIRST;
        public const int DISPID_IHTMLFORMELEMENT_ACTION = DISPID_FORM + 1;
        public const int DISPID_IHTMLFORMELEMENT_DIR = DISPID_A_DIR;
        public const int DISPID_IHTMLFORMELEMENT_ENCODING = DISPID_FORM + 3;
        public const int DISPID_IHTMLFORMELEMENT_METHOD = DISPID_FORM + 4;
        public const int DISPID_IHTMLFORMELEMENT_ELEMENTS = DISPID_FORM + 5;
        public const int DISPID_IHTMLFORMELEMENT_TARGET = DISPID_FORM + 6;
        public const int DISPID_IHTMLFORMELEMENT_NAME = STDPROPID_XOBJ_NAME;
        public const int DISPID_IHTMLFORMELEMENT_ONSUBMIT = DISPID_EVPROP_ONSUBMIT;
        public const int DISPID_IHTMLFORMELEMENT_ONRESET = DISPID_EVPROP_ONRESET;
        public const int DISPID_IHTMLFORMELEMENT_SUBMIT = DISPID_FORM + 9;
        public const int DISPID_IHTMLFORMELEMENT_RESET = DISPID_FORM + 10;
        public const int DISPID_IHTMLFORMELEMENT_LENGTH = DISPID_COLLECTION;
        public const int DISPID_IHTMLFORMELEMENT__NEWENUM = DISPID_NEWENUM;
        public const int DISPID_IHTMLFORMELEMENT_ITEM = DISPID_VALUE;
        public const int DISPID_IHTMLFORMELEMENT_TAGS = DISPID_COLLECTION + 2;

        public const int DISPID_IHTMLCONTROLELEMENT_TABINDEX = STDPROPID_XOBJ_TABINDEX;
        public const int DISPID_IHTMLCONTROLELEMENT_FOCUS = DISPID_SITE + 0;
        public const int DISPID_IHTMLCONTROLELEMENT_ACCESSKEY = DISPID_SITE + 5;
        public const int DISPID_IHTMLCONTROLELEMENT_ONBLUR = DISPID_EVPROP_ONBLUR;
        public const int DISPID_IHTMLCONTROLELEMENT_ONFOCUS = DISPID_EVPROP_ONFOCUS;
        public const int DISPID_IHTMLCONTROLELEMENT_ONRESIZE = DISPID_EVPROP_ONRESIZE;
        public const int DISPID_IHTMLCONTROLELEMENT_BLUR = DISPID_SITE + 2;
        public const int DISPID_IHTMLCONTROLELEMENT_ADDFILTER = DISPID_SITE + 17;
        public const int DISPID_IHTMLCONTROLELEMENT_REMOVEFILTER = DISPID_SITE + 18;
        public const int DISPID_IHTMLCONTROLELEMENT_CLIENTHEIGHT = DISPID_SITE + 19;
        public const int DISPID_IHTMLCONTROLELEMENT_CLIENTWIDTH = DISPID_SITE + 20;
        public const int DISPID_IHTMLCONTROLELEMENT_CLIENTTOP = DISPID_SITE + 21;
        public const int DISPID_IHTMLCONTROLELEMENT_CLIENTLEFT = DISPID_SITE + 22;

        public const int DISPID_IHTMLCONTROLRANGE_SELECT = DISPID_RANGE + 2;
        public const int DISPID_IHTMLCONTROLRANGE_ADD = DISPID_RANGE + 3;
        public const int DISPID_IHTMLCONTROLRANGE_REMOVE = DISPID_RANGE + 4;
        public const int DISPID_IHTMLCONTROLRANGE_ITEM = DISPID_VALUE;
        public const int DISPID_IHTMLCONTROLRANGE_SCROLLINTOVIEW = DISPID_RANGE + 6;
        public const int DISPID_IHTMLCONTROLRANGE_QUERYCOMMANDSUPPORTED = DISPID_RANGE + 7;
        public const int DISPID_IHTMLCONTROLRANGE_QUERYCOMMANDENABLED = DISPID_RANGE + 8;
        public const int DISPID_IHTMLCONTROLRANGE_QUERYCOMMANDSTATE = DISPID_RANGE + 9;
        public const int DISPID_IHTMLCONTROLRANGE_QUERYCOMMANDINDETERM = DISPID_RANGE + 10;
        public const int DISPID_IHTMLCONTROLRANGE_QUERYCOMMANDTEXT = DISPID_RANGE + 11;
        public const int DISPID_IHTMLCONTROLRANGE_QUERYCOMMANDVALUE = DISPID_RANGE + 12;
        public const int DISPID_IHTMLCONTROLRANGE_EXECCOMMAND = DISPID_RANGE + 13;
        public const int DISPID_IHTMLCONTROLRANGE_EXECCOMMANDSHOWHELP = DISPID_RANGE + 14;
        public const int DISPID_IHTMLCONTROLRANGE_COMMONPARENTELEMENT = DISPID_RANGE + 15;
        public const int DISPID_IHTMLCONTROLRANGE_LENGTH = DISPID_RANGE + 5;

    }

    #region IHTMLStyle Interface
    /// <summary><para><c>IHTMLStyle</c> interface.</para></summary>
    [Guid("3050F25E-98B5-11CF-BB82-00AA00BDCE0B")]
    [ComImport]
    [TypeLibType((short)4160)]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLStyle
    {
        /// <summary><para><c>setAttribute</c> method of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>setAttribute</c> method was the following:  <c>HRESULT setAttribute (BSTR strAttributeName, VARIANT AttributeValue, [optional, defaultvalue(1)] long lFlags)</c>;</para></remarks>
        // IDL: HRESULT setAttribute (BSTR strAttributeName, VARIANT AttributeValue, [optional, defaultvalue(1)] long lFlags);
        // VB6: Sub setAttribute (ByVal strAttributeName As String, ByVal AttributeValue As Any, [ByVal lFlags As Long = 1])
        [DispId(-2147417611)]
        void setAttribute([MarshalAs(UnmanagedType.BStr)] string strAttributeName, object AttributeValue, int lFlags);

        /// <summary><para><c>getAttribute</c> method of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>getAttribute</c> method was the following:  <c>HRESULT getAttribute (BSTR strAttributeName, [optional, defaultvalue(0)] long lFlags, [out, retval] VARIANT* ReturnValue)</c>;</para></remarks>
        // IDL: HRESULT getAttribute (BSTR strAttributeName, [optional, defaultvalue(0)] long lFlags, [out, retval] VARIANT* ReturnValue);
        // VB6: Function getAttribute (ByVal strAttributeName As String, [ByVal lFlags As Long = 0]) As Any
        [DispId(-2147417610)]
        object getAttribute([MarshalAs(UnmanagedType.BStr)] string strAttributeName, int lFlags);

        /// <summary><para><c>removeAttribute</c> method of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>removeAttribute</c> method was the following:  <c>HRESULT removeAttribute (BSTR strAttributeName, [optional, defaultvalue(1)] long lFlags, [out, retval] VARIANT_BOOL* ReturnValue)</c>;</para></remarks>
        // IDL: HRESULT removeAttribute (BSTR strAttributeName, [optional, defaultvalue(1)] long lFlags, [out, retval] VARIANT_BOOL* ReturnValue);
        // VB6: Function removeAttribute (ByVal strAttributeName As String, [ByVal lFlags As Long = 1]) As Boolean
        [DispId(-2147417609)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        bool removeAttribute([MarshalAs(UnmanagedType.BStr)] string strAttributeName, int lFlags);

        /// <summary><para><c>toString</c> method of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>toString</c> method was the following:  <c>HRESULT toString ([out, retval] BSTR* ReturnValue)</c>;</para></remarks>
        // IDL: HRESULT toString ([out, retval] BSTR* ReturnValue);
        // VB6: Function toString As String
        [DispId(-2147414104)]
        [return: MarshalAs(UnmanagedType.BStr)]
        string toString();

        /// <summary><para><c>background</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>background</c> property was the following:  <c>BSTR background</c>;</para></remarks>
        // IDL: BSTR background;
        // VB6: background As String
        string background
        {
            // IDL: HRESULT background ([out, retval] BSTR* ReturnValue);
            // VB6: Function background As String
            [DispId(-2147413080)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT background (BSTR value);
            // VB6: Sub background (ByVal value As String)
            [DispId(-2147413080)]
            set;
        }

        /// <summary><para><c>backgroundAttachment</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>backgroundAttachment</c> property was the following:  <c>BSTR backgroundAttachment</c>;</para></remarks>
        // IDL: BSTR backgroundAttachment;
        // VB6: backgroundAttachment As String
        string backgroundAttachment
        {
            // IDL: HRESULT backgroundAttachment ([out, retval] BSTR* ReturnValue);
            // VB6: Function backgroundAttachment As String
            [DispId(-2147413067)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT backgroundAttachment (BSTR value);
            // VB6: Sub backgroundAttachment (ByVal value As String)
            [DispId(-2147413067)]
            set;
        }

        /// <summary><para><c>backgroundColor</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>backgroundColor</c> property was the following:  <c>VARIANT backgroundColor</c>;</para></remarks>
        // IDL: VARIANT backgroundColor;
        // VB6: backgroundColor As Any
        object backgroundColor
        {
            // IDL: HRESULT backgroundColor ([out, retval] VARIANT* ReturnValue);
            // VB6: Function backgroundColor As Any
            [DispId(-501)]
            get;
            // IDL: HRESULT backgroundColor (VARIANT value);
            // VB6: Sub backgroundColor (ByVal value As Any)
            [DispId(-501)]
            set;
        }

        /// <summary><para><c>backgroundImage</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>backgroundImage</c> property was the following:  <c>BSTR backgroundImage</c>;</para></remarks>
        // IDL: BSTR backgroundImage;
        // VB6: backgroundImage As String
        string backgroundImage
        {
            // IDL: HRESULT backgroundImage ([out, retval] BSTR* ReturnValue);
            // VB6: Function backgroundImage As String
            [DispId(-2147413111)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT backgroundImage (BSTR value);
            // VB6: Sub backgroundImage (ByVal value As String)
            [DispId(-2147413111)]
            set;
        }

        /// <summary><para><c>backgroundPosition</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>backgroundPosition</c> property was the following:  <c>BSTR backgroundPosition</c>;</para></remarks>
        // IDL: BSTR backgroundPosition;
        // VB6: backgroundPosition As String
        string backgroundPosition
        {
            // IDL: HRESULT backgroundPosition ([out, retval] BSTR* ReturnValue);
            // VB6: Function backgroundPosition As String
            [DispId(-2147413066)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT backgroundPosition (BSTR value);
            // VB6: Sub backgroundPosition (ByVal value As String)
            [DispId(-2147413066)]
            set;
        }

        /// <summary><para><c>backgroundPositionX</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>backgroundPositionX</c> property was the following:  <c>VARIANT backgroundPositionX</c>;</para></remarks>
        // IDL: VARIANT backgroundPositionX;
        // VB6: backgroundPositionX As Any
        object backgroundPositionX
        {
            // IDL: HRESULT backgroundPositionX ([out, retval] VARIANT* ReturnValue);
            // VB6: Function backgroundPositionX As Any
            [DispId(-2147413079)]
            get;
            // IDL: HRESULT backgroundPositionX (VARIANT value);
            // VB6: Sub backgroundPositionX (ByVal value As Any)
            [DispId(-2147413079)]
            set;
        }

        /// <summary><para><c>backgroundPositionY</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>backgroundPositionY</c> property was the following:  <c>VARIANT backgroundPositionY</c>;</para></remarks>
        // IDL: VARIANT backgroundPositionY;
        // VB6: backgroundPositionY As Any
        object backgroundPositionY
        {
            // IDL: HRESULT backgroundPositionY ([out, retval] VARIANT* ReturnValue);
            // VB6: Function backgroundPositionY As Any
            [DispId(-2147413078)]
            get;
            // IDL: HRESULT backgroundPositionY (VARIANT value);
            // VB6: Sub backgroundPositionY (ByVal value As Any)
            [DispId(-2147413078)]
            set;
        }

        /// <summary><para><c>backgroundRepeat</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>backgroundRepeat</c> property was the following:  <c>BSTR backgroundRepeat</c>;</para></remarks>
        // IDL: BSTR backgroundRepeat;
        // VB6: backgroundRepeat As String
        string backgroundRepeat
        {
            // IDL: HRESULT backgroundRepeat ([out, retval] BSTR* ReturnValue);
            // VB6: Function backgroundRepeat As String
            [DispId(-2147413068)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT backgroundRepeat (BSTR value);
            // VB6: Sub backgroundRepeat (ByVal value As String)
            [DispId(-2147413068)]
            set;
        }

        /// <summary><para><c>border</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>border</c> property was the following:  <c>BSTR border</c>;</para></remarks>
        // IDL: BSTR border;
        // VB6: border As String
        string border
        {
            // IDL: HRESULT border ([out, retval] BSTR* ReturnValue);
            // VB6: Function border As String
            [DispId(-2147413063)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT border (BSTR value);
            // VB6: Sub border (ByVal value As String)
            [DispId(-2147413063)]
            set;
        }

        /// <summary><para><c>borderBottom</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderBottom</c> property was the following:  <c>BSTR borderBottom</c>;</para></remarks>
        // IDL: BSTR borderBottom;
        // VB6: borderBottom As String
        string borderBottom
        {
            // IDL: HRESULT borderBottom ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderBottom As String
            [DispId(-2147413060)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderBottom (BSTR value);
            // VB6: Sub borderBottom (ByVal value As String)
            [DispId(-2147413060)]
            set;
        }

        /// <summary><para><c>borderBottomColor</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderBottomColor</c> property was the following:  <c>VARIANT borderBottomColor</c>;</para></remarks>
        // IDL: VARIANT borderBottomColor;
        // VB6: borderBottomColor As Any
        object borderBottomColor
        {
            // IDL: HRESULT borderBottomColor ([out, retval] VARIANT* ReturnValue);
            // VB6: Function borderBottomColor As Any
            [DispId(-2147413055)]
            get;
            // IDL: HRESULT borderBottomColor (VARIANT value);
            // VB6: Sub borderBottomColor (ByVal value As Any)
            [DispId(-2147413055)]
            set;
        }

        /// <summary><para><c>borderBottomStyle</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderBottomStyle</c> property was the following:  <c>BSTR borderBottomStyle</c>;</para></remarks>
        // IDL: BSTR borderBottomStyle;
        // VB6: borderBottomStyle As String
        string borderBottomStyle
        {
            // IDL: HRESULT borderBottomStyle ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderBottomStyle As String
            [DispId(-2147413045)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderBottomStyle (BSTR value);
            // VB6: Sub borderBottomStyle (ByVal value As String)
            [DispId(-2147413045)]
            set;
        }

        /// <summary><para><c>borderBottomWidth</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderBottomWidth</c> property was the following:  <c>VARIANT borderBottomWidth</c>;</para></remarks>
        // IDL: VARIANT borderBottomWidth;
        // VB6: borderBottomWidth As Any
        object borderBottomWidth
        {
            // IDL: HRESULT borderBottomWidth ([out, retval] VARIANT* ReturnValue);
            // VB6: Function borderBottomWidth As Any
            [DispId(-2147413050)]
            get;
            // IDL: HRESULT borderBottomWidth (VARIANT value);
            // VB6: Sub borderBottomWidth (ByVal value As Any)
            [DispId(-2147413050)]
            set;
        }

        /// <summary><para><c>borderColor</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderColor</c> property was the following:  <c>BSTR borderColor</c>;</para></remarks>
        // IDL: BSTR borderColor;
        // VB6: borderColor As String
        string borderColor
        {
            // IDL: HRESULT borderColor ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderColor As String
            [DispId(-2147413058)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderColor (BSTR value);
            // VB6: Sub borderColor (ByVal value As String)
            [DispId(-2147413058)]
            set;
        }

        /// <summary><para><c>borderLeft</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderLeft</c> property was the following:  <c>BSTR borderLeft</c>;</para></remarks>
        // IDL: BSTR borderLeft;
        // VB6: borderLeft As String
        string borderLeft
        {
            // IDL: HRESULT borderLeft ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderLeft As String
            [DispId(-2147413059)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderLeft (BSTR value);
            // VB6: Sub borderLeft (ByVal value As String)
            [DispId(-2147413059)]
            set;
        }

        /// <summary><para><c>borderLeftColor</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderLeftColor</c> property was the following:  <c>VARIANT borderLeftColor</c>;</para></remarks>
        // IDL: VARIANT borderLeftColor;
        // VB6: borderLeftColor As Any
        object borderLeftColor
        {
            // IDL: HRESULT borderLeftColor ([out, retval] VARIANT* ReturnValue);
            // VB6: Function borderLeftColor As Any
            [DispId(-2147413054)]
            get;
            // IDL: HRESULT borderLeftColor (VARIANT value);
            // VB6: Sub borderLeftColor (ByVal value As Any)
            [DispId(-2147413054)]
            set;
        }

        /// <summary><para><c>borderLeftStyle</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderLeftStyle</c> property was the following:  <c>BSTR borderLeftStyle</c>;</para></remarks>
        // IDL: BSTR borderLeftStyle;
        // VB6: borderLeftStyle As String
        string borderLeftStyle
        {
            // IDL: HRESULT borderLeftStyle ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderLeftStyle As String
            [DispId(-2147413044)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderLeftStyle (BSTR value);
            // VB6: Sub borderLeftStyle (ByVal value As String)
            [DispId(-2147413044)]
            set;
        }

        /// <summary><para><c>borderLeftWidth</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderLeftWidth</c> property was the following:  <c>VARIANT borderLeftWidth</c>;</para></remarks>
        // IDL: VARIANT borderLeftWidth;
        // VB6: borderLeftWidth As Any
        object borderLeftWidth
        {
            // IDL: HRESULT borderLeftWidth ([out, retval] VARIANT* ReturnValue);
            // VB6: Function borderLeftWidth As Any
            [DispId(-2147413049)]
            get;
            // IDL: HRESULT borderLeftWidth (VARIANT value);
            // VB6: Sub borderLeftWidth (ByVal value As Any)
            [DispId(-2147413049)]
            set;
        }

        /// <summary><para><c>borderRight</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderRight</c> property was the following:  <c>BSTR borderRight</c>;</para></remarks>
        // IDL: BSTR borderRight;
        // VB6: borderRight As String
        string borderRight
        {
            // IDL: HRESULT borderRight ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderRight As String
            [DispId(-2147413061)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderRight (BSTR value);
            // VB6: Sub borderRight (ByVal value As String)
            [DispId(-2147413061)]
            set;
        }

        /// <summary><para><c>borderRightColor</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderRightColor</c> property was the following:  <c>VARIANT borderRightColor</c>;</para></remarks>
        // IDL: VARIANT borderRightColor;
        // VB6: borderRightColor As Any
        object borderRightColor
        {
            // IDL: HRESULT borderRightColor ([out, retval] VARIANT* ReturnValue);
            // VB6: Function borderRightColor As Any
            [DispId(-2147413056)]
            get;
            // IDL: HRESULT borderRightColor (VARIANT value);
            // VB6: Sub borderRightColor (ByVal value As Any)
            [DispId(-2147413056)]
            set;
        }

        /// <summary><para><c>borderRightStyle</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderRightStyle</c> property was the following:  <c>BSTR borderRightStyle</c>;</para></remarks>
        // IDL: BSTR borderRightStyle;
        // VB6: borderRightStyle As String
        string borderRightStyle
        {
            // IDL: HRESULT borderRightStyle ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderRightStyle As String
            [DispId(-2147413046)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderRightStyle (BSTR value);
            // VB6: Sub borderRightStyle (ByVal value As String)
            [DispId(-2147413046)]
            set;
        }

        /// <summary><para><c>borderRightWidth</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderRightWidth</c> property was the following:  <c>VARIANT borderRightWidth</c>;</para></remarks>
        // IDL: VARIANT borderRightWidth;
        // VB6: borderRightWidth As Any
        object borderRightWidth
        {
            // IDL: HRESULT borderRightWidth ([out, retval] VARIANT* ReturnValue);
            // VB6: Function borderRightWidth As Any
            [DispId(-2147413051)]
            get;
            // IDL: HRESULT borderRightWidth (VARIANT value);
            // VB6: Sub borderRightWidth (ByVal value As Any)
            [DispId(-2147413051)]
            set;
        }

        /// <summary><para><c>borderStyle</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderStyle</c> property was the following:  <c>BSTR borderStyle</c>;</para></remarks>
        // IDL: BSTR borderStyle;
        // VB6: borderStyle As String
        string borderStyle
        {
            // IDL: HRESULT borderStyle ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderStyle As String
            [DispId(-2147413048)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderStyle (BSTR value);
            // VB6: Sub borderStyle (ByVal value As String)
            [DispId(-2147413048)]
            set;
        }

        /// <summary><para><c>borderTop</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderTop</c> property was the following:  <c>BSTR borderTop</c>;</para></remarks>
        // IDL: BSTR borderTop;
        // VB6: borderTop As String
        string borderTop
        {
            // IDL: HRESULT borderTop ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderTop As String
            [DispId(-2147413062)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderTop (BSTR value);
            // VB6: Sub borderTop (ByVal value As String)
            [DispId(-2147413062)]
            set;
        }

        /// <summary><para><c>borderTopColor</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderTopColor</c> property was the following:  <c>VARIANT borderTopColor</c>;</para></remarks>
        // IDL: VARIANT borderTopColor;
        // VB6: borderTopColor As Any
        object borderTopColor
        {
            // IDL: HRESULT borderTopColor ([out, retval] VARIANT* ReturnValue);
            // VB6: Function borderTopColor As Any
            [DispId(-2147413057)]
            get;
            // IDL: HRESULT borderTopColor (VARIANT value);
            // VB6: Sub borderTopColor (ByVal value As Any)
            [DispId(-2147413057)]
            set;
        }

        /// <summary><para><c>borderTopStyle</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderTopStyle</c> property was the following:  <c>BSTR borderTopStyle</c>;</para></remarks>
        // IDL: BSTR borderTopStyle;
        // VB6: borderTopStyle As String
        string borderTopStyle
        {
            // IDL: HRESULT borderTopStyle ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderTopStyle As String
            [DispId(-2147413047)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderTopStyle (BSTR value);
            // VB6: Sub borderTopStyle (ByVal value As String)
            [DispId(-2147413047)]
            set;
        }

        /// <summary><para><c>borderTopWidth</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderTopWidth</c> property was the following:  <c>VARIANT borderTopWidth</c>;</para></remarks>
        // IDL: VARIANT borderTopWidth;
        // VB6: borderTopWidth As Any
        object borderTopWidth
        {
            // IDL: HRESULT borderTopWidth ([out, retval] VARIANT* ReturnValue);
            // VB6: Function borderTopWidth As Any
            [DispId(-2147413052)]
            get;
            // IDL: HRESULT borderTopWidth (VARIANT value);
            // VB6: Sub borderTopWidth (ByVal value As Any)
            [DispId(-2147413052)]
            set;
        }

        /// <summary><para><c>borderWidth</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>borderWidth</c> property was the following:  <c>BSTR borderWidth</c>;</para></remarks>
        // IDL: BSTR borderWidth;
        // VB6: borderWidth As String
        string borderWidth
        {
            // IDL: HRESULT borderWidth ([out, retval] BSTR* ReturnValue);
            // VB6: Function borderWidth As String
            [DispId(-2147413053)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT borderWidth (BSTR value);
            // VB6: Sub borderWidth (ByVal value As String)
            [DispId(-2147413053)]
            set;
        }

        /// <summary><para><c>clear</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>clear</c> property was the following:  <c>BSTR clear</c>;</para></remarks>
        // IDL: BSTR clear;
        // VB6: clear As String
        string clear
        {
            // IDL: HRESULT clear ([out, retval] BSTR* ReturnValue);
            // VB6: Function clear As String
            [DispId(-2147413096)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT clear (BSTR value);
            // VB6: Sub clear (ByVal value As String)
            [DispId(-2147413096)]
            set;
        }

        /// <summary><para><c>clip</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>clip</c> property was the following:  <c>BSTR clip</c>;</para></remarks>
        // IDL: BSTR clip;
        // VB6: clip As String
        string clip
        {
            // IDL: HRESULT clip ([out, retval] BSTR* ReturnValue);
            // VB6: Function clip As String
            [DispId(-2147413020)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT clip (BSTR value);
            // VB6: Sub clip (ByVal value As String)
            [DispId(-2147413020)]
            set;
        }

        /// <summary><para><c>color</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>color</c> property was the following:  <c>VARIANT color</c>;</para></remarks>
        // IDL: VARIANT color;
        // VB6: color As Any
        object color
        {
            // IDL: HRESULT color ([out, retval] VARIANT* ReturnValue);
            // VB6: Function color As Any
            [DispId(-2147413110)]
            get;
            // IDL: HRESULT color (VARIANT value);
            // VB6: Sub color (ByVal value As Any)
            [DispId(-2147413110)]
            set;
        }

        /// <summary><para><c>cssText</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>cssText</c> property was the following:  <c>BSTR cssText</c>;</para></remarks>
        // IDL: BSTR cssText;
        // VB6: cssText As String
        string cssText
        {
            // IDL: HRESULT cssText ([out, retval] BSTR* ReturnValue);
            // VB6: Function cssText As String
            [DispId(-2147413013)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT cssText (BSTR value);
            // VB6: Sub cssText (ByVal value As String)
            [DispId(-2147413013)]
            set;
        }

        /// <summary><para><c>cursor</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>cursor</c> property was the following:  <c>BSTR cursor</c>;</para></remarks>
        // IDL: BSTR cursor;
        // VB6: cursor As String
        string cursor
        {
            // IDL: HRESULT cursor ([out, retval] BSTR* ReturnValue);
            // VB6: Function cursor As String
            [DispId(-2147413010)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT cursor (BSTR value);
            // VB6: Sub cursor (ByVal value As String)
            [DispId(-2147413010)]
            set;
        }

        /// <summary><para><c>display</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>display</c> property was the following:  <c>BSTR display</c>;</para></remarks>
        // IDL: BSTR display;
        // VB6: display As String
        string display
        {
            // IDL: HRESULT display ([out, retval] BSTR* ReturnValue);
            // VB6: Function display As String
            [DispId(-2147413041)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT display (BSTR value);
            // VB6: Sub display (ByVal value As String)
            [DispId(-2147413041)]
            set;
        }

        /// <summary><para><c>filter</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>filter</c> property was the following:  <c>BSTR filter</c>;</para></remarks>
        // IDL: BSTR filter;
        // VB6: filter As String
        string filter
        {
            // IDL: HRESULT filter ([out, retval] BSTR* ReturnValue);
            // VB6: Function filter As String
            [DispId(-2147413030)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT filter (BSTR value);
            // VB6: Sub filter (ByVal value As String)
            [DispId(-2147413030)]
            set;
        }

        /// <summary><para><c>font</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>font</c> property was the following:  <c>BSTR font</c>;</para></remarks>
        // IDL: BSTR font;
        // VB6: font As String
        string font
        {
            // IDL: HRESULT font ([out, retval] BSTR* ReturnValue);
            // VB6: Function font As String
            [DispId(-2147413071)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT font (BSTR value);
            // VB6: Sub font (ByVal value As String)
            [DispId(-2147413071)]
            set;
        }

        /// <summary><para><c>fontFamily</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>fontFamily</c> property was the following:  <c>BSTR fontFamily</c>;</para></remarks>
        // IDL: BSTR fontFamily;
        // VB6: fontFamily As String
        string fontFamily
        {
            // IDL: HRESULT fontFamily ([out, retval] BSTR* ReturnValue);
            // VB6: Function fontFamily As String
            [DispId(-2147413094)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT fontFamily (BSTR value);
            // VB6: Sub fontFamily (ByVal value As String)
            [DispId(-2147413094)]
            set;
        }

        /// <summary><para><c>fontSize</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>fontSize</c> property was the following:  <c>VARIANT fontSize</c>;</para></remarks>
        // IDL: VARIANT fontSize;
        // VB6: fontSize As Any
        object fontSize
        {
            // IDL: HRESULT fontSize ([out, retval] VARIANT* ReturnValue);
            // VB6: Function fontSize As Any
            [DispId(-2147413093)]
            get;
            // IDL: HRESULT fontSize (VARIANT value);
            // VB6: Sub fontSize (ByVal value As Any)
            [DispId(-2147413093)]
            set;
        }

        /// <summary><para><c>fontStyle</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>fontStyle</c> property was the following:  <c>BSTR fontStyle</c>;</para></remarks>
        // IDL: BSTR fontStyle;
        // VB6: fontStyle As String
        string fontStyle
        {
            // IDL: HRESULT fontStyle ([out, retval] BSTR* ReturnValue);
            // VB6: Function fontStyle As String
            [DispId(-2147413088)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT fontStyle (BSTR value);
            // VB6: Sub fontStyle (ByVal value As String)
            [DispId(-2147413088)]
            set;
        }

        /// <summary><para><c>fontVariant</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>fontVariant</c> property was the following:  <c>BSTR fontVariant</c>;</para></remarks>
        // IDL: BSTR fontVariant;
        // VB6: fontVariant As String
        string fontVariant
        {
            // IDL: HRESULT fontVariant ([out, retval] BSTR* ReturnValue);
            // VB6: Function fontVariant As String
            [DispId(-2147413087)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT fontVariant (BSTR value);
            // VB6: Sub fontVariant (ByVal value As String)
            [DispId(-2147413087)]
            set;
        }

        /// <summary><para><c>fontWeight</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>fontWeight</c> property was the following:  <c>BSTR fontWeight</c>;</para></remarks>
        // IDL: BSTR fontWeight;
        // VB6: fontWeight As String
        string fontWeight
        {
            // IDL: HRESULT fontWeight ([out, retval] BSTR* ReturnValue);
            // VB6: Function fontWeight As String
            [DispId(-2147413085)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT fontWeight (BSTR value);
            // VB6: Sub fontWeight (ByVal value As String)
            [DispId(-2147413085)]
            set;
        }

        /// <summary><para><c>height</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>height</c> property was the following:  <c>VARIANT height</c>;</para></remarks>
        // IDL: VARIANT height;
        // VB6: height As Any
        object height
        {
            // IDL: HRESULT height ([out, retval] VARIANT* ReturnValue);
            // VB6: Function height As Any
            [DispId(-2147418106)]
            get;
            // IDL: HRESULT height (VARIANT value);
            // VB6: Sub height (ByVal value As Any)
            [DispId(-2147418106)]
            set;
        }

        /// <summary><para><c>left</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>left</c> property was the following:  <c>VARIANT left</c>;</para></remarks>
        // IDL: VARIANT left;
        // VB6: left As Any
        object left
        {
            // IDL: HRESULT left ([out, retval] VARIANT* ReturnValue);
            // VB6: Function left As Any
            [DispId(-2147418109)]
            get;
            // IDL: HRESULT left (VARIANT value);
            // VB6: Sub left (ByVal value As Any)
            [DispId(-2147418109)]
            set;
        }

        /// <summary><para><c>letterSpacing</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>letterSpacing</c> property was the following:  <c>VARIANT letterSpacing</c>;</para></remarks>
        // IDL: VARIANT letterSpacing;
        // VB6: letterSpacing As Any
        object letterSpacing
        {
            // IDL: HRESULT letterSpacing ([out, retval] VARIANT* ReturnValue);
            // VB6: Function letterSpacing As Any
            [DispId(-2147413104)]
            get;
            // IDL: HRESULT letterSpacing (VARIANT value);
            // VB6: Sub letterSpacing (ByVal value As Any)
            [DispId(-2147413104)]
            set;
        }

        /// <summary><para><c>lineHeight</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>lineHeight</c> property was the following:  <c>VARIANT lineHeight</c>;</para></remarks>
        // IDL: VARIANT lineHeight;
        // VB6: lineHeight As Any
        object lineHeight
        {
            // IDL: HRESULT lineHeight ([out, retval] VARIANT* ReturnValue);
            // VB6: Function lineHeight As Any
            [DispId(-2147413106)]
            get;
            // IDL: HRESULT lineHeight (VARIANT value);
            // VB6: Sub lineHeight (ByVal value As Any)
            [DispId(-2147413106)]
            set;
        }

        /// <summary><para><c>listStyle</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>listStyle</c> property was the following:  <c>BSTR listStyle</c>;</para></remarks>
        // IDL: BSTR listStyle;
        // VB6: listStyle As String
        string listStyle
        {
            // IDL: HRESULT listStyle ([out, retval] BSTR* ReturnValue);
            // VB6: Function listStyle As String
            [DispId(-2147413037)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT listStyle (BSTR value);
            // VB6: Sub listStyle (ByVal value As String)
            [DispId(-2147413037)]
            set;
        }

        /// <summary><para><c>listStyleImage</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>listStyleImage</c> property was the following:  <c>BSTR listStyleImage</c>;</para></remarks>
        // IDL: BSTR listStyleImage;
        // VB6: listStyleImage As String
        string listStyleImage
        {
            // IDL: HRESULT listStyleImage ([out, retval] BSTR* ReturnValue);
            // VB6: Function listStyleImage As String
            [DispId(-2147413038)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT listStyleImage (BSTR value);
            // VB6: Sub listStyleImage (ByVal value As String)
            [DispId(-2147413038)]
            set;
        }

        /// <summary><para><c>listStylePosition</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>listStylePosition</c> property was the following:  <c>BSTR listStylePosition</c>;</para></remarks>
        // IDL: BSTR listStylePosition;
        // VB6: listStylePosition As String
        string listStylePosition
        {
            // IDL: HRESULT listStylePosition ([out, retval] BSTR* ReturnValue);
            // VB6: Function listStylePosition As String
            [DispId(-2147413039)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT listStylePosition (BSTR value);
            // VB6: Sub listStylePosition (ByVal value As String)
            [DispId(-2147413039)]
            set;
        }

        /// <summary><para><c>listStyleType</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>listStyleType</c> property was the following:  <c>BSTR listStyleType</c>;</para></remarks>
        // IDL: BSTR listStyleType;
        // VB6: listStyleType As String
        string listStyleType
        {
            // IDL: HRESULT listStyleType ([out, retval] BSTR* ReturnValue);
            // VB6: Function listStyleType As String
            [DispId(-2147413040)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT listStyleType (BSTR value);
            // VB6: Sub listStyleType (ByVal value As String)
            [DispId(-2147413040)]
            set;
        }

        /// <summary><para><c>margin</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>margin</c> property was the following:  <c>BSTR margin</c>;</para></remarks>
        // IDL: BSTR margin;
        // VB6: margin As String
        string margin
        {
            // IDL: HRESULT margin ([out, retval] BSTR* ReturnValue);
            // VB6: Function margin As String
            [DispId(-2147413076)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT margin (BSTR value);
            // VB6: Sub margin (ByVal value As String)
            [DispId(-2147413076)]
            set;
        }

        /// <summary><para><c>marginBottom</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>marginBottom</c> property was the following:  <c>VARIANT marginBottom</c>;</para></remarks>
        // IDL: VARIANT marginBottom;
        // VB6: marginBottom As Any
        object marginBottom
        {
            // IDL: HRESULT marginBottom ([out, retval] VARIANT* ReturnValue);
            // VB6: Function marginBottom As Any
            [DispId(-2147413073)]
            get;
            // IDL: HRESULT marginBottom (VARIANT value);
            // VB6: Sub marginBottom (ByVal value As Any)
            [DispId(-2147413073)]
            set;
        }

        /// <summary><para><c>marginLeft</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>marginLeft</c> property was the following:  <c>VARIANT marginLeft</c>;</para></remarks>
        // IDL: VARIANT marginLeft;
        // VB6: marginLeft As Any
        object marginLeft
        {
            // IDL: HRESULT marginLeft ([out, retval] VARIANT* ReturnValue);
            // VB6: Function marginLeft As Any
            [DispId(-2147413072)]
            get;
            // IDL: HRESULT marginLeft (VARIANT value);
            // VB6: Sub marginLeft (ByVal value As Any)
            [DispId(-2147413072)]
            set;
        }

        /// <summary><para><c>marginRight</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>marginRight</c> property was the following:  <c>VARIANT marginRight</c>;</para></remarks>
        // IDL: VARIANT marginRight;
        // VB6: marginRight As Any
        object marginRight
        {
            // IDL: HRESULT marginRight ([out, retval] VARIANT* ReturnValue);
            // VB6: Function marginRight As Any
            [DispId(-2147413074)]
            get;
            // IDL: HRESULT marginRight (VARIANT value);
            // VB6: Sub marginRight (ByVal value As Any)
            [DispId(-2147413074)]
            set;
        }

        /// <summary><para><c>marginTop</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>marginTop</c> property was the following:  <c>VARIANT marginTop</c>;</para></remarks>
        // IDL: VARIANT marginTop;
        // VB6: marginTop As Any
        object marginTop
        {
            // IDL: HRESULT marginTop ([out, retval] VARIANT* ReturnValue);
            // VB6: Function marginTop As Any
            [DispId(-2147413075)]
            get;
            // IDL: HRESULT marginTop (VARIANT value);
            // VB6: Sub marginTop (ByVal value As Any)
            [DispId(-2147413075)]
            set;
        }

        /// <summary><para><c>overflow</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>overflow</c> property was the following:  <c>BSTR overflow</c>;</para></remarks>
        // IDL: BSTR overflow;
        // VB6: overflow As String
        string overflow
        {
            // IDL: HRESULT overflow ([out, retval] BSTR* ReturnValue);
            // VB6: Function overflow As String
            [DispId(-2147413102)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT overflow (BSTR value);
            // VB6: Sub overflow (ByVal value As String)
            [DispId(-2147413102)]
            set;
        }

        /// <summary><para><c>padding</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>padding</c> property was the following:  <c>BSTR padding</c>;</para></remarks>
        // IDL: BSTR padding;
        // VB6: padding As String
        string padding
        {
            // IDL: HRESULT padding ([out, retval] BSTR* ReturnValue);
            // VB6: Function padding As String
            [DispId(-2147413101)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT padding (BSTR value);
            // VB6: Sub padding (ByVal value As String)
            [DispId(-2147413101)]
            set;
        }

        /// <summary><para><c>paddingBottom</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>paddingBottom</c> property was the following:  <c>VARIANT paddingBottom</c>;</para></remarks>
        // IDL: VARIANT paddingBottom;
        // VB6: paddingBottom As Any
        object paddingBottom
        {
            // IDL: HRESULT paddingBottom ([out, retval] VARIANT* ReturnValue);
            // VB6: Function paddingBottom As Any
            [DispId(-2147413098)]
            get;
            // IDL: HRESULT paddingBottom (VARIANT value);
            // VB6: Sub paddingBottom (ByVal value As Any)
            [DispId(-2147413098)]
            set;
        }

        /// <summary><para><c>paddingLeft</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>paddingLeft</c> property was the following:  <c>VARIANT paddingLeft</c>;</para></remarks>
        // IDL: VARIANT paddingLeft;
        // VB6: paddingLeft As Any
        object paddingLeft
        {
            // IDL: HRESULT paddingLeft ([out, retval] VARIANT* ReturnValue);
            // VB6: Function paddingLeft As Any
            [DispId(-2147413097)]
            get;
            // IDL: HRESULT paddingLeft (VARIANT value);
            // VB6: Sub paddingLeft (ByVal value As Any)
            [DispId(-2147413097)]
            set;
        }

        /// <summary><para><c>paddingRight</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>paddingRight</c> property was the following:  <c>VARIANT paddingRight</c>;</para></remarks>
        // IDL: VARIANT paddingRight;
        // VB6: paddingRight As Any
        object paddingRight
        {
            // IDL: HRESULT paddingRight ([out, retval] VARIANT* ReturnValue);
            // VB6: Function paddingRight As Any
            [DispId(-2147413099)]
            get;
            // IDL: HRESULT paddingRight (VARIANT value);
            // VB6: Sub paddingRight (ByVal value As Any)
            [DispId(-2147413099)]
            set;
        }

        /// <summary><para><c>paddingTop</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>paddingTop</c> property was the following:  <c>VARIANT paddingTop</c>;</para></remarks>
        // IDL: VARIANT paddingTop;
        // VB6: paddingTop As Any
        object paddingTop
        {
            // IDL: HRESULT paddingTop ([out, retval] VARIANT* ReturnValue);
            // VB6: Function paddingTop As Any
            [DispId(-2147413100)]
            get;
            // IDL: HRESULT paddingTop (VARIANT value);
            // VB6: Sub paddingTop (ByVal value As Any)
            [DispId(-2147413100)]
            set;
        }

        /// <summary><para><c>pageBreakAfter</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>pageBreakAfter</c> property was the following:  <c>BSTR pageBreakAfter</c>;</para></remarks>
        // IDL: BSTR pageBreakAfter;
        // VB6: pageBreakAfter As String
        string pageBreakAfter
        {
            // IDL: HRESULT pageBreakAfter ([out, retval] BSTR* ReturnValue);
            // VB6: Function pageBreakAfter As String
            [DispId(-2147413034)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT pageBreakAfter (BSTR value);
            // VB6: Sub pageBreakAfter (ByVal value As String)
            [DispId(-2147413034)]
            set;
        }

        /// <summary><para><c>pageBreakBefore</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>pageBreakBefore</c> property was the following:  <c>BSTR pageBreakBefore</c>;</para></remarks>
        // IDL: BSTR pageBreakBefore;
        // VB6: pageBreakBefore As String
        string pageBreakBefore
        {
            // IDL: HRESULT pageBreakBefore ([out, retval] BSTR* ReturnValue);
            // VB6: Function pageBreakBefore As String
            [DispId(-2147413035)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT pageBreakBefore (BSTR value);
            // VB6: Sub pageBreakBefore (ByVal value As String)
            [DispId(-2147413035)]
            set;
        }

        /// <summary><para><c>pixelHeight</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>pixelHeight</c> property was the following:  <c>long pixelHeight</c>;</para></remarks>
        // IDL: long pixelHeight;
        // VB6: pixelHeight As Long
        int pixelHeight
        {
            // IDL: HRESULT pixelHeight ([out, retval] long* ReturnValue);
            // VB6: Function pixelHeight As Long
            [DispId(-2147414109)]
            get;
            // IDL: HRESULT pixelHeight (long value);
            // VB6: Sub pixelHeight (ByVal value As Long)
            [DispId(-2147414109)]
            set;
        }

        /// <summary><para><c>pixelLeft</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>pixelLeft</c> property was the following:  <c>long pixelLeft</c>;</para></remarks>
        // IDL: long pixelLeft;
        // VB6: pixelLeft As Long
        int pixelLeft
        {
            // IDL: HRESULT pixelLeft ([out, retval] long* ReturnValue);
            // VB6: Function pixelLeft As Long
            [DispId(-2147414111)]
            get;
            // IDL: HRESULT pixelLeft (long value);
            // VB6: Sub pixelLeft (ByVal value As Long)
            [DispId(-2147414111)]
            set;
        }

        /// <summary><para><c>pixelTop</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>pixelTop</c> property was the following:  <c>long pixelTop</c>;</para></remarks>
        // IDL: long pixelTop;
        // VB6: pixelTop As Long
        int pixelTop
        {
            // IDL: HRESULT pixelTop ([out, retval] long* ReturnValue);
            // VB6: Function pixelTop As Long
            [DispId(-2147414112)]
            get;
            // IDL: HRESULT pixelTop (long value);
            // VB6: Sub pixelTop (ByVal value As Long)
            [DispId(-2147414112)]
            set;
        }

        /// <summary><para><c>pixelWidth</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>pixelWidth</c> property was the following:  <c>long pixelWidth</c>;</para></remarks>
        // IDL: long pixelWidth;
        // VB6: pixelWidth As Long
        int pixelWidth
        {
            // IDL: HRESULT pixelWidth ([out, retval] long* ReturnValue);
            // VB6: Function pixelWidth As Long
            [DispId(-2147414110)]
            get;
            // IDL: HRESULT pixelWidth (long value);
            // VB6: Sub pixelWidth (ByVal value As Long)
            [DispId(-2147414110)]
            set;
        }

        /// <summary><para><c>posHeight</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>posHeight</c> property was the following:  <c>float posHeight</c>;</para></remarks>
        // IDL: float posHeight;
        // VB6: posHeight As Single
        float posHeight
        {
            // IDL: HRESULT posHeight ([out, retval] float* ReturnValue);
            // VB6: Function posHeight As Single
            [DispId(-2147414105)]
            get;
            // IDL: HRESULT posHeight (float value);
            // VB6: Sub posHeight (ByVal value As Single)
            [DispId(-2147414105)]
            set;
        }

        /// <summary><para><c>position</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>position</c> property was the following:  <c>BSTR position</c>;</para></remarks>
        // IDL: BSTR position;
        // VB6: position As String
        string position
        {
            // IDL: HRESULT position ([out, retval] BSTR* ReturnValue);
            // VB6: Function position As String
            [DispId(-2147413022)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
        }

        /// <summary><para><c>posLeft</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>posLeft</c> property was the following:  <c>float posLeft</c>;</para></remarks>
        // IDL: float posLeft;
        // VB6: posLeft As Single
        float posLeft
        {
            // IDL: HRESULT posLeft ([out, retval] float* ReturnValue);
            // VB6: Function posLeft As Single
            [DispId(-2147414107)]
            get;
            // IDL: HRESULT posLeft (float value);
            // VB6: Sub posLeft (ByVal value As Single)
            [DispId(-2147414107)]
            set;
        }

        /// <summary><para><c>posTop</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>posTop</c> property was the following:  <c>float posTop</c>;</para></remarks>
        // IDL: float posTop;
        // VB6: posTop As Single
        float posTop
        {
            // IDL: HRESULT posTop ([out, retval] float* ReturnValue);
            // VB6: Function posTop As Single
            [DispId(-2147414108)]
            get;
            // IDL: HRESULT posTop (float value);
            // VB6: Sub posTop (ByVal value As Single)
            [DispId(-2147414108)]
            set;
        }

        /// <summary><para><c>posWidth</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>posWidth</c> property was the following:  <c>float posWidth</c>;</para></remarks>
        // IDL: float posWidth;
        // VB6: posWidth As Single
        float posWidth
        {
            // IDL: HRESULT posWidth ([out, retval] float* ReturnValue);
            // VB6: Function posWidth As Single
            [DispId(-2147414106)]
            get;
            // IDL: HRESULT posWidth (float value);
            // VB6: Sub posWidth (ByVal value As Single)
            [DispId(-2147414106)]
            set;
        }

        /// <summary><para><c>styleFloat</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>styleFloat</c> property was the following:  <c>BSTR styleFloat</c>;</para></remarks>
        // IDL: BSTR styleFloat;
        // VB6: styleFloat As String
        string styleFloat
        {
            // IDL: HRESULT styleFloat ([out, retval] BSTR* ReturnValue);
            // VB6: Function styleFloat As String
            [DispId(-2147413042)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT styleFloat (BSTR value);
            // VB6: Sub styleFloat (ByVal value As String)
            [DispId(-2147413042)]
            set;
        }

        /// <summary><para><c>textAlign</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>textAlign</c> property was the following:  <c>BSTR textAlign</c>;</para></remarks>
        // IDL: BSTR textAlign;
        // VB6: textAlign As String
        string textAlign
        {
            // IDL: HRESULT textAlign ([out, retval] BSTR* ReturnValue);
            // VB6: Function textAlign As String
            [DispId(-2147418040)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT textAlign (BSTR value);
            // VB6: Sub textAlign (ByVal value As String)
            [DispId(-2147418040)]
            set;
        }

        /// <summary><para><c>textDecoration</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>textDecoration</c> property was the following:  <c>BSTR textDecoration</c>;</para></remarks>
        // IDL: BSTR textDecoration;
        // VB6: textDecoration As String
        string textDecoration
        {
            // IDL: HRESULT textDecoration ([out, retval] BSTR* ReturnValue);
            // VB6: Function textDecoration As String
            [DispId(-2147413077)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT textDecoration (BSTR value);
            // VB6: Sub textDecoration (ByVal value As String)
            [DispId(-2147413077)]
            set;
        }

        /// <summary><para><c>textDecorationBlink</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>textDecorationBlink</c> property was the following:  <c>VARIANT_BOOL textDecorationBlink</c>;</para></remarks>
        // IDL: VARIANT_BOOL textDecorationBlink;
        // VB6: textDecorationBlink As Boolean
        bool textDecorationBlink
        {
            // IDL: HRESULT textDecorationBlink ([out, retval] VARIANT_BOOL* ReturnValue);
            // VB6: Function textDecorationBlink As Boolean
            [DispId(-2147413090)]
            [return: MarshalAs(UnmanagedType.VariantBool)]
            get;
            // IDL: HRESULT textDecorationBlink (VARIANT_BOOL value);
            // VB6: Sub textDecorationBlink (ByVal value As Boolean)
            [DispId(-2147413090)]
            set;
        }

        /// <summary><para><c>textDecorationLineThrough</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>textDecorationLineThrough</c> property was the following:  <c>VARIANT_BOOL textDecorationLineThrough</c>;</para></remarks>
        // IDL: VARIANT_BOOL textDecorationLineThrough;
        // VB6: textDecorationLineThrough As Boolean
        bool textDecorationLineThrough
        {
            // IDL: HRESULT textDecorationLineThrough ([out, retval] VARIANT_BOOL* ReturnValue);
            // VB6: Function textDecorationLineThrough As Boolean
            [DispId(-2147413092)]
            [return: MarshalAs(UnmanagedType.VariantBool)]
            get;
            // IDL: HRESULT textDecorationLineThrough (VARIANT_BOOL value);
            // VB6: Sub textDecorationLineThrough (ByVal value As Boolean)
            [DispId(-2147413092)]
            set;
        }

        /// <summary><para><c>textDecorationNone</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>textDecorationNone</c> property was the following:  <c>VARIANT_BOOL textDecorationNone</c>;</para></remarks>
        // IDL: VARIANT_BOOL textDecorationNone;
        // VB6: textDecorationNone As Boolean
        bool textDecorationNone
        {
            // IDL: HRESULT textDecorationNone ([out, retval] VARIANT_BOOL* ReturnValue);
            // VB6: Function textDecorationNone As Boolean
            [DispId(-2147413089)]
            [return: MarshalAs(UnmanagedType.VariantBool)]
            get;
            // IDL: HRESULT textDecorationNone (VARIANT_BOOL value);
            // VB6: Sub textDecorationNone (ByVal value As Boolean)
            [DispId(-2147413089)]
            set;
        }

        /// <summary><para><c>textDecorationOverline</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>textDecorationOverline</c> property was the following:  <c>VARIANT_BOOL textDecorationOverline</c>;</para></remarks>
        // IDL: VARIANT_BOOL textDecorationOverline;
        // VB6: textDecorationOverline As Boolean
        bool textDecorationOverline
        {
            // IDL: HRESULT textDecorationOverline ([out, retval] VARIANT_BOOL* ReturnValue);
            // VB6: Function textDecorationOverline As Boolean
            [DispId(-2147413043)]
            [return: MarshalAs(UnmanagedType.VariantBool)]
            get;
            // IDL: HRESULT textDecorationOverline (VARIANT_BOOL value);
            // VB6: Sub textDecorationOverline (ByVal value As Boolean)
            [DispId(-2147413043)]
            set;
        }

        /// <summary><para><c>textDecorationUnderline</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>textDecorationUnderline</c> property was the following:  <c>VARIANT_BOOL textDecorationUnderline</c>;</para></remarks>
        // IDL: VARIANT_BOOL textDecorationUnderline;
        // VB6: textDecorationUnderline As Boolean
        bool textDecorationUnderline
        {
            // IDL: HRESULT textDecorationUnderline ([out, retval] VARIANT_BOOL* ReturnValue);
            // VB6: Function textDecorationUnderline As Boolean
            [DispId(-2147413091)]
            [return: MarshalAs(UnmanagedType.VariantBool)]
            get;
            // IDL: HRESULT textDecorationUnderline (VARIANT_BOOL value);
            // VB6: Sub textDecorationUnderline (ByVal value As Boolean)
            [DispId(-2147413091)]
            set;
        }

        /// <summary><para><c>textIndent</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>textIndent</c> property was the following:  <c>VARIANT textIndent</c>;</para></remarks>
        // IDL: VARIANT textIndent;
        // VB6: textIndent As Any
        object textIndent
        {
            // IDL: HRESULT textIndent ([out, retval] VARIANT* ReturnValue);
            // VB6: Function textIndent As Any
            [DispId(-2147413105)]
            get;
            // IDL: HRESULT textIndent (VARIANT value);
            // VB6: Sub textIndent (ByVal value As Any)
            [DispId(-2147413105)]
            set;
        }

        /// <summary><para><c>textTransform</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>textTransform</c> property was the following:  <c>BSTR textTransform</c>;</para></remarks>
        // IDL: BSTR textTransform;
        // VB6: textTransform As String
        string textTransform
        {
            // IDL: HRESULT textTransform ([out, retval] BSTR* ReturnValue);
            // VB6: Function textTransform As String
            [DispId(-2147413108)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT textTransform (BSTR value);
            // VB6: Sub textTransform (ByVal value As String)
            [DispId(-2147413108)]
            set;
        }

        /// <summary><para><c>top</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>top</c> property was the following:  <c>VARIANT top</c>;</para></remarks>
        // IDL: VARIANT top;
        // VB6: top As Any
        object top
        {
            // IDL: HRESULT top ([out, retval] VARIANT* ReturnValue);
            // VB6: Function top As Any
            [DispId(-2147418108)]
            get;
            // IDL: HRESULT top (VARIANT value);
            // VB6: Sub top (ByVal value As Any)
            [DispId(-2147418108)]
            set;
        }

        /// <summary><para><c>verticalAlign</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>verticalAlign</c> property was the following:  <c>VARIANT verticalAlign</c>;</para></remarks>
        // IDL: VARIANT verticalAlign;
        // VB6: verticalAlign As Any
        object verticalAlign
        {
            // IDL: HRESULT verticalAlign ([out, retval] VARIANT* ReturnValue);
            // VB6: Function verticalAlign As Any
            [DispId(-2147413064)]
            get;
            // IDL: HRESULT verticalAlign (VARIANT value);
            // VB6: Sub verticalAlign (ByVal value As Any)
            [DispId(-2147413064)]
            set;
        }

        /// <summary><para><c>visibility</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>visibility</c> property was the following:  <c>BSTR visibility</c>;</para></remarks>
        // IDL: BSTR visibility;
        // VB6: visibility As String
        string visibility
        {
            // IDL: HRESULT visibility ([out, retval] BSTR* ReturnValue);
            // VB6: Function visibility As String
            [DispId(-2147413032)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT visibility (BSTR value);
            // VB6: Sub visibility (ByVal value As String)
            [DispId(-2147413032)]
            set;
        }

        /// <summary><para><c>whiteSpace</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>whiteSpace</c> property was the following:  <c>BSTR whiteSpace</c>;</para></remarks>
        // IDL: BSTR whiteSpace;
        // VB6: whiteSpace As String
        string whiteSpace
        {
            // IDL: HRESULT whiteSpace ([out, retval] BSTR* ReturnValue);
            // VB6: Function whiteSpace As String
            [DispId(-2147413036)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT whiteSpace (BSTR value);
            // VB6: Sub whiteSpace (ByVal value As String)
            [DispId(-2147413036)]
            set;
        }

        /// <summary><para><c>width</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>width</c> property was the following:  <c>VARIANT width</c>;</para></remarks>
        // IDL: VARIANT width;
        // VB6: width As Any
        object width
        {
            // IDL: HRESULT width ([out, retval] VARIANT* ReturnValue);
            // VB6: Function width As Any
            [DispId(-2147418107)]
            get;
            // IDL: HRESULT width (VARIANT value);
            // VB6: Sub width (ByVal value As Any)
            [DispId(-2147418107)]
            set;
        }

        /// <summary><para><c>wordSpacing</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>wordSpacing</c> property was the following:  <c>VARIANT wordSpacing</c>;</para></remarks>
        // IDL: VARIANT wordSpacing;
        // VB6: wordSpacing As Any
        object wordSpacing
        {
            // IDL: HRESULT wordSpacing ([out, retval] VARIANT* ReturnValue);
            // VB6: Function wordSpacing As Any
            [DispId(-2147413065)]
            get;
            // IDL: HRESULT wordSpacing (VARIANT value);
            // VB6: Sub wordSpacing (ByVal value As Any)
            [DispId(-2147413065)]
            set;
        }

        /// <summary><para><c>zIndex</c> property of <c>IHTMLStyle</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>zIndex</c> property was the following:  <c>VARIANT zIndex</c>;</para></remarks>
        // IDL: VARIANT zIndex;
        // VB6: zIndex As Any
        object zIndex
        {
            // IDL: HRESULT zIndex ([out, retval] VARIANT* ReturnValue);
            // VB6: Function zIndex As Any
            [DispId(-2147413021)]
            get;
            // IDL: HRESULT zIndex (VARIANT value);
            // VB6: Sub zIndex (ByVal value As Any)
            [DispId(-2147413021)]
            set;
        }
    }
    #endregion

    #region IHTMLElement Interface
    /// <summary><para><c>IHTMLElement</c> interface.</para></summary>
    [Guid("3050F1FF-98B5-11CF-BB82-00AA00BDCE0B")]
    [ComImport]
    [TypeLibType((short)4160)]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLElement
    {
        /// <summary><para><c>setAttribute</c> method of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>setAttribute</c> method was the following:  <c>HRESULT setAttribute (BSTR strAttributeName, VARIANT AttributeValue, [optional, defaultvalue(1)] long lFlags)</c>;</para></remarks>
        // IDL: HRESULT setAttribute (BSTR strAttributeName, VARIANT AttributeValue, [optional, defaultvalue(1)] long lFlags);
        // VB6: Sub setAttribute (ByVal strAttributeName As String, ByVal AttributeValue As Any, [ByVal lFlags As Long = 1])
        [DispId(HTMLDispIDs.DISPID_IHTMLELEMENT_SETATTRIBUTE)] // - 2147417611)]
        void setAttribute([MarshalAs(UnmanagedType.BStr)] string strAttributeName, object AttributeValue, int lFlags);

        /// <summary><para><c>getAttribute</c> method of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>getAttribute</c> method was the following:  <c>HRESULT getAttribute (BSTR strAttributeName, [optional, defaultvalue(0)] long lFlags, [out, retval] VARIANT* ReturnValue)</c>;</para></remarks>
        // IDL: HRESULT getAttribute (BSTR strAttributeName, [optional, defaultvalue(0)] long lFlags, [out, retval] VARIANT* ReturnValue);
        // VB6: Function getAttribute (ByVal strAttributeName As String, [ByVal lFlags As Long = 0]) As Any
        [DispId(-2147417610)]
        object getAttribute([MarshalAs(UnmanagedType.BStr)] string strAttributeName, int lFlags);

        /// <summary><para><c>removeAttribute</c> method of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>removeAttribute</c> method was the following:  <c>HRESULT removeAttribute (BSTR strAttributeName, [optional, defaultvalue(1)] long lFlags, [out, retval] VARIANT_BOOL* ReturnValue)</c>;</para></remarks>
        // IDL: HRESULT removeAttribute (BSTR strAttributeName, [optional, defaultvalue(1)] long lFlags, [out, retval] VARIANT_BOOL* ReturnValue);
        // VB6: Function removeAttribute (ByVal strAttributeName As String, [ByVal lFlags As Long = 1]) As Boolean
        [DispId(-2147417609)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        bool removeAttribute([MarshalAs(UnmanagedType.BStr)] string strAttributeName, int lFlags);

        /// <summary><para><c>scrollIntoView</c> method of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>scrollIntoView</c> method was the following:  <c>HRESULT scrollIntoView ([optional] VARIANT varargStart)</c>;</para></remarks>
        // IDL: HRESULT scrollIntoView ([optional] VARIANT varargStart);
        // VB6: Sub scrollIntoView ([ByVal varargStart As Any])
        [DispId(-2147417093)]
        void scrollIntoView(object varargStart);

        /// <summary><para><c>contains</c> method of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>contains</c> method was the following:  <c>HRESULT contains (IHTMLElement* pChild, [out, retval] VARIANT_BOOL* ReturnValue)</c>;</para></remarks>
        // IDL: HRESULT contains (IHTMLElement* pChild, [out, retval] VARIANT_BOOL* ReturnValue);
        // VB6: Function contains (ByVal pChild As IHTMLElement) As Boolean
        [DispId(-2147417092)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        bool contains(IHTMLElement pChild);

        /// <summary><para><c>insertAdjacentHTML</c> method of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>insertAdjacentHTML</c> method was the following:  <c>HRESULT insertAdjacentHTML (BSTR where, BSTR html)</c>;</para></remarks>
        // IDL: HRESULT insertAdjacentHTML (BSTR where, BSTR html);
        // VB6: Sub insertAdjacentHTML (ByVal where As String, ByVal html As String)
        [DispId(-2147417082)]
        void insertAdjacentHTML([MarshalAs(UnmanagedType.BStr)] string where, [MarshalAs(UnmanagedType.BStr)] string html);

        /// <summary><para><c>insertAdjacentText</c> method of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>insertAdjacentText</c> method was the following:  <c>HRESULT insertAdjacentText (BSTR where, BSTR text)</c>;</para></remarks>
        // IDL: HRESULT insertAdjacentText (BSTR where, BSTR text);
        // VB6: Sub insertAdjacentText (ByVal where As String, ByVal text As String)
        [DispId(-2147417081)]
        void insertAdjacentText([MarshalAs(UnmanagedType.BStr)] string where, [MarshalAs(UnmanagedType.BStr)] string text);

        /// <summary><para><c>click</c> method of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>click</c> method was the following:  <c>HRESULT click (void)</c>;</para></remarks>
        // IDL: HRESULT click (void);
        // VB6: Sub click
        [DispId(-2147417079)]
        void click();

        /// <summary><para><c>toString</c> method of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>toString</c> method was the following:  <c>HRESULT toString ([out, retval] BSTR* ReturnValue)</c>;</para></remarks>
        // IDL: HRESULT toString ([out, retval] BSTR* ReturnValue);
        // VB6: Function toString As String
        [DispId(-2147417076)]
        [return: MarshalAs(UnmanagedType.BStr)]
        string toString();

        /// <summary><para><c>all</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>all</c> property was the following:  <c>IDispatch* all</c>;</para></remarks>
        // IDL: IDispatch* all;
        // VB6: all As IDispatch
        object all
        {
            // IDL: HRESULT all ([out, retval] IDispatch** ReturnValue);
            // VB6: Function all As IDispatch
            [DispId(-2147417074)]
            [return: MarshalAs(UnmanagedType.IDispatch)]
            get;
        }

        /// <summary><para><c>children</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>children</c> property was the following:  <c>IDispatch* children</c>;</para></remarks>
        // IDL: IDispatch* children;
        // VB6: children As IDispatch
        object children
        {
            // IDL: HRESULT children ([out, retval] IDispatch** ReturnValue);
            // VB6: Function children As IDispatch
            [DispId(-2147417075)]
            [return: MarshalAs(UnmanagedType.IDispatch)]
            get;
        }

        /// <summary><para><c>className</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>className</c> property was the following:  <c>BSTR className</c>;</para></remarks>
        // IDL: BSTR className;
        // VB6: className As String
        string className
        {
            // IDL: HRESULT className ([out, retval] BSTR* ReturnValue);
            // VB6: Function className As String
            [DispId(-2147417111)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT className (BSTR value);
            // VB6: Sub className (ByVal value As String)
            [DispId(-2147417111)]
            set;
        }

        /// <summary><para><c>document</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>document</c> property was the following:  <c>IDispatch* document</c>;</para></remarks>
        // IDL: IDispatch* document;
        // VB6: document As IDispatch
        object document
        {
            // IDL: HRESULT document ([out, retval] IDispatch** ReturnValue);
            // VB6: Function document As IDispatch
            [DispId(-2147417094)]
            [return: MarshalAs(UnmanagedType.IDispatch)]
            get;
        }

        /// <summary><para><c>filters</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>filters</c> property was the following:  <c>IHTMLFiltersCollection* filters</c>;</para></remarks>
        // IDL: IHTMLFiltersCollection* filters;
        // VB6: filters As IHTMLFiltersCollection
        //IHTMLFiltersCollection filters
        object filters
        {
            // IDL: HRESULT filters ([out, retval] IHTMLFiltersCollection** ReturnValue);
            // VB6: Function filters As IHTMLFiltersCollection
            [DispId(-2147417077)]
            [return: MarshalAs(UnmanagedType.Interface)]
            get;
        }

        /// <summary><para><c>id</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>id</c> property was the following:  <c>BSTR id</c>;</para></remarks>
        // IDL: BSTR id;
        // VB6: id As String
        string id
        {
            // IDL: HRESULT id ([out, retval] BSTR* ReturnValue);
            // VB6: Function id As String
            [DispId(-2147417110)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT id (BSTR value);
            // VB6: Sub id (ByVal value As String)
            [DispId(-2147417110)]
            set;
        }

        /// <summary><para><c>innerHTML</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>innerHTML</c> property was the following:  <c>BSTR innerHTML</c>;</para></remarks>
        // IDL: BSTR innerHTML;
        // VB6: innerHTML As String
        string innerHTML
        {
            // IDL: HRESULT innerHTML ([out, retval] BSTR* ReturnValue);
            // VB6: Function innerHTML As String
            [DispId(-2147417086)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT innerHTML (BSTR value);
            // VB6: Sub innerHTML (ByVal value As String)
            [DispId(-2147417086)]
            set;
        }

        /// <summary><para><c>innerText</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>innerText</c> property was the following:  <c>BSTR innerText</c>;</para></remarks>
        // IDL: BSTR innerText;
        // VB6: innerText As String
        string innerText
        {
            // IDL: HRESULT innerText ([out, retval] BSTR* ReturnValue);
            // VB6: Function innerText As String
            [DispId(-2147417085)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT innerText (BSTR value);
            // VB6: Sub innerText (ByVal value As String)
            [DispId(-2147417085)]
            set;
        }

        /// <summary><para><c>isTextEdit</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>isTextEdit</c> property was the following:  <c>VARIANT_BOOL isTextEdit</c>;</para></remarks>
        // IDL: VARIANT_BOOL isTextEdit;
        // VB6: isTextEdit As Boolean
        bool isTextEdit
        {
            // IDL: HRESULT isTextEdit ([out, retval] VARIANT_BOOL* ReturnValue);
            // VB6: Function isTextEdit As Boolean
            [DispId(-2147417078)]
            [return: MarshalAs(UnmanagedType.VariantBool)]
            get;
        }

        /// <summary><para><c>lang</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>lang</c> property was the following:  <c>BSTR lang</c>;</para></remarks>
        // IDL: BSTR lang;
        // VB6: lang As String
        string lang
        {
            // IDL: HRESULT lang ([out, retval] BSTR* ReturnValue);
            // VB6: Function lang As String
            [DispId(-2147413103)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT lang (BSTR value);
            // VB6: Sub lang (ByVal value As String)
            [DispId(-2147413103)]
            set;
        }

        /// <summary><para><c>language</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>language</c> property was the following:  <c>BSTR language</c>;</para></remarks>
        // IDL: BSTR language;
        // VB6: language As String
        string language
        {
            // IDL: HRESULT language ([out, retval] BSTR* ReturnValue);
            // VB6: Function language As String
            [DispId(-2147413012)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT language (BSTR value);
            // VB6: Sub language (ByVal value As String)
            [DispId(-2147413012)]
            set;
        }

        /// <summary><para><c>offsetHeight</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>offsetHeight</c> property was the following:  <c>long offsetHeight</c>;</para></remarks>
        // IDL: long offsetHeight;
        // VB6: offsetHeight As Long
        int offsetHeight
        {
            // IDL: HRESULT offsetHeight ([out, retval] long* ReturnValue);
            // VB6: Function offsetHeight As Long
            [DispId(-2147417101)]
            get;
        }

        /// <summary><para><c>offsetLeft</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>offsetLeft</c> property was the following:  <c>long offsetLeft</c>;</para></remarks>
        // IDL: long offsetLeft;
        // VB6: offsetLeft As Long
        int offsetLeft
        {
            // IDL: HRESULT offsetLeft ([out, retval] long* ReturnValue);
            // VB6: Function offsetLeft As Long
            [DispId(-2147417104)]
            get;
        }

        /// <summary><para><c>offsetParent</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>offsetParent</c> property was the following:  <c>IHTMLElement* offsetParent</c>;</para></remarks>
        // IDL: IHTMLElement* offsetParent;
        // VB6: offsetParent As IHTMLElement
        IHTMLElement offsetParent
        {
            // IDL: HRESULT offsetParent ([out, retval] IHTMLElement** ReturnValue);
            // VB6: Function offsetParent As IHTMLElement
            [DispId(-2147417100)]
            get;
        }

        /// <summary><para><c>offsetTop</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>offsetTop</c> property was the following:  <c>long offsetTop</c>;</para></remarks>
        // IDL: long offsetTop;
        // VB6: offsetTop As Long
        int offsetTop
        {
            // IDL: HRESULT offsetTop ([out, retval] long* ReturnValue);
            // VB6: Function offsetTop As Long
            [DispId(-2147417103)]
            get;
        }

        /// <summary><para><c>offsetWidth</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>offsetWidth</c> property was the following:  <c>long offsetWidth</c>;</para></remarks>
        // IDL: long offsetWidth;
        // VB6: offsetWidth As Long
        int offsetWidth
        {
            // IDL: HRESULT offsetWidth ([out, retval] long* ReturnValue);
            // VB6: Function offsetWidth As Long
            [DispId(-2147417102)]
            get;
        }

        /// <summary><para><c>onafterupdate</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onafterupdate</c> property was the following:  <c>VARIANT onafterupdate</c>;</para></remarks>
        // IDL: VARIANT onafterupdate;
        // VB6: onafterupdate As Any
        object onafterupdate
        {
            // IDL: HRESULT onafterupdate ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onafterupdate As Any
            [DispId(-2147412090)]
            get;
            // IDL: HRESULT onafterupdate (VARIANT value);
            // VB6: Sub onafterupdate (ByVal value As Any)
            [DispId(-2147412090)]
            set;
        }

        /// <summary><para><c>onbeforeupdate</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onbeforeupdate</c> property was the following:  <c>VARIANT onbeforeupdate</c>;</para></remarks>
        // IDL: VARIANT onbeforeupdate;
        // VB6: onbeforeupdate As Any
        object onbeforeupdate
        {
            // IDL: HRESULT onbeforeupdate ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onbeforeupdate As Any
            [DispId(-2147412091)]
            get;
            // IDL: HRESULT onbeforeupdate (VARIANT value);
            // VB6: Sub onbeforeupdate (ByVal value As Any)
            [DispId(-2147412091)]
            set;
        }

        /// <summary><para><c>onclick</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onclick</c> property was the following:  <c>VARIANT onclick</c>;</para></remarks>
        // IDL: VARIANT onclick;
        // VB6: onclick As Any
        object onclick
        {
            // IDL: HRESULT onclick ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onclick As Any
            [DispId(-2147412104)]
            get;
            // IDL: HRESULT onclick (VARIANT value);
            // VB6: Sub onclick (ByVal value As Any)
            [DispId(-2147412104)]
            set;
        }

        /// <summary><para><c>ondataavailable</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>ondataavailable</c> property was the following:  <c>VARIANT ondataavailable</c>;</para></remarks>
        // IDL: VARIANT ondataavailable;
        // VB6: ondataavailable As Any
        object ondataavailable
        {
            // IDL: HRESULT ondataavailable ([out, retval] VARIANT* ReturnValue);
            // VB6: Function ondataavailable As Any
            [DispId(-2147412071)]
            get;
            // IDL: HRESULT ondataavailable (VARIANT value);
            // VB6: Sub ondataavailable (ByVal value As Any)
            [DispId(-2147412071)]
            set;
        }

        /// <summary><para><c>ondatasetchanged</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>ondatasetchanged</c> property was the following:  <c>VARIANT ondatasetchanged</c>;</para></remarks>
        // IDL: VARIANT ondatasetchanged;
        // VB6: ondatasetchanged As Any
        object ondatasetchanged
        {
            // IDL: HRESULT ondatasetchanged ([out, retval] VARIANT* ReturnValue);
            // VB6: Function ondatasetchanged As Any
            [DispId(-2147412072)]
            get;
            // IDL: HRESULT ondatasetchanged (VARIANT value);
            // VB6: Sub ondatasetchanged (ByVal value As Any)
            [DispId(-2147412072)]
            set;
        }

        /// <summary><para><c>ondatasetcomplete</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>ondatasetcomplete</c> property was the following:  <c>VARIANT ondatasetcomplete</c>;</para></remarks>
        // IDL: VARIANT ondatasetcomplete;
        // VB6: ondatasetcomplete As Any
        object ondatasetcomplete
        {
            // IDL: HRESULT ondatasetcomplete ([out, retval] VARIANT* ReturnValue);
            // VB6: Function ondatasetcomplete As Any
            [DispId(-2147412070)]
            get;
            // IDL: HRESULT ondatasetcomplete (VARIANT value);
            // VB6: Sub ondatasetcomplete (ByVal value As Any)
            [DispId(-2147412070)]
            set;
        }

        /// <summary><para><c>ondblclick</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>ondblclick</c> property was the following:  <c>VARIANT ondblclick</c>;</para></remarks>
        // IDL: VARIANT ondblclick;
        // VB6: ondblclick As Any
        object ondblclick
        {
            // IDL: HRESULT ondblclick ([out, retval] VARIANT* ReturnValue);
            // VB6: Function ondblclick As Any
            [DispId(-2147412103)]
            get;
            // IDL: HRESULT ondblclick (VARIANT value);
            // VB6: Sub ondblclick (ByVal value As Any)
            [DispId(-2147412103)]
            set;
        }

        /// <summary><para><c>ondragstart</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>ondragstart</c> property was the following:  <c>VARIANT ondragstart</c>;</para></remarks>
        // IDL: VARIANT ondragstart;
        // VB6: ondragstart As Any
        object ondragstart
        {
            // IDL: HRESULT ondragstart ([out, retval] VARIANT* ReturnValue);
            // VB6: Function ondragstart As Any
            [DispId(-2147412077)]
            get;
            // IDL: HRESULT ondragstart (VARIANT value);
            // VB6: Sub ondragstart (ByVal value As Any)
            [DispId(-2147412077)]
            set;
        }

        /// <summary><para><c>onerrorupdate</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onerrorupdate</c> property was the following:  <c>VARIANT onerrorupdate</c>;</para></remarks>
        // IDL: VARIANT onerrorupdate;
        // VB6: onerrorupdate As Any
        object onerrorupdate
        {
            // IDL: HRESULT onerrorupdate ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onerrorupdate As Any
            [DispId(-2147412074)]
            get;
            // IDL: HRESULT onerrorupdate (VARIANT value);
            // VB6: Sub onerrorupdate (ByVal value As Any)
            [DispId(-2147412074)]
            set;
        }

        /// <summary><para><c>onfilterchange</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onfilterchange</c> property was the following:  <c>VARIANT onfilterchange</c>;</para></remarks>
        // IDL: VARIANT onfilterchange;
        // VB6: onfilterchange As Any
        object onfilterchange
        {
            // IDL: HRESULT onfilterchange ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onfilterchange As Any
            [DispId(-2147412069)]
            get;
            // IDL: HRESULT onfilterchange (VARIANT value);
            // VB6: Sub onfilterchange (ByVal value As Any)
            [DispId(-2147412069)]
            set;
        }

        /// <summary><para><c>onhelp</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onhelp</c> property was the following:  <c>VARIANT onhelp</c>;</para></remarks>
        // IDL: VARIANT onhelp;
        // VB6: onhelp As Any
        object onhelp
        {
            // IDL: HRESULT onhelp ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onhelp As Any
            [DispId(-2147412099)]
            get;
            // IDL: HRESULT onhelp (VARIANT value);
            // VB6: Sub onhelp (ByVal value As Any)
            [DispId(-2147412099)]
            set;
        }

        /// <summary><para><c>onkeydown</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onkeydown</c> property was the following:  <c>VARIANT onkeydown</c>;</para></remarks>
        // IDL: VARIANT onkeydown;
        // VB6: onkeydown As Any
        object onkeydown
        {
            // IDL: HRESULT onkeydown ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onkeydown As Any
            [DispId(-2147412107)]
            get;
            // IDL: HRESULT onkeydown (VARIANT value);
            // VB6: Sub onkeydown (ByVal value As Any)
            [DispId(-2147412107)]
            set;
        }

        /// <summary><para><c>onkeypress</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onkeypress</c> property was the following:  <c>VARIANT onkeypress</c>;</para></remarks>
        // IDL: VARIANT onkeypress;
        // VB6: onkeypress As Any
        object onkeypress
        {
            // IDL: HRESULT onkeypress ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onkeypress As Any
            [DispId(-2147412105)]
            get;
            // IDL: HRESULT onkeypress (VARIANT value);
            // VB6: Sub onkeypress (ByVal value As Any)
            [DispId(-2147412105)]
            set;
        }

        /// <summary><para><c>onkeyup</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onkeyup</c> property was the following:  <c>VARIANT onkeyup</c>;</para></remarks>
        // IDL: VARIANT onkeyup;
        // VB6: onkeyup As Any
        object onkeyup
        {
            // IDL: HRESULT onkeyup ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onkeyup As Any
            [DispId(-2147412106)]
            get;
            // IDL: HRESULT onkeyup (VARIANT value);
            // VB6: Sub onkeyup (ByVal value As Any)
            [DispId(-2147412106)]
            set;
        }

        /// <summary><para><c>onmousedown</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onmousedown</c> property was the following:  <c>VARIANT onmousedown</c>;</para></remarks>
        // IDL: VARIANT onmousedown;
        // VB6: onmousedown As Any
        object onmousedown
        {
            // IDL: HRESULT onmousedown ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onmousedown As Any
            [DispId(-2147412110)]
            get;
            // IDL: HRESULT onmousedown (VARIANT value);
            // VB6: Sub onmousedown (ByVal value As Any)
            [DispId(-2147412110)]
            set;
        }

        /// <summary><para><c>onmousemove</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onmousemove</c> property was the following:  <c>VARIANT onmousemove</c>;</para></remarks>
        // IDL: VARIANT onmousemove;
        // VB6: onmousemove As Any
        object onmousemove
        {
            // IDL: HRESULT onmousemove ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onmousemove As Any
            [DispId(-2147412108)]
            get;
            // IDL: HRESULT onmousemove (VARIANT value);
            // VB6: Sub onmousemove (ByVal value As Any)
            [DispId(-2147412108)]
            set;
        }

        /// <summary><para><c>onmouseout</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onmouseout</c> property was the following:  <c>VARIANT onmouseout</c>;</para></remarks>
        // IDL: VARIANT onmouseout;
        // VB6: onmouseout As Any
        object onmouseout
        {
            // IDL: HRESULT onmouseout ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onmouseout As Any
            [DispId(-2147412111)]
            get;
            // IDL: HRESULT onmouseout (VARIANT value);
            // VB6: Sub onmouseout (ByVal value As Any)
            [DispId(-2147412111)]
            set;
        }

        /// <summary><para><c>onmouseover</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onmouseover</c> property was the following:  <c>VARIANT onmouseover</c>;</para></remarks>
        // IDL: VARIANT onmouseover;
        // VB6: onmouseover As Any
        object onmouseover
        {
            // IDL: HRESULT onmouseover ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onmouseover As Any
            [DispId(-2147412112)]
            get;
            // IDL: HRESULT onmouseover (VARIANT value);
            // VB6: Sub onmouseover (ByVal value As Any)
            [DispId(-2147412112)]
            set;
        }

        /// <summary><para><c>onmouseup</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onmouseup</c> property was the following:  <c>VARIANT onmouseup</c>;</para></remarks>
        // IDL: VARIANT onmouseup;
        // VB6: onmouseup As Any
        object onmouseup
        {
            // IDL: HRESULT onmouseup ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onmouseup As Any
            [DispId(-2147412109)]
            get;
            // IDL: HRESULT onmouseup (VARIANT value);
            // VB6: Sub onmouseup (ByVal value As Any)
            [DispId(-2147412109)]
            set;
        }

        /// <summary><para><c>onrowenter</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onrowenter</c> property was the following:  <c>VARIANT onrowenter</c>;</para></remarks>
        // IDL: VARIANT onrowenter;
        // VB6: onrowenter As Any
        object onrowenter
        {
            // IDL: HRESULT onrowenter ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onrowenter As Any
            [DispId(-2147412093)]
            get;
            // IDL: HRESULT onrowenter (VARIANT value);
            // VB6: Sub onrowenter (ByVal value As Any)
            [DispId(-2147412093)]
            set;
        }

        /// <summary><para><c>onrowexit</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onrowexit</c> property was the following:  <c>VARIANT onrowexit</c>;</para></remarks>
        // IDL: VARIANT onrowexit;
        // VB6: onrowexit As Any
        object onrowexit
        {
            // IDL: HRESULT onrowexit ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onrowexit As Any
            [DispId(-2147412094)]
            get;
            // IDL: HRESULT onrowexit (VARIANT value);
            // VB6: Sub onrowexit (ByVal value As Any)
            [DispId(-2147412094)]
            set;
        }

        /// <summary><para><c>onselectstart</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>onselectstart</c> property was the following:  <c>VARIANT onselectstart</c>;</para></remarks>
        // IDL: VARIANT onselectstart;
        // VB6: onselectstart As Any
        object onselectstart
        {
            // IDL: HRESULT onselectstart ([out, retval] VARIANT* ReturnValue);
            // VB6: Function onselectstart As Any
            [DispId(-2147412075)]
            get;
            // IDL: HRESULT onselectstart (VARIANT value);
            // VB6: Sub onselectstart (ByVal value As Any)
            [DispId(-2147412075)]
            set;
        }

        /// <summary><para><c>outerHTML</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>outerHTML</c> property was the following:  <c>BSTR outerHTML</c>;</para></remarks>
        // IDL: BSTR outerHTML;
        // VB6: outerHTML As String
        string outerHTML
        {
            // IDL: HRESULT outerHTML ([out, retval] BSTR* ReturnValue);
            // VB6: Function outerHTML As String
            [DispId(-2147417084)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT outerHTML (BSTR value);
            // VB6: Sub outerHTML (ByVal value As String)
            [DispId(-2147417084)]
            set;
        }

        /// <summary><para><c>outerText</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>outerText</c> property was the following:  <c>BSTR outerText</c>;</para></remarks>
        // IDL: BSTR outerText;
        // VB6: outerText As String
        string outerText
        {
            // IDL: HRESULT outerText ([out, retval] BSTR* ReturnValue);
            // VB6: Function outerText As String
            [DispId(-2147417083)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT outerText (BSTR value);
            // VB6: Sub outerText (ByVal value As String)
            [DispId(-2147417083)]
            set;
        }

        /// <summary><para><c>parentElement</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>parentElement</c> property was the following:  <c>IHTMLElement* parentElement</c>;</para></remarks>
        // IDL: IHTMLElement* parentElement;
        // VB6: parentElement As IHTMLElement
        IHTMLElement parentElement
        {
            // IDL: HRESULT parentElement ([out, retval] IHTMLElement** ReturnValue);
            // VB6: Function parentElement As IHTMLElement
            [DispId(-2147418104)]
            get;
        }

        /// <summary><para><c>parentTextEdit</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>parentTextEdit</c> property was the following:  <c>IHTMLElement* parentTextEdit</c>;</para></remarks>
        // IDL: IHTMLElement* parentTextEdit;
        // VB6: parentTextEdit As IHTMLElement
        IHTMLElement parentTextEdit
        {
            // IDL: HRESULT parentTextEdit ([out, retval] IHTMLElement** ReturnValue);
            // VB6: Function parentTextEdit As IHTMLElement
            [DispId(-2147417080)]
            get;
        }

        /// <summary><para><c>recordNumber</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>recordNumber</c> property was the following:  <c>VARIANT recordNumber</c>;</para></remarks>
        // IDL: VARIANT recordNumber;
        // VB6: recordNumber As Any
        object recordNumber
        {
            // IDL: HRESULT recordNumber ([out, retval] VARIANT* ReturnValue);
            // VB6: Function recordNumber As Any
            [DispId(-2147417087)]
            get;
        }

        /// <summary><para><c>sourceIndex</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>sourceIndex</c> property was the following:  <c>long sourceIndex</c>;</para></remarks>
        // IDL: long sourceIndex;
        // VB6: sourceIndex As Long
        int sourceIndex
        {
            // IDL: HRESULT sourceIndex ([out, retval] long* ReturnValue);
            // VB6: Function sourceIndex As Long
            [DispId(-2147417088)]
            get;
        }

        /// <summary><para><c>style</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>style</c> property was the following:  <c>IHTMLStyle* style</c>;</para></remarks>
        // IDL: IHTMLStyle* style;
        // VB6: style As IHTMLStyle
        IHTMLStyle style
        {
            // IDL: HRESULT style ([out, retval] IHTMLStyle** ReturnValue);
            // VB6: Function style As IHTMLStyle
            [DispId(-2147418038)]
            get;
        }

        /// <summary><para><c>tagName</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>tagName</c> property was the following:  <c>BSTR tagName</c>;</para></remarks>
        // IDL: BSTR tagName;
        // VB6: tagName As String
        string tagName
        {
            // IDL: HRESULT tagName ([out, retval] BSTR* ReturnValue);
            // VB6: Function tagName As String
            [DispId(-2147417108)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
        }

        /// <summary><para><c>title</c> property of <c>IHTMLElement</c> interface.</para></summary>
        /// <remarks><para>An original IDL definition of <c>title</c> property was the following:  <c>BSTR title</c>;</para></remarks>
        // IDL: BSTR title;
        // VB6: title As String
        string title
        {
            // IDL: HRESULT title ([out, retval] BSTR* ReturnValue);
            // VB6: Function title As String
            [DispId(-2147418043)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
            // IDL: HRESULT title (BSTR value);
            // VB6: Sub title (ByVal value As String)
            [DispId(-2147418043)]
            set;
        }
    }
    #endregion

    public class cHTMLElementEvents : HTMLElementEvents2
    {
        public bool m_IsCOnnected = false;
        private IConnectionPoint m_WBConnectionPoint = null;
        private IHTMLEventCallBack m_EventCallBack = null;
        public Guid m_guid = Guid.Empty;
        private int m_dwCookie = 0;
        public int[] m_dispIds = null;

        ~cHTMLElementEvents()
        {
            m_EventCallBack = null;
            m_dispIds = null;
        }

        public void InitHTMLEvents(IHTMLEventCallBack EventCallBack, int[] EventsDispIds, Guid guid)
        {
            m_EventCallBack = EventCallBack;
            m_dispIds = EventsDispIds;
            m_guid = guid;
        }

        //elem = IHTMLElement
        public bool ConnectToHtmlEvents(object elem)
        {
            if (elem == null)
                throw new ApplicationException("ConnectToHtmlEvents, elem can not be null!");
            //Get connectionpointcontainer
            IConnectionPointContainer cpCont = (IConnectionPointContainer)elem;
            //Find connection point
            if (cpCont != null)
            {
                cpCont.FindConnectionPoint(ref m_guid, out m_WBConnectionPoint);
                //Advice
                if (m_WBConnectionPoint != null)
                {
                    m_WBConnectionPoint.Advise(this, out m_dwCookie);
                    m_IsCOnnected = true;
                }
            }
            return m_IsCOnnected;
        }

        public bool DisconnectHtmlEvents()
        {
            m_IsCOnnected = false;
            bool bRet = false;
            //Do we have a connection point
            if (m_WBConnectionPoint != null)
            {
                if (m_dwCookie > 0)
                {
                    m_WBConnectionPoint.Unadvise(m_dwCookie);
                    m_dwCookie = 0;
                    bRet = true;
                }
                Marshal.ReleaseComObject(m_WBConnectionPoint);
            }
            return bRet;
        }

        private bool Invoke_Handler(int id, IHTMLEventObj pEvt)
        {
            foreach (int i in m_dispIds)
            {
                if (i == id)
                {
                    if (m_EventCallBack != null)
                        return m_EventCallBack.HandleHTMLEvent(HTMLEventType.HTMLElementEvent, (HTMLEventDispIds)i, pEvt);
                }
            }
            return true; //Allow, default
        }

        #region HTMLElementEvents2 Members

        bool HTMLElementEvents2.onclick(IHTMLEventObj pEvtObj)
        {
            return Invoke_Handler(HTMLDispIDs.DISPID_HTMLELEMENTEVENTS2_ONCLICK, pEvtObj);
        }

        bool HTMLElementEvents2.oncontextmenu(IHTMLEventObj pEvtObj)
        {
            return Invoke_Handler(HTMLDispIDs.DISPID_HTMLELEMENTEVENTS2_ONCONTEXTMENU, pEvtObj);
        }

        bool HTMLElementEvents2.onmousedown(IHTMLEventObj pEvtObj)
        {
            string srcEleName = pEvtObj.SrcElement.tagName;
            return Invoke_Handler(HTMLDispIDs.DISPID_HTMLELEMENTEVENTS2_ONMOUSEDOWN, pEvtObj);
        }

        bool HTMLElementEvents2.onmouseup(IHTMLEventObj pEvtObj)
        {
            string srcEleName = pEvtObj.SrcElement.tagName;
            return Invoke_Handler(HTMLDispIDs.DISPID_HTMLELEMENTEVENTS2_ONMOUSEUP, pEvtObj);
        }
        #endregion
    }

    #region HTMLElementEvents2 Interface

    [ComVisible(true), ComImport()]
    [TypeLibType((short)4160)]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("3050f60f-98b5-11cf-bb82-00aa00bdce0b")]
    public interface HTMLElementEvents2
    {
        [DispId(HTMLDispIDs.DISPID_HTMLELEMENTEVENTS2_ONCLICK)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        bool onclick([In, MarshalAs(UnmanagedType.Interface)] IHTMLEventObj pEvtObj);

        [DispId(HTMLDispIDs.DISPID_HTMLELEMENTEVENTS2_ONCONTEXTMENU)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        bool oncontextmenu([In, MarshalAs(UnmanagedType.Interface)] IHTMLEventObj pEvtObj);

        [DispId(HTMLDispIDs.DISPID_HTMLELEMENTEVENTS2_ONMOUSEDOWN)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        bool onmousedown([In, MarshalAs(UnmanagedType.Interface)] IHTMLEventObj pEvtObj);

        [DispId(HTMLDispIDs.DISPID_HTMLELEMENTEVENTS2_ONMOUSEUP)]
        [return: MarshalAs(UnmanagedType.VariantBool)]
        bool onmouseup([In, MarshalAs(UnmanagedType.Interface)] IHTMLEventObj pEvtObj);
    }

    #endregion

    #region IHTMLEventCallBack Interface
    /// <summary>
    /// Simple interface used as a callback implemented by the main control
    /// GUID generated using PSDK GUID generator
    /// </summary>
    [ComVisible(true), Guid("9995A2E0-CD26-4f3a-87FD-0E2B9B1F4648")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLEventCallBack
    {
        bool HandleHTMLEvent([In] HTMLEventType EventType, [In] HTMLEventDispIds EventDispId, [In, MarshalAs(UnmanagedType.Interface)] IHTMLEventObj pEvtObj);
    }
    #endregion

    [ComVisible(true), ComImport()]
    [TypeLibType((short)4160)] //TypeLibTypeFlags.FDispatchable
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("3050f32d-98b5-11cf-bb82-00aa00bdce0b")]
    public interface IHTMLEventObj
    {
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_SRCELEMENT)]
        IHTMLElement SrcElement { [return: MarshalAs(UnmanagedType.Interface)] get;}

        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_ALTKEY)]
        bool AltKey { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_CTRLKEY)]
        bool CtrlKey { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_SHIFTKEY)]
        bool ShiftKey { get;}
        //VARIANT
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_RETURNVALUE)]
        Object ReturnValue { set; get;}

        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_CANCELBUBBLE)]
        bool CancelBubble { set; [return: MarshalAs(UnmanagedType.VariantBool)] get;}

        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_FROMELEMENT)]
        IHTMLElement FromElement { [return: MarshalAs(UnmanagedType.Interface)] get;}

        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_TOELEMENT)]
        IHTMLElement ToElement { [return: MarshalAs(UnmanagedType.Interface)] get;}

        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_KEYCODE)]
        int keyCode { set; get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_BUTTON)]
        int Button { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_TYPE)]
        string EventType { [return: MarshalAs(UnmanagedType.BStr)] get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_QUALIFIER)]
        string Qualifier { [return: MarshalAs(UnmanagedType.BStr)] get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_REASON)]
        int Reason { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_X)]
        int X { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_Y)]
        int Y { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_CLIENTX)]
        int ClientX { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_CLIENTY)]
        int ClientY { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_OFFSETX)]
        int OffsetX { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_OFFSETY)]
        int OffsetY { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_SCREENX)]
        int ScreenX { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_SCREENY)]
        int ScreenY { get;}
        [DispId(HTMLDispIDs.DISPID_IHTMLEVENTOBJ_SRCFILTER)]
        object SrcFilter { [return: MarshalAs(UnmanagedType.IDispatch)] get;}
    }

    #region IWebBrowser2 Interface

    [ComImport,
    Guid("D30C1661-CDAF-11D0-8A3E-00C04FC9E26E"),
    InterfaceType(ComInterfaceType.InterfaceIsIDispatch),
    SuppressUnmanagedCodeSecurity]
    public interface IWebBrowser2
    {
        [DispId(100)]
        void GoBack();
        [DispId(0x65)]
        void GoForward();
        [DispId(0x66)]
        void GoHome();
        [DispId(0x67)]
        void GoSearch();
        [DispId(0x68)]
        void Navigate([MarshalAs(UnmanagedType.BStr)] string URL, [In] ref object Flags, [In] ref object TargetFrameName, [In] ref object PostData, [In] ref object Headers);
        [DispId(-550)]
        void Refresh();
        [DispId(0x69)]
        void Refresh2([In] ref object Level);
        [DispId(0x6a)]
        void Stop();
        [DispId(300)]
        void Quit();
        [DispId(0x12d)]
        void ClientToWindow([In, Out] ref int pcx, [In, Out] ref int pcy);
        [DispId(0x12e)]
        void PutProperty([MarshalAs(UnmanagedType.BStr)] string Property, object vtValue);
        [DispId(0x12f)]
        object GetProperty([MarshalAs(UnmanagedType.BStr)] string Property);
        [DispId(500)]
        void Navigate2([In] ref object URL, [In] ref object Flags, [In] ref object TargetFrameName, [In] ref object PostData, [In] ref object Headers);
        [DispId(0x1f5)]
        OLECMDF QueryStatusWB(OLECMDID cmdID);
        [DispId(0x1f6)]
        void ExecWB(OLECMDID cmdID, OLECMDEXECOPT cmdexecopt, [In] ref object pvaIn, [In, Out] ref object pvaOut);
        [DispId(0x1f7)]
        void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow, [In] ref object pvarSize);
        bool AddressBar { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0x22b)] get; [DispId(0x22b)] set; }
        object Application { [return: MarshalAs(UnmanagedType.IDispatch)] [DispId(200)] get; }
        bool Busy { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0xd4)] get; }
        object Container { [return: MarshalAs(UnmanagedType.IDispatch)] [DispId(0xca)] get; }
        object Document { [return: MarshalAs(UnmanagedType.IDispatch)] [DispId(0xcb)] get; }
        string FullName { [return: MarshalAs(UnmanagedType.BStr)] [DispId(400)] get; }
        bool FullScreen { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0x197)] get; [DispId(0x197)] set; }
        int Height { [DispId(0xd1)] get; [DispId(0xd1)] set; }
        int HWND { [DispId(-515)] get; }
        int Left { [DispId(0xce)] get; [DispId(0xce)] set; }
        string LocationName { [return: MarshalAs(UnmanagedType.BStr)] [DispId(210)] get; }
        string LocationURL { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0xd3)] get; }
        bool MenuBar { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0x196)] get; [DispId(0x196)] set; }
        string Name { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0)] get; }
        bool Offline { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(550)] get; [DispId(550)] set; }
        object Parent { [return: MarshalAs(UnmanagedType.IDispatch)] [DispId(0xc9)] get; }
        string Path { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x191)] get; }
        tagREADYSTATE ReadyState { [DispId(-525)] get; }
        bool RegisterAsBrowser { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0x228)] get; [DispId(0x228)] set; }
        bool RegisterAsDropTarget { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0x229)] get; [DispId(0x229)] set; }
        bool Resizable { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0x22c)] get; [DispId(0x22c)] set; }
        bool Silent { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0x227)] get; [DispId(0x227)] set; }
        bool StatusBar { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0x193)] get; [DispId(0x193)] set; }
        string StatusText { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0x194)] get; [DispId(0x194)] set; }
        bool TheaterMode { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0x22a)] get; [DispId(0x22a)] set; }
        int ToolBar { [DispId(0x195)] get; [DispId(0x195)] set; }
        int Top { [DispId(0xcf)] get; [DispId(0xcf)] set; }
        bool TopLevelContainer { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0xcc)] get; }
        string Type { [return: MarshalAs(UnmanagedType.BStr)] [DispId(0xcd)] get; }
        bool Visible { [return: MarshalAs(UnmanagedType.VariantBool)] [DispId(0x192)] get; [DispId(0x192)] set; }
        int Width { [DispId(0xd0)] get; [DispId(0xd0)] set; }
    }

    #endregion

    #region HTMLEditHelper class
    /// <summary>
    /// 
    /// </summary>
    public class HTMLEditHelper
    {
        private mshtml.IHTMLDocument2 m_pDoc2 = null;

        public enum DocumentColors
        {
            Backcolor = 0,
            Forecolor = 1,
            Linkcolor = 2,
            ALinkcolor = 3,
            VLinkcolor = 4
        }

        public string HtmlSpace = "&nbsp;";  // space
        public string HtmlTagOpen = "&lt;";  // <
        public string HtmlTagClose = "&gt;"; // >
        public string HtmlAmp = "&amp;";     // &

        public HTMLEditHelper()
        {

        }

        public mshtml.IHTMLDocument2 DOMDocument
        {
            get { return m_pDoc2; }
            set { m_pDoc2 = value; }
        }

        /// <summary>
        /// Searches for a parent (or grandparent) element with the given tag
        /// ParentTagName must be in the form "TD" "TR" "TABLE" (uppercase)
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="ParentTagName"></param>
        /// <returns></returns>
        public IHTMLElement FindParent(IHTMLElement elem, string ParentTagName)
        {
            IHTMLElement retelem = elem.parentElement;
            while ((retelem != null) && (retelem.tagName != ParentTagName))
            {
                retelem = retelem.parentElement;
            }
            return retelem;
        }

        /// <summary>
        /// Returns the right neighbour which is a IHTMLElement in the HTML hierarchy
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IHTMLElement NextSibiling(mshtml.IHTMLDOMNode node)
        {
            mshtml.IHTMLDOMNode pnode = node;
            while (true)
            {
                mshtml.IHTMLDOMNode pnext = pnode.nextSibling;
                if (pnext == null) //No more
                    break;
                //See if this is an HTMLElement
                IHTMLElement elem = pnext as IHTMLElement;
                if (elem != null)
                    return elem;
                pnode = pnext;
            }
            return null;
        }

        /// <summary>
        /// Returns the left neighbour which is a IHTMLElement in the HTML hierarchy
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public IHTMLElement PreviousSibling(mshtml.IHTMLDOMNode node)
        {
            mshtml.IHTMLDOMNode pnode = node;
            while (true)
            {
                mshtml.IHTMLDOMNode pnext = pnode.previousSibling;
                if (pnext == null) //No more
                    break;
                //See if this is an HTMLElement
                IHTMLElement elem = pnext as IHTMLElement;
                if (elem != null)
                    return elem;
                pnode = pnext;
            }
            return null;
        }

        /// <summary>
        /// Removes node element
        /// If RemoveAllChildren == true, Removes this element and all it's children from the document
        /// else it just strips this element but does not remove its children
        /// E.g.  "<BIG><b>Hello World</b></BIG>"  ---> strip BIG tag --> "<b>Hello World</b>"
        /// </summary>
        /// <param name="node">element to remove</param>
        /// <param name="RemoveAllChildren"></param>
        public mshtml.IHTMLDOMNode RemoveNode(IHTMLElement elem, bool RemoveAllChildren)
        {
            mshtml.IHTMLDOMNode node = elem as mshtml.IHTMLDOMNode;
            if (node != null)
                return node.removeNode(RemoveAllChildren);
            else
                return null;
        }

        /// <summary>
        /// Return TRUE if the element is empty inside (e.g. <a name="#Pos1"></a>)
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public bool IsElementEmpty(IHTMLElement elem)
        {
            string str = elem.innerHTML;
            char[] c = { '\r', '\n', '\t' };
            if (string.IsNullOrEmpty(str))
                return true;
            str.Trim(c);
            return (str.Length > 0);
        }

        /// <summary>
        /// 1) If nothing is selected returns false
        /// 2) If the user has selected text or multiple elements
        /// inserts s_BeginHtml before and s_EndHtml behind the current selection
        /// 3)If the user has selected a control returns false
        /// </summary>
        /// <param name="s_BeginHtml"></param>
        /// <param name="s_EndHtml"></param>
        /// <returns></returns>
        // Example s_BeginHtml = "<SUB>", s_EndHtml = "</SUB>" will subscript the selected text
        // Example s_BeginHtml = "", s_EndHtml = "<BR>" will add a BR to the end of the selected text
        //AddToSelection(string.Empty, "<br>");
        public bool AddToSelection(string s_BeginHtml, string s_EndHtml)
        {
            if (m_pDoc2 == null)
                return false;
            mshtml.IHTMLSelectionObject sel = m_pDoc2.selection as mshtml.IHTMLSelectionObject;
            if (sel == null)
                return false;
            mshtml.IHTMLTxtRange range = sel.createRange() as mshtml.IHTMLTxtRange;
            if (range == null)
                return false;
            string shtml = string.Empty;
            if (!string.IsNullOrEmpty(s_BeginHtml))
                shtml = s_BeginHtml + range.htmlText;
            if (!string.IsNullOrEmpty(s_EndHtml))
                shtml += s_EndHtml;
            range.pasteHTML(shtml);
            range.collapse(false);
            range.select();
            return true;
        }

        /*  /// <summary>
          /// The currently selected text/controls will be replaced by the given HTML code.
          /// If nothing is selected, the HTML code is inserted at the cursor position
          /// </summary>
          /// <param name="s_Html"></param>
          /// <returns></returns>
          public bool PasteIntoSelection(string s_Html)
          {
              if (m_pDoc2 == null)
                  return false;
              mshtml.IHTMLSelectionObject sel = m_pDoc2.selection as mshtml.IHTMLSelectionObject;
              if (sel == null)
                  return false;
              mshtml.IHTMLTxtRange range = sel.createRange() as mshtml.IHTMLTxtRange;
              if (range == null)
                  return false;
              //none
              //text
              //control
              if ((!String.IsNullOrEmpty(sel.EventType)) &&
                  (sel.EventType == "control"))
              {
                  mshtml.IHTMLControlRange ctlrange = range as mshtml.IHTMLControlRange;
                  if (ctlrange != null) //Control(s) selected
                  {
                      IHTMLElement elem = null;
                      int ctls = ctlrange.length;
                      for (int i = 0; i < ctls; i++)
                      {
                          //Remove all selected controls
                          elem = ctlrange.item(i);
                          if (elem != null)
                          {
                              RemoveNode(elem, true);
                          }
                      }
                      // Now get the textrange after deleting all selected controls
                      range = null;
                      range = sel.createRange() as mshtml.IHTMLTxtRange;
                  }
              }

              if (range != null)
              {
                  // range will be valid if nothing is selected or if text is selected
                  // or if multiple elements are seleted
                  range.pasteHTML(s_Html);
                  range.collapse(false);
                  range.select();
              }
              return true;
          }*/

        /// <summary>
        /// Inserts the given HTML code inside or outside of this Html element
        /// There are 4 possible insert positions:
        /// Outside-Before<TAG>Inside-Before InnerHTML Inside-After</TAG>Ouside-After
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="s_Html"></param>
        /// <param name="b_AtBegin"></param>
        /// <param name="b_Inside"></param>
        public void InsertHTML(IHTMLElement elem, string s_Html, bool b_AtBegin, bool b_Inside)
        {
            if (elem == null)
                return;
            string bs_Where;
            if (b_Inside)
            {
                if (b_AtBegin) bs_Where = "afterBegin";
                else bs_Where = "beforeEnd";
            }
            else // Outside
            {
                if (b_AtBegin) bs_Where = "beforeBegin";
                else bs_Where = "afterEnd";
            }
            elem.insertAdjacentHTML(bs_Where, s_Html);
        }

        /// <summary>
        /// Creates and appends an HTMLElement to the end of the document DOM
        /// </summary>
        /// <param name="TagName">a, img, table,...</param>
        public mshtml.IHTMLDOMNode AppendChild(string TagName)
        {
            if (m_pDoc2 == null)
                return null;
            IHTMLElement elem = m_pDoc2.createElement(TagName) as IHTMLElement;
            if (elem == null)
                return null;
            //Append to body DOM collection
            mshtml.IHTMLDOMNode nd = (mshtml.IHTMLDOMNode)elem;
            mshtml.IHTMLDOMNode body = (mshtml.IHTMLDOMNode)m_pDoc2.body;
            return body.appendChild(nd);
        }

        /// <summary>
        /// Creates and appends an HTMLElement to a parent element
        /// </summary>
        /// <param name="TagName">a, img, table,...</param>
        public mshtml.IHTMLDOMNode AppendChild(IHTMLElement parent, string TagName)
        {
            if (m_pDoc2 == null)
                return null;
            IHTMLElement elem = m_pDoc2.createElement(TagName) as IHTMLElement;
            if (elem == null)
                return null;
            //Append to body DOM collection
            mshtml.IHTMLDOMNode nd = (mshtml.IHTMLDOMNode)elem;
            mshtml.IHTMLDOMNode body = (mshtml.IHTMLDOMNode)parent;
            return body.appendChild(nd);
        }

        public System.Drawing.Color GetDocumentColor(DocumentColors whichcolor)
        {
            System.Drawing.Color cret = System.Drawing.Color.Empty;
            if (m_pDoc2 != null)
            {
                if ((whichcolor == DocumentColors.Backcolor) && (m_pDoc2.bgColor != null))
                    cret = System.Drawing.ColorTranslator.FromHtml(m_pDoc2.bgColor.ToString());
                else if ((whichcolor == DocumentColors.Forecolor) && (m_pDoc2.fgColor != null))
                    cret = System.Drawing.ColorTranslator.FromHtml(m_pDoc2.fgColor.ToString());
                else if ((whichcolor == DocumentColors.Linkcolor) && (m_pDoc2.linkColor != null))
                    cret = System.Drawing.ColorTranslator.FromHtml(m_pDoc2.linkColor.ToString());
                else if ((whichcolor == DocumentColors.ALinkcolor) && (m_pDoc2.alinkColor != null))
                    cret = System.Drawing.ColorTranslator.FromHtml(m_pDoc2.alinkColor.ToString());
                else if ((whichcolor == DocumentColors.VLinkcolor) && (m_pDoc2.vlinkColor != null))
                    cret = System.Drawing.ColorTranslator.FromHtml(m_pDoc2.vlinkColor.ToString());
            }
            return cret;
        }

        public bool EmbedBr()
        {
            return AddToSelection(string.Empty, "<br>");
        }

        //<img border="2" src="file:///C:/csEXWB/csEXWB.gif" align="center" hspace="2" vspace="2" alt="hello there" lowsrc="file:///C:/Desktop/blank.gif" width="600" height="463">
        public bool AppendImage(string src, string width, string height, string border, string alignment, string alt, string hspace, string vspace, string lowsrc)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLElement elem = m_pDoc2.createElement("img") as IHTMLElement;

            if (elem == null)
                return false;

            mshtml.IHTMLImgElement imgelem = elem as mshtml.IHTMLImgElement;
            if (imgelem == null)
                return false;

            if (!string.IsNullOrEmpty(src))
                imgelem.src = src;
            if (!string.IsNullOrEmpty(width))
                imgelem.width = int.Parse(width);
            if (!string.IsNullOrEmpty(height))
                imgelem.height = int.Parse(height);
            if (!string.IsNullOrEmpty(border))
                imgelem.border = border;
            if (!string.IsNullOrEmpty(alignment))
                imgelem.align = alignment;
            if (!string.IsNullOrEmpty(hspace))
                imgelem.hspace = int.Parse(hspace);
            if (!string.IsNullOrEmpty(vspace))
                imgelem.vspace = int.Parse(vspace);
            if (!string.IsNullOrEmpty(alt))
                imgelem.alt = alt;
            if (!string.IsNullOrEmpty(lowsrc))
                imgelem.lowsrc = lowsrc;

            //Append to body DOM collection
            mshtml.IHTMLDOMNode nd = (mshtml.IHTMLDOMNode)elem;
            mshtml.IHTMLDOMNode body = (mshtml.IHTMLDOMNode)m_pDoc2.body;
            return (body.appendChild(nd) != null);
        }

        //<a href="http://www.google.com" target="_blank">google</a>
        //Uses selection text
        public bool AppendAnchor(string href, string target)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLElement elem = m_pDoc2.createElement("a") as IHTMLElement;

            if (elem == null)
                return false;

            mshtml.IHTMLAnchorElement aelem = elem as mshtml.IHTMLAnchorElement;
            if (aelem == null)
                return false;

            if (!string.IsNullOrEmpty(href))
                aelem.href = href;
            if (!string.IsNullOrEmpty(target))
                aelem.target = target;

            //Append to body DOM collection
            mshtml.IHTMLDOMNode nd = (mshtml.IHTMLDOMNode)elem;
            mshtml.IHTMLDOMNode body = (mshtml.IHTMLDOMNode)m_pDoc2.body;
            return (body.appendChild(nd) != null);
        }

        //editor.AppendHr("center", string.Empty, "300", false);
        //left, center, right, justify
        public bool AppendHr(string align, string hrcolor, string width, bool noshade)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLElement elem = m_pDoc2.createElement("hr") as IHTMLElement;

            if (elem == null)
                return false;

            mshtml.IHTMLHRElement hrelem = elem as mshtml.IHTMLHRElement;
            if (hrelem == null)
                return false;

            if (!string.IsNullOrEmpty(align))
                hrelem.align = align;
            if (!string.IsNullOrEmpty(hrcolor))
                hrelem.color = hrcolor;
            if (!string.IsNullOrEmpty(width))
                hrelem.width = width;
            hrelem.noShade = noshade;

            //Append to body DOM collection
            IHTMLDOMNode nd = (IHTMLDOMNode)elem;
            IHTMLDOMNode body = (IHTMLDOMNode)m_pDoc2.body;
            return (body.appendChild(nd) != null);
        }

        #region Table specific

        //bordersize 2 or "2"
        public bool AppendTable(int colnum, int rownum, int bordersize, string alignment, int cellpadding, int cellspacing, string widthpercentage, int widthpixel, string backcolor, string bordercolor, string lightbordercolor, string darkbordercolor)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLTable t = (IHTMLTable)m_pDoc2.createElement("table");
            //set the cols
            t.cols = colnum;
            t.border = bordersize;

            if (!string.IsNullOrEmpty(alignment))
                t.align = alignment; //"center"
            t.cellPadding = cellpadding; //1
            t.cellSpacing = cellspacing; //2

            if (!string.IsNullOrEmpty(widthpercentage))
                t.width = widthpercentage; //"50%"; 
            else if (widthpixel > 0)
                t.width = widthpixel; //80;

            if (!string.IsNullOrEmpty(backcolor))
                t.bgColor = backcolor;

            if (!string.IsNullOrEmpty(bordercolor))
                t.borderColor = bordercolor;

            if (!string.IsNullOrEmpty(lightbordercolor))
                t.borderColorLight = lightbordercolor;

            if (!string.IsNullOrEmpty(darkbordercolor))
                t.borderColorDark = darkbordercolor;

            //Insert rows and fill them with space
            int cells = colnum - 1;
            int rows = rownum - 1;

            CalculateCellWidths(colnum);
            for (int i = 0; i <= rows; i++)
            {
                IHTMLTableRow tr = (IHTMLTableRow)t.insertRow(-1);
                for (int j = 0; j <= cells; j++)
                {
                    IHTMLElement c = tr.insertCell(-1) as IHTMLElement;
                    if (c != null)
                    {
                        c.innerHTML = HtmlSpace;
                        IHTMLTableCell tcell = c as IHTMLTableCell;
                        if (tcell != null)
                        {
                            //set width so as user enters text
                            //the cell width would not adjust
                            if (j == cells) //last cell
                                tcell.width = m_lastcellwidth;
                            else
                                tcell.width = m_cellwidth;
                        }
                    }
                }
            }

            //Append to body DOM collection
            IHTMLDOMNode nd = (IHTMLDOMNode)t;
            IHTMLDOMNode body = (IHTMLDOMNode)m_pDoc2.body;
            return (body.appendChild(nd) != null);

        }

        /// <summary>
        /// Returns parent row of passed cell element
        /// </summary>
        /// <param name="cellelem"></param>
        /// <returns></returns>
        public mshtml.IHTMLTableRow GetParentRow(IHTMLElement cellelem)
        {
            IHTMLElement elem = FindParent(cellelem, "TR");
            if (elem != null)
                return elem as IHTMLTableRow;
            else
                return null;
        }

        /// <summary>
        /// Returns parent table of passed cell element
        /// </summary>
        /// <param name="cellelem"></param>
        /// <returns></returns>
        public IHTMLTable GetParentTable(IHTMLElement cellelem)
        {
            IHTMLElement elem = FindParent(cellelem, "TABLE");
            if (elem != null)
                return elem as IHTMLTable;
            else
                return null;
        }

        /// <summary>
        /// Get the column of the current cell
        /// zero based
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public int GetColIndex(mshtml.IHTMLTableCell cell)
        {
            if (cell == null)
                return 0;
            int iret = 0;
            IHTMLTableCell tcell = cell;
            IHTMLDOMNode node = null;
            IHTMLElement elem = null;
            while (true)
            {
                node = tcell as IHTMLDOMNode;
                if (node == null)
                    break;
                elem = PreviousSibling(node);
                if (elem == null)
                    break;
                tcell = elem as IHTMLTableCell;
                if (tcell == null)
                    break;
                iret += tcell.colSpan;
            }
            return iret;
        }

        // zero based
        public int GetRowIndex(IHTMLElement cellelem)
        {
            IHTMLTableRow row = GetParentRow(cellelem);
            if (row != null)
                return row.rowIndex;
            return 0;
        }

        /// <summary>
        /// Zero based
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rowindex"></param>
        /// <returns></returns>
        public mshtml.IHTMLTableRow GetRow(IHTMLTable table, int rowindex)
        {
            mshtml.IHTMLElementCollection rows = table.rows;
            if (rows == null)
                return null;
            object obj = rowindex;
            return rows.item(obj, obj) as mshtml.IHTMLTableRow;
        }

        public int GetRowCount(IHTMLTable table)
        {
            mshtml.IHTMLElementCollection rows = table.rows;
            if (rows != null)
                return rows.length;
            return 0;
        }

        /// <summary>
        /// Gets the column count of row(rowindex)
        /// Accounts for colSpan property
        /// </summary>
        /// <param name="table"></param>
        /// <param name="colindex"></param>
        /// <returns></returns>
        public int GetColCount(IHTMLTable table, int rowindex)
        {
            mshtml.IHTMLTableRow row = GetRow(table, rowindex);
            if (row == null)
                return 0;
            int counter = 0;
            int cols = 0;
            while (true)
            {
                mshtml.IHTMLTableCell cell = Row_GetCell(row, counter);
                if (cell == null)
                    break;
                cols += cell.colSpan;
                counter++;
            }
            
            return cols;
        }

        /// <summary>
        /// Deletes a given rowindex in a given table
        /// zero based
        /// If the table has no rows after deletion anymore
        /// we delete it compeletely
        /// </summary>
        /// <param name="table"></param>
        /// <param name="rowindex"></param>
        public void DeleteRow(IHTMLTable table, int rowindex)
        {
            table.deleteRow(rowindex);
            if (GetRowCount(table) == 0)
                RemoveNode(table as IHTMLElement, true);
        }

        public void DeleteCol(IHTMLTable table, int colindex)
        {
            for (int i = 0; true; i++)
            {
                IHTMLTableRow row = GetRow(table, i);
                if (row == null)
                    break;

                IHTMLTableCell cell = Row_GetCell(row, colindex);
                if (cell == null)
                    continue;
                RemoveNode(cell as IHTMLElement, true);
                if (Row_GetCellCount(row) == 0)
                {
                    RemoveNode(table as IHTMLElement, true);
                    break;
                }

                //Accounts for colspan
                //Row_DeleteCol(row, colindex);

                IHTMLElementCollection cells = row.cells;
                CalculateCellWidths(cells.length);
                for (int j = 0; j < cells.length; j++)
                {
                    object obj = j;
                    IHTMLTableCell cella = cells.item(obj, obj) as IHTMLTableCell;
                    if (cella != null)
                    {
                        if ((j + 1) == cells.length)
                            cella.width = m_lastcellwidth;
                        else
                            cella.width = m_cellwidth;
                    }
                }
            }
        }

        public int Row_GetCellCount(mshtml.IHTMLTableRow row)
        {
            if (row == null)
                return 0;
            IHTMLElementCollection cells = row.cells;
            if (cells != null)
                return cells.length;
            return 0;
        }

        public void Row_DeleteCol(mshtml.IHTMLTableRow row, int index)
        {
            int col = 0;
            int span = 0;
            IHTMLTableCell cell = Row_GetCell(row, 0);
            IHTMLElement elem = null;
            while (true)
            {
                if (cell == null)
                    return;
                span = cell.colSpan;
                //cell.cellIndex
                if (span == 1)
                {
                    if (col == index)
                    {
                        RemoveNode(cell as IHTMLElement, true);
                        if (Row_GetCellCount(row) == 0)
                        {
                            IHTMLTable table = GetParentTable(row as IHTMLElement) as IHTMLTable;
                            if (table != null)
                                RemoveNode(table as IHTMLElement, true);
                            break;
                        }
                    }
                }
                else if (span > 1)// cell spans about multiple columns
                {
                    if (index >= col && index < col + span)
                    {
                        cell.colSpan = span - 1; // reduce cellspan
                        break;
                    }
                }
                col += span;
                //Get next sibiling
                elem = NextSibiling(cell as IHTMLDOMNode);
                if (elem != null)
                    cell = elem as IHTMLTableCell;
                else
                    cell = null;
            }
        }

        public IHTMLElement Row_InsertCell(mshtml.IHTMLTableRow row, int index)
        {
            IHTMLElement elem = row.insertCell(index) as IHTMLElement;
            //if (elem != null)
            //    elem.innerHTML = HtmlSpace;
            return elem;
        }

        public IHTMLElement Row_InsertCell(mshtml.IHTMLTableRow row, int index, string cellwidth)
        {
            IHTMLElement elem = row.insertCell(index) as IHTMLElement;
            if (elem != null)
            {
                elem.innerHTML = HtmlSpace;
                if (!string.IsNullOrEmpty(cellwidth))
                {
                    mshtml.IHTMLTableCell tcell = elem as mshtml.IHTMLTableCell;
                    if (tcell != null)
                    {
                        tcell.width = cellwidth;
                    }
                }
            }
            return elem;
        }

        public IHTMLElement Row_InsertCol(mshtml.IHTMLTableRow row, int index)
        {
            int col = 0;
            int span = 0;
            object obj = 0;
            mshtml.IHTMLElementCollection cells = row.cells;
            IHTMLElement retelem = null;

            for (int i = 0; true; i++)
            {
                obj = i;
                mshtml.IHTMLTableCell cell = cells.item(obj, obj) as mshtml.IHTMLTableCell;
                if (cell == null) // insert behind the rightmost cell
                {
                    retelem = Row_InsertCell(row, i);
                    break;
                }
                span = cell.colSpan;
                if (span == col) // insert at the left of the specified cell
                {
                    retelem = Row_InsertCell(row, i);
                    break;
                }
                if ((index > col) && (index < (col + span)))
                {
                    cell.colSpan = span + 1; // increase cellspan of multi column cell
                    retelem = cell as IHTMLElement;
                    break;
                }
                col += span;
                retelem = null;
            }

            //Set width as evenly as possible
            CalculateCellWidths(cells.length);
            for (int i = 0; i < cells.length; i++)
            {
                obj = i;
                IHTMLTableCell cell = cells.item(obj, obj) as IHTMLTableCell;
                if (cell != null)
                {
                    if ((i + 1) == cells.length)
                        cell.width = m_lastcellwidth;
                    else
                        cell.width = m_cellwidth;
                }
            }
            return retelem;
        }

        public mshtml.IHTMLTableCell Row_GetCell(mshtml.IHTMLTableRow row, int index)
        {
            IHTMLElementCollection cells = row.cells;
            if (cells == null)
                return null;
            object obj = index;
            return cells.item(obj, obj) as IHTMLTableCell;
        }

        private string m_cellwidth = string.Empty;
        private string m_lastcellwidth = string.Empty;
        private void CalculateCellWidths(int numberofcols)
        {
            //Even numbers. for 2 cols; 50%, 50%
            //Odd numbers.  for 3 cols; 33%, 33%, 34%
            int cellwidth = (int)(100 / numberofcols);
            m_cellwidth = cellwidth.ToString() + "%";
            //modulus operator (%).
            //http://msdn2.microsoft.com/en-us/library/0w4e0fzs.aspx
            //http://msdn2.microsoft.com/en-us/library/6a71f45d.aspx
            cellwidth += 100 % numberofcols;
            m_lastcellwidth = cellwidth.ToString() + "%";
        }

        public void InsertRow(IHTMLTable table, int index, int numberofcells)
        {
            IHTMLTableRow row = table.insertRow(index) as IHTMLTableRow;
            if (row == null)
                return;

            CalculateCellWidths(numberofcells);
            for (int j = 0; j < numberofcells; j++)
            {
                if ((j + 1) == numberofcells)
                    Row_InsertCell(row, -1, m_lastcellwidth);
                else
                    Row_InsertCell(row, -1, m_cellwidth);
            }
        }

        public void InsertCol(IHTMLTable table, int index)
        {
            for (int j = 0; true; j++)
            {
                IHTMLTableRow row = GetRow(table, j);
                if (row == null)
                    return;
                if (Row_InsertCol(row, index) == null)
                    return;
            }
        }

        #endregion

    }
    #endregion

    #region IHTMLTable Interface
    [ComImport, ComVisible(true), Guid("3050f21e-98b5-11cf-bb82-00aa00bdce0b")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
    TypeLibType(TypeLibTypeFlags.FDispatchable)]
    public interface IHTMLTable
    {
        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_COLS)]
        int cols { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_BORDER)]
        object border { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_FRAME)]
        string frame { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_RULES)]
        string rules { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_CELLSPACING)]
        object cellSpacing { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_CELLPADDING)]
        object cellPadding { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_BACKGROUND)]
        string background { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_BGCOLOR)]
        object bgColor { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_BORDERCOLOR)]
        object borderColor { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_BORDERCOLORLIGHT)]
        object borderColorLight { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_BORDERCOLORDARK)]
        object borderColorDark { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_ALIGN)]
        string align { set; [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_REFRESH)]
        int refresh();

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_ROWS)]
        mshtml.IHTMLElementCollection rows { get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_WIDTH)]
        object width { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_HEIGHT)]
        object height { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_DATAPAGESIZE)]
        int dataPageSize { set; get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_NEXTPAGE)]
        int nextPage();

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_PREVIOUSPAGE)]
        int previousPage();

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_THEAD)]
        object tHead { [return: MarshalAs(UnmanagedType.Interface)] get; } //IHTMLTableSection

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_TFOOT)]
        object tFoot { [return: MarshalAs(UnmanagedType.Interface)] get; } //IHTMLTableSection

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_TBODIES)]
        mshtml.IHTMLElementCollection tBodies { [return: MarshalAs(UnmanagedType.Interface)] get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_CAPTION)]
        object caption { [return: MarshalAs(UnmanagedType.Interface)] get; } //IHTMLTableCaption

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_CREATETHEAD)]
        [return: MarshalAs(UnmanagedType.Interface)]
        object createTHead();

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_DELETETHEAD)]
        void deleteTHead();

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_CREATETFOOT)]
        [return: MarshalAs(UnmanagedType.Interface)]
        object createTFoot();

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_DELETETFOOT)]
        void deleteTFoot();

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_CREATECAPTION)]
        [return: MarshalAs(UnmanagedType.Interface)]
        object createCaption(); //IHTMLTableCaption

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_DELETECAPTION)]
        int deleteCaption();

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_INSERTROW)]
        [return: MarshalAs(UnmanagedType.Interface)]
        object insertRow(int index);

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_DELETEROW)]
        void deleteRow(int index);

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_READYSTATE)]
        string readyState { [return: MarshalAs(UnmanagedType.BStr)] get; }

        [DispId(HTMLDispIDs.DISPID_IHTMLTABLE_ONREADYSTATECHANGE)]
        object onreadystatechange { set; get; }

    }
    #endregion

    #region 
    [ComImport(), ComVisible(true),
Guid("B722BCCB-4E68-101B-A2BC-00AA00404770"),
InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleCommandTarget
    {

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int QueryStatus(
            [In] IntPtr pguidCmdGroup,
            [In, MarshalAs(UnmanagedType.U4)] uint cCmds,
            [In, Out, MarshalAs(UnmanagedType.Struct)] ref tagOLECMD prgCmds,
            //This parameter must be IntPtr, as it can be null
            [In, Out] IntPtr pCmdText);

        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        int Exec(
            //[In] ref Guid pguidCmdGroup,
            //have to be IntPtr, since null values are unacceptable
            //and null is used as default group!
            [In] IntPtr pguidCmdGroup,
            [In, MarshalAs(UnmanagedType.U4)] uint nCmdID,
            [In, MarshalAs(UnmanagedType.U4)] uint nCmdexecopt,
            [In] IntPtr pvaIn,
            [In, Out] IntPtr pvaOut);
    }

    //typedef struct _tagOLECMD
    //{
    //ULONG cmdID;
    //DWORD cmdf;
    //} 	OLECMD;
    [ComVisible(true), StructLayout(LayoutKind.Sequential)]
    public struct tagOLECMD
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint cmdID;
        [MarshalAs(UnmanagedType.U4)]
        public uint cmdf;
    }
    #endregion


    public enum MSHTML_COMMAND_IDS
    {
        IDM_UNKNOWN = 0,
        IDM_ALIGNBOTTOM = 1,
        IDM_ALIGNHORIZONTALCENTERS = 2,
        IDM_ALIGNLEFT = 3,
        IDM_ALIGNRIGHT = 4,
        IDM_ALIGNTOGRID = 5,
        IDM_ALIGNTOP = 6,
        IDM_ALIGNVERTICALCENTERS = 7,
        IDM_ARRANGEBOTTOM = 8,
        IDM_ARRANGERIGHT = 9,
        IDM_BRINGFORWARD = 10,
        IDM_BRINGTOFRONT = 11,
        IDM_CENTERHORIZONTALLY = 12,
        IDM_CENTERVERTICALLY = 13,
        IDM_CODE = 14,
        IDM_DELETE = 17,
        IDM_FONTNAME = 18,
        IDM_FONTSIZE = 19,
        IDM_GROUP = 20,
        IDM_HORIZSPACECONCATENATE = 21,
        IDM_HORIZSPACEDECREASE = 22,
        IDM_HORIZSPACEINCREASE = 23,
        IDM_HORIZSPACEMAKEEQUAL = 24,
        IDM_INSERTOBJECT = 25,
        IDM_MULTILEVELREDO = 30,
        IDM_SENDBACKWARD = 32,
        IDM_SENDTOBACK = 33,
        IDM_SHOWTABLE = 34,
        IDM_SIZETOCONTROL = 35,
        IDM_SIZETOCONTROLHEIGHT = 36,
        IDM_SIZETOCONTROLWIDTH = 37,
        IDM_SIZETOFIT = 38,
        IDM_SIZETOGRID = 39,
        IDM_SNAPTOGRID = 40,
        IDM_TABORDER = 41,
        IDM_TOOLBOX = 42,
        IDM_MULTILEVELUNDO = 44,
        IDM_UNGROUP = 45,
        IDM_VERTSPACECONCATENATE = 46,
        IDM_VERTSPACEDECREASE = 47,
        IDM_VERTSPACEINCREASE = 48,
        IDM_VERTSPACEMAKEEQUAL = 49,
        IDM_JUSTIFYFULL = 50,
        IDM_BACKCOLOR = 51,
        IDM_BOLD = 52,
        IDM_BORDERCOLOR = 53,
        IDM_FLAT = 54,
        IDM_FORECOLOR = 55,
        IDM_ITALIC = 56,
        IDM_JUSTIFYCENTER = 57,
        IDM_JUSTIFYGENERAL = 58,
        IDM_JUSTIFYLEFT = 59,
        IDM_JUSTIFYRIGHT = 60,
        IDM_RAISED = 61,
        IDM_SUNKEN = 62,
        IDM_UNDERLINE = 63,
        IDM_CHISELED = 64,
        IDM_ETCHED = 65,
        IDM_SHADOWED = 66,
        IDM_FIND = 67,
        IDM_SHOWGRID = 69,
        IDM_OBJECTVERBLIST0 = 72,
        IDM_OBJECTVERBLIST1 = 73,
        IDM_OBJECTVERBLIST2 = 74,
        IDM_OBJECTVERBLIST3 = 75,
        IDM_OBJECTVERBLIST4 = 76,
        IDM_OBJECTVERBLIST5 = 77,
        IDM_OBJECTVERBLIST6 = 78,
        IDM_OBJECTVERBLIST7 = 79,
        IDM_OBJECTVERBLIST8 = 80,
        IDM_OBJECTVERBLIST9 = 81,
        IDM_OBJECTVERBLISTLAST = IDM_OBJECTVERBLIST9,
        IDM_CONVERTOBJECT = 82,
        IDM_CUSTOMCONTROL = 83,
        IDM_CUSTOMIZEITEM = 84,
        IDM_RENAME = 85,
        IDM_IMPORT = 86,
        IDM_NEWPAGE = 87,
        IDM_MOVE = 88,
        IDM_CANCEL = 89,
        IDM_FONT = 90,
        IDM_STRIKETHROUGH = 91,
        IDM_DELETEWORD = 92,
        IDM_EXECPRINT = 93,
        IDM_JUSTIFYNONE = 94,
        IDM_TRISTATEBOLD = 95,
        IDM_TRISTATEITALIC = 96,
        IDM_TRISTATEUNDERLINE = 97,
        IDM_FOLLOW_ANCHOR = 2008,
        IDM_INSINPUTIMAGE = 2114,
        IDM_INSINPUTBUTTON = 2115,
        IDM_INSINPUTRESET = 2116,
        IDM_INSINPUTSUBMIT = 2117,
        IDM_INSINPUTUPLOAD = 2118,
        IDM_INSFIELDSET = 2119,
        IDM_PASTEINSERT = 2120,
        IDM_REPLACE = 2121,
        IDM_EDITSOURCE = 2122,
        IDM_BOOKMARK = 2123,
        IDM_HYPERLINK = 2124,
        IDM_UNLINK = 2125,
        IDM_BROWSEMODE = 2126,
        IDM_EDITMODE = 2127,
        IDM_UNBOOKMARK = 2128,
        IDM_TOOLBARS = 2130,
        IDM_STATUSBAR = 2131,
        IDM_FORMATMARK = 2132,
        IDM_TEXTONLY = 2133,
        IDM_OPTIONS = 2135,
        IDM_FOLLOWLINKC = 2136,
        IDM_FOLLOWLINKN = 2137,
        IDM_VIEWSOURCE = 2139,
        IDM_ZOOMPOPUP = 2140,
        IDM_BASELINEFONT1 = 2141,
        IDM_BASELINEFONT2 = 2142,
        IDM_BASELINEFONT3 = 2143,
        IDM_BASELINEFONT4 = 2144,
        IDM_BASELINEFONT5 = 2145,
        IDM_HORIZONTALLINE = 2150,
        IDM_LINEBREAKNORMAL = 2151,
        IDM_LINEBREAKLEFT = 2152,
        IDM_LINEBREAKRIGHT = 2153,
        IDM_LINEBREAKBOTH = 2154,
        IDM_NONBREAK = 2155,
        IDM_SPECIALCHAR = 2156,
        IDM_HTMLSOURCE = 2157,
        IDM_IFRAME = 2158,
        IDM_HTMLCONTAIN = 2159,
        IDM_TEXTBOX = 2161,
        IDM_TEXTAREA = 2162,
        IDM_CHECKBOX = 2163,
        IDM_RADIOBUTTON = 2164,
        IDM_DROPDOWNBOX = 2165,
        IDM_LISTBOX = 2166,
        IDM_BUTTON = 2167,
        IDM_IMAGE = 2168,
        IDM_OBJECT = 2169,
        IDM_1D = 2170,
        IDM_IMAGEMAP = 2171,
        IDM_FILE = 2172,
        IDM_COMMENT = 2173,
        IDM_SCRIPT = 2174,
        IDM_JAVAAPPLET = 2175,
        IDM_PLUGIN = 2176,
        IDM_PAGEBREAK = 2177,
        IDM_HTMLAREA = 2178,
        IDM_PARAGRAPH = 2180,
        IDM_FORM = 2181,
        IDM_MARQUEE = 2182,
        IDM_LIST = 2183,
        IDM_ORDERLIST = 2184,
        IDM_UNORDERLIST = 2185,
        IDM_INDENT = 2186,
        IDM_OUTDENT = 2187,
        IDM_PREFORMATTED = 2188,
        IDM_ADDRESS = 2189,
        IDM_BLINK = 2190,
        IDM_DIV = 2191,
        IDM_TABLEINSERT = 2200,
        IDM_RCINSERT = 2201,
        IDM_CELLINSERT = 2202,
        IDM_CAPTIONINSERT = 2203,
        IDM_CELLMERGE = 2204,
        IDM_CELLSPLIT = 2205,
        IDM_CELLSELECT = 2206,
        IDM_ROWSELECT = 2207,
        IDM_COLUMNSELECT = 2208,
        IDM_TABLESELECT = 2209,
        IDM_TABLEPROPERTIES = 2210,
        IDM_CELLPROPERTIES = 2211,
        IDM_ROWINSERT = 2212,
        IDM_COLUMNINSERT = 2213,
        IDM_HELP_CONTENT = 2220,
        IDM_HELP_ABOUT = 2221,
        IDM_HELP_README = 2222,
        IDM_REMOVEFORMAT = 2230,
        IDM_PAGEINFO = 2231,
        IDM_TELETYPE = 2232,
        IDM_GETBLOCKFMTS = 2233,
        IDM_BLOCKFMT = 2234,
        IDM_SHOWHIDE_CODE = 2235,
        IDM_TABLE = 2236,
        IDM_COPYFORMAT = 2237,
        IDM_PASTEFORMAT = 2238,
        IDM_GOTO = 2239,
        IDM_CHANGEFONT = 2240,
        IDM_CHANGEFONTSIZE = 2241,
        IDM_CHANGECASE = 2246,
        IDM_SHOWSPECIALCHAR = 2249,
        IDM_SUBSCRIPT = 2247,
        IDM_SUPERSCRIPT = 2248,
        IDM_CENTERALIGNPARA = 2250,
        IDM_LEFTALIGNPARA = 2251,
        IDM_RIGHTALIGNPARA = 2252,
        IDM_REMOVEPARAFORMAT = 2253,
        IDM_APPLYNORMAL = 2254,
        IDM_APPLYHEADING1 = 2255,
        IDM_APPLYHEADING2 = 2256,
        IDM_APPLYHEADING3 = 2257,
        IDM_DOCPROPERTIES = 2260,
        IDM_ADDFAVORITES = 2261,
        IDM_COPYSHORTCUT = 2262,
        IDM_SAVEBACKGROUND = 2263,
        IDM_SETWALLPAPER = 2264,
        IDM_COPYBACKGROUND = 2265,
        IDM_CREATESHORTCUT = 2266,
        IDM_PAGE = 2267,
        IDM_SAVETARGET = 2268,
        IDM_SHOWPICTURE = 2269,
        IDM_SAVEPICTURE = 2270,
        IDM_DYNSRCPLAY = 2271,
        IDM_DYNSRCSTOP = 2272,
        IDM_PRINTTARGET = 2273,
        IDM_IMGARTPLAY = 2274,
        IDM_IMGARTSTOP = 2275,
        IDM_IMGARTREWIND = 2276,
        IDM_PRINTQUERYJOBSPENDING = 2277,
        IDM_SETDESKTOPITEM = 2278,
        IDM_CONTEXTMENU = 2280,
        IDM_GOBACKWARD = 2282,
        IDM_GOFORWARD = 2283,
        IDM_PRESTOP = 2284,
        IDM_MP_MYPICS = 2287,
        IDM_MP_EMAILPICTURE = 2288,
        IDM_MP_PRINTPICTURE = 2289,
        IDM_CREATELINK = 2290,
        IDM_COPYCONTENT = 2291,
        IDM_LANGUAGE = 2292,
        IDM_GETPRINTTEMPLATE = 2295,
        IDM_SETPRINTTEMPLATE = 2296,
        IDM_TEMPLATE_PAGESETUP = 2298,
        IDM_REFRESH = 2300,
        IDM_STOPDOWNLOAD = 2301,
        IDM_ENABLE_INTERACTION = 2302,
        IDM_LAUNCHDEBUGGER = 2310,
        IDM_BREAKATNEXT = 2311,
        IDM_INSINPUTHIDDEN = 2312,
        IDM_INSINPUTPASSWORD = 2313,
        IDM_OVERWRITE = 2314,
        IDM_PARSECOMPLETE = 2315,
        IDM_HTMLEDITMODE = 2316,
        IDM_REGISTRYREFRESH = 2317,
        IDM_COMPOSESETTINGS = 2318,
        IDM_SHOWALLTAGS = 2327,
        IDM_SHOWALIGNEDSITETAGS = 2321,
        IDM_SHOWSCRIPTTAGS = 2322,
        IDM_SHOWSTYLETAGS = 2323,
        IDM_SHOWCOMMENTTAGS = 2324,
        IDM_SHOWAREATAGS = 2325,
        IDM_SHOWUNKNOWNTAGS = 2326,
        IDM_SHOWMISCTAGS = 2320,
        IDM_SHOWZEROBORDERATDESIGNTIME = 2328,
        IDM_AUTODETECT = 2329,
        IDM_SCRIPTDEBUGGER = 2330,
        IDM_GETBYTESDOWNLOADED = 2331,
        IDM_NOACTIVATENORMALOLECONTROLS = 2332,
        IDM_NOACTIVATEDESIGNTIMECONTROLS = 2333,
        IDM_NOACTIVATEJAVAAPPLETS = 2334,
        IDM_NOFIXUPURLSONPASTE = 2335,
        IDM_EMPTYGLYPHTABLE = 2336,
        IDM_ADDTOGLYPHTABLE = 2337,
        IDM_REMOVEFROMGLYPHTABLE = 2338,
        IDM_REPLACEGLYPHCONTENTS = 2339,
        IDM_SHOWWBRTAGS = 2340,
        IDM_PERSISTSTREAMSYNC = 2341,
        IDM_SETDIRTY = 2342,
        IDM_RUNURLSCRIPT = 2343,
        IDM_ZOOMRATIO = 2344,
        IDM_GETZOOMNUMERATOR = 2345,
        IDM_GETZOOMDENOMINATOR = 2346,
        IDM_DIRLTR = 2350,
        IDM_DIRRTL = 2351,
        IDM_BLOCKDIRLTR = 2352,
        IDM_BLOCKDIRRTL = 2353,
        IDM_INLINEDIRLTR = 2354,
        IDM_INLINEDIRRTL = 2355,
        IDM_ISTRUSTEDDLG = 2356,
        IDM_INSERTSPAN = 2357,
        IDM_LOCALIZEEDITOR = 2358,
        IDM_SAVEPRETRANSFORMSOURCE = 2370,
        IDM_VIEWPRETRANSFORMSOURCE = 2371,
        IDM_SCROLL_HERE = 2380,
        IDM_SCROLL_TOP = 2381,
        IDM_SCROLL_BOTTOM = 2382,
        IDM_SCROLL_PAGEUP = 2383,
        IDM_SCROLL_PAGEDOWN = 2384,
        IDM_SCROLL_UP = 2385,
        IDM_SCROLL_DOWN = 2386,
        IDM_SCROLL_LEFTEDGE = 2387,
        IDM_SCROLL_RIGHTEDGE = 2388,
        IDM_SCROLL_PAGELEFT = 2389,
        IDM_SCROLL_PAGERIGHT = 2390,
        IDM_SCROLL_LEFT = 2391,
        IDM_SCROLL_RIGHT = 2392,
        IDM_MULTIPLESELECTION = 2393,
        IDM_2D_POSITION = 2394,
        IDM_2D_ELEMENT = 2395,
        IDM_1D_ELEMENT = 2396,
        IDM_ABSOLUTE_POSITION = 2397,
        IDM_LIVERESIZE = 2398,
        IDM_AUTOURLDETECT_MODE = 2400,
        IDM_IE50_PASTE = 2401,
        IDM_IE50_PASTE_MODE = 2402,
        IDM_GETIPRINT = 2403,
        IDM_DISABLE_EDITFOCUS_UI = 2404,
        IDM_RESPECTVISIBILITY_INDESIGN = 2405,
        IDM_CSSEDITING_LEVEL = 2406,
        IDM_UI_OUTDENT = 2407,
        IDM_UPDATEPAGESTATUS = 2408,
        IDM_UNLOADDOCUMENT = 2411,
        IDM_OVERRIDE_CURSOR = 2420,
        IDM_PEERHITTESTSAMEINEDIT = 2423,
        IDM_TRUSTAPPCACHE = 2425,
        IDM_BACKGROUNDIMAGECACHE = 2430,
        IDM_GETUSERACTIONTIME = 2431,
        IDM_BEGINUSERACTION = 2432,
        IDM_ENDUSERACTION = 2433,
        IDM_SETCUSTOMCURSOR = 2434,
        IDM_DEFAULTBLOCK = 6046,
        IDM_MIMECSET__FIRST__ = 3609,
        IDM_MIMECSET__LAST__ = 3699,
        IDM_MENUEXT_FIRST__ = 3700,
        IDM_MENUEXT_LAST__ = 3732,
        IDM_MENUEXT_COUNT = 3733,
        IDM_OPEN = 2000,
        IDM_NEW = 2001,
        IDM_SAVE = 70,
        IDM_SAVEAS = 71,
        IDM_SAVECOPYAS = 2002,
        IDM_PRINTPREVIEW = 2003,
        IDM_SHOWPRINT = 2010,
        IDM_SHOWPAGESETUP = 2011,
        IDM_PRINT = 27,
        IDM_PAGESETUP = 2004,
        IDM_SPELL = 2005,
        IDM_PASTESPECIAL = 2006,
        IDM_CLEARSELECTION = 2007,
        IDM_PROPERTIES = 28,
        IDM_REDO = 29,
        IDM_UNDO = 43,
        IDM_SELECTALL = 31,
        IDM_ZOOMPERCENT = 50,
        IDM_GETZOOM = 68,
        IDM_STOP = 2138,
        IDM_COPY = 15,
        IDM_CUT = 16,
        IDM_PASTE = 26,
        CMD_ZOOM_PAGEWIDTH = -1,
        CMD_ZOOM_ONEPAGE = -2,
        CMD_ZOOM_TWOPAGES = -3,
        CMD_ZOOM_SELECTION = -4,
        CMD_ZOOM_FIT = -5,
        IDM_CONTEXT = 1,
        IDM_HWND = 2,
        IDM_NEW_TOPLEVELWINDOW = 7050,
        IDM_PRESERVEUNDOALWAYS = 6049,
        IDM_PERSISTDEFAULTVALUES = 7100,
        IDM_PROTECTMETATAGS = 7101,
        IDM_GETFRAMEZONE = 6037,
        IDM_FIRE_PRINTTEMPLATEUP = 15000,
        IDM_FIRE_PRINTTEMPLATEDOWN = 15001,
        IDM_SETPRINTHANDLES = 15002,
        IDM_GETUSERINITFLAGS = 15004,
        IDM_GETDOCDLGFLAGS = 15005
    }

    public sealed class Hresults
    {
        public const int NOERROR = 0;
        public const int S_OK = 0;
        public const int S_FALSE = 1;
        public const int E_PENDING = unchecked((int)0x8000000A);
        public const int E_HANDLE = unchecked((int)0x80070006);
        public const int E_NOTIMPL = unchecked((int)0x80004001);
        public const int E_NOINTERFACE = unchecked((int)0x80004002);
        //ArgumentNullException. NullReferenceException uses COR_E_NULLREFERENCE
        public const int E_POINTER = unchecked((int)0x80004003);
        public const int E_ABORT = unchecked((int)0x80004004);
        public const int E_FAIL = unchecked((int)0x80004005);
        public const int E_OUTOFMEMORY = unchecked((int)0x8007000E);
        public const int E_ACCESSDENIED = unchecked((int)0x80070005);
        public const int E_UNEXPECTED = unchecked((int)0x8000FFFF);
        public const int E_FLAGS = unchecked((int)0x1000);
        public const int E_INVALIDARG = unchecked((int)0x80070057);

        //Wininet
        public const int ERROR_SUCCESS = 0;
        public const int ERROR_FILE_NOT_FOUND = 2;
        public const int ERROR_ACCESS_DENIED = 5;
        public const int ERROR_INSUFFICIENT_BUFFER = 122;
        public const int ERROR_NO_MORE_ITEMS = 259;

        //Ole Errors
        public const int OLE_E_FIRST = unchecked((int)0x80040000);
        public const int OLE_E_LAST = unchecked((int)0x800400FF);
        public const int OLE_S_FIRST = unchecked((int)0x00040000);
        public const int OLE_S_LAST = unchecked((int)0x000400FF);
        //OLECMDERR_E_FIRST = 0x80040100
        public const int OLECMDERR_E_FIRST = unchecked((int)(OLE_E_LAST + 1));
        public const int OLECMDERR_E_NOTSUPPORTED = unchecked((int)(OLECMDERR_E_FIRST));
        public const int OLECMDERR_E_DISABLED = unchecked((int)(OLECMDERR_E_FIRST + 1));
        public const int OLECMDERR_E_NOHELP = unchecked((int)(OLECMDERR_E_FIRST + 2));
        public const int OLECMDERR_E_CANCELED = unchecked((int)(OLECMDERR_E_FIRST + 3));
        public const int OLECMDERR_E_UNKNOWNGROUP = unchecked((int)(OLECMDERR_E_FIRST + 4));

        public const int OLEOBJ_E_NOVERBS = unchecked((int)0x80040180);
        public const int OLEOBJ_S_INVALIDVERB = unchecked((int)0x00040180);
        public const int OLEOBJ_S_CANNOT_DOVERB_NOW = unchecked((int)0x00040181);
        public const int OLEOBJ_S_INVALIDHWND = unchecked((int)0x00040182);

        public const int DV_E_LINDEX = unchecked((int)0x80040068);
        public const int OLE_E_OLEVERB = unchecked((int)0x80040000);
        public const int OLE_E_ADVF = unchecked((int)0x80040001);
        public const int OLE_E_ENUM_NOMORE = unchecked((int)0x80040002);
        public const int OLE_E_ADVISENOTSUPPORTED = unchecked((int)0x80040003);
        public const int OLE_E_NOCONNECTION = unchecked((int)0x80040004);
        public const int OLE_E_NOTRUNNING = unchecked((int)0x80040005);
        public const int OLE_E_NOCACHE = unchecked((int)0x80040006);
        public const int OLE_E_BLANK = unchecked((int)0x80040007);
        public const int OLE_E_CLASSDIFF = unchecked((int)0x80040008);
        public const int OLE_E_CANT_GETMONIKER = unchecked((int)0x80040009);
        public const int OLE_E_CANT_BINDTOSOURCE = unchecked((int)0x8004000A);
        public const int OLE_E_STATIC = unchecked((int)0x8004000B);
        public const int OLE_E_PROMPTSAVECANCELLED = unchecked((int)0x8004000C);
        public const int OLE_E_INVALIDRECT = unchecked((int)0x8004000D);
        public const int OLE_E_WRONGCOMPOBJ = unchecked((int)0x8004000E);
        public const int OLE_E_INVALIDHWND = unchecked((int)0x8004000F);
        public const int OLE_E_NOT_INPLACEACTIVE = unchecked((int)0x80040010);
        public const int OLE_E_CANTCONVERT = unchecked((int)0x80040011);
        public const int OLE_E_NOSTORAGE = unchecked((int)0x80040012);
        public const int RPC_E_RETRY = unchecked((int)0x80010109);
    }

}
