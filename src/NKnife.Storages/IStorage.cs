using System;

namespace NKnife.Storages
{
    public interface IStorage
    {
        /// <summary>
        ///     该实体类是否需要分表进行存储
        /// </summary>
        bool IsCutIntoMultiTables { get; }

        /// <summary>
        ///     根据当前数据表对应的实体类，生成数据表表名
        /// </summary>
        /// <param name="tableNameFunc">表名生成的逻辑函数</param>
        /// <returns>数据表表名</returns>
        string BuildTableName(Func<IStorage, string> tableNameFunc = null);
    }
}