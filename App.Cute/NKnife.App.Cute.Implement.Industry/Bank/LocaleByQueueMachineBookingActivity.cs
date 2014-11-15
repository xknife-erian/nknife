using NKnife.App.Cute.Base.Attributes;
using NKnife.App.Cute.Base.Exceptions;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.App.Cute.Implement.Abstracts;

namespace NKnife.App.Cute.Implement.Industry.Bank
{
    [ActivityImpl(1001, "在银行使用传统排队时，在取号机上进行现场预约的动作")]
    public class LocaleByQueueMachineBookingActivity : BaseBookingActivity
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
                throw new ActiveParamsTypeErrorException(typeof(LocaleByQueueMachineBookingParams), param.GetType());
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

        /// <summary>找到与本Activty匹配的参数类型
        /// </summary>
        /// <returns>一个空的参数类型实体</returns>
        public override IActiveParams Find()
        {
            var param = new LocaleByQueueMachineBookingParams();
            return param;
        }

        #endregion

    }
}