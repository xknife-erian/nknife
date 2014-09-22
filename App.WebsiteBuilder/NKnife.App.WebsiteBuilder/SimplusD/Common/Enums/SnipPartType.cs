namespace Jeelu.SimplusD
{
    /// <summary>
    /// ҳ��Ƭ��ɲ��ֵ�����
    /// </summary>
    public enum SnipPartType
    {
        /// <summary>
        /// ����ҳ��Ƭ����ɲ���
        /// </summary>
        None = 0,

        /// <summary>
        /// ��̬���͵���ɲ���(Html����)
        /// </summary>
        Static = 2,

        /// <summary>
        /// �������͵���ɲ���
        /// </summary>
        Navigation = 3,

        /// <summary>
        /// List���͵���ɲ���
        /// </summary>
        List = 4,

        /// <summary>
        /// List����Part�е��ض����ͣ��������ͣ�
        /// </summary>
        ListBox = 5,
        
        /// <summary>
        /// �����������͵���ɲ���
        /// </summary>
        Box = 6,

        /// <summary>
        /// ͨ���������Դ�����SNIP����
        /// </summary>
        Attribute = 7,
        
        /// <summary>
        /// �����
        /// </summary>
        Path = 8
    }
}