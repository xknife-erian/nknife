using System;

namespace NKnife.Configuring.Common
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class OptionPanelAttribute : Attribute, IComparable
    {
        /// <summary>����һ��ѡ����������
        /// </summary>
        /// <param name="name">�������������.</param>
        /// <param name="panel">����ʵ������</param>
        /// <param name="plugin">��������Ĳ��(��ΪNullʱ����ѡ��Ϊ����ѡ��������).</param>
        /// <param name="precondition">����Ƿ���ص��Ⱦ�����</param>
        /// <param name="iconIndex">����ͼ�������ֵ.</param>
        /// <param name="order">���������б��е�˳��.</param>
        /// <param name="group">������ص���.</param>
        /// <param name="description">��������.</param>
        /// <param name="enable">if set to <c>true</c> ��������ã���������(������).</param>
        public OptionPanelAttribute(string name, Type panel, Type plugin, Type precondition, ushort iconIndex = 0, int order = 0, string @group = "BASE", string description = "", bool enable = true)
        {
            Name = name;
            Panel = panel;
            Precondition = precondition;
            Description = string.IsNullOrWhiteSpace(description) ? name : description;
            IconIndex = iconIndex;
            OrderIndex = order;
            Enable = enable;
            Plugin = plugin;
            MenuGroup = group;
        }

        public Type Panel { get; set; }

        public Type Precondition { get; set; }

        /// <summary>���������(ΪNullʱ��ʾΪ����ѡ��)
        /// </summary>
        public Type Plugin { get; set; }

        /// <summary>�Ƿ�����
        /// </summary>
        /// <value>The name of the menu.</value>
        public bool Enable { get; set; }

        /// <summary>��Item��Ϊ�˵�ʱ����ʾ��
        /// </summary>
        /// <value>The name of the menu.</value>
        public string Name { get; set; }

        /// <summary>�˵�����
        /// </summary>
        public string MenuGroup { get; set; }

        /// <summary>��Item��Ϊ�˵�ʱ����ʾ
        /// </summary>
        public string Description { get; set; }

        /// <summary>����ѡ��List�е�Icon
        /// </summary>
        /// <value>
        /// The index of the icon.
        /// </value>
        public ushort IconIndex { get; set; }

        /// <summary>��Item��һ�������е���������Խ�󣬽�����Խ��ǰ
        /// </summary>
        /// <value>The index of the order.</value>
        public int OrderIndex { get; set; }

        #region Implementation of IComparable

        public int CompareTo(object obj)
        {
            var attribute = obj as OptionPanelAttribute;
            if (attribute != null)
            {
                var item = attribute;
                return item.OrderIndex - OrderIndex;
            }
            return 0;
        }

        #endregion
    }
}