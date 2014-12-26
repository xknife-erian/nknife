using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Kernel;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SerialKnife.Views
{
    public class ControlPanelView : DockContent
    {
        private System.Windows.Forms.Button _StartTunnelButton;
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private bool _IsTunnelStarted;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ManualPackageTabPage;
        private System.Windows.Forms.PropertyGrid BytesProtocolPropertyGrid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox ReceivedProtocolListBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ClearReceiveListButton;
        private readonly Tunnels _Tunnels = DI.Get<Tunnels>();

        #region 初始化
        public ControlPanelView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this._StartTunnelButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ManualPackageTabPage = new System.Windows.Forms.TabPage();
            this.BytesProtocolPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ReceivedProtocolListBox = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ClearReceiveListButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // _StartTunnelButton
            // 
            this._StartTunnelButton.Location = new System.Drawing.Point(275, 463);
            this._StartTunnelButton.Name = "_StartTunnelButton";
            this._StartTunnelButton.Size = new System.Drawing.Size(106, 35);
            this._StartTunnelButton.TabIndex = 55;
            this._StartTunnelButton.Text = "启动";
            this._StartTunnelButton.UseVisualStyleBackColor = true;
            this._StartTunnelButton.Click += new System.EventHandler(this._StartTunnelButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ManualPackageTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(467, 218);
            this.tabControl1.TabIndex = 56;
            // 
            // ManualPackageTabPage
            // 
            this.ManualPackageTabPage.Location = new System.Drawing.Point(4, 22);
            this.ManualPackageTabPage.Name = "ManualPackageTabPage";
            this.ManualPackageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ManualPackageTabPage.Size = new System.Drawing.Size(459, 192);
            this.ManualPackageTabPage.TabIndex = 0;
            this.ManualPackageTabPage.Text = "自由组包";
            this.ManualPackageTabPage.UseVisualStyleBackColor = true;
            // 
            // BytesProtocolPropertyGrid
            // 
            this.BytesProtocolPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BytesProtocolPropertyGrid.Location = new System.Drawing.Point(3, 17);
            this.BytesProtocolPropertyGrid.Name = "BytesProtocolPropertyGrid";
            this.BytesProtocolPropertyGrid.Size = new System.Drawing.Size(478, 400);
            this.BytesProtocolPropertyGrid.TabIndex = 57;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(144, 463);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 35);
            this.button1.TabIndex = 58;
            this.button1.Text = "发送";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(473, 238);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "协议发送";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ReceivedProtocolListBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 256);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(473, 179);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "协议接收";
            // 
            // ReceivedProtocolListBox
            // 
            this.ReceivedProtocolListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReceivedProtocolListBox.FormattingEnabled = true;
            this.ReceivedProtocolListBox.ItemHeight = 12;
            this.ReceivedProtocolListBox.Location = new System.Drawing.Point(3, 17);
            this.ReceivedProtocolListBox.Name = "ReceivedProtocolListBox";
            this.ReceivedProtocolListBox.Size = new System.Drawing.Size(467, 159);
            this.ReceivedProtocolListBox.TabIndex = 0;
            this.ReceivedProtocolListBox.Click += new System.EventHandler(this.ReceivedProtocolListBox_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BytesProtocolPropertyGrid);
            this.groupBox3.Location = new System.Drawing.Point(502, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(484, 420);
            this.groupBox3.TabIndex = 61;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "协议内容";
            // 
            // ClearReceiveListButton
            // 
            this.ClearReceiveListButton.Location = new System.Drawing.Point(15, 463);
            this.ClearReceiveListButton.Name = "ClearReceiveListButton";
            this.ClearReceiveListButton.Size = new System.Drawing.Size(106, 35);
            this.ClearReceiveListButton.TabIndex = 62;
            this.ClearReceiveListButton.Text = "清空";
            this.ClearReceiveListButton.UseVisualStyleBackColor = true;
            this.ClearReceiveListButton.Click += new System.EventHandler(this.ClearReceiveListButton_Click);
            // 
            // ControlPanelView
            // 
            this.ClientSize = new System.Drawing.Size(998, 543);
            this.Controls.Add(this.ClearReceiveListButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this._StartTunnelButton);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ControlPanelView";
            this.Text = "操作面板";
            this.Load += new System.EventHandler(this.ControlPanelView_Load);
            this.tabControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void ControlPanelView_Load(object sender, EventArgs e)
        {
            _Tunnels.ProtocolsReceived += _Tunnels_ProtocolsReceived;
        }
        #endregion

        private void _StartTunnelButton_Click(object sender, EventArgs e)
        {
            if (_IsTunnelStarted)
            {
                _Tunnels.Stop();
                _StartTunnelButton.Text = "启动";
            }
            else
            {
                _Tunnels.Start();
                _StartTunnelButton.Text = "停止";
            }
            _IsTunnelStarted = !_IsTunnelStarted;
        }

        private void ReceivedProtocolListBox_Click(object sender, EventArgs e)
        {
            if (ReceivedProtocolListBox.SelectedIndex < 0)
            {
                return;
            }
            var protocol = (ProtocolWrapper)ReceivedProtocolListBox.SelectedItem;
            BytesProtocolPropertyGrid.SelectedObject = protocol;
        }

        private void ClearReceiveListButton_Click(object sender, EventArgs e)
        {
            ReceivedProtocolListBox.Items.Clear();
        }

        /// <summary>
        /// 协议接收处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void _Tunnels_ProtocolsReceived(object sender, Events.EventArgs<IEnumerable<Protocol.IProtocol<byte[]>>> e)
        {
            var protocols = e.Item;
            foreach (var protocol in protocols)
            {
                ReceivedProtocolListBox.Items.Insert(0,new ProtocolWrapper(protocol));
            }
        }

        public class ProtocolWrapper
        {
            private IProtocol<byte[]> _Protocol;

            [DisplayName("协议族")]
            public string Family { get; set; }

            [DisplayName("命令字")]
            public string CommandInString { get; set; }

            [DisplayName("CommandParam字符串表示")]
            public string CommandParamInString { get; set; }

            [DisplayName("CommandParam")]
            public byte[] CommandParam { get; set; }

            private ProtocolWrapper()
            {
                
            }

            public ProtocolWrapper(IProtocol<byte[]> protocol)
            {
                _Protocol = protocol;
                Family = protocol.Family;
                CommandInString = protocol.Command.ToHexString();
                CommandParamInString = protocol.CommandParam.ToHexString();
                CommandParam = protocol.CommandParam;
            }



            public override string ToString()
            {
                return string.Format("{0}:Command={1}, Data={2}",DateTime.Now.ToString("HH:mm:ss fff"),_Protocol.Command.ToHexString(),_Protocol.CommandParam.ToHexString());
            }
        }




    }
}
