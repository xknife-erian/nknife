namespace NKnife.NSerial.Base
{
    /// <summary>����ͨѶ������
    /// </summary>
    public interface ISerialCommunicationManager
    {
        bool Initialize(SerialType serialType = SerialType.API, bool enableDetialLog = false);

        /// <summary>����һ������
        /// </summary>
        /// <param name="port">����һ���������</param>
        /// <returns></returns>
        bool AddPort(ushort port);

        /// <summary>
        /// �ر�һ������
        /// </summary>
        /// <param name="port">����һ���������</param>
        /// <returns></returns>
        bool RemovePort(ushort port);

        /// <summary>�ر����д��ڣ��������д��ڴӹ��������Ƴ�
        /// </summary>
        /// <returns></returns>
        bool RemoveAllPorts();

        /// <summary>��ָ���Ĵ���д�����ݰ�
        /// </summary>
        /// <param name="port">����һ���������</param>
        /// <param name="package">�����������ݣ��Լ����ָ���Ϣ���¼��ķ�װ</param>
        /// <returns></returns>
        bool AddPackage(ushort port, PackageBase package);
    }
}