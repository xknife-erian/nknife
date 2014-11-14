using Didaku.Engine.Timeaxis.Base.Attributes;
using Didaku.Engine.Timeaxis.Base.Exceptions;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using Didaku.Engine.Timeaxis.Implement.Abstracts;

namespace Didaku.Engine.Timeaxis.Implement.Industry.Bank
{
    /// <summary>在银行使用传统排队时，采用呼叫器进行呼叫的动作
    /// </summary>
    [ActivityImpl(9001, "在银行使用传统排队时，采用评价器邀请客户进行评价的动作")]
    public class FinishedActivity : BaseRunningActivity
    {
        #region Overrides of BaseActivity

        /// <summary>向本活动发出请求。
        /// </summary>
        /// <param name="param">活动的参数</param>
        /// <param name="transaction">活动关联的交易信息</param>
        /// <returns>请求是否成功</returns>
        public override bool Ask<T>(T param, out ITransaction transaction)
        {
            if (!(param is LocaleByQueueMachineBookingParams))
                throw new ActiveParamsTypeErrorException(typeof (LocaleByQueueMachineBookingParams), param.GetType());
            return InnerAsk(param as LocaleByQueueMachineBookingParams, out transaction);
        }

        private bool InnerAsk(LocaleByQueueMachineBookingParams param, out ITransaction transaction)
        {
            transaction = new TicketByQueueMachineTransaction
                              {
                                  User = param.Asker,
                                  Queue = param.QueueId,
                                  Identifier = ""
                              };
            return true;
        }

        /// <summary>根据参数创建请求时的参数
        /// </summary>
        public override IActiveParams Find()
        {
            var param = new LocaleByQueueMachineBookingParams();
            return param;
        }

        #endregion
    }
}