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
using NKnife.Kits.SocketKnife.StressTest.Kernel;
using NKnife.Kits.SocketKnife.StressTest.Protocol;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Client;
using NKnife.Kits.SocketKnife.StressTest.Protocol.Generic;
using NKnife.Kits.SocketKnife.StressTest.TestCase;
using NKnife.Protocol.Generic;
using NKnife.Tunnel.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SocketKnife.StressTest.View
{
    public class MockClientView:DockContent
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private TestKernel _Kernel = DI.Get<TestKernel>();
        private BytesCodec _Codec = DI.Get<BytesCodec>();

        #region UI
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CreateClientButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button SendProtocolButton;
        private System.Windows.Forms.ListBox ClientProtocolListBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox MockClientProtocolReceiveHistoryTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox ConnectedMockClientListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ClientCountTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox AutoConnectAfterCreationCheckBox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox DataToSendByClientTextBox;
        private System.Windows.Forms.Panel panel3;
        private TextBox InvokeFunctionCountTextBox;
        private RadioButton InvokeFunctionOneTimeRadioButton;
        private Label label6;
        private Label label3;
        private TextBox InvokeFunctionIntervalTextBox;
        private RadioButton InvokeFunctionSeveralTimeRadioButton;
        private System.Windows.Forms.SplitContainer ClientViewSplitContainer;
    
        public MockClientView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.AutoConnectAfterCreationCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ClientCountTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CreateClientButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ClientViewSplitContainer = new System.Windows.Forms.SplitContainer();
            this.ConnectedMockClientListBox = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.InvokeFunctionCountTextBox = new System.Windows.Forms.TextBox();
            this.InvokeFunctionOneTimeRadioButton = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.InvokeFunctionIntervalTextBox = new System.Windows.Forms.TextBox();
            this.InvokeFunctionSeveralTimeRadioButton = new System.Windows.Forms.RadioButton();
            this.DataToSendByClientTextBox = new System.Windows.Forms.TextBox();
            this.SendProtocolButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ClientProtocolListBox = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MockClientProtocolReceiveHistoryTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientViewSplitContainer)).BeginInit();
            this.ClientViewSplitContainer.Panel1.SuspendLayout();
            this.ClientViewSplitContainer.Panel2.SuspendLayout();
            this.ClientViewSplitContainer.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.AutoConnectAfterCreationCheckBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.ClientCountTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CreateClientButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 55);
            this.panel1.TabIndex = 0;
            // 
            // AutoConnectAfterCreationCheckBox
            // 
            this.AutoConnectAfterCreationCheckBox.AutoSize = true;
            this.AutoConnectAfterCreationCheckBox.Checked = true;
            this.AutoConnectAfterCreationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoConnectAfterCreationCheckBox.Location = new System.Drawing.Point(398, 20);
            this.AutoConnectAfterCreationCheckBox.Name = "AutoConnectAfterCreationCheckBox";
            this.AutoConnectAfterCreationCheckBox.Size = new System.Drawing.Size(72, 16);
            this.AutoConnectAfterCreationCheckBox.TabIndex = 21;
            this.AutoConnectAfterCreationCheckBox.Text = "自动连接";
            this.AutoConnectAfterCreationCheckBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(318, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "（1-10）";
            // 
            // ClientCountTextBox
            // 
            this.ClientCountTextBox.Location = new System.Drawing.Point(212, 18);
            this.ClientCountTextBox.Name = "ClientCountTextBox";
            this.ClientCountTextBox.Size = new System.Drawing.Size(100, 21);
            this.ClientCountTextBox.TabIndex = 19;
            this.ClientCountTextBox.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "Client数量：";
            // 
            // CreateClientButton
            // 
            this.CreateClientButton.Location = new System.Drawing.Point(24, 13);
            this.CreateClientButton.Name = "CreateClientButton";
            this.CreateClientButton.Size = new System.Drawing.Size(75, 28);
            this.CreateClientButton.TabIndex = 0;
            this.CreateClientButton.Text = "创建";
            this.CreateClientButton.UseVisualStyleBackColor = true;
            this.CreateClientButton.Click += new System.EventHandler(this.CreateClientButtonClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ClientViewSplitContainer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(715, 530);
            this.panel2.TabIndex = 1;
            // 
            // ClientViewSplitContainer
            // 
            this.ClientViewSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientViewSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.ClientViewSplitContainer.Name = "ClientViewSplitContainer";
            // 
            // ClientViewSplitContainer.Panel1
            // 
            this.ClientViewSplitContainer.Panel1.Controls.Add(this.ConnectedMockClientListBox);
            // 
            // ClientViewSplitContainer.Panel2
            // 
            this.ClientViewSplitContainer.Panel2.Controls.Add(this.groupBox4);
            this.ClientViewSplitContainer.Panel2.Controls.Add(this.groupBox3);
            this.ClientViewSplitContainer.Panel2.Controls.Add(this.groupBox1);
            this.ClientViewSplitContainer.Size = new System.Drawing.Size(715, 530);
            this.ClientViewSplitContainer.SplitterDistance = 177;
            this.ClientViewSplitContainer.TabIndex = 0;
            // 
            // ConnectedMockClientListBox
            // 
            this.ConnectedMockClientListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectedMockClientListBox.FormattingEnabled = true;
            this.ConnectedMockClientListBox.ItemHeight = 12;
            this.ConnectedMockClientListBox.Location = new System.Drawing.Point(0, 0);
            this.ConnectedMockClientListBox.Name = "ConnectedMockClientListBox";
            this.ConnectedMockClientListBox.Size = new System.Drawing.Size(177, 530);
            this.ConnectedMockClientListBox.TabIndex = 0;
            this.ConnectedMockClientListBox.Click += new System.EventHandler(this.ConnectedMockClientListBox_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel4);
            this.groupBox4.Controls.Add(this.panel3);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 242);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(534, 288);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "协议发送窗口";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.InvokeFunctionCountTextBox);
            this.panel4.Controls.Add(this.InvokeFunctionOneTimeRadioButton);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.InvokeFunctionIntervalTextBox);
            this.panel4.Controls.Add(this.InvokeFunctionSeveralTimeRadioButton);
            this.panel4.Controls.Add(this.DataToSendByClientTextBox);
            this.panel4.Controls.Add(this.SendProtocolButton);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(153, 17);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panel4.Size = new System.Drawing.Size(378, 268);
            this.panel4.TabIndex = 23;
            // 
            // InvokeFunctionCountTextBox
            // 
            this.InvokeFunctionCountTextBox.Location = new System.Drawing.Point(81, 180);
            this.InvokeFunctionCountTextBox.Name = "InvokeFunctionCountTextBox";
            this.InvokeFunctionCountTextBox.Size = new System.Drawing.Size(33, 21);
            this.InvokeFunctionCountTextBox.TabIndex = 49;
            this.InvokeFunctionCountTextBox.Text = "3";
            // 
            // InvokeFunctionOneTimeRadioButton
            // 
            this.InvokeFunctionOneTimeRadioButton.AutoSize = true;
            this.InvokeFunctionOneTimeRadioButton.Checked = true;
            this.InvokeFunctionOneTimeRadioButton.Location = new System.Drawing.Point(14, 158);
            this.InvokeFunctionOneTimeRadioButton.Name = "InvokeFunctionOneTimeRadioButton";
            this.InvokeFunctionOneTimeRadioButton.Size = new System.Drawing.Size(119, 16);
            this.InvokeFunctionOneTimeRadioButton.TabIndex = 45;
            this.InvokeFunctionOneTimeRadioButton.TabStop = true;
            this.InvokeFunctionOneTimeRadioButton.Text = "一次执行（默认）";
            this.InvokeFunctionOneTimeRadioButton.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 48;
            this.label6.Text = "毫秒，不小于50";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 50;
            this.label3.Text = "次，间隔";
            // 
            // InvokeFunctionIntervalTextBox
            // 
            this.InvokeFunctionIntervalTextBox.Location = new System.Drawing.Point(171, 180);
            this.InvokeFunctionIntervalTextBox.Name = "InvokeFunctionIntervalTextBox";
            this.InvokeFunctionIntervalTextBox.Size = new System.Drawing.Size(47, 21);
            this.InvokeFunctionIntervalTextBox.TabIndex = 47;
            this.InvokeFunctionIntervalTextBox.Text = "1000";
            // 
            // InvokeFunctionSeveralTimeRadioButton
            // 
            this.InvokeFunctionSeveralTimeRadioButton.AutoSize = true;
            this.InvokeFunctionSeveralTimeRadioButton.Location = new System.Drawing.Point(14, 181);
            this.InvokeFunctionSeveralTimeRadioButton.Name = "InvokeFunctionSeveralTimeRadioButton";
            this.InvokeFunctionSeveralTimeRadioButton.Size = new System.Drawing.Size(71, 16);
            this.InvokeFunctionSeveralTimeRadioButton.TabIndex = 46;
            this.InvokeFunctionSeveralTimeRadioButton.Text = "循环执行";
            this.InvokeFunctionSeveralTimeRadioButton.UseVisualStyleBackColor = true;
            // 
            // DataToSendByClientTextBox
            // 
            this.DataToSendByClientTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.DataToSendByClientTextBox.Location = new System.Drawing.Point(10, 0);
            this.DataToSendByClientTextBox.Multiline = true;
            this.DataToSendByClientTextBox.Name = "DataToSendByClientTextBox";
            this.DataToSendByClientTextBox.Size = new System.Drawing.Size(368, 102);
            this.DataToSendByClientTextBox.TabIndex = 22;
            // 
            // SendProtocolButton
            // 
            this.SendProtocolButton.Location = new System.Drawing.Point(13, 118);
            this.SendProtocolButton.Name = "SendProtocolButton";
            this.SendProtocolButton.Size = new System.Drawing.Size(111, 32);
            this.SendProtocolButton.TabIndex = 1;
            this.SendProtocolButton.Text = "发送协议";
            this.SendProtocolButton.UseVisualStyleBackColor = true;
            this.SendProtocolButton.Click += new System.EventHandler(this.SendProtocolButtonClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ClientProtocolListBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(3, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(150, 268);
            this.panel3.TabIndex = 22;
            // 
            // ClientProtocolListBox
            // 
            this.ClientProtocolListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientProtocolListBox.FormattingEnabled = true;
            this.ClientProtocolListBox.ItemHeight = 12;
            this.ClientProtocolListBox.Location = new System.Drawing.Point(0, 0);
            this.ClientProtocolListBox.Name = "ClientProtocolListBox";
            this.ClientProtocolListBox.Size = new System.Drawing.Size(150, 268);
            this.ClientProtocolListBox.TabIndex = 0;
            this.ClientProtocolListBox.Click += new System.EventHandler(this.ClientProtocolListBoxClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.MockClientProtocolReceiveHistoryTextBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(534, 142);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "协议接收及提示窗口";
            // 
            // MockClientProtocolReceiveHistoryTextBox
            // 
            this.MockClientProtocolReceiveHistoryTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MockClientProtocolReceiveHistoryTextBox.Location = new System.Drawing.Point(3, 17);
            this.MockClientProtocolReceiveHistoryTextBox.Multiline = true;
            this.MockClientProtocolReceiveHistoryTextBox.Name = "MockClientProtocolReceiveHistoryTextBox";
            this.MockClientProtocolReceiveHistoryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MockClientProtocolReceiveHistoryTextBox.Size = new System.Drawing.Size(528, 122);
            this.MockClientProtocolReceiveHistoryTextBox.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(534, 100);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "仿真Client状态";
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(303, 50);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(111, 32);
            this.button4.TabIndex = 28;
            this.button4.Text = "删除";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(37, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 27;
            this.label7.Text = "已断开";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(167, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 32);
            this.button2.TabIndex = 25;
            this.button2.Text = "断开";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(30, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 32);
            this.button1.TabIndex = 24;
            this.button1.Text = "连接";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MockClientView
            // 
            this.ClientSize = new System.Drawing.Size(715, 585);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MockClientView";
            this.Text = "仿真客户端";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ClientViewSplitContainer.Panel1.ResumeLayout(false);
            this.ClientViewSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ClientViewSplitContainer)).EndInit();
            this.ClientViewSplitContainer.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        protected override void OnShown(EventArgs e)
        {
            _Kernel.MockClientAmountChanged += MockClientAmountChanged;
            _Kernel.MockClientProtocolReceived += OnMockClientProtocolReceived;
            ClientProtocolListBox.Items.Add(new InitializeTestReply(NangleProtocolUtility.EmptyBytes4, 0x00, new byte[] { 0x00, 0x00, 0x00, 0x01 }));
            ClientProtocolListBox.Items.Add(new ExecuteTestCaseReply(NangleProtocolUtility.EmptyBytes4, 0x00));
            ClientProtocolListBox.Items.Add(new StopExecuteTestCaseReply(NangleProtocolUtility.EmptyBytes4, 0x00));
            ClientProtocolListBox.Items.Add(new ReadTestCaseResultReply(
                NangleProtocolUtility.EmptyBytes4,
                NangleProtocolUtility.GetTestCaseIndex(1), //用例编号
                new byte[] { 0x00, 0x00, 0x00, 0x00 },
                new byte[] { 0x00, 0x00, 0x00, 0x00 },
                new byte[] { 0x00, 0x00, 0x00, 0x00 },
                new byte[] { 0x00, 0x00, 0x00, 0x00 }));
            ClientProtocolListBox.Items.Add(new TestRawData(NangleProtocolUtility.EmptyBytes4, 0x00,
                new byte[] {0x00, 0x01}));
            base.OnShown(e);
        }



        #endregion

        /// <summary>
        /// 校验测试参数
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
                int i = 0;
                foreach (var mainTestClientHandler in clients)
                {
                    i += 1;
                    ConnectedMockClientListBox.Items.Add(string.Format("仿真{0}", i));
                }
            });


        }

        /// <summary>
        /// 仿真客户端收到协议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="nangleProtocolEventArgs"></param>
        private void OnMockClientProtocolReceived(object sender, NangleProtocolEventArgs nangleProtocolEventArgs)
        {
            MockClientProtocolReceiveHistoryTextBox.ThreadSafeInvoke(() =>
            {
                var index = _Kernel.ClientHandlers.IndexOf(sender as MockClientHandler) + 1;
                MockClientProtocolReceiveHistoryTextBox.Text = string.Format("{0} 仿真客户端[{1}]收到协议[{2}]: \r\n{3}",
                     DateTime.Now.ToString("HH:mm:ss fff"), index, NangleProtocolUtility.GetProtocolDescription(nangleProtocolEventArgs.Protocol), MockClientProtocolReceiveHistoryTextBox.Text);
                AppUtility.LimitTextBoxTextLengh(MockClientProtocolReceiveHistoryTextBox);
            });
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
                    byte[] data = UtilityConvert.HexToBytes(DataToSendByClientTextBox.Text);
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
                            for (int i = 0; i < count; i++)
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
            if (ClientProtocolListBox.SelectedIndex >= 0)
            {
                var protocol = ClientProtocolListBox.SelectedItem as BytesProtocol;
                //_ProtocolHandler.WriteToSession(sessionId, protocol);

                var family = _Kernel.GetProtocolFamily();
                byte[] original = family.Generate(protocol);
                byte[] data = _Codec.BytesEncoder.Execute(original);
                DataToSendByClientTextBox.Text = data.ToHexString();
            }
        }

        /// <summary>
        /// 点选仿真客户端列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectedMockClientListBox_Click(object sender, EventArgs e)
        {
            var index = ConnectedMockClientListBox.SelectedIndex;
            if (index >-1)
            {
                var clients = _Kernel.Clients;
                if (index < clients.Count)
                {
                    var client = clients[index];
                }

            }
        }
    }
}
