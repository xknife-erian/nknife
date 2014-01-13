using System;
using System.Data;
using System.Linq;
using Gean.Attributes;
using NLog;

namespace NKnife.Extensions
{
    public static class DataExtensions
    {
        /// <summary>像Linq to DataSet中得到字段值的操作
        /// </summary>
        public static T Field<T>(this IDataRecord record, string fieldName)
        {
            T fieldValue = default(T);
            for (int i = 0; i < record.FieldCount; i++)
            {
                if (String.Equals(record.GetName(i), fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    if (record[i] != DBNull.Value)
                    {
                        var value = record[fieldName];
                        fieldValue = (T) value;
                    }
                }
            }
            return fieldValue;
        }

        private static readonly NLog.Logger _Logger = LogManager.GetCurrentClassLogger();

        /// <summary>使用指定对象的数据填充DataRow
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row">The row.</param>
        /// <param name="obj">The obj.</param>
        public static void From<T>(this DataRow row, T obj) where T : class, new()
        {
            var pes = obj.GetType().GetProperties();
            foreach (var propertyInfo in pes) //获取所有的属性
            {
                var pattrs = propertyInfo.GetCustomAttributes(false); //获取所有的定制特性
                if (pattrs.OfType<EntityColumnAttribute>().Any()) //如果该属性的定制特性包含指定的特性
                {
                    string columnName;
                    var pa = pattrs.OfType<EntityColumnAttribute>().FirstOrDefault();
                    if (pa != null && !String.IsNullOrWhiteSpace(pa.ColumnName))
                        columnName = pa.ColumnName;
                    else
                        columnName = propertyInfo.Name; //使用属性名作为列名
                    if (!row.Table.Columns.Contains(columnName)) //如果列中不包含属性名
                    {
                        var column = new DataColumn
                                         {
                                             ColumnName = columnName,
                                             DataType = propertyInfo.PropertyType
                                         };
                        row.Table.Columns.Add(column);
                        _Logger.Warn(String.Format("添加列。属性:{0} 没有找到对应数据列,对应表:{1}", propertyInfo.Name, row.Table.TableName));
                    }
                    var value = propertyInfo.GetValue(obj, null) ?? DBNull.Value;
                    row[columnName] = value; //反射调用属性值填充
                }
            }
        }

        /// <summary>
        /// 从数据行中获取数据创建对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newobj"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T ToEntity<T>(this DataRow row, T newobj) where T : class, new()
        {
            var pes = typeof(T).GetProperties();
            foreach (DataColumn dataColumn in row.Table.Columns)
            {
                bool hasColumn = false;
                foreach (var propertyInfo in pes)
                {
                    var pattrs = propertyInfo.GetCustomAttributes(false); //获取所有的定制特性
                    EntityColumnAttribute pa = null;
                    if (pattrs.OfType<EntityColumnAttribute>().Any()) //如果该属性的定制特性包含指定的特性
                    {
                        pa = pattrs.OfType<EntityColumnAttribute>().FirstOrDefault();
                    }
                    if (propertyInfo.Name.Equals(dataColumn.ColumnName) ||
                        (pa != null && !String.IsNullOrWhiteSpace(pa.ColumnName) && pa.ColumnName.Equals(dataColumn.ColumnName)))
                    {
                        var value = row[dataColumn];
                        if (value is DBNull)
                            value = null;
                        try
                        {
                            if (propertyInfo.PropertyType == typeof(Guid))
                            {
                                if (value != null && !(value is Guid)) 
                                    propertyInfo.SetValue(newobj, Guid.Parse(value.ToString()), null);
                            }
                            else
                            {
                                propertyInfo.SetValue(newobj, value, null);
                            }
                        }
                        catch (Exception e)
                        {
                            _Logger.WarnE(String.Format("属性:{0}赋值失败。{1}", propertyInfo.Name, row.Table.TableName), e);
                        }
                        hasColumn = true;
                        break;
                    }
                }
                if (!hasColumn)
                {
                    _Logger.Warn(String.Format("数据行中的列:{0}没有找到对应的属性", dataColumn.ColumnName));
                }
            }
            return newobj;
        }

        /// <summary>通过<see cref="EntityColumnAttribute"/>定制特性返回该对象持久化为<see cref="DataTable"/>时的表结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static DataTable GetTableSchema<T>(this T obj) where T : class, new()
        {
            var table = new DataTable(String.Format("{0}Table", typeof(T).Name));
            var type = typeof(T);
            var propertieInfos = type.GetProperties();
            foreach (var propertyInfo in propertieInfos) //获取所有的属性
            {
                object[] pattrs = propertyInfo.GetCustomAttributes(false); //获取所有的定制特性
                if (pattrs.OfType<EntityColumnAttribute>().Any()) //如果该属性的定制特性包含指定的特性
                {
                    DataColumn column;
                    var pa = pattrs.OfType<EntityColumnAttribute>().FirstOrDefault();
                    var columnType = propertyInfo.PropertyType == typeof(DateTime?) ? typeof(DateTime) : propertyInfo.PropertyType;
                    if (pa != null && !String.IsNullOrWhiteSpace(pa.ColumnName))
                        column = new DataColumn(pa.ColumnName, columnType);
                    else
                        column = new DataColumn(propertyInfo.Name, columnType);
                    if (!table.Columns.Contains(column.ColumnName))
                        table.Columns.Add(column);
                }
            }
            return table;
        }

        /// <summary>将列的数据结构相关属性复制到克隆的对象中，并返回这个克隆的对象。
        /// </summary>
        public static DataColumn Clone(this DataColumn column)
        {
            var c = new DataColumn
            {
                AllowDBNull = column.AllowDBNull,
                AutoIncrement = column.AutoIncrement,
                AutoIncrementSeed = column.AutoIncrementSeed,
                AutoIncrementStep = column.AutoIncrementStep,
                Caption = column.Caption,
                ColumnMapping = column.ColumnMapping,
                ColumnName = column.ColumnName,
                DataType = column.DataType,
                DateTimeMode = column.DateTimeMode,
                DefaultValue = column.DefaultValue,
                Expression = column.Expression,
                MaxLength = column.MaxLength,
                Namespace = column.Namespace,
                Prefix = column.Prefix,
                ReadOnly = column.ReadOnly,
                Site = column.Site,
                Unique = column.Unique
            };
            return c;
        }
    }
}
