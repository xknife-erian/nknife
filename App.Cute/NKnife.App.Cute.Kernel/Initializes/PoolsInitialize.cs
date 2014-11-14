using System;
using Didaku.Attributes;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using Didaku.Engine.Timeaxis.Implement.Environment;
using Didaku.Engine.Timeaxis.Kernel.Configurations;
using Didaku.Interface;
using NLog;

namespace Didaku.Engine.Timeaxis.Kernel.Initializes
{
    /// <summary>�ؼ������صĹ����
    /// </summary>
    [EnvironmentItem(220, "�ؼ������صĹ���ء�")]
    class PoolsInitialize : IInitializer
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IInitializer

        /// <summary>�Ƿ��Ѿ���ʼ��
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>ִ�г�ʼ������
        /// </summary>
        /// <param name="args">��ʼ���Ķ�������</param>
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
                _Logger.WarnException("�����ؼ������صĹ�����쳣", e);
                return false;
            }
        }

        #endregion
    }
}