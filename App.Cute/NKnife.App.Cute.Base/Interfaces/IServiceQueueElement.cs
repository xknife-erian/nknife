namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>һ������ָ�����Ϳ���һ���ŶӶ��е��Ⱦ�Ҫ�ء���ҵ�����͡��ͻ�����������Ϊ���е��Ⱦ�Ҫ�ء�
    /// </summary>
    public interface IServiceQueueElement
    {
        /// <summary>��ñ�Ԫ�ص�IdName
        /// </summary>
        /// <returns></returns>
        string Id { get; set; }

        /// <summary>���е�Ԫ���Ƿ��ǿ���״̬���磬ҵ�����Ϳ��ܲ��ڹ���ʱ�䷶Χ֮�ڡ�
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is actived; otherwise, <c>false</c>.
        /// </returns>
        bool IsActived();
    }
}