using SerialKnife.Abstracts;
using SerialKnife.Common;

namespace SerialKnife.Interfaces
{
    /// <summary>����ͨѶ������
    /// </summary>
    public interface ISerialClientManager
    {
        /// <summary>���һ������
        /// </summary>
        /// <param name="port">����һ���������</param>
        /// <param name="serialType"></param>
        /// <param name="enableDetialLog"></param>
        /// <returns></returns>
        bool AddPort(ushort port, SerialType serialType = SerialType.WinApi, bool enableDetialLog = false);

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