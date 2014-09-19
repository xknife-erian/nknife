using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Jeelu.SimplusD.Client.Win
{
    /// <summary>
    /// 整个发布流程的入口类
    /// </summary>
    class Publish
    {
        //DisposePage _executePage;
        //DisposeTmplt _executeTmplt;

        ////modified by zhangling on June 2,2008
        //UpFileXmlDoc _upFileXmlDoc; 此类撤除，改为用list存储上传文件路径
        List<string> _upFileList;
        Dictionary<string, string> _publishDictionary;
        //DisposeResource _executeResource;
        DisposeAnyFile _executeAnyFile;
        public Publish()
        {
            /*add a definition by zhangling on May 27, 2008
             * 
             *UpFileXmlDoc类用来存在上传文件的地址 (只是存放在内存中)
             * _publishDictionary此实例用来存储需发布文件，主供修改状态使用;参数1：文件路径；参数2：文件Id
             * DisposeAnyFile类用来判断文件是否发布,是，则加入上传列表及_publishDictinary中
             */
            //_upFileXmlDoc = new UpFileXmlDoc();
            _upFileList = new List<string>();
             _publishDictionary = new Dictionary<string, string>();
            //_executePage = new DisposePage( _upFileXmlDoc, _publishDictionary);
            //_executeTmplt = new DisposeTmplt( _upFileXmlDoc, _publishDictionary);
            //_executeResource = new DisposeResource( _upFileXmlDoc, _publishDictionary);
            //_executeAnyFile = new DisposeAnyFile(_upFileXmlDoc, _publishDictionary);
             _executeAnyFile = new DisposeAnyFile(_upFileList, _publishDictionary);
        }

        /// <summary>
        /// 入口函数
        /// </summary>
        public void ExecutePublish()
        {
            //判断是否登陆
            if (!Service.User.IsLogined)
            {
                ///弹出登陆窗口
                Service.User.ShowLoginForm();
            }

            ///再次确定是否登陆，若已登陆则继续，否则返回。
            if (!Service.User.IsLogined)
            {
                return;
            }
            //add a definition by zhangling on May 27,2008
            //将用户名添加到.sdsite文件
            Service.Sdsite.CurrentDocument.OwnerUser = Service.User.UserID;

            ///备份上传前的.sdsite文件 ,暂时还没啥作用
            string sdsiteFilePath = Service.Sdsite.CurrentDocument.AbsoluteFilePath;
            File.Copy(sdsiteFilePath, PathService.Site_Temp_Folder + Service.Sdsite.CurrentDocument.SdsiteName, true);

            ////modified by zhangling on June 2,2008
            ///把.sdsite加入上传列表
            //_upFileXmlDoc.AddFileItem(Path.GetFileName(sdsiteFilePath));
            _upFileList.Add(Path.GetFileName(sdsiteFilePath));

            ///得到.sdsite文件里sdsite/channel这个元素引用
            FolderXmlElement channelRootEle = Service.Sdsite.CurrentDocument.RootChannel;

            //遍历本地.sdsite，将IsModified==true的加入上传列表
            Search(channelRootEle);

            ////modified by zhangling on June 2,2008
            //开始发送文件并修改发布状态
            //SocketServer socketServer = new SocketServer(_upFileXmlDoc, _publishDictionary);
            SocketServer socketServer = new SocketServer(_upFileList, _publishDictionary);
            socketServer.Upload();
            //{
            //    if (MessageService.Show("发布成功！\r\n\r\n是否现在浏览？", System.Windows.Forms.MessageBoxButtons.OKCancel)
            //        == DialogResult.OK)
            //    {
            //        string url = @"http://{0}.{1}.{2}";
            //        url = string.Format(url, Service.Sdsite.CurrentDocument.SdsiteName,
            //            Service.User.UserID, "SimplusD.net");
            //        Process.Start(url);
            //    }
            //}
        }

        /// <summary>
        /// 遍历本地.sdsite，将IsModified==true的加入上传列表
        /// </summary>
        private void Search(FolderXmlElement folderEle)
        {
            foreach (AnyXmlElement anyXmlEle in folderEle.ChildNodes)
            {
                if (anyXmlEle.Name != "siteShowItem")
                {
                    SimpleExIndexXmlElement simpleEle = anyXmlEle as SimpleExIndexXmlElement;

                    switch (simpleEle.Name)
                    {
                        case "folder":
                        case "channel":
                            //todo:有时候IsModified没改过来，比如某文件移动到频道下等，处理好后将注释删除
                            //&&simpleEle.IsModified == true)
                            if (simpleEle.IsDeleted == false && simpleEle.IsPublish == true)
                            {
                                Search(simpleEle as FolderXmlElement);
                            }
                            break;
                        case "tmpltRootFolder":
                        case "resources":
                            Search(simpleEle as FolderXmlElement);
                            break;
                        case "page":
                        case "tmplt":
                        case "file":
                            //修改为真
                            _executeAnyFile.ExecuteAnyFilePublish(simpleEle as SimpleExIndexXmlElement);
                            break;
                    }
                }
            }
        }
    }
}
