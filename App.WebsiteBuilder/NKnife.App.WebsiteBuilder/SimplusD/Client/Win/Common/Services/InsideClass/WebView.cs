using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        static public class WebView
        {
            static SinglePagePublishHandle _singlePagePublishHandle;

            static int _port;
            static string _pageName;

            static private Process _process;
            static public Process Process
            {
                get { return _process; }
            }

            static public void Initialize(SinglePagePublishHandle handle)
            {
                _singlePagePublishHandle = handle;
                Service.Project.ProjectClosed += new EventHandler(ProjectService_ProjectClosed);
            }

            static void ProjectService_ProjectClosed(object sender, EventArgs e)
            {
                CloseProcess();
            }

            static public void StartupProcess()
            {
                bool isUseStartup = false;
                _pageName = _singlePagePublishHandle();

                if (!string.IsNullOrEmpty(_pageName))
                {
                    try
                    {
                        if (Process == null || Process.HasExited)
                        {
                            isUseStartup = true;
                        }
                    }
                    catch (System.Exception)
                    {
                        isUseStartup = true;
                    }

                  
                    if (isUseStartup)
                    {

                        try
                        {
                            _process = new Process();
                            _process.StartInfo.FileName = Path.Combine(PathService.SoftwarePath, "SimplusPP.exe");
                            string path = Service.Sdsite.CurrentDocument.AbsoluteFilePath;
                            path = Convert.ToBase64String(Encoding.UTF8.GetBytes(path), Base64FormattingOptions.None);
                            _process.StartInfo.Arguments = path;
                            _process.Start();
                            _process.WaitForInputIdle();

                            ///一直循环直到能取到窗口句柄
                            while (_process.MainWindowHandle == IntPtr.Zero)
                            {
                                Application.DoEvents();
                                //Thread.Sleep(100);
                                _process.Refresh();
                            }

                            ///给WebView.exe发送消息告诉它本Form的句柄
                            Utility.DllImport.SendMessage(_process.MainWindowHandle,
                                Utility.DllImport.WM_SENDTHISHWND, IntPtr.Zero, Service.Workbench.MainForm.Handle);

                            //bool isCreate;
                            //EventWaitHandle waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, path, out isCreate);
                            //waitHandle.WaitOne();
                        }
                        catch (System.Exception e)
                        {
                            Debug.Fail(e.Message);
                        }

                    }

                    _port = RunCmd(_process.Id.ToString());
                    StartExplore(_port, _pageName);
                
                }
                else
                {
                    return;
                }
             
            }

            static public void WebViewStartuped()
            {
                if (!string.IsNullOrEmpty(_pageName))
                {
                    StartExplore(_port, _pageName);
                    _pageName = "";
                }
            }

            /// <summary>
            /// 获取端口号
            /// </summary>
            static int RunCmd(string Pid)
            {
                try
                {
                    string strCmd = "netstat -a -n -o" + "|findstr " + Pid;
                    string port = "";
                    Process prc = new Process();
                    prc.StartInfo.FileName = "cmd.exe ";
                    prc.StartInfo.UseShellExecute = false;
                    prc.StartInfo.RedirectStandardInput = true;
                    prc.StartInfo.RedirectStandardOutput = true;
                    prc.StartInfo.RedirectStandardError = true;
                    prc.StartInfo.CreateNoWindow = true;
                    prc.Start();
                    prc.StandardInput.WriteLine(strCmd);
                    prc.StandardInput.Close();
                    string str = prc.StandardOutput.ReadToEnd();
                    str = str.Substring(120);
                    int i = 0;
                    while (true)
                    {
                        char[] chars = str.ToCharArray();
                        if (chars[i] == ':')
                        {
                            for (int j = i + 1; j < i + 5; j++)
                            {
                                port += chars[j];
                            }
                            break;

                        }
                        i += 1;
                    }
                    prc.Close();
                    return Convert.ToInt32(port);
                }
                catch
                {
                    return new Random().Next(1025, 60000);
                }
            }

            /// <summary>
            /// 启动默认浏览器
            /// </summary>
            static void StartExplore(int port, string pageUrl)
            {
                try
                {
                    string url = "http://127.0.0.1:" + port.ToString() + pageUrl;

                    //Process.Start(@"iexplore.exe", url);
                    Process.Start(url);
                }
                catch (System.Exception)
                {
                }
            }

            static public void CloseProcess()
            {
                if (Process != null && Process.HasExited != true)
                {
                    Utility.DllImport.SendMessage(Process.MainWindowHandle,
                        Utility.DllImport.WM_SENDTOCLOSEFORM, IntPtr.Zero, IntPtr.Zero);
                    try
                    {

                        bool isExited = Process.WaitForExit(1000);
                        if (!isExited)
                        {
                            Process.Kill();
                        }
                    }
                    catch
                    {
                        try
                        {
                            Process.Kill();
                        }
                        catch { }
                    }
                }
            }

        }

        public delegate string SinglePagePublishHandle();
        public delegate void WindowOpen(string url, string target);
    }
}