using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Gean.Base
{
    /// <summary>��Ӧ�ó��������������ƽ̨�������ں���λ�õ������Ϣ�ķ�װ��eg: ���������������������дӻ��ȡ�
    /// </summary>
    [Serializable]
    public class MultiMachine : IXmlSerializable
    {
        public MultiMachine()
        {
            Id = "00";
            State = MultiMachineState.Single;
            ServerIpAddress = "127.0.0.1";
        }

        /// <summary>��ǰӦ�ó����ID
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }

        /// <summary>Ӧ�ó���λ��״̬
        /// </summary>
        /// <value>The state of the application.</value>
        public MultiMachineState State { get; set; }

        /// <summary>��Ϊ�ӻ�ʱ���������IP��ַ
        /// </summary>
        /// <value>The server ip address.</value>
        public string ServerIpAddress { get; set; }

        public override string ToString()
        {
            var ms = new MemoryStream();
            XmlWriter writer = new XmlTextWriter(ms, Encoding.Default);
            writer.WriteStartDocument();
            writer.WriteStartElement("ROOT");
            writer.WriteStartElement(GetType().Name);
            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("ApplicationState", State.ToString());
            writer.WriteAttributeString("ServerIpAddress", ServerIpAddress);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            return Encoding.Default.GetString(ms.ToArray());
        }

        public static string GetMachineId()
        {
            return string.Format("{0}-{1}", UtilityHardware.GetMacAddress(), UtilityHardware.GetCpuID()).Replace(":", "");
        }

        public static MultiMachine Parse(string content)
        {
            var machine = new MultiMachine();
            var doc = new XmlDocument();
            doc.LoadXml(content);
            if (doc.DocumentElement != null)
            {
                XmlNode node = doc.DocumentElement.SelectSingleNode(machine.GetType().Name);
                if (null != node)
                {
                    var ele = (XmlElement) node;
                    var stateValue = ele.GetAttribute("ApplicationState");
                    MultiMachineState parseState;
                    if (!Enum.TryParse(stateValue, false, out parseState))
                        parseState = MultiMachineState.Single;
                    machine.State = parseState;
                    machine.Id = machine.State == MultiMachineState.Slave ? ele.GetAttribute("Id") : "00";
                    machine.ServerIpAddress = ele.GetAttribute("ServerIpAddress");
                }
            }
            return machine;
        }

        #region Implementation of IXmlSerializable

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Id = reader.GetAttribute("Id");
            var stateValue = reader.GetAttribute("ApplicationState");

            MultiMachineState parseState;
            if (!Enum.TryParse(stateValue, false, out parseState))
                parseState = MultiMachineState.Single;
            State = parseState;
            ServerIpAddress = reader.GetAttribute("ServerIpAddress");
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Id", Id);
            writer.WriteAttributeString("ApplicationState", State.ToString());
            writer.WriteAttributeString("ServerIpAddress", ServerIpAddress);
        }

        #endregion
    }
}