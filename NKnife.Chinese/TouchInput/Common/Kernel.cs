using System;
using System.Drawing;
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
            string data = e.Data.ToLower().Replace("@", "");
            string[] command = data.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            short mode = 0;
            if (command.Length < 1 || !(short.TryParse(command[0], out mode)))
            {
                _Logger.Warn("不识别的指令:{0}", e.Data);
                return;
            }
            int x = 0;
            int y = 0;
            if (command.Length >= 3)
            {
                int.TryParse(command[1], out x);
                int.TryParse(command[2], out y);
            }

            ThreadPool.QueueUserWorkItem(CallTouchInput, Command.Build(mode, x, y));
        }

        private void CallTouchInput(object state)
        {
            var command = (Command) state;
            try
            {
                //1.拼音;2.手写;3.符号;4.小写英文;5.大写英文;6.数字
                switch (command.Mode)
                {
                    case -1:
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
            public int X { get; private set; }
            public int Y { get; private set; }

            public static Command Build(short mode, int x, int y)
            {
                var command = new Command {Mode = mode, X = x, Y = y};
                return command;
            }
        }
    }
}