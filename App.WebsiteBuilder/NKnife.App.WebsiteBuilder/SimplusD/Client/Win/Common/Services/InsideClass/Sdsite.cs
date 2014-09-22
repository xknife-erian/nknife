using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        public static class Sdsite
        {
            static readonly private string NoValidate = "*NoValidate*";
            static public SdsiteXmlDocument CurrentDocument { get; private set; }
            static public DesignDataXmlDocument DesignDataDocument { get; private set; }

            static public bool IsOpened
            {
                get
                {
                    return CurrentDocument != null;
                }
            }

            /// <summary>
            /// 打开Sdsite网站
            /// </summary>
            /// <param name="sdsiteFilePath">.sdsite的路径</param>
            static public void OpenSdsite(string sdsiteFilePath, string userName)
            {
                if (!CanOpenProject(sdsiteFilePath, userName))
                {
                    return;
                }
                string folder = Path.GetDirectoryName(sdsiteFilePath);
                string sdsiteName = Path.GetFileNameWithoutExtension(sdsiteFilePath);
                string designDataFile = Path.Combine(folder, sdsiteName + ".layout");

                ///打开sdsite
                try
                {
                    SdsiteXmlDocument sdsiteDoc = new SdsiteXmlDocument(sdsiteFilePath);
                    sdsiteDoc.Load();

                    CurrentDocument = sdsiteDoc;
                }
                catch (System.Exception)
                {
                    Service.Sdsite.CurrentDocument = null;
                    MessageService.Show(string.Format("载入{0}失败!", Path.GetFileName(sdsiteFilePath)), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ///打开DesignData
                if (File.Exists(designDataFile))
                {
                    DesignDataDocument = new DesignDataXmlDocument(designDataFile);
                    DesignDataDocument.Load();
                }
                else
                {
                    DesignDataDocument = new DesignDataXmlDocument(designDataFile);
                    DesignDataDocument.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
<layout>
  <tree>
  </tree>
  <documents>
  </documents>
<productPropertyGroup></productPropertyGroup>
<treeOpenItems></treeOpenItems>
</layout>");
                }

                OnSdsiteOpened(EventArgs.Empty);
            }
            static internal void OpenSdsite(string sdsiteFilePath)
            {
                OpenSdsite(sdsiteFilePath, NoValidate);
            }

            /// <summary>
            /// 关闭Sdsite网站
            /// </summary>
            static public void CloseSdsite()
            {
                OnSdsiteClosing(EventArgs.Empty);

                CurrentDocument.Save();
                DesignDataDocument.Save();
                CurrentDocument.Close();
                CurrentDocument = null;
                DesignDataDocument = null;

                OnSdsiteClosed(EventArgs.Empty);
            }

            /// <summary>
            /// 测试此用户名是否可以打开此网站
            /// </summary>
            /// <param name="sdsiteFilePath"></param>
            /// <param name="userName"></param>
            /// <returns></returns>
            static public bool CanOpenProject(string sdsiteFilePath, string userName)
            {
                ////得到projectFile的目录信息
                //string projectFolder = Path.GetDirectoryName(sdsiteFilePath);
                ////得到文件名
                //string projectName = Path.GetFileNameWithoutExtension(sdsiteFilePath);
                ////为了得到layout的路径
                //string designDataFile = Path.Combine(projectFolder, projectName + ".layout");

                SdsiteXmlDocument sdsiteDoc = new SdsiteXmlDocument(sdsiteFilePath);
                sdsiteDoc.Load();
                if (userName != NoValidate)
                {
                    ///校验用户
                    if ((!string.IsNullOrEmpty(sdsiteDoc.OwnerUser))
                        && sdsiteDoc.OwnerUser != userName)
                    {
                        return false;
                    }
                }

                return true;
            }

            static public event EventHandler SdsiteOpened;
            static void OnSdsiteOpened(EventArgs e)
            {
                if (SdsiteOpened != null)
                {
                    SdsiteOpened(null, e);
                }
            }

            static public event EventHandler SdsiteClosing;
            static void OnSdsiteClosing(EventArgs e)
            {
                if (SdsiteClosing != null)
                {
                    SdsiteClosing(null, e);
                }
            }

            static public event EventHandler SdsiteClosed;
            static void OnSdsiteClosed(EventArgs e)
            {
                if (SdsiteClosed != null)
                {
                    SdsiteClosed(null, e);
                }
            }
        }
    }
}