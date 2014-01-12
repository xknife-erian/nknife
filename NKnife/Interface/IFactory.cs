namespace NKnife.Interface
{
    /// <summary>
    /// 工厂接口
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// 获取管理器
        /// </summary>
        /// <param name="parameters">初始化管理器需要的参数列表</param>
        /// <returns>管理器</returns>
        object GetManager(params object[] parameters);
    }
}
