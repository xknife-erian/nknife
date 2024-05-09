using System.IO.Ports;

namespace NKnife.Serials
{
    /// <summary>
    ///     �������ò���
    /// </summary>
    public class SerialConfig
    {
        /// <summary>
        ///     ������
        /// </summary>
        public uint BaudRate { get; set; } = 9600;

        /// <summary>
        ///     ֹͣλ
        /// </summary>
        public StopBits StopBit { get; set; } = StopBits.One;

        /// <summary>
        ///     ����λ
        /// </summary>
        public DataBits DataBit { get; set; } = DataBits.Eight;

        public Parity Parity { get; set; } = Parity.None;

        public bool RtsEnable { get; set; } = false;

        public bool DtrEnable { get; set; } = false;

        /// <summary>
        ///     ���������ݵĹ̶���ʱ��
        /// </summary>
        public int ReadTotalTimeoutConstant { get; set; } = 1 * 1000;

        /// <summary>
        ///     д�������ݵĹ̶���ʱ��
        /// </summary>
        public int WriteTotalTimeoutConstant { get; set; } = 1 * 1000;

        /// <summary>
        ///     �¼�����ǰ�ڲ����뻺�����е��ֽ�����Ĭ��ֵΪ 1��
        ///     ֻҪ�ڲ��������д��� ReceivedBytesThreshold ���ֽڵ�ʱ�򣬾ͻ����� DataReceived �¼���
        ///     һ�������������ġ�
        /// </summary>
        public int ReceivedBytesThreshold { get; set; } = 1;

        /// <summary>
        ///     ������������С
        /// </summary>
        public int ReadBufferSize { get; set; } = 64;

        /// <summary>
        ///     �� ReceivedBytesThreshold ���� DataReceived �¼��󣬵ȴ� ReadWait ��ʱ�䣬���������ݽ��յ��׶���ʱ�ٽ��ж�ȡ��Ĭ��ֵ0��
        /// </summary>
        public int ReadWait { get; set; } = 0;

        public static SerialConfig Default => new SerialConfig();
    }
}