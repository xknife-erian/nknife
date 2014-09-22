using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Gean.Net.Interfaces.Buffer
{
    /// <summary>
    /// 数据包读取类(功能是讲通讯数据包重新转换成.NET 数据类型）
    /// </summary>
    public class ReadBytes
    {
        private int _current;
        private byte[] _data;

        /// <summary>
        /// 数据包长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 当前其位置
        /// </summary>
        public int Postion
        {
            get
            {
                return _current;
            }
            set
            {
                Interlocked.Exchange(ref _current, value);
            }
        }

        public void Reset()
        {
            _current = 0;
        }

        public ReadBytes(Byte[] data)
        {
            _data = data;
            this.Length = _data.Length;
            _current = 0;
        }

        #region 整数
        /// <summary>
        /// 读取内存流中的头2位并转换成整型
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadInt16(out short values)
        {

            try
            {
                values = BitConverter.ToInt16(_data, _current);
                _current = Interlocked.Add(ref _current, 2);
                return true;
            }
            catch
            {
                values = 0;
                return false;
            }
        }


        /// <summary>
        /// 读取内存流中的头4位并转换成整型
        /// </summary>
        /// <param name="ms">内存流</param>
        /// <returns></returns>
        public bool ReadInt32(out int values)
        {
            try
            {
                values = BitConverter.ToInt32(_data, _current);
                _current = Interlocked.Add(ref _current, 4);
                return true;
            }
            catch
            {
                values = 0;
                return false;
            }
        }


        /// <summary>
        /// 读取内存流中的头8位并转换成长整型
        /// </summary>
        /// <param name="ms">内存流</param>
        /// <returns></returns>
        public bool ReadInt64(out long values)
        {
            try
            {
                values = BitConverter.ToInt64(_data, _current);
                _current = Interlocked.Add(ref _current, 8);
                return true;
            }
            catch
            {
                values = 0;
                return false;
            }
        }

        /// <summary>
        /// 读取内存流中的首位
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadByte(out byte values)
        {
            try
            {
                values = (byte)_data[_current];
                _current = Interlocked.Increment(ref _current);
                return true;
            }
            catch
            {
                values = 0;
                return false;
            }
        }

        #endregion

        #region 浮点数


        /// <summary>
        /// 读取内存流中的头4位并转换成单精度浮点数
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadFloat(out float values)
        {

            try
            {
                values = BitConverter.ToSingle(_data, _current);
                _current = Interlocked.Add(ref _current, 4);
                return true;
            }
            catch
            {
                values = 0.0f;
                return false;
            }
        }


        /// <summary>
        /// 读取内存流中的头8位并转换成浮点数
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadDouble(out double values)
        {

            try
            {
                values = BitConverter.ToDouble(_data, _current);
                _current = Interlocked.Add(ref _current, 8);
                return true;
            }
            catch
            {
                values = 0.0;
                return false;
            }
        }


        #endregion

        #region 布尔值
        /// <summary>
        /// 读取内存流中的头1位并转换成布尔值
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadBoolean(out bool values)
        {

            try
            {
                values = BitConverter.ToBoolean(_data, _current);
                _current = Interlocked.Add(ref _current, 1);
                return true;
            }
            catch
            {
                values = false;
                return false;
            }
        }

        #endregion

        #region 字符串
        /// <summary>
        /// 读取内存流中一段字符串
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadString(out string values)
        {
            int lengt;
            try
            {
                if (ReadInt32(out lengt))
                {

                    Byte[] buf = new Byte[lengt];

                    Array.Copy(_data, _current, buf, 0, buf.Length);

                    values = Encoding.Unicode.GetString(buf, 0, buf.Length);

                    _current = Interlocked.Add(ref _current, lengt);

                    return true;

                }
                else
                {
                    values = "";
                    return false;
                }
            }
            catch
            {
                values = "";
                return false;
            }

        }
        #endregion

        #region 数据
        /// <summary>
        /// 读取内存流中一段数据
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public bool ReadByteArray(out byte[] values)
        {
            int lengt;
            try
            {
                if (ReadInt32(out lengt))
                {
                    values = new Byte[lengt];
                    Array.Copy(_data, _current, values, 0, values.Length);
                    _current = Interlocked.Add(ref _current, lengt);
                    return true;
                }
                else
                {
                    values = null;
                    return false;
                }
            }
            catch
            {
                values = null;
                return false;
            }

        }
        #endregion

        #region 对象

        /// <summary>
        /// 把字节反序列化成相应的对象
        /// </summary>
        /// <param name="pBytes">字节流</param>
        /// <returns>object</returns>
        private object DeserializeObject(byte[] pBytes)
        {
            object obj = null;
            if (pBytes == null)
                return obj;
            System.IO.MemoryStream memory = new System.IO.MemoryStream(pBytes);
            memory.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            obj = formatter.Deserialize(memory);
            memory.Close();
            return obj;
        }

        /// <summary>
        /// 读取一个对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ReadObject(out object obj)
        {
            byte[] data;
            if (this.ReadByteArray(out data))
            {
                obj = DeserializeObject(data);
                return true;
            }
            else
            {
                obj = null;
                return false;
            }

        }

        #endregion
    }
}
