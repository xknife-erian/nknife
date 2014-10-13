namespace SocketKnife.Protocol.Interfaces
{
    /// <summary>
    /// 一个最常用的 字符串 => 字节数组 转换器。
    /// </summary>
    public interface IDatagramEncoder
    {
        bool EnabelCompress { get; set; }
        byte[] Execute(string replay);
    }
}
