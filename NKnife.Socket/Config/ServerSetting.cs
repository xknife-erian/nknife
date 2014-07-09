using System.Xml;
using NKnife.Configuring.CoderSetting;
using NKnife.Utility;
using SocketKnife.Interfaces;

namespace SocketKnife.Config
{
    public abstract class ServerSetting : XmlCoderSetting, ISocketServerSetting
    {
        public string Host { get; private set; }
        public int Port { get; private set; }
        public int MaxBufferSize { get; private set; }
        public int MaxConnectCount { get; private set; }

        /// <summary>
        ///     Gets or sets 心跳间隔.
        /// </summary>
        /// <value>The heart range.</value>
        public int HeartRange { get; private set; }

        public IDatagramDecoder Decoder { get; private set; }
        public IDatagramEncoder Encoder { get; private set; }
        public IDatagramCommandParser CommandParser { get; private set; }

        protected override void Load(XmlElement source)
        {
            Host = source.SelectSingleNode("Host").InnerText.Trim();
            Port = int.Parse(source.SelectSingleNode("Port").InnerText.Trim());
            MaxBufferSize = int.Parse(source.SelectSingleNode("MaxBufferSize").InnerText.Trim());
            MaxConnectCount = int.Parse(source.SelectSingleNode("MaxConnectCount").InnerText.Trim());

            HeartRange = int.Parse(source.SelectSingleNode("heartRange").InnerText.Trim());
            Decoder =
                (IDatagramDecoder)
                    UtilityType.CreateSimpleObject(source.SelectSingleNode("IDatagramDecoder").InnerText.Trim());
            Encoder =
                (IDatagramEncoder)
                    UtilityType.CreateSimpleObject(source.SelectSingleNode("IDatagramEncoder").InnerText.Trim());
            CommandParser =
                (IDatagramCommandParser)
                    UtilityType.CreateSimpleObject(source.SelectSingleNode("IDatagramCommandParser").InnerText.Trim());
        }
    }
}