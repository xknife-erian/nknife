using System;
using System.Collections.Concurrent;
using NKnife.Adapters;
using NKnife.Interface;
using NKnife.NSerial.Base;

namespace NKnife.NSerial
{
    /// <summary>串口通讯管理器
    /// </summary>
    public sealed class SerialCommunicationManager : ISerialCommunicationManager, IDisposable
    {
        private static readonly ILogger _Logger = LogFactory.GetCurrentClassLogger();
        private bool _EnableDetialLog;

        /// <summary>串口管理器字典，以串口号作为键值
        /// </summary>
        private readonly ConcurrentDictionary<ushort, ISerialCommunication> _SerialMap;
        
        /// <summary>实现了两种串口管理器，一种是通过.net实现，一种是通过windows api实现，默认采用.net实现的版本
        /// </summary>
        private SerialType _SerialType;

        public SerialCommunicationManager()
        {

            _SerialMap = new ConcurrentDictionary<ushort, ISerialCommunication>();
        }

        #region ISerialCommunicationManager Members

        public bool Initialize(SerialType serialType = SerialType.API, bool enableDetialLog = false)
        {
            _EnableDetialLog = enableDetialLog;
            _SerialType = serialType;
            return true;
        }

        /// <summary>添加一个串口
        /// </summary>
        /// <param name="port">描述一个串口序号</param>
        /// <returns></returns>
        public bool AddPort(ushort port)
        {
            if (_SerialMap.ContainsKey(port))
            {
                return _SerialMap[port].Active;
            }
            ISerialCommunication serial = new SerialCommunication(_EnableDetialLog, _SerialType);
            serial.OpenPort(port);
            _SerialMap.TryAdd(port, serial);
            return true;
        }

        /// <summary>关闭一个串口
        /// </summary>
        /// <param name="port">描述一个串口序号</param>
        /// <returns></returns>
        public bool RemovePort(ushort port)
        {
            ISerialCommunication serial;
            if (_SerialMap.TryRemove(port, out serial))
            {
                serial.ClosePort();
            }
            return true;
        }

        /// <summary>关闭所有串口，并将所有串口从管理器中移除
        /// </summary>
        /// <returns></returns>
        public bool RemoveAllPorts()
        {
            try
            {
                foreach (SerialCommunication serialuti in _SerialMap.Values)
                {
                    serialuti.ClosePort();
                }
                _SerialMap.Clear();
                return true;
            }
            catch (Exception ex)
            {
                _Logger.Warn("SerialCommunicationManager类UnInitialize异常", ex);
                return false;
            }
        }

        /// <summary>向指定的串口写入一个数据包
        /// </summary>
        /// <param name="port">描述一个串口序号</param>
        /// <param name="package">包含发送数据，以及相关指令及信息与事件的封装</param>
        public bool AddPackage(ushort port, PackageBase package)
        {
            ISerialCommunication serialCommunication;
            if (_SerialMap.TryGetValue(port, out serialCommunication))
            {
                if (serialCommunication.Active)//该串口是否激活
                {
                    serialCommunication.DataPool.AddPackage(package);
                    return true;
                }
                return false;
            }
            return false;
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        void IDisposable.Dispose()
        {
            RemoveAllPorts();
        }

        #endregion
    }
}