using System;
using System.Collections.Generic;
using NKnife.Interface.Datas.NoSql;

namespace NKnife.DataLite
{
    /// <summary>
    ///     这是一个描述捕获分页请求后的处理结果及元信息的类型。
    ///     分页请求的处理结果一般来讲是所有Document的总集列表的子集列表。同时，允许获取该集合的相关信息。
    /// </summary>
    public class Page<T> : IPage<T>
    {
        protected readonly IPageable<T> _Pageable;
        protected readonly ulong _TotalElements;

        /// <summary>
        ///     构造函数：这是描述捕获分页请求后的处理结果及元信息的类型。
        ///     通过构造函数将各分页请求的引用回传给调用方。并将核心的一些数据集合元信息给入，以便于调用方进行处理。
        /// </summary>
        /// <param name="pageable">分页请求。</param>
        /// <param name="content">页中的内容集合。</param>
        /// <param name="totalElement">合计总的项目数量。一般来讲，应在处理分页请求后，将集合的Count数量给入。</param>
        public Page(IPageable<T> pageable, ICollection<T> content, ulong totalElement = 0)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content), "页项目集合不能为空");
            Content = content;
            if (pageable == null)
                throw new ArgumentNullException(nameof(pageable), "页请求不能为空");
            _Pageable = pageable;
            _TotalElements = (pageable.Offset + content.Count > (long) totalElement)//当给入的总数量小于已能计算的偏移量+当前内容量时
                ? (ulong) (pageable.Offset + content.Count)//非正确的集合的Count数量给入，故计算总项目数量。
                : totalElement;//正确的集合的Count数量给入。
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
        public uint SizeOfElements => (uint) Content.Count;

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
        public uint TotalPages => Size == 0 ? 1 : (uint) Math.Ceiling(_TotalElements / (double) Size);

        /// <summary>
        ///     合计总的项目数量。一般来讲，应在处理分页请求后，将集合的Count数量给入。
        /// </summary>
        public ulong TotalElements => _TotalElements;

        #endregion
    }
}