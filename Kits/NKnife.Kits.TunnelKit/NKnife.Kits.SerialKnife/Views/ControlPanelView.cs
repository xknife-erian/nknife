using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Common.Logging;
using NKnife.Extensions;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Kernel;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Wrapper;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SerialKnife.Views
{
    public class ControlPanelView : DockContent
    {
        private System.Windows.Forms.Button _StartTunnelButton;
        private static readonly ILog _logger = LogManager.GetLogger<ControlPanelView>();
        private bool _IsTunnelStarted;

        private readonly Tunnels _Tunnels = DI.Get<Tunnels>();

        #region 初始化
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ManualPackageTabPage;
        private System.Windows.Forms.PropertyGrid BytesProtocolPropertyGrid;
        private System.Windows.Forms.Button SendProtocolToRemoteButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox ReceivedProtocolListBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ClearReceiveListButton;
        private System.Windows.Forms.TabPage Pan485PackageTabPage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox Pan485ProtocolListBox;
        public ControlPanelView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this._StartTunnelButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ManualPackageTabPage = new System.Windows.Forms.TabPage();
            this.Pan485PackageTabPage = new System.Windows.Forms.TabPage();
            this.Pan485ProtocolListBox = new System.Windows.Forms.ListBox();
            this.BytesProtocolPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SendProtocolToRemoteButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ReceivedProtocolListBox = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ClearReceiveListButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1.SuspendLayout();
            this.Pan485PackageTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _StartTunnelButton
            // 
            this._StartTunnelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._StartTunnelButton.Location = new System.Drawing.Point(9, 500);
            this._StartTunnelButton.Name = "_StartTunnelButton";
            this._StartTunnelButton.Size = new System.Drawing.Size(82, 31);
            this._StartTunnelButton.TabIndex = 55;
            this._StartTunnelButton.Text = "启动";
            this._StartTunnelButton.UseVisualStyleBackColor = true;
            this._StartTunnelButton.Click += new System.EventHandler(this._StartTunnelButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ManualPackageTabPage);
            this.tabControl1.Controls.Add(this.Pan485PackageTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(643, 228);
            this.tabControl1.TabIndex = 56;
            // 
            // ManualPackageTabPage
            // 
            this.ManualPackageTabPage.Location = new System.Drawing.Point(4, 22);
            this.ManualPackageTabPage.Name = "ManualPackageTabPage";
            this.ManualPackageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ManualPackageTabPage.Size = new System.Drawing.Size(635, 202);
            this.ManualPackageTabPage.TabIndex = 0;
            this.ManualPackageTabPage.Text = "自由组包";
            this.ManualPackageTabPage.UseVisualStyleBackColor = true;
            // 
            // Pan485PackageTabPage
            // 
            this.Pan485PackageTabPage.Controls.Add(this.Pan485ProtocolListBox);
            this.Pan485PackageTabPage.Location = new System.Drawing.Point(4, 22);
            this.Pan485PackageTabPage.Name = "Pan485PackageTabPage";
            this.Pan485PackageTabPage.Size = new System.Drawing.Size(459, 192);
            this.Pan485PackageTabPage.TabIndex = 1;
            this.Pan485PackageTabPage.Text = "Pan485协议族";
            this.Pan485PackageTabPage.UseVisualStyleBackColor = true;
            // 
            // Pan485ProtocolListBox
            // 
            this.Pan485ProtocolListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pan485ProtocolListBox.FormattingEnabled = true;
            this.Pan485ProtocolListBox.ItemHeight = 12;
            this.Pan485ProtocolListBox.Location = new System.Drawing.Point(0, 0);
            this.Pan485ProtocolListBox.Name = "Pan485ProtocolListBox";
            this.Pan485ProtocolListBox.Size = new System.Drawing.Size(459, 192);
            this.Pan485ProtocolListBox.TabIndex = 0;
            // 
            // BytesProtocolPropertyGrid
            // 
            this.BytesProtocolPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BytesProtocolPropertyGrid.Location = new System.Drawing.Point(3, 17);
            this.BytesProtocolPropertyGrid.Name = "BytesProtocolPropertyGrid";
            this.BytesProtocolPropertyGrid.Size = new System.Drawing.Size(327, 469);
            this.BytesProtocolPropertyGrid.TabIndex = 57;
            // 
            // SendProtocolToRemoteButton
            // 
            this.SendProtocolToRemoteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SendProtocolToRemoteButton.Location = new System.Drawing.Point(185, 500);
            this.SendProtocolToRemoteButton.Name = "SendProtocolToRemoteButton";
            this.SendProtocolToRemoteButton.Size = new System.Drawing.Size(82, 31);
            this.SendProtocolToRemoteButton.TabIndex = 58;
            this.SendProtocolToRemoteButton.Text = "发送";
            this.SendProtocolToRemoteButton.UseVisualStyleBackColor = true;
            this.SendProtocolToRemoteButton.Click += new System.EventHandler(this.SendProtocolToRemoteButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 248);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "协议发送";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ReceivedProtocolListBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(649, 237);
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
            this.ReceivedProtocolListBox.Size = new System.Drawing.Size(643, 217);
            this.ReceivedProtocolListBox.TabIndex = 0;
            this.ReceivedProtocolListBox.Click += new System.EventHandler(this.ReceivedProtocolListBox_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BytesProtocolPropertyGrid);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(333, 489);
            this.groupBox3.TabIndex = 61;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "协议内容";
            // 
            // ClearReceiveListButton
            // 
            this.ClearReceiveListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearReceiveListButton.Location = new System.Drawing.Point(97, 500);
            this.ClearReceiveListButton.Name = "ClearReceiveListButton";
            this.ClearReceiveListButton.Size = new System.Drawing.Size(82, 31);
            this.ClearReceiveListButton.TabIndex = 62;
            this.ClearReceiveListButton.Text = "清空";
            this.ClearReceiveListButton.UseVisualStyleBackColor = true;
            this.ClearReceiveListButton.Click += new System.EventHandler(this.ClearReceiveListButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(5, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(986, 489);
            this.splitContainer1.SplitterDistance = 649;
            this.splitContainer1.TabIndex = 63;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(649, 489);
            this.splitContainer2.SplitterDistance = 237;
            this.splitContainer2.TabIndex = 0;
            // 
            // ControlPanelView
            // 
            this.ClientSize = new System.Drawing.Size(998, 543);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ClearReceiveListButton);
            this.Controls.Add(this.SendProtocolToRemoteButton);
            this.Controls.Add(this._StartTunnelButton);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ControlPanelView";
            this.Text = "操作面板";
            this.Load += new System.EventHandler(this.ControlPanelView_Load);
            this.tabControl1.ResumeLayout(false);
            this.Pan485PackageTabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void ControlPanelView_Load(object sender, EventArgs e)
        {
            _Tunnels.ProtocolsReceived += _Tunnels_ProtocolsReceived;
            FillUpPan485ProtocolList();
        }


        private void FillUpPan485ProtocolList()
        {
            var family = DI.Get<BytesProtocolFamily>();
            family.FamilyName = "p-an485";
            var protocol = family.Build(new byte[] {0x3D}); //登录成功
            Pan485ProtocolListBox.Items.Add(new ProtocolWrapper(protocol)
            {
                Description = "登录成功",
            });
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

            [DisplayName("描述")]
            public string Description { get; set; }

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
                Description = string.Empty;
            }



            public override string ToString()
            {
                if(string.IsNullOrEmpty(Description))
                    return string.Format("{0}:Command={1}, Data={2}",DateTime.Now.ToString("HH:mm:ss fff"),_Protocol.Command.ToHexString(),_Protocol.CommandParam.ToHexString());
                else
                    return string.Format("{0}:{1}, Command={2}, Data={3}", DateTime.Now.ToString("HH:mm:ss fff"), Description, _Protocol.Command.ToHexString(), _Protocol.CommandParam.ToHexString());
            }
        }

        private void SendProtocolToRemoteButton_Click(object sender, EventArgs e)
        {

        }



    }
}
