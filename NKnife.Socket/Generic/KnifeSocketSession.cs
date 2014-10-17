using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic
{
    public class KnifeSocketSession : ISocketSession
    {
        protected Dictionary<object, object> _Attributes = new Dictionary<object, object>(1); 
        public KnifeSocketSession()
        {
            Id = DateTime.Now.Ticks;
            _Attributes.Add("WAIT", false);
        }

        public long Id { get; private set; }
        public EndPoint Source { get; set; }
        public Socket Connector { get; set; }
        public object TryGetAttribute(object key, object defaultValue)
        {
            object value;
            if (!_Attributes.TryGetValue(key, out value))
            {
                value = defaultValue;
                _Attributes.Add(key, defaultValue);
            }
            return value;
        }

        public void SetAttribute(object key, object value)
        {
            _Attributes.Add(key, value);
        }

        public object GetAttribute(object key)
        {
            return _Attributes[key];
        }

        public bool RemoveAttribute(object key)
        {
            return _Attributes.Remove(key);
        }

        public bool ContainsAttribute(object key)
        {
            return _Attributes.ContainsKey(key);
        }
    }
}
