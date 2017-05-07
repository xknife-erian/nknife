using System;
using System.Collections.Generic;
using NKnife.DataLite.Interfaces;

namespace NKnife.DataLite
{
    /// <summary>
    ///     这是一个描述捕获分页请求后的处理结果及元信息。
    ///     分页请求的处理结果一般来讲是所有Document的总集列表的子集列表。同时，允许获取该集合的相关信息。
    /// </summary>
    public class Page<T> : IPage<T>
    {
        protected readonly IPageable<T> _Pageable;
        protected readonly ulong _Total;

        /// <summary>Constructor</summary>
        /// <param name="content">the content of this page, must not be null.</param>
        /// <param name="pageable">the paging information, can be null.</param>
        /// <param name="total">
        ///     the total amount of items available. The total might be adapted considering the length of the
        ///     content given, if it is going to be the content of the last page.This is in place to mitigate inconsistencies
        /// </param>
        public Page(ICollection<T> content, IPageable<T> pageable, ulong total)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content), "页项目集合不能为空");
            _Pageable = pageable;
            _Total = !(content.Count <= 0) && pageable != null && pageable.Offset + pageable.PageSize > total
                ? (ulong) (pageable.Offset + content.Count)
                : total;
        }

        #region Implementation of IPage<T>

        /// <summary>
        ///     当前页码
        /// </summary>
        public uint Number => _Pageable?.PageNumber ?? 0;

        /// <summary>
        ///     当前页中的期望项目数量
        /// </summary>
        public uint Size => _Pageable?.PageSize ?? 0;

        /// <summary>
        ///     当前页中的实际项目数量，因查询条件或尾页等原因，有可能小于期望项目数量
        /// </summary>
        public uint NumberOfElements => (uint) Content.Count;

        /// <summary>
        ///     当前页的处理结果。即项目的集合。
        /// </summary>
        public ICollection<T> Content { get; }

        /// <summary>
        ///     是否有结果。
        /// </summary>
        public bool HasContent => Content?.Count > 0;

        /// <summary>
        ///     是否是首页
        /// </summary>
        public bool IsFirst => !HasPrevious;

        /// <summary>
        ///     是否是尾页
        /// </summary>
        public bool IsLast => !HasNext;

        /// <summary>
        ///     是否有下一页
        /// </summary>
        public bool HasNext => Number + 1 < TotalPages;

        /// <summary>
        ///     是否有上一页
        /// </summary>
        public bool HasPrevious => Number > 0;

        /// <summary>
        ///     依据当前页创建下一页的请求
        /// </summary>
        public IPageable<T> NextPageable => HasNext ? _Pageable.Next() : null;

        /// <summary>
        ///     依据当前页创建上一页的请求
        /// </summary>
        public IPageable<T> PreviousPageable
        {
            get
            {
                if (HasPrevious)
                    return _Pageable.PreviousOrFirst();
                return null;
            }
        }

        /// <summary>
        ///     排序比较器
        /// </summary>
        public IComparer<T> Comparer
        {
            get { return (IComparer<T>) _Pageable.Comparer; }
        }

        /// <summary>
        ///     总页数
        /// </summary>
        public uint TotalPages => Size == 0 ? 1 : (uint) Math.Ceiling(_Total / (double) Size);

        /// <summary>
        ///     总项目数
        /// </summary>
        public ulong TotalElements => _Total;

        #endregion
    }
}