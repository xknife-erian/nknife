using System.Xml;
using Gean;
using NKnife.Configuring.CoderSetting;
using NKnife.NetWork.Interfaces;
using NKnife.Utility;

namespace NKnife.NetWork.Config
{
    public abstract class LightSocketServerSetting : XmlCoderSetting, ISocketServerSetting
    {
        public string Host { get; private set; }
        public int Port { get; private set; }
        public int MaxBufferSize { get; private set; }
        public int MaxConnectCount { get; private set; }
        /// <summary>
        /// Gets or sets 心跳间隔.
        /// </summary>
        /// <value>The heart range.</value>
        public int HeartRange { get; private set; }

        public IDatagramDecoder Decoder { get; private set; }
        public IDatagramEncoder Encoder { get; private set; }
        public IDatagramCommandParser CommandParser { get; private set; }

        protected override void Load(XmlElement source)
        {
            this.Host = source.SelectSingleNode("Host").InnerText.Trim();
            this.Port = int.Parse(source.SelectSingleNode("Port").InnerText.Trim());
            this.MaxBufferSize = int.Parse(source.SelectSingleNode("MaxBufferSize").InnerText.Trim());
            this.MaxConnectCount = int.Parse(source.SelectSingleNode("MaxConnectCount").InnerText.Trim());

            this.HeartRange = int.Parse(source.SelectSingleNode("heartRange").InnerText.Trim());
            this.Decoder = (IDatagramDecoder)UtilityType.CreateSimpleObject(source.SelectSingleNode("IDatagramDecoder").InnerText.Trim());
            this.Encoder = (IDatagramEncoder)UtilityType.CreateSimpleObject(source.SelectSingleNode("IDatagramEncoder").InnerText.Trim());
            this.CommandParser = (IDatagramCommandParser)UtilityType.CreateSimpleObject(source.SelectSingleNode("IDatagramCommandParser").InnerText.Trim());
        }

    }
}
