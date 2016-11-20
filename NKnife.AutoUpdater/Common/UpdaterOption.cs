using System;

namespace NKnife.AutoUpdater.Common
{
    /// <summary>程序关于自动更新配置的选项类
    /// </summary>
    [Serializable]
    public class UpdaterOption
    {
        public UpdaterOption()
        {
            Port = 21;
            UpdateStyle = UpdateStyle.None;
        }

        public string Uri { get; set; }
        public short Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RemotePath { get; set; }
        public UpdateStyle UpdateStyle { get; set; }
    }
}