using System;
using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using NKnife.Interface;

namespace Didaku.Engine.Timeaxis.Implement.Environment
{
    /// <summary>本系统的应用者(应用者可以是:商家，企业，组织，个人等)管理池
    /// </summary>
    public class UserPool : Dictionary<string, IUser>, IFeaturePool<string, IUser>, IInitializer
    {
        #region Implementation of IInitializer

        /// <summary>是否已经初始化
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>执行初始化动作
        /// </summary>
        /// <param name="args">初始化的动作参数</param>
        public bool Initialize(params object[] args)
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                OnInitialized(EventArgs.Empty);
            }
            return true;
        }

        /// <summary>初始化完成时发生的事件
        /// </summary>
        public event EventHandler InitializedEvent;

        protected virtual void OnInitialized(EventArgs e)
        {
            if (InitializedEvent != null)
                InitializedEvent(this, e);
        }

        #endregion

    }
}