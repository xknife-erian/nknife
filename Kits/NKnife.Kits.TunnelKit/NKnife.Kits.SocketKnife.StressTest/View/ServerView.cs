using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Logging;
using NKnife.Converts;
using NKnife.IoC;
using NKnife.Kits.SocketKnife.StressTest.Base;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Kits.SocketKnife.StressTest.Kernel;
using NKnife.Kits.SocketKnife.StressTest.Protocol;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Client;
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


        private bool _OnTesting;
        private object _CurrentTestcaseParam;
        private int _TestCaseStartTime;

        private bool _TempStopReceiveInfoList = true;

        #region UI

        private System.Windows.Forms.Label SessionCountLabel;
        private System.Windows.Forms.Label label6;
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
        private TextBox InvokeFunctionCountTextBox;
        private RadioButton InvokeFunctionOneTimeRadioButton;
        private Label label1;
        private Label label3;
        private TextBox InvokeFunctionIntervalTextBox;
        private RadioButton InvokeFunctionSeveralTimeRadioButton;
        private Button ExecuteTestCaseButton;
        private ComboBox TestCaseListComboBox;
        private PropertyGrid TestCasePropertyGrid;
        private CheckBox TempStopReceiveInfoListCheckBox;
        private System.Windows.Forms.ListBox ServerProtocolListBox;
    
        public ServerView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SessionCountLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TempStopReceiveInfoListCheckBox = new System.Windows.Forms.CheckBox();
            this.ServerListenStatusLabel = new System.Windows.Forms.Label();
            this.ServerListenPortLabel = new System.Windows.Forms.Label();
            this.StopServerListenButton = new System.Windows.Forms.Button();
            this.StartServerListenButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ServerProtocolReceiveHistoryTextBox = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TestCasePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.TestCaseListComboBox = new System.Windows.Forms.ComboBox();
            this.ExecuteTestCaseButton = new System.Windows.Forms.Button();
            this.InvokeFunctionSeveralTimeRadioButton = new System.Windows.Forms.RadioButton();
            this.SendProtocolButton = new System.Windows.Forms.Button();
            this.InvokeFunctionIntervalTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DataToSendByServerTextBox = new System.Windows.Forms.TextBox();
            this.InvokeFunctionCountTextBox = new System.Windows.Forms.TextBox();
            this.InvokeFunctionOneTimeRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ServerProtocolListBox = new System.Windows.Forms.ListBox();
            this.ConnectedClientListBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
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
            // SessionCountLabel
            // 
            this.SessionCountLabel.BackColor = System.Drawing.Color.White;
            this.SessionCountLabel.Location = new System.Drawing.Point(387, 23);
            this.SessionCountLabel.Name = "SessionCountLabel";
            this.SessionCountLabel.Size = new System.Drawing.Size(100, 18);
            this.SessionCountLabel.TabIndex = 10;
            this.SessionCountLabel.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(298, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "Session数量：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TempStopReceiveInfoListCheckBox);
            this.groupBox1.Controls.Add(this.ServerListenStatusLabel);
            this.groupBox1.Controls.Add(this.SessionCountLabel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ServerListenPortLabel);
            this.groupBox1.Controls.Add(this.StopServerListenButton);
            this.groupBox1.Controls.Add(this.StartServerListenButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(984, 91);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server状态";
            // 
            // TempStopReceiveInfoListCheckBox
            // 
            this.TempStopReceiveInfoListCheckBox.AutoSize = true;
            this.TempStopReceiveInfoListCheckBox.Checked = true;
            this.TempStopReceiveInfoListCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TempStopReceiveInfoListCheckBox.Location = new System.Drawing.Point(300, 51);
            this.TempStopReceiveInfoListCheckBox.Name = "TempStopReceiveInfoListCheckBox";
            this.TempStopReceiveInfoListCheckBox.Size = new System.Drawing.Size(264, 16);
            this.TempStopReceiveInfoListCheckBox.TabIndex = 28;
            this.TempStopReceiveInfoListCheckBox.Text = "临时停止协议接收提示（刷屏时可以先停止）";
            this.TempStopReceiveInfoListCheckBox.UseVisualStyleBackColor = true;
            this.TempStopReceiveInfoListCheckBox.Click += new System.EventHandler(this.TempStopReceiveInfoListCheckBoxClick);
            // 
            // ServerListenStatusLabel
            // 
            this.ServerListenStatusLabel.AutoSize = true;
            this.ServerListenStatusLabel.Location = new System.Drawing.Point(37, 23);
            this.ServerListenStatusLabel.Name = "ServerListenStatusLabel";
            this.ServerListenStatusLabel.Size = new System.Drawing.Size(41, 12);
            this.ServerListenStatusLabel.TabIndex = 27;
            this.ServerListenStatusLabel.Text = "已停止";
            // 
            // ServerListenPortLabel
            // 
            this.ServerListenPortLabel.AutoSize = true;
            this.ServerListenPortLabel.Location = new System.Drawing.Point(107, 23);
            this.ServerListenPortLabel.Name = "ServerListenPortLabel";
            this.ServerListenPortLabel.Size = new System.Drawing.Size(95, 12);
            this.ServerListenPortLabel.TabIndex = 26;
            this.ServerListenPortLabel.Text = "监听端口：22011";
            // 
            // StopServerListenButton
            // 
            this.StopServerListenButton.Location = new System.Drawing.Point(167, 42);
            this.StopServerListenButton.Name = "StopServerListenButton";
            this.StopServerListenButton.Size = new System.Drawing.Size(111, 32);
            this.StopServerListenButton.TabIndex = 25;
            this.StopServerListenButton.Text = "停止监听";
            this.StopServerListenButton.UseVisualStyleBackColor = true;
            this.StopServerListenButton.Click += new System.EventHandler(this.StopServerListenButtonClick);
            // 
            // StartServerListenButton
            // 
            this.StartServerListenButton.Location = new System.Drawing.Point(30, 42);
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
            this.groupBox3.Size = new System.Drawing.Size(767, 103);
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
            this.ServerProtocolReceiveHistoryTextBox.Size = new System.Drawing.Size(761, 83);
            this.ServerProtocolReceiveHistoryTextBox.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel3);
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 103);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(767, 438);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "协议发送窗口";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TestCasePropertyGrid);
            this.panel3.Controls.Add(this.TestCaseListComboBox);
            this.panel3.Controls.Add(this.ExecuteTestCaseButton);
            this.panel3.Controls.Add(this.InvokeFunctionSeveralTimeRadioButton);
            this.panel3.Controls.Add(this.SendProtocolButton);
            this.panel3.Controls.Add(this.InvokeFunctionIntervalTextBox);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.DataToSendByServerTextBox);
            this.panel3.Controls.Add(this.InvokeFunctionCountTextBox);
            this.panel3.Controls.Add(this.InvokeFunctionOneTimeRadioButton);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(162, 17);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panel3.Size = new System.Drawing.Size(602, 418);
            this.panel3.TabIndex = 22;
            // 
            // TestCasePropertyGrid
            // 
            this.TestCasePropertyGrid.Location = new System.Drawing.Point(10, 142);
            this.TestCasePropertyGrid.Name = "TestCasePropertyGrid";
            this.TestCasePropertyGrid.Size = new System.Drawing.Size(339, 180);
            this.TestCasePropertyGrid.TabIndex = 59;
            // 
            // TestCaseListComboBox
            // 
            this.TestCaseListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TestCaseListComboBox.FormattingEnabled = true;
            this.TestCaseListComboBox.Items.AddRange(new object[] {
            "单点测试",
            "1对1转发测试",
            "1对1对传测试",
            "语音对讲测试",
            "1对1分组对传测试",
            "数据广播测试",
            "语音广播测试",
            "语音Echo测试"});
            this.TestCaseListComboBox.Location = new System.Drawing.Point(146, 111);
            this.TestCaseListComboBox.Name = "TestCaseListComboBox";
            this.TestCaseListComboBox.Size = new System.Drawing.Size(203, 20);
            this.TestCaseListComboBox.TabIndex = 58;
            this.TestCaseListComboBox.SelectedIndexChanged += new System.EventHandler(this.TestCaseListComboBoxSelectedIndexChanged);
            // 
            // ExecuteTestCaseButton
            // 
            this.ExecuteTestCaseButton.Location = new System.Drawing.Point(10, 104);
            this.ExecuteTestCaseButton.Name = "ExecuteTestCaseButton";
            this.ExecuteTestCaseButton.Size = new System.Drawing.Size(114, 32);
            this.ExecuteTestCaseButton.TabIndex = 57;
            this.ExecuteTestCaseButton.Text = "执行测试案例";
            this.ExecuteTestCaseButton.UseVisualStyleBackColor = true;
            this.ExecuteTestCaseButton.Click += new System.EventHandler(this.ExecuteTestCaseButtonClick);
            // 
            // InvokeFunctionSeveralTimeRadioButton
            // 
            this.InvokeFunctionSeveralTimeRadioButton.AutoSize = true;
            this.InvokeFunctionSeveralTimeRadioButton.Location = new System.Drawing.Point(146, 73);
            this.InvokeFunctionSeveralTimeRadioButton.Name = "InvokeFunctionSeveralTimeRadioButton";
            this.InvokeFunctionSeveralTimeRadioButton.Size = new System.Drawing.Size(47, 16);
            this.InvokeFunctionSeveralTimeRadioButton.TabIndex = 52;
            this.InvokeFunctionSeveralTimeRadioButton.Text = "执行";
            this.InvokeFunctionSeveralTimeRadioButton.UseVisualStyleBackColor = true;
            // 
            // SendProtocolButton
            // 
            this.SendProtocolButton.Location = new System.Drawing.Point(13, 53);
            this.SendProtocolButton.Name = "SendProtocolButton";
            this.SendProtocolButton.Size = new System.Drawing.Size(111, 32);
            this.SendProtocolButton.TabIndex = 1;
            this.SendProtocolButton.Text = "发送协议";
            this.SendProtocolButton.UseVisualStyleBackColor = true;
            this.SendProtocolButton.Click += new System.EventHandler(this.SendProtocolButtonClick);
            // 
            // InvokeFunctionIntervalTextBox
            // 
            this.InvokeFunctionIntervalTextBox.Location = new System.Drawing.Point(289, 72);
            this.InvokeFunctionIntervalTextBox.Name = "InvokeFunctionIntervalTextBox";
            this.InvokeFunctionIntervalTextBox.Size = new System.Drawing.Size(47, 21);
            this.InvokeFunctionIntervalTextBox.TabIndex = 53;
            this.InvokeFunctionIntervalTextBox.Text = "1000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 56;
            this.label3.Text = "次，间隔";
            // 
            // DataToSendByServerTextBox
            // 
            this.DataToSendByServerTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.DataToSendByServerTextBox.Location = new System.Drawing.Point(10, 0);
            this.DataToSendByServerTextBox.Multiline = true;
            this.DataToSendByServerTextBox.Name = "DataToSendByServerTextBox";
            this.DataToSendByServerTextBox.Size = new System.Drawing.Size(592, 47);
            this.DataToSendByServerTextBox.TabIndex = 20;
            // 
            // InvokeFunctionCountTextBox
            // 
            this.InvokeFunctionCountTextBox.Location = new System.Drawing.Point(199, 72);
            this.InvokeFunctionCountTextBox.Name = "InvokeFunctionCountTextBox";
            this.InvokeFunctionCountTextBox.Size = new System.Drawing.Size(33, 21);
            this.InvokeFunctionCountTextBox.TabIndex = 55;
            this.InvokeFunctionCountTextBox.Text = "3";
            // 
            // InvokeFunctionOneTimeRadioButton
            // 
            this.InvokeFunctionOneTimeRadioButton.AutoSize = true;
            this.InvokeFunctionOneTimeRadioButton.Checked = true;
            this.InvokeFunctionOneTimeRadioButton.Location = new System.Drawing.Point(146, 53);
            this.InvokeFunctionOneTimeRadioButton.Name = "InvokeFunctionOneTimeRadioButton";
            this.InvokeFunctionOneTimeRadioButton.Size = new System.Drawing.Size(119, 16);
            this.InvokeFunctionOneTimeRadioButton.TabIndex = 51;
            this.InvokeFunctionOneTimeRadioButton.TabStop = true;
            this.InvokeFunctionOneTimeRadioButton.Text = "一次执行（默认）";
            this.InvokeFunctionOneTimeRadioButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(342, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 54;
            this.label1.Text = "毫秒";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ServerProtocolListBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(159, 418);
            this.panel2.TabIndex = 21;
            // 
            // ServerProtocolListBox
            // 
            this.ServerProtocolListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerProtocolListBox.FormattingEnabled = true;
            this.ServerProtocolListBox.ItemHeight = 12;
            this.ServerProtocolListBox.Location = new System.Drawing.Point(0, 0);
            this.ServerProtocolListBox.Name = "ServerProtocolListBox";
            this.ServerProtocolListBox.Size = new System.Drawing.Size(159, 418);
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
            this.ConnectedClientListBox.Size = new System.Drawing.Size(207, 521);
            this.ConnectedClientListBox.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 541);
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
            this.splitContainer1.Size = new System.Drawing.Size(984, 541);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.ConnectedClientListBox);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(213, 541);
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
            ServerProtocolListBox.Items.Add(new InitializeConnection(new byte[] { 0x00, 0x00, 0x00, 0x00 }));
            ServerProtocolListBox.Items.Add(
                new ExecuteTestCase(
                    NangleProtocolUtility.GetTestCaseIndex(1), //用例编号
                    (byte) NangleProtocolUtility.SendEnable.Enable, //发送使能
                    new byte[] {0x00, 0x00, 0x00, 0x00}, //发送目的地址
                    NangleProtocolUtility.GetSendInterval(100), //发送时间间隔
                    NangleProtocolUtility.GetTestDataLength(0), //发送测试数据长度
                    NangleProtocolUtility.GetFrameCount(0))); //发送帧数
            ServerProtocolListBox.Items.Add(
                new StopExecuteTestCase(new byte[]{0x00,0x01}));
            ServerProtocolListBox.Items.Add(
               new ReadTestCaseResult(new byte[] { 0x00, 0x01 }));
            ServerProtocolListBox.Items.Add(
              new TestRawData(new byte[] { 0x00, 0x00, 0x00, 0x00 }, 0x01, new byte[] { 0x00, 0x01 }));
            ServerProtocolListBox.Items.Add(
                new SetSpeechMode(
                    (byte)NangleProtocolUtility.SpeechMode.Talk,
                    new byte[] {0x00, 0x00, 0x00, 0x00}));
            ServerProtocolListBox.Items.Add(new SpeechRawData(new byte[] {0x00, 0x00, 0x00, 0x00}, 0x01,
                new byte[] {0x00, 0x01}));
            _Kernel.ServerProtocolFilter.StateChanged += StateChanged;

            _Kernel.ServerHandler.ProtocolReceived += ProtocolReceived;
            _Kernel.BuildServer();
            base.OnShown(e);
        }

        private void ProtocolReceived(object sender, NangleProtocolEventArgs nangleProtocolEventArgs)
        {
            if (!_TempStopReceiveInfoList)
            {
                ServerProtocolReceiveHistoryTextBox.ThreadSafeInvoke(() =>
                {

                        var sessionId = nangleProtocolEventArgs.SessionId;
                        var protocol = nangleProtocolEventArgs.Protocol;
                        ServerProtocolReceiveHistoryTextBox.Text = string.Format("{0} 服务端收到来自[Session {1}]协议[{2}]: \r\n{3}",
                            DateTime.Now.ToString("HH:mm:ss fff"), sessionId,
                            NangleProtocolUtility.GetProtocolDescription(protocol), ServerProtocolReceiveHistoryTextBox.Text);
                        AppUtility.LimitTextBoxTextLengh(ServerProtocolReceiveHistoryTextBox);

    //                if (NangleCodecUtility.ConvertFromTwoBytesToInt(protocol.Command) ==
    //                    InitializeConnectionReply.CommandIntValue) //初始化回复
    //                {
    //                    var address = new byte[] {0x00, 0x00, 0x00, 0x00};
    //                    if (InitializeConnectionReply.Parse(ref address, protocol))
    //                    {
    //                        UpdateClientAddress(sessionId, address);
    //
    //                    }
    //                }
                });
                }
        }

        private void UpdateClientAddress(long sessionId, byte[] address)
        {
            foreach (var item in ConnectedClientListBox.Items)
            {
                if (((SessionWrapper) item).Id == sessionId)
                {
                    ((SessionWrapper) item).Address = NangleCodecUtility.ConvertFromFourBytesToInt(address);
                }
            }
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
                ConnectedClientListBox.Items.Clear();
                foreach (var l in _Kernel.ServerProtocolFilter.SessionList)
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
                ConnectedClientListBox.Items.Clear();
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
                    if (InvokeFunctionOneTimeRadioButton.Checked) //只发一次
                    {
                        _Kernel.ServerHandler.WriteToSession(sessionId, data);
                    }
                    else
                    {
                        int count;
                        int interval;
                        if (!InvokeFunctionCountTextBox.Text.IsPositiveInteger(out count))
                        {
                            MessageBox.Show("执行次数必须为正整数");
                            return;
                        }
                        if (!InvokeFunctionIntervalTextBox.Text.IsPositiveInteger(out interval))
                        {
                            MessageBox.Show("执行间隔必须为正整数");
                            return;
                        }
                        Task.Factory.StartNew(() =>
                        {
                            for (int i = 0; i < count; i++)
                            {
                                _Kernel.ServerHandler.WriteToSession(sessionId, data);
                                Thread.Sleep(interval);
                            }
                        });
                    }
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

        /// <summary>
        /// 执行测试案例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteTestCaseButtonClick(object sender, EventArgs e)
        {
            if (TestCaseListComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("请选择要执行的测试案例");
                return;
            }
            ITestCase testcase = null;

            switch (TestCaseListComboBox.SelectedIndex)
            {
                case 0:
                    testcase= new SingleTalkTestCase();
                    break;
                case 1:
                    testcase = new PointToPointTestCase();
                    break;
                case 2:
                    testcase = new PointToPointDualTestCase();
                    break;
                case 3:
                    testcase = new SpeechTalkTestCase();
                    break;
                case 4:
                    testcase = new PointToPointGroupTestCase();
                    break;
                case 5:
                    testcase = new BroadcastTestCase();
                    break;
                case 6:
                    testcase = new SpeechBroadcastTestCase();
                    break;
                case 7:
                    testcase = new SpeechEchoTestCase();
                    break;
            }

            if (testcase != null)
            {
                testcase.Finished += TestcaseFinished;

                _TestCaseStartTime = System.Environment.TickCount;
                testcase.Start(_Kernel, _CurrentTestcaseParam);

                ExecuteTestCaseButton.Enabled = false;
            }
        }

        void TestcaseFinished(object sender, TestCaseResultEventArgs e)
        {
            var result = e.Result;
            var message = e.Message;

            var duration = (System.Environment.TickCount - _TestCaseStartTime) / 1000; //秒
            _logger.Info(string.Format("测试案例执行约{0}秒，结果{1}",duration, result ? "成功" : "失败"));
            ExecuteTestCaseButton.ThreadSafeInvoke(()=>
            {
                ExecuteTestCaseButton.Enabled = true;
                var dlg = new TestCaseResultDialog(message);
                dlg.ShowDialog();
                //MessageBox.Show(this, message, "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private void TestCaseListComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TestCaseListComboBox.SelectedIndex)
            {
                case 0:
                    _CurrentTestcaseParam = new ExecuteHardwareTestParam();
                    break;
                case 1:
                    _CurrentTestcaseParam = new ExecuteHardwareTestParam();
                    break;
                case 2:
                    _CurrentTestcaseParam = new ExecuteHardwareTestParam();
                    break;
                case 3:
                    _CurrentTestcaseParam = new SpeechTestParam();
                    break;
                case 4:
                    _CurrentTestcaseParam = new ExecuteHardwareTestParam();
                    break;
                case 5:
                    _CurrentTestcaseParam = new ExecuteHardwareTestParam();
                    break;
                case 6:
                    _CurrentTestcaseParam = new SpeechTestParam();
                    break;
                case 7:
                    _CurrentTestcaseParam = new SpeechTestParam();
                    break;
            }
            TestCasePropertyGrid.SelectedObject = _CurrentTestcaseParam;
            TestCasePropertyGrid.Refresh();

        }

        private void TempStopReceiveInfoListCheckBoxClick(object sender, EventArgs e)
        {
            _TempStopReceiveInfoList = TempStopReceiveInfoListCheckBox.Checked;
        }
    }
}
