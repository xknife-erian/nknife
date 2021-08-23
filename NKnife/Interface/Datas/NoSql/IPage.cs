using System.Collections.Generic;

namespace NKnife.Interface.Datas.NoSql
{
    /// <summary>
    ///     这是一个描述捕获分页请求后的处理结果及元信息。
    ///     分页请求的处理结果一般来讲是所有Document的总集列表的子集列表。同时，允许获取该集合的相关信息。
    /// </summary>
    public interface IPage<T>
    {
        /// <summary>
        ///     当前页的处理结果。即项目的集合。
        /// </summary>
        ICollection<T> Content { get; }

        /// <summary>
        ///     当前页码
        /// </summary>
        uint Number { get; }

        /// <summary>
        ///     当前页中的期望项目数量
        /// </summary>
        uint Size { get; }

        /// <summary>
        ///     当前页中的实际项目数量，因查询条件或尾页等原因，有可能小于期望项目数量
        /// </summary>
        uint SizeOfElements { get; }

        /// <summary>
        ///     是否有结果。
        /// </summary>
        bool HasContent { get; }

        /// <summary>
        ///     是否是首页
        /// </summary>
        bool IsFirst { get; }

        /// <summary>
        ///     是否是尾页
        /// </summary>
        bool IsLast { get; }

        /// <summary>
        ///     是否有下一页
        /// </summary>
        bool HasNext { get; }

        /// <summary>
        ///     是否有上一页
        /// </summary>
        bool HasPrevious { get; }

        /// <summary>
        ///     依据当前页创建下一页的请求
        /// </summary>
        IPageable<T> NextPageable { get; }

        /// <summary>
        ///     依据当前页创建上一页的请求
        /// </summary>
        IPageable<T> PreviousPageable { get; }

        /// <summary>
        ///     排序比较器
        /// </summary>
        IComparer<T> Comparer { get; }

        /// <summary>
        ///     总页数
        /// </summary>
        uint TotalPages { get; }

        /// <summary>
        ///     合计总的项目数量。一般来讲，应在处理分页请求后，将集合的Count数量给入。
        /// </summary>
        ulong TotalElements { get; }

    }
}