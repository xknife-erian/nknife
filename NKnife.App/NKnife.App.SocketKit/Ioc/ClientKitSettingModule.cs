using System;
using NKnife.Socket.StarterKit.Base;
using SocketKnife.Config;

namespace NKnife.Socket.StarterKit.Ioc
{
    public class ClientKitSettingModule : ClientSetting
    {
        protected override Type GetBindType()
        {
            return typeof (SocketOption);
        }
    }
}