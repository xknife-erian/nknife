namespace SerialKnife.Interfaces
{
    /// <summary>���ڲ�����ӿ�
    /// </summary>
    public interface ISerialPortWrapper
    {
        /// <summary>�����Ƿ�򿪱��
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>��ʼ��������ͨѶ����
        /// </summary>
        /// <param name="portName"></param>
        /// <returns></returns>
        bool InitPort(string portName);

        /// <summary> �رմ���
        /// </summary>
        /// <returns></returns>
        bool Close();

        /// <summary>���ô��ڶ�ȡ��ʱ
        /// </summary>
        /// <param name="timeout"></param>
        void SetTimeOut(int timeout);

        /// <summary>��������
        /// </summary>
        /// <param name="cmd">�����͵�����</param>
        /// <param name="recv">�ظ�������</param>
        /// <returns>�ظ������ݵĳ���</returns>
        int SendData(byte[] cmd, out byte[] recv);
    }
}