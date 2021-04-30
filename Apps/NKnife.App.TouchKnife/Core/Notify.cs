using System;
using System.Text;
using System.Windows.Forms;
using NKnife.App.TouchInputKnife.Properties;
using NKnife.Interface;
using NKnife.IoC;
using NKnife.NLog;
using NKnife.Utility;

namespace NKnife.App.TouchInputKnife.Core
{
    public class Notify
    {
        private readonly Client _Client;
        private readonly NotifyIcon _NotifyIcon;
        private readonly UtilityRandom _Random = new UtilityRandom();
        private bool _IsConnection;

        public Notify()
        {
            _NotifyIcon = new NotifyIcon();
            _NotifyIcon.ContextMenuStrip = BuildContextMenuStrip();
            _NotifyIcon.Icon = OwnResources.Main;
            _NotifyIcon.MouseClick += _NotifyIcon_MouseClick;
            _Client = new Client();
        }

        private ContextMenuStrip BuildContextMenuStrip()
        {
            ToolStripItem hide = new ToolStripMenuItem("隐藏");
            hide.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                string command = GetCommand(0);
                _Client.SendTo(command);
            };

            ToolStripItem a = new ToolStripMenuItem("中文拼音");
            a.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                var f = new LocationForm(1);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _Client.SendTo(f.Command);
                }
            };

            ToolStripItem b = new ToolStripMenuItem("手写");
            b.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                var f = new LocationForm(2);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _Client.SendTo(f.Command);
                }
            };

            ToolStripItem c = new ToolStripMenuItem("符号");
            c.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                var f = new LocationForm(3);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _Client.SendTo(f.Command);
                }
            };

            ToolStripItem d = new ToolStripMenuItem("大写英语");
            d.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                var f = new LocationForm(5);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _Client.SendTo(f.Command);
                }
            };

            ToolStripItem e = new ToolStripMenuItem("小写英语");
            e.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                var f = new LocationForm(4);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _Client.SendTo(f.Command);
                }
            };

            ToolStripItem g = new ToolStripMenuItem("数字");
            g.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                var f = new LocationForm(6);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    _Client.SendTo(f.Command);
                }
            };

            ToolStripItem logger = new ToolStripMenuItem("日志");
            logger.Click += delegate
            {
                var loggerForm = new NLogForm {ShowInTaskbar = false};
                loggerForm.Show();
            };

            ToolStripItem version = new ToolStripLabel(DI.Get<IAbout>().AssemblyVersion.ToString());
            ToolStripItem framework = new ToolStripLabel(Environment.Version.ToString());

            ToolStripItem exit = new ToolStripMenuItem("退出");
            exit.Click += delegate
            {
                if (!_IsConnection)
                {
                    _IsConnection = true;
                    _Client.Connect();
                }
                _Client.SendTo(GetCommand(9));
            };

            var menu = new ContextMenuStrip();
            menu.Items.Add(hide);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(a);
            menu.Items.Add(b);
            menu.Items.Add(c);
            menu.Items.Add(d);
            menu.Items.Add(e);
            menu.Items.Add(g);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(logger);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(version);
            menu.Items.Add(framework);
            menu.Items.Add(exit);
            return menu;
        }

        private string GetCommand(int c)
        {
            var sb = new StringBuilder();

            sb.Append(_Random.Next(1, 9));
            sb.Append(c);
            sb.Append(_Random.Next(1, 9));
            sb.Append(_Random.Next(1, 3));
            sb.Append(_Random.Next(1, 9));
            sb.Append(_Random.Next(1, 3));

            sb.Append(_Random.Next(10000, 99999));
            sb.Append(_Random.Next(10000, 99999));
            sb.Append("`");
            string command = sb.ToString();
            return command;
        }

        private void _NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            _NotifyIcon.ContextMenuStrip.Show();
        }

        public void Show()
        {
            _NotifyIcon.Visible = true;
        }

        public void Hide()
        {
            _NotifyIcon.Visible = false;
        }
    }
}