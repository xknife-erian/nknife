namespace NKnife.AutoUpdater.Interfaces
{
    internal interface IUpdaterFileGetter<out T>
    {
        /// <summary>从远程获取更新起始时，比较本地文件与远程文件之间异同的索引文件
        /// </summary>
        /// <returns></returns>
        T GetUpdaterIndexFile();

        /// <summary>根据指定的文件信息从远程获取指定的文件(当FTP时，指的是文件在远程的相对路径)
        /// </summary>
        /// <param name="fileUpdateInfo">指定的文件信息，当FTP时，指的是文件在远程的相对路径</param>
        /// <returns></returns>
        T GetTargetFile(string fileUpdateInfo);
    }
}