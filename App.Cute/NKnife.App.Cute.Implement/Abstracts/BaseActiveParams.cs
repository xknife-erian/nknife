using System;
using NKnife.App.Cute.Base.Exceptions;
using NKnife.App.Cute.Base.Interfaces;
using NKnife.Utility;

namespace NKnife.App.Cute.Implement.Abstracts
{
    public abstract class BaseActiveParams : IActiveParams
    {
        #region Implementation of IActiveParams

        /// <summary>���λ�������ߣ���ʵ����һ��Ҳ�Ǳ�ϵͳ���û���ֻ������ʱ��Ϊ�����ߡ�
        /// </summary>
        public string Asker { get; set; }

        /// <summary>������ʱ����Դ�ı�ϵͳ�û���ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>����ID��ԤԼ������ָ���ָ������
        /// </summary>
        public string QueueId { get; set; }

        /// <summary>��������Ĳ������ϲ���䱾����
        /// </summary>
        /// <param name="args">����Ĳ�������</param>
        public virtual IActiveParams Pack(params object[] args)
        {
            if (UtilityCollection.IsNullOrEmpty(args) || args.Length != 3)
                throw new ActiveParamsDataErrorException("���������������ʱ���ݲ�����Ҫ��");
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

        /// <summary>��������Ĳ������ϲ���䱾����
        /// </summary>
        /// <param name="args">����Ĳ�������</param>
        protected abstract void Fill(object[] args);

        #endregion
    }
}