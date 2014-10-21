using System;
using NKnife.Events;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public abstract class KnifeSocketClientFilter : KnifeSocketFilter, ISocketClientFilter
    {
        public Func<KnifeSocketSession> SessionGetter { get; protected set; }

        public void Bind(Func<KnifeSocketSession> sessionGetter)
        {
            SessionGetter = sessionGetter;
            OnBoundGetter();
        }

        public event EventHandler<ConnectioningEventArgs> Connectioning;

        public event EventHandler<ConnectionedEventArgs> Connectioned;

        public event EventHandler<ChangedEventArgs<ConnectionStatus>> SocketStatusChanged;

        public event EventHandler<ConnectionedEventArgs> ConnectionBroke;

        protected internal virtual void OnConnectioning(ConnectioningEventArgs e)
        {
            EventHandler<ConnectioningEventArgs> handler = Connectioning;
            if (handler != null)
                handler(this, e);
        }

        protected internal virtual void OnConnectioned(ConnectionedEventArgs e)
        {
            EventHandler<ConnectionedEventArgs> handler = Connectioned;
            if (handler != null)
                handler(this, e);
        }

        protected internal virtual void OnSocketStatusChanged(ChangedEventArgs<ConnectionStatus> e)
        {
            EventHandler<ChangedEventArgs<ConnectionStatus>> handler = SocketStatusChanged;
            if (handler != null)
                handler(this, e);
        }

        protected internal virtual void OnConnectionBroke(ConnectionedEventArgs e)
        {
            EventHandler<ConnectionedEventArgs> handler = ConnectionBroke;
            if (handler != null)
                handler(this, e);
        }
    }
}