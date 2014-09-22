using System;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Text;
using System.Collections.Generic;

namespace Jeelu
{
    static public partial class Utility
    {
        static public class File
        {
            /// <summary>
            /// 判断此路径指向的是否目录
            /// </summary>
            static public bool IsDirectory(string path)
            {
                if (System.IO.Directory.Exists(path))
                {
                    return true;
                }
                return false;
                //return (System.IO.File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
            }

            /// <summary>
            /// 拷贝目录(目录下的所有文件及文件夹将一起拷贝)
            /// </summary>
            /// <param name="srcPath">源目录</param>
            /// <param name="targetPath">目标目录</param>
            static public void DirectoryCopy(string srcPath, string targetPath, bool offReadonlyAttribute)
            {
                if (!Directory.Exists(srcPath))
                {
                    throw new Exception("源目录不存在:" + srcPath);
                }

                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                DirectoryCopyEx(srcPath, targetPath, offReadonlyAttribute);
            }
            /// <summary>
            /// 拷贝目录(目录下的所有文件及文件夹将一起拷贝)
            /// </summary>
            /// <param name="srcPath">源目录</param>
            /// <param name="targetPath">目标目录</param>
            static public void DirectoryCopy(string srcPath, string targetPath)
            {
                DirectoryCopy(srcPath, targetPath, true);
            }
            static private void DirectoryCopyEx(string srcPath, string targetPath, bool offReadonlyAttribute)
            {
                ///遍历目录，拷贝所有子目录
                string[] dirs = Directory.GetDirectories(srcPath, "*", SearchOption.TopDirectoryOnly);

                foreach (string dir in dirs)
                {
                    string targetDir = Path.Combine(targetPath, Path.GetFileName(dir));
                    Directory.CreateDirectory(targetDir);

                    ///递归拷贝子目录
                    DirectoryCopyEx(dir, targetDir, offReadonlyAttribute);
                }

                ///遍历目录，拷贝所有文件
                string[] files = Directory.GetFiles(srcPath, "*", SearchOption.TopDirectoryOnly);

                foreach (string file in files)
                {
                    string targetFile = Path.Combine(targetPath, Path.GetFileName(file));
                    System.IO.File.Copy(file, targetFile);
                    if (offReadonlyAttribute)
                    {
                        System.IO.File.SetAttributes(targetFile, FileAttributes.Normal);
                    }
                }
            }

