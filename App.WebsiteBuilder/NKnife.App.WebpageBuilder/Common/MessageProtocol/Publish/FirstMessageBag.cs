using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    /// <summary>
    /// 客户端第一次想服务器发送的消息包
    /// </summary>
    public class FirstMessageBag : MessageBag
    {
        /*SimplusD的版本号,用户名，用户的通行证，上传的文件数，网站的名称，网站的Guid*/
        public string SdVersion { get; private set; }
        public string UserName { get; private set; }
        public string UserPassport { get; private set; }
        public int FilesCount { get; private set; }
        public string SiteName { get; private set; }
        public string SiteId { get; private set; }

        public FirstMessageBag(
            string sdVersion,
            string userName,
            string userPassport,
            int filesCount,
            string siteName,
            string siteId)
            : base(new MessageHead((int)MessageType.User, 0, 1))
        {
            SdVersion = sdVersion;
            UserName = userName;
            UserPassport = userPassport;
            FilesCount = filesCount;
            SiteName = siteName;
            SiteId = siteId;
        }

        //消息类型，1为用户消息2.文件消息
        //从消息头里得到消息体的长度，则将其值取出
        public FirstMessageBag(byte[] bytesBody)
            :base(new MessageHead ((int)MessageType.User,0,1),bytesBody)
        {
            string all = Encoding.UTF8.GetString(bytesBody);
            string[] arrstr = all.Split(new[] { '&' }, StringSplitOptions.None);
            SdVersion = arrstr[0];
            UserName = arrstr[1];
            UserPassport = arrstr [2];
            FilesCount = int.Parse(arrstr[3]);
            SiteName = arrstr [4];
            SiteId = arrstr [5];
        }

        //将用户的消息作为消息体传送出去
        public override byte[] ToBytes()
        {
            //为BytesBody赋初值
            string all = SdVersion + "&" + UserName + "&" + UserPassport + "&" + FilesCount + "&" + SiteName + "&" + SiteId;
            BytesBody = Encoding.UTF8.GetBytes(all);

            return base.ToBytes();
        }
    }
}
