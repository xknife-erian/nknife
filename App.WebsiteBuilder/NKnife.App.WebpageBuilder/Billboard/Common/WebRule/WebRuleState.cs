using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Jeelu.Billboard
{
    /// <summary>
    /// 表示规则状态
    /// </summary>
    public enum WebRuleState
    {
        /// <summary>
        /// 从服务器读来的状态
        /// </summary>
        None, 

        /// <summary>
        /// 新增的
        /// </summary>
        New,

        /// <summary>
        /// 修改的
        /// </summary>
        
        Modify,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
    }
}
