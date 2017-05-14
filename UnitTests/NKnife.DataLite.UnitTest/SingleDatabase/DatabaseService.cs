using System;
using System.IO;
using LiteDB;
using NKnife.Utility;

namespace NKnife.DataLite.UnitTest.SingleDatabase
{
    public class DatabaseService : IDisposable
    {
        static DatabaseService()
        {
            Instance = new DatabaseService();
        }

        public static DatabaseService Instance { get; }

        private readonly LiteDatabase _Database;

        private DatabaseService()
        {
            if (_Database == null)
            {
                var fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Datas\");
                if (!Directory.Exists(fullpath))
                    UtilityFile.CreateDirectory(fullpath);
                fullpath = Path.Combine(fullpath, "mk.litedb");
                _Database = new LiteDatabase(fullpath);
            }
        }

        public LiteDatabase DataBase => _Database;

        #region IDisposable

        /// <summary>执行与释放或重置非托管资源相关的应用程序定义的任务。</summary>
        public void Dispose()
        {
            _Database?.Dispose();
        }

        #endregion
    }
}