using System;

namespace Didaku.Engine.Timeaxis.Implement.Abstracts
{
    /// <summary>��������
    /// </summary>
    public abstract class BaseCommentingTransaction : BaseTransaction
    {
        /// <summary>����ԤԼ�¼���Ա��ID
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>ʱ����Դӵ���ߵ�ID���������Ŷ���һ���ǹ�̨��
        /// </summary>
        public string TimeaxisId { get; set; }

        /// <summary>��һ��ԤԼ��ԤԼ�����Ľ��׽���������
        /// </summary>
        public BaseComment Comment { get; set; }
    }
}