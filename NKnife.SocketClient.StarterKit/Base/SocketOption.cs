using System;
using SocketKnife.Config;

namespace NKnife.SocketClient.StarterKit.Base
{
    public class SocketOption : ClientSetting
    {
        #region 单件实例

        private SocketOption()
        {
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static SocketOption ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            internal static readonly SocketOption Instance;

            static Singleton()
            {
                Instance = new SocketOption();
            }
        }

        #endregion

        public override string IPAddress
        {
            set { }
            get
            {
                var result = Params.ServerIpAddress;
                if (string.IsNullOrWhiteSpace(result))
                    throw new NullReferenceException("服务器IP地址不能为空.");
                return result;
            }
        }

        public override int Port
        {
            set { }
            get { return 9098; }
        }
    }
}