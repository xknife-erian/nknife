using System;
using System.Collections.Generic;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.Interface;

namespace NKnife.App.Cute.Implement.Environment
{
    /// <summary>��ʶ���������Ĺ����
    /// </summary>
    public class IdentifierGeneratorPool : Dictionary<string, IIdentifierGenerator>, IFeaturePool<string, IIdentifierGenerator>, IInitializer
    {
        public IdentifierGeneratorPool()
        {
            Initialize();
        }

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