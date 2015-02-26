using System;
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

        public void BindDataConnector(IDataConnector<TData, TSessionId> dataConnector)
        {
            if (!_IsDataConnectedBound)
            {
                DataConnector = dataConnector;
                DataConnector.SessionBuilt += OnSessionBuilt;
                DataConnector.SessionBroken += OnSessionBroken;
                DataConnector.DataReceived += OnDataReceived;
                foreach (var filter in FilterChain)
                {
                    filter.OnSendToSession += OnFilterSendToSession;
                    filter.OnSendToAll += OnFilterSendToAll;
                    filter.OnKillSession += OnFilterKillSession;
                }
                _logger.Debug(string.Format("DataConnector[{0}]绑定成功",dataConnector.GetType()));
                _IsDataConnectedBound = true;
            }
            else
            {
                _logger.Debug(string.Format("DataConnector[{0}]已经绑定，不需重复绑定", dataConnector.GetType()));
            }
        }

        private void OnFilterKillSession(object sender, NKnife.Events.EventArgs<TSessionId> e)
        {
            DataConnector.KillSession(e.Item);
        }

        private void OnFilterSendToAll(object sender, NKnife.Events.EventArgs<TData> e)
        {
            //取得上一个（靠近dataconnector的）filter
            var currentFilter = sender as ITunnelFilter<TData, TSessionId>;
            if (currentFilter == null)
                return;

            var node = FilterChain.Find(currentFilter);
            if (node == null)
                return;

            var previous = node.Previous;
            while (previous != null)
            {
                previous.Value.ProcessSendToAll(e.Item);
                previous = previous.Previous;
            }

            DataConnector.SendAll(e.Item);
        }

        private void OnFilterSendToSession(object sender, SessionEventArgs<TData, TSessionId> e)
        {
            //取得上一个（靠近dataconnector的）filter
            var currentFilter = sender as ITunnelFilter<TData, TSessionId>;
            if (currentFilter == null)
            {
                return;
            }

            var node = FilterChain.Find(currentFilter);
            if (node == null)
            {
                return;
            }

            var previous = node.Previous;
            while (previous != null)
            {
                previous.Value.ProcessSendToSession(e.Item);
                previous = previous.Previous;
            }

            DataConnector.Send(e.Item.Id,e.Item.Data);
        }

        private void OnDataReceived(object sender, SessionEventArgs<TData, TSessionId> e)
        {
            foreach (var filter in FilterChain)
            {
                var continueNextFilter = filter.PrcoessReceiveData(e.Item); // 调用filter对数据进行处理

                if (!continueNextFilter)
                    break;
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
