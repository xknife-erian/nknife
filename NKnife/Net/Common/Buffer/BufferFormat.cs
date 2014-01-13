using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gean.Net.Interfaces.Buffer
{
    /// <summary>
    /// 数据包格式化类:将.NET数据转换成通讯数据包
    /// </summary>
    public class BufferFormat
    {
        bool _IsFinish;
        List<byte> _BuffList;

        /// <summary>
        /// 字符串格式化字符编码
        /// </summary>
        public Encoding _Encode { get; private set; }

        /// <summary>
        /// 数据包格式化类
        /// </summary>
        /// <param name="buffType">包类型</param>
        public BufferFormat(int buffType)
        {
            _BuffList = new List<byte>();
            _BuffList.AddRange(GetSocketBytes(buffType));
            _Encode = Encoding.Unicode;
            _IsFinish = false;
        }

        #region 布尔值
        /// <summary>
        /// 添加一个布尔值
        /// </summary>
        /// <param name="data"></param>
        public void AddItem(bool data)
        {
            if (_IsFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            _BuffList.AddRange(GetSocketBytes(data));
        }

        #endregion

        #region 整数
        /// <summary>
        /// 添加一个1字节的整数
        /// </summary>
        /// <param name="data"></param>
        public void AddItem(byte data)
        {
            if (_IsFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            _BuffList.Add(data);
        }

        /// <summary>
        /// 添加一个2字节的整数
        /// </summary>
        /// <param name="data"></param>
        public void AddItem(Int16 data)
        {
            _BuffList.AddRange(GetSocketBytes(data));
        }

        /// <summary>
        /// 添加一个4字节的整数
        /// </summary>
        /// <param name="data"></param>
        public void AddItem(Int32 data)
        {
            if (_IsFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            _BuffList.AddRange(GetSocketBytes(data));
        }

        /// <summary>
        /// 添加一个8字节的整数
        /// </summary>
        /// <param name="data"></param>
        public void AddItem(UInt64 data)
        {
            if (_IsFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            _BuffList.AddRange(GetSocketBytes(data));
        }

        #endregion

        #region 浮点数

        /// <summary>
        /// 添加一个4字节的浮点
        /// </summary>
        /// <param name="data"></param>
        public void AddItem(float data)
        {
            if (_IsFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            _BuffList.AddRange(GetSocketBytes(data));
        }

        /// <summary>
        /// 添加一个8字节的浮点
        /// </summary>
        /// <param name="data"></param>
        public void AddItem(double data)
        {
            _BuffList.AddRange(GetSocketBytes(data));
        }

        #endregion

        #region 数据包

        /// <summary>
        /// 添加一个BYTE[]数据包
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public void AddItem(Byte[] data)
        {
            if (_IsFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            byte[] ldata = GetSocketBytes(data.Length);
            _BuffList.AddRange(ldata);
            _BuffList.AddRange(data);

        }

        #endregion

        #region 字符串

        /// <summary>
        /// 添加一个字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public void AddItem(String data)
        {
            if (_IsFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            Byte[] bytes = _Encode.GetBytes(data);
            _BuffList.AddRange(GetSocketBytes(bytes.Length));
            _BuffList.AddRange(bytes);

        }

        #endregion

        #region 时间

        /// <summary>
        /// 添加一个一个DATATIME
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public void AddItem(DateTime data)
        {
            if (_IsFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            AddItem(data.ToString());
        }

        #endregion

        #region 对象

        /// <summary>
        /// 将一个对象转换为二进制数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void AddItem(object obj)
        {
            if (_IsFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            byte[] data = SerializeObject(obj);
            _BuffList.AddRange(GetSocketBytes(data.Length));
            _BuffList.AddRange(data);
        }

        #endregion

        /// <summary>
        /// 直接格式化一个带FormatClassAttibutes 标签的类，返回BYTE[]数组，此数组可以直接发送不需要组合所数据包。所以也称为类抽象数据包
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static byte[] FormatFCA(object o)
        {
            Type otype = o.GetType();
            Attribute[] Attributes = Attribute.GetCustomAttributes(otype);

            foreach (Attribute p in Attributes)
            {
                FormatClassAttibutes fca = p as FormatClassAttibutes;

                if (fca != null)
                {
                    List<byte> bufflist = new List<byte>();

                    bufflist.AddRange(GetSocketBytes(fca.BufferCmdType));

                    byte[] classdata = SerializeObject(o);
                    bufflist.AddRange(GetSocketBytes(classdata.Length));
                    bufflist.AddRange(classdata);

                    int l = bufflist.Count + 4;
                    byte[] data = GetSocketBytes(l);
                    for (int i = data.Length - 1; i >= 0; i--)
                    {
                        bufflist.Insert(0, data[i]);
                    }

                    byte[] datap = new byte[bufflist.Count];

                    bufflist.CopyTo(0, datap, 0, datap.Length);

                    bufflist.Clear();

                    return datap;
                }
            }

            throw new EntryPointNotFoundException("无法找到 FormatClassAttibutes 标签");
        }

        /// <summary>
        /// 完毕
        /// </summary>
        /// <returns></returns>
        public byte[] Finish()
        {
            if (_IsFinish)
                throw new ObjectDisposedException("BufferFormat", "无法使用已经调用了 Finish 方法的BufferFormat对象");

            int l = _BuffList.Count + 4;
            byte[] data = GetSocketBytes(l);
            for (int i = data.Length - 1; i >= 0; i--)
            {
                _BuffList.Insert(0, data[i]);
            }

            byte[] datap = new byte[_BuffList.Count];

            _BuffList.CopyTo(0, datap, 0, datap.Length);

            _BuffList.Clear();
            _IsFinish = true;

            return datap;
        }

        #region V对象

        /// <summary>
        /// 把对象序列化并返回相应的字节
        /// </summary>
        /// <param name="pObj">需要序列化的对象</param>
        /// <returns>byte[]</returns>
        public static byte[] SerializeObject(object pObj)
        {
            System.IO.MemoryStream memory = new System.IO.MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            // formatter.TypeFormat=System.Runtime.Serialization.Formatters.FormatterTypeStyle.XsdString;
            formatter.Serialize(memory, pObj);
            memory.Position = 0;
            byte[] read = new byte[memory.Length];
            memory.Read(read, 0, read.Length);
            memory.Close();
            return read;
        }

        #endregion

        #region V整数

        /// <summary>
        /// 将一个32位整形转换成一个BYTE[]4字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(Int32 data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个64位整形转换成一个BYTE[]8字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(UInt64 data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个 1位CHAR转换成1位的BYTE[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetSocketBytes(Char data)
        {
            Byte[] bytes = new Byte[] { (Byte)data };
            return bytes;
        }

        /// <summary>
        /// 将一个 16位整数转换成2位的BYTE[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetSocketBytes(Int16 data)
        {
            return BitConverter.GetBytes(data);
        }

        #endregion

        #region V布尔值
        /// <summary>
        /// 将一个布尔值转换成一个BYTE[]
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(bool data)
        {
            return BitConverter.GetBytes(data);
        }
        #endregion

        #region V浮点数
        /// <summary>
        /// 将一个32位浮点数转换成一个BYTE[]4字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetSocketBytes(float data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个64位浮点数转换成一个BYTE[]8字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Byte[] GetSocketBytes(double data)
        {
            return BitConverter.GetBytes(data);
        }
        #endregion
    }
}
