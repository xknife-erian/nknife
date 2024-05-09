using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace NKnife.SerialBox
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    [CompilerGenerated]
    public sealed class Settings : ApplicationSettingsBase
    {
        public static Settings Default { get; } = (Settings) Synchronized(new Settings());

        [DebuggerNonUserCode]
        [UserScopedSetting]
        [DefaultSettingValue("")]
        public string PortName
        {
            get => (string) this["PortName"];
            set => this["PortName"] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("9600")]
        public string BaudRate
        {
            get => (string) this["BaudRate"];
            set => this["BaudRate"] = value;
        }

        [DefaultSettingValue("1")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public string StopBits
        {
            get => (string) this["StopBits"];
            set => this["StopBits"] = value;
        }

        [DefaultSettingValue("8")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public string DataBits
        {
            get => (string) this["DataBits"];
            set => this["DataBits"] = value;
        }

        [DefaultSettingValue("无")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string Parity
        {
            get => (string) this["Parity"];
            set => this["Parity"] = value;
        }

        [DefaultSettingValue("False")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool RevHex
        {
            get => (bool) this["RevHex"];
            set => this["RevHex"] = value;
        }

        [DefaultSettingValue("False")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool RTS
        {
            get => (bool) this["RTS"];
            set => this["RTS"] = value;
        }

        [DefaultSettingValue("False")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool DTR
        {
            get => (bool) this["DTR"];
            set => this["DTR"] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("开源电子网:www.openedv.com")]
        [DebuggerNonUserCode]
        public string SendData
        {
            get => (string) this["SendData"];
            set => this["SendData"] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool TimeSend
        {
            get => (bool) this["TimeSend"];
            set => this["TimeSend"] = value;
        }

        [DefaultSettingValue("1000")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string SendPeriod
        {
            get => (string) this["SendPeriod"];
            set => this["SendPeriod"] = value;
        }

        [DefaultSettingValue("False")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public bool HexSend
        {
            get => (bool) this["HexSend"];
            set => this["HexSend"] = value;
        }

        [DefaultSettingValue("True")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool SendNewLine
        {
            get => (bool) this["SendNewLine"];
            set => this["SendNewLine"] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("True")]
        [DebuggerNonUserCode]
        public bool MultiSendNewLine
        {
            get => (bool) this["MultiSendNewLine"];
            set => this["MultiSendNewLine"] = value;
        }

        [DefaultSettingValue("False")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool MultiHexSend
        {
            get => (bool) this["MultiHexSend"];
            set => this["MultiHexSend"] = value;
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        [UserScopedSetting]
        public bool RelateKeyBoard
        {
            get => (bool) this["RelateKeyBoard"];
            set => this["RelateKeyBoard"] = value;
        }

        [DefaultSettingValue("False")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool MultiAutoSend
        {
            get => (bool) this["MultiAutoSend"];
            set => this["MultiAutoSend"] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("1000")]
        [DebuggerNonUserCode]
        public string MutilSendPeriod
        {
            get => (string) this["MutilSendPeriod"];
            set => this["MutilSendPeriod"] = value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("False")]
        public bool ChangWinColor
        {
            get => (bool) this["ChangWinColor"];
            set => this["ChangWinColor"] = value;
        }

        [DefaultSettingValue("False")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool ShowTimeCheckBox
        {
            get => (bool) this["ShowTimeCheckBox"];
            set => this["ShowTimeCheckBox"] = value;
        }

        [DefaultSettingValue(
            "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n</ArrayOfString>")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public StringCollection StrCkbMultiSendList
        {
            get => (StringCollection) this["strCkbMultiSendList"];
            set => this["strCkbMultiSendList"] = value;
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue(
            "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n</ArrayOfString>")]
        [UserScopedSetting]
        public StringCollection StrTbxMultiSendList
        {
            get => (StringCollection) this["strTbxMultiSendList"];
            set => this["strTbxMultiSendList"] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue(
            "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<ArrayOfString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n  <string />\r\n</ArrayOfString>")]
        [DebuggerNonUserCode]
        public StringCollection StrReMarkTbxMultiSendList
        {
            get => (StringCollection) this["strReMarkTbxMultiSendList"];
            set => this["strReMarkTbxMultiSendList"] = value;
        }

        [DefaultSettingValue("True")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public bool CkbTransportProtocolAutoNewLine
        {
            get => (bool) this["ckbTransportProtocolAutoNewLine"];
            set => this["ckbTransportProtocolAutoNewLine"] = value;
        }

        [DefaultSettingValue("True")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public bool CkbTransportProtocolDispOrigialData
        {
            get => (bool) this["ckbTransportProtocolDispOrigialData"];
            set => this["ckbTransportProtocolDispOrigialData"] = value;
        }

        [DefaultSettingValue("01")]
        [DebuggerNonUserCode]
        [UserScopedSetting]
        public string TbxTransportProtocolSlaveDeviceAddr
        {
            get => (string) this["tbxTransportProtocolSlaveDeviceAddr"];
            set => this["tbxTransportProtocolSlaveDeviceAddr"] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("01")]
        [DebuggerNonUserCode]
        public string TbxTransportProtocolSendFunctionType
        {
            get => (string) this["tbxTransportProtocolSendFunctionType"];
            set => this["tbxTransportProtocolSendFunctionType"] = value;
        }

        [DefaultSettingValue("1000")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string TbxTransportProtocolSendPeriod
        {
            get => (string) this["tbxTransportProtocolSendPeriod"];
            set => this["tbxTransportProtocolSendPeriod"] = value;
        }

        [UserScopedSetting]
        [DefaultSettingValue("False")]
        [DebuggerNonUserCode]
        public bool CkbTransportProtocolAutoSend
        {
            get => (bool) this["ckbTransportProtocolAutoSend"];
            set => this["ckbTransportProtocolAutoSend"] = value;
        }

        [DefaultSettingValue("10")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string TbxTransportProtocolRetryCount
        {
            get => (string) this["tbxTransportProtocolRetryCount"];
            set => this["tbxTransportProtocolRetryCount"] = value;
        }

        [DefaultSettingValue("SUM(累加)")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string CbxTransportProtocolChecksum
        {
            get => (string) this["cbxTransportProtocolChecksum"];
            set => this["cbxTransportProtocolChecksum"] = value;
        }

        [DefaultSettingValue("01 02 03 04 05")]
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public string TransportProtocolSendDataTextBox
        {
            get => (string) this["TransportProtocolSendDataTextBox"];
            set => this["TransportProtocolSendDataTextBox"] = value;
        }

        [DebuggerNonUserCode]
        [DefaultSettingValue("255")]
        [UserScopedSetting]
        public string TransportProtocolMaxDataLengthTextBox
        {
            get => (string) this["TransportProtocolMaxDataLengthTextBox"];
            set => this["TransportProtocolMaxDataLengthTextBox"] = value;
        }
    }
}