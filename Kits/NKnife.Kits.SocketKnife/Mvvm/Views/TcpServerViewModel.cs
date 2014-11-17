using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo;
using NKnife.Mvvm;
using NKnife.Protocol.Generic;
using NKnife.Utility;
using SocketKnife.Generic;
using Timer = System.Timers.Timer;

namespace NKnife.Kits.SocketKnife.Mvvm.Views
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
        private bool _OnRandomTimeReplay = true;

        private IPEndPoint _CurrentServerPoint;
        private DemoServerHandler _Handler;

        private readonly ServerMap _ServerMap = DI.Get<ServerMap>();
        private readonly DemoServer _Server = new DemoServer();
        private readonly ProtocolViewModel _ProtocolViewModel = DI.Get<ProtocolViewModel>();
        private readonly Timer _Timer = new Timer();
        private readonly UtilityRandom _Random = new UtilityRandom();

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

        public bool OnRandomTimeReplay
        {
            get { return _OnRandomTimeReplay; }
            set
            {
                _OnRandomTimeReplay = value;
                RaisePropertyChanged(() => OnRandomTimeReplay);
            }
        }

        public DemoServerViewModel()
        {
            SocketMessages = new ObservableCollection<SocketMessage>();
            Sessions = new SessionList();
            _ProtocolViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "SelectedProtocol")
                    CurrentProtocol = _ProtocolViewModel.SelectedProtocol;
            };
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
                    string error = string.Format("�첽���Ͽ��Ʒ����쳣.{0}{1}", e.Message, e.StackTrace);
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
                    string error = string.Format("�첽���Ͽ��Ʒ����쳣.{0}{1}", e.Message, e.StackTrace);
                    Debug.Fail(error);
                }
            };
            _Server.StartServer();
            _ProtocolViewModel.SetFamily(_Server.GetFamily());
        }

        public void StopServer()
        {
            if (_Timer != null)
                _Timer.Stop();
            if (_CurrentServerPoint != null)
                _ServerMap.Remove(_CurrentServerPoint);
            _Server.StopServer();
        }

        public void Replay()
        {
            _OnRandomTimeReplay = true;
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
                var thread = new Thread(() =>
                {
                    while (_OnRandomTimeReplay)
                    {
                        WriteCurrentProtocol();
                        Thread.Sleep(_Random.Next((int) _RandomMinTime, (int) _RandomMaxTime));
                    }
                });
                thread.Name = "RandomReplayThread";
                thread.IsBackground = true;
                thread.Start();
            }
        }

        public void StopReplay()
        {
            if (_Timer != null)
                _Timer.Stop();
            _OnRandomTimeReplay = false;
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

        #region �ڲ���

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

        #region ����ί��

        private delegate void SessionRemover(EndPoint endPoint);

        private delegate void SessionAdder(KnifeSocketSession session);

        #endregion
    }
}