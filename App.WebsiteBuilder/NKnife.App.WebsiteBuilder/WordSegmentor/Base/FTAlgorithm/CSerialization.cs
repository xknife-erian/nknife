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
    /// ��װ�Զ����һϵ�����л��ͷ����л�����
    /// ���ö����Ʒ�ʽ���л��ͷ����л���ʵ��
    /// �Ȳ���XML��ʽ�콫��20�����������Ʒ�ʽ
    /// �����Ժͽṹ�������ã�ʹ���߿ɸ���ʵ�����
    /// �Լ�ѡ��
    /// </summary>
    public class CSerialization
    {
        /// <summary>
        /// ���л�ΪXML��ʽ��������
        /// </summary>
        /// <param name="Obj">Ҫ���л��Ķ���</param>
        /// <param name="Encode">XML���ַ����뷽ʽ</param>
        /// <returns>���л����������</returns>
        /// <exception cref="NotImplementedException">
        /// ��δ����������д�÷���ʱ����ͼ�Ը÷������з��ʡ�
        /// </exception>
        /// <remarks>
        /// ˽���ֶν����ᱻ���л�������XML��ʽ���л�������
        /// ��һ��һ��Ҫע�⣡
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
        /// ���������л�ΪUTF-8���͵�XML��ʽ������
        /// </summary>
        /// <param name="Obj">Ҫ���л��Ķ���</param>
        /// <returns>���л����������</returns>
        /// <exception cref="NotImplementedException">
        /// ��δ����������д�÷���ʱ����ͼ�Ը÷������з��ʡ�
        /// </exception>
        /// 
        /// <remarks>
        /// ˽���ֶν����ᱻ���л�������XML��ʽ���л�������
        /// ��һ��һ��Ҫע�⣡
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
        /// ����������XML��ʽ�����л�Ϊ����
        /// </summary>
        /// <param name="In">������</param>
        /// <param name="ObjType">���������</param>
        /// <returns>�����л���Ķ���</returns>
        /// <exception cref="InvalidOperationException">
        /// �����л��ڼ䷢������ʹ�� InnerException ����ʱ��ʹ��ԭʼ�쳣�� 
        /// </exception>
        ///            TestXml Obj = new TestXml();
        ///            Obj.a = "��1��";
        ///            Obj.i = 100;
        ///            try
        ///            {
        ///                 Stream In = General.CSerialization.SerializeXml(Obj);
        ///            }
        ///            catch
        ///            {
        ///            }
        ///
        ///            TestBinary Obj_expected = new TestBinary(); // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///            Obj_expected.a = "��1��";
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
        /// ���������л�Ϊ��������
        /// </summary>
        /// <param name="Obj">Ҫ���л��Ķ���</param>
        /// <returns>��������</returns>
        /// <exception cref="NotImplementedException">
        /// ��δ����������д�÷���ʱ����ͼ�Ը÷������з��ʡ�
        /// </exception>
        /// <remarks>
        /// �������ʹ�� [Serializable] ��������
        /// </remarks>
        /// <example>
        ///            TestBinary Obj = new TestBinary();
        ///            Obj.a = "��1��";
        ///            Obj.i = 100;
        ///            Stream In = General.CSerialization.SerializeBinary(Obj);
        ///
        ///            TestBinary Obj_expected = new TestBinary(); // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///            Obj_expected.a = "��1��";
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
        /// ���������������л�Ϊ����
        /// </summary>
        /// <param name="In">��������</param>
        /// <param name="Obj">�������</param>
        /// <exception cref="InvalidOperationException">
        /// �����л��ڼ䷢������ʹ�� InnerException ����ʱ��ʹ��ԭʼ�쳣�� 
        /// </exception>
        /// <example>
        ///            TestBinary Obj = new TestBinary();
        ///            Obj.a = "��1��";
        ///            Obj.i = 100;
        ///            Stream In = General.CSerialization.SerializeBinary(Obj);
        ///
        ///            TestBinary Obj_expected = new TestBinary(); // TODO: ��ʼ��Ϊ�ʵ���ֵ
        ///            Obj_expected.a = "��1��";
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