            /// <summary>
            /// Copy Directory
            /// </summary>
            /// <param name="OldDirectory">Old Directory</param>
            /// <param name="NewDirectory">New Directory</param>
            private static void CopyDirectory(DirectoryInfo OldDirectory, DirectoryInfo NewDirectory)
            {
                string NewDirectoryFullName = NewDirectory.FullName + @"\" + OldDirectory.Name;

                if (!Directory.Exists(NewDirectoryFullName))
                    Directory.CreateDirectory(NewDirectoryFullName);

                FileInfo[] OldFileAry = OldDirectory.GetFiles();
                foreach (FileInfo aFile in OldFileAry)
                    System.IO.File.Copy(aFile.FullName, NewDirectoryFullName + @"\" + aFile.Name, true);

                DirectoryInfo[] OldDirectoryAry = OldDirectory.GetDirectories();
                foreach (DirectoryInfo aOldDirectory in OldDirectoryAry)
                {
                    DirectoryInfo aNewDirectory = new DirectoryInfo(NewDirectoryFullName);
                    CopyDirectory(aOldDirectory, aNewDirectory);
                }
            }

            /// <summary>
            /// 检查文件或文件夹是否存在
            /// </summary>
            /// <param name="path">文件或文件夹路径</param>
            public static bool Exists(string path)
            {
                if (System.IO.File.Exists(path))
                {
                    return true;
                }
                else if (Directory.Exists(path))
                {
                    return true;
                }

                return false;
            }

            /// <summary>
            /// 获取指定路径的文件名或文件夹名。
            /// </summary>
            public static string GetFileOrDirectoryName(string path)
            {
                ///把最后带\的形式统一成无\。如d:\test\转成d:\test
                if (path.EndsWith(@"\"))
                {
                    path = path.Substring(0, path.Length - 1);
                }
                int index = path.LastIndexOf('\\');
                return path.Substring(index);
            }

            /// <summary>
            /// 获取指定路径的父文件夹路径。返回路径最后带有\,如d:\test\
            /// </summary>
            public static string GetParentDirectory(string path)
            {
                ///把最后带\的形式统一成无\。如d:\test\转成d:\test
                if (path.EndsWith(@"\"))
                {
                    path = path.Substring(0, path.Length - 1);
                }
                int index = path.LastIndexOf('\\');
                return path.Substring(0, index + 1);
            }

            /// <summary>
            /// 通过指定文件夹和默认名称创建一个递增的新文件名
            /// </summary>
            /// <param name="parentFolderPath">父文件夹路径</param>
            /// <param name="defaultFileName">默认文件名，新生成的文件名将以这个作为前缀，以数字作为后缀</param>
            /// <param name="extension">扩展名。如：.sdtmplt</param>
            /// <param name="isFolder">是否文件夹。</param>
            /// <returns>生成的新文件名</returns>
            //static public string CreateIncreaseFileName(string parentFolderPath, string defaultFileName, string extension,bool isFolder)
            //{
            //    Debug.Assert(Directory.Exists(parentFolderPath));
            //    Debug.Assert(!string.IsNullOrEmpty(defaultFileName));

            //    string newFileName = null;
            //    string newFilePath = null;
            //    int index = 1;
            //    do
            //    {
            //        newFileName = defaultFileName + index;
            //        newFilePath = Path.Combine(parentFolderPath, newFileName + extension);
            //        index++;
            //    }
            //    while (isFolder ? Directory.Exists(newFilePath) : File.Exists(newFilePath));

            //    return newFileName;
            //}

            ///// <summary>
            ///// 通过指定文件夹和默认名称创建一个递增的新文件名
            ///// </summary>
            ///// <param name="parentFolderPath">父文件夹路径</param>
            ///// <param name="defaultFileName">默认文件名，新生成的文件名将以这个作为前缀，以数字作为后缀</param>
            ///// <param name="isFolder">是否文件夹。</param>
            ///// <returns>生成的新文件名</returns>
            //static public string CreateIncreaseFileName(string parentFolderPath, string defaultFileName, bool isFolder)
            //{
            //    return CreateIncreaseFileName(parentFolderPath, defaultFileName, null, isFolder);
            //}

            ///// <summary>
            ///// 通过指定文件夹和默认名称创建一个递增的新文件名
            ///// </summary>
            ///// <param name="parentFolderPath">父文件夹路径</param>
            ///// <param name="defaultFileName">默认文件名，新生成的文件名将以这个作为前缀，以数字作为后缀</param>
            ///// <param name="extension">扩展名。如：.sdtmplt</param>
            ///// <returns>生成的新文件名</returns>
            //static public string CreateIncreaseFileName(string parentFolderPath, string defaultFileName, string extension)
            //{
            //    return CreateIncreaseFileName(parentFolderPath, defaultFileName, extension, false);
            //}

            /// <summary>
            /// 直接取XmlDocument的Id。（在DocumentElement里面找id属性的值）
            /// </summary>
            public static string GetXmlDocumentId(string filePath)
            {
                Debug.Assert(!string.IsNullOrEmpty(filePath));

                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                return doc.DocumentElement.GetAttribute("id");
            }

            /// <summary>
            /// 删除文件或文件夹
            /// </summary>
            public static void DeleteFileOrDirectory(string path)
            {
                if (Exists(path))
                {
                    if (!IsDirectory(path))
                    {
                        System.IO.File.SetAttributes(path, FileAttributes.Normal);
                        System.IO.File.Delete(path);
                    }
                    else
                    {
                        System.IO.Directory.Delete(path,true);
                    }
                }
            }

            /// <summary>
            /// 将文本格式化成标准的文件名。
            /// 中文会转换成拼音，保留英文数字和下划线
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            static public string FormatFileName(string input, bool format)
            {
                StringBuilder sb = new StringBuilder();

                foreach (char ch in input)
                {
                    ///若是汉字，将此汉字转换成拼音，添加到结果
                    if (Regex.IsHanzi(ch))
                    {
                        string pinyin = Pinyin.ToPinyinSingle(ch);
                        if (!string.IsNullOrEmpty(pinyin))
                        {
                            if (format)
                            {
                                sb.Append(pinyin).Append(",");
                            }
                            else
                            {
                                sb.Append(pinyin);
                            }
                        }
                    }
                    ///不是汉字，判断是否合法，若合法，则添加到结果
                    else if (IsValidateFileName(ch))
                    {
                        sb.Append(ch);
                    }
                }
                string str = sb.ToString();
                if (format)
                {
                    int index = str.LastIndexOf(',');
                    if (index != -1)
                    {
                        str = str.Remove(index, 1);
                    }
                }
                return str;
            }

            /// <summary>
            /// 通过原文件名生成一个不重名的文件名。若有重名则会加数字后缀。
            /// </summary>
            /// <param name="parentFolderPath"></param>
            /// <param name="srcFileName"></param>
            /// <returns></returns>
            static public string BuildFileName(string parentFolderPath, string srcFileName, bool isFolder, bool isReturnExtension)
            {
                ///若此文件名的文件不存在，则直接返回
                if (isFolder)
                {
                    if (!System.IO.Directory.Exists(Path.Combine(parentFolderPath, srcFileName))
                        && !string.IsNullOrEmpty(srcFileName))
                    {
                        return srcFileName;
                    }

                    ///循环，用数字做后缀不停尝试，直到没有重名
                    for (int i = 1; i < int.MaxValue; i++)
                    {
                        string newFileName = srcFileName + i;
                        if (!System.IO.Directory.Exists(Path.Combine(parentFolderPath, newFileName)))
                        {
                            return newFileName;
                        }
                    }

                    ///若一直都有重名，则返回个Guid的名字
                    return Guid.NewGuid();
                }
                else
                {
                    ///分析文件名
                    string fileName = Path.GetFileNameWithoutExtension(srcFileName);
                    string extension = Path.GetExtension(srcFileName);

                    if (!System.IO.File.Exists(Path.Combine(parentFolderPath, srcFileName))
                        && !string.IsNullOrEmpty(fileName))
                    {
                        return isReturnExtension ? srcFileName : Path.GetFileNameWithoutExtension(srcFileName);
                    }

                    ///循环，用数字做后缀不停尝试，直到没有重名
                    for (int i = 1; i < int.MaxValue; i++)
                    {
                        string newFileName = fileName + i + extension;
                        if (!System.IO.File.Exists(Path.Combine(parentFolderPath, newFileName)))
                        {
                            return isReturnExtension ? newFileName : (fileName + i);
                        }
                    }

                    ///若一直都有重名，则返回个Guid的名字
                    return Guid.NewGuid() + (isReturnExtension ? extension : "");
                }
            }

            static private bool IsValidateFileName(char ch)
            {
                if ((ch >= 'a' && ch <= 'z')
                    || (ch >= 'A' && ch <= 'Z')
                    || (ch >= '0' && ch <= '9')
                    || ch == '_'
                    || ch == '.')
                {
                    return true;
                }
                return false;
            }


            /* 给定一个目录，在这个目录内创建子目录，子目录名为无符号整型递增，
             * 每一个目录下仅能存限定的文件数目。
             */
            public static string GetLimitDirectory(string rootdir, int fileCountForDir, out string fullpath)
            {
                if (!Directory.Exists(rootdir))
                {
                    #region Try CreateDirectory Catch throw
                    try
                    {
                        Directory.CreateDirectory(rootdir);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    #endregion
                }

                uint subdir = 1;
                string[] dirs = Directory.GetDirectories(rootdir, "*", SearchOption.TopDirectoryOnly);
                if (dirs == null || dirs.Length == 0)
                {
                    fullpath = Path.Combine(rootdir, subdir.ToString());
                    return subdir.ToString();
                }
                Array.Sort(dirs);/// 对Root目录下的所有子目录进行排序
                for (int i = dirs.Length - 1; i >= 0; i--)/// 反向循环看是否有子目录名为uint的值转换成
                {
                    string dirstring = dirs[i].Substring(dirs[i].LastIndexOf('\\') + 1, dirs[i].Length - dirs[i].LastIndexOf('\\') - 1);
                    if (uint.TryParse(dirstring, out subdir))
                    {
                        if (Directory.GetFiles(dirs[i], "*.*", SearchOption.TopDirectoryOnly).Length >= fileCountForDir)
                        {
                            subdir++;
                        }
                        break;
                    }
                }

                fullpath = Path.Combine(rootdir, subdir.ToString());
                return subdir.ToString();
            }
        }
    }
}
