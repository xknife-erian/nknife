using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win.Startup
{
    public class SimplusDMain
    {
        [STAThread()]
        public static void Main(string[] args)
        {
            try
            {
                //�����������Ƿ��ڽ���
                Mutex mutex = new Mutex(false, "SimplusD_Update");
                bool isHasMutex = mutex.WaitOne(100, false);
                if (!isHasMutex)
                {
                    mutex.Close();
                    MessageBox.Show("����ִ��������£��������������ɺ�����������", "SimplusD!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    mutex.ReleaseMutex();
                    mutex.Close();
                }

                ////��鱾���Ƿ��Ѿ���������ϵĸ��£����У�����ʾ��װ
                //XmlDocument docUpdateInfo = new XmlDocument();
                //string strUpdateInfoFile = Path.Combine(Application.StartupPath, "UpdateInfo.xml");
                //docUpdateInfo.Load(strUpdateInfoFile);
                //XmlElement eleUpdate = docUpdateInfo.DocumentElement["update"];
                //string updateState = eleUpdate.GetAttribute("state");

                ////�Ѿ����سɹ�������ʾ��װ
                //if (updateState.Equals("Downloaded", StringComparison.CurrentCultureIgnoreCase))
                //{
                //    if (MessageBox.Show("�Ѿ����°汾����������ϡ�\r\n\r\n�Ƿ�װ��", "SimplusD!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                //        == DialogResult.OK)
                //    {
                //        string setupFile = eleUpdate.GetAttribute("setupFile");
                //        Process pSetupFile = new Process();
                //        pSetupFile.StartInfo.FileName = setupFile;
                //        pSetupFile.StartInfo.UseShellExecute = true;
                //        pSetupFile.Start();
                //        pSetupFile.Dispose();
                //    }
                //    return;
                //}

                #region �����������

                bool noUpdate = false;
                string willOpenFile = null;
                if (args != null && args.Length != 0)
                {
                    string prevArgs = "";   //��һ������
                    foreach (string item in args)
                    {
                        ///-n��ʾ����Ҫ�����������
                        if (item.Equals("-n", StringComparison.CurrentCultureIgnoreCase))
                        {
                            noUpdate = true;
                        }
                        else
                        {
                            //-s���.sdsite�ļ�·��
                            if (prevArgs.Equals("-s", StringComparison.CurrentCultureIgnoreCase))
                            {
                                willOpenFile = item;
                            }
                        }

                        ///��¼��һ������
                        prevArgs = item;
                    }
                }

                #endregion

#if DEBUG
#else
                ///���ø��³���
                if (!noUpdate)
                {
                    string strUpdateFile = Path.Combine(Application.StartupPath, "Update.exe");
                    if (File.Exists(strUpdateFile))
                    {
                        List<string> list = new List<string>();
                        list.Add("-s");   //-s��ʾ�����ֶ����(update.exe)
                        if (!string.IsNullOrEmpty(willOpenFile))
                        {
                            list.Add("-r \"" + willOpenFile + "\"");   //-r��ʾ����sdsite�ļ�·��(update.exe)
                        }

                        Process.Start(strUpdateFile, string.Join(" ", list.ToArray()));
                        return;
                    }
                    else
                    {
                        Debug.Fail("û���ҵ����³���:" + Path.GetFileName(strUpdateFile));
                    }
                }
#endif
                //����ź������Ը���Update��ʵ��������
                Semaphore sema = new Semaphore(100, 100, "SimplusD_MainForm");
                bool hasSema = sema.WaitOne(1000, false);
                if (!hasSema)
                {
                    sema.Close();
                    MessageBox.Show("���̴򿪹��࣡", "SimplusD��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    try
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        ///����MainForm
                        SplashScreenForm.ShowSplashScreen();
#if DEBUG
                        ///��Ϊ�˹ۿ���ӭ����
                        //Thread.Sleep(3 * 1000);
#endif
                        WorkbenchForm.Initialized += delegate
                        {
                            SplashScreenForm.SplashScreen.Close();
                        };
                        //��ʼ��WorkbenchForm����,���洫�����,�������˫��ĳ�ļ�,�����Ĺ��������˱�����,��ô���������ļ�·��
                        WorkbenchForm.Initialize(willOpenFile);
                        Application.Run(WorkbenchForm.MainForm);
                    }
                    finally
                    {
                        sema.Release();
                        sema.Close();
                    }
                }
            }
#if DEBUG
            finally
            {
            }
#else
            catch (Exception exception)
            {
                try
                {
                    string errorMsg = "�쳣ʱ��:{0}\r\n�쳣����:{1}\r\n�쳣��Ϣ:{2}\r\n��ջ��Ϣ:\r\n{3}\r\n\r\n";
                    errorMsg = string.Format(errorMsg, DateTime.Now, exception.GetType().FullName,
                        exception.Message, exception.StackTrace);
                    string fileName = Path.Combine(Application.StartupPath, "error.log");
                    File.AppendAllText(fileName, errorMsg, Encoding.UTF8);
                }
                catch { }
                MessageBox.Show("�����쳣��������ֹ��","SimplusD!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
#endif
        }
    }
}
