using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.IO;
using System.Runtime.InteropServices;

namespace Jeelu.Win
{
    public class InsertElementHelper
    {
        public static bool AddToSelection(IHTMLDocument2 m_pDoc2, string s_BeginHtml, string s_EndHtml)
        {
            if (m_pDoc2 == null)
                return false;
            IHTMLSelectionObject sel = m_pDoc2.selection as IHTMLSelectionObject;
            IHTMLElement ele = m_pDoc2.activeElement as IHTMLElement;
            if (sel == null)
                return false;
            IHTMLTxtRange range = sel.createRange() as IHTMLTxtRange;
            if (range == null)
                return false;
            string shtml = string.Empty;
            if (!string.IsNullOrEmpty(s_BeginHtml))
            {
                if (s_EndHtml.IndexOf("span") < 0)
                {
                    shtml = s_BeginHtml + range.htmlText + s_EndHtml;
                }
                else
                {
                    #region
                    string xmstr = range.htmlText;// GeneralMethods.tidy();
                    if (xmstr.Length > 4)
                        xmstr = xmstr.Replace("\n", "");//去掉/n/r
                    xmstr = xmstr.Replace("\r", "");
                    string[] style = new string[100];
                    int i = 0;
                    int span = xmstr.IndexOf("span");
                    string spanstr = ((span == 1) ? "</span>" : "");
                    while (xmstr.Length > 0)//将选中字符串分段存入数组
                    {
                        if (xmstr.IndexOf("<span") != 0)//span处理的字符串不在最前
                        {
                            if (xmstr.IndexOf("<span") == -1)//没有span的情况，即没有经过样式处理的字符串
                            {
                                style[i++] = xmstr;
                                xmstr = "";
                                break;
                            }
                            style[i++] = xmstr.Substring(0, xmstr.IndexOf("<span"));
                            xmstr = xmstr.Substring(xmstr.IndexOf("<span"));
                        }
                        else
                        {//将span处理的字符串存入数组
                            int spans = xmstr.IndexOf("<span");
                            int spand = xmstr.IndexOf("</span>");
                            if (spand != -1 && spans != -1)
                            {
                                style[i++] = xmstr.Substring(spans, spand + 7);
                                xmstr = xmstr.Substring(spand + 7);
                            }
                        }
                    }
                    int k = -1;
                    for (int j = 0; j < style.Length; j++)
                    {
                        if (style[j] != null && style[j] != " ")
                        {
                            if (style[j].IndexOf("span") > -1)//如果包含Span
                            {
                                k++;
                                if (k == 0)
                                    style[j] = "</span>" + style[j].Substring(0, style[j].IndexOf(">") - 1) + ";" + s_BeginHtml + style[j].Substring(style[j].IndexOf(">") - 1);
                                else
                                    style[j] = style[j].Substring(0, style[j].IndexOf(">") - 1) + ";" + s_BeginHtml + style[j].Substring(style[j].IndexOf(">") - 1);
                            }
                            else
                            {
                                k++;
                                style[j] = "<span style='" + s_BeginHtml + "'>" + style[j] + "</span>";
                            }
                            spanstr += style[j];
                        }
                        if (style[j] == null)
                        {
                            break;
                        }

                    }
                    #endregion
                    if (spanstr.IndexOf("spanstyle") > -1)
                        spanstr = spanstr.Replace("spanstyle", "span style");
                    shtml = spanstr;
                }
            }
            //if (shtml.Contains("CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95"))
            //{
            //    shtml=shtml.Replace("CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95", "{id}");
            //}

            range.pasteHTML(shtml);
            range.findText("cao", 0, 0);
            //range.collapse(false);
            range.select();

            return true;
        }

