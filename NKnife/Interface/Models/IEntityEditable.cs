using System;

namespace NKnife.Interface.Models
{
    public interface IEntityEditable<T>
        where T : class, new()
    {
        /// <summary>�Ƿ����½�ʵ��
        /// </summary>
        bool IsNewBuild { get; set; }

        /// <summary>��ȡ��ǰ��ʵ�����
        /// </summary>
        T Entity { get; set; }

        /// <summary>��ʵ�巢���仯ʱ���¼�
        /// </summary>
        event EventHandler EntityChangedEvent;
    }
}