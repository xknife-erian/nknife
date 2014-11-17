using System.Web.Mvc;

namespace NKnife.App.Cute.API.Controllers
{
    /// <summary>面向jqGrid的Json格式结果。请注意在Js中要加入下面的代码即可得到正确的解析。
    /// <code>
    /// jsonReader: {
    ///    root: "Data",
    ///    repeatitems: false
    /// }</code>
    /// </summary>
    public class JqGridResult : JsonResult
    {
        public JqGridResult()
        {
            page = 1;
        }
        /// <summary>总页数
        /// </summary>
        public int total { get; set; }
        /// <summary>当前页码
        /// </summary>
        public int page { get; set; }
        /// <summary>查询出的记录数
        /// </summary>
        public int records { get; set; }
    }
}