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
                _TouchInput.Hide();
                _Logger.Info("屏幕输入法窗体关闭成功.");

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
            var command = e.Data.ToLower().Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            var location = new Point(int.Parse(command[1]), int.Parse(command[2]));
            try
            {
                //0.拼音;1.手写;2.符号;3.小写英文;4.大写英文;5.数字
                switch (command[0])
                {
                    case "-1":
                        _TouchInput.Hide();
                        break;
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                        _TouchInput.Show(ushort.Parse(command[0]), location);
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