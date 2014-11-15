namespace NKnife.App.Cute.API.Controllers
{
    public class JqGridSearchDetailToParam
    {
        /// <summary>
        ///     查询字段
        /// </summary>
        public string field { get; set; }

        /// <summary>
        ///     查询操作
        /// </summary>
        public string op { get; set; }

        /// <summary>
        ///     选择的查询值
        /// </summary>
        public string data { get; set; }
    }
}