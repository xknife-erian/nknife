using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Jeelu.WordSegmentor
{
    /// <summary>
    /// 获取当前进程运行的工作路径名的类
    /// 该类不运行被继承
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
        /// 进程的启动路径
        /// </summary>
        static public String ProcessDirectory
        {
            get
            {
                return _ProcessDir;
            }
        }

        /// <summary>
        /// 获取设置当前的工作路径
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
    /// 目录操作类
    /// </summary>
    public class CDir
    {
        /// <summary>
        /// 获取系统目录的完全限定路径,路径名以"\"结尾
        /// 例如，返回值为字符串“C:\WinNT\System32\”。
        /// </summary>
        public static String SystemDirectory
        {
            get
            {
                return Environment.SystemDirectory + "\\";
            }
        }

        /// <summary>
        /// 系统所在驱动器号
        /// </summary>
        public static Char SystemDriver
        {
            get
            {
                return Environment.SystemDirectory[0];
            }
        }

        /// <summary>
        /// 创建目录 
        /// </summary>
        /// <param name="dir">目录名</param>
        public static void CreateDirectory(String dir)
        {
            Directory.CreateDirectory(dir);
        }

        /// <summary>
        /// 目录是否存在
        /// </summary>
        /// <param name="dir">目录名</param>
        /// <returns>存在返回true</returns>
        public static bool DirectoryExists(String dir)
        {
            return Directory.Exists(dir);
        }
    };


    /// <summary>
    /// 文件操作类
    /// </summary>
    public class CFile
    {
        /// <summary>
        /// 移动文件 
        /// </summary>
        /// <param name="oldFile"></param>
        /// <param name="newFile"></param>
        static public void Move(String oldFile, String newFile)
        {
            File.Move(oldFile, newFile);
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="oldFile"></param>
        /// <param name="newFile"></param>
        static public void Copy(String oldFile, String newFile)
        {
            File.Copy(oldFile, newFile);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        static public void Delete(String fileName)
        {
            File.Delete(fileName);
        }

        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>存在返回true</returns>
        static public bool Exists(String fileName)
        {
            return File.Exists(fileName);
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static public Int64 GetFileLength(String fileName)
        {
            FileInfo myInfo = new FileInfo(fileName);
            return myInfo.Length; 
        }

        /// <summary>
        /// 获取文件最后一次修改时间
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>返回修改时间</returns>
        static public DateTime GetLastWriteTime(String fileName)
        {
            return File.GetLastWriteTime(fileName);
        }

        /// <summary>
        /// 获取文件创建时间
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>返回创建时间</returns>
        static public DateTime GetCreationTime(String fileName)
        {
            return File.GetCreationTime(fileName);
        }


        /// <summary>
        /// 把文件读取到内存流中
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns>内存流</returns>
        /// <exception cref="CFileException">
        /// 读入失败触发返回 ExceptionCode.FileReadFail异常
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
        /// 文件读入到字符数组中
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <param name="buf">输出字符数组</param>
        /// <param name="Encode">
        /// 编码方式 
        /// 如 "gb2312" "utf-8"
        /// </param>        
        /// <exception cref="CFileException">
        /// 读入失败触发返回 ExceptionCode.FileReadFail异常
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
        /// 将文件读入到一个String对象中
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <param name="Encode">编码方式</param>
        /// <returns>String 对象</returns>
        /// <exception cref="CFileException">
        /// 读入失败触发返回 ExceptionCode.FileReadFail异常
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
        /// 将文件读入到一个StringBuilder对象中
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <param name="Encode">编码方式</param>
        /// <returns>StringBuilder 对象</returns>
        /// <exception cref="CFileException">
        /// 读入失败触发返回 ExceptionCode.FileReadFail异常
        /// </exception>
        static public StringBuilder ReadFileToStringBuilder(String FileName, String Encode)
        {
            String temp;
            temp = ReadFileToString(FileName, Encode);

            StringBuilder ret = new StringBuilder(temp);
            return ret;
        }

        /// <summary>
        /// 将内存流输出到文件
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <param name="In">内存流</param>
        /// <exception cref="CFileException">
        /// 写入失败触发返回 ExceptionCode.FileReadFail异常
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
        /// 将字符串拷贝到文件中
        /// </summary>
        /// <param name="str"></param>
        /// <param name="FileName"></param>
        /// <param name="Encode"></param>
        /// <remarks>
        /// 调用该函数源文件将会被清空
        /// </remarks>
        /// <exception cref="CFileException">
        /// 写入失败触发返回 ExceptionCode.FileReadFail异常
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
        /// 向文件末尾追加一行
        /// </summary>
        /// <param name="str">要追加的字符串</param>
        /// <param name="FileName">文件名</param>
        /// <param name="Encode">编码方式</param>
        /// <exception cref="CFileException">
        /// 写入失败触发返回 ExceptionCode.FileReadFail异常
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
        /// 获取绝对路径
        /// </summary>
        /// <param name="BasePath">基准路径</param>
        /// <param name="RelPath">相对路径</param>
        /// <returns>绝对路径，绝对路径以"\"结尾</returns>
        /// <exception>
        /// ArgumentException
        /// SecurityException
        /// ArgumentNullException
        /// NotSupportedException
        /// PathTooLongException
        /// 具体见Path.GetFullPath的帮助
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
        /// 获取纯文件名
        /// </summary>
        /// <param name="PathName"></param>
        /// <returns>纯文件名</returns>
        /// <exception>
        /// ArgumentException
        /// SecurityException
        /// ArgumentNullException
        /// NotSupportedException
        /// PathTooLongException
        /// 具体见Path.GetFileName的帮助
        /// </exception>        
        public static String GetFileName(String PathName)
        {
            return Path.GetFileName(PathName);
        }

        /// <summary>
        /// 获取不带扩展名的纯文件名
        /// </summary>
        /// <param name="PathName"></param>
        /// <returns>不带扩展名的纯文件名</returns>
        /// <exception>
        /// ArgumentException
        /// SecurityException
        /// ArgumentNullException
        /// NotSupportedException
        /// PathTooLongException
        /// 具体见Path.GetFileNameWithoutExtension的帮助
        /// </exception>        
        public static String GetFileNameWithoutExt(String PathName)
        {
            return Path.GetFileNameWithoutExtension(PathName);
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="PathName"></param>
        /// <returns>
        /// 文件扩展名，扩展名包括前面的.
        /// 如输入 a.txt ，返回 .txt
        /// </returns>
        /// <exception>
        /// ArgumentException
        /// SecurityException
        /// ArgumentNullException
        /// NotSupportedException
        /// PathTooLongException
        /// 具体见Path.GetExtension的帮助
        /// </exception>        
        public static String GetExt(String PathName)
        {
            return Path.GetExtension(PathName);
        }

        /// <summary>
        /// 获取文件的路径名
        /// </summary>
        /// <param name="PathName"></param>
        /// <returns>返回文件的路径，不包括文件名。路径以"\"结尾</returns>
        /// <exception>
        /// ArgumentException
        /// SecurityException
        /// ArgumentNullException
        /// NotSupportedException
        /// PathTooLongException
        /// 具体见Path.GetDirectoryName的帮助
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
        /// 把Base64编码的字符串转换为实际数据存入文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="data">Base64编码的字符串</param>
        /// <remarks>文件操作可能触发异常</remarks>
        static public void WriteFileByBase64String(String fileName, String data)
        {
            byte[] buf = Convert.FromBase64String(data);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            fs.Write(buf, 0, buf.Length);
            fs.Close();
        }


        /// <summary>
        /// 把文件内容读入Base64编码的字符串中
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>Base64编码的字符串</returns>
        /// <remarks>文件操作可能触发异常</remarks>
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
        /// 获取文件版本号
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>如果文件有版本号，返回版本号，否则返回null</returns>
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
