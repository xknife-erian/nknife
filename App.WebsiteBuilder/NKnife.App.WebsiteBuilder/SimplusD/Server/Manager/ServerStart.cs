using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Jeelu.SimplusD.Server;
using System.Diagnostics;
using System.IO;
using Jeelu.Data;

namespace Jeelu.SimplusD.Server.Manager
{
    class ServerStart
    {
        Thread t1;
        private ToolStripMenuItem ViewContentToolStripMenuItem;  // 显示详细内容ToolStripMenuItem
        private ToolStripMenuItem OpenSdsitesFolderToolStripMenuItem;  // 打开Sdsites文件夹ToolStripMenuItem
        private ToolStripMenuItem OpenHtmlsFolderToolStripMenuItem;  // 打开htmls文件夹ToolStripMenuItem
        private ToolStripMenuItem ExitToolStripMenuItem;  // 退出ToolStripMenuItem
        private ToolStripMenuItem ShowExLogToolStripMenuItem;   //打开异常日志

        private System.Windows.Forms.ContextMenuStrip ContainMenuStrip;
        private NotifyIcon notifyIcon;
        public void Run()
        {
            notifyIcon = new NotifyIcon();
            this.ContainMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.ViewContentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowExLogToolStripMenuItem = new ToolStripMenuItem();
            OpenSdsitesFolderToolStripMenuItem = new ToolStripMenuItem();
            OpenHtmlsFolderToolStripMenuItem = new ToolStripMenuItem();

            //
            //notifyIcon
            //
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Jeelu.SimplusD.Server.Properties.Resources));
            System.Drawing.Icon icon = (Icon)(resources.GetObject("a"));
            notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
            notifyIcon.Icon = icon;
            notifyIcon.ContextMenuStrip = this.ContainMenuStrip;

            //
            //ContainMenuStrip
            //
            this.ContainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.ShowExLogToolStripMenuItem,
                this.OpenSdsitesFolderToolStripMenuItem,
                this.OpenHtmlsFolderToolStripMenuItem,
                this.ViewContentToolStripMenuItem,
                this.ExitToolStripMenuItem});
            this.ContainMenuStrip.Name = "ContainMenuStrip";
            this.ContainMenuStrip.Size = new System.Drawing.Size(143, 48);
            // 
            // 打开异常日志ToolStripMenuItem
            // 
            this.ViewContentToolStripMenuItem.Name = "ViewContentToolStripMenuItem";
            this.ViewContentToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.ViewContentToolStripMenuItem.Text = "显示详细内容";
            this.ViewContentToolStripMenuItem.Click += new EventHandler(ViewContentToolStripMenuItem_Click);
            // 
            // 打开Sdsites文件夹ToolStripMenuItem
            // 
            this.OpenSdsitesFolderToolStripMenuItem.Name = "OpenSdsitesFolderToolStripMenuItem";
            this.OpenSdsitesFolderToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.OpenSdsitesFolderToolStripMenuItem.Text = "打开Sdsites文件夹";
            this.OpenSdsitesFolderToolStripMenuItem.Click += new EventHandler(OpenSdsitesFolderToolStripMenuItem_Click);
            // 
            // 打开Htmls文件夹ToolStripMenuItem
            // 
            this.OpenHtmlsFolderToolStripMenuItem.Name = "OpenHtmlsFolderToolStripMenuItem";
            this.OpenHtmlsFolderToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.OpenHtmlsFolderToolStripMenuItem.Text = "打开Htmls文件夹";
            this.OpenHtmlsFolderToolStripMenuItem.Click += new EventHandler(OpenHtmlsFolderToolStripMenuItem_Click);
            // 
            // 查看异常日志ToolStripMenuItem
            // 
            this.ShowExLogToolStripMenuItem.Name = "ShowExLogToolStripMenuItem";
            this.ShowExLogToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.ShowExLogToolStripMenuItem.Text = "查看异常日志";
            this.ShowExLogToolStripMenuItem.Click += new EventHandler(ShowExLogToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new EventHandler(ExitToolStripMenuItem_Click);

            notifyIcon.Visible = true;

            ///将初始化MySql
            LogService.init();

            ///启动一个线程，监听客户端请求
            ServerCore serverCore = new ServerCore();
            t1 = new Thread(new ThreadStart(serverCore.Listen));
            t1.IsBackground = true;
            t1.Start();
            Application.Run();
        }

        void OpenHtmlsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(AnyFilePath.SdWebFilePath);
            }
            catch
            {
            }
        }

        void OpenSdsitesFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(AnyFilePath.SdSiteFilePath);
            }
            catch
            {
            }
        }

        void ShowExLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strExFile = Path.Combine(Application.StartupPath, @"Log\Exception.txt");
            Process p = Process.Start(strExFile);
            if (p != null)
            {
                p.Dispose();
            }
        }

        void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void ViewContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manager.ManageForm manageFrom = new ManageForm();
            manageFrom.ShowDialog();
        }

        void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Manager.ManageForm manageFrom = new ManageForm();
            manageFrom.ShowDialog();
        }
    }
}
