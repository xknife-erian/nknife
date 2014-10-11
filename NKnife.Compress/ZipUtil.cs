using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using NKnife.NIo;

namespace NKnife.Compress
{
    public static class ZipUtil
    {
        /// <summary>
        ///     将指定的文件集合压缩为指定的路径下的文件
        /// </summary>
        /// <param name="fileList">指定的文件集合.</param>
        /// <param name="outputPath">指定的路径下的文件，含完整的路径与文件名</param>
        /// <param name="inpath">
        ///     压缩包中的文件的相对路径。
        ///     可以为Null和空值，当为Null或空值时，将删除文件的所有路径信息，解压时文件将全部在一个目录中。
        /// </param>
        /// <param name="level">The level.</param>
        /// <param name="password">The password.</param>
        public static void ZipFiles(string[] fileList, string outputPath, string inpath, int level = 6, string password = "")
        {
            var outZipStream = new ZipOutputStream(File.Create(outputPath)); //创建Zip流
            if (!string.IsNullOrEmpty(password))
                outZipStream.Password = password;
            if (level < 0 || level > 9)
                level = 5;
            outZipStream.SetLevel(level); //设置压缩等级
            foreach (string file in fileList)
            {
                if (!File.Exists(file) || file.EndsWith(@"/")) //如果文件不存在，或者表达的是目录
                    continue;
                string newfilename = null;
                if (string.IsNullOrEmpty(inpath))
                {
                    newfilename = file.Contains("\\") ? file.Substring(file.LastIndexOf('\\') + 1) : file;
                }
                else
                {
                    if (file.Contains(inpath))
                    {
                        newfilename = file.Replace(inpath, "");
                        if (newfilename.StartsWith(@"\"))
                            newfilename = newfilename.TrimStart('\\');
                    }
                }
                var oZipEntry = new ZipEntry(newfilename)
                {
                    Size = new FileInfo(file).Length,
                    IsUnicodeText = true
                };
                outZipStream.PutNextEntry(oZipEntry);

                using (FileStream ostream = File.OpenRead(file))
                {
                    var obuffer = new byte[ostream.Length];
                    ostream.Read(obuffer, 0, obuffer.Length);
                    outZipStream.Write(obuffer, 0, obuffer.Length);
                    ostream.Close();
                }
            }
            outZipStream.Finish();
            outZipStream.Close();
        }

        public static void UnZipFiles(FileInfo zipPathAndFile, string outputFolder, string password = "", bool needDeleteZipFile = false)
        {
            UnZipFiles(zipPathAndFile.FullName, outputFolder, password, needDeleteZipFile);
        }

        public static void UnZipFiles(string zipPathAndFile, string outputFolder, string password = "", bool needDeleteZipFile = false)
        {
            var zipStream = new ZipInputStream(File.OpenRead(zipPathAndFile));
            if (!string.IsNullOrWhiteSpace(password))
                zipStream.Password = password;
            ZipEntry zipEntry;
            while ((zipEntry = zipStream.GetNextEntry()) != null)
            {
                string fileName = Path.GetFileName(zipEntry.Name);
                if (string.IsNullOrWhiteSpace(fileName))
                    continue;
                if (!string.IsNullOrWhiteSpace(outputFolder))
                    UtilityFile.CreateDirectory(outputFolder);
                string fullPath = outputFolder + "\\" + zipEntry.Name;
                fullPath = fullPath.Replace("\\ ", "\\");
                string fullDirPath = Path.GetDirectoryName(fullPath);
                if (fullDirPath != null && !Directory.Exists(fullDirPath))
                    UtilityFile.CreateDirectory(fullDirPath);
                using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    int size = 2048;
                    var data = new byte[size];
                    while (true)
                    {
                        size = zipStream.Read(data, 0, data.Length);
                        if (size > 0)
                            fileStream.Write(data, 0, size);
                        else
                            break;
                    }
                    fileStream.Flush();
                    fileStream.Close();
                }
            }
            zipStream.Close();
            if (needDeleteZipFile)
                File.Delete(zipPathAndFile);
        }
    }
}