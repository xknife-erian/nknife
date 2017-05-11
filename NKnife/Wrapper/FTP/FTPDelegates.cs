using System.ComponentModel;
using System.Net;

namespace NKnife.Wrapper.FTP
{
    public delegate void FtpDownloadDataCompletedDelegate(object sender, AsyncCompletedEventArgs e);

    public delegate void FtpDownloadProgressChangedDelegate(object sender, DownloadProgressChangedEventArgs e);

    public delegate void FtpUploadFileCompletedDelegate(object sender, UploadFileCompletedEventArgs e);

    public delegate void FtpUploadProgressChangedDelegate(object sender, UploadProgressChangedEventArgs e);

}
