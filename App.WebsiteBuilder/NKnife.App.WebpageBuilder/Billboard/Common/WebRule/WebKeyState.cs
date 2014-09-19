using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.Billboard
{
    /// <summary>
    /// KEY 的状态 目前只有一个状态 即该参数有效/无效
    /// </summary>
    public enum WebKeyState
    {
        /// <summary>
        /// 指示该参数有效
        /// </summary>
        Availability = 0, 

        /// <summary>
        /// 指示该参数无效
        /// </summary>
        Invalidation = 1, 
    }
}
