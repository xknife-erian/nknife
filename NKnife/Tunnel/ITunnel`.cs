using System;
using System.Net;
using NKnife.Protocol;

namespace NKnife.Tunnel
{
    public interface ITunnel<TData> : IDisposable
    {
        ITunnelConfig Config { get; set; }
        void AddFilters(params ITunnelFilter[] filter);
        void RemoveFilter(ITunnelFilter filter);
        void BindDataConnector(IDataConnector<TData> dataConnector);
    }
}