using System;

namespace NKnife.DataLite.Interfaces
{
    public interface IRepository<T>: IDisposable
    {
        string RepositoryPath { get; }
    }
}