using System;
using Dapper;

namespace NKnife.Storages
{
    public class GeneralStorageWrite<T> : BaseStorageWrite<T>
    {
        public GeneralStorageWrite(IConnectionManager connectionManager, SimpleCRUD.ITableNameResolver tableNameResolver, DomainSqlConfig option) 
            : base(connectionManager, tableNameResolver, option)
        {
        }
    }
}