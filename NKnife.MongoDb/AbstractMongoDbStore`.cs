using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using NKnife.Databases;

namespace NKnife.MongoDb
{
    /// <summary>
    ///     基于MongoDb的一个数据类型存储器的抽象实现
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="TId">实体的Id的类型</typeparam>
    public abstract class AbstractMongoDbStore<T, TId> : IMongoDbStore<T, TId>
    {
        private static readonly ILog _logger = LogManager.GetLogger<AbstractMongoDbStore<T, TId>>();

        private readonly MongoDatabase _Database;
        private readonly MongoServer _Mongo;

        /// <summary>
        ///     构造函数：基于MongoDb的一个数据类型存储器的抽象实现
        /// </summary>
        /// <param name="connection">连接字符串</param>
        /// <param name="database">数据库名称</param>
        /// <param name="collection">数据集合名称</param>
        protected AbstractMongoDbStore(string connection, string database, string collection)
        {
            Connection = connection;
            Database = database;
            Collection = collection;
            _Mongo = MongoServer.Create(Connection); //建立一个Mongo的连接
            _Database = _Mongo.GetDatabase(Database); //获取数据库或者创建数据库（不存在的话）。
        }

        protected virtual IDisposable Connect(out bool isConnectSuccess)
        {
            isConnectSuccess = true;
            try
            {
                return _Mongo.RequestStart(_Database);
            }
            catch (Exception e)
            {
                _logger.WarnFormat("连接出现异常：{0}", e.Message);
                isConnectSuccess = false;
                return null;
            }
        }

        #region Implementation of IDbStore<T,in TId>

        /// <summary>
        ///     增加指定的实体记录
        /// </summary>
        public virtual bool Add(params T[] entities)
        {
            if (null == entities)
                return true;
            bool isConnectSuccess = false;
            using (Connect(out isConnectSuccess))
            {
                if (!isConnectSuccess)
                    return false;
                MongoCollection<BsonDocument> c = _Database.GetCollection<BsonDocument>(Collection);
                WriteConcernResult[] safeResults = c.InsertBatch(entities).ToArray();
                return safeResults[0] != null && safeResults[0].Ok;
            }
        }

        /// <summary>
        ///     删除指定ID的记录
        /// </summary>
        public virtual bool Remove(params TId[] entityIds)
        {
            bool isConnectSuccess = false;
            using (Connect(out isConnectSuccess))
            {
                if (!isConnectSuccess)
                    return false;
                MongoCollection<BsonDocument> c = _Database.GetCollection<BsonDocument>(Collection);
                var safeResults = new List<SafeModeResult>(entityIds.Length);
                safeResults.AddRange(entityIds.Select(id => c.Remove(Query.EQ("_id", id.ToString()))));
                return safeResults[0] != null && safeResults[0].Ok;
            }
        }

        /// <summary>
        ///     删除(清除)所有记录
        /// </summary>
        public virtual bool Clear()
        {
            bool isConnectSuccess = false;
            using (Connect(out isConnectSuccess))
            {
                if (!isConnectSuccess)
                    return false;
                MongoCollection<BsonDocument> c = _Database.GetCollection<BsonDocument>(Collection);
                WriteConcernResult result = c.RemoveAll();
                return result != null && result.Ok;
            }
        }

        /// <summary>
        ///     更新指定的实体记录
        /// </summary>
        public virtual bool Update(params T[] entity)
        {
            bool isConnectSuccess = false;
            using (Connect(out isConnectSuccess))
            {
                if (!isConnectSuccess)
                    return false;
                MongoCollection<BsonDocument> c = _Database.GetCollection<BsonDocument>(Collection);
                WriteConcernResult result = c.Save(entity);
                return result != null && result.Ok;
            }
        }

        /// <summary>
        ///     查询实体数量
        /// </summary>
        public long Count()
        {
            return Count(null);
        }

        /// <summary>
        ///     查找指定的实体记录
        /// </summary>
        public virtual T Find(TId entityId)
        {
            bool isConnectSuccess = false;
            using (Connect(out isConnectSuccess))
            {
                if (!isConnectSuccess)
                    return default(T);
                MongoCollection<BsonDocument> c = _Database.GetCollection<BsonDocument>(Collection);
                var result = c.FindOneAs<T>(Query.EQ("_id", entityId.ToString()));
                return result;
            }
        }

        /// <summary>
        ///     查找指定的实体记录集合
        /// </summary>
        public virtual IEnumerable<T> Find(params TId[] entityIds)
        {
            if (entityIds == null || entityIds.Length <= 0)
                throw new ArgumentException("没有指定需查找的实体ID", "entityIds");

            IMongoQuery query = Query.And(Query.In("_id", BsonArray.Create(entityIds)));
            var result = new List<T>(entityIds.Length);
            bool isConnectSuccess = false;
            using (Connect(out isConnectSuccess))
            {
                if (!isConnectSuccess)
                    return null;
                MongoCollection<BsonDocument> c = _Database.GetCollection<BsonDocument>(Collection);
                MongoCursor<T> cursor = c.FindAs<T>(query);
                result.AddRange(cursor);
                result.TrimExcess();
            }
            return result;
        }

        #endregion

        #region Implementation of IMongoDbStore<T,in TId>

        /// <summary>
        ///     连接字符串
        /// </summary>
        public string Connection { get; private set; }

        /// <summary>
        ///     数据库名
        /// </summary>
        public string Database { get; private set; }

        /// <summary>
        ///     数据集合名称
        /// </summary>
        public string Collection { get; private set; }

        /// <summary>
        ///     删除指定条件的记录
        /// </summary>
        /// <param name="mongoQuery"></param>
        /// <returns></returns>
        public virtual bool Delete(IMongoQuery mongoQuery)
        {
            if (mongoQuery == null)
                throw new ArgumentNullException("mongoQuery", "查询条件未定义");
            bool isConnectSuccess = false;
            using (Connect(out isConnectSuccess))
            {
                if (!isConnectSuccess)
                    return false;
                MongoCollection<BsonDocument> c = _Database.GetCollection<BsonDocument>(Collection);
                var result = c.Remove(mongoQuery);
                return result.DocumentsAffected == 1;
            }
        }

        /// <summary>
        ///     按指定的查询条件获取实体数量
        /// </summary>
        public long Count(IMongoQuery mongoQuery)
        {
            bool isConnectSuccess = false;
            using (Connect(out isConnectSuccess))
            {
                if (!isConnectSuccess)
                    return -1;
                MongoCollection<BsonDocument> c = _Database.GetCollection<BsonDocument>(Collection);
                return mongoQuery == null ? c.Count() : c.Count(mongoQuery);
            }
        }

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
        public virtual IEnumerable<T> Find(IMongoQuery mongoQuery)
        {
            return Find(mongoQuery, PagerInfo.Empty);
        }

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
        /// <exception cref="System.ArgumentNullException">查询条件未定义;mongoQuery</exception>
        public virtual IEnumerable<T> Find(IMongoQuery mongoQuery, PagerInfo pagerInfo)
        {
            return Find(mongoQuery, PagerInfo.Empty, null);
        }

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
        public virtual IEnumerable<T> Find(IMongoQuery mongoQuery, PagerInfo pagerInfo, IMongoSortBy sortBy, params string[] fields)
        {
            var result = new List<T>();
            bool isConnectSuccess = false;
            using (Connect(out isConnectSuccess))
            {
                if (!isConnectSuccess)
                    return null;
                MongoCollection<BsonDocument> c = _Database.GetCollection<BsonDocument>(Collection);
                MongoCursor<T> cursor = c.FindAs<T>(mongoQuery);

                if (null != sortBy)
                    cursor.SetSortOrder(sortBy);

                if (null != fields)
                    cursor.SetFields(fields);

                if (pagerInfo != PagerInfo.Empty)
                    cursor = cursor.SetSkip((int) ((pagerInfo.CurrentPage - 1)*pagerInfo.Count)).SetLimit((int) pagerInfo.Count);

                result.AddRange(cursor);
                result.TrimExcess();
            }
            return result;
        }

        #endregion
    }
}