using System.Collections.Generic;
using System.Linq;
using Didaku.Engine.Timeaxis.Base.Interfaces;

namespace Didaku.Engine.Timeaxis.Base.Common
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