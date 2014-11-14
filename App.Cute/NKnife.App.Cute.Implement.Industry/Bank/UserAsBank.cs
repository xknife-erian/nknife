using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Didaku.Engine.Timeaxis.Base.Common;
using Didaku.Engine.Timeaxis.Base.Interfaces;

namespace Didaku.Engine.Timeaxis.Implement.Industry.Bank
{
    /// <summary>当银行行业的营业网点作为用户时。
    /// </summary>
    public class UserAsBank : IUser
    {
        #region Implementation of IUser

        /// <summary>用户ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>用户登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>用户编号，这个编号一般是用户自己设置。
        /// </summary>
        public string Number { get; set; }

        /// <summary>电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>手机号码
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>用户的时间资源的预约动作。
        /// </summary>
        public IActivity BookingActivity { get; set; }

        /// <summary>用户所拥有的时间资源
        /// </summary>
        public IDictionary<string, ITimeaxis> Timeaxises { get; set; }

        /// <summary>用户存在的预约
        /// </summary>
        public Bookings Bookings { get; set; }

        /// <summary>用户的流水线
        /// </summary>
        public IDictionary<string, Pipeline> Pipelines { get; set; }

        /// <summary>分配指定的队列所拥有预约
        /// </summary>
        /// <param name="queueId">指定的队列</param>
        /// <param name="transaction">描述预约的交易 </param>
        /// <returns>是否分配成功</returns>
        public bool Assign(string queueId, out ITransaction transaction)
        {
            transaction = null;
            return true;
        }

        #endregion
    }
}
