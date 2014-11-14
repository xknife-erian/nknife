using System;
using NKnife.Base;
using NKnife.Events;
using SocketKnife.Common;

namespace SocketKnife.Interfaces
{
    public interface IHeartbeatFilter
    {
        /// <summary>
        /// ����Э��
        /// </summary>
        Heartbeat Heartbeat { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        double Interval { get; set; }

        /// <summary>
        /// �ϸ�ģʽ����
        /// </summary>
        /// <returns>
        /// true  ������������һ��Ҫ��HeartBeat���ж����ReplayOfClientһ�²�����������Ӧ
        /// false ���������κ����ݾ�����������Ӧ
        /// </returns>
        bool EnableStrictMode { get; set; }

        /// <summary>
        /// ����ģʽ
        /// </summary>
        bool EnableAggressiveMode { get; set; }
    }
}