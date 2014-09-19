using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace Jeelu.SimplusSoftwareUpdate
{
    /// <summary>
    /// 用来将下载下来的文件安装到SimplusD目录
    /// </summary>
    public class Setup
    {
        public string SrcFolder { get; private set; }
        public string TargetFolder { get; private set; }
        private string[] Files;

        public int AllCount { get; private set; }
        public int ProcessedCount { get; private set; }
        private Thread _thread;

        Action _callbackDoEnd;
        Action<Exception> _callbackError;

        public Setup(string srcFolder,string targetFolder)
        {
            this.SrcFolder = srcFolder;
            this.TargetFolder = targetFolder;

            Files = Directory.GetFiles(srcFolder,"*",SearchOption.AllDirectories);
            AllCount = Files.Length;
        }

        /// <summary>
        /// 异步的方式执行
        /// </summary>
        /// <param name="callbackDoEnd"></param>
        /// <param name="callbackError"></param>
        public void BeginRun(Action callbackDoEnd,Action<Exception> callbackError)
        {
            Debug.Assert(callbackDoEnd != null);
            Debug.Assert(callbackError != null);

            _callbackDoEnd = callbackDoEnd;
            _callbackError = callbackError;

            _thread = new Thread(new ThreadStart(BeginRunCore));
            _thread.Start();
        }

        private void BeginRunCore()
        {
            try
            {
                RunCore();
            }
            catch (Exception ex)
            {
                if (_callbackError != null)
                {
                    _callbackError(ex);
                }
                return;
            }
            if (this._callbackDoEnd != null)
            {
                _callbackDoEnd();
            }
        }

        /// <summary>
        /// 阻塞的方式执行
        /// </summary>
        public bool Run()
        {
            return RunCore();
        }

        private bool RunCore()
        {
            return MoveFolderFiles(SrcFolder, TargetFolder);
        }

        private bool MoveFolderFiles(string srcFolder, string targetFolder)
        {
            Debug.Assert(!string.IsNullOrEmpty(srcFolder));
            Debug.Assert(!string.IsNullOrEmpty(targetFolder));

            ///若目标文件夹不存在，创建
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }

            ///找文件夹下的文件路径集合
            string[] files = Directory.GetFiles(srcFolder, "*", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                ///移动文件
                string newFileName = Path.Combine(targetFolder, Path.GetFileName(file));

            ContinueMove:
                try
                {
                    Nullable<FileAttributes> fileAtts = null;
                    ///去掉文件只读属性
                    if (File.Exists(newFileName))
                    {
                        fileAtts = File.GetAttributes(newFileName);
                        File.SetAttributes(newFileName, FileAttributes.Normal);

                        ///先删除原文件
                        File.Delete(newFileName);
                    }

                    ///移动文件
                    File.Move(file, newFileName);

                    if (fileAtts != null)
                    {
                        File.SetAttributes(newFileName, fileAtts.Value);
                    }

                    ///增加处理过的文件计数
                    ProcessedCount++;
                    break;
                }
                catch (Exception)
                {
                MsgAgain:
                    string strMsg = "移动文件“{0}”失败！请关闭SimplusD!后点“是”重试。\r\n\r\n是否重试？";
                    strMsg = string.Format(strMsg, Path.GetFileName(file));
                    if (MessageBox.Show(strMsg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        goto ContinueMove;
                    }
                    else
                    {
                        if (MessageBox.Show("确定退出安装吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                            == DialogResult.OK)
                        {
                            return false;
                        }
                        else
                        {
                            goto MsgAgain;
                        }
                    }
                }
            }

            ///遍历子文件夹
            string[] folders = Directory.GetDirectories(srcFolder, "*", SearchOption.TopDirectoryOnly);
            foreach (string folder in folders)
            {
                bool result = MoveFolderFiles(folder, Path.Combine(targetFolder, Path.GetFileName(folder)));
                if (!result)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public delegate void Action();
}
