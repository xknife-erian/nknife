using System.Diagnostics;
using System.IO;

namespace NKnife.Compress
{
    public class RAR
    {
        public static string RarExecutableFile { get; private set; }

        public static void Initialization(string fileRarExecutable)
        {
            RarExecutableFile = fileRarExecutable;
        }

        /// <summary>
        ///     用RAR命令行执行程序解压指定的压缩文件
        /// </summary>
        /// <param name="compressionFile">指定的待解压缩文件</param>
        /// <returns>返回解压后的目录及文件数等相关信息</returns>
        public static string UnRar(string compressionFile)
        {
            //要解压的文件的路径，请自行设置 
            var rarFilePath = compressionFile;
            //确定要解压到的目录，是系统临时文件夹下，与原压缩文件同名的目录里 
            var unRarDestPath = Path.Combine(Path.GetDirectoryName(compressionFile),
                Path.GetFileNameWithoutExtension(rarFilePath));
            //组合出需要shell的完整格式 
            string shellArguments = $"x -o+ \"{rarFilePath}\" \"{unRarDestPath}\\\"";

            //用Process调用 
            using (var unrar = new Process())
            {
                unrar.StartInfo.FileName = RarExecutableFile;
                unrar.StartInfo.Arguments = shellArguments;
                unrar.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; //隐藏rar本身的窗口 
                unrar.Start();
                unrar.WaitForExit(); //等待解压完成  
                unrar.Close();
            }

            //统计解压后的目录和文件数 
            var di = new DirectoryInfo(unRarDestPath);
            string info = $"解压完成，共解压出：{di.GetDirectories().Length}个目录，{di.GetFiles().Length}个文件";
            return info;
        }
    }
}