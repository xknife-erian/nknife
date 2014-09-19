using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 发送文件列表的核心类
    /// </summary>
    public class SendFileCore
    {
        public int TotleFileBytes { get; private set; }
        public int SendedFileBytes { get; private set; }

        public SendFileCore()
        {
        }
    }
}
