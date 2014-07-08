
using SocketKnife;
using SocketKnife.Common;
using SocketKnife.Interfaces;
using SocketKnife.Protocol;

namespace NKnife.SocketClientTestTool.Base
{
    //Socket文件夹下面的客户端
    public class PanSomsSocketClient : Client
    {
        public PanSomsSocketClient()
            : base(SocketMode.Talk, ProtocolFamilyType.PanSOMS)
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
            get { return PanSomsSocketOption.ME; }
        }

    }
}