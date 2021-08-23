using System;

namespace NKnife.Interface.Datas.NoSql
{
    public interface IRepository<T>: IDisposable
    {
        string RepositoryPath { get; }
    }
}