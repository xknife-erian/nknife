﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Gean;
using Microsoft.Win32;
using NKnife.Resources;

namespace NKnife.Utility.File
{
    /// <summary>
    /// 文件与目录等System.IO下的类的扩展
    /// </summary>
    public static class UtilityFile
    {
        /// <summary>
        /// 路径分割符
        /// </summary>
        private const string PATH_SPLIT_CHAR = "\\";
        
        /// <summary>
        /// Gets or sets the application root path.
        /// </summary>
        /// <value>The application root path.</value>
        public static string ApplicationRootPath
        {
            get { return _ApplicationRootPath; }
            set { _ApplicationRootPath = value; }
        }
        private static string _ApplicationRootPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        /// <summary>
        /// 扩展Path.Combine方法，可以合并多个路径字符串.
        /// </summary>
        /// <param name="paths">The paths.</param>
        /// <returns></returns>
        public static string Combine(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
            {
                return string.Empty;
            }
            if (paths.Length >= 32)
            {
                throw new ArgumentOutOfRangeException("路径字段太多，可能导致系统IO错误");
            }

            string result = paths[0];
            for (int i = 1; i < paths.Length; i++)
            {
                result = Path.Combine(result, paths[i]);
            }
            return result;
        }

        /// <summary>
        /// 检查文件名是否规范(windows)
        /// </summary>
        public static bool IsValidFileName(string fileName)
        {
            if (fileName == null || fileName.Length == 0 || fileName.Length >= MaxPathLength)
            {
                return false;
            }
            if (fileName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                return false;
            }
            if (fileName.IndexOf('?') >= 0 || fileName.IndexOf('*') >= 0)
            {
                return false;
            }
            if (!Regex.IsMatch(fileName, RegexString.RegexStr_FileName))
            {
                return false;
            }

            // platform dependend : Check for invalid file names (DOS)
            // this routine checks for follwing bad file names :
            // CON, PRN, AUX, NUL, COM1-9 and LPT1-9
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            if (nameWithoutExtension != null)
            {
                nameWithoutExtension = nameWithoutExtension.ToUpperInvariant();
            }

            if (nameWithoutExtension == "CON" ||
                nameWithoutExtension == "PRN" ||
                nameWithoutExtension == "AUX" ||
                nameWithoutExtension == "NUL")
            {
                return false;
            }

            char ch = nameWithoutExtension.Length == 4 ? nameWithoutExtension[3] : '\0';

            return !((nameWithoutExtension.StartsWith("COM") ||
                      nameWithoutExtension.StartsWith("LPT")) &&
                     Char.IsDigit(ch));
        }