        #region 插入Falsh,音频,视频
        public static void InsertFlash(HTMLDesignerContrl htmlDesigner)
        {
            string htmlCode = htmlDesigner.OpenFlashDialog();
            InsertElementHelper.AddToSelection(htmlDesigner.DesignWebBrowser.idoc2, htmlCode, "");
           
            /*InsertFlashCodeForm insertflash = new InsertFlashCodeForm(htmlDesigner);
            if (insertflash.ShowDialog() == DialogResult.OK)
            {
                if (insertflash.MediaPath != string.Empty)
                {
                    //不管如何,只要存在此文件则加入到HTML编辑器中
                    string path = insertflash.MediaPath;
                    string fwidth = insertflash.MediaWidth.ToString() + insertflash.MediaWidUint;
                    string fheight = insertflash.MediaHeight.ToString() + insertflash.MediaHeigUint;
                    string fvspace = insertflash.MediaVspace.ToString();
                    string fhspace = insertflash.MediaHspace.ToString();
                    string ftitle = insertflash.MediaTitle;
                    string faccesskey = insertflash.MediaAccessKey;
                    string ftabindex = insertflash.MediaTab;
                    string fscale = insertflash.MediaScale;
                    Align falign = insertflash.FlashAlign;
                    Quality fquality = insertflash.FlashQuality;
                    bool fisloopplay = insertflash.MediaLoop;
                    bool fisautopaly = insertflash.MediaAutoPlay;
                    string mediaID = insertflash.MediaID;
                    Flash insflash = new Flash();
                    string insflashhtml = insflash.FlashHtml(htmlDesigner.InsertUseMode,path, fwidth, fheight, fvspace, fhspace, ftitle, faccesskey, ftabindex, falign, fquality, fisloopplay, fisautopaly, fscale, mediaID);
                    AddToSelection(htmlDesigner.DesignWebBrowser.idoc2, insflashhtml, "");
                    // m_pDoc2.body.outerHTML;
                }
            }*/
        }
        public static void InsertAudio(HTMLDesignerContrl htmlDesigner)
        {

            string htmlCode = htmlDesigner.OpenAudioDialog();
            InsertElementHelper.AddToSelection(htmlDesigner.DesignWebBrowser.idoc2, htmlCode, "");
            /*InsertAudioCodeForm insertAudio = new InsertAudioCodeForm(htmlDesigner);
            if (insertAudio.ShowDialog() == DialogResult.OK)
            {
                if (insertAudio.MediaPath != string.Empty)
                {
                    //不管如何,只要存在此文件则加入到HTML编辑器中
                    string path = insertAudio.MediaPath;// FileFullPath;// Path.GetFileName(Path.GetDirectoryName(FileFullPath)) + "/" + Path.GetFileName(FileFullPath);
                    string mwidth = insertAudio.MediaWidth.ToString() + insertAudio.MediaWidUint;
                    string mheight = insertAudio.MediaHeight.ToString() + insertAudio.MediaHeigUint;
                    string mvspace = insertAudio.MediaVspace.ToString();
                    string mhspace = insertAudio.MediaHspace.ToString();
                    string mtitle = insertAudio.MediaTitle;
                    string maccesskey = insertAudio.MediaAccessKey;
                    string mtabindex = insertAudio.MediaTab;
                    string mscale = insertAudio.MediaScale;
                    Align malign = insertAudio.AudioAlign;
                    Quality mquality = insertAudio.AudioQuality;
                    bool misloopplay = insertAudio.MediaLoop;
                    bool misautopaly = insertAudio.MediaAutoPlay;
                    string mediaID = insertAudio.MediaID;
                    Audio insAudio = new Audio();
                    string insAudioHtml = insAudio.AudioHtml(htmlDesigner.InsertUseMode,path, mwidth, mheight, mvspace, mhspace, mtitle, maccesskey, mtabindex, malign, mquality, misloopplay, misautopaly, mscale, mediaID);
                    AddToSelection(htmlDesigner.DesignWebBrowser.idoc2, insAudioHtml, "");
                    string s = htmlDesigner.DesignWebBrowser.idoc2.body.outerHTML;
                }
            }*/
        }
        public static void InsertVideo(HTMLDesignerContrl htmlDesigner)
        {
            string htmlCode = htmlDesigner.OpenMediaDialog();
            InsertElementHelper.AddToSelection(htmlDesigner.DesignWebBrowser.idoc2, htmlCode, "");
            /*InsertVideoCodeForm inserVideo = new InsertVideoCodeForm(htmlDesigner);
            if (inserVideo.ShowDialog() == DialogResult.OK)
            {
                if (inserVideo.MediaPath != string.Empty)
                {
                    //不管如何,只要存在此文件则加入到HTML编辑器中
                    string path = inserVideo.MediaPath;// FileFullPath;// Path.GetFileName(Path.GetDirectoryName(FileFullPath)) + "/" + Path.GetFileName(FileFullPath);
                    string mwidth = inserVideo.MediaWidth.ToString() + inserVideo.MediaWidUint;
                    string mheight = inserVideo.MediaHeight.ToString() + inserVideo.MediaHeigUint;
                    string mvspace = inserVideo.MediaVspace.ToString();
                    string mhspace = inserVideo.MediaHspace.ToString();
                    string mtitle = inserVideo.MediaTitle;
                    string maccesskey = inserVideo.MediaAccessKey;
                    string mtabindex = inserVideo.MediaTab;
                    string mscale = inserVideo.MediaScale;
                    Align malign = inserVideo.MediaAlign;
                    Quality mquality = inserVideo.MediaQuality;
                    bool misloopplay = inserVideo.MediaLoop;
                    bool misautopaly = inserVideo.MediaAutoPlay;
                    string mediaID = inserVideo.MediaID;
                    Video insmedia = new Video();
                    string insMediahtml = insmedia.MediaHtml(htmlDesigner.InsertUseMode,path, mwidth, mheight, mvspace, mhspace, mtitle, maccesskey, mtabindex, malign, mquality, misloopplay, misautopaly, mscale, mediaID);
                    AddToSelection(htmlDesigner.DesignWebBrowser.idoc2, insMediahtml, "");
                }
            }*/
        }
        #endregion
        #region Email&Image
        public static void InsertEmail(IHTMLDocument2 m_pDoc2)
        {
            InsertEmailCodeForm insertemail = new InsertEmailCodeForm();
            if (insertemail.ShowDialog() == DialogResult.OK)
            {
                AddToSelection(m_pDoc2, insertemail.InsertEmailHTML, ""); ;
            }
        }
        public static void InsertImage(HTMLDesignerContrl htmlDesigner)
        {
          string htmlCode= htmlDesigner.OpenPicDialog();
          
           /* InsertPicCodeForm insertpic = new InsertPicCodeForm();
            insertpic.HtmlDesigner = htmlDesigner;
            if (insertpic.ShowDialog() == DialogResult.OK)
            {
                InsertElementHelper.AddToSelection(htmlDesigner.DesignWebBrowser.idoc2, insertpic.InsertImageHTML, "");
            }*/
           InsertElementHelper.AddToSelection(htmlDesigner.DesignWebBrowser.idoc2, htmlCode, "");
           
        }
        #endregion
        #region Table & DataTime
        public static void Inserttable(IHTMLDocument2 m_pDoc2)
        {
            InsertTableCodeForm insertTable = new InsertTableCodeForm();
            if (insertTable.ShowDialog() == DialogResult.OK)
            {
                AddToSelection(m_pDoc2, insertTable.InsertTableHTML, "");
            }
        }
        public static void InsertDateTime(IHTMLDocument2 m_pDoc2)
        {
            InsertDatetimeForm insertDateTime = new InsertDatetimeForm();
            if (insertDateTime.ShowDialog() == DialogResult.OK)
            {

                InsertElementHelper.AddToSelection(m_pDoc2, insertDateTime.InsertDateTimeHTML, "");
            }
        }
        #endregion




