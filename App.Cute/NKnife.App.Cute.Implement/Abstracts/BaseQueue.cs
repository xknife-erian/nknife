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
    /// <summary>����Ӫҵ����һ������(��̨)������е������������͡�
    /// ������������һ������(��̨)��ӵ�е�<see cref="IServiceLogic"/>�������С�
    /// </summary>
    [Serializable]
    public abstract class BaseQueue : IServiceQueue
    {
        private readonly ConcurrentBag<ITransaction> _Trans = new ConcurrentBag<ITransaction>();

        /// <summary>����Ӫҵ����һ������(��̨)������е������������͡�
        /// ������������һ������(��̨)��ӵ�е�<see cref="IServiceLogic"/>�������С�
        /// </summary>
        protected BaseQueue()
        {
            Elements = new List<IServiceQueueElement>(1);
        }

        #region IServiceQueue: ������������

        /// <summary>���б�ʶ
        /// </summary>
        public string Id { get; set; }

        /// <summary>���Ǳ�ţ�������Ψһ��
        /// </summary>
        public string Number { get; set; }

        /// <summary>���еı�ʾԪ�أ�������ҵ��,ҵ�����,�ͻ����,Ա��,�ȵ�
        /// </summary>
        public List<IServiceQueueElement> Elements { get; set; }

        /// <summary>ʶ�����������
        /// </summary>
        public IIdentifierGenerator IdentifierGenerator { get; set; }

        /// <summary>������е������Ӧ�Ĺ�����ˮ��
        /// </summary>
        public string PipelineId { get; set; }

        /// <summary>��������
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region IServiceQueue: ��������

        /// <summary>��ǰ��еĽ��ס���ΪNullʱ������û�л�Ľ��ף�������û��ԤԼ�Ľ��ס�
        /// </summary>
        public virtual ITransaction Current { get; protected set; }

        /// <summary>����ָ���ķ���Ľ�������
        /// </summary>
        /// <returns></returns>
        public virtual int Calculate(int minKind, int maxKind)
        {
            //TODO:����ָ���ķ���Ľ�������
            throw new NotImplementedException();
        }

        /// <summary>����һ���µ���ʼ����<see cref="BaseTransaction"/>
        /// </summary>
        /// <returns>��ʼ����<see cref="BaseTransaction"/></returns>
        public abstract ITransaction GetBookingTransaction();

        /// <summary>���Ը���ָ��������������������<see cref="BaseTransaction"/>.��ָ��������ΪNullʱ,�����������ԤԼʱ������ԭ��
        /// </summary>
        /// <param name="transaction">���:�ɰ���Ľ���</param>
        /// <param name="func">ָ��������������ΪNullʱ,�����������ԤԼʱ������ԭ��</param>
        /// <returns>����пɰ���Ľ��ף������档����֮��</returns>
        public virtual bool TryTake(out ITransaction transaction, Func<IEnumerable<ITransaction>, ITransaction> func = null)
        {
            transaction = (func != null) ? func.Invoke(_Trans) : GetByGetNumberTimeFunc().Invoke(_Trans);
            return transaction != null;
        }

        /// <summary>���Ը���ָ����Ʊ�ŷ���<see cref="BaseTransaction"/>,���Ӷ������Ƴ����<see cref="BaseTransaction"/>
        /// </summary>
        public bool TryFind(out ITransaction transaction, Func<IQueryable<ITransaction>, ITransaction> func)
        {
            transaction = null; // func.Invoke(Datas.Instance.Transactions);
            return transaction != null;
        }

        /// <summary>����������һ���µĽ���<see cref="BaseTransaction"/>,�����µĽ���ʱ�������лἤ�������¼�
        /// </summary>
        /// <param name="tran">The tran.</param>
        /// <param name="isNewTransaction">����ӵĽ����Ƿ����µĽ���(�����Ǳ�ת�ƹ�����,�Ͳ������½���)</param>
        public virtual void Add(ITransaction tran, bool isNewTransaction = true)
        {
            _Trans.Add(tran);
            if (isNewTransaction)
                OnAdded(new ServiceQueueEventArgs(tran));
        }

        /// <summary>�Ӷ������Ƴ�һ������<see cref="BaseTransaction"/>,�������лἤ���Ƴ��¼�
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

        /// <summary>����ѭ������<see cref="BaseQueue"/>�е�<see cref="BaseTransaction"/>��ö����
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

        /// <summary>�������Ƿ����ָ����<see cref="BaseTransaction"/>
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        protected bool Contains(ITransaction item)
        {
            return _Trans.Contains(item);
        }

        /// <summary>�������е�<see cref="BaseTransaction"/>������ָ����������
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        /// <remarks></remarks>
        protected void CopyTo(ITransaction[] array, int arrayIndex)
        {
            _Trans.CopyTo(array, arrayIndex);
        }

        #endregion

        #region �¼��Ķ���

        /// <summary>������������һ������<see cref="BaseTransaction"/>ʱ�������¼�
        /// </summary>
        public event ServiceQueueEventHandler TransactionAddedEvent;

        /// <summary>���������Ƴ�һ������<see cref="BaseTransaction"/>ʱ�������¼�
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

        #region ��һ�������и���ԤԼʱ�䰴ԤԼʱ������ԭ��ɸѡ

        /// <summary>��һ�������и���ԤԼʱ�䰴ԤԼʱ������ԭ��ɸѡ
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