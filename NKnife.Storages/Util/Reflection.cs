using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NKnife.Storages.SQL.Exceptions;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.ForeignKeyAttribute;

namespace NKnife.Storages.Util
{
    public static class Reflection
    {
        public static IEnumerable<A> GetAttributes<T, A>()
        {
            var type = typeof(T);
            foreach (A attr in type.GetCustomAttributes(typeof(A), false)) yield return attr;
        }

        public static string GetTableName<T>()
        {
            var result = typeof(T).Name;
            foreach (TableAttribute a in typeof(T).GetCustomAttributes(typeof(TableAttribute), false))
            {
                result = a.Name;
                break;
            }

            return result;
        }

        public static string GetPrimaryKey<T>()
        {
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            foreach (PrimaryKeyAttribute pk in property.GetCustomAttributes(typeof(PrimaryKeyAttribute), false))
                return property.Name;

            throw new PrimaryKeyNotFoundException();
        }

        public static string[] GetForeignKeys<T>()
        {
            var type = typeof(T);
            var result = new List<string>();
            foreach (var property in type.GetProperties())
            foreach (ForeignKeyAttribute pk in property.GetCustomAttributes(typeof(ForeignKeyAttribute), false))
            {
                result.Add(property.Name);
                break;
            }

            return result.ToArray();
        }
    }
}