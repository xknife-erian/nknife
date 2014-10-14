using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Utility;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Families
{
    public class FixedTailDecoder : IDatagramDecoder
    {
        private static readonly byte[] _tail = Encoding.Default.GetBytes("\n");

        protected virtual byte[] GetTail()
        {
            return _tail;
        }

        public string[] Execute(byte[] data, out int done)
        {
            done = data.Length;
            data.Contains(GetTail())
            string src = UtilityString.TidyUTF8(data);
            string[] result = src.Contains("ö") ? src.Split(new[] { 'ö' }, StringSplitOptions.RemoveEmptyEntries) : new[] { src };
            if (result.Length > 1)
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = UtilityString.TidyUTF8(result[i]).Trim();
                }
            }
            return result;
        }
    }
}
