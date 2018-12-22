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
        private StringProtocol _protocol;
        private bool _isOnlyOnce = true;
        private bool _isFixTime = false;
        private bool _isRandomTime = false;
        private uint _fixTime = 200;
        private uint _randomMinTime = 50;
        private uint _randomMaxTime = 500;
        private bool _onRandomTimeReplay = true;

        private IPEndPoint _currentServerPoint;
        private DemoServerHandler _handler;

        private readonly ServerMap _serverMap = Di.Get<ServerMap>();
        private readonly DemoServer _server = new DemoServer();
        private readonly ProtocolViewModel _protocolViewModel = Di.Get<ProtocolViewModel>();
        private readonly Timer _timer = new Timer();
        private readonly UtilityRandom _random = new UtilityRandom();

        public ObservableCollection<SocketMessage> SocketMessages { get; set; }

        public SessionList Sessions { get; set; }

        public StringProtocol CurrentProtocol
        {
            get { return _protocol; }
            set
            {
                _protocol = value;
                RaisePropertyChanged(() => CurrentProtocol);
            }
        }

        public bool IsOnlyOnce
        {
            get { return _isOnlyOnce; }
            set
            {
                _isOnlyOnce = value;
                RaisePropertyChanged(() => IsOnlyOnce);
            }
        }

        public bool IsFixTime
        {
            get { return _isFixTime; }
            set
            {
                _isFixTime = value;
                RaisePropertyChanged(() => IsFixTime);
            }
        }

        public bool IsRandomTime
        {
            get { return _isRandomTime; }
            set
            {
                _isRandomTime = value;
                RaisePropertyChanged(() => IsRandomTime);
            }
        }

        public uint FixTime
        {
            get { return _fixTime; }
            set
            {
                _fixTime = value;
                RaisePropertyChanged(() => FixTime);
            }
        }

        public uint RandomMinTime
        {
            get { return _randomMinTime; }
            set
            {
                _randomMinTime = value;
                RaisePropertyChanged(() => RandomMinTime);
            }
        }

        public uint RandomMaxTime
        {
            get { return _randomMaxTime; }
            set
            {
                _randomMaxTime = value;
                RaisePropertyChanged(() => RandomMaxTime);
            }
        }

        public bool OnRandomTimeReplay
        {
            get { return _onRandomTimeReplay; }
            set
            {
                _onRandomTimeReplay = value;
                RaisePropertyChanged(() => OnRandomTimeReplay);
            }
        }

        public TcpServerViewModel()
        {
            SocketMessages = new ObservableCollection<SocketMessage>();
            Sessions = new SessionList();
            _protocolViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "SelectedProtocol")
                    CurrentProtocol = _protocolViewModel.SelectedProtocol;
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
            _handler = new DemoServerHandler(_server.GetFamily(),SocketMessages);
            _currentServerPoint = new IPEndPoint(customSetting.IpAddress, customSetting.Port);
            if (Di.Get<ServerMap>().ContainsKey(_currentServerPoint))
            {
                MessageBox.Show("已启动相同端口号的Server端，请关闭后，重新选择后启动。");
                return;
            }
            _serverMap.Add(_currentServerPoint, _server.GetSocketServer());
            _server.Initialize(config, customSetting, _handler);

            var dataConnector = _server.GetSocket();
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

            _server.StartServer();
            //_ProtocolViewModel.AddFamily(_Server.GetFamily());
        }

        public void StopServer()
        {
            if (_timer != null)
                _timer.Stop();
            if (_currentServerPoint != null)
                _serverMap.Remove(_currentServerPoint);
            _server.StopServer();
        }

        public void Replay()
        {
            _onRandomTimeReplay = true;
            if (IsOnlyOnce)
            {
                WriteCurrentProtocol();
            }
            else if (IsFixTime)
            {
                _timer.Interval = _fixTime;
                _timer.Elapsed += (s, e) => WriteCurrentProtocol();
                _timer.Start();
            }
            else if (IsRandomTime)
            {
                var thread = new Thread(() =>
                {
                    while (_onRandomTimeReplay)
                    {
                        WriteCurrentProtocol();
                        Thread.Sleep(_random.Next((int) _randomMinTime, (int) _randomMaxTime));
                    }
                });
                thread.Name = "RandomReplayThread";
                thread.IsBackground = true;
                thread.Start();
            }
        }

        public void StopReplay()
        {
            if (_timer != null)
                _timer.Stop();
            _onRandomTimeReplay = false;
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
            private bool _enableAutoReplay;
            private IPEndPoint _endPoint;
            private bool _isSelected;

            public SessionByView()
            {
                IsSelected = false;
                EnableAutoReplay = false;
            }

            public bool IsSelected
            {
                get { return _isSelected; }
                set
                {
                    _isSelected = value;
                    RaisePropertyChanged(() => IsSelected);
                }
            }

            public IPEndPoint EndPoint
            {
                get { return _endPoint; }
                set
                {
                    _endPoint = value;
                    RaisePropertyChanged(() => EndPoint);
                }
            }

            public bool EnableAutoReplay
            {
                get { return _enableAutoReplay; }
                set
                {
                    _enableAutoReplay = value;
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