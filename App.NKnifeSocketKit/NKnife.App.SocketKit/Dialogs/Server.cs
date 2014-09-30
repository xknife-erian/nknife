using System.Collections.Generic;
using System.Net;
using System.Windows.Documents;
using NKnife.Base;
using NKnife.GUI.WinForm.IPAddressControl;
using SocketKnife;
using SocketKnife.Interfaces;

namespace NKnife.App.SocketKit.Dialogs
{
    class Server : TcpServerKnife
    {
    }

    class ServerList : Dictionary<Pair<IPAddress, int>, ISocketServerKnife>
    {

    }
}