using System.Collections.Generic;

namespace Didaku.Engine.Timeaxis.API.Controllers
{
    public class JqGridParam
    {
        /// <summary>
        ///     多字段查询时分组类型，主要是AND或者OR
        /// </summary>
        public string groupOp { get; set; }

        /// <summary>
        ///     多字段查询时候，查询条件的集合
        /// </summary>
        public List<string> rules { get; set; }

        /// <summary>
        ///     当前第几页
        /// </summary>
        public int page { get; set; }

        /// <summary>
        ///     每页显示多少条数据
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        ///     排序字段
        /// </summary>
        public string sidx { get; set; }

        /// <summary>
        ///     排序类型 ASC或者DESC
        /// </summary>
        public Sord sord { get; set; }

        /// <summary>
        ///     是否是查询 true 或者 false
        /// </summary>
        public bool _search { get; set; }

        public string nd { get; set; }

        /// <summary>
        ///     单字段查询的时候，查询字段名称
        /// </summary>
        public string searchField { get; set; }

        /// <summary>
        ///     单字段查询的时候，查询字段的值
        /// </summary>
        public string searchString { get; set; }

        /// <summary>
        ///     单字段查询的时候，查询的操作
        /// </summary>
        public string searchOper { get; set; }
    }

    public enum Sord
    {
        asc,desc
    }
}