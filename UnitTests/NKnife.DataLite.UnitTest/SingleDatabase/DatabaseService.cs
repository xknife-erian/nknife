using System;
using System.IO;
using LiteDB;
using NKnife.Utility;

namespace NKnife.DataLite.UnitTest.SingleDatabase
{
    public class DatabaseService : IDisposable
    {
        #region 简单单例

        static DatabaseService()
        {
            Instance = new DatabaseService();
        }

        public static DatabaseService Instance { get; }

        private readonly LiteDatabase _database;

        #endregion

        private DatabaseService()
        {
            if (_database == null)
            {
                var fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Datas\");
                if (!Directory.Exists(fullpath))
                    UtilityFile.CreateDirectory(fullpath);
                fullpath = Path.Combine(fullpath, "mk.litedb");
                _database = new LiteDatabase(fullpath);
            }
        }

        public LiteDatabase DataBase => _database;

        #region IDisposable

        /// <summary>执行与释放或重置非托管资源相关的应用程序定义的任务。</summary>
        public void Dispose()
        {
            _database?.Dispose();
        }

        #endregion
    }
}