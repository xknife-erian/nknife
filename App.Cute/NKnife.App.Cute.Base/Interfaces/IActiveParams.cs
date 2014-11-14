using System;

namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>活动的参数
    /// </summary>
    public interface IActiveParams
    {
        /// <summary>本次活动的请求者，事实上它一般也是本系统的用户，只是在这时做为请求者。
        /// </summary>
        string Asker { get; set; }

        /// <summary>被请求时间资源的本系统用户的ID
        /// </summary>
        string UserId { get; set; }

        /// <summary>队列ID，预约动作将指向该指定队列
        /// </summary>
        string QueueId { get; set; }

        /// <summary>解析传入的参数集合并填充本类型
        /// </summary>
        /// <param name="args"></param>
        IActiveParams Parse(params object[] args);
    }
}