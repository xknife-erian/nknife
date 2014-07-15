using System;
using System.Drawing;
using NKnife.Interface;
using NLog;
using SocketKnife;

namespace NKnife.Chinese.TouchInput
{
    public class Kernel
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();
        private AsynListener _Listener;
        private ITouchInput _TouchInput;

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

            short c = 0;
            if (command.Length < 1 || !(short.TryParse(command[0], out c)))
            {
                _Logger.Warn("不识别的指令:{0}", e.Data);
                return;
            }
            int x = 0, y = 0;
            if (command.Length >= 3)
            {
                int.TryParse(command[1], out x);
                int.TryParse(command[2], out y);
            }

            var location = new Point(x, y);
            try
            {
                //1.拼音;2.手写;3.符号;4.小写英文;5.大写英文;6.数字
                switch (c)
                {
                    case -1:
                        _TouchInput.Exit();
                        break;
                    case 0:
                        _TouchInput.HideInputView();
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        _TouchInput.ShowInputView(c, location);
                        break;
                    default:
                        _Listener.Send(e.Client, e.Data);
                        break;
                }
            }
            catch (Exception exception)
            {
                _Logger.Info(exception.Message, exception);
            }
        }
    }
}