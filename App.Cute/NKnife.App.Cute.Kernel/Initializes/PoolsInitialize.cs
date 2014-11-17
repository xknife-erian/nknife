using System;
using Didaku.Attributes;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using Didaku.Engine.Timeaxis.Implement.Environment;
using Didaku.Engine.Timeaxis.Kernel.Configurations;
using Didaku.Interface;
using NLog;

namespace Didaku.Engine.Timeaxis.Kernel.Initializes
{
    /// <summary>关键特征池的管理池
    /// </summary>
    [EnvironmentItem(220, "关键特征池的管理池。")]
    class PoolsInitialize : IInitializer
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IInitializer

        /// <summary>是否已经初始化
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>执行初始化动作
        /// </summary>
        /// <param name="args">初始化的动作参数</param>
        public bool Initialize(params object[] args)
        {
            try
            {
                if (!IsInitialized)
                {
                    Pools.Instance.Activities = (IFeaturePool<IActivity>)Activator.CreateInstance(PoolsOption.Instance.Activities);
                    Pools.Instance.IdentifierGenerators = (IFeaturePool<IIdentifierGenerator>)Activator.CreateInstance(PoolsOption.Instance.IdentifierGenerators);
                    Pools.Instance.Users = (IFeaturePool<IUser>)Activator.CreateInstance(PoolsOption.Instance.Users);
                    Pools.Instance.ServiceQueues = (IFeaturePool<IServiceQueue>)Activator.CreateInstance(PoolsOption.Instance.ServiceQueues);
                    IsInitialized = true;
                }
                return true;
            }
            catch (Exception e)
            {
                _Logger.WarnException("启动关键特征池的管理池异常", e);
                return false;
            }
        }

        #endregion
    }
}