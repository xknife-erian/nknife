using System.Collections.Generic;
using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Implement.Abstracts
{
    /// <summary>����һ��ʱ���ᡣ���Ǳ�ϵͳ��������ؼ��ĸ������������һ����Ҫ����ʱ��������ʵ���ڣ��磺��̨���ˣ���������̨�ȵȡ�
    /// </summary>
    public abstract class BaseTimeaxis : ITimeaxis
    {
        #region Implementation of ITimeaxis

        /// <summary>����Ķ����߼�<see cref="IServiceLogic"/>�洢����
        /// [<see cref="IServiceLogic"/>]�����ȼ���[<see cref="IServiceLogic"/>]��List�е�˳�������
        /// </summary>
        public IList<IServiceLogic> Logics { get; set; }

        #endregion

        #region Implementation of ICloneable

        public object Clone()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}