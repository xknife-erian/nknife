using LiteDB;
using NKnife.DataLite.Interfaces;

namespace NKnife.DataLite
{
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        protected readonly LiteCollection<T> _Collection;
        protected readonly LiteDatabase _Database;
        private string _RepositoryPath;

        protected RepositoryBase(string repositoryPath)
        {
            _RepositoryPath = repositoryPath;
            _Database = new LiteDatabase(repositoryPath);
            _Collection = _Database.GetCollection<T>(nameof(T));
        }
    }
}