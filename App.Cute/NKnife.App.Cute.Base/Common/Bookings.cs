using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Base.Common
{
    /// <summary>用户存在的预约
    /// </summary>
    public class Bookings : TransactionCollection
    {
        /// <summary>
        /// 找出指定队列的按条件弹出的交易。
        /// </summary>
        /// <param name="queueId">指定队列的ID</param>
        /// <param name="findFunc">找出交易的方法，当该方法为Null时，按时间优先法则</param>
        /// <returns>按条件弹出的交易</returns>
        public ITransaction FirstByTime(string queueId, Func<IList<ITransaction>, ITransaction> findFunc = null)
        {
            if (findFunc != null)
                return findFunc.Invoke(this);
            return (from tran in this orderby tran.Time select tran).FirstOrDefault();
        }
    }
}