        public static void Insertlink(HTMLDesignerContrl htmlDesigner)
        {
            htmlDesigner.InsertLink();
            return;
            string innerText = "";
            if (htmlDesigner.DesignWebBrowser.idoc2.selection.type == "Text")//如果选中的是文本
            {
                IHTMLTxtRange searchRange = (IHTMLTxtRange)htmlDesigner.DesignWebBrowser.idoc2.selection.createRange();
                innerText = searchRange.htmlText;//获取文本的html代码
            }


            //if (m_pDoc2.selection.type == "Control")//如果选中的控件
            {
                /* IHTMLTxtRange searchRange = (IHTMLTxtRange)m_pDoc2.selection.createRange();
                 innerText = searchRange.htmlText;//获取文本的html代码*/
            }

            //frmInsertLinkCode insertLink = new frmInsertLinkCode();
            //HTMLAnchorElement linkEle = htmlDesigner.CurrentElement as HTMLAnchorElement;
            //if (linkEle != null)
            //{//给弹出的控件赋值
            //    string hrefStr = linkEle.href.Replace("about:blank", "").Replace("about:", "");
            //    if (hrefStr.IndexOf("#") > 0)
            //    {
            //        insertLink.linkUrl = hrefStr.Substring(0, hrefStr.IndexOf("#"));
            //        insertLink.BookMark = hrefStr.Substring(1 + hrefStr.IndexOf("#"));
            //        insertLink.linkTarget = linkEle.target;
            //        insertLink.LinkTip = linkEle.title;
            //        insertLink.AccessKey = linkEle.accessKey;
            //    }
            //    else
            //    {
            //        insertLink.linkUrl = hrefStr;
            //    }
            //}
            //if (insertLink.ShowDialog() == DialogResult.OK)
            //{
            //    //从弹出的控件中取值
            //    if ((m_pDoc2.selection.type == "None") && (htmlDesigner.LinkBe != null))//如果没有选中文本，则对当前链接进行更改，否则插入新链接
            //    {
            //        htmlDesigner.LinkBe.Element.href = insertLink.linkUrl;
            //        htmlDesigner.LinkBe.Element.target = insertLink.linkTarget;
            //        htmlDesigner.LinkBe.Element.accessKey = insertLink.AccessKey;
            //         htmlDesigner.LinkBe.Element.tabIndex = insertLink.BookMark;
            //    }
            //    else
            //    {
            //        m_pDoc2.selection.clear();
            //        string linkURL = insertLink.linkUrl;
            //        string linkTarget = insertLink.linkTarget;
            //        string linkAccesskey = insertLink.AccessKey;
            //        string linkTip = insertLink.LinkTip;
            //        string linkBookMark = insertLink.BookMark;
            //        LINK link = new LINK();
            //        string insertlinkhtml = link.LinkHtml(innerText, linkURL, linkTarget, linkTip, linkAccesskey, linkBookMark);
            //        AddToSelection(m_pDoc2, insertlinkhtml, "");

            //    }
            //}
        }

