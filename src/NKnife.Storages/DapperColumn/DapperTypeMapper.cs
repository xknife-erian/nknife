using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Dapper.DataAnnotations
{
    public class DapperTypeMapper : SqlMapper.ITypeMap
    {
        private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

        public DapperTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
        {
            _mappers = mappers;
        }

        public ConstructorInfo FindConstructor(string[] names, Type[] types)
        {
            foreach (var mapper in _mappers)
                try
                {
                    var result = mapper.FindConstructor(names, types);
                    if (result != null) return result;
                }
                catch (NotImplementedException)
                {
                    /* The CustomPropertyTypeMap only supports a no-args
                     * constructor and throws a not implemented exception.
                     * To work around that, catch and ignore.
                     */
                }

            return null;
        }

        public ConstructorInfo FindExplicitConstructor()
        {
            return _mappers
                .Select(mapper => mapper.FindExplicitConstructor())
                .FirstOrDefault(result => result != null);
        }

        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            foreach (var mapper in _mappers)
                try
                {
                    var result = mapper.GetConstructorParameter(constructor, columnName);
                    if (result != null) return result;
                }
                catch (NotImplementedException)
                {
                    /* The CustomPropertyTypeMap only supports a no-args
                     * constructor and throws a not implemented exception.
                     * To work around that, catch and ignore.
                     */
                }

            return null;
        }

        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            foreach (var mapper in _mappers)
                try
                {
                    var result = mapper.GetMember(columnName);
                    if (result != null) return result;
                }
                catch (NotImplementedException)
                {
                    /* The CustomPropertyTypeMap only supports a no-args
                     * constructor and throws a not implemented exception.
                     * To work around that, catch and ignore.
                     */
                }

            return null;
        }
    }
}