using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;
using NKnife.AutoUpdater.Common;
using NKnife.AutoUpdater.Interfaces;

namespace NKnife.AutoUpdater.Commands.UpdatingSubMethods
{
    /// <summary>一些升级补丁程序的处理
    /// </summary>
    internal class Patchs
    {
        private readonly AutoResetEvent _AutoResetEvent = new AutoResetEvent(false);

        public bool Run(IUpdaterFileVerify fileVerify, XmlNode indexXml, bool isDebug = false, FileInfo debugFile = null)
        {
            StringCollection patchs;
            if (fileVerify.VerifyPatchs(indexXml, out patchs))
            {
                foreach (string patchfile in patchs)
                {
                    string patch = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, patchfile);
                    try
                    {
                        FileInfo self = Currents.Me.FileGetter.GetTargetFile(patchfile);
                        self.CopyTo(patch, true);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                    Logger.WriteLine(string.Format("探索到补丁程序:{0}", patch));
                    if (File.Exists(patch))
                    {
                        try
                        {
                            var process = new Process();
                            process.Exited += ProcessExited;
                            process.StartInfo = new ProcessStartInfo(patch);
                            Logger.WriteLine(string.Format("执行补丁程序:{0}", patch), true);
                            process.Start();
                            _AutoResetEvent.WaitOne(); //等待补丁程序执行完成
                        }
                        catch (Exception e)
                        {
                            Logger.WriteLine(string.Format("补丁程序:{0}执行异常。", patch), e);
                        }
                    }
                    else
                    {
                        Logger.WriteLine(string.Format("补丁程序不存在:{0}", patch));
                    }
                }
            }
            return true;
        }

        /// <summary>当补丁程序执行完成时，结束线程的等待
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessExited(object sender, EventArgs e)
        {
            Thread.Sleep(50);
            _AutoResetEvent.Reset();
        }
    }
}