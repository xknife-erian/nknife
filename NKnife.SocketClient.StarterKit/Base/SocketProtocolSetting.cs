using SocketKnife.Config;

namespace NKnife.SocketClient.StarterKit.Base
{
    public class SocketProtocolSetting : ProtocolSetting
    {
        #region 单件实例

        private SocketProtocolSetting()
        {
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static SocketProtocolSetting ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            internal static readonly SocketProtocolSetting Instance;

            static Singleton()
            {
                Instance = new SocketProtocolSetting();
            }
        }

        #endregion
    }
}
