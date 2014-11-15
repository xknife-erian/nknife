using System.Collections.Generic;
using System.Linq;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Base.Common
{
    /// <summary>�û����ڵ�ԤԼ
    /// </summary>
    public class Bookings : List<ITransaction>
    {
        public ITransaction FirstByTime(string queueId)
        {
            return (from tran in this orderby tran.Time select tran).FirstOrDefault();
        }
    }
}