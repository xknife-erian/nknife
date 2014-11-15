using System;
using System.Collections.Generic;
using System.Linq;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using NKnife.App.Cute.Base.EventArg;

namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>描述一个服务队列
    /// </summary>
    /// <remarks></remarks>
    public interface IServiceQueue : ICloneable, IEnumerable<ITransaction>, IEquatable<IServiceQueue>
    {
        /// <summary>队列标识
        /// </summary>
        string Id { get; set; }

        /// <summary>队列名称
        /// </summary>
        string Name { get; set; }

        /// <summary>助记编号
        /// </summary>
        string Number { get; set; }

        /// <summary>队列的表示元素，可以是业务,业务分类,客户类别,员工,等等
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        List<IServiceQueueElement> Elements { get; set; }

        /// <summary>识别符的生成器
        /// </summary>
        IIdentifierGenerator IdentifierGenerator { get; set; }

        /// <summary>这个队列的事务对应的工作流水线
        /// </summary>
        string PipelineId { get; set; }

        /// <summary>计算指定的分类的交易数量
        /// </summary>
        /// <returns></returns>
        int Calculate(int minKind, int maxKind);

        /// <summary>生成一条新的起始交易<see cref="ITransaction"/>
        /// </summary>
        /// <returns>起始交易<see cref="ITransaction"/></returns>
        ITransaction GetBookingTransaction();

        /// <summary>根据指定的条件返加最早加入的<see cref="ITransaction"/>,并从队列中移除这个<see cref="ITransaction"/>.
        /// </summary>
        /// <param name="transaction">输出:可办理的交易</param>
        /// <param name="func">指定的条件</param>
        /// <returns>如果有可办理的交易，返回真。否则反之。</returns>
        bool TryTake(out ITransaction transaction, Func<IEnumerable<ITransaction>, ITransaction> func);

        /// <summary>尝试根据指定的票号返回<see cref="ITransaction"/>,并从队列中移除这个<see cref="ITransaction"/>
        /// </summary>
        bool TryFind(out ITransaction transaction, Func<IQueryable<ITransaction>, ITransaction> func);

        /// <summary>向队列中添加一个新的交易<see cref="ITransaction"/>,本方法中会激发新增事件
        /// </summary>
        /// <param name="tran">The tran.</param>
        /// <param name="isNewTransaction">被添加的交易是否是新的交易(可能是被转移过来的,就不属于新交易)</param>
        void Add(ITransaction tran, bool isNewTransaction = true);

        /// <summary>从队列中移除一个交易<see cref="ITransaction"/>,本方法中会激发移除事件
        /// </summary>
        void Remove(ITransaction tran);

        /// <summary>当队列中新增一条交易<see cref="ITransaction"/>时发生的事件
        /// </summary>
        event ServiceQueueEventHandler TransactionAddedEvent;

        /// <summary>当队列中移除一条交易<see cref="ITransaction"/>时发生的事件
        /// </summary>
        event ServiceQueueEventHandler TransactionRemovedEvent;
    }
}