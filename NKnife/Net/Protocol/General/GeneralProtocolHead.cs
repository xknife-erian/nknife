namespace NKnife.Net.Protocol.General
{
    public class GeneralProtocolHead : IProtocolHead
    {
        public byte[] Head
        {
            get { return new byte[] { }; }
        }
        //public static MessageBuffer GetPacketMessage(BaseSocketConnection connection, ref byte[] buffer)
        //{
        //    byte[] workBuffer = null;
        //    workBuffer = CryptUtils.EncryptData(connection, buffer);

        //    if (connection.Header != null && connection.Header.Length >= 0)
        //    {
        //        // -----需要消息头！ 
        //        int headerSize = connection.Header.Length + 2;
        //        byte[] result = new byte[workBuffer.Length + headerSize];

        //        int messageLength = result.Length;

        //        // -----消息头！ 
        //        for (int i = 0; i < connection.Header.Length; i++)
        //        {
        //            result[i] = connection.Header[i];
        //        }

        //        // -----长度！ 
        //        result[connection.Header.Length] = Convert.ToByte((messageLength & 0xFF00) >> 8);
        //        result[connection.Header.Length + 1] = Convert.ToByte(messageLength & 0xFF);
        //        Array.Copy(workBuffer, 0, result, headerSize, workBuffer.Length);

        //        return new MessageBuffer(ref buffer, ref result);
        //    }
        //    else
        //    {
        //        // -----无消息头！ 
        //        return new MessageBuffer(ref buffer, ref workBuffer);
        //    }
        //}
    }
}
