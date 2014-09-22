using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.Win
{
    public enum InsertMode
    {
        File,
        Id,
    }

    public enum MediaFileType
    {
        Pic,
        Flash,
        Audio,
        Video,
    }

    public enum Align
    {
        Default,
        baseline,
        top,
        middle,
        bottom,
        texttop,
        absolutemiddle,
        absolutebottom,
        left,
        right,
    }

    public enum Quality
    {
        Hight,
        Low,
        AutoHight,
        AutoLow,
    }

    public enum Unit
    {
        percent,
        pix,
    }

    public enum HeadScope
    {
        none,
        left,
        top,
        both,
    }

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
