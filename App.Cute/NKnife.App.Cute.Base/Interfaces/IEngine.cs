using Didaku.Engine.Timeaxis.Base.Interfaces;
using Didaku.Engine.Timeaxis.Base.Interfaces.Services;

namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>一个描述单体组织时间管理模型的引擎接口
    /// </summary>
    /// <remarks></remarks>
    public interface IEngine
    {
        /// <summary>交易统计服务
        /// </summary>
        ICountService CountService { get; }

        /// <summary>引擎的初始化
        /// </summary>
        /// <param name="user">本框架的用户</param>
        /// <param name="option">引擎的选项</param>
        bool Initialize(IUser user, IEngineOption option);
    }
}