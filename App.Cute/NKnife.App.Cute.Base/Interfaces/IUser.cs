using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Common;

namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>领域核心定义:系统的用户。这个用户的概念比较宽，可以是个人，银行网点，餐馆等等。
    /// </summary>
    public interface IUser
    {
        /// <summary>用户ID
        /// </summary>
        string Id { get; set; }

        /// <summary>用户登录名
        /// </summary>
        string LoginName { get; set; }

        /// <summary>用户名称
        /// </summary>
        string Name { get; set; }

        /// <summary>用户编号，这个编号一般是用户自己设置。
        /// </summary>
        string Number { get; set; }

        /// <summary>电子邮件
        /// </summary>
        string Email { get; set; }

        /// <summary>手机号码
        /// </summary>
        string MobilePhone { get; set; }

        /// <summary>用户的时间资源的预约动作。
        /// </summary>
        IActivity BookingActivity { get; set; }

        /// <summary>用户所拥有的时间资源
        /// </summary>
        IDictionary<string, ITimeaxis> Timeaxises { get; set; }

        /// <summary>用户存在的预约
        /// </summary>
        Bookings Bookings { get; set; }

        /// <summary>用户的流水线
        /// </summary>
        IDictionary<string, Pipeline> Pipelines { get; set; }

        /// <summary>分配指定的队列所拥有预约
        /// </summary>
        /// <param name="queueId">指定的队列</param>
        /// <param name="transaction">描述预约的交易 </param>
        /// <returns>是否分配成功</returns>
        bool Assign(string queueId, out ITransaction transaction);
    }
}