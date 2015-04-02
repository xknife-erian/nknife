using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.Common;
using NKnife.Kits.SocketKnife.Demo;
using NKnife.Protocol.Generic;
using NKnife.Utility;
using SocketKnife.Generic;
using MessageBox = System.Windows.Forms.MessageBox;
using Timer = System.Timers.Timer;

namespace NKnife.Kits.SocketKnife.Mvvm.Views
{
    public class TcpServerViewModel : ViewModelBase
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

        public TcpServerViewModel()
        {
            SocketMessages = new ObservableCollection<SocketMessage>();
            Sessions = new SessionList();
            _ProtocolViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "SelectedProtocol")
                    CurrentProtocol = _ProtocolViewModel.SelectedProtocol;
            };
        }

        protected void AddSession(SocketSession session)
        {
            Sessions.Add(new SessionByView { EndPoint = (IPEndPoint)session.AcceptSocket.LocalEndPoint });
        }

        protected void RemoveSession(long endPoint)
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

        internal void StartServer(SocketConfig config, SocketCustomSetting customSetting)
        {
            _Handler = new DemoServerHandler(_Server.GetFamily(),SocketMessages);
            _CurrentServerPoint = new IPEndPoint(customSetting.IpAddress, customSetting.Port);
            if (DI.Get<ServerMap>().ContainsKey(_CurrentServerPoint))
            {
                MessageBox.Show("已启动相同端口号的Server端，请关闭后，重新选择后启动。");
                return;
            }
            _ServerMap.Add(_CurrentServerPoint, _Server.GetSocketServer());
            _Server.Initialize(config, customSetting, _Handler);

            var dataConnector = _Server.GetSocket();
            dataConnector.SessionBuilt += (sender, args) =>
            {
                try
                {
                    if (Application.Current.Dispatcher.CheckAccess())
                    {
                        AddSession((SocketSession) args.Item);
                    }
                    else
                    {
                        var sessionDelegate = new SessionAdder(AddSession);
                        Application.Current.Dispatcher.BeginInvoke(sessionDelegate, new object[] {args.Item});
                    }
                }
                catch (Exception e)
                {
                    string error = string.Format("异步集合控制发生异常.{0}{1}", e.Message, e.StackTrace);
                    Debug.Fail(error);
                }
            };

            dataConnector.SessionBroken += (sender, args) =>
            {
                try
                {
                    if (Application.Current.Dispatcher.CheckAccess())
                    {
                        RemoveSession(args.Item.Id);
                    }
                    else
                    {
                        var sessionDelegate = new SessionRemover(RemoveSession);
                        Application.Current.Dispatcher.BeginInvoke(sessionDelegate, new object[] { args.Item.Id });
                    }
                }
                catch (Exception e)
                {
                    string error = string.Format("异步集合控制发生异常.{0}{1}", e.Message, e.StackTrace);
                    Debug.Fail(error);
                }
            };

            _Server.StartServer();
            //_ProtocolViewModel.AddFamily(_Server.GetFamily());
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
                    //if (session.IsSelected)
                        //_Handler.WriteToSession(session.Id, CurrentProtocol);
                }
            }
        }

        #region 内部类

        public class SessionByView : ObservableObject
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

        private delegate void SessionRemover(long endPoint);

        private delegate void SessionAdder(SocketSession session);

        #endregion
    }
}