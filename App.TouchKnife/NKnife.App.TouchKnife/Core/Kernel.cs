using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Common.Logging;
using NKnife.Configuring.Interfaces;
using NKnife.Interface;
using NKnife.IoC;
using SocketKnife;

namespace NKnife.App.TouchKnife.Core
{
    public class Kernel
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
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
                _logger.Info("屏幕输入法窗体实例成功.");
                _Listener = new AsynListener();
                _Listener.ReceivedData += Listener_ReceivedData;
                _Listener.BeginListening();
                _logger.Info("屏幕输入法控制监听器启动完成.");
                return true;
            }
            catch (Exception e)
            {
                _logger.Info(e.Message, e);
                return false;
            }
        }

        public bool Finish()
        {
            try
            {
                _Listener.EndListening();
                _logger.Info("屏幕输入法控制监听器关闭完成.");

                return true;
            }
            catch (Exception e)
            {
                _logger.Info(e.Message, e);
                return false;
            }
        }

        private void Listener_ReceivedData(object sender, AsynListener.ReceivedDataEventArgs e)
        {
            _logger.Info(string.Format("收到控制:{0}", e.Data.ToLower()));
            string command = e.Data.ToLower().Replace(_Listener.Tail.ToString(), "");
            if (command.IndexOf("keepalivetestfromclient", StringComparison.Ordinal)>=0)
            {
                const string RESPONSE = "KeepAliveTestFromServer@";
                _Listener.Send(e.Client, RESPONSE);
                _logger.Trace(string.Format("心跳回复:{0}", RESPONSE));
                return;
            }
            if (!Regex.IsMatch(command, @"\d{16}"))
            {
                _logger.Warn(string.Format("不识别的指令:{0}", command));
                return;
            }

            short mode;
            int x;
            int y;
            if (Parse(command, out mode, out x, out y)) 
                return;

            ThreadPool.QueueUserWorkItem(CallTouchInput, Command.Build(mode, x, y));
        }

        private static bool Parse(string command, out short mode, out int x, out int y)
        {
            x = y = 0;
            mode = short.Parse(command[1].ToString(CultureInfo.InvariantCulture));

            int xw = int.Parse(command[3].ToString(CultureInfo.InvariantCulture));
            int yw = int.Parse(command[5].ToString(CultureInfo.InvariantCulture));

            if (xw > 4 || yw > 4)
            {
                _logger.Warn(string.Format("不识别的指令:{0}", command));
                return true;
            }

            string xs = command.Substring(7, xw);
            string ysSrc = command.Substring(command.Length - yw, yw);
            var sb = new StringBuilder(yw);
            for (int i = ysSrc.Length - 1; i >= 0; i--)
            {
                sb.Append(ysSrc[i]);
            }

            string ys = sb.ToString();

            x = 0;
            y = 0;
            if (command.Length >= 3)
            {
                int.TryParse(xs, out x);
                int.TryParse(ys, out y);
            }
            return false;
        }

        private void CallTouchInput(object state)
        {
            var command = (Command) state;
            SetLocation(command);
            _logger.Trace(string.Format("窗体控制:{0},{1},{2}", command.Mode, command.X, command.Y));
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
                _logger.Info(exception.Message, exception);
            }
        }

        private void SetLocation(Command command)
        {
            command.X += int.Parse(DI.Get<IOptionManager>().GetOptionValue("", "OffsetX"));
            command.Y += int.Parse(DI.Get<IOptionManager>().GetOptionValue("", "OffsetY"));
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