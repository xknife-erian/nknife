using System.Collections.Generic;
using System.Linq;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Base.Common
{
    /// <summary>用户存在的预约
    /// </summary>
    public class Bookings : List<ITransaction>
    {
        public ITransaction FirstByTime(string queueId)
        {
            return (from tran in this orderby tran.Time select tran).FirstOrDefault();
        }
    }
}