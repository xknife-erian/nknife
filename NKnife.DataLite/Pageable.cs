using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using NKnife.Interface.Datas.NoSql;

namespace NKnife.DataLite
{
    /// <summary>
    ///    这是一个描述分页请求的接口的实现。
    /// </summary>
    public class Pageable<T> : IPageable<T>
    {
        /// <summary>
        /// 构造函数。描述分页请求。
        /// </summary>
        /// <param name="number">页码。索引从0开始。</param>
        /// <param name="size">项目数</param>
        /// <param name="comparer">排序比较器</param>
        /// <param name="predicate">查询条件</param>
        public Pageable(uint number, uint size, IComparer<T> comparer, Expression<Func<T, bool>> predicate)
        {
            PageNumber = number;
            PageSize = size;
            Comparer = comparer;
            Predicate = predicate;
        }

        #region Implementation of IPageable

        /// <summary>
        ///     页码。索引从0开始。
        /// </summary>
        public uint PageNumber { get; protected set; }

        /// <summary>
        ///     当前页中的项目数量
        /// </summary>
        public uint PageSize { get; protected set; }

        /// <summary>
        ///     当本次请求结束后在总记录中的偏移量。
        /// </summary>
        public uint Offset => PageNumber * PageSize;

        /// <summary>
        ///     当前将要被使用的用于排序的比较器
        /// </summary>
        public IComparer<T> Comparer { get; }

        /// <summary>
        ///     基于本次请求返回一个“首页”的新请求<see cref="IPageable{T}"/>。
        /// </summary>
        public IPageable<T> First()
        {
            return new Pageable<T>(0, PageSize, Comparer, Predicate);
        }

        /// <summary>
        ///     基于本次请求返回一个“下一页”的新请求<see cref="IPageable{T}"/>。
        /// </summary>
        public IPageable<T> Next()
        {
            return new Pageable<T>(PageNumber + 1, PageSize, Comparer, Predicate);
        }

        /// <summary>
        ///     基于本次请求返回一个“上一页”的新请求<see cref="IPageable{T}"/>。
        /// </summary>
        public IPageable<T> Previous()
        {
            return PageNumber == 0 ? this : new Pageable<T>(PageNumber - 1, PageSize, Comparer, Predicate);
        }

        /// <summary>
        ///     基于本次请求返回一个“上一页”的新请求<see cref="IPageable{T}"/>，如果“上一页”是第一页时，返回“首页”请求。
        /// </summary>
        public IPageable<T> PreviousOrFirst()
        {
            return HasPrevious ? Previous() : First();
        }

        /// <summary>
        ///     是否有“上一页”，即当前页是否是“首页”
        /// </summary>
        public bool HasPrevious
        {
            get { return PageNumber > 0; }
        }

        /// <summary>
        ///     分页的查询条件
        /// </summary>
        public Expression<Func<T, bool>> Predicate { get; }

        #endregion
    }
}