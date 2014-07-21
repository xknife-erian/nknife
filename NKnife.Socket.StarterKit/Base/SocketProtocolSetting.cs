using NKnife.Ioc;
using SocketKnife.Config;

namespace NKnife.Socket.StarterKit.Base
{
    public class SocketProtocolSetting : ProtocolSetting
    {
        #region 单件实例

        public SocketProtocolSetting()
        {
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static SocketProtocolSetting ME
        {
            get { return (SocketProtocolSetting) DI.Get<ProtocolSetting>(); }
        }

        #endregion
    }
}
