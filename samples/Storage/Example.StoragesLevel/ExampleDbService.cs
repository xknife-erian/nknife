using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NKnife.Storages;
using NLog;

namespace Example.StoragesLevel
{
    public class ExampleDbService : DbService
    {
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();

        public ExampleDbService(DomainSqlConfig domainSqlConfig, IConnectionManager connectionManager) 
            : base(domainSqlConfig, connectionManager)
        {
        }
    }
}
