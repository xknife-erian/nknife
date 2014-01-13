using System;
using Gean.Configuring.Option;

namespace Gean.Configuring.Interfaces
{
    /// <summary>
    /// Ӧ�ó���ѡ����Ϣ�Ĺ�����
    /// </summary>
    public interface IOptionManager
    {
        /// <summary>��ǰӦ�ó���ID
        /// </summary>
        /// <value>The name of the curr option group.</value>
        string CurrentClientId { get; }

        /// <summary>��������+������ȡѡ����Ϣ
        /// </summary>
        /// <param name="tablename">ѡ����Ϣ���ڵ���Ϣ��</param>
        /// <param name="key">ѡ���</param>
        /// <returns></returns>
        string GetOptionValue(string tablename, string key);

        /// <summary>��������+������ȡѡ����Ϣ
        /// </summary>
        /// <param name="tablename">ѡ����Ϣ���ڵ���Ϣ��</param>
        /// <param name="key">ѡ���</param>
        /// <returns></returns>
        T GetOptionValue<T>(string tablename, string key) where T : struct;

        /// <summary>��������+������ȡѡ����Ϣ����ͨ����������ѡ����Ϣת����ָ��������
        /// </summary>
        /// <param name="tablename">ѡ����Ϣ���ڵ���Ϣ��</param>
        /// <param name="key">ѡ���</param>
        /// <param name="parser">������</param>
        /// <returns></returns>
        T GetOptionValue<T>(string tablename, string key, Func<object, T> parser);

        /// <summary>��������+��������ѡ����Ϣ
        /// </summary>
        /// <param name="tablename">ѡ����Ϣ���ڵ���Ϣ��</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool SetOptionValue(string tablename, string key, object value);

        // ===========================

        /// <summary>���¼������е�ѡ��ֵ
        /// </summary>
        /// <returns></returns>
        bool ReLoad();

        /// <summary>����ѡ����صĻ�����Ŀ¼��
        /// </summary>
        /// <returns></returns>
        bool Clean();

        /// <summary>����ѡ��洢��
        /// </summary>
        /// <returns></returns>
        object Backup();

        /// <summary>�־û�ѡ����Ϣ
        /// </summary>
        /// <returns></returns>
        bool Save();

        /// <summary>����ʼ����ɺ������¼�
        /// </summary>
        event EventHandler LoadedEvent;

        // ==== OptionSolution ===========================

        /// <summary>��ǰӦ�ó����ѡ����Ϣ�������
        /// </summary>
        /// <value>The name of the curr option group.</value>
        OptionCase.OptionCaseItem CurrentCase { get; set; }

        /// <summary>���½�������ı���(���Ѵ���ʱ�͸��£��������)
        /// </summary>
        void AddOrUpdateCaseStore(OptionCase.OptionCaseItem optionCase);

        /// <summary>ɾ��һ����������ı���
        /// </summary>
        void RemoveCaseStore(OptionCase.OptionCaseItem optionCase);

        /// <summary>��Դ����Ϊģ�帴��һ���µĽ������
        /// </summary>
        /// <param name="srcCase">Դ����.</param>
        OptionCase.OptionCaseItem CopyCaseFrom(OptionCase.OptionCaseItem srcCase);

        /// <summary>����һ�׷���
        /// </summary>
        /// <param name="optionCase">The solution.</param>
        /// <param name="isStore">Trueʱͬʱ�־û�������֮</param>
        void AddCase(OptionCase.OptionCaseItem optionCase, bool isStore);

        /// <summary>ɾ��һ�׷���
        /// </summary>
        /// <param name="optionCase">The solution.</param>
        /// <param name="isStore">Trueʱͬʱ�ӳ־û���ɾ��������֮</param>
        void RemoveCase(OptionCase.OptionCaseItem optionCase, bool isStore);
    }
}