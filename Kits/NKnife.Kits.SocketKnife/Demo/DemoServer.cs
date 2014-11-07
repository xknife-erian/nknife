using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Windows.Data;
using System.Windows.Threading;
using NKnife.Base;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo.Protocols;
using NKnife.Mvvm;
using NKnife.Protocol.Generic;
using SocketKnife;
using SocketKnife.Common;
using SocketKnife.Generic;
using SocketKnife.Generic.Filters;

namespace NKnife.Kits.SocketKnife.Demo
{
    public class DemoServer : NotificationObject
    {
        #region 界面绑定

        private StringProtocol _Protocol;
        private bool _IsOnlyOnce = true;
        private bool _IsFixTime = false;
        private bool _IsRandomTime = false;
        private uint _FixTime = 200;
        private uint _RandomMinTime = 50;
        private uint _RandomMaxTime = 500;

        private readonly ServerList _ServerList = DI.Get<ServerList>();
        private Pair<IPAddress, int> _ServerListKey;
        internal Dispatcher Dispatcher { get; set; }
        public ObservableCollection<SocketMessage> SocketMessages { get; set; }
        public SessionList Sessions { get; set; }

        public StringProtocol CurrentProtocol
        {
            get { return _Protocol; }
            set
            {
                _Protocol = value;
                RaisePropertyChanged(() => CurrentProtocol);
            }
        }

        public bool IsOnlyOnce
        {
            get { return _IsOnlyOnce; }
            set
            {
                _IsOnlyOnce = value;
                RaisePropertyChanged(() => IsOnlyOnce);
            }
        }

        public bool IsFixTime
        {
            get { return _IsFixTime; }
            set
            {
                _IsFixTime = value;
                RaisePropertyChanged(() => IsFixTime);
            }
        }

        public bool IsRandomTime
        {
            get { return _IsRandomTime; }
            set
            {
                _IsRandomTime = value;
                RaisePropertyChanged(() => IsRandomTime);
            }
        }

        public uint FixTime
        {
            get { return _FixTime; }
            set
            {
                _FixTime = value;
                RaisePropertyChanged(() => FixTime);
            }
        }

        public uint RandomMinTime
        {
            get { return _RandomMinTime; }
            set
            {
                _RandomMinTime = value;
                RaisePropertyChanged(() => RandomMinTime);
            }
        }

        public uint RandomMaxTime
        {
            get { return _RandomMaxTime; }
            set
            {
                _RandomMaxTime = value;
                RaisePropertyChanged(() => RandomMaxTime);
            }
        }

        #endregion

        #region Sokcet相关

        private bool _IsInitialized;
        private KeepAliveServerFilter _KeepAliveFilter = DI.Get<KeepAliveServerFilter>();
        private KnifeSocketServer _Server;

        public DemoServer()
        {
            SocketMessages = new ObservableCollection<SocketMessage>();
            Sessions = new SessionList();
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
                        Dispatcher.BeginInvoke(sessionDelegate, new object[] {args.Item});
                    }
                }
                catch (Exception e)
                {
                    string error = string.Format("异步集合控制发生异常.{0}{1}", e.Message, e.StackTrace);
                    Debug.Fail(error);
                }
            };
            _KeepAliveFilter.SessionMapGetter.Invoke().Removed += (sender, args) =>
            {
                try
                {
                    if (Dispatcher.CheckAccess())
                    {
                        RemoveSession(args.Item);
                    }
                    else
                    {
                        var sessionDelegate = new SessionRemover(RemoveSession);
                        Dispatcher.BeginInvoke(sessionDelegate, new object[] {args.Item});
                    }
                }
                catch (Exception e)
                {
                    string error = string.Format("异步集合控制发生异常.{0}{1}", e.Message, e.StackTrace);
                    Debug.Fail(error);
                }
            };
        }

        protected void AddSession(KnifeSocketSession session)
        {
            Sessions.Add(new SessionByView {EndPoint = session.Source.ToString()});
        }

        protected void RemoveSession(EndPoint endPoint)
        {
            for (int i = 0; i < Sessions.Count; i++)
            {
                if (Sessions[i].EndPoint == endPoint.ToString())
                {
                    Sessions.RemoveAt(i);
                    break;
                }
            }
        }

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

        public void RemoveServer()
        {
            _ServerList.Remove(_ServerListKey);
        }

        public void StopServer()
        {
            _Server.Stop();
        }

        #endregion

        #region 内部类

        public class SessionByView : NotificationObject
        {
            private bool _EnableAutoReplay;
            private string _EndPoint;
            private bool _IsSelected;

            public SessionByView()
            {
                IsSelected = false;
                EnableAutoReplay = false;
            }

            public bool IsSelected
            {
                get { return _IsSelected; }
                set
                {
                    _IsSelected = value;
                    RaisePropertyChanged(() => IsSelected);
                }
            }

            public string EndPoint
            {
                get { return _EndPoint; }
                set
                {
                    _EndPoint = value;
                    RaisePropertyChanged(() => EndPoint);
                }
            }

            public bool EnableAutoReplay
            {
                get { return _EnableAutoReplay; }
                set
                {
                    _EnableAutoReplay = value;
                    RaisePropertyChanged(() => EnableAutoReplay);
                }
            }
        }

        public class SessionList : ObservableCollection<SessionByView>
        {
        }

        #endregion

        #region 界面委托

        private delegate void SessionRemover(EndPoint endPoint);

        private delegate void SessionAdder(KnifeSocketSession session);

        #endregion

    }


}