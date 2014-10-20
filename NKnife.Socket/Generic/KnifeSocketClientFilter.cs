using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Events;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketClientFilter : KnifeSocketFilter, ISocketClientFilter
    {
        public event EventHandler<ConnectioningEventArgs> Connectioning;

        protected internal virtual void OnConnectioning(ConnectioningEventArgs e)
        {
            EventHandler<ConnectioningEventArgs> handler = Connectioning;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ConnectionedEventArgs> Connectioned;

        protected internal virtual void OnConnectioned(ConnectionedEventArgs e)
        {
            EventHandler<ConnectionedEventArgs> handler = Connectioned;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ChangedEventArgs<ConnectionStatus>> SocketStatusChanged;

        protected internal virtual void OnSocketStatusChanged(ChangedEventArgs<ConnectionStatus> e)
        {
            EventHandler<ChangedEventArgs<ConnectionStatus>> handler = SocketStatusChanged;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<ConnectionedEventArgs> ConnectionBroke;

        protected internal virtual void OnConnectionBroke(ConnectionedEventArgs e)
        {
            EventHandler<ConnectionedEventArgs> handler = ConnectionBroke;
            if (handler != null) handler(this, e);
        }
    }
}
