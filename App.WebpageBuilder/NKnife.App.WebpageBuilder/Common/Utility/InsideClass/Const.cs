using System;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class Const
        {
            #region 后缀名

            /// <summary>
            /// 页面类型文件的扩展名(.sdpage)
            /// </summary>
            public const string PageFileExt = ".sdpage";

            /// <summary>
            /// 模板类型文件的扩展名(.sdtmplt)
            /// </summary>
            public const string TmpltFileExt = ".sdtmplt";

            /// <summary>
            /// Sdsite文件的扩展名(.sdsite)
            /// </summary>
            public const string SdsiteFileExt = ".sdsite";

            /// <summary>
            /// 被include的文件的后缀名(.inc)
            /// </summary>
            public const string IncludeFileExt = ".inc";

            /// <summary>
            /// 页面文件，模板文件因调用服务器SSI技术所采用的后缀名(.shtml)
            /// </summary>
            public const string ShtmlFileExt = ".shtml";

            /// <summary>
            /// CSS樣式文件的後綴名(.css)
            /// </summary>
            public const string CssFileExt = ".css";


            #endregion

            /// <summary>
            /// 顶级频道的ID，这是一种约定，没有规则而言。默認“00000000”。
            /// </summary>
            public const string ChannelRootId = "00000000";

            /// <summary>
            /// 保存在项目文件中的日期格式
            /// </summary>
            public const string TimeFormat = "yyyy-MM-dd HH:mm:ss";

            //public const string INDEXFILENAME = "index.shtml";
            //public const string STRCSS = "#{0} {{{1}}}";
            //public const string REQUESTHEAD = "<files></files>";

            //public const string PublishDtd = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
            //public const string SHTML = ".shtml";
            //public const string PHP = ".php";
            //public const string HTML = ".html";
            //public const string PUBLISH = "Publish";
            //public const string PREVIEW = "Preview";
            //public const string USERXML = "user.xml";
        }
    }
}
