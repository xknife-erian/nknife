using System.Collections.Generic;
using MongoDB.Driver;
using NKnife.Database;
using NKnife.Database.Interface;

namespace NKnife.MongoDb
{
    /// <summary>
    ///     基于MongoDb的一个数据类型存储器
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="TId">实体的Id的类型</typeparam>
    public interface IMongoDbStore<T, in TId> : IDbStore<T, TId>
    {
        /// <summary>
        ///     连接字符串
        /// </summary>
        string Connection { get; }

        /// <summary>
        ///     数据库名
        /// </summary>
        string Database { get; }

        /// <summary>
        ///     数据集合名称
        /// </summary>
        string Collection { get; }

        /// <summary>
        ///     删除指定条件的记录
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        bool Delete(IMongoQuery query);

        /// <summary>
        ///     按指定的查询条件获取实体数量
        /// </summary>
        long Count(IMongoQuery mongoQuery);

        /// <summary>
        ///     查找指定的实体记录
        /// </summary>
        /// <param name="mongoQuery">
        ///     条件查询。
        ///     调用示例：
        ///     <code>
        ///    Query.All("name", "a", "b");//通过多个元素来匹配数组
        ///    Query.And(Query.EQ("name", "a"), Query.EQ("title", "t"));//同时满足多个条件
        ///    Query.EQ("name", "a");//等于
        ///    Query.Exists("type", true);//判断键值是否存在
        ///    Query.GT("value", 2);//大于>
        ///    Query.GTE("value", 3);//大于等于>=
        ///    Query.In("name", "a", "b");//包括指定的所有值,可以指定不同类型的条件和值
        ///    Query.LT("value", 9);//小于
        ///    Query.LTE("value", 8);//小于等于
        ///    Query.Mod("value", 3, 1);//将查询值除以第一个给定值,若余数等于第二个给定值则返回该结果
        ///    Query.NE("name", "c");//不等于
        ///    Query.Nor(Array);//不包括数组中的值
        ///    Query.Not("name");//元素条件语句
        ///    Query.NotIn("name", "a", 2);//返回与数组中所有条件都不匹配的文档
        ///    Query.Or(Query.EQ("name", "a"), Query.EQ("title", "t"));//满足其中一个条件
        ///    Query.Size("name", 2);//给定键的长度
        ///    Query.Type("_id", BsonType.ObjectId );//给定键的类型
        ///    Query.Where(BsonJavaScript);//执行JavaScript
        ///    Query.Matches("Title",str);//模糊查询 相当于sql中like  -- str可包含正则表达式
        /// </code>
        /// </param>
        IEnumerable<T> Find(IMongoQuery mongoQuery);

        /// <summary>
        ///     查找指定的实体记录
        /// </summary>
        /// <param name="mongoQuery">
        ///     条件查询。
        ///     调用示例：
        ///     <code>
        ///    Query.All("name", "a", "b");//通过多个元素来匹配数组
        ///    Query.And(Query.EQ("name", "a"), Query.EQ("title", "t"));//同时满足多个条件
        ///    Query.EQ("name", "a");//等于
        ///    Query.Exists("type", true);//判断键值是否存在
        ///    Query.GT("value", 2);//大于>
        ///    Query.GTE("value", 3);//大于等于>=
        ///    Query.In("name", "a", "b");//包括指定的所有值,可以指定不同类型的条件和值
        ///    Query.LT("value", 9);//小于
        ///    Query.LTE("value", 8);//小于等于
        ///    Query.Mod("value", 3, 1);//将查询值除以第一个给定值,若余数等于第二个给定值则返回该结果
        ///    Query.NE("name", "c");//不等于
        ///    Query.Nor(Array);//不包括数组中的值
        ///    Query.Not("name");//元素条件语句
        ///    Query.NotIn("name", "a", 2);//返回与数组中所有条件都不匹配的文档
        ///    Query.Or(Query.EQ("name", "a"), Query.EQ("title", "t"));//满足其中一个条件
        ///    Query.Size("name", 2);//给定键的长度
        ///    Query.Type("_id", BsonType.ObjectId );//给定键的类型
        ///    Query.Where(BsonJavaScript);//执行JavaScript
        ///    Query.Matches("Title",str);//模糊查询 相当于sql中like  -- str可包含正则表达式
        /// </code>
        /// </param>
        /// <param name="pagerInfo">分页的相关参数。</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">查询条件未定义;mongoQuery</exception>
        IEnumerable<T> Find(IMongoQuery mongoQuery, PagerInfo pagerInfo);

        /// <summary>
        ///     查找指定的实体记录
        /// </summary>
        /// <param name="mongoQuery">
        ///     条件查询。
        ///     调用示例：
        ///     <code>
        ///    Query.All("name", "a", "b");//通过多个元素来匹配数组
        ///    Query.And(Query.EQ("name", "a"), Query.EQ("title", "t"));//同时满足多个条件
        ///    Query.EQ("name", "a");//等于
        ///    Query.Exists("type", true);//判断键值是否存在
        ///    Query.GT("value", 2);//大于>
        ///    Query.GTE("value", 3);//大于等于>=
        ///    Query.In("name", "a", "b");//包括指定的所有值,可以指定不同类型的条件和值
        ///    Query.LT("value", 9);//小于
        ///    Query.LTE("value", 8);//小于等于
        ///    Query.Mod("value", 3, 1);//将查询值除以第一个给定值,若余数等于第二个给定值则返回该结果
        ///    Query.NE("name", "c");//不等于
        ///    Query.Nor(Array);//不包括数组中的值
        ///    Query.Not("name");//元素条件语句
        ///    Query.NotIn("name", "a", 2);//返回与数组中所有条件都不匹配的文档
        ///    Query.Or(Query.EQ("name", "a"), Query.EQ("title", "t"));//满足其中一个条件
        ///    Query.Size("name", 2);//给定键的长度
        ///    Query.Type("_id", BsonType.ObjectId );//给定键的类型
        ///    Query.Where(BsonJavaScript);//执行JavaScript
        ///    Query.Matches("Title",str);//模糊查询 相当于sql中like  -- str可包含正则表达式
        /// </code>
        /// </param>
        /// <param name="pagerInfo">分页的相关参数。</param>
        /// <param name="sortBy">
        ///     排序用的。
        ///     调用示例：
        ///     <code>
        /// SortBy.Descending("Title") 
        /// SortBy.Descending("Title").Ascending("Author")
        /// </code>
        /// </param>
        /// <param name="fields">只返回所需要的字段的数据。</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">查询条件未定义;mongoQuery</exception>
        IEnumerable<T> Find(IMongoQuery mongoQuery, PagerInfo pagerInfo, IMongoSortBy sortBy, params string[] fields);
    }
}