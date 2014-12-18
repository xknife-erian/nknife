using System;
using System.Net;
using NKnife.Protocol;

namespace NKnife.Tunnel
{
    public interface ITunnel<TData, TSessionId> : IDisposable
    {
        ITunnelConfig Config { get; set; }
        void AddFilters(params ITunnelFilter<TData, TSessionId>[] filter);
        void RemoveFilter(ITunnelFilter<TData, TSessionId> filter);
        bool Start();
        bool ReStart();
        bool Stop();
        void BindDataConnector(IDataConnector<TData, TSessionId> dataConnector);
    }
}