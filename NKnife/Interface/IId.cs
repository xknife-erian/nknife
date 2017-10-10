using System;

namespace NKnife.Interface
{
    /// <summary>
    /// 描述一个有ID的对象
    /// </summary>
    public interface IId
    {
        /// <summary>
        /// 观察点的ID
        /// </summary>
        string Id { get; set; }
    }
}
