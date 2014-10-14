using System;
using System.Globalization;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Protocols
{
    public class TextPlainUnPackager : IProtocolUnPackager
    {
        public short Version
        {
            get { return 1; }
        }

        public void Execute(ref IProtocolContent content, string data, string family, string command)
        {
            string[] array = data.Split(new[] {'|', '#', '@'}, StringSplitOptions.RemoveEmptyEntries);
            content.Command = command;
            if (array.Length > 1)
                content.CommandParam = array[1];
            if (array.Length > 2)
            {
                for (int i = 2; i < array.Length; i++)
                {
                    content.AddInfo(i.ToString(CultureInfo.InvariantCulture), array[i]);
                }
            }
        }
    }
}