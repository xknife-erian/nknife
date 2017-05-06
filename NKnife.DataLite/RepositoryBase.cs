using System;
using System.IO;
using LiteDB;
using NKnife.DataLite.Interfaces;
using NKnife.Utility;

namespace NKnife.DataLite
{
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        private LiteCollection<T> _Collection;
        private LiteDatabase _Database;
        protected bool _NeedDispose = false;

        protected RepositoryBase(string repositoryPath)
        {
            if (string.IsNullOrWhiteSpace(repositoryPath))
                throw new ArgumentNullException(nameof(repositoryPath), "数据库文件路径不能为空");
            var dir = new DirectoryInfo(repositoryPath);
            if (dir.Exists)
                UtilityFile.CreateDirectory(dir.FullName);
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
                    _Database = new LiteDatabase(RepositoryPath);
                    _NeedDispose = true;
                }
                return _Database;
            }
        }

        /// <summary>
        ///     数据集合
        /// </summary>
        protected virtual LiteCollection<T> Collection => _Collection ?? (_Collection = Database.GetCollection<T>(nameof(T)));

        public string RepositoryPath { get; }

        #region IDisposable

        /// <summary>执行与释放或重置非托管资源相关的应用程序定义的任务。</summary>
        public void Dispose()
        {
            if (_NeedDispose)
                Database?.Dispose();
        }

        #endregion
    }
}