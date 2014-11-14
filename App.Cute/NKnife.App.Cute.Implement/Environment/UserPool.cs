using System;
using System.Collections.Generic;
using Didaku.Engine.Timeaxis.Base.Interfaces;
using NKnife.Interface;

namespace Didaku.Engine.Timeaxis.Implement.Environment
{
    /// <summary>��ϵͳ��Ӧ����(Ӧ���߿�����:�̼ң���ҵ����֯�����˵�)�����
    /// </summary>
    public class UserPool : Dictionary<string, IUser>, IFeaturePool<string, IUser>, IInitializer
    {
        #region Implementation of IInitializer

        /// <summary>�Ƿ��Ѿ���ʼ��
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>ִ�г�ʼ������
        /// </summary>
        /// <param name="args">��ʼ���Ķ�������</param>
        public bool Initialize(params object[] args)
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                OnInitialized(EventArgs.Empty);
            }
            return true;
        }

        /// <summary>��ʼ�����ʱ�������¼�
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