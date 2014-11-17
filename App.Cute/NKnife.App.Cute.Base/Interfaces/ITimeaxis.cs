using System;
using System.Collections.Generic;

namespace NKnife.App.Cute.Base.Interfaces
{
    /// <summary>������Ķ���:ʱ���ᡣ���Ǳ�ϵͳ��������ؼ��ĸ������������һ����Ҫ����ʱ��������ʵ���ڣ��磺��̨���ˣ���������̨�ȵȡ�
    /// </summary>
    public interface ITimeaxis : ICloneable
    {
        /// <summary>����Ķ����߼�<see cref="IServiceLogic"/>�洢����
        /// [<see cref="IServiceLogic"/>]�����ȼ���[<see cref="IServiceLogic"/>]��List�е�˳�������
        /// </summary>
        IList<IServiceLogic> Logics { get; set; }
    }
}