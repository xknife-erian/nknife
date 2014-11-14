using Didaku.Engine.Timeaxis.Base.Attributes;
using Didaku.Engine.Timeaxis.Base.Exceptions;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using Didaku.Engine.Timeaxis.Implement.Abstracts;

namespace Didaku.Engine.Timeaxis.Implement.Industry.Bank
{
    /// <summary>������ʹ�ô�ͳ�Ŷ�ʱ�����ú��������к��еĶ���
    /// </summary>
    [ActivityImpl(9001, "������ʹ�ô�ͳ�Ŷ�ʱ����������������ͻ��������۵Ķ���")]
    public class FinishedActivity : BaseRunningActivity
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