       /* private IntPtr m_NullPointer = IntPtr.Zero;
        public bool OleCommandExec(bool bTopLevel, MSHTML_COMMAND_IDS CmdID, IHTMLDocument2 doc2)
        {
            IOleCommandTarget m_Doc2OleCommandTraget = null;
            IntPtr m_Guid_MSHTML = m_NullPointer;
            bool bret = false;
            try
            {
                byte[] guidbytes = Iid_Clsids.Guid_MSHTML.ToByteArray();
                m_Guid_MSHTML = Marshal.AllocCoTaskMem((int)(guidbytes.Length * 2));
                Marshal.Copy(guidbytes, 0, m_Guid_MSHTML, guidbytes.Length);

                if (doc2 == null)
                    return false;
                m_Doc2OleCommandTraget = doc2 as IOleCommandTarget;
                if (m_Doc2OleCommandTraget == null)
                    return false;

                bret = (m_Doc2OleCommandTraget.Exec(m_Guid_MSHTML, (uint)CmdID,
                    (uint)OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT,
                    m_NullPointer, m_NullPointer) == Hresults.S_OK) ? true : false;

                Marshal.FreeCoTaskMem(m_Guid_MSHTML);
                m_Guid_MSHTML = m_NullPointer;
            }
            finally
            {
                if (m_Guid_MSHTML != m_NullPointer)
                    Marshal.FreeCoTaskMem(m_Guid_MSHTML);
            }
            return bret;
        }*/

        //public bool ExecCommand(bool bTopLevel, string CmdId, bool showUI, object CmdValue, IHTMLDocument2 doc2)
        //{
        //    if (doc2 != null)
        //        return doc2.execCommand(CmdId, showUI, CmdValue);
        //    else
        //        return false;
        //}
        //
        



        //#endregion

    }
}

