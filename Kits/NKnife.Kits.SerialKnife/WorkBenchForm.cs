﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Kernel;
using NKnife.Kits.SerialKnife.Views;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SerialKnife
{
    public partial class WorkBenchForm : Form
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        private readonly string _DockPath = Path.Combine(Application.StartupPath, "dockpanel.config");
        private readonly DockPanel _DockPanel = new DockPanel();
        private readonly DockContent _LogView = DI.Get<LogView>();
        private readonly DockContent _ControlPanelView = DI.Get<ControlPanelView>();
        private readonly DockContent _MockDataConnectorView = DI.Get<MockSerialDataConnectorView>();



        #region 初始化
        public WorkBenchForm()
        {
            InitializeComponent();
        }

        private void WorkBenchForm_Load(object sender, EventArgs e)
        {
            InitializeDockPanel();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _DockPanel.SaveAsXml(_DockPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存Dockpanel配置文件失败，" + ex.Message);
                return;
            }

            _ControlPanelView.Close();
            base.OnFormClosing(e);
        }

        private void InitializeDockPanel()
        {
            MainPanel.Controls.Add(_DockPanel);
            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();

            try
            {
                var deserialize = new DeserializeDockContent(GetContentForm);
                _DockPanel.LoadFromXml(_DockPath, deserialize);
            }
            catch (Exception)
            {
                // 配置文件不存在或配置文件有问题时 按系统默认规则加载子窗体
                InitializeDefaultViews();
            }
            _logger.Info("DockPanel初始化完成");
        }

        private IDockContent GetContentForm(string xml)
        {
            if (xml == typeof (ControlPanelView).ToString())
                return _ControlPanelView;
            if (xml == typeof(LogView).ToString())
                return _LogView;
            if (xml == typeof (MockSerialDataConnectorView).ToString())
                return _MockDataConnectorView;
            return null;
        }

        private void InitializeDefaultViews()
        {
            _LogView.HideOnClose = true;
            _LogView.Show(_DockPanel, DockState.DockBottom);
            _MockDataConnectorView.HideOnClose = true;
            _MockDataConnectorView.Show(_DockPanel,DockState.DockRight);
            _ControlPanelView.HideOnClose = true;
            _ControlPanelView.Show(_DockPanel, DockState.Document);

        }
        #endregion

        #region 菜单工具栏
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 数据连接器设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var setting = new SettingForm();
            setting.ShowDialog();
        }

        private void 操作面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_ControlPanelView !=null)
                _ControlPanelView.Show();
        }

        private void 日志面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_LogView !=null)
                _LogView.Show();
        }

        private void 模拟串口连接器面板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_MockDataConnectorView !=null)
                _MockDataConnectorView.Show();
        }
        #endregion




    }
}
