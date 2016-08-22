using System;
using System.ComponentModel;
using System.Drawing;
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
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SocketKnife.StressTest.View
{
    public class MockClientView : DockContent
    {
        private static readonly ILog _logger = LogManager.GetLogger<MockClientView>();
        private readonly BytesCodec _Codec = DI.Get<BytesCodec>();
        private readonly TestKernel _Kernel = DI.Get<TestKernel>();

        private bool _TempStopReceiveInfoList = true;

        /// <summary>
        ///     校验测试参数
        /// </summary>
        /// <param name="testOption"></param>
        private bool VerifyTestOption(MainTestOption testOption)
        {
            int clientCount;
            if (!ClientCountTextBox.Text.IsInteger(out clientCount, 1, 10))
            {
                return false;
            }
            testOption.ClientCount = clientCount;
            return true;
        }

        private void CreateClientButtonClick(object sender, EventArgs e)
        {
            var testOption = new MainTestOption();
            if (VerifyTestOption(testOption))
            {
                _Kernel.BuildCient(testOption);
            }
            else
            {
                _logger.Warn("创建仿真客户端参数不正确");
            }
        }

        private void MockClientAmountChanged(object sender, EventArgs eventArgs)
        {
            ConnectedMockClientListBox.ThreadSafeInvoke(() =>
            {
                //更新仿真客户端列表状态
                ConnectedMockClientListBox.Items.Clear();
                var clients = _Kernel.ClientHandlers;
                var i = 0;
                foreach (var clientHandler in clients)
                {
                    i += 1;
                    ConnectedMockClientListBox.Items.Add(string.Format("仿真{0} [{1}]", i, NangleCodecUtility.ConvertFromIntToFourBytes(clientHandler.ClientAddressValue).ToHexString()));
                }
            });
        }

        /// <summary>
        ///     仿真客户端收到协议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="nangleProtocolEventArgs"></param>
        private void OnMockClientProtocolReceived(object sender, NangleProtocolEventArgs nangleProtocolEventArgs)
        {
            var handler = sender as MockClientHandler;
            var index = _Kernel.ClientHandlers.IndexOf(handler) + 1;
            var protocol = nangleProtocolEventArgs.Protocol;
            if (AutoReplyCheckBox.Checked)
            {
                ProcessProtocol(handler, protocol);
            }

            if (!_TempStopReceiveInfoList)
            {
                MockClientProtocolReceiveHistoryTextBox.ThreadSafeInvoke(() =>
                {
                    MockClientProtocolReceiveHistoryTextBox.Text = string.Format("{0} 仿真客户端[{1}]收到协议[{2}]: \r\n{3}",
                        DateTime.Now.ToString("HH:mm:ss fff"), index,
                        NangleProtocolUtility.GetProtocolDescription(protocol),
                        MockClientProtocolReceiveHistoryTextBox.Text);
                    AppUtility.LimitTextBoxTextLengh(MockClientProtocolReceiveHistoryTextBox);
                });
            }
        }


        /// <summary>
        ///     自动应答处理
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="protocol"></param>
        private void ProcessProtocol(MockClientHandler handler, BytesProtocol protocol)
        {
            var addrBytes = NangleCodecUtility.ConvertFromIntToFourBytes(handler.ClientAddressValue);
            var commandIntValue = NangleCodecUtility.ConvertFromTwoBytesToInt(protocol.Command);
            BytesProtocol reply;
            if (commandIntValue == InitializeConnection.CommandIntValue) //初始化
            {
                reply = new InitializeConnectionReply(0x01, addrBytes);
                handler.WriteToAllSession(reply);
            }
            else if (commandIntValue == ExecuteTestCase.CommandIntValue) //执行测试案例
            {
                reply = new ExecuteTestCaseReply(0x01);
                handler.WriteToAllSession(reply);
            }
            else if (commandIntValue == StopExecuteTestCase.CommandIntValue) //停止执行测试案例
            {
                reply = new StopExecuteTestCaseReply(0x01);
                handler.WriteToAllSession(reply);
                _logger.Debug("发出了停止测试用例的回复");
            }
            else if (commandIntValue == ReadTestCaseResult.CommandIntValue) //读取测试结果
            {
                reply = new ReadTestCaseResultReply(
                    NangleProtocolUtility.GetTestCaseIndex(1), //用例编号
                    NangleCodecUtility.ConvertFromIntToFourBytes(handler.FrameSent),
                    NangleCodecUtility.ConvertFromIntToFourBytes(handler.FrameReceived),
                    NangleCodecUtility.ConvertFromIntToFourBytes(handler.FrameLost));
                handler.WriteToAllSession(reply);
            }
        }


        private void SendProtocolButtonClick(object sender, EventArgs e)
        {
            if (ConnectedMockClientListBox.SelectedIndex < 0)
            {
                MessageBox.Show("请在左侧选择要发送协议的客户端id");
                return;
            }
            if (ClientProtocolListBox.SelectedIndex < 0)
            {
                MessageBox.Show("请选择要发送的协议");
                return;
            }
            var index = ConnectedMockClientListBox.SelectedIndex;
            var handlers = _Kernel.ClientHandlers;
            if (index < handlers.Count)
            {
                var handler = handlers[index];
                try
                {
                    var data = UtilityConvert.HexToBytes(DataToSendByClientTextBox.Text);
                    if (InvokeFunctionOneTimeRadioButton.Checked) //只发一次
                    {
                        handler.WriteToAllSession(data);
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
                            for (var i = 0; i < count; i++)
                            {
                                handler.WriteToAllSession(data);
                                Thread.Sleep(interval);
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(string.Format("发送协议异常：{0}", ex.Message));
                }
            }
        }

        private void ClientProtocolListBoxClick(object sender, EventArgs e)
        {
            if (ConnectedMockClientListBox.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择要发送协议的客户端");
                return;
            }

            var clientHandlerss = _Kernel.ClientHandlers;
            var clientHandler = clientHandlerss[ConnectedMockClientListBox.SelectedIndex];

            if (ClientProtocolListBox.SelectedIndex >= 0)
            {
                var protocol = ClientProtocolListBox.SelectedItem as BytesProtocol;
                //_ProtocolHandler.WriteToSession(sessionId, protocol);

                //初始化回复，需要根据session返回对应的地址
                if (NangleCodecUtility.ConvertFromTwoBytesToInt(protocol.Command) == InitializeConnectionReply.CommandIntValue)
                {
                    var addrBytes = NangleCodecUtility.ConvertFromIntToFourBytes(clientHandler.ClientAddressValue);
                    Array.Copy(addrBytes, 0, protocol.CommandParam, 1, 4);
                }

                var family = _Kernel.GetProtocolFamily();
                var original = family.Generate(protocol);
                var data = _Codec.BytesEncoder.Execute(original);
                DataToSendByClientTextBox.Text = data.ToHexString();
            }
        }

        /// <summary>
        ///     点选仿真客户端列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectedMockClientListBoxClick(object sender, EventArgs e)
        {
            if (ConnectedMockClientListBox.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择要发送协议的客户端");
                return;
            }

            var clientHandlerss = _Kernel.ClientHandlers;
            var clientHandler = clientHandlerss[ConnectedMockClientListBox.SelectedIndex];


            if (ClientProtocolListBox.SelectedIndex >= 0)
            {
                var protocol = ClientProtocolListBox.SelectedItem as BytesProtocol;
                //_ProtocolHandler.WriteToSession(sessionId, protocol);

                //初始化回复，需要根据session返回对应的地址
                if (NangleCodecUtility.ConvertFromTwoBytesToInt(protocol.Command) == InitializeConnectionReply.CommandIntValue)
                {
                    var addrBytes = NangleCodecUtility.ConvertFromIntToFourBytes(clientHandler.ClientAddressValue);
                    Array.Copy(addrBytes, 0, protocol.CommandParam, 1, 4);

                    var family = _Kernel.GetProtocolFamily();
                    var original = family.Generate(protocol);
                    var data = _Codec.BytesEncoder.Execute(original);
                    DataToSendByClientTextBox.Text = data.ToHexString();
                }
            }
        }

        private void MockClientConnectButton_Click(object sender, EventArgs e)
        {
            var index = ConnectedMockClientListBox.SelectedIndex;
            if (index >= 0)
            {
                _Kernel.StartClientConnection(index);
            }
        }

        /// <summary>
        ///     断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MockClientDisconnectButton_Click(object sender, EventArgs e)
        {
            var index = ConnectedMockClientListBox.SelectedIndex;
            if (index >= 0)
            {
                _Kernel.StopClientConnection(index);
            }
        }

        private void MockClientRemoveButton_Click(object sender, EventArgs e)
        {
            var index = ConnectedMockClientListBox.SelectedIndex;
            if (index >= 0)
            {
                _Kernel.RemoveClient(index);
            }
        }

        private void TempStopReceiveInfoListCheckBoxClick(object sender, EventArgs e)
        {
            _TempStopReceiveInfoList = TempStopReceiveInfoListCheckBox.Checked;
        }

        #region UI

        private Panel panel1;
        private Button CreateClientButton;
        private Panel panel2;
        private GroupBox groupBox4;
        private Button SendProtocolButton;
        private ListBox ClientProtocolListBox;
        private GroupBox groupBox3;
        private TextBox MockClientProtocolReceiveHistoryTextBox;
        private GroupBox groupBox1;
        private Label label7;
        private Button MockClientDisconnectButton;
        private Button MockClientConnectButton;
        private ListBox ConnectedMockClientListBox;
        private Label label4;
        private TextBox ClientCountTextBox;
        private Label label1;
        private Button MockClientRemoveButton;
        private CheckBox AutoConnectAfterCreationCheckBox;
        private Panel panel4;
        private TextBox DataToSendByClientTextBox;
        private Panel panel3;
        private TextBox InvokeFunctionCountTextBox;
        private RadioButton InvokeFunctionOneTimeRadioButton;
        private Label label6;
        private Label label3;
        private TextBox InvokeFunctionIntervalTextBox;
        private RadioButton InvokeFunctionSeveralTimeRadioButton;
        private CheckBox AutoReplyCheckBox;
        private CheckBox TempStopReceiveInfoListCheckBox;
        private TextBox textBox2;
        private Label label5;
        private TextBox textBox1;
        private Label label2;
        private SplitContainer ClientViewSplitContainer;

        public MockClientView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            panel1 = new Panel();
            TempStopReceiveInfoListCheckBox = new CheckBox();
            AutoReplyCheckBox = new CheckBox();
            AutoConnectAfterCreationCheckBox = new CheckBox();
            label4 = new Label();
            ClientCountTextBox = new TextBox();
            label1 = new Label();
            CreateClientButton = new Button();
            panel2 = new Panel();
            ClientViewSplitContainer = new SplitContainer();
            ConnectedMockClientListBox = new ListBox();
            groupBox4 = new GroupBox();
            panel4 = new Panel();
            InvokeFunctionCountTextBox = new TextBox();
            InvokeFunctionOneTimeRadioButton = new RadioButton();
            label6 = new Label();
            label3 = new Label();
            InvokeFunctionIntervalTextBox = new TextBox();
            InvokeFunctionSeveralTimeRadioButton = new RadioButton();
            DataToSendByClientTextBox = new TextBox();
            SendProtocolButton = new Button();
            panel3 = new Panel();
            ClientProtocolListBox = new ListBox();
            groupBox3 = new GroupBox();
            MockClientProtocolReceiveHistoryTextBox = new TextBox();
            groupBox1 = new GroupBox();
            MockClientRemoveButton = new Button();
            label7 = new Label();
            MockClientDisconnectButton = new Button();
            MockClientConnectButton = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label5 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize) (ClientViewSplitContainer)).BeginInit();
            ClientViewSplitContainer.Panel1.SuspendLayout();
            ClientViewSplitContainer.Panel2.SuspendLayout();
            ClientViewSplitContainer.SuspendLayout();
            groupBox4.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(TempStopReceiveInfoListCheckBox);
            panel1.Controls.Add(AutoReplyCheckBox);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(ClientCountTextBox);
            panel1.Controls.Add(AutoConnectAfterCreationCheckBox);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(CreateClientButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(715, 66);
            panel1.TabIndex = 0;
            // 
            // TempStopReceiveInfoListCheckBox
            // 
            TempStopReceiveInfoListCheckBox.AutoSize = true;
            TempStopReceiveInfoListCheckBox.Checked = true;
            TempStopReceiveInfoListCheckBox.CheckState = CheckState.Checked;
            TempStopReceiveInfoListCheckBox.Location = new Point(274, 40);
            TempStopReceiveInfoListCheckBox.Name = "TempStopReceiveInfoListCheckBox";
            TempStopReceiveInfoListCheckBox.Size = new Size(264, 16);
            TempStopReceiveInfoListCheckBox.TabIndex = 23;
            TempStopReceiveInfoListCheckBox.Text = "临时停止协议接收提示（刷屏时可以先停止）";
            TempStopReceiveInfoListCheckBox.UseVisualStyleBackColor = true;
            TempStopReceiveInfoListCheckBox.Click += TempStopReceiveInfoListCheckBoxClick;
            // 
            // AutoReplyCheckBox
            // 
            AutoReplyCheckBox.AutoSize = true;
            AutoReplyCheckBox.Checked = true;
            AutoReplyCheckBox.CheckState = CheckState.Checked;
            AutoReplyCheckBox.Location = new Point(196, 40);
            AutoReplyCheckBox.Name = "AutoReplyCheckBox";
            AutoReplyCheckBox.Size = new Size(72, 16);
            AutoReplyCheckBox.TabIndex = 22;
            AutoReplyCheckBox.Text = "自动应答";
            AutoReplyCheckBox.UseVisualStyleBackColor = true;
            // 
            // AutoConnectAfterCreationCheckBox
            // 
            AutoConnectAfterCreationCheckBox.AutoSize = true;
            AutoConnectAfterCreationCheckBox.Checked = true;
            AutoConnectAfterCreationCheckBox.CheckState = CheckState.Checked;
            AutoConnectAfterCreationCheckBox.Location = new Point(116, 40);
            AutoConnectAfterCreationCheckBox.Name = "AutoConnectAfterCreationCheckBox";
            AutoConnectAfterCreationCheckBox.Size = new Size(72, 16);
            AutoConnectAfterCreationCheckBox.TabIndex = 21;
            AutoConnectAfterCreationCheckBox.Text = "自动连接";
            AutoConnectAfterCreationCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(482, 11);
            label4.Name = "label4";
            label4.Size = new Size(53, 12);
            label4.TabIndex = 20;
            label4.Text = "（1-10）";
            // 
            // ClientCountTextBox
            // 
            ClientCountTextBox.Location = new Point(438, 7);
            ClientCountTextBox.Name = "ClientCountTextBox";
            ClientCountTextBox.Size = new Size(39, 21);
            ClientCountTextBox.TabIndex = 19;
            ClientCountTextBox.Text = "4";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(355, 11);
            label1.Name = "label1";
            label1.Size = new Size(77, 12);
            label1.TabIndex = 18;
            label1.Text = "Client数量：";
            // 
            // CreateClientButton
            // 
            CreateClientButton.Location = new Point(24, 32);
            CreateClientButton.Name = "CreateClientButton";
            CreateClientButton.Size = new Size(75, 28);
            CreateClientButton.TabIndex = 0;
            CreateClientButton.Text = "创建";
            CreateClientButton.UseVisualStyleBackColor = true;
            CreateClientButton.Click += CreateClientButtonClick;
            // 
            // panel2
            // 
            panel2.Controls.Add(ClientViewSplitContainer);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 66);
            panel2.Name = "panel2";
            panel2.Size = new Size(715, 519);
            panel2.TabIndex = 1;
            // 
            // ClientViewSplitContainer
            // 
            ClientViewSplitContainer.Dock = DockStyle.Fill;
            ClientViewSplitContainer.Location = new Point(0, 0);
            ClientViewSplitContainer.Name = "ClientViewSplitContainer";
            // 
            // ClientViewSplitContainer.Panel1
            // 
            ClientViewSplitContainer.Panel1.Controls.Add(ConnectedMockClientListBox);
            // 
            // ClientViewSplitContainer.Panel2
            // 
            ClientViewSplitContainer.Panel2.Controls.Add(groupBox4);
            ClientViewSplitContainer.Panel2.Controls.Add(groupBox3);
            ClientViewSplitContainer.Panel2.Controls.Add(groupBox1);
            ClientViewSplitContainer.Size = new Size(715, 519);
            ClientViewSplitContainer.SplitterDistance = 177;
            ClientViewSplitContainer.TabIndex = 0;
            // 
            // ConnectedMockClientListBox
            // 
            ConnectedMockClientListBox.Dock = DockStyle.Fill;
            ConnectedMockClientListBox.FormattingEnabled = true;
            ConnectedMockClientListBox.ItemHeight = 12;
            ConnectedMockClientListBox.Location = new Point(0, 0);
            ConnectedMockClientListBox.Name = "ConnectedMockClientListBox";
            ConnectedMockClientListBox.Size = new Size(177, 519);
            ConnectedMockClientListBox.TabIndex = 0;
            ConnectedMockClientListBox.Click += ConnectedMockClientListBoxClick;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(panel4);
            groupBox4.Controls.Add(panel3);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 205);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(534, 314);
            groupBox4.TabIndex = 28;
            groupBox4.TabStop = false;
            groupBox4.Text = "协议发送窗口";
            // 
            // panel4
            // 
            panel4.Controls.Add(InvokeFunctionCountTextBox);
            panel4.Controls.Add(InvokeFunctionOneTimeRadioButton);
            panel4.Controls.Add(label6);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(InvokeFunctionIntervalTextBox);
            panel4.Controls.Add(InvokeFunctionSeveralTimeRadioButton);
            panel4.Controls.Add(DataToSendByClientTextBox);
            panel4.Controls.Add(SendProtocolButton);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(153, 17);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(10, 0, 0, 0);
            panel4.Size = new Size(378, 294);
            panel4.TabIndex = 23;
            // 
            // InvokeFunctionCountTextBox
            // 
            InvokeFunctionCountTextBox.Location = new Point(186, 97);
            InvokeFunctionCountTextBox.Name = "InvokeFunctionCountTextBox";
            InvokeFunctionCountTextBox.Size = new Size(33, 21);
            InvokeFunctionCountTextBox.TabIndex = 49;
            InvokeFunctionCountTextBox.Text = "3";
            // 
            // InvokeFunctionOneTimeRadioButton
            // 
            InvokeFunctionOneTimeRadioButton.AutoSize = true;
            InvokeFunctionOneTimeRadioButton.Checked = true;
            InvokeFunctionOneTimeRadioButton.Location = new Point(137, 75);
            InvokeFunctionOneTimeRadioButton.Name = "InvokeFunctionOneTimeRadioButton";
            InvokeFunctionOneTimeRadioButton.Size = new Size(119, 16);
            InvokeFunctionOneTimeRadioButton.TabIndex = 45;
            InvokeFunctionOneTimeRadioButton.TabStop = true;
            InvokeFunctionOneTimeRadioButton.Text = "一次执行（默认）";
            InvokeFunctionOneTimeRadioButton.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(327, 101);
            label6.Name = "label6";
            label6.Size = new Size(29, 12);
            label6.TabIndex = 48;
            label6.Text = "毫秒";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(220, 101);
            label3.Name = "label3";
            label3.Size = new Size(53, 12);
            label3.TabIndex = 50;
            label3.Text = "次，间隔";
            // 
            // InvokeFunctionIntervalTextBox
            // 
            InvokeFunctionIntervalTextBox.Location = new Point(276, 97);
            InvokeFunctionIntervalTextBox.Name = "InvokeFunctionIntervalTextBox";
            InvokeFunctionIntervalTextBox.Size = new Size(47, 21);
            InvokeFunctionIntervalTextBox.TabIndex = 47;
            InvokeFunctionIntervalTextBox.Text = "1000";
            // 
            // InvokeFunctionSeveralTimeRadioButton
            // 
            InvokeFunctionSeveralTimeRadioButton.AutoSize = true;
            InvokeFunctionSeveralTimeRadioButton.Location = new Point(137, 98);
            InvokeFunctionSeveralTimeRadioButton.Name = "InvokeFunctionSeveralTimeRadioButton";
            InvokeFunctionSeveralTimeRadioButton.Size = new Size(47, 16);
            InvokeFunctionSeveralTimeRadioButton.TabIndex = 46;
            InvokeFunctionSeveralTimeRadioButton.Text = "执行";
            InvokeFunctionSeveralTimeRadioButton.UseVisualStyleBackColor = true;
            // 
            // DataToSendByClientTextBox
            // 
            DataToSendByClientTextBox.Dock = DockStyle.Top;
            DataToSendByClientTextBox.Location = new Point(10, 0);
            DataToSendByClientTextBox.Multiline = true;
            DataToSendByClientTextBox.Name = "DataToSendByClientTextBox";
            DataToSendByClientTextBox.Size = new Size(368, 66);
            DataToSendByClientTextBox.TabIndex = 22;
            // 
            // SendProtocolButton
            // 
            SendProtocolButton.Location = new Point(13, 78);
            SendProtocolButton.Name = "SendProtocolButton";
            SendProtocolButton.Size = new Size(111, 32);
            SendProtocolButton.TabIndex = 1;
            SendProtocolButton.Text = "发送协议";
            SendProtocolButton.UseVisualStyleBackColor = true;
            SendProtocolButton.Click += SendProtocolButtonClick;
            // 
            // panel3
            // 
            panel3.Controls.Add(ClientProtocolListBox);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(3, 17);
            panel3.Name = "panel3";
            panel3.Size = new Size(150, 294);
            panel3.TabIndex = 22;
            // 
            // ClientProtocolListBox
            // 
            ClientProtocolListBox.Dock = DockStyle.Fill;
            ClientProtocolListBox.FormattingEnabled = true;
            ClientProtocolListBox.ItemHeight = 12;
            ClientProtocolListBox.Location = new Point(0, 0);
            ClientProtocolListBox.Name = "ClientProtocolListBox";
            ClientProtocolListBox.Size = new Size(150, 294);
            ClientProtocolListBox.TabIndex = 0;
            ClientProtocolListBox.Click += ClientProtocolListBoxClick;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(MockClientProtocolReceiveHistoryTextBox);
            groupBox3.Dock = DockStyle.Top;
            groupBox3.Location = new Point(0, 80);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(534, 125);
            groupBox3.TabIndex = 27;
            groupBox3.TabStop = false;
            groupBox3.Text = "协议接收及提示窗口";
            // 
            // MockClientProtocolReceiveHistoryTextBox
            // 
            MockClientProtocolReceiveHistoryTextBox.Dock = DockStyle.Fill;
            MockClientProtocolReceiveHistoryTextBox.Location = new Point(3, 17);
            MockClientProtocolReceiveHistoryTextBox.Multiline = true;
            MockClientProtocolReceiveHistoryTextBox.Name = "MockClientProtocolReceiveHistoryTextBox";
            MockClientProtocolReceiveHistoryTextBox.ScrollBars = ScrollBars.Vertical;
            MockClientProtocolReceiveHistoryTextBox.Size = new Size(528, 105);
            MockClientProtocolReceiveHistoryTextBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(MockClientRemoveButton);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(MockClientDisconnectButton);
            groupBox1.Controls.Add(MockClientConnectButton);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(534, 80);
            groupBox1.TabIndex = 26;
            groupBox1.TabStop = false;
            groupBox1.Text = "仿真Client状态";
            // 
            // MockClientRemoveButton
            // 
            MockClientRemoveButton.Location = new Point(303, 35);
            MockClientRemoveButton.Name = "MockClientRemoveButton";
            MockClientRemoveButton.Size = new Size(111, 32);
            MockClientRemoveButton.TabIndex = 28;
            MockClientRemoveButton.Text = "删除";
            MockClientRemoveButton.UseVisualStyleBackColor = true;
            MockClientRemoveButton.Click += MockClientRemoveButton_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(38, 17);
            label7.Name = "label7";
            label7.Size = new Size(41, 12);
            label7.TabIndex = 27;
            label7.Text = "已连接";
            // 
            // MockClientDisconnectButton
            // 
            MockClientDisconnectButton.Location = new Point(167, 35);
            MockClientDisconnectButton.Name = "MockClientDisconnectButton";
            MockClientDisconnectButton.Size = new Size(111, 32);
            MockClientDisconnectButton.TabIndex = 25;
            MockClientDisconnectButton.Text = "断开";
            MockClientDisconnectButton.UseVisualStyleBackColor = true;
            MockClientDisconnectButton.Click += MockClientDisconnectButton_Click;
            // 
            // MockClientConnectButton
            // 
            MockClientConnectButton.Location = new Point(30, 35);
            MockClientConnectButton.Name = "MockClientConnectButton";
            MockClientConnectButton.Size = new Size(111, 32);
            MockClientConnectButton.TabIndex = 24;
            MockClientConnectButton.Text = "连接";
            MockClientConnectButton.UseVisualStyleBackColor = true;
            MockClientConnectButton.Click += MockClientConnectButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(77, 7);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 21);
            textBox1.TabIndex = 25;
            textBox1.Text = "127.0.0.1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 10);
            label2.Name = "label2";
            label2.Size = new Size(53, 12);
            label2.TabIndex = 24;
            label2.Text = "主机Ip：";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(234, 7);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 21);
            textBox2.TabIndex = 27;
            textBox2.Text = "1000";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(194, 11);
            label5.Name = "label5";
            label5.Size = new Size(41, 12);
            label5.TabIndex = 26;
            label5.Text = "端口：";
            // 
            // MockClientView
            // 
            ClientSize = new Size(715, 585);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            Name = "MockClientView";
            Text = "仿真客户端";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ClientViewSplitContainer.Panel1.ResumeLayout(false);
            ClientViewSplitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize) (ClientViewSplitContainer)).EndInit();
            ClientViewSplitContainer.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        protected override void OnShown(EventArgs e)
        {
            _Kernel.MockClientAmountChanged += MockClientAmountChanged;
            _Kernel.MockClientProtocolReceived += OnMockClientProtocolReceived;
            ClientProtocolListBox.Items.Add(new InitializeConnectionReply(0x00, new byte[] {0x00, 0x00, 0x00, 0x01}));
            ClientProtocolListBox.Items.Add(new ExecuteTestCaseReply(0x00));
            ClientProtocolListBox.Items.Add(new StopExecuteTestCaseReply(0x00));
            ClientProtocolListBox.Items.Add(new ReadTestCaseResultReply(
                NangleProtocolUtility.GetTestCaseIndex(1), //用例编号
                new byte[] {0x00, 0x00, 0x00, 0x00},
                new byte[] {0x00, 0x00, 0x00, 0x00},
                new byte[] {0x00, 0x00, 0x00, 0x00}));
            ClientProtocolListBox.Items.Add(new TestRawData(NangleProtocolUtility.EmptyBytes4, 0x00,
                new byte[] {0x00, 0x01}));
            ClientProtocolListBox.Items.Add(new SetSpeechModeReply(0x01));
            ClientProtocolListBox.Items.Add(new SpeechRawData(NangleProtocolUtility.EmptyBytes4, 0x00,
                new byte[] {0x00, 0x01}));

            base.OnShown(e);
        }

        #endregion
    }
}