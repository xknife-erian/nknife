using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialKnife.Common;
using SerialKnife.Wrappers;
using SerialKnife2;

namespace NKnife.Kits.SerialKnife2.Consoles
{
    class Program
    {
        static void Main(string[] args)
        {
            var serial = new SerialPortWrapperDotNet();
            serial.Initialize("COM10", new SerialConfig());
            byte[] r = null;
            var n = serial.SendReceived(Encoding.Default.GetBytes("abcd"), out r);


            Console.ReadLine();
        }

        private static void Serial_DataRecieved(object sender, RecievedDataEventArgs e)
        {
            Console.WriteLine(e.RecievedData);
        }
    }
}
