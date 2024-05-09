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
using SocketKnife.Common;

namespace NKnife.App.TouchInputKnife.Core
{
    public class Kernel
    {
        private static readonly ILog _logger = LogManager.GetLogger<Kernel>();

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
            _logger.Info($"收到控制:{e.Data.ToLower()}");
            string command = e.Data.ToLower().Replace(_Listener.Tail.ToString(), "");
            if (command.IndexOf("keepalivetestfromclient", StringComparison.Ordinal)>=0)
            {
                const string RESPONSE = "KeepAliveTestFromServer`";
                _Listener.Send(e.Client, RESPONSE);
                _logger.Trace($"心跳回复:{RESPONSE}");
                return;
            }
            if (!Regex.IsMatch(command, @"\d{16}"))
            {
                _logger.Warn($"不识别的指令:{command}");
                return;
            }

            short mode = short.Parse(command[1].ToString(CultureInfo.InvariantCulture));

            int xw = int.Parse(command[3].ToString(CultureInfo.InvariantCulture));
            int yw = int.Parse(command[5].ToString(CultureInfo.InvariantCulture));

            if (xw > 4 || yw > 4)
            {
                _logger.Warn($"不识别的指令:{command}");
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
            SetLocation(command);
            _logger.Trace($"窗体控制:{command.Mode},{command.X},{command.Y}");
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
//            var x = command.X;
//            var screenH = Screen.PrimaryScreen.WorkingArea.Height;
//            command.Y += 54;
//            switch (command.Mode)
//            {
//                case 1:
//                    command.X += 118;
//                    break;
//                case 2:
//                    command.X += 95;
//                    break;
//                default:
//                    command.X += 50;
//                    break;
//            }
//            var tih = _TouchInput.OwnSize.Height;
//            if ((screenH - command.X + 65) < tih)
//            {
//                command.X = (int) (x - tih);
//            }
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