using System;
using System.IO;
using LiteDB;
using NKnife.DataLite.Exceptions;
using NKnife.Interface.Datas.NoSql;
using NKnife.Utility;

namespace NKnife.DataLite
{
    /// <summary>
    /// 抽象类。本类型描述数据库中的一个表名。
    /// </summary>
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        private LiteCollection<T> _Collection;
        protected LiteDatabase _Database;
        protected bool _NeedDispose;

        protected RepositoryBase()
        {
        }

        /// <summary>
        /// 构造函数。本类型描述数据库中的一个表名。
        /// </summary>
        /// <param name="repositoryPath">数据库文件全名。含路径。</param>
        protected RepositoryBase(string repositoryPath)
        {
            if (string.IsNullOrWhiteSpace(repositoryPath))
                throw new DatabaseFileInvalidOperationException("数据库文件路径不能为空", nameof(repositoryPath));
            DirectoryInfo directory;
            try
            {
                directory = new DirectoryInfo(repositoryPath);
            }
            catch
            {
                throw new DatabaseFileInvalidOperationException("目录无效", nameof(repositoryPath));
            }
            if (!directory.Exists)
            {
                if (directory.Parent != null)
                    UtilityFile.CreateDirectory(directory.Parent.FullName);
                else
                    throw new DatabaseFileInvalidOperationException("无法创建目录", nameof(repositoryPath));
            }
            RepositoryPath = repositoryPath;
        }

        /// <summary>
        ///     集合所在的数据库，当多个集合在一个数据库时，应重写本函数。
        /// </summary>
        protected virtual LiteDatabase Database
        {
            get
            {
                if (_Database == null)
                {
                    if (string.IsNullOrWhiteSpace(RepositoryPath))
                        RepositoryPath = BuildDefaultDatabaseName();
                    _Database = new LiteDatabase(RepositoryPath);
                    _NeedDispose = true; //如果当多个集全在一个数据库文件时，不能轻易Dispose数据库，由外部自行控制数据库操作的释放
                }
                return _Database;
            }
        }

        /// <summary>
        ///     数据集合
        /// </summary>
        protected virtual LiteCollection<T> Collection => _Collection ?? (_Collection = Database.GetCollection<T>(BuildCollectionName()));

        /// <summary>
        ///     数据库主文件的实际路径
        /// </summary>
        public string RepositoryPath { get; private set; }

        #region IDisposable

        /// <summary>执行与释放或重置非托管资源相关的应用程序定义的任务。</summary>
        public void Dispose()
        {
            if (_NeedDispose)
                Database?.Dispose();
        }

        #endregion

        /// <summary>
        ///     默认数据库文件路径。存储在程序目录下。以类名+Repository为数据库文件名。
        /// </summary>
        /// <returns>默认数据库文件路径</returns>
        public static string BuildDefaultDatabaseName()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{typeof(T).Name}Repository.litedb");
        }

        /// <summary>
        ///     获取集合(表)名
        /// </summary>
        /// <returns>集合(表)名</returns>
        protected virtual string BuildCollectionName()
        {
            var name = typeof(T).Name;
            if (name.Contains("`"))//当一些泛型类型的名字需要替换掉的字符
                name = name.Replace("`", "-");
            return name;
        }
    }
}