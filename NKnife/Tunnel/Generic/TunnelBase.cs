﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel.Generic
{
    public abstract class TunnelBase<TData, TSessionId> : ITunnel<TData, TSessionId>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private bool _IsDataConnectedBound;

        protected IDataConnector<TData, TSessionId> DataConnector;
        protected ITunnelFilterChain<TData, TSessionId> FilterChain;

        public abstract void Dispose();
        protected abstract void SetFilterChain();

        public ITunnelConfig Config { get; set; }
        public virtual void AddFilters(params ITunnelFilter<TData, TSessionId>[] filters)
        {
            if (FilterChain == null)
                SetFilterChain();
            foreach (var filter in filters)
            {
                if (FilterChain != null)
                    FilterChain.AddLast(filter);
            }
        }

        public void RemoveFilter(ITunnelFilter<TData, TSessionId> filter)
        {
            FilterChain.Remove(filter);
        }

        public bool Start()
        {
            return DataConnector.Start();
        }

        public virtual bool ReStart()
        {
            if (DataConnector.Stop())
            {
                return DataConnector.Start();
            }
            return false;
        }

        public virtual bool Stop()
        {
            return DataConnector.Stop();
        }

        public void BindDataConnector(IDataConnector<TData, TSessionId> dataConnector)
        {
            if (!_IsDataConnectedBound)
            {
                DataConnector = dataConnector;
                DataConnector.SessionBuilt += OnSessionBuilt;
                DataConnector.SessionBroken += OnSessionBroken;
                DataConnector.DataReceived += OnDataReceived;
                DataConnector.DataSent += OnDataSent;
                foreach (var filter in FilterChain)
                {
                    filter.BindSessionHandler(dataConnector);
                }
                _logger.Debug(string.Format("DataConnector[{0}]绑定成功",dataConnector.GetType()));
                _IsDataConnectedBound = true;
            }
            else
            {
                _logger.Debug(string.Format("DataConnector[{0}]已经绑定，不需重复绑定", dataConnector.GetType()));
            }
        }

        private void OnDataReceived(object sender, SessionEventArgs<TData, TSessionId> e)
        {
            foreach (var filter in FilterChain)
            {
                filter.PrcoessReceiveData(e.Item); // 调用filter对数据进行处理

                if (!filter.ContinueNextFilter)
                    break;
            }
        }

        private void OnDataSent(object sender, SessionEventArgs<TData, TSessionId> e)
        {
            foreach (var filter in FilterChain)
            {
                filter.PrcoessSendData(e.Item); // 调用filter对数据进行处理
            }
        }

        private void OnSessionBroken(object sender, SessionEventArgs<TData, TSessionId> e)
        {
            foreach (var filter in FilterChain)
            {
                filter.ProcessSessionBroken(e.Item.Id);
            }
        }

        private void OnSessionBuilt(object sender, SessionEventArgs<TData, TSessionId> e)
        {
            foreach (var filter in FilterChain)
            {
                filter.ProcessSessionBuilt(e.Item.Id);
            }
        }
    }
}
