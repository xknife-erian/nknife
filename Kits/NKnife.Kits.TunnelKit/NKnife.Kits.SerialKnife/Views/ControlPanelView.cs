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
        private System.Windows.Forms.Button _startTunnelButton;
        private static readonly ILog _Logger = LogManager.GetLogger<ControlPanelView>();
        private bool _isTunnelStarted;

        private readonly Tunnels _tunnels = Di.Get<Tunnels>();

        #region 初始化
        private System.Windows.Forms.TabControl _tabControl1;
        private System.Windows.Forms.TabPage _manualPackageTabPage;
        private System.Windows.Forms.PropertyGrid _bytesProtocolPropertyGrid;
        private System.Windows.Forms.Button _sendProtocolToRemoteButton;
        private System.Windows.Forms.GroupBox _groupBox1;
        private System.Windows.Forms.GroupBox _groupBox2;
        private System.Windows.Forms.ListBox _receivedProtocolListBox;
        private System.Windows.Forms.GroupBox _groupBox3;
        private System.Windows.Forms.Button _clearReceiveListButton;
        private System.Windows.Forms.TabPage _pan485PackageTabPage;
        private System.Windows.Forms.SplitContainer _splitContainer1;
        private System.Windows.Forms.SplitContainer _splitContainer2;
        private System.Windows.Forms.ListBox _pan485ProtocolListBox;
        public ControlPanelView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this._startTunnelButton = new System.Windows.Forms.Button();
            this._tabControl1 = new System.Windows.Forms.TabControl();
            this._manualPackageTabPage = new System.Windows.Forms.TabPage();
            this._pan485PackageTabPage = new System.Windows.Forms.TabPage();
            this._pan485ProtocolListBox = new System.Windows.Forms.ListBox();
            this._bytesProtocolPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this._sendProtocolToRemoteButton = new System.Windows.Forms.Button();
            this._groupBox1 = new System.Windows.Forms.GroupBox();
            this._groupBox2 = new System.Windows.Forms.GroupBox();
            this._receivedProtocolListBox = new System.Windows.Forms.ListBox();
            this._groupBox3 = new System.Windows.Forms.GroupBox();
            this._clearReceiveListButton = new System.Windows.Forms.Button();
            this._splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._tabControl1.SuspendLayout();
            this._pan485PackageTabPage.SuspendLayout();
            this._groupBox1.SuspendLayout();
            this._groupBox2.SuspendLayout();
            this._groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer1)).BeginInit();
            this._splitContainer1.Panel1.SuspendLayout();
            this._splitContainer1.Panel2.SuspendLayout();
            this._splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).BeginInit();
            this._splitContainer2.Panel1.SuspendLayout();
            this._splitContainer2.Panel2.SuspendLayout();
            this._splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _StartTunnelButton
            // 
            this._startTunnelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._startTunnelButton.Location = new System.Drawing.Point(9, 500);
            this._startTunnelButton.Name = "_startTunnelButton";
            this._startTunnelButton.Size = new System.Drawing.Size(82, 31);
            this._startTunnelButton.TabIndex = 55;
            this._startTunnelButton.Text = "启动";
            this._startTunnelButton.UseVisualStyleBackColor = true;
            this._startTunnelButton.Click += new System.EventHandler(this._StartTunnelButton_Click);
            // 
            // tabControl1
            // 
            this._tabControl1.Controls.Add(this._manualPackageTabPage);
            this._tabControl1.Controls.Add(this._pan485PackageTabPage);
            this._tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl1.Location = new System.Drawing.Point(3, 17);
            this._tabControl1.Name = "_tabControl1";
            this._tabControl1.SelectedIndex = 0;
            this._tabControl1.Size = new System.Drawing.Size(643, 228);
            this._tabControl1.TabIndex = 56;
            // 
            // ManualPackageTabPage
            // 
            this._manualPackageTabPage.Location = new System.Drawing.Point(4, 22);
            this._manualPackageTabPage.Name = "_manualPackageTabPage";
            this._manualPackageTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._manualPackageTabPage.Size = new System.Drawing.Size(635, 202);
            this._manualPackageTabPage.TabIndex = 0;
            this._manualPackageTabPage.Text = "自由组包";
            this._manualPackageTabPage.UseVisualStyleBackColor = true;
            // 
            // Pan485PackageTabPage
            // 
            this._pan485PackageTabPage.Controls.Add(this._pan485ProtocolListBox);
            this._pan485PackageTabPage.Location = new System.Drawing.Point(4, 22);
            this._pan485PackageTabPage.Name = "_pan485PackageTabPage";
            this._pan485PackageTabPage.Size = new System.Drawing.Size(459, 192);
            this._pan485PackageTabPage.TabIndex = 1;
            this._pan485PackageTabPage.Text = "Pan485协议族";
            this._pan485PackageTabPage.UseVisualStyleBackColor = true;
            // 
            // Pan485ProtocolListBox
            // 
            this._pan485ProtocolListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pan485ProtocolListBox.FormattingEnabled = true;
            this._pan485ProtocolListBox.ItemHeight = 12;
            this._pan485ProtocolListBox.Location = new System.Drawing.Point(0, 0);
            this._pan485ProtocolListBox.Name = "_pan485ProtocolListBox";
            this._pan485ProtocolListBox.Size = new System.Drawing.Size(459, 192);
            this._pan485ProtocolListBox.TabIndex = 0;
            // 
            // BytesProtocolPropertyGrid
            // 
            this._bytesProtocolPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bytesProtocolPropertyGrid.Location = new System.Drawing.Point(3, 17);
            this._bytesProtocolPropertyGrid.Name = "_bytesProtocolPropertyGrid";
            this._bytesProtocolPropertyGrid.Size = new System.Drawing.Size(327, 469);
            this._bytesProtocolPropertyGrid.TabIndex = 57;
            // 
            // SendProtocolToRemoteButton
            // 
            this._sendProtocolToRemoteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._sendProtocolToRemoteButton.Location = new System.Drawing.Point(185, 500);
            this._sendProtocolToRemoteButton.Name = "_sendProtocolToRemoteButton";
            this._sendProtocolToRemoteButton.Size = new System.Drawing.Size(82, 31);
            this._sendProtocolToRemoteButton.TabIndex = 58;
            this._sendProtocolToRemoteButton.Text = "发送";
            this._sendProtocolToRemoteButton.UseVisualStyleBackColor = true;
            this._sendProtocolToRemoteButton.Click += new System.EventHandler(this.SendProtocolToRemoteButton_Click);
            // 
            // groupBox1
            // 
            this._groupBox1.Controls.Add(this._tabControl1);
            this._groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBox1.Location = new System.Drawing.Point(0, 0);
            this._groupBox1.Name = "_groupBox1";
            this._groupBox1.Size = new System.Drawing.Size(649, 248);
            this._groupBox1.TabIndex = 59;
            this._groupBox1.TabStop = false;
            this._groupBox1.Text = "协议发送";
            // 
            // groupBox2
            // 
            this._groupBox2.Controls.Add(this._receivedProtocolListBox);
            this._groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBox2.Location = new System.Drawing.Point(0, 0);
            this._groupBox2.Name = "_groupBox2";
            this._groupBox2.Size = new System.Drawing.Size(649, 237);
            this._groupBox2.TabIndex = 60;
            this._groupBox2.TabStop = false;
            this._groupBox2.Text = "协议接收";
            // 
            // ReceivedProtocolListBox
            // 
            this._receivedProtocolListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receivedProtocolListBox.FormattingEnabled = true;
            this._receivedProtocolListBox.ItemHeight = 12;
            this._receivedProtocolListBox.Location = new System.Drawing.Point(3, 17);
            this._receivedProtocolListBox.Name = "_receivedProtocolListBox";
            this._receivedProtocolListBox.Size = new System.Drawing.Size(643, 217);
            this._receivedProtocolListBox.TabIndex = 0;
            this._receivedProtocolListBox.Click += new System.EventHandler(this.ReceivedProtocolListBox_Click);
            // 
            // groupBox3
            // 
            this._groupBox3.Controls.Add(this._bytesProtocolPropertyGrid);
            this._groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBox3.Location = new System.Drawing.Point(0, 0);
            this._groupBox3.Name = "_groupBox3";
            this._groupBox3.Size = new System.Drawing.Size(333, 489);
            this._groupBox3.TabIndex = 61;
            this._groupBox3.TabStop = false;
            this._groupBox3.Text = "协议内容";
            // 
            // ClearReceiveListButton
            // 
            this._clearReceiveListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._clearReceiveListButton.Location = new System.Drawing.Point(97, 500);
            this._clearReceiveListButton.Name = "_clearReceiveListButton";
            this._clearReceiveListButton.Size = new System.Drawing.Size(82, 31);
            this._clearReceiveListButton.TabIndex = 62;
            this._clearReceiveListButton.Text = "清空";
            this._clearReceiveListButton.UseVisualStyleBackColor = true;
            this._clearReceiveListButton.Click += new System.EventHandler(this.ClearReceiveListButton_Click);
            // 
            // splitContainer1
            // 
            this._splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._splitContainer1.Location = new System.Drawing.Point(5, 6);
            this._splitContainer1.Name = "_splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this._splitContainer1.Panel1.Controls.Add(this._splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this._splitContainer1.Panel2.Controls.Add(this._groupBox3);
            this._splitContainer1.Size = new System.Drawing.Size(986, 489);
            this._splitContainer1.SplitterDistance = 649;
            this._splitContainer1.TabIndex = 63;
            // 
            // splitContainer2
            // 
            this._splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer2.Location = new System.Drawing.Point(0, 0);
            this._splitContainer2.Name = "_splitContainer2";
            this._splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this._splitContainer2.Panel1.Controls.Add(this._groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this._splitContainer2.Panel2.Controls.Add(this._groupBox1);
            this._splitContainer2.Size = new System.Drawing.Size(649, 489);
            this._splitContainer2.SplitterDistance = 237;
            this._splitContainer2.TabIndex = 0;
            // 
            // ControlPanelView
            // 
            this.ClientSize = new System.Drawing.Size(998, 543);
            this.Controls.Add(this._splitContainer1);
            this.Controls.Add(this._clearReceiveListButton);
            this.Controls.Add(this._sendProtocolToRemoteButton);
            this.Controls.Add(this._startTunnelButton);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ControlPanelView";
            this.Text = "操作面板";
            this.Load += new System.EventHandler(this.ControlPanelView_Load);
            this._tabControl1.ResumeLayout(false);
            this._pan485PackageTabPage.ResumeLayout(false);
            this._groupBox1.ResumeLayout(false);
            this._groupBox2.ResumeLayout(false);
            this._groupBox3.ResumeLayout(false);
            this._splitContainer1.Panel1.ResumeLayout(false);
            this._splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer1)).EndInit();
            this._splitContainer1.ResumeLayout(false);
            this._splitContainer2.Panel1.ResumeLayout(false);
            this._splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer2)).EndInit();
            this._splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void ControlPanelView_Load(object sender, EventArgs e)
        {
            _tunnels.ProtocolsReceived += _Tunnels_ProtocolsReceived;
            FillUpPan485ProtocolList();
        }


        private void FillUpPan485ProtocolList()
        {
            var family = Di.Get<BytesProtocolFamily>();
            family.FamilyName = "p-an485";
            var protocol = family.Build(new byte[] {0x3D}); //登录成功
            _pan485ProtocolListBox.Items.Add(new ProtocolWrapper(protocol)
            {
                Description = "登录成功",
            });
        }
        #endregion

        private void _StartTunnelButton_Click(object sender, EventArgs e)
        {
            if (_isTunnelStarted)
            {
                _tunnels.Stop();
                _startTunnelButton.Text = "启动";
            }
            else
            {
                _tunnels.Start();
                _startTunnelButton.Text = "停止";
            }
            _isTunnelStarted = !_isTunnelStarted;
        }

        private void ReceivedProtocolListBox_Click(object sender, EventArgs e)
        {
            if (_receivedProtocolListBox.SelectedIndex < 0)
            {
                return;
            }
            var protocol = (ProtocolWrapper)_receivedProtocolListBox.SelectedItem;
            _bytesProtocolPropertyGrid.SelectedObject = protocol;
        }

        private void ClearReceiveListButton_Click(object sender, EventArgs e)
        {
            _receivedProtocolListBox.Items.Clear();
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
                _receivedProtocolListBox.Items.Insert(0,new ProtocolWrapper(protocol));
            }
        }

        public class ProtocolWrapper
        {
            private IProtocol<byte[]> _protocol;

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
                _protocol = protocol;
                Family = protocol.Family;
                CommandInString = protocol.Command.ToHexString();
                CommandParamInString = protocol.CommandParam.ToHexString();
                CommandParam = protocol.CommandParam;
                Description = string.Empty;
            }



            public override string ToString()
            {
                if(string.IsNullOrEmpty(Description))
                    return string.Format("{0}:Command={1}, Data={2}",DateTime.Now.ToString("HH:mm:ss fff"),_protocol.Command.ToHexString(),_protocol.CommandParam.ToHexString());
                else
                    return string.Format("{0}:{1}, Command={2}, Data={3}", DateTime.Now.ToString("HH:mm:ss fff"), Description, _protocol.Command.ToHexString(), _protocol.CommandParam.ToHexString());
            }
        }

        private void SendProtocolToRemoteButton_Click(object sender, EventArgs e)
        {

        }



    }
}
