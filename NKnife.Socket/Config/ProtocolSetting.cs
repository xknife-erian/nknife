using System;
using System.Collections.Concurrent;
using System.Xml;
using NKnife.Configuring.CoderSetting;
using NKnife.Utility;
using NLog;
using SocketKnife.Protocol;

namespace SocketKnife.Config
{
    public abstract class ProtocolSetting
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        protected ProtocolSetting()
        {
            FamilyMap = new ConcurrentDictionary<string, ProtocolFamily>();
            ProtocolContentMap = new ConcurrentDictionary<string, Type>();
            ProtocolToolsMap = new ConcurrentDictionary<string, ProtocolTools>();
        }

        protected internal ConcurrentDictionary<string, ProtocolFamily> FamilyMap { get; set; }
        protected internal ConcurrentDictionary<string, Type> ProtocolContentMap { get; set; }
        protected internal ConcurrentDictionary<string, ProtocolTools> ProtocolToolsMap { get; set; }
    }
}