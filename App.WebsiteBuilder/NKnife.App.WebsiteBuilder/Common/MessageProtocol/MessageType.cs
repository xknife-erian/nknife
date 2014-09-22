using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    public enum MessageType
    { 
        /// <summary>
        /// 未知消息类型
        /// </summary>
        None = 0,

        /// <summary>
        /// 用户，项目相关消息（第一个消息包）
        /// </summary>
        User = 1,

        File = 2,
        /// <summary>
        /// 文件接收
        /// </summary>
        FileReceive = 3,

        /// <summary>
        /// 文件整理　（整理文件）
        /// </summary>
        FileTidy = 4,

        /// <summary>
        /// 网站生成
        /// </summary>
        SiteBuild = 5,
    }

    //消息说明: 消息区分将通过消息类型的状态来区分
    /*
     * 
     User :  0失败;1成功;2用户退出;3由CGI传来UserID
     FileReceive :  0失败; 1成功; 2单个文件接收完成
     FileTidy : 0失败; 1成功
     SiteBuild : 0失败; 1成功
     * 
     * 
     */

}
