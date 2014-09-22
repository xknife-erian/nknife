using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace Gean.Net.Interfaces.Buffer
{
    /// <summary>
    /// 数据包组合类（此类是线程安全的）
    /// 保持数据包完整性。通过互联网发送数据包，实际上是将一个较大的包拆分成诺干小包，
    /// 此类的功能就是将若干小包重新组合成完整的数据包。
    /// </summary>
    public class BufferList
    {
        private object _Lock = new object();
        private int _Current;
        private int _Lengt;
        private int _Vlent;

        /// <summary>
        /// 数据包有可能出现的最大长度。如果不想服务器被人攻击到内存崩溃请按实际情况设置
        /// </summary>
        public int MaxSize { get; private set; }

        /// <summary>
        /// 数据包列表
        /// </summary>
        public List<byte> ByteList { get; private set; }

        /// <summary>
        /// 数据包组合类
        /// </summary>
        /// <param name="maxSize">数据包有可能出现的最大长度。</param>
        public BufferList(int maxSize)
        {
            MaxSize = maxSize;
            _Lengt = -1;
            _Vlent = 0;
            ByteList = new List<byte>();
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _Lengt, -1);
            Interlocked.Exchange(ref _Vlent, 0);
            Interlocked.Exchange(ref _Current, 0);
            ByteList.Clear();

        }

        public bool InsertByteArray(byte[] Data, int ml, out byte[] datax)
        {
            lock (_Lock)
            {
                datax = null;
                ByteList.AddRange(Data);
                Interlocked.Add(ref _Vlent, Data.Length);

                if (_Lengt == -1 && _Vlent > ml)
                {
                    int res = 0;
                    for (int i = 0; i < ml; i++)
                    {
                        int temp = ((int)ByteList[_Current + i]) & 0xff;
                        temp <<= i * 8;
                        res = temp + res;
                    }
                    if (res > MaxSize)
                    {
                        Reset();
                        throw new Exception("数据包大于预设长度，如果你传入的数据比较大，请设置重新 maxSize 值");
                    }
                    if (res <= 0)
                    {
                        Reset();
                        return false;
                    }
                    Interlocked.Exchange(ref _Lengt, res);
                }
                if ((_Vlent - _Current) >= _Lengt)
                {
                    int lengx = _Lengt;
                    Interlocked.Exchange(ref _Lengt, -1);
                    datax = new byte[lengx];
                    ByteList.CopyTo(_Current, datax, 0, lengx);
                    Interlocked.Add(ref _Current, lengx);
                    if (_Current == ByteList.Count)
                    {
                        Reset();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