        /// <summary>
        /// 检查目录名是否规范
        /// Checks that a single directory name (not the full path) is valid.
        /// </summary>
        public static bool IsValidDirectoryName(string name)
        {
            if (!IsValidFileName(name))
            {
                return false;
            }
            if (name.IndexOfAny(new char[] { Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar }) >= 0)
            {
                return false;
            }
            if (name.Trim(' ').Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the specified filename is directory.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>
        /// 	<c>true</c> if the specified filename is directory; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDirectory(string filename)
        {
            if (!Directory.Exists(filename))
            {
                return false;
            }
            FileAttributes attr = System.IO.File.GetAttributes(filename);
            return (attr & FileAttributes.Directory) != 0;
        }

        /// <summary>获取一个路径下的所有子目录，只取当前目录，不递归获取
        /// </summary>
        /// <param name="path">路径名</param>
        public static IList<TreeNode> GetDirectoryNodes(string path)
        {
            return GetDirectoryNodes(path, SearchOption.TopDirectoryOnly, "*", false, true);
        }

        /// <summary>获取一个路径下的所有子目录(包含文件显示，TreeNode可选择)
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="searchOption">是否包含子目录</param>
        /// <param name="searchPattern">要与 path 中的文件名匹配的搜索字符串。此参数不能以两个句点（“..”）结束，不能在 System.IO.Path.DirectorySeparatorChar
        /// 或 System.IO.Path.AltDirectorySeparatorChar 的前面包含两个句点（“..”），也不能包含 System.IO.Path.InvalidPathChars
        /// 中的任何字符。
        /// </param>
        /// <param name="isFullName">是否显示目录全名</param>
        /// <returns></returns>
        public static IList<TreeNode> GetDirectoryNodes(string path, SearchOption searchOption, string searchPattern, bool isFullName)
        {
            return GetDirectoryNodes(path, searchOption, searchPattern, isFullName, true);
        }

        /// <summary>获取一个路径下的所有子目录
        /// </summary>
        /// <param name="path">路径名</param>
        /// <param name="searchOption">是否包含子目录</param>
        /// <param name="searchPattern">要与 path 中的文件名匹配的搜索字符串。此参数不能以两个句点（“..”）结束，不能在 System.IO.Path.DirectorySeparatorChar
        /// 或 System.IO.Path.AltDirectorySeparatorChar 的前面包含两个句点（“..”），也不能包含 System.IO.Path.InvalidPathChars
        /// 中的任何字符。
        /// </param>
        /// <param name="isFullName">是否显示目录全名</param>
        /// <param name="isViewFile">是否包含文件的显示</param>
        /// <returns></returns>
        public static IList<TreeNode> GetDirectoryNodes(string path, SearchOption searchOption, string searchPattern, bool isFullName, bool isViewFile)
        {
            if (!Directory.Exists(path))
            {
                Debug.Fail(path + " isn't exist!");
                return null;
            }
            var nodelist = new List<TreeNode>();
            foreach (string dir in Directory.EnumerateDirectories(path, searchPattern, SearchOption.TopDirectoryOnly))
            {
                TreeNode node = !isFullName ? new TreeNode(Path.GetFileName(dir)) : new TreeNode(dir);
                node.ToolTipText = dir;
                nodelist.Add(node);

                if (searchOption == SearchOption.AllDirectories)//递归获取目录
                {
                    var nodes = GetDirectoryNodes(dir, searchOption, searchPattern, isFullName, isViewFile);
                    if (nodes != null)
                    {
                        foreach (TreeNode subnode in nodes)
                            node.Nodes.Add(subnode);
                    }
                }
            }
            if (isViewFile) //如果需要显示文件
            {
                foreach (var file in Directory.EnumerateFiles(path))
                {
                    var fileNode = new TreeNode(Path.GetFileName(file));
                    fileNode.ToolTipText = file;
                    nodelist.Add(fileNode);
                }
            }
            return nodelist;
        }

        private static void AddFiles(bool isViewFile, TreeNode node, string dir)
        {
            if (isViewFile) //如果需要显示文件
            {
                foreach (var file in Directory.EnumerateFiles(dir))
                {
                    var fileNode = new TreeNode(Path.GetFileName(file));
                    fileNode.ToolTipText = file;
                    node.Nodes.Add(fileNode);
                }
            }
        }

        /// <summary>获取当前目录中与指定搜索模式匹配并使用某个值确定是否在子目录中搜索的目录的数组。
        /// 与系统不同的是对系统文件夹和隐藏文件夹进行了处理。系统文件夹将不再搜索，也不再列出。
        /// </summary>
        /// <param name="path">要搜索的路径。</param>
        /// <param name="searchPattern">要与 path 中的文件名匹配的搜索字符串。此参数不能以两个句点（“..”）结束，不能在 System.IO.Path.DirectorySeparatorChar
        /// 或 System.IO.Path.AltDirectorySeparatorChar 的前面包含两个句点（“..”），也不能包含 System.IO.Path.InvalidPathChars
        /// 中的任何字符。
        /// </param>
        /// <param name="searchOption">System.IO.SearchOption 值之一，指定搜索操作应包括所有子目录还是仅包括当前目录。</param>
        /// <returns>与搜索模式匹配的目录的 String 数组。</returns>
        public static string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }
            if (Directory.GetDirectoryRoot(path).Equals(path))
            {
                string[] tmpDirs = Directory.GetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
                List<string> rtnDirs = new List<string>();
                foreach (string tmpDir in tmpDirs)
                {
                    Console.WriteLine(tmpDir);
                    if (System.IO.File.GetAttributes(tmpDir) != (FileAttributes.System | FileAttributes.Directory | FileAttributes.Hidden))
                    {
                        rtnDirs.Add(tmpDir);
                        rtnDirs.AddRange(Directory.GetDirectories(tmpDir, searchPattern, searchOption));
                    }
                }
                return rtnDirs.ToArray();
            }
            else
            {
                return Directory.GetDirectories(path, searchPattern, searchOption);
            }
        }

