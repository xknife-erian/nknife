using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel.Generic
{
    public abstract class TunnelBase<TData, TSessionId> : ITunnel<TData, TSessionId>
    {
        protected IDataConnector<TData, TSessionId> _DataConnector;
        protected ITunnelFilterChain<TData, TSessionId> _FilterChain;

        public abstract void Dispose();
        protected abstract void SetFilterChain();

        public ITunnelConfig Config { get; set; }
        public virtual void AddFilters(params ITunnelFilter<TData, TSessionId>[] filters)
        {
            if (_FilterChain == null)
                SetFilterChain();
            foreach (var filter in filters)
            {
                if (_FilterChain != null)
                    _FilterChain.AddLast(filter);
            }
        }

        public void RemoveFilter(ITunnelFilter<TData, TSessionId> filter)
        {
            _FilterChain.Remove(filter);
        }

        public bool Start()
        {
            _DataConnector.SessionBuilt += OnSessionBuilt;
            _DataConnector.SessionBroken += OnSessionBroken;
            _DataConnector.DataReceived += OnDataReceived;
            _DataConnector.Start();

            return true;
        }

        public virtual bool ReStart()
        {
            return true;
        }

        public virtual bool Stop()
        {
            return true;
        }

        public void BindDataConnector(IDataConnector<TData, TSessionId> dataConnector)
        {
            _DataConnector = dataConnector;
            _DataConnector.SessionBuilt += OnSessionBuilt;
            _DataConnector.SessionBroken += OnSessionBroken;
            _DataConnector.DataReceived += OnDataReceived;
            foreach (var filter in _FilterChain)
            {
                if (filter.Listener != null)
                {
                    filter.Listener.BindSessionHandler((ISessionProvider<TData, TSessionId>)dataConnector);
                }
            }
        }

        private void OnDataReceived(object sender, SessionEventArgs<TData, TSessionId> e)
        {
            foreach (var filter in _FilterChain)
            {
                filter.PrcoessReceiveData(e.Item); // 调用filter对数据进行处理

                if (!filter.ContinueNextFilter)
                    break;
            }
        }

        private void OnSessionBroken(object sender, SessionEventArgs<TData, TSessionId> e)
        {
            foreach (var filter in _FilterChain)
            {
                filter.ProcessSessionBroken(e.Item.Id);
            }
        }

        private void OnSessionBuilt(object sender, SessionEventArgs<TData, TSessionId> e)
        {
            foreach (var filter in _FilterChain)
            {
                filter.ProcessSessionBuilt(e.Item.Id);
            }
        }
    }
}
