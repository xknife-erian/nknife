using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Converts;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Protocol;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Generic;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Server;
using NKnife.Kits.SocketKnife.StressTest.TestCase;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Generic;
using NKnife.Utility;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SocketKnife.StressTest.View
{
    public class ServerView:DockContent
    {
        private static readonly ILog _logger = LogManager.GetLogger<ServerView>();
        private TestKernel _Kernel = DI.Get<TestKernel>();
        private BytesCodec _Codec = DI.Get<BytesCodec>();
        private MainTestServerHandler _ProtocolHandler;
        private TestServerMonitorFilter _TestMonitorFilter;
        private bool _OnTesting;

        #region UI

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label TalkCountLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label SessionCountLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ClientSendIntervalTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button StopServerListenButton;
        private System.Windows.Forms.Button StartServerListenButton;
        private System.Windows.Forms.Label ServerListenPortLabel;
        private System.Windows.Forms.Label ServerListenStatusLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox ServerProtocolReceiveHistoryTextBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button SendProtocolButton;
        private System.Windows.Forms.ListBox ConnectedClientListBox;
        private TextBox DataToSendByServerTextBox;
        private Panel panel1;
        private SplitContainer splitContainer1;
        private GroupBox groupBox5;
        private Panel panel3;
        private Panel panel2;
        private System.Windows.Forms.ListBox ServerProtocolListBox;
    
        public ServerView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TalkCountLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SessionCountLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClientSendIntervalTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ServerListenStatusLabel = new System.Windows.Forms.Label();
            this.ServerListenPortLabel = new System.Windows.Forms.Label();
            this.StopServerListenButton = new System.Windows.Forms.Button();
            this.StartServerListenButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ServerProtocolReceiveHistoryTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DataToSendByServerTextBox = new System.Windows.Forms.TextBox();
            this.SendProtocolButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ServerProtocolListBox = new System.Windows.Forms.ListBox();
            this.ConnectedClientListBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TalkCountLabel);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.SessionCountLabel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(296, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(245, 82);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // TalkCountLabel
            // 
            this.TalkCountLabel.BackColor = System.Drawing.Color.White;
            this.TalkCountLabel.Location = new System.Drawing.Point(107, 45);
            this.TalkCountLabel.Name = "TalkCountLabel";
            this.TalkCountLabel.Size = new System.Drawing.Size(100, 18);
            this.TalkCountLabel.TabIndex = 12;
            this.TalkCountLabel.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "Talk数量：";
            // 
            // SessionCountLabel
            // 
            this.SessionCountLabel.BackColor = System.Drawing.Color.White;
            this.SessionCountLabel.Location = new System.Drawing.Point(107, 18);
            this.SessionCountLabel.Name = "SessionCountLabel";
            this.SessionCountLabel.Size = new System.Drawing.Size(100, 18);
            this.SessionCountLabel.TabIndex = 10;
            this.SessionCountLabel.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Session数量：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(136, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "（毫秒，不小于50）";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "发送间隔：";
            // 
            // ClientSendIntervalTextBox
            // 
            this.ClientSendIntervalTextBox.Location = new System.Drawing.Point(93, 160);
            this.ClientSendIntervalTextBox.Name = "ClientSendIntervalTextBox";
            this.ClientSendIntervalTextBox.Size = new System.Drawing.Size(47, 21);
            this.ClientSendIntervalTextBox.TabIndex = 15;
            this.ClientSendIntervalTextBox.Text = "1000";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ServerListenStatusLabel);
            this.groupBox1.Controls.Add(this.ServerListenPortLabel);
            this.groupBox1.Controls.Add(this.StopServerListenButton);
            this.groupBox1.Controls.Add(this.StartServerListenButton);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(984, 110);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server状态";
            // 
            // ServerListenStatusLabel
            // 
            this.ServerListenStatusLabel.AutoSize = true;
            this.ServerListenStatusLabel.Location = new System.Drawing.Point(37, 27);
            this.ServerListenStatusLabel.Name = "ServerListenStatusLabel";
            this.ServerListenStatusLabel.Size = new System.Drawing.Size(41, 12);
            this.ServerListenStatusLabel.TabIndex = 27;
            this.ServerListenStatusLabel.Text = "已停止";
            // 
            // ServerListenPortLabel
            // 
            this.ServerListenPortLabel.AutoSize = true;
            this.ServerListenPortLabel.Location = new System.Drawing.Point(107, 27);
            this.ServerListenPortLabel.Name = "ServerListenPortLabel";
            this.ServerListenPortLabel.Size = new System.Drawing.Size(95, 12);
            this.ServerListenPortLabel.TabIndex = 26;
            this.ServerListenPortLabel.Text = "监听端口：22011";
            // 
            // StopServerListenButton
            // 
            this.StopServerListenButton.Location = new System.Drawing.Point(167, 50);
            this.StopServerListenButton.Name = "StopServerListenButton";
            this.StopServerListenButton.Size = new System.Drawing.Size(111, 32);
            this.StopServerListenButton.TabIndex = 25;
            this.StopServerListenButton.Text = "停止监听";
            this.StopServerListenButton.UseVisualStyleBackColor = true;
            this.StopServerListenButton.Click += new System.EventHandler(this.StopServerListenButtonClick);
            // 
            // StartServerListenButton
            // 
            this.StartServerListenButton.Location = new System.Drawing.Point(30, 50);
            this.StartServerListenButton.Name = "StartServerListenButton";
            this.StartServerListenButton.Size = new System.Drawing.Size(111, 32);
            this.StartServerListenButton.TabIndex = 24;
            this.StartServerListenButton.Text = "启动监听";
            this.StartServerListenButton.UseVisualStyleBackColor = true;
            this.StartServerListenButton.Click += new System.EventHandler(this.StartServerListenButtonClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ServerProtocolReceiveHistoryTextBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(767, 110);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "协议接收及提示窗口";
            // 
            // ServerProtocolReceiveHistoryTextBox
            // 
            this.ServerProtocolReceiveHistoryTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerProtocolReceiveHistoryTextBox.Location = new System.Drawing.Point(3, 17);
            this.ServerProtocolReceiveHistoryTextBox.Multiline = true;
            this.ServerProtocolReceiveHistoryTextBox.Name = "ServerProtocolReceiveHistoryTextBox";
            this.ServerProtocolReceiveHistoryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ServerProtocolReceiveHistoryTextBox.Size = new System.Drawing.Size(761, 90);
            this.ServerProtocolReceiveHistoryTextBox.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel3);
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 110);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(767, 412);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "协议发送窗口";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.DataToSendByServerTextBox);
            this.panel3.Controls.Add(this.SendProtocolButton);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.ClientSendIntervalTextBox);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(162, 17);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panel3.Size = new System.Drawing.Size(602, 392);
            this.panel3.TabIndex = 22;
            // 
            // DataToSendByServerTextBox
            // 
            this.DataToSendByServerTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.DataToSendByServerTextBox.Location = new System.Drawing.Point(10, 0);
            this.DataToSendByServerTextBox.Multiline = true;
            this.DataToSendByServerTextBox.Name = "DataToSendByServerTextBox";
            this.DataToSendByServerTextBox.Size = new System.Drawing.Size(592, 84);
            this.DataToSendByServerTextBox.TabIndex = 20;
            // 
            // SendProtocolButton
            // 
            this.SendProtocolButton.Location = new System.Drawing.Point(13, 109);
            this.SendProtocolButton.Name = "SendProtocolButton";
            this.SendProtocolButton.Size = new System.Drawing.Size(111, 32);
            this.SendProtocolButton.TabIndex = 1;
            this.SendProtocolButton.Text = "发送协议";
            this.SendProtocolButton.UseVisualStyleBackColor = true;
            this.SendProtocolButton.Click += new System.EventHandler(this.SendProtocolButtonClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ServerProtocolListBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(159, 392);
            this.panel2.TabIndex = 21;
            // 
            // ServerProtocolListBox
            // 
            this.ServerProtocolListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerProtocolListBox.FormattingEnabled = true;
            this.ServerProtocolListBox.ItemHeight = 12;
            this.ServerProtocolListBox.Location = new System.Drawing.Point(0, 0);
            this.ServerProtocolListBox.Name = "ServerProtocolListBox";
            this.ServerProtocolListBox.Size = new System.Drawing.Size(159, 392);
            this.ServerProtocolListBox.TabIndex = 0;
            this.ServerProtocolListBox.Click += new System.EventHandler(this.ServerProtocolListBoxClick);
            // 
            // ConnectedClientListBox
            // 
            this.ConnectedClientListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectedClientListBox.FormattingEnabled = true;
            this.ConnectedClientListBox.ItemHeight = 12;
            this.ConnectedClientListBox.Location = new System.Drawing.Point(3, 17);
            this.ConnectedClientListBox.Name = "ConnectedClientListBox";
            this.ConnectedClientListBox.Size = new System.Drawing.Size(207, 502);
            this.ConnectedClientListBox.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 522);
            this.panel1.TabIndex = 24;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(984, 522);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ConnectedClientListBox);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(213, 522);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "已连接客户端列表";
            // 
            // ServerView
            // 
            this.ClientSize = new System.Drawing.Size(984, 632);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ServerView";
            this.Text = "服务端";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        protected override void OnShown(EventArgs e)
        {
            ServerListenPortLabel.Text = string.Format("监听端口：{0}", Properties.Settings.Default.ServerPort);
            ServerProtocolListBox.Items.Add(new InitializeTest(new byte[] { 0x00, 0x00, 0x00, 0x00 }, new byte[] { 0x00, 0x00, 0x00, 0x00 }));
            ServerProtocolListBox.Items.Add(
                new ExecuteTestCase(
                    new byte[] { 0x00, 0x00, 0x00, 0x00 },
                    new byte[] {0x00, 0x00},
                    0x01,
                    new byte[] {0x00, 0x00, 0x00, 0x00},
                    new byte[] {0x00, 0x00}, 
                    new byte[] {0x00, 0x00},
                    new byte[]{},
                    new byte[] { 0x00, 0x00, 0x00, 0x00 }));
            ServerProtocolListBox.Items.Add(
                new StopExecuteTestCase(new byte[]{0x00,0x00,0x00,0x00}, new byte[]{0x00,0x01}));
            ServerProtocolListBox.Items.Add(
               new ReadTestCaseResult(new byte[] { 0x00, 0x00, 0x00, 0x00 }, new byte[] { 0x00, 0x01 }));
            ServerProtocolListBox.Items.Add(
              new TestRawData(new byte[] { 0x00, 0x00, 0x00, 0x00 }, 0x01, new byte[] { 0x00, 0x01 }));

            _TestMonitorFilter = new TestServerMonitorFilter();
            _TestMonitorFilter.StateChanged += StateChanged;
            _ProtocolHandler = new MainTestServerHandler();
            _ProtocolHandler.ProtocolReceived += ProtocolReceived;
            _Kernel.BuildServer(_TestMonitorFilter, _ProtocolHandler);
            base.OnShown(e);
        }

        private void ProtocolReceived(object sender, NangleProtocolEventArgs nangleProtocolEventArgs)
        {
            ServerProtocolReceiveHistoryTextBox.ThreadSafeInvoke(() =>
            {
                ServerProtocolReceiveHistoryTextBox.Text = string.Format("{0} 服务端收到来自[Session {1}]协议[{2}]: \r\n{3}",
                    DateTime.Now.ToString("HH:mm:ss fff"),nangleProtocolEventArgs.SessionId, NangleProtocolUtility.GetProtocolDescription(nangleProtocolEventArgs.Protocol), ServerProtocolReceiveHistoryTextBox.Text);
                AppUtility.LimitTextBoxTextLengh(ServerProtocolReceiveHistoryTextBox);
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            if (_OnTesting)
            {
                _Kernel.StopServer();
            }
            base.OnClosed(e);
        }


        private void StateChanged(object sender, ServerStateEventArgs serverStateEventArgs)
        {
            SessionCountLabel.ThreadSafeInvoke(() =>
            {
                SessionCountLabel.Text = string.Format("{0}", serverStateEventArgs.SessionCount);
                TalkCountLabel.Text = string.Format("{0}", serverStateEventArgs.TalkCount);
                ConnectedClientListBox.Items.Clear();
                foreach (var l in _TestMonitorFilter.SessionList)
                {
                    ConnectedClientListBox.Items.Add(l);
                }
            });

        }



        /// <summary>
        /// 启动Server监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartServerListenButtonClick(object sender, EventArgs e)
        {
            if (_Kernel.StartServer())
            {
                _OnTesting = true;
                ServerListenStatusLabel.Text = "已启动";
                StartServerListenButton.Enabled = false;
                StopServerListenButton.Enabled = true;
            }
        }

        //停止监听
        private void StopServerListenButtonClick(object sender, EventArgs e)
        {
            if (_Kernel.StopServer())
            {
                _OnTesting = false;
                ServerListenStatusLabel.Text = "已停止";
                StartServerListenButton.Enabled = true;
                StopServerListenButton.Enabled = false;
            }
        }

        /// <summary>
        /// 向指定客户端发送协议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendProtocolButtonClick(object sender, EventArgs e)
        {
            if (ConnectedClientListBox.SelectedIndex < 0)
            {
                MessageBox.Show("请在左侧选择要发送协议的客户端id");
                return;
            }
            if (ServerProtocolListBox.SelectedIndex < 0)
            {
                MessageBox.Show("请选择要发送的协议");
                return;
            }
            var sessionWrapper = ConnectedClientListBox.SelectedItem as SessionWrapper;
            if (sessionWrapper != null)
            {
                long sessionId = sessionWrapper.Id;
                //_ProtocolHandler.WriteToSession(sessionId, protocol);
                try
                {
                    byte[] data = UtilityConvert.HexToBytes(DataToSendByServerTextBox.Text);
                    _TestMonitorFilter.WriteToSession(sessionId, data);
                }
                catch (Exception ex)
                {
                    _logger.Error(string.Format("发送协议异常：{0}",ex.Message));
                }
            }
        }

        private void ServerProtocolListBoxClick(object sender, EventArgs e)
        {
            if (ServerProtocolListBox.SelectedIndex >= 0)
            {
                var protocol = ServerProtocolListBox.SelectedItem as BytesProtocol;
                //_ProtocolHandler.WriteToSession(sessionId, protocol);

                var family = _Kernel.GetProtocolFamily();
                byte[] original = family.Generate(protocol);
                byte[] data = _Codec.BytesEncoder.Execute(original);
                DataToSendByServerTextBox.Text = data.ToHexString();
            }
        }
    }
}
