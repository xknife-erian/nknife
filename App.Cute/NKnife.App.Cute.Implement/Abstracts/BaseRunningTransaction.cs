namespace NKnife.App.Cute.Implement.Abstracts
{
    /// <summary>����������ԤԼ�¼��Ľ�����Ϣ
    /// </summary>
    public abstract class BaseRunningTransaction : BaseTransaction
    {
        /// <summary>����ԤԼ�¼���Ա��ID
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>ʱ����Դӵ���ߵ�ID���������Ŷ���һ���ǹ�̨��
        /// </summary>
        public string TimeaxisId { get; set; }
    }
}