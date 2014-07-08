using System;
using SocketKnife.Config;

namespace NKnife.SocketClientTestTool.Base
{
    public class PanSomsSocketOption : AliveSocketClientSetting
    {
        #region 单件实例

        private PanSomsSocketOption()
        {
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static PanSomsSocketOption ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            internal static readonly PanSomsSocketOption Instance;

            static Singleton()
            {
                Instance = new PanSomsSocketOption();
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