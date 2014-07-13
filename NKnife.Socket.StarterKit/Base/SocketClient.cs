using SocketKnife;
using SocketKnife.Common;
using SocketKnife.Interfaces;
using SocketKnife.Protocol;

namespace NKnife.Socket.StarterKit.Base
{
    public class ClientKit : Client
    {
        public ClientKit()
            : base(SocketMode.AsyncKeepAlive, ProtocolFamilyType.PanSOMS)
        {
        }

        /// <summary>是否采用抽象基类中的重连机制(较简单:在有动作触发时，发现连接断开时，按频率尝试重连服务器)
        /// </summary>
        public override bool IsCustomTryConnectionMode
        {
            get { return false; }
        }

        public override ProtocolFactory Protocols
        {
            get { return null; }
        }

        public override ISocketClientSetting Option
        {
            get { return SocketOption.ME; }
        }

    }
}