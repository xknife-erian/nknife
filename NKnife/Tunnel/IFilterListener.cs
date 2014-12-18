using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.Tunnel
{
    public interface IFilterListener<TData, TSessionId>
    {
        void OnSessionBroken(TSessionId id);
        void OnSessionBuilt(TSessionId id);
        void BindSessionHandler(ISessionProvider<TData, TSessionId> sessionProvider);
    }
}
