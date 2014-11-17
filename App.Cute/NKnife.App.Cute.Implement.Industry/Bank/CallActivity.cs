using NKnife.App.Cute.Base.Attributes;
using NKnife.App.Cute.Base.Exceptions;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.App.Cute.Implement.Abstracts;

namespace NKnife.App.Cute.Implement.Industry.Bank
{
    /// <summary>������ʹ�ô�ͳ�Ŷ�ʱ�����ú��������к��еĶ���
    /// </summary>
    [ActivityImpl(2001, "������ʹ�ô�ͳ�Ŷ�ʱ�����ú��������к��еĶ���")]
    public class CallActivity : BaseRunningActivity
    {
        #region Overrides of BaseActivity

        /// <summary>�򱾻��������
        /// </summary>
        /// <param name="param">��Ĳ���</param>
        /// <param name="transaction">������Ľ�����Ϣ</param>
        /// <returns>�����Ƿ�ɹ�</returns>
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

        /// <summary>���ݲ�����������ʱ�Ĳ���
        /// </summary>
        public override IActiveParams Find()
        {
            var param = new LocaleByQueueMachineBookingParams();
            return param;
        }

        #endregion
    }
}