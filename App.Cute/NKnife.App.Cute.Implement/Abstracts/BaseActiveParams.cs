using System;
using NKnife.App.Cute.Base.Exceptions;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.Utility;

namespace NKnife.App.Cute.Implement.Abstracts
{
    public abstract class BaseActiveParams : IActiveParams
    {
        #region Implementation of IActiveParams

        /// <summary>本次活动的请求者，事实上它一般也是本系统的用户，只是在这时做为请求者。
        /// </summary>
        public string Asker { get; set; }

        /// <summary>被请求时间资源的本系统用户的ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>队列ID，预约动作将指向该指定队列
        /// </summary>
        public string QueueId { get; set; }

        /// <summary>解析传入的参数集合并填充本类型
        /// </summary>
        /// <param name="args">传入的参数集合</param>
        public virtual IActiveParams Pack(params object[] args)
        {
            if (UtilityCollection.IsNullOrEmpty(args) || args.Length != 3)
                throw new ActiveParamsDataErrorException("构建请求参数类型时数据不符合要求");
            try
            {
                Fill(args);
            }
            catch (Exception e)
            {
                throw new ActiveParamsDataConvertErrorException(e);
            }
            return this;
        }

        /// <summary>解析传入的参数集合并填充本类型
        /// </summary>
        /// <param name="args">传入的参数集合</param>
        protected abstract void Fill(object[] args);

        #endregion
    }
}