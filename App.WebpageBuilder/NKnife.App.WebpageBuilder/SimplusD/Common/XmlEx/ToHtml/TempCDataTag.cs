using System;

namespace Jeelu
{
    /// <summary>
    /// 临时的为了处理CData节点的字符串。
    /// 下一步在Xhtml组件完成XhtmlParser后应该不再使用。
    /// design by lukan，2008-6-28 17:04:40
    /// </summary>
    internal class TempCDataTag
    {
        /// <summary>
        /// 临时的为了处理CData节点的字符串,"!#@#!"。
        /// </summary>
        internal const string CDataTag = "!#@#!";
    }
}
