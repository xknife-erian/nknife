using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.App.Cute.Base.EventArg;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Implement.Abstracts
{
    /// <summary>描述营业厅的一个窗口(柜台)服务队列的属性特征类型。
    /// 它将被包括在一个窗口(柜台)所拥有的<see cref="IServiceLogic"/>队列组中。
    /// </summary>
    [Serializable]
    public abstract class BaseQueue : IServiceQueue
    {
        private readonly ConcurrentBag<ITransaction> _Trans = new ConcurrentBag<ITransaction>();

        /// <summary>描述营业厅的一个窗口(柜台)服务队列的属性特征类型。
        /// 它将被包括在一个窗口(柜台)所拥有的<see cref="IServiceLogic"/>队列组中。
        /// </summary>
        protected BaseQueue()
        {
            Elements = new List<IServiceQueueElement>(1);
        }

        #region IServiceQueue: 基本特征属性

        /// <summary>队列标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>助记编号（单网点唯一）
        /// </summary>
        public string Number { get; set; }

        /// <summary>队列的表示元素，可以是业务,业务分类,客户类别,员工,等等
        /// </summary>
        public List<IServiceQueueElement> Elements { get; set; }

        /// <summary>识别符的生成器
        /// </summary>
        public IIdentifierGenerator IdentifierGenerator { get; set; }

        /// <summary>这个队列的事务对应的工作流水线
        /// </summary>
        public string PipelineId { get; set; }

        /// <summary>队列名称
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region IServiceQueue: 基本方法

        /// <summary>当前活动中的交易。当为Null时仅代表没有活动的交易，不代表没有预约的交易。
        /// </summary>
        public virtual ITransaction Current { get; protected set; }

        /// <summary>计算指定的分类的交易数量
        /// </summary>
        /// <returns></returns>
        public virtual int Calculate(int minKind, int maxKind)
        {
            //TODO:计算指定的分类的交易数量
            throw new NotImplementedException();
        }

        /// <summary>生成一条新的起始交易<see cref="BaseTransaction"/>
        /// </summary>
        /// <returns>起始交易<see cref="BaseTransaction"/></returns>
        public abstract ITransaction GetBookingTransaction();

        /// <summary>尝试根据指定的条件返回最早加入的<see cref="BaseTransaction"/>.当指定的条件为Null时,采用最基本的预约时间优先原则。
        /// </summary>
        /// <param name="transaction">输出:可办理的交易</param>
        /// <param name="func">指定的条件。条件为Null时,采用最基本的预约时间优先原则。</param>
        /// <returns>如果有可办理的交易，返回真。否则反之。</returns>
        public virtual bool TryTake(out ITransaction transaction, Func<IEnumerable<ITransaction>, ITransaction> func = null)
        {
            transaction = (func != null) ? func.Invoke(_Trans) : GetByGetNumberTimeFunc().Invoke(_Trans);
            return transaction != null;
        }

        /// <summary>尝试根据指定的票号返回<see cref="BaseTransaction"/>,并从队列中移除这个<see cref="BaseTransaction"/>
        /// </summary>
        public bool TryFind(out ITransaction transaction, Func<IQueryable<ITransaction>, ITransaction> func)
        {
            transaction = null; // func.Invoke(Datas.Instance.Transactions);
            return transaction != null;
        }

        /// <summary>向队列中添加一个新的交易<see cref="BaseTransaction"/>,当是新的交易时本方法中会激发新增事件
        /// </summary>
        /// <param name="tran">The tran.</param>
        /// <param name="isNewTransaction">被添加的交易是否是新的交易(可能是被转移过来的,就不属于新交易)</param>
        public virtual void Add(ITransaction tran, bool isNewTransaction = true)
        {
            _Trans.Add(tran);
            if (isNewTransaction)
                OnAdded(new ServiceQueueEventArgs(tran));
        }

        /// <summary>从队列中移除一个交易<see cref="BaseTransaction"/>,本方法中会激发移除事件
        /// </summary>
        public virtual void Remove(ITransaction tran)
        {
            ITransaction item;
            _Trans.TryTake(out item);
            if (item != null)
                OnRemoved(new ServiceQueueEventArgs(item));
        }

        private bool IsSearchCondition(ITransaction tran, string number)
        {
            return tran.Identifier.Equals(number);
        }

        #endregion

        #region Implementation of IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>返回循环访问<see cref="BaseQueue"/>中的<see cref="BaseTransaction"/>的枚举器
        /// </summary>
        public IEnumerator<ITransaction> GetEnumerator()
        {
// ReSharper disable AssignNullToNotNullAttribute
            return _Trans.GetEnumerator();
// ReSharper restore AssignNullToNotNullAttribute
        }

        #endregion

        #region Protected

        private readonly List<string> _ElementIds = new List<string>();
        private int _CurrentTicketNumber = -1;
        private StringBuilder _TicketNumberBuilder = new StringBuilder();

        protected int Count
        {
            get { return _Trans.Count; }
        }

        /// <summary>队列中是否包含指定的<see cref="BaseTransaction"/>
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        protected bool Contains(ITransaction item)
        {
            return _Trans.Contains(item);
        }

        /// <summary>将队列中的<see cref="BaseTransaction"/>拷贝到指定的数组中
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        /// <remarks></remarks>
        protected void CopyTo(ITransaction[] array, int arrayIndex)
        {
            _Trans.CopyTo(array, arrayIndex);
        }

        #endregion

        #region 事件的定义

        /// <summary>当队列中新增一条交易<see cref="BaseTransaction"/>时发生的事件
        /// </summary>
        public event ServiceQueueEventHandler TransactionAddedEvent;

        /// <summary>当队列中移除一条交易<see cref="BaseTransaction"/>时发生的事件
        /// </summary>
        public event ServiceQueueEventHandler TransactionRemovedEvent;

        protected virtual void OnAdded(ServiceQueueEventArgs e)
        {
            if (TransactionAddedEvent != null)
                TransactionAddedEvent(this, e);
        }

        protected virtual void OnRemoved(ServiceQueueEventArgs e)
        {
            if (TransactionRemovedEvent != null)
                TransactionRemovedEvent(this, e);
        }

        #endregion

        #region IServiceQueue Members

        public bool Equals(IServiceQueue other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public abstract object Clone();

        #endregion

        #region Override

        /// <summary>Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (BaseQueue)) return false;
            return Equals((BaseQueue) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseQueue left, BaseQueue right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseQueue left, BaseQueue right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region 从一个集合中根据预约时间按预约时间优先原则筛选

        /// <summary>从一个集合中根据预约时间按预约时间优先原则筛选
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static Func<IEnumerable<ITransaction>, ITransaction> GetByGetNumberTimeFunc()
        {
            return trans =>
            {
                if (trans == null || !trans.Any())
                    return null;
                IOrderedEnumerable<ITransaction> result = from tran in trans
                                                          orderby
                                                              tran.Time
                                                          select tran;
                return result.FirstOrDefault();
            };
        }

        #endregion
    }
}