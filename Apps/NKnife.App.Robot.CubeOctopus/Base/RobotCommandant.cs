using System;
using System.Text;

namespace NKnife.Tools.Robot.CubeOctopus.Base
{
    class RobotCommandant
    {
        //private ISerialPortWrapper _Serial;
        byte[] _Replay = new byte[0];

        public void Initialize(ushort port, uint timeout = 600)
        {
//            _Serial = new SerialPortWrapperDotNet();
//            _Serial.Initialize("COM" + port, new SerialConfig());
//            _Serial.SetTimeOut((int) timeout);
        }

        public void Stop()
        {
//            if (_Serial.IsOpen)
//                _Serial.Close();
        }

        public void Wait(uint timeout)
        {
//            _Serial.SetTimeOut((int) timeout);
        }

        public void SendCommand(Command cmd)
        {
            var bytesCmd = EncodeCommand(cmd.ToString());
//            _Serial.SendReceived(bytesCmd, out _Replay);
        }

        protected static byte[] EncodeCommand(string cmd)
        {
            var bytes = Encoding.ASCII.GetBytes(cmd);
            var bytesCmd = new byte[bytes.Length + 2];
            Buffer.BlockCopy(bytes, 0, bytesCmd, 0, bytes.Length);
            bytesCmd[bytes.Length] = 0x0D;
            bytesCmd[bytes.Length + 1] = 0x0A;
            return bytesCmd;
        }
    }
}
