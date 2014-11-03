using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Threading;
using NKnife.Base;
using NKnife.Collections;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo.Protocols;
using NKnife.Mvvm;
using NKnife.Protocol.Generic;
using SocketKnife;
using SocketKnife.Common;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;
using SocketKnife.Interfaces;

namespace NKnife.Kits.SocketKnife.Demo
{
    public class DemoServer : NotificationObject
    {
        #region App

        internal Dispatcher Dispatcher { get; set; }

        private readonly ServerList _ServerList = DI.Get<ServerList>();
        public ObservableCollection<SocketMessage> SocketMessages { get; set; }
        public ObservableCollection<KnifeSocketSession> Sessions { get; set; }

        private Pair<IPAddress, int> _ServerListKey;

        public void RemoveServer()
        {
            _ServerList.Remove(_ServerListKey);
        }

        #endregion

        private bool _IsInitialized = false;
        private KnifeSocketServer _Server;
        KeepAliveServerFilter _KeepAliveFilter = DI.Get<KeepAliveServerFilter>();

        public DemoServer()
        {
            SocketMessages = new ObservableCollection<SocketMessage>();
            Sessions = new ObservableCollection<KnifeSocketSession>();
        }

        public void Initialize(KnifeSocketConfig config, SocketTools socketTools)
        {
            if (_IsInitialized) return;

            Pair<IPAddress, int> key = Pair<IPAddress, int>.Build(socketTools.IpAddress, socketTools.Port);
            _ServerListKey = key;

            var heartbeatServerFilter = DI.Get<HeartbeatServerFilter>();
            heartbeatServerFilter.Interval = 1000*5;
            heartbeatServerFilter.Heartbeat = new Heartbeat();

            _KeepAliveFilter = DI.Get<KeepAliveServerFilter>();
            var codec = DI.Get<KnifeSocketCodec>();
            if (codec.SocketDecoder.GetType() != socketTools.Decoder)
                codec.SocketDecoder = (KnifeSocketDatagramDecoder) DI.Get(socketTools.Decoder);
            if (codec.SocketEncoder.GetType() != socketTools.Encoder)
                codec.SocketEncoder = (KnifeSocketDatagramEncoder) DI.Get(socketTools.Encoder);

            StringProtocolFamily protocolFamily = GetProtocolFamily();
            if (protocolFamily.CommandParser.GetType() != socketTools.CommandParser)
                protocolFamily.CommandParser = (StringProtocolCommandParser) DI.Get(socketTools.CommandParser);

            if (!_ServerList.ContainsKey(key))
            {
                _Server = DI.Get<KnifeSocketServer>();
                _Server.Config = config;
                if (socketTools.NeedHeartBeat)
                    _Server.AddFilters(heartbeatServerFilter);
                _Server.AddFilters(_KeepAliveFilter);
                _Server.Configure(socketTools.IpAddress, socketTools.Port);
                _Server.Bind(codec, protocolFamily);
                _Server.AddHandlers(new DemoServerHandler(SocketMessages, Dispatcher));
                _ServerList.Add(key, _Server);
            }
            else
            {
                Debug.Fail(string.Format("不应出现相同IP与端口的服务器请求。{0}", key));
            }
            _IsInitialized = true;
            _KeepAliveFilter.SessionMapGetter.Invoke().Added += (sender, args) =>
            {
                try
                {
                    if (Dispatcher.CheckAccess())
                    {
                        AddSession(args.Item);
                    }
                    else
                    {
                        var sessionDelegate = new SessionAdder(AddSession);
                        Dispatcher.BeginInvoke(sessionDelegate, new object[] { args.Item });
                    }
                }
                catch (Exception e)
                {
                    string error = string.Format("向控件写日志发生异常.{0}{1}", e.Message, e.StackTrace);
                    Debug.Fail(error);
                }
            };
            _KeepAliveFilter.SessionMapGetter.Invoke().Removed += (sender, args) =>
            {
                var session = ((KnifeSocketSessionMap) sender)[args.Item];
                try
                {
                    if (Dispatcher.CheckAccess())
                    {
                        RemoveSession(session);
                    }
                    else
                    {
                        var sessionDelegate = new SessionRemover(RemoveSession);
                        Dispatcher.BeginInvoke(sessionDelegate, new object[] { session });
                    }
                }
                catch (Exception e)
                {
                    string error = string.Format("向控件写日志发生异常.{0}{1}", e.Message, e.StackTrace);
                    Debug.Fail(error);
                }
            };
        }

        protected void AddSession(KnifeSocketSession session)
        {
            Sessions.Add(session);
        }
        protected void RemoveSession(KnifeSocketSession session)
        {
            Sessions.Remove(session);
        }

        private delegate void SessionRemover(KnifeSocketSession session);
        private delegate void SessionAdder(KnifeSocketSession session);

        private StringProtocolFamily GetProtocolFamily()
        {
            var register = DI.Get<Register>();

            var family = DI.Get<StringProtocolFamily>();
            family.FamilyName = "socket-kit";

            var custom = DI.Get<StringProtocol>("TestCustom");
            custom.Family = family.FamilyName;
            custom.Command = "custom";

            family.Add(family.Build("call"));
            family.Add(family.Build("recall"));
            family.Add(family.Build("sing"));
            family.Add(family.Build("dance"));
            family.Add(register);
            family.Add(custom);

            return family;
        }

        public void StartServer()
        {
            _Server.Start();
        }

        public void StopServer()
        {
            _Server.Stop();
        }

    }
}