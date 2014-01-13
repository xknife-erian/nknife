using System;
namespace Gean
{
    /// <summary>
    /// 一个描述应用程序服务的接口
    /// </summary>
    public interface IService<in T>
    {
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="args">初始化服务携带的参数.</param>
        /// <returns></returns>
        bool Initializes(params T[] args);

        /// <summary>
        /// 重新启动服务。先初始化，再执行Start.
        /// </summary>
        /// <param name="args">初始化服务携带的参数.</param>
        /// <returns></returns>
        bool ReStart(params T[] args);

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        bool Start();

        /// <summary>
        /// 终止服务
        /// </summary>
        /// <returns></returns>
        bool Stop();
    }
}
