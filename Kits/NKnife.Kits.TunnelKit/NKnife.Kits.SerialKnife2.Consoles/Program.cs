using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SerialKnife2;

namespace NKnife.Kits.SerialKnife2.Consoles
{
    class Program
    {
        static void Main(string[] args)
        {
            var serial = new SerialPortSharp();
            serial.PortName = "COM10";
            serial.Open();
            serial.DataRecieved += Serial_DataRecieved;
        }

        private static void Serial_DataRecieved(object sender, RecievedDataEventArgs e)
        {
            
        }
    }
}
