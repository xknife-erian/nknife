﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Dapper
{
    /// <summary>
    ///     Dapper extensions.
    ///     https://www.cnblogs.com/zhaozhengyan/p/ecommon_dapper.html
    /// </summary>
    public static class DapperExtensions
    {
        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _ParamCache = new ConcurrentDictionary<Type, List<PropertyInfo>>();

        /// <summary>
        ///     Insert data into table.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static long Insert(this IDbConnection connection, dynamic data, string table, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var properties = GetProperties(obj);
            var columns = string.Join(",", properties);
            var values = string.Join(",", properties.Select(p => "@" + p));
            var sql = $"insert into {table} ({columns}) values ({values})";

            return connection.ExecuteScalar<long>(sql, obj, transaction, commandTimeout);
        }

        /// <summary>
        ///     Insert data async into table.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<long> InsertAsync(this IDbConnection connection, dynamic data, string table, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var properties = GetProperties(obj);
            var columns = string.Join(",", properties);
            var values = string.Join(",", properties.Select(p => "@" + p));
            var sql = $"insert into {table} ({columns}) values ({values})";

            return connection.ExecuteScalarAsync<long>(sql, obj, transaction, commandTimeout);
        }

        /// <summary>
        ///     Update data for table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int Update(this IDbConnection connection, dynamic data, dynamic condition, string table, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var conditionObj = condition as object;

            var updatePropertyInfos = GetPropertyInfos(obj);
            var wherePropertyInfos = GetPropertyInfos(conditionObj);

            var updateProperties = updatePropertyInfos.Select(p => p.Name);
            var whereProperties = wherePropertyInfos.Select(p => p.Name);

            var updateFields = string.Join(",", updateProperties.Select(p => p + " = @" + p));
            var whereFields = string.Empty;

            if (whereProperties.Any()) 
                whereFields = " where " + string.Join(" and ", whereProperties.Select(p => p + " = @w_" + p));

            var sql = $"update {table} set {updateFields}{whereFields}";

            var parameters = new DynamicParameters(data);
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;
            wherePropertyInfos.ForEach(p => expandoObject.Add("w_" + p.Name, p.GetValue(conditionObj, null)));
            parameters.AddDynamicParams(expandoObject);

            return connection.Execute(sql, parameters, transaction, commandTimeout);
        }

        /// <summary>
        ///     Update data async for table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> UpdateAsync(this IDbConnection connection, dynamic data, dynamic condition, string table, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var obj = data as object;
            var conditionObj = condition as object;

            var updatePropertyInfos = GetPropertyInfos(obj);
            var wherePropertyInfos = GetPropertyInfos(conditionObj);

            var updateProperties = updatePropertyInfos.Select(p => p.Name);
            var whereProperties = wherePropertyInfos.Select(p => p.Name);

            var updateFields = string.Join(",", updateProperties.Select(p => p + " = @" + p));
            var whereFields = string.Empty;

            if (whereProperties.Any()) 
                whereFields = " where " + string.Join(" and ", whereProperties.Select(p => p + " = @w_" + p));

            var sql = $"update {table} set {updateFields}{whereFields}";

            var parameters = new DynamicParameters(data);
            var expandoObject = new ExpandoObject() as IDictionary<string, object>;
            wherePropertyInfos.ForEach(p => expandoObject.Add("w_" + p.Name, p.GetValue(conditionObj, null)));
            parameters.AddDynamicParams(expandoObject);

            return connection.ExecuteAsync(sql, parameters, transaction, commandTimeout);
        }

        /// <summary>
        ///     Delete data from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int Delete(this IDbConnection connection, dynamic condition, string table, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var whereFields = string.Empty;
            var whereProperties = GetProperties(conditionObj);
            if (whereProperties.Count > 0) 
                whereFields = " where " + string.Join(" and ", whereProperties.Select(p => p + " = @" + p));

            var sql = $"delete from {table}{whereFields}";

            return connection.Execute(sql, conditionObj, transaction, commandTimeout);
        }

        /// <summary>
        ///     Delete data async from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> DeleteAsync(this IDbConnection connection, dynamic condition, string table, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var whereFields = string.Empty;
            var whereProperties = GetProperties(conditionObj);
            if (whereProperties.Count > 0) whereFields = " where " + string.Join(" and ", whereProperties.Select(p => p + " = @" + p));

            var sql = $"delete from {table}{whereFields}";

            return connection.ExecuteAsync(sql, conditionObj, transaction, commandTimeout);
        }

        /*
        /// <summary>
        ///     Get data count from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static int GetCount(this IDbConnection connection, object condition, string table, bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryList<int>(connection, condition, table, "COUNT(*)", isOr, transaction, commandTimeout).Single();
        }

        /// <summary>
        ///     Get data count async from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<int> GetCountAsync(this IDbConnection connection, object condition, string table, bool isOr = false, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryListAsync<int>(connection, condition, table, "COUNT(*)", isOr, transaction, commandTimeout).ContinueWith(t => t.Result.Single());
        }
        */

        /// <summary>
        ///     Query a list of data from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> QueryList(this IDbConnection connection, dynamic condition, string table, string columns = "*", bool isOr = false, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            return QueryList<dynamic>(connection, condition, table, columns, isOr, transaction, commandTimeout);
        }

        /// <summary>
        ///     Query a list of data async from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> QueryListAsync(this IDbConnection connection, dynamic condition, string table, string columns = "*", bool isOr = false,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return QueryListAsync<dynamic>(connection, condition, table, columns, isOr, transaction, commandTimeout);
        }

        /// <summary>
        ///     Query a list of data from table with specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryList<T>(this IDbConnection connection, object condition, string table, string columns = "*", bool isOr = false, IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            return connection.Query<T>(BuildQuerySql(condition, table, columns, isOr), condition, transaction, true, commandTimeout);
        }

        /// <summary>
        ///     Query a list of data async from table with specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> QueryListAsync<T>(this IDbConnection connection, object condition, string table, string columns = "*", bool isOr = false,
            IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return connection.QueryAsync<T>(BuildQuerySql(condition, table, columns, isOr), condition, transaction, commandTimeout);
        }

        private static string BuildQuerySql(dynamic condition, string table, string selectPart = "*", bool isOr = false)
        {
            var conditionObj = condition as object;
            var properties = GetProperties(conditionObj);
            if (properties.Count == 0) return string.Format("SELECT {1} FROM {0}", table, selectPart);

            var separator = isOr ? " OR " : " AND ";
            var wherePart = string.Join(separator, properties.Select(p => p + " = @" + p));

            return string.Format("SELECT {2} FROM {0} WHERE {1}", table, wherePart, selectPart);
        }

        private static List<string> GetProperties(object obj)
        {
            if (obj == null) 
                return new List<string>();
            if (obj is DynamicParameters parameters) 
                return parameters.ParameterNames.ToList();
            return GetPropertyInfos(obj).Select(x => x.Name).ToList();
        }

        private static List<PropertyInfo> GetPropertyInfos(object obj)
        {
            if (obj == null) 
                return new List<PropertyInfo>();

            var t = obj.GetType();
            if (!_ParamCache.TryGetValue(t, out var properties))
            {
                properties = t.GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
                _ParamCache[t] = properties;
            }
            return properties;
        }
    }
}