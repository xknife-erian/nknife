using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization ;
using System.Runtime.Serialization.Formatters.Binary;

namespace Jeelu.WordSegmentor
{
    /// <summary>
    /// 封装对对象的一系列序列化和反序列化操作
    /// 采用二进制方式序列化和发序列化经实测
    /// 比采用XML方式快将近20倍。但二进制方式
    /// 兼容性和结构化都不好，使用者可根据实际情况
    /// 自己选择。
    /// </summary>
    public class CSerialization
    {
        /// <summary>
        /// 序列化为XML格式的流数据
        /// </summary>
        /// <param name="Obj">要序列化的对象</param>
        /// <param name="Encode">XML的字符编码方式</param>
        /// <returns>序列化后的流数据</returns>
        /// <exception cref="NotImplementedException">
        /// 当未在子类中重写该方法时，试图对该方法进行访问。
        /// </exception>
        /// <remarks>
        /// 私有字段将不会被序列化，这是XML方式序列化的限制
        /// 这一点一定要注意！
        /// </remarks>
        /// 
        /// <example>
        /// struct TestStruct
        /// {
        ///     public int a ;
        /// }
        /// 
        /// TestStruct testOut ;
        /// test.a = 1 ;
        /// 
        /// try
        /// {
        ///     Stream s = CSerialization.SerializeXml(test,"utf-8")
        /// }
        /// catch
        /// {
        /// }
        /// 
        /// TestStruct testIn ;
        /// 
        /// try
        /// {
        ///     CSerialization.DeserializeXml(s, out testIn);
        /// }
        /// catch
        /// {
        /// }
        /// </example>
        public static Stream SerializeXml(object Obj, String Encode)
        {
            TextWriter writer = null;
            MemoryStream s = new MemoryStream();
            writer = new StreamWriter(s, Encoding.GetEncoding(Encode));

            XmlSerializer ser = new XmlSerializer(Obj.GetType());

            ser.Serialize(writer, Obj);
            s.Position = 0;
            return s;

        }

        /// <summary>
        /// 将对象序列化为UTF-8类型的XML格式数据流
        /// </summary>
        /// <param name="Obj">要序列化的对象</param>
        /// <returns>序列化后的流数据</returns>
        /// <exception cref="NotImplementedException">
        /// 当未在子类中重写该方法时，试图对该方法进行访问。
        /// </exception>
        /// 
        /// <remarks>
        /// 私有字段将不会被序列化，这是XML方式序列化的限制
        /// 这一点一定要注意！
        /// </remarks>
        /// 
        /// <example>
        /// struct TestStruct
        /// {
        ///     public int a ;
        /// }
        /// 
        /// TestStruct testOut ;
        /// test.a = 1 ;
        /// 
        /// try
        /// {
        ///     Stream s = CSerialization.SerializeXml(test)
        /// }
        /// catch
        /// {
        /// }
        /// 
        /// TestStruct testIn ;
        /// 
        /// try
        /// {
        ///     CSerialization.DeserializeXml(s, out testIn);
        /// }
        /// catch
        /// {
        /// }
        /// </example>
        public static Stream SerializeXml(object Obj)
        {
            return SerializeXml(Obj, "UTF-8");
        }

        /// <summary>
        /// 将数据流按XML方式反序列化为对象
        /// </summary>
        /// <param name="In">数据流</param>
        /// <param name="ObjType">对象的类型</param>
        /// <returns>反序列化后的对象</returns>
        /// <exception cref="InvalidOperationException">
        /// 反序列化期间发生错误。使用 InnerException 属性时可使用原始异常。 
        /// </exception>
        ///            TestXml Obj = new TestXml();
        ///            Obj.a = "中1局";
        ///            Obj.i = 100;
        ///            try
        ///            {
        ///                 Stream In = General.CSerialization.SerializeXml(Obj);
        ///            }
        ///            catch
        ///            {
        ///            }
        ///
        ///            TestBinary Obj_expected = new TestBinary(); // TODO: 初始化为适当的值
        ///            Obj_expected.a = "中1局";
        ///            Obj_expected.i = 100;
        ///
        ///            TestXml Out = (TestXml) General.CSerialization.DeserializeXml(In, typeof(TestXml));
        /// <example>
        /// </example>
        public static object DeserializeXml(Stream In, Type ObjType)
        {
            In.Position = 0;
            XmlSerializer ser = new XmlSerializer(ObjType);
            return ser.Deserialize(In);
        }

        /// <summary>
        /// 将对象序列化为二进制流
        /// </summary>
        /// <param name="Obj">要序列化的对象</param>
        /// <returns>二进制流</returns>
        /// <exception cref="NotImplementedException">
        /// 当未在子类中重写该方法时，试图对该方法进行访问。
        /// </exception>
        /// <remarks>
        /// 对象必须使用 [Serializable] 属性声明
        /// </remarks>
        /// <example>
        ///            TestBinary Obj = new TestBinary();
        ///            Obj.a = "中1局";
        ///            Obj.i = 100;
        ///            Stream In = General.CSerialization.SerializeBinary(Obj);
        ///
        ///            TestBinary Obj_expected = new TestBinary(); // TODO: 初始化为适当的值
        ///            Obj_expected.a = "中1局";
        ///            Obj_expected.i = 100;
        ///
        ///            object obj;
        ///            General.CSerialization.DeserializeBinary(In, out obj);
        ///            TestBinary Out = (TestBinary)obj;
        /// </example>
        public static Stream SerializeBinary(object Obj)
        {
            MemoryStream s = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, Obj);
            s.Position = 0;
            return s;
        }

        /// <summary>
        /// 将二进制流反序列化为对象
        /// </summary>
        /// <param name="In">二进制流</param>
        /// <param name="Obj">输出对象</param>
        /// <exception cref="InvalidOperationException">
        /// 反序列化期间发生错误。使用 InnerException 属性时可使用原始异常。 
        /// </exception>
        /// <example>
        ///            TestBinary Obj = new TestBinary();
        ///            Obj.a = "中1局";
        ///            Obj.i = 100;
        ///            Stream In = General.CSerialization.SerializeBinary(Obj);
        ///
        ///            TestBinary Obj_expected = new TestBinary(); // TODO: 初始化为适当的值
        ///            Obj_expected.a = "中1局";
        ///            Obj_expected.i = 100;
        ///
        ///            object obj;
        ///            General.CSerialization.DeserializeBinary(In, out obj);
        ///            TestBinary Out = (TestBinary)obj;
        /// </example>
        public static void DeserializeBinary(Stream In, out object Obj)
        {
            In.Position = 0;
            IFormatter formatter = new BinaryFormatter();
            Obj = formatter.Deserialize(In);
        }

    }
}