        /// <summary>将指定目录下的子目录和文件生成xml文档
        /// </summary>
        /// <param name="targetDir">根目录</param>
        /// <returns>返回XmlDocument对象</returns>
        public static XmlDocument CreateXml(string targetDir)
        {
            var doc = new XmlDocument();
            var declaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(declaration);

            var rootElement = doc.CreateElement("Root");
            rootElement.SetAttribute("Path", targetDir);
            doc.AppendChild(rootElement);

            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                XmlElement childElement = doc.CreateElement("File");
                childElement.InnerText = fileName.Substring(fileName.LastIndexOf(PATH_SPLIT_CHAR) + 1);
                rootElement.AppendChild(childElement);
            }
            foreach (string directory in Directory.GetDirectories(targetDir))
            {
                XmlElement childElement = doc.CreateElement("Directory");
                childElement.SetAttribute("Name", directory.Substring(directory.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                rootElement.AppendChild(childElement);
                CreateBranch(directory, childElement, doc);
            }
            return doc;
        }

        /// <summary>(子方法)将指定目录下的子目录和文件生成xml文档方法中生成Xml分支的子方法
        /// </summary>
        /// <param name="targetDir">子目录</param>
        /// <param name="xmlNode">父目录XmlDocument</param>
        /// <param name="myDocument">XmlDocument对象</param>
        private static void CreateBranch(string targetDir, XmlElement xmlNode, XmlDocument myDocument)
        {
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                XmlElement childElement = myDocument.CreateElement("File");
                childElement.InnerText = fileName.Substring(fileName.LastIndexOf(PATH_SPLIT_CHAR) + 1);
                xmlNode.AppendChild(childElement);
            }
            foreach (string directory in Directory.GetDirectories(targetDir))
            {
                XmlElement childElement = myDocument.CreateElement("Directory");
                childElement.SetAttribute("Name", directory.Substring(directory.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                xmlNode.AppendChild(childElement);
                CreateBranch(directory, childElement, myDocument);
            }
        }

        /// <summary>
        /// 复制大文件。即每次复制文件的一小段，以节省总内存开销。
        /// </summary>
        /// <param name="fromFile">要复制的文件</param>
        /// <param name="toFile">要保存的位置</param>
        /// <param name="lengthEachTime">每次复制的长度</param>
        public static void CopyLargeFile(string fromFile, string toFile, int lengthEachTime)
        {
            var fileToCopy = new FileStream(fromFile, FileMode.Open, FileAccess.Read);
            var copyToFile = new FileStream(toFile, FileMode.Append, FileAccess.Write);

            if (lengthEachTime < fileToCopy.Length)//如果分段拷贝，即每次拷贝内容小于文件总长度
            {
                var buffer = new byte[lengthEachTime];
                var copied = 0;
                int lengthToCopy;
                while (copied <= ((int)fileToCopy.Length - lengthEachTime))//拷贝主体部分
                {
                    lengthToCopy = fileToCopy.Read(buffer, 0, lengthEachTime);
                    fileToCopy.Flush();
                    copyToFile.Write(buffer, 0, lengthEachTime);
                    copyToFile.Flush();
                    copyToFile.Position = fileToCopy.Position;
                    copied += lengthToCopy;
                }

                var left = (int)fileToCopy.Length - copied;//拷贝剩余部分
                fileToCopy.Read(buffer, 0, left);
                fileToCopy.Flush();
                copyToFile.Write(buffer, 0, left);
                copyToFile.Flush();
            }
            else//如果整体拷贝，即每次拷贝内容大于文件总长度
            {
                var buffer = new byte[fileToCopy.Length];
                fileToCopy.Read(buffer, 0, (int)fileToCopy.Length);
                fileToCopy.Flush();
                copyToFile.Write(buffer, 0, (int)fileToCopy.Length);
                copyToFile.Flush();
            }
            fileToCopy.Close();
            copyToFile.Close();
        }

        /// <summary>
        /// 复制指定目录的所有文件,不包含子目录及子目录中的文件
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,表示覆盖同名文件,否则不覆盖</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite)
        {
            CopyFiles(sourceDir, targetDir, overWrite, false);
        }

        /// <summary>
        /// 复制指定目录的所有文件
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        /// <param name="copySubDir">如果为true,包含子目录,否则不包含</param>
        public static void CopyFiles(string sourceDir, string targetDir, bool overWrite, bool copySubDir)
        {
            //复制当前目录文件
            foreach (string sourceFileName in Directory.GetFiles(sourceDir))
            {
                string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                if (System.IO.File.Exists(targetFileName))
                {
                    if (overWrite)
                    {
                        System.IO.File.SetAttributes(targetFileName, FileAttributes.Normal);
                        System.IO.File.Copy(sourceFileName, targetFileName, true);
                    }
                }
                else
                {
                    System.IO.File.Copy(sourceFileName, targetFileName, overWrite);
                }
            }

            //复制子目录
            if (copySubDir)
            {
                foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    CopyFiles(sourceSubDir, targetSubDir, overWrite, true);
                }
            }
        }

