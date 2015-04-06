using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonitorKnife.Tunnels.Common
{
    /// <summary>
    /// 面向Care制定的协议的封装
    /// </summary>
    public class CareSaying
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public byte Command { get; set; }
        /// <summary>
        /// GPIB地址
        /// </summary>
        public short GpibAddress { get; set; }
        /// <summary>
        /// 子命令
        /// </summary>
        public byte SubCommand { get; set; }
        /// <summary>
        /// 内容的长度
        /// </summary>
        public short Length { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
