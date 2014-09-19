using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.Billboard
{
    public enum KeywordMessageType
    {
        None = 0,
        /// <summary>
        /// 用户校验信息,例如：登录，上传前,更新前的验证
        /// </summary>
        User = 1,
        /// <summary>
        /// 上传新词库
        /// </summary>
        Upload = 2,
        /// <summary>
        /// 更新本地词库(C/S)
        /// </summary>
        Update = 3,
    }
    /*user：    0表示失败，返回：错误信息; 
     *          1表示成功，返回值 SessionId

     *update：  0表示失败 返回：失败信息
     *          1表示成功 返回成功信息  (更新本地词库)
     */
}
