using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using Ninject;
using NKnife.Configuring;
using NKnife.Configuring.Interfaces;
using NKnife.Utility;
using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Sockets;

namespace SocketKnife.Config
{
    public abstract class ClientSetting : CoderSettingModule
    {
        protected override FileInfo GetSettingFile()
        {
            return new FileInfo(Path.Combine(_SettingFileBasePath, "socketclient.codersetting"));
        }

    }
}