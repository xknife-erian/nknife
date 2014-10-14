namespace SocketKnife.Interfaces
{
    /// <summary>一个将接收到的消息字节数组转换成字符串的工具接口
    /// </summary>
    public interface IDatagramDecoder
    {
        /// <summary>
        /// 解码。将字节数组解析成字符串。
        /// </summary>
        /// <param name="data">需解码的字节数组.</param>
        /// <param name="done">已完成解码的数组的长度.</param>
        /// <returns></returns>
        string[] Execute(byte[] data, out int done);
    }
}
