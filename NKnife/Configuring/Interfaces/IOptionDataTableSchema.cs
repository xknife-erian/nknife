using System.Data;
using Gean.Configuring.Option;
using Gean.Configuring.Common;

namespace Gean.Configuring.Interfaces
{
    public interface IOptionDataTableSchema
    {
        /// <summary>选项值所应该保存在的DataTable的名字
        /// </summary>
        /// <value>The name of the table.</value>
        string TableName { get; }
        /// <summary>选项值所应该保存在的DataTable的模板
        /// </summary>
        /// <value>The data table schema.</value>
        OptionDataTable OptionDataTableSchema { get; }
    }
}