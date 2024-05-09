using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NKnife.Interface.Datas.NoSql
{
    /// <summary>
    ///    这是一个描述分页请求的接口
    /// </summary>
    public interface IPageable<T>
    {
        /// <summary>
        ///     页码。索引从0开始。
        /// </summary>
        uint PageNumber { get; }

        /// <summary>
        ///     当前页中的项目数量
        /// </summary>
        uint PageSize { get; }

        /// <summary>
        ///     当本次请求结束后在总记录中的偏移量。
        /// </summary>
        uint Offset { get; }

        /// <summary>
        ///     当前将要被使用的用于排序的比较器
        /// </summary>
        IComparer<T> Comparer { get; }

        /// <summary>
        ///     基于本次请求返回一个“首页”的新请求<see cref="IPageable"/>。
        /// </summary>
        IPageable<T> First();

        /// <summary>
        ///     基于本次请求返回一个“下一页”的新请求<see cref="IPageable"/>。
        /// </summary>
        IPageable<T> Next();

        /// <summary>
        ///     基于本次请求返回一个“上一页”的新请求<see cref="IPageable"/>。
        /// </summary>
        IPageable<T> Previous();

        /// <summary>
        ///     基于本次请求返回一个“上一页”的新请求<see cref="IPageable"/>，如果“上一页”是第一页时，返回“首页”请求。
        /// </summary>
        IPageable<T> PreviousOrFirst();

        /// <summary>
        ///     是否有“上一页”，即当前页是否是“首页”
        /// </summary>
        bool HasPrevious { get; }

        /// <summary>
        ///     分页的查询条件
        /// </summary>
        Expression<Func<T, bool>> Predicate { get; }
    }
}