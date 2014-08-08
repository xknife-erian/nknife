using NKnife.Configuring.Option;

namespace NKnife.Configuring.Interfaces
{
    public interface IOptionDataTableSchema
    {
        /// <summary>ѡ��ֵ��Ӧ�ñ����ڵ�DataTable������
        /// </summary>
        /// <value>The name of the table.</value>
        string TableName { get; }
        /// <summary>ѡ��ֵ��Ӧ�ñ����ڵ�DataTable��ģ��
        /// </summary>
        /// <value>The data table schema.</value>
        OptionDataTable OptionDataTableSchema { get; }
    }
}