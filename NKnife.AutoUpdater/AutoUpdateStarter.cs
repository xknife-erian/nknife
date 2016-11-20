using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace NKnife.AutoUpdater
{
    /// <summary>主程序对Update组件的调用。本类型一般应该采用链接的方式加入调用项目中进行使用。
    /// </summary>
    public class AutoUpdateStarter
    {
        /// <summary>自动更新器，一个可执行文件
        /// </summary>
        private static string UpdaterExeFile
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pansoft.Updater.exe"); }
        }

        /// <summary>
        /// 起始方法：主调用程序对 Updater 组件的调用。
        /// </summary>
        /// <param name="requestUpdate">是否需要自动更新，一般来讲，该参数将保存主程序中</param>
        /// <param name="args">参数集合</param>
        /// <param name="logger">记录日志的方法</param>
        /// <param name="isDebug">是否是调试状态，仅供开发期测试时使用</param>
        /// <returns>当返回True时，程序应及时终止，反之，程序应继续运行</returns>
        public static bool Run(bool requestUpdate, string[] args, Action<string> logger, bool isDebug = false)
        {
            //描述“已经更新过”的时候，出现在由更新程序更新动作完成后，调用本程序启动时
            bool alreadyUpdate = IsAlreadyUpdated(args, logger);

            if (logger != null)
                logger.Invoke(string.Format("检查远程自动更新选项:{0}", requestUpdate ? "需要自动更新。" : "无需自动更新。"));
            if (!alreadyUpdate && requestUpdate)
            {
                if (File.Exists(UpdaterExeFile))
                {
                    string exePath = Application.ExecutablePath;
                    var arguments = new StringBuilder();
                    //传递当前主程序的路径
                    arguments.Append(string.Format("-caller:\"{0}\"", exePath));
                    //传递当前主程序的版本号
                    string callVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
                    arguments.Append(":").Append(callVersion);
                    arguments.Append(' ');
                    //传递Update的命令，参数是临时目录名
                    arguments.Append(string.Format("-updating:{0}", Guid.NewGuid()));
                    if (!isDebug)
                        RunUpdater(arguments);
                    else //仅供测试使用
                        GetTestForm(arguments).ShowDialog();
                    return true;
                }
                else
                {
                    if (logger != null)
                        logger.Invoke(string.Format("未找到自动更新程序。{0}", UpdaterExeFile));
                }
            }
            if (logger != null)
                logger.Invoke(string.Format("远程自动更新函数调用完成。"));
            return false;
        }

        /// <summary>运行自动更新器
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        private static void RunUpdater(StringBuilder arguments)
        {
            Process.Start(UpdaterExeFile, arguments.ToString());
        }

        /// <summary>根据指定的命令行参数判断是否已经执行过自动更新。描述“已经更新过”的时候，出现在由更新程序更新动作完成后，调用本程序启动时。
        /// </summary>
        /// <param name="args">The args.</param>
        /// <param name="logger">The logger.</param>
        /// <returns>
        ///   <c>true</c> if [is already updated] [the specified args]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsAlreadyUpdated(string[] args, Action<string> logger)
        {
            bool alreadyUpdate = false;
            if (args != null && args.Length > 0 && args[0].Equals("-alreadyupdate"))
            {
                alreadyUpdate = true;
                logger.Invoke(string.Format("自动更新已经执行过。"));
            }
            return alreadyUpdate;
        }

        /// <summary>获取一个测试时显示命令行参数的窗体
        /// </summary>
        private static Form GetTestForm(StringBuilder args)
        {
            var form = new Form
                           {
                               ShowIcon = false,
                               StartPosition = FormStartPosition.CenterScreen,
                               Size = new Size(750, 300),
                               Font = new Font("Tahoma", 11F),
                           };
            var box = new TextBox
                          {
                              Text = args.ToString(),
                              Multiline = true,
                              ScrollBars = ScrollBars.Vertical,
                              Dock = DockStyle.Fill
                          };
            form.Controls.Add(box);
            return form;
        }
    }
}