using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NKnife.XML
{
    public  class SerializeUtility
    {
        /// <summary>
        /// 序列化成字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>序列化后的字符串</returns>
        public static string Serialiaze(object obj)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.UTF8);
            xtw.Formatting = Formatting.Indented;
            xs.Serialize(xtw, obj);
            ms.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(ms);
            string str = sr.ReadToEnd();
            xtw.Close();
            ms.Close();
            return str;
        }

        /// <summary>
        /// 反序列化 从字符串
        /// </summary>
        /// <param name="xml">xml字符串</param>
        /// <param name="type">要生成的对象类型</param>
        /// <returns>反序列化后的对象</returns>
        public static object Deserialize(string xml, Type type)
        {
            var xs = new XmlSerializer(type);
            var sr = new StringReader(xml);
            object obj = xs.Deserialize(sr);
            return obj;
        }

        /// <summary>
        /// 反序列化，从文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeFile(string filePath, Type type)
        {
            using (var sr = new StreamReader(filePath))
            {
                var xml = sr.ReadToEnd();
                return Deserialize(xml, type);
            }
        }

        public static object GetDeserializationObject(byte[] dataByte)
        {
            try
            {
                var formatter = new BinaryFormatter();

                using (var stream = new MemoryStream(dataByte))
                {
                    return formatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
