using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Mvvm;
using NKnife.Protocol.Generic;
using SocketKnife.Generic;

namespace NKnife.Kits.SocketKnife.Demo
{
    public class DemoServerViewModel : NotificationObject
    {
        private StringProtocol _Protocol;
        private bool _IsOnlyOnce = true;
        private bool _IsFixTime = false;
        private bool _IsRandomTime = false;
        private uint _FixTime = 200;
        private uint _RandomMinTime = 50;
        private uint _RandomMaxTime = 500;

        private IPEndPoint _CurrentServerPoint;
        private DemoServerHandler _Handler;
        private readonly ServerMap _ServerMap = DI.Get<ServerMap>();
        private readonly DemoServer _Server = new DemoServer();
        private Timer _Timer = new Timer();

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

        public DemoServerViewModel()
        {
            SocketMessages = new ObservableCollection<SocketMessage>();
            Sessions = new SessionList();
        }

        protected void AddSession(KnifeSocketSession session)
        {
            Sessions.Add(new SessionByView {EndPoint = (IPEndPoint) session.Source});
        }

        protected void RemoveSession(EndPoint endPoint)
        {
            for (int i = 0; i < Sessions.Count; i++)
            {
                if (Equals(Sessions[i].EndPoint, endPoint))
                {
                    Sessions.RemoveAt(i);
                    break;
                }
            }
        }

        public void StartServer(KnifeSocketConfig config, SocketTools tools)
        {
            _Handler = new DemoServerHandler(SocketMessages);
            _CurrentServerPoint = new IPEndPoint(tools.IpAddress, tools.Port);
            _ServerMap.Add(_CurrentServerPoint, _Server.GetSocketServer());
            _Server.Initialize(config, tools, _Handler);

            var filter = _Server.GetSocketServerFilter();
            filter.SessionMapGetter.Invoke().Added += (sender, args) =>
            {
                try
                {
                    if (Application.Current.Dispatcher.CheckAccess())
                    {
                        AddSession(args.Item);
                    }
                    else
                    {
                        var sessionDelegate = new SessionAdder(AddSession);
                        Application.Current.Dispatcher.BeginInvoke(sessionDelegate, new object[] { args.Item });
                    }
                }
                catch (Exception e)
                {
                    string error = string.Format("异步集合控制发生异常.{0}{1}", e.Message, e.StackTrace);
                    Debug.Fail(error);
                }
            };
            filter.SessionMapGetter.Invoke().Removed += (sender, args) =>
            {
                try
                {
                    if (Application.Current.Dispatcher.CheckAccess())
                    {
                        RemoveSession(args.Item);
                    }
                    else
                    {
                        var sessionDelegate = new SessionRemover(RemoveSession);
                        Application.Current.Dispatcher.BeginInvoke(sessionDelegate, new object[] { args.Item });
                    }
                }
                catch (Exception e)
                {
                    string error = string.Format("异步集合控制发生异常.{0}{1}", e.Message, e.StackTrace);
                    Debug.Fail(error);
                }
            };
            _Server.StartServer();
        }

        public void StopServer()
        {
            if (_Timer != null)
                _Timer.Stop();
            _ServerMap.Remove(_CurrentServerPoint);
            _Server.StopServer();
        }

        public void Replay()
        {
            if (IsOnlyOnce)
            {
                WriteCurrentProtocol();
            }
            else if (IsFixTime)
            {
                _Timer.Interval = _FixTime;
                _Timer.Elapsed += (s, e) => WriteCurrentProtocol();
                _Timer.Start();
            }
            else if (IsRandomTime)
            {
            }
        }

        private void WriteCurrentProtocol()
        {
            if (CurrentProtocol != null)
            {
                foreach (var session in Sessions)
                {
                    if (session.IsSelected)
                        _Handler.Write(_Handler.SessionMap[session.EndPoint], CurrentProtocol);
                }
            }
        }

        #region 内部类

        public class SessionByView : NotificationObject
        {
            private bool _EnableAutoReplay;
            private IPEndPoint _EndPoint;
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

            public IPEndPoint EndPoint
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