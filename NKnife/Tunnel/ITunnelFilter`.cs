using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Events;
using NKnife.Tunnel.Events;

namespace NKnife.Tunnel
{
    public interface ITunnelFilter<TData, TSessionId>
    {
        #region 针对DataConnector的事件处理
        bool PrcoessReceiveData(ITunnelSession<TData, TSessionId> session);
        void ProcessSessionBroken(TSessionId id);
        void ProcessSessionBuilt(TSessionId id);
        #endregion

        #region 针对filter的事件处理和方法
        void ProcessSendToSession(ITunnelSession<TData, TSessionId> session);
        void ProcessSendToAll(TData data);
        event EventHandler<SessionEventArgs<TData, TSessionId>> OnSendToSession;
        event EventHandler<EventArgs<TData>> OnSendToAll;
        event EventHandler<EventArgs<TSessionId>> OnKillSession;

        #endregion

    }
}