        /// <summary>
        /// 剪切并粘贴指定目录的所有文件,不包含子目录
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite)
        {
            MoveFiles(sourceDir, targetDir, overWrite, false);
        }

        /// <summary>
        /// 剪切并粘贴指定目录的所有文件
        /// </summary>
        /// <param name="sourceDir">原始目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overWrite">如果为true,覆盖同名文件,否则不覆盖</param>
        /// <param name="moveSubDir">如果为true,包含目录,否则不包含</param>
        public static void MoveFiles(string sourceDir, string targetDir, bool overWrite, bool moveSubDir)
        {
            //移动当前目录文件
            foreach (string sourceFileName in Directory.GetFiles(sourceDir))
            {
                string targetFileName = Path.Combine(targetDir, sourceFileName.Substring(sourceFileName.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                if (System.IO.File.Exists(targetFileName))
                {
                    if (overWrite)
                    {
                        System.IO.File.SetAttributes(targetFileName, FileAttributes.Normal);
                        System.IO.File.Delete(targetFileName);
                        System.IO.File.Move(sourceFileName, targetFileName);
                    }
                }
                else
                {
                    System.IO.File.Move(sourceFileName, targetFileName);
                }
            }
            if (moveSubDir)
            {
                foreach (string sourceSubDir in Directory.GetDirectories(sourceDir))
                {
                    string targetSubDir = Path.Combine(targetDir, sourceSubDir.Substring(sourceSubDir.LastIndexOf(PATH_SPLIT_CHAR) + 1));
                    if (!Directory.Exists(targetSubDir))
                        Directory.CreateDirectory(targetSubDir);
                    MoveFiles(sourceSubDir, targetSubDir, overWrite, true);
                    Directory.Delete(sourceSubDir);
                }
            }
        }

        /// <summary>
        /// 删除指定目录的所有文件和子目录(考虑了文件的只读或一些影响操作的属性)
        /// </summary>
        /// <param name="targetDir">操作目录</param>
        /// <param name="delSubDir">如果为true,包含对子目录的操作</param>
        public static void DeleteFiles(string targetDir, bool delSubDir = false)
        {
            foreach (string fileName in Directory.GetFiles(targetDir))
            {
                System.IO.File.SetAttributes(fileName, FileAttributes.Normal);
                System.IO.File.Delete(fileName);
            }
            if (delSubDir)
            {
                var dir = new DirectoryInfo(targetDir);
                foreach (DirectoryInfo subDi in dir.GetDirectories())
                {
                    DeleteFiles(subDi.FullName, true);
                    subDi.Delete();
                }
            }
        }

        /// <summary>创建指定目录(可级联创建)
        /// </summary>
        /// <param name="targetDir"></param>
        public static void CreateDirectory(string targetDir)
        {
            var dir = new DirectoryInfo(targetDir);
            var dirList = new List<DirectoryInfo>();
            LoopDir(dir, dirList);
            for (int i = dirList.Count-1 ; i >= 0; i--)
            {
                DirectoryInfo di = dirList[i];
                if (!di.Exists)
                {
                    di.Create();
                }
            }
        }

        static void LoopDir(DirectoryInfo dir, List<DirectoryInfo> dirList)
        {
            if (dirList.Count <= 0)
            {
                dirList.Add(dir);
            }
            if(dir.Parent != null)
            {
                dirList.Add(dir.Parent);
                LoopDir(dir.Parent, dirList);
            }
        }

        /// <summary>
        /// 建立子目录
        /// </summary>
        /// <param name="parentDir">父目录名称</param>
        /// <param name="subDirName">子目录名称</param>
        public static void CreateDirectory(string parentDir, string subDirName)
        {
            CreateDirectory(parentDir + PATH_SPLIT_CHAR + subDirName);
        }

        /// <summary>删除指定目录
        /// </summary>
        /// <param name="targetDir">目录路径</param>
        public static void DeleteDirectory(string targetDir)
        {
            var dirInfo = new DirectoryInfo(targetDir);
            if (dirInfo.Exists)
            {
                DeleteFiles(targetDir, true);
                dirInfo.Delete(true);
            }
        }

        /// <summary>删除指定目录的所有子目录,不包括对当前目录文件的删除
        /// </summary>
        /// <param name="targetDir">目录路径</param>
        public static void DeleteSubDirectory(string targetDir)
        {
            foreach (string subDir in Directory.GetDirectories(targetDir))
            {
                DeleteDirectory(subDir);
            }
        }

        static readonly char[] _Separators = { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, Path.VolumeSeparatorChar };

        /// <summary>
        /// Gets the installation root of the .NET Framework (@"C:\Windows\Microsoft.NET\Framework\")
        /// </summary>
        public static string NETFrameworkInstallRoot
        {
            get
            {
                using (RegistryKey installRootKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework"))
                {
                    object o = installRootKey.GetValue("InstallRoot");
                    return o == null ? String.Empty : o.ToString();
                }
            }
        }

        /// <summary>
        /// Gets the Windows Vista SDK installation root. If the Vista SDK is not installed, the
        /// .NET 2.0 SDK installation root is returned. If both are not installed, an empty string is returned.
        /// </summary>
        public static string NetSdkInstallRoot
        {
            get
            {
                string val = String.Empty;
                RegistryKey sdkRootKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SDKs\Windows\v6.0");
                if (sdkRootKey != null)
                {
                    object o = sdkRootKey.GetValue("InstallationFolder");
                    val = o == null ? String.Empty : o.ToString();
                    sdkRootKey.Close();
                }

                if (val.Length == 0)
                {
                    RegistryKey installRootKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework");
                    if (installRootKey != null)
                    {
                        object o = installRootKey.GetValue("sdkInstallRootv2.0");
                        val = o == null ? String.Empty : o.ToString();
                        installRootKey.Close();
                    }
                }
                return val;
            }
        }

        public static bool IsUrl(string path)
        {
            return path.IndexOf(':') >= 2;
        }

        public static string GetCommonBaseDirectory(string dir1, string dir2)
        {
            if (dir1 == null || dir2 == null) return null;
            if (IsUrl(dir1) || IsUrl(dir2)) return null;

            dir1 = Path.GetFullPath(dir1);
            dir2 = Path.GetFullPath(dir2);

            string[] aPath = dir1.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            string[] bPath = dir2.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var result = new StringBuilder();
            int indx = 0;
            for (; indx < System.Math.Min(bPath.Length, aPath.Length); ++indx)
            {
                if (bPath[indx].Equals(aPath[indx], StringComparison.OrdinalIgnoreCase))
                {
                    if (result.Length > 0) result.Append(Path.DirectorySeparatorChar);
                    result.Append(aPath[indx]);
                }
                else
                {
                    break;
                }
            }
            if (indx == 0)
                return null;
            return result.ToString();
        }

        /// <summary>
        /// Converts a given absolute path and a given base path to a path that leads
        /// from the base path to the absoulte path. (as a relative path)
        /// </summary>
        public static string GetRelativePath(string baseDirectoryPath, string absPath)
        {
            if (IsUrl(absPath) || IsUrl(baseDirectoryPath))
            {
                return absPath;
            }
            try
            {
                baseDirectoryPath = Path.GetFullPath(baseDirectoryPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                absPath = Path.GetFullPath(absPath);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("GetRelativePath error '" + baseDirectoryPath + "' -> '" + absPath + "'", ex);
            }

            string[] bPath = baseDirectoryPath.Split(_Separators);
            string[] aPath = absPath.Split(_Separators);
            int indx = 0;
            for (; indx < System.Math.Min(bPath.Length, aPath.Length); ++indx)
            {
                if (!bPath[indx].Equals(aPath[indx], StringComparison.OrdinalIgnoreCase))
                    break;
            }

            if (indx == 0)
            {
                return absPath;
            }

            var erg = new StringBuilder();

            if (indx == bPath.Length)
            {
                //	erg.Append('.');
                //	erg.Append(Path.DirectorySeparatorChar);
            }
            else
            {
                for (int i = indx; i < bPath.Length; ++i)
                {
                    erg.Append("..");
                    erg.Append(Path.DirectorySeparatorChar);
                }
            }
            erg.Append(String.Join(Path.DirectorySeparatorChar.ToString(), aPath, indx, aPath.Length - indx));
            return erg.ToString();
        }

        /// <summary>
        /// Converts a given relative path and a given base path to a path that leads
        /// to the relative path absoulte.
        /// </summary>
        public static string GetAbsolutePath(string baseDirectoryPath, string relPath)
        {
            return Path.GetFullPath(Path.Combine(baseDirectoryPath, relPath));
        }

        public static bool IsEqualFileName(string fileName1, string fileName2)
        {
            // Optimized for performance:
            //return Path.GetFullPath(fileName1.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)).ToLower() == Path.GetFullPath(fileName2.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)).ToLower();

            if (string.IsNullOrEmpty(fileName1) || string.IsNullOrEmpty(fileName2)) return false;

            char lastChar = fileName1[fileName1.Length - 1];
            if (lastChar == Path.DirectorySeparatorChar || lastChar == Path.AltDirectorySeparatorChar)
                fileName1 = fileName1.Substring(0, fileName1.Length - 1);
            lastChar = fileName2[fileName2.Length - 1];
            if (lastChar == Path.DirectorySeparatorChar || lastChar == Path.AltDirectorySeparatorChar)
                fileName2 = fileName2.Substring(0, fileName2.Length - 1);

            try
            {
                if (fileName1.Length < 2 || fileName1[1] != ':' || fileName1.IndexOf("/.") >= 0 || fileName1.IndexOf("\\.") >= 0)
                    fileName1 = Path.GetFullPath(fileName1);
                if (fileName2.Length < 2 || fileName2[1] != ':' || fileName2.IndexOf("/.") >= 0 || fileName2.IndexOf("\\.") >= 0)
                    fileName2 = Path.GetFullPath(fileName2);
            }
            catch (Exception) { }
            return string.Equals(fileName1, fileName2, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsBaseDirectory(string baseDirectory, string testDirectory)
        {
            try
            {
                baseDirectory = Path.GetFullPath(baseDirectory).ToUpperInvariant();
                testDirectory = Path.GetFullPath(testDirectory).ToUpperInvariant();
                baseDirectory = baseDirectory.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                testDirectory = testDirectory.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

                if (baseDirectory[baseDirectory.Length - 1] != Path.DirectorySeparatorChar)
                    baseDirectory += Path.DirectorySeparatorChar;
                if (testDirectory[testDirectory.Length - 1] != Path.DirectorySeparatorChar)
                    testDirectory += Path.DirectorySeparatorChar;

                return testDirectory.StartsWith(baseDirectory);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string RenameBaseDirectory(string fileName, string oldDirectory, string newDirectory)
        {
            fileName = Path.GetFullPath(fileName);
            oldDirectory = Path.GetFullPath(oldDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            newDirectory = Path.GetFullPath(newDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            if (IsBaseDirectory(oldDirectory, fileName))
            {
                if (fileName.Length == oldDirectory.Length)
                {
                    return newDirectory;
                }
                return Path.Combine(newDirectory, fileName.Substring(oldDirectory.Length + 1));
            }
            return fileName;
        }

        /// <summary>
        /// Deeps the copy.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="destinationDirectory">The destination directory.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        public static void DeepCopy(string sourceDirectory, string destinationDirectory, bool overwrite)
        {
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }
            foreach (string fileName in Directory.GetFiles(sourceDirectory))
            {
                System.IO.File.Copy(fileName, Path.Combine(destinationDirectory, Path.GetFileName(fileName)), overwrite);
            }
            foreach (string directoryName in Directory.GetDirectories(sourceDirectory))
            {
                DeepCopy(directoryName, Path.Combine(destinationDirectory, Path.GetFileName(directoryName)), overwrite);
            }
        }

        public static void CopyDirectory(string oldDirectoryStr, string newDirectoryStr)
        {
            var oldDirectory = new DirectoryInfo(oldDirectoryStr);
            var newDirectory = new DirectoryInfo(newDirectoryStr);
            CopyDirectory(oldDirectory, newDirectory);
        }

        private static void CopyDirectory(DirectoryInfo oldDirectory, DirectoryInfo newDirectory)
        {
            string newDirectoryFullName = newDirectory.FullName + @"\" + oldDirectory.Name;

            if (!Directory.Exists(newDirectoryFullName))
                Directory.CreateDirectory(newDirectoryFullName);

            FileInfo[] oldFileAry = oldDirectory.GetFiles();
            foreach (FileInfo aFile in oldFileAry)
            {
                System.IO.File.Copy(aFile.FullName, newDirectoryFullName + @"\" + aFile.Name, true);
            }

            DirectoryInfo[] oldDirectoryAry = oldDirectory.GetDirectories();
            foreach (DirectoryInfo aOldDirectory in oldDirectoryAry)
            {
                var aNewDirectory = new DirectoryInfo(newDirectoryFullName);
                CopyDirectory(aOldDirectory, aNewDirectory);
            }
        }

        private static StringCollection _AssemblyFiles;
        /// <summary>
        /// 搜索指定目录下所有.Net的程序集文件(Dll,Exe)
        /// </summary>
        /// <param name="directory">指定目录.</param>
        /// <returns></returns>
        public static StringCollection SearchAssemblyFileByDirectory(string directory)
        {
            if (_AssemblyFiles == null)
            {
                StringCollection dlls = SearchDirectory(directory, ".dll", true, true);
                StringCollection exes = SearchDirectory(directory, ".exe", true, true);
                _AssemblyFiles = new StringCollection();
                foreach (var dllPath in dlls)
                {
                    try
                    {
                        if (!NoDotNetAssemblyFiles.IsDotNetAssembly(dllPath))
                            continue;
                        Assembly.LoadFile(dllPath);
                        _AssemblyFiles.Add(dllPath);
                    }
                    catch { }
                }
                foreach (var exePath in exes)
                {
                    try
                    {
                        Assembly.LoadFile(exePath);
                        _AssemblyFiles.Add(exePath);
                    }
                    catch { }
                }
            }
            return _AssemblyFiles;
        }

        private static Assembly[] _Assemblies;

        /// <summary>
        /// 搜索指定目录下所有.Net的程序集("*.dll","*.exe")
        /// </summary>
        /// <param name="directory">指定目录.</param>
        /// <returns></returns>
        public static Assembly[] SearchAssemblyByDirectory(string directory)
        {
            if (UtilityCollection.IsNullOrEmpty(_Assemblies))
            {
                StringCollection dlls = SearchDirectory(directory, "*.dll", true, true);
                StringCollection exes = SearchDirectory(directory, "*.exe", true, true);
                var result = new List<Assembly>();
                foreach (var dllPath in dlls)
                {
                    try
                    {
                        if (!NoDotNetAssemblyFiles.IsDotNetAssembly(dllPath))
                            continue;
                        Assembly asse = Assembly.LoadFile(dllPath);
                        result.Add(asse);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                foreach (var exePath in exes)
                {
                    try
                    {
                        Assembly asse = Assembly.LoadFile(exePath);
                        result.Add(asse);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                _Assemblies = result.ToArray();
            }
            return _Assemblies;
        }

        /// <summary>
        /// 搜索目录指定格式的文件.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="filemask">The filemask.</param>
        /// <param name="searchSubdirectories">if set to <c>true</c> [search subdirectories].</param>
        /// <param name="ignoreHidden">if set to <c>true</c> [ignore hidden].</param>
        /// <returns></returns>
        public static StringCollection SearchDirectory(string directory, string filemask, bool searchSubdirectories, bool ignoreHidden)
        {
            var collection = new StringCollection();
            SearchDirectory(directory, filemask, collection, searchSubdirectories, ignoreHidden);
            return collection;
        }

        /// <summary>
        /// 搜索目录指定格式的文件.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="filemask">The filemask.</param>
        /// <param name="searchSubdirectories">if set to <c>true</c> [search subdirectories].</param>
        /// <returns></returns>
        public static StringCollection SearchDirectory(string directory, string filemask, bool searchSubdirectories)
        {
            return SearchDirectory(directory, filemask, searchSubdirectories, true);
        }

        /// <summary>
        /// 搜索目录指定格式的文件.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="filemask">The filemask.</param>
        /// <returns></returns>
        public static StringCollection SearchDirectory(string directory, string filemask)
        {
            return SearchDirectory(directory, filemask, true, true);
        }

        /// <summary>
        /// Finds all files which are valid to the mask <paramref name="filemask"/> in the path
        /// <paramref name="directory"/> and all subdirectories
        /// (if <paramref name="searchSubdirectories"/> is true).
        /// The found files are added to the List&lt;string&gt;
        /// <paramref name="collection"/>.
        /// If <paramref name="ignoreHidden"/> is true, hidden files and folders are ignored.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="filemask">The filemask.</param>
        /// <param name="collection">The collection.</param>
        /// <param name="searchSubdirectories">if set to <c>true</c> [search subdirectories].</param>
        /// <param name="ignoreHidden">if set to <c>true</c> [ignore hidden].</param>
        static void SearchDirectory(string directory, string filemask, StringCollection collection, bool searchSubdirectories, bool ignoreHidden)
        {
            // If Directory.GetFiles() searches the 8.3 name as well as the full name so if the filemask is 
            // "*.xpt" it will return "Template.xpt~"
            bool isExtMatch = Regex.IsMatch(filemask, @"^\*\..{3}$");
            string ext = null;
            string[] file = Directory.GetFiles(directory, filemask);
            if (isExtMatch) 
                ext = filemask.Remove(0, 1);

            foreach (var f in file)
            {
                if (ignoreHidden && (System.IO.File.GetAttributes(f) & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    continue;
                }
                if (isExtMatch && Path.GetExtension(f) != ext) continue;

                collection.Add(f);
            }

            if (searchSubdirectories)
            {
                string[] dir = Directory.GetDirectories(directory);
                foreach (string d in dir)
                {
                    if (ignoreHidden && (System.IO.File.GetAttributes(d) & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue;
                    }
                    SearchDirectory(d, filemask, collection, searchSubdirectories, ignoreHidden);
                }
            }
        }

        // This is an arbitrary limitation built into the .NET Framework.
        // Windows supports paths up to 32k length.
        public readonly static int MaxPathLength = 260;

        static bool MatchN(string src, int srcidx, string pattern, int patidx)
        {
            int patlen = pattern.Length;
            int srclen = src.Length;
            char next_char;

            for (; ; )
            {
                if (patidx == patlen)
                    return (srcidx == srclen);
                next_char = pattern[patidx++];
                if (next_char == '?')
                {
                    if (srcidx == src.Length)
                        return false;
                    srcidx++;
                }
                else if (next_char != '*')
                {
                    if ((srcidx == src.Length) || (src[srcidx] != next_char))
                        return false;
                    srcidx++;
                }
                else
                {
                    if (patidx == pattern.Length)
                        return true;
                    while (srcidx < srclen)
                    {
                        if (MatchN(src, srcidx, pattern, patidx))
                            return true;
                        srcidx++;
                    }
                    return false;
                }
            }
        }

        static bool Match(string src, string pattern)
        {
            if (pattern[0] == '*')
            {
                // common case optimization
                int i = pattern.Length;
                int j = src.Length;
                while (--i > 0)
                {
                    if (pattern[i] == '*')
                        return MatchN(src, 0, pattern, 0);
                    if (j-- == 0)
                        return false;
                    if ((pattern[i] != src[j]) && (pattern[i] != '?'))
                        return false;
                }
                return true;
            }
            return MatchN(src, 0, pattern, 0);
        }

        public static bool MatchesPattern(string filename, string pattern)
        {
            filename = filename.ToUpper();
            pattern = pattern.ToUpper();
            string[] patterns = pattern.Split(';');
            foreach (string p in patterns)
            {
                if (Match(filename, p))
                {
                    return true;
                }
            }
            return false;
        }


    }

} 