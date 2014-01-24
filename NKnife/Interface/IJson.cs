namespace NKnife.Interface
{
    /// <summary>
    /// 实现Json的序列化与反序列化的接口。之所以用这样一个接口，是因为.Net Framework2.0中没有对Json的支持，在3.5中已有对Json的支持。故通过这样的一个接口以反应在 Gean.Json2(.Net2.0) 与 Gean.Json3(.Net3.5) 两个项目的不同.Net版本的实现。
    /// </summary>
    public interface IJson
    {
        /// <summary>
        /// 序列化一个指定的对象
        /// </summary>
        /// <param name="obj">一个指定的对象.</param>
        /// <returns>以 JSON（JavaScript 对象表示法）格式表示的序列化后的字符串</returns>
        string SerializeObject(object obj);

        /// <summary>
        /// 以 JSON（JavaScript 对象表示法）格式读取指定的字符串，并返回反序列化的对象。
        /// </summary>
        /// <typeparam name="T">将返回的反序列化的对象类型</typeparam>
        /// <param name="jsonInput">以 JSON（JavaScript 对象表示法）格式表示的字符串.</param>
        /// <returns>反序列化的对象</returns>
        T DeserializeObject<T>(string jsonInput);
    }
}
