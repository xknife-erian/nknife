using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Dapper.DataAnnotations
{
    public class DapperColumnAttributeMapper : DapperTypeMapper
    {
        public DapperColumnAttributeMapper(Type obj)
            : base(new SqlMapper.ITypeMap[]
            {
                new CustomPropertyTypeMap(obj, GetPropertyInfo),
                new DefaultTypeMap(obj)
            })
        {
        }

        private static PropertyInfo GetPropertyInfo(Type type, string columnName)
        {
            var allPropInfo = type.GetProperties();
            var info = allPropInfo.FirstOrDefault(prop => prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName));
            return info;
        }
    }
}