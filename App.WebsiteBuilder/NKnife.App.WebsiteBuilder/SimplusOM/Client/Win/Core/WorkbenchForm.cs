using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jeelu.SimplusOM.Client.Win
{
    public partial class WorkbenchForm : Form
    {
        private IContainer components = null;
        private ToolStrip _toolStrip;
        private MenuStrip _menuStrip;
        private StatusStrip _statusStrip;
        private ToolStripButton _toolStripButton;

        //WorkBench工作台
        private static WorkbenchForm _mainForm = new WorkbenchForm();
        public static WorkbenchForm MainForm
        {
            get { return _mainForm; }
        }

        //左侧树面板
        private LeftFunctionPad _leftfunctionPad = LeftFunctionPad.LeftFunctionPadSingle();
        public LeftFunctionPad MainLeftfunctionPad
        {
            get { return _leftfunctionPad; }
        }

        /// 主框架(存放左侧面板及右侧的主要数据操作台)
        DockPanel _dockPanel = new DockPanel();
        public DockPanel MainDockPanel
        {
            get { return _dockPanel; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkbenchForm()
        {
            this._toolStrip = new ToolStrip();
            this._menuStrip = new MenuStrip();
            this._statusStrip = new StatusStrip();

            InitializeComponent();
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            this._toolStripButton = new ToolStripButton("测试");
            this._toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this._toolStripButton.Click += new EventHandler(_toolStripButton_Click);
            this._toolStrip.Items.Add(this._toolStripButton);

            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "WorkbenchForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.Text = "SimplusOM" + " " + DateTime.Now.ToString("yyyy-MM-dd");
            this.IsMdiContainer = true;
            this.MainMenuStrip = this._menuStrip;

            this.Controls.Add(this._dockPanel);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this._toolStrip);
            this.Controls.Add(this._menuStrip);

            this._leftfunctionPad.Show(this._dockPanel, DockState.DockLeft);

            _dockPanel.Dock = DockStyle.Fill;
            _dockPanel.DockTopPortion = 150;
            _dockPanel.DockBottomPortion = 180;
            _dockPanel.DockLeftPortion = 100;
            _dockPanel.DockRightPortion = 260;


            this.ResumeLayout(false);

        }

        void _toolStripButton_Click(object sender, EventArgs e)
        {
            //TestForm1 form = new TestForm1();
            //form.Show(this.MainDockPanel, DockState.Document);
        }
    }
}