using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Jeelu.SimplusPP.Properties;
using Jeelu.Win;

namespace Jeelu.SimplusPagePreviewer
{
    public class PerviewViewForm : BaseForm
    {
        #region 定义变量
        Thread _thread;
        Perviewer _previewer;
        private System.Windows.Forms.ToolStripMenuItem ViewContentToolStripMenuItem;  // 显示详细内容ToolStripMenuItem
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;  // 退出ToolStripMenuItem
        private System.Windows.Forms.ContextMenuStrip ContainMenuStrip;
        private string absolutePath;
        private NotifyIcon notifyIcon;
        private IntPtr _simplusDHandle;
        public IntPtr SimplusDHandle
        {
            get { return _simplusDHandle; }
            set { _simplusDHandle = value; }
        }
        #endregion

        PerviewInfoForm _perviewInfo;

        /// <summary>
        /// 构造函数:传入路径.
        /// </summary>
        public PerviewViewForm(string AbsolutePath)
        {
            this.absolutePath = AbsolutePath;
            this.Opacity = 0;
            //this.ShowInTaskbar = false;
        }

        /// <summary>
        /// 初始化NotifyIcon形式的这个应用程序（无窗体，仅一个Context菜单）
        /// </summary>
        public void Run()
        {
            /// 初始化拼音码表
            Utility.Pinyin.Initialize(Path.Combine(Application.StartupPath, "PinYin.mb"));

            #region 定义窗体
            notifyIcon = new NotifyIcon();
            this.ContainMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.ViewContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            //
            //notifyIcon
            //

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resources));
            Icon icon = (Icon)(resources.GetObject("perview"));
            notifyIcon.DoubleClick += new EventHandler(NotifyIcon_DoubleClick);
            notifyIcon.Icon = icon;
            notifyIcon.MouseMove += new MouseEventHandler(NotifyIcon_MouseMove);
            notifyIcon.ContextMenuStrip = this.ContainMenuStrip;

            //
            //ContainMenuStrip
            //
            this.ContainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewContentToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.ContainMenuStrip.Name = "ContainMenuStrip";
            this.ContainMenuStrip.Size = new System.Drawing.Size(143, 48);
            // 
            // 显示详细内容ToolStripMenuItem
            // 
            this.ViewContentToolStripMenuItem.Name = "ViewContentToolStripMenuItem";
            this.ViewContentToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.ViewContentToolStripMenuItem.Text = "显示详细内容";
            this.ViewContentToolStripMenuItem.Click += new EventHandler(ViewContentToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new EventHandler(ExitToolStripMenuItem_Click);

#if DEBUG
            ///DEBUG状态添加“查看异常日志”菜单
            ToolStripMenuItem showErrorLogMenuItem = new ToolStripMenuItem("查看异常日志");
            showErrorLogMenuItem.Click += new EventHandler(ShowErrorLogMenuItem_Click);
            this.ContainMenuStrip.Items.Insert(0, showErrorLogMenuItem);
#endif
            #endregion

            _previewer = new Perviewer();
            PathService.AbsolutePath = absolutePath;

            _thread = new Thread(new ThreadStart(_previewer.Run));
            _thread.IsBackground = true;
            _thread.Start();

            notifyIcon.Visible = true;

            Application.ThreadExit += new EventHandler(Application_ThreadExit);

            Application.Run(this);
        }

        #region 具体执行事件

        void ShowErrorLogMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(ExceptionService.LogFileName);
        }

        void Application_ThreadExit(object sender, EventArgs e)
        {
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            }
        }

        void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 显示详细内容
        /// </summary>
        void ViewContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _perviewInfo = new PerviewInfoForm(_previewer.Index,Perviewer.Port ,_previewer.ProjectName);
            _perviewInfo.ShowDialog();
        }

        /// <summary>
        /// 鼠标划过显示状态
        /// </summary>
        void NotifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            ((NotifyIcon)sender).Text = _previewer.ProjectName+"--预览服务器正在使用" + Perviewer.Port + "端口";
        }

        /// <summary>
        /// 双击显示详细内容
        /// </summary>
        void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ViewContentToolStripMenuItem.PerformClick();
        }

        #endregion

        protected override void WndProc(ref Message m)
        {
            ///当点击右上角的叉进行关闭时,应该关闭项目
            if (m.Msg == 16)
            {
                this.Hide();
                return;
            }
            else if (m.Msg == WM_SENDTHISHWND)
            {
                _simplusDHandle = m.WParam;
                SendMessage(_simplusDHandle, WM_WEBVIEWISSTART, IntPtr.Zero, IntPtr.Zero);
            }
            else if (m.Msg == WM_SENDTOCLOSEFORM)
            {
                Application.Exit();
            }

            base.WndProc(ref m);
        }

        public const int WM_USER = 0x0400;
        public const int WM_SENDTHISHWND = WM_USER + 143;
        public const int WM_WEBVIEWISSTART = WM_USER + 144;
        public const int WM_SENDTOCLOSEFORM = WM_USER + 145;
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hwnd,
            int wMsg,
            IntPtr wParam,
            IntPtr lParam
        );

        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        bool _isFirstShow = true;
        protected override void OnShown(EventArgs e)
        {
            if (_isFirstShow)
            {
                _isFirstShow = false;
                _timer.Interval = 3000;
                _timer.Tick += delegate
                {
                    Hide();
                    _timer.Stop();
                    _timer.Dispose();
                };
                _timer.Start();
            }
            base.OnShown(e);
        }
    }
}
