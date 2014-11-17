using System;
using System.Collections.Generic;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.Interface;

namespace NKnife.App.Cute.Implement.Environment
{
    /// <summary>标识符生成器的管理池
    /// </summary>
    public class IdentifierGeneratorPool : Dictionary<string, IIdentifierGenerator>, IFeaturePool<string, IIdentifierGenerator>, IInitializer
    {
        public IdentifierGeneratorPool()
        {
            Initialize();
        }

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