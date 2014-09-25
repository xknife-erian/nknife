using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces.Sockets;

namespace SocketKnife
{
    internal class TempServer
    {
        public TempServer()
        {
            ISocketServerKnife server = new TcpServerKnife();
            server.Bind("localhost", 11001);
            server.GetConfig.SetReadBufferSize(2048);
            server.GetFilterChain().AddLast("KeepConn", new KeepFilter(true));
            server.GetFilterChain().AddLast("Codec", new ProtocolCodecFilter());
            server.GetFilterChain().AddLast("logger", new LoggingFilter());
            server.Start();
            server.ReStart();
            server.Stop();
        }
    }

    public class LoggingFilter : IFilter
    {
    }

    public class ProtocolCodecFilter : IFilter
    {
    }

    public class KeepFilter : IFilter
    {
        public KeepFilter(bool b)
        {
        }
    }

    public interface ISocketServerKnife
    {
        void Bind(string localhost, int port);
        ISocketConfig GetConfig { get; }
        IFilterChain GetFilterChain();
        void Start();
        void ReStart();
        void Stop();
    }

    public interface IFilterChain : IDictionary<string, IFilter>
    {
        IFilter AddLast(string key, IFilter filter);
        IFilter AddFirst(string key, IFilter filter);
        IFilter Insert(int index, string key, IFilter filter);
    }

    public interface IFilter
    {
    }

    public interface ISocketConfig
    {
        void SetReadBufferSize(int size);
        void SetIdleTime(object bothIdle, int i);
    }
}
