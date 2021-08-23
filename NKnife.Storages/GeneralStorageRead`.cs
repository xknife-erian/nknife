using Dapper;

namespace NKnife.Storages
{
    public class GeneralStorageRead<T> : BaseStorageRead<T>
    {
        public GeneralStorageRead(IConnectionManager connectionManager, SimpleCRUD.ITableNameResolver tableNameResolver, DomainSqlConfig option)
            : base(connectionManager,tableNameResolver, option)
        {
        }
    }
}