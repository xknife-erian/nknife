﻿using System;
using System.Threading;
using NKnife.NSerial.Base;
using NLog;

namespace NKnife.NSerial
{
    /// <summary>串口通讯器。每个实例绑定一个端口。
    /// </summary>
    internal class SerialCommunication : ISerialCommunication
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        private readonly bool _EnableDetialLogger;
        private readonly ManualResetEventSlim _QSendWaitEvent = new ManualResetEventSlim(false);
        private readonly ISerialPortWrapper _SerialComm;
        private bool _Active;
        private ISerialDataPool _DataPool;
        private string _PortName;

        public SerialCommunication(bool enableDetialLog = false, SerialType serialType = SerialType.API)
        {
            _EnableDetialLogger = enableDetialLog;
            switch (serialType)
            {
                case SerialType.DotNet:
                    _SerialComm = new SerialPortWrapperDotNet();
                    break;
                default:
                    _SerialComm = new SerialPortWrapper();
                    break;
            }
        }

        #region ISerialCommunication

        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="port">用序号表达一个串口</param>
        /// <returns></returns>
        public bool OpenPort(ushort port)
        {
            _PortName = string.Format("COM{0}", port);
            if (_SerialComm.InitPort(_PortName))
            {
                _Active = true;
                _DataPool = new SerialDataPool();
                _Logger.Info(string.Format("通讯串口{0}打开成功", _PortName));

                var queryThread = new Thread(QuerySendLoop) {IsBackground = true};
                queryThread.Start();
                return true;
            }
            _Active = false;
            _Logger.Info("通讯串口" + _PortName + "打开失败");
            return false;
        }

        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <returns></returns>
        public bool ClosePort()
        {
            try
            {
                if (_SerialComm.IsOpen)
                    _SerialComm.Close();
                _Active = false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 该串口是否激活
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active
        {
            get { return _Active; }
        }

        /// <summary>
        /// 向串口即将发送的指令包的集合
        /// </summary>
        public ISerialDataPool DataPool
        {
            get { return _DataPool ?? (_DataPool = new SerialDataPool()); }
        }

        #endregion

        /// <summary>指令发送的循环线程
        /// </summary>
        private void QuerySendLoop()
        {
            while (_Active)
            {
                SendProcess();
                Thread.Sleep(1);
            }
        }

        /// <summary>串口数据发送函数
        /// 从数据池中检索数据，没有数据包则直接返回，
        /// 有数据包则先按照数据包中的发送准备时长进行延时等候（PreSleepBeforeSendData），
        /// 然以将数据从串口发出，待数据接收超时后，激发数据包发送完成事件
        /// </summary>
        private void SendProcess()
        {
            try
            {
                PackageBase package;
                int packageType = 0; //packageType 1=单向，2=双向,3=轮询
                if (!_DataPool.TryGetPackage(out package,out packageType))
                {
                    return;
                }

                PreSleepBeforeSendData(package.SendInterval.PreSendInterval);
                SetReadTimeOutAccordingToDevice(packageType == 1
                                                    ? TimeSpan.FromMilliseconds(10)
                                                    : package.SendInterval.ReadTimeoutInterval);

                byte[] received;
                var recvCount = _SerialComm.SendData(package.DataToSend, out received);

                SendLogger(package);
                
                SleepAfterReadData(package.SendInterval.AfterReadInterval);

                package.OnPackageSent(PackageSentEventArgs(received, package, recvCount));

            }
            catch (Exception e)
            {
                _Logger.WarnException("SendProcess异常", e);
            }
        }

        /// <summary>打印日志，根据不同的情况
        /// </summary>
        /// <param name="package"></param>
        private void SendLogger(PackageBase package)
        {
            if (_EnableDetialLogger)
            {
                _Logger.Trace(() => string.Format("串发:{0}", package.DataToSend.ToHex()));
            }
            else if (package.GetType() == typeof (OneWayPackage))
            {
                _Logger.Debug(() =>
                              string.Format("串发:{0}, {1},{2}",
                                            package.DataToSend.ToHex(),
                                            package.GetType().Name,
                                            package.SendInterval));
            }
            else if (package.GetType() == typeof (TwoWayPackage))
            {
                var twowayPackage = (TwoWayPackage) package;
                _Logger.Debug(() =>
                              string.Format("串发:{0}, {1},{2},{3}",
                                            package.DataToSend.ToHex(),
                                            package.GetType().Name,
                                            package.SendInterval,
                                            twowayPackage.PackageId));
            }
        }

        /// <summary>数据发送前延时
        /// </summary>
        /// <param name="preSendInterval"></param>
        private void PreSleepBeforeSendData(TimeSpan preSendInterval)
        {
            var duration = (int) preSendInterval.TotalMilliseconds;

            if (duration > 0)
            {
                _QSendWaitEvent.Reset();
                //阻塞等待消息
                _QSendWaitEvent.Wait(duration);
            }
        }

        private void SleepAfterReadData(TimeSpan afterReadInterval)
        {
            var duration = (int)afterReadInterval.TotalMilliseconds;

            if (duration > 0)
            {
                _QSendWaitEvent.Reset();
                //阻塞等待消息
                _QSendWaitEvent.Wait(duration);
            }
        }

        private void SetReadTimeOutAccordingToDevice(TimeSpan afterSendInterval)
        {
            var duration = (int) afterSendInterval.TotalMilliseconds;
            _SerialComm.SetTimeOut(duration);
        }

        private static PackageSentEventArgs PackageSentEventArgs(byte[] received, PackageBase package, int recvCount)
        {
            var pk = package as TwoWayPackage;
            var id = pk == null ? 0 : pk.PackageId;
            if (recvCount > 0)
            {
                //if (package.GetType() == typeof (TwoWayPackage))
                //    id = ((TwoWayPackage) package).PackageId;
                return new PackageSentEventArgs(package.Port, true, received, id);
            }
            return new PackageSentEventArgs(package.Port, false, null,id);
        }
    }
}