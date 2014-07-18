﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NKnife.Interface;
using NLog;
using SocketKnife;

namespace NKnife.Chinese.TouchInput.Common
{
    public class Kernel
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();
        private AsynListener _Listener;
        private ITouchInput _TouchInput;

        static Kernel()
        {
            ThreadPool.SetMaxThreads(16, 16);
        }

        public bool Start(ITouchInput touchInput)
        {
            _TouchInput = touchInput;
            try
            {
                _Logger.Info("屏幕输入法窗体实例成功.");
                _Listener = new AsynListener();
                _Listener.ReceivedData += Listener_ReceivedData;
                _Listener.Listening();
                _Logger.Info("屏幕输入法控制监听器启动完成.");
                return true;
            }
            catch (Exception e)
            {
                _Logger.Info(e.Message, e);
                return false;
            }
        }

        public bool Finish()
        {
            try
            {
                _Listener.Abort();
                _Logger.Info("屏幕输入法控制监听器关闭完成.");

                return true;
            }
            catch (Exception e)
            {
                _Logger.Info(e.Message, e);
                return false;
            }
        }

        private void Listener_ReceivedData(object sender, AsynListener.ReceivedDataEventArgs e)
        {
            _Logger.Info("收到控制:{0}", e.Data.ToLower());
            string command = e.Data.ToLower().Replace("@", "");
            if (command.IndexOf("keepalivetestfromclient", StringComparison.Ordinal)>=0)
            {
                const string RESPONSE = "KeepAliveTestFromServer@";
                _Listener.Send(e.Client, RESPONSE);
                _Logger.Trace("心跳回复:{0}", RESPONSE);
                return;
            }
            if (!Regex.IsMatch(command, @"\d{16}"))
            {
                _Logger.Warn("不识别的指令:{0}", command);
                return;
            }

            short mode = short.Parse(command[1].ToString(CultureInfo.InvariantCulture));

            int xw = int.Parse(command[3].ToString(CultureInfo.InvariantCulture));
            int yw = int.Parse(command[5].ToString(CultureInfo.InvariantCulture));

            if (xw > 4 || yw > 4)
            {
                _Logger.Warn("不识别的指令:{0}", command);
                return;
            }

            string xs = command.Substring(7, xw);
            string ysc = command.Substring(command.Length - yw, yw);
            var sb = new StringBuilder(yw);
            for (int i = ysc.Length - 1; i >= 0; i--)
            {
                sb.Append(ysc[i]);
            }

            string ys = sb.ToString();

            int x = 0;
            int y = 0;
            if (command.Length >= 3)
            {
                int.TryParse(xs, out x);
                int.TryParse(ys, out y);
            }

            ThreadPool.QueueUserWorkItem(CallTouchInput, Command.Build(mode, x, y));
        }

        private void CallTouchInput(object state)
        {
            var command = (Command) state;
            command.Y += 123;
            switch (command.Mode)
            {
                case 1:
                    command.X += 124;
                    break;
                case 2:
                    command.X += 99;
                    break;
                default:
                    command.X += 55;
                    break;
            }
            _Logger.Trace("窗体控制:{0},{1},{2}", command.Mode, command.X, command.Y);
            try
            {
                //1.拼音;2.手写;3.符号;4.小写英文;5.大写英文;6.数字
                switch (command.Mode)
                {
                    case 9:
                        _TouchInput.Exit();
                        break;
                    case 0:
                        _TouchInput.HideInputView();
                        break;
                    default:
                        _TouchInput.ShowInputView(command.Mode, new Point(command.X, command.Y));
                        break;
                }
            }
            catch (Exception exception)
            {
                _Logger.Info(exception.Message, exception);
            }
        }

        private class Command
        {
            public short Mode { get; private set; }
            public int X { get; set; }
            public int Y { get; set; }

            public static Command Build(short mode, int x, int y)
            {
                var command = new Command {Mode = mode, X = x, Y = y};
                return command;
            }
        }
    }
}