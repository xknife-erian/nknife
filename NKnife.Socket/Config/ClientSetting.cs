using System.Net;
using System.Xml;
using NKnife.Configuring.CoderSetting;
using NKnife.Configuring.Interfaces;
using NKnife.Utility;
using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Sockets;

namespace SocketKnife.Config
{
    public abstract class ClientSetting : XmlCoderSetting, ISocketClientSetting
    {
        /// <summary>
        ///     Gets or sets 服务器IP地址
        /// </summary>
        /// <value>The IP address.</value>
        public virtual string IPAddress { get; set; }

        /// <summary>
        ///     Gets or sets 服务器的端口
        /// </summary>
        /// <value>The port.</value>
        public virtual int Port { get; set; }

        public int TalkOneLength { get; private set; }

        public bool EnablePingServer { get; set; }

        /// <summary>
        ///     Gets 心跳间隔.
        /// </summary>
        /// <value>The heart range.</value>
        public virtual int HeartRange { get; protected set; }

        /// <summary>
        ///     Gets 缓存区大小.
        /// </summary>
        public virtual int BufferSize { get; protected set; }

        /// <summary>
        ///     同步的发送与接收的超时
        /// </summary>
        public virtual int Timeout { get; protected set; }

        public IDatagramDecoder Decoder { get; protected set; }
        public IDatagramEncoder Encoder { get; protected set; }
        public IDatagramCommandParser CommandParser { get; protected set; }

        protected override void Load(XmlElement source)
        {
            var serverElement = (XmlElement)source.SelectSingleNode("Server");
            if (serverElement != null)
            {
                IPAddress tempIp;
                if (System.Net.IPAddress.TryParse(serverElement.GetAttribute("ServerIp"), out tempIp))
                    IPAddress = serverElement.GetAttribute("ServerIp");
                Port = int.Parse(serverElement.GetAttribute("Port"));
            }

            int heartRange = 15 * 1000;
            var node = source.SelectSingleNode("HeartRange");
            if (node != null)
                int.TryParse(node.InnerText, out heartRange);
            HeartRange = heartRange;

            int bufferSize = 2048;
            var xmlNode = source.SelectSingleNode("BufferSize");
            if (xmlNode != null)
                int.TryParse(xmlNode.InnerText, out bufferSize);
            BufferSize = bufferSize;

            int timeout = 3000;
            XmlNode singleNode = source.SelectSingleNode("Timeout");
            if (singleNode != null)
                int.TryParse(singleNode.InnerText, out timeout);
            Timeout = timeout;

            int talkOneLength = 512;
            XmlNode selectSingleNode = source.SelectSingleNode("TalkOneLength");
            if (selectSingleNode != null)
                int.TryParse(selectSingleNode.InnerText, out talkOneLength);
            TalkOneLength = talkOneLength;

            bool pingServer = true;
            XmlNode pingServerNode = source.SelectSingleNode("EnablePingServer");
            if (pingServerNode != null)
                bool.TryParse(pingServerNode.InnerText, out pingServer);
            EnablePingServer = pingServer;

            Decoder = (IDatagramDecoder)UtilityType.CreateSimpleObject(source.SelectSingleNode("IDatagramDecoder").InnerText);
            Encoder = (IDatagramEncoder)UtilityType.CreateSimpleObject(source.SelectSingleNode("IDatagramEncoder").InnerText);
            CommandParser = (IDatagramCommandParser)UtilityType.CreateSimpleObject(source.SelectSingleNode("IDatagramCommandParser").InnerText);
        }
    }
}