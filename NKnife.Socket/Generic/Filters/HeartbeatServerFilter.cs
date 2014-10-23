﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Timers;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.Wrapper;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Exceptions;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class HeartbeatServerFilter : KnifeSocketServerFilter
    {
        private const string WAIT = "WAIT";
        private static readonly ILogger _logger = LogFactory.GetCurrentClassLogger();
        protected bool _ContinueNextFilter = true;
        private bool _IsTimerStarted;

        public HeartbeatServerFilter()
        {
            Heartbeat = new Heartbeat();
            Interval = 1000*15;
            IsStrictMode = false;
        }

        public Heartbeat Heartbeat { get; set; }
        public double Interval { get; set; }
        /// <summary>
        /// 严格模式开关
        /// true 心跳返回内容一定要和HeartBeat类中定义的_ReplayOfClient一致才算有心跳响应
        /// false 心跳返回任何内容均算有心跳相应
        /// </summary>
        public bool IsStrictMode { get; set; }

        public override bool ContinueNextFilter
        {
            get { return _ContinueNextFilter; }
        }

        protected internal override void OnClientCome(SocketSessionEventArgs e)
        {
            base.OnClientCome(e);
            if (!_IsTimerStarted) //第一次监听到时启动
            {
                _IsTimerStarted = true;

                var beatingTimer = new Timer();
                beatingTimer.Elapsed += BeatingTimer_Elapsed;
                beatingTimer.Interval = Interval;
                beatingTimer.Start();
                _logger.Info(string.Format("服务器心跳启动。间隔:{0}", Interval));
            }
        }

        private void BeatingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            KnifeSocketProtocolHandler[] handlers = _HandlersGetter.Invoke();
            KnifeSocketSessionMap map = SessionMapGetter.Invoke();

            var list = new List<EndPoint>(0);
            foreach (KeyValuePair<EndPoint, KnifeSocketSession> pair in map)
            {
                EndPoint endpoint = pair.Key;
                KnifeSocketSession session = pair.Value;
                if (!(session.WaitingForReply)) //两种情况：1.第一次检查时为非等待状态，2.心跳后收到回复后回写为非等待状态
                {
                    try
                    {
                        handlers[0].Write(session, Heartbeat.BeatingOfServerHeart);
                        session.WaitingForReply = true; //在PrcoessReceiveData方法里，当收到回复时会回写为false
                    }
                    catch (SocketClientDisOpenedException ex) //发送异常，发不出去则立即移出
                    {
                        _logger.Warn(string.Format("向客户端{0}发送心跳时异常:{1}", endpoint, ex.Message), ex);
                        RemoveEndPointFromSessionMap(endpoint);
                    }
                }
                else
                {
                    list.Add(endpoint);
                }
            }
            foreach (EndPoint endPoint in list)
            {
                RemoveEndPointFromSessionMap(endPoint);
            }
        }

        private void RemoveEndPointFromSessionMap(EndPoint endPoint)
        {
            KnifeSocketSessionMap map = SessionMapGetter.Invoke();
            KnifeSocketSession session;
            if (map.TryGetValue(endPoint, out session))
            {
                try
                {
                    session.Connector.Close();
                }
                catch (Exception ex)
                {
                    _logger.Warn(string.Format("断开客户端{0}时异常:{1}", endPoint, ex.Message), ex);
                }
                map.Remove(endPoint);
                _logger.Info(string.Format("心跳检查客户端{0}无响应，从SessionMap中移除之。池中:{1}", endPoint, map.Count));
            }
        }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            if (!IsStrictMode)
            {
                session.WaitingForReply = false;
                _ContinueNextFilter = true;
                _logger.Trace(() => string.Format("收到{0}信息,关闭心跳等待.", session.Source));
                return;
            }
            if (data.IndexOf(Heartbeat.ReplayOfClient) == 0)
            {
                session.WaitingForReply = false;
                _ContinueNextFilter = false;
                _logger.Trace(() => string.Format("收到{0}心跳回复.", session.Source));
            }
        }
    }
}