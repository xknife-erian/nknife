using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Jeelu.WordSegmentor
{
    /// <summary>
    /// ��ȡ��ǰ�������еĹ���·��������
    /// ���಻���б��̳�
    /// </summary>
    public sealed class CGetCurrentDir
    {
        private static CGetCurrentDir soGetCurrentDir = new CGetCurrentDir();
        private static String _ProcessDir;

        private CGetCurrentDir ()
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.GetCurrentProcess();
            _ProcessDir = p.MainModule.FileName;
            _ProcessDir = CFile.GetPath(_ProcessDir);
        }

        /// <summary>
        /// ���̵�����·��
        /// </summary>
        static public String ProcessDirectory
        {
            get
            {
                return _ProcessDir;
            }
        }

        /// <summary>
        /// ��ȡ���õ�ǰ�Ĺ���·��
        /// </summary>
        static public String CurrentDirectory 
        {
            set
            {
                Environment.CurrentDirectory = value;
            }

            get
            {
                return Environment.CurrentDirectory + "\\"; 
            }
        }
    };

    /// <summary>
    /// Ŀ¼������
    /// </summary>
    public class CDir
    {
        /// <summary>
        /// ��ȡϵͳĿ¼����ȫ�޶�·��,·������"\"��β
        /// ���磬����ֵΪ�ַ�����C:\WinNT\System32\����
        /// </summary>
        public static String SystemDirectory
        {
            get
            {
                return Environment.SystemDirectory + "\\";
            }
        }

        /// <summary>
        /// ϵͳ������������
        /// </summary>
        public static Char SystemDriver
        {
            get
            {
                return Environment.SystemDirectory[0];
            }
        }

        /// <summary>
        /// ����Ŀ¼ 
        /// </summary>
        /// <param name="dir">Ŀ¼��</param>
        public static void CreateDirectory(String dir)
        {
            Directory.CreateDirectory(dir);
        }

        /// <summary>
        /// Ŀ¼�Ƿ����
        /// </summary>
        /// <param name="dir">Ŀ¼��</param>
        /// <returns>���ڷ���true</returns>
        public static bool DirectoryExists(String dir)
        {
            return Directory.Exists(dir);
        }
    };


    /// <summary>
    /// �ļ�������
    /// </summary>
    public class CFile
    {
        /// <summary>
        /// �ƶ��ļ� 
        /// </summary>
        /// <param name="oldFile"></param>
        /// <param name="newFile"></param>
        static public void Move(String oldFile, String newFile)
        {
            File.Move(oldFile, newFile);
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="oldFile"></param>
        /// <param name="newFile"></param>
        static public void Copy(String oldFile, String newFile)
        {
            File.Copy(oldFile, newFile);
        }

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="fileName"></param>
        static public void Delete(String fileName)
        {
            File.Delete(fileName);
        }

        /// <summary>
        /// �ļ��Ƿ����
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <returns>���ڷ���true</returns>
        static public bool Exists(String fileName)
        {
            return File.Exists(fileName);
        }

        /// <summary>
        /// ��ȡ�ļ���С
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static public Int64 GetFileLength(String fileName)
        {
            FileInfo myInfo = new FileInfo(fileName);
            return myInfo.Length; 
        }

        /// <summary>
        /// ��ȡ�ļ����һ���޸�ʱ��
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <returns>�����޸�ʱ��</returns>
        static public DateTime GetLastWriteTime(String fileName)
        {
            return File.GetLastWriteTime(fileName);
        }

        /// <summary>
        /// ��ȡ�ļ�����ʱ��
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <returns>���ش���ʱ��</returns>
        static public DateTime GetCreationTime(String fileName)
        {
            return File.GetCreationTime(fileName);
        }


        /// <summary>
        /// ���ļ���ȡ���ڴ�����
        /// </summary>
        /// <param name="FileName">�ļ���</param>
        /// <returns>�ڴ���</returns>
        /// <exception cref="CFileException">
        /// ����ʧ�ܴ������� ExceptionCode.FileReadFail�쳣
        /// </exception>
        static public MemoryStream ReadFileToStream(String FileName)
        {
            byte[] Bytes = new byte[32768];
            int read = 0;
            int offset = 0;
            FileStream fs = null;
            bool FileOpened = false ;
            
            try 
            {
                MemoryStream mem = new MemoryStream();
                fs = new FileStream(FileName, FileMode.Open , FileAccess.Read);
                FileOpened = true ;
                mem.Position = 0;

                while ((read = fs.Read(Bytes, 0, Bytes.Length)) > 0)
                {
                    offset += read;
                    mem.Write(Bytes, 0, read);
                }

                fs.Close();
                return mem;            
            }
            catch (Exception e)
            {
                if (FileOpened)
                {
                    try
                    {
                        fs.Close();
                    }
                    catch 
                    {
                    }
                }

                CFileException fse = new CFileException(ExceptionCode.FileReadFail
                    ,String.Format("Read File : {0} Fail! ErrMsg={1}",FileName, e.Message), e);
                fse.Raise();
                return null;
            }
            
        }

        /// <summary>
        /// �ļ����뵽�ַ�������
        /// </summary>
        /// <param name="FileName">�ļ���</param>
        /// <param name="buf">����ַ�����</param>
        /// <param name="Encode">
        /// ���뷽ʽ 
        /// �� "gb2312" "utf-8"
        /// </param>        
        /// <exception cref="CFileException">
        /// ����ʧ�ܴ������� ExceptionCode.FileReadFail�쳣
        /// </exception>
        static public void ReadFileToChars(String FileName, out Char[] buf, String Encode)
        {
            bool FileOpened = false ;
            FileStream fs = null;
            buf = null;

            try
            {
                fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                CStream.ReadStreamToChars(fs, out buf, Encode);
                fs.Close();
            }
            catch (Exception e)
            {
                if (FileOpened)
                {
                    try
                    {
                        fs.Close();
                    }
                    catch 
                    {
                    }
                }

                CFileException fse = new CFileException(ExceptionCode.FileReadFail
                    ,String.Format("Read File : {0} Fail!",FileName), e);
                fse.Raise();
            }

        }

        /// <summary>
        /// ���ļ����뵽һ��String������
        /// </summary>
        /// <param name="FileName">�ļ���</param>
        /// <param name="Encode">���뷽ʽ</param>
        /// <returns>String ����</returns>
        /// <exception cref="CFileException">
        /// ����ʧ�ܴ������� ExceptionCode.FileReadFail�쳣
        /// </exception>
        static public String ReadFileToString(String FileName, String Encode)
        {
            bool FileOpened = false;
            FileStream fs = null;
            String str;

            try
            {
                fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                CStream.ReadStreamToString(fs, out str, Encode);
                fs.Close();
                return str;
            }
            catch (Exception e)
            {
                if (FileOpened)
                {
                    try
                    {
                        fs.Close();
                    }
                    catch
                    {
                    }
                }

                CFileException fse = new CFileException(ExceptionCode.FileReadFail
                    , String.Format("Read File : {0} Fail!", FileName), e);
                fse.Raise();
                return null;
            }
        }


        /// <summary>
        /// ���ļ����뵽һ��StringBuilder������
        /// </summary>
        /// <param name="FileName">�ļ���</param>
        /// <param name="Encode">���뷽ʽ</param>
        /// <returns>StringBuilder ����</returns>
        /// <exception cref="CFileException">
        /// ����ʧ�ܴ������� ExceptionCode.FileReadFail�쳣
        /// </exception>
        static public StringBuilder ReadFileToStringBuilder(String FileName, String Encode)
        {
            String temp;
            temp = ReadFileToString(FileName, Encode);

            StringBuilder ret = new StringBuilder(temp);
            return ret;
        }

        /// <summary>
        /// ���ڴ���������ļ�
        /// </summary>
        /// <param name="FileName">�ļ���</param>
        /// <param name="In">�ڴ���</param>
        /// <exception cref="CFileException">
        /// д��ʧ�ܴ������� ExceptionCode.FileReadFail�쳣
        /// </exception>
        public static void WriteStream(String FileName, MemoryStream In)
        {
            bool FileOpened = false;
            FileStream fs = null;

            try
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }

                fs = new FileStream(FileName, FileMode.CreateNew);
                In.WriteTo(fs);
                fs.Close();
            }
            catch (Exception e)
            {
                if (FileOpened)
                {
                    try
                    {
                        fs.Close();
                    }
                    catch
                    {
                    }
                }

                CFileException fse = new CFileException(ExceptionCode.FileWriteFail
                    , String.Format("Write File : {0} Fail!", FileName), e);
                fse.Raise();
            }

        }

        /// <summary>
        /// ���ַ����������ļ���
        /// </summary>
        /// <param name="str"></param>
        /// <param name="FileName"></param>
        /// <param name="Encode"></param>
        /// <remarks>
        /// ���øú���Դ�ļ����ᱻ���
        /// </remarks>
        /// <exception cref="CFileException">
        /// д��ʧ�ܴ������� ExceptionCode.FileReadFail�쳣
        /// </exception>
        public static void WriteString(String FileName, String str, String Encode)
        {
            TextWriter writer = null;
            bool FileOpened = false;
            FileStream fs = null;

            try
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }

                fs = new FileStream(FileName, FileMode.CreateNew);
                writer = new StreamWriter(fs, Encoding.GetEncoding(Encode));
                writer.Write(str);
                writer.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                if (FileOpened)
                {
                    try
                    {
                        fs.Close();
                    }
                    catch
                    {
                    }
                }

                CFileException fse = new CFileException(ExceptionCode.FileWriteFail
                    , String.Format("Write File : {0} Fail!", FileName), e);
                fse.Raise();
            }
        }

        /// <summary>
        /// ���ļ�ĩβ׷��һ��
        /// </summary>
        /// <param name="str">Ҫ׷�ӵ��ַ���</param>
        /// <param name="FileName">�ļ���</param>
        /// <param name="Encode">���뷽ʽ</param>
        /// <exception cref="CFileException">
        /// д��ʧ�ܴ������� ExceptionCode.FileReadFail�쳣
        /// </exception>
        public static void WriteLine(String FileName, String str, String Encode)
        {
            TextWriter writer = null;
            bool FileOpened = false;
            FileStream fs = null;

            try
            {
                if (File.Exists(FileName))
                {
                    fs = new FileStream(FileName, FileMode.Append);
                }
                else
                {
                    fs = new FileStream(FileName, FileMode.CreateNew);
                }

                
                fs.Seek(0, SeekOrigin.End);
                writer = new StreamWriter(fs, Encoding.GetEncoding(Encode));
                writer.WriteLine(str);
                writer.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                if (FileOpened)
                {
                    try
                    {
                        fs.Close();
                    }
                    catch
                    {
                    }
                }

                CFileException fse = new CFileException(ExceptionCode.FileWriteFail
                    , String.Format("Write File : {0} Fail!", FileName), e);
                fse.Raise();
            }
        }

        /// <summary>
        /// ��ȡ����·��
        /// </summary>
        /// <param name="BasePath">��׼·��</param>
        /// <param name="RelPath">���·��</param>
        /// <returns>����·��������·����"\"��β</returns>
        /// <exception>
        /// ArgumentException
        /// SecurityException
        /// ArgumentNullException
        /// NotSupportedException
        /// PathTooLongException
        /// �����Path.GetFullPath�İ���
        /// </exception>
        public static String GetAbsPath(String BasePath, String RelPath)
        {
            Environment.CurrentDirectory = BasePath;
            String ret = Path.GetFullPath(RelPath);
            if (ret[ret.Length - 1] != '\\' && ret[ret.Length - 1] != '/')
            {

                ret += "\\";
            }

            return ret ;
        }

        /// <summary>
        /// ��ȡ���ļ���
        /// </summary>
        /// <param name="PathName"></param>
        /// <returns>���ļ���</returns>
        /// <exception>
        /// ArgumentException
        /// SecurityException
        /// ArgumentNullException
        /// NotSupportedException
        /// PathTooLongException
        /// �����Path.GetFileName�İ���
        /// </exception>        
        public static String GetFileName(String PathName)
        {
            return Path.GetFileName(PathName);
        }

        /// <summary>
        /// ��ȡ������չ���Ĵ��ļ���
        /// </summary>
        /// <param name="PathName"></param>
        /// <returns>������չ���Ĵ��ļ���</returns>
        /// <exception>
        /// ArgumentException
        /// SecurityException
        /// ArgumentNullException
        /// NotSupportedException
        /// PathTooLongException
        /// �����Path.GetFileNameWithoutExtension�İ���
        /// </exception>        
        public static String GetFileNameWithoutExt(String PathName)
        {
            return Path.GetFileNameWithoutExtension(PathName);
        }

        /// <summary>
        /// ��ȡ�ļ���չ��
        /// </summary>
        /// <param name="PathName"></param>
        /// <returns>
        /// �ļ���չ������չ������ǰ���.
        /// ������ a.txt ������ .txt
        /// </returns>
        /// <exception>
        /// ArgumentException
        /// SecurityException
        /// ArgumentNullException
        /// NotSupportedException
        /// PathTooLongException
        /// �����Path.GetExtension�İ���
        /// </exception>        
        public static String GetExt(String PathName)
        {
            return Path.GetExtension(PathName);
        }

        /// <summary>
        /// ��ȡ�ļ���·����
        /// </summary>
        /// <param name="PathName"></param>
        /// <returns>�����ļ���·�����������ļ�����·����"\"��β</returns>
        /// <exception>
        /// ArgumentException
        /// SecurityException
        /// ArgumentNullException
        /// NotSupportedException
        /// PathTooLongException
        /// �����Path.GetDirectoryName�İ���
        /// </exception>        
        public static String GetPath(String PathName)
        {
            String ret = Path.GetDirectoryName(PathName);
            if (ret[ret.Length - 1] != '\\')
            {

                ret += "\\";
            }

            return ret;
        }

        /// <summary>
        /// ��Base64������ַ���ת��Ϊʵ�����ݴ����ļ�
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <param name="data">Base64������ַ���</param>
        /// <remarks>�ļ��������ܴ����쳣</remarks>
        static public void WriteFileByBase64String(String fileName, String data)
        {
            byte[] buf = Convert.FromBase64String(data);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            fs.Write(buf, 0, buf.Length);
            fs.Close();
        }


        /// <summary>
        /// ���ļ����ݶ���Base64������ַ�����
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <returns>Base64������ַ���</returns>
        /// <remarks>�ļ��������ܴ����쳣</remarks>
        static public String GetBase64StringFromFile(String fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            if (stream == null)
            {
                return null;
            }

            byte[] buf = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(buf, 0, (int)stream.Length);
            stream.Close();
            return Convert.ToBase64String(buf);

        }

        /// <summary>
        /// ��ȡ�ļ��汾��
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <returns>����ļ��а汾�ţ����ذ汾�ţ����򷵻�null</returns>
        static public String GetFileVersionNo(String fileName)
        {
            try
            {
                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(fileName);
                return fileInfo.FileVersion;
            }
            catch
            {
                return null;
            }
        }


    }
}
