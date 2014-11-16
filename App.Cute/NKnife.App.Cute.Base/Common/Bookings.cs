using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Base.Common
{
    /// <summary>�û����ڵ�ԤԼ
    /// </summary>
    public class Bookings : TransactionCollection
    {
        /// <summary>
        /// �ҳ�ָ�����еİ����������Ľ��ס�
        /// </summary>
        /// <param name="queueId">ָ�����е�ID</param>
        /// <param name="findFunc">�ҳ����׵ķ��������÷���ΪNullʱ����ʱ�����ȷ���</param>
        /// <returns>�����������Ľ���</returns>
        public ITransaction FirstByTime(string queueId, Func<IList<ITransaction>, ITransaction> findFunc = null)
        {
            if (findFunc != null)
                return findFunc.Invoke(this);
            return (from tran in this orderby tran.Time select tran).FirstOrDefault();
        }
    }
}