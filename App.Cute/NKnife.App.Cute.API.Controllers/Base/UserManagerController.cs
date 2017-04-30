using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AttributeRouting.Web.Http;
using NKnife.App.Cute.Base.Common;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.App.Cute.Implement.Environment;
using NKnife.App.Cute.Kernel.IoC;
using NKnife.IoC;

namespace NKnife.App.Cute.API.Controllers.Base
{
    public class UserManagerController : ApiController
    {
        [POST("User/List")]
        public JqGridResult List()
        {
            var json = new JqGridResult();
            var users = DI.Get<UserPool>().Values;
            if (users.Count <= 0)
            {
                UserPoolDemo();
            }
            //json.Data = users;
            json.records = users.Count();
            json.total = json.records / 10;
            return json;
        }

        static void UserPoolDemo()
        {
            for (int i = 0; i < 5; i++)
            {
                var user = new UserAsBank();
                user.Id = Guid.NewGuid().ToString("N").ToUpper();
                user.LoginName = user.Id.Substring(2, 10);
                user.Name = user.Id.Substring(5, 10);
                user.Number = user.Id.Substring(10, 18);
                user.Email = user.Id.Substring(7, 15) + "@icbc.com.cn";
                user.MobilePhone = user.Id.Substring(4, 15);
                DI.Get<UserPool>().Add(user.Id, user);
            }
        }

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
}
