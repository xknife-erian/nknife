using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Hosting;

namespace NKnife.Storages
{
    /// <summary>
    /// 数据库相关管理的全局服务
    /// </summary>
    public interface IDbService : IHostedService
    {
    }
}