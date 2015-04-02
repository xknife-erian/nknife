using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Mock;
using SerialKnife.Interfaces;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SerialKnife.Views
{
    public class MockSerialDataConnectorView:DockContent
    {
        private MockSerialDataConnector _DataConnector = (MockSerialDataConnector) DI.Get<ISerialConnector>("Mock");

        #region 初始化
        private System.Windows.Forms.ListBox MockReceiveListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox MockReceiveContentTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button DoMockReceiveButton;
        private System.Windows.Forms.Button DoMockDisconnectButton;

        public MockSerialDataConnectorView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.MockReceiveListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MockReceiveContentTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DoMockReceiveButton = new System.Windows.Forms.Button();
            this.DoMockDisconnectButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MockReceiveListBox
            // 
            this.MockReceiveListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MockReceiveListBox.FormattingEnabled = true;
            this.MockReceiveListBox.ItemHeight = 12;
            this.MockReceiveListBox.Location = new System.Drawing.Point(3, 17);
            this.MockReceiveListBox.Name = "MockReceiveListBox";
            this.MockReceiveListBox.Size = new System.Drawing.Size(297, 217);
            this.MockReceiveListBox.TabIndex = 0;
            this.MockReceiveListBox.Click += new System.EventHandler(this.MockReceiveListBox_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MockReceiveListBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 237);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预定义模拟收到数据";
            // 
            // MockReceiveContentTextBox
            // 
            this.MockReceiveContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MockReceiveContentTextBox.Location = new System.Drawing.Point(3, 17);
            this.MockReceiveContentTextBox.Multiline = true;
            this.MockReceiveContentTextBox.Name = "MockReceiveContentTextBox";
            this.MockReceiveContentTextBox.Size = new System.Drawing.Size(297, 121);
            this.MockReceiveContentTextBox.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MockReceiveContentTextBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 141);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据内容";
            // 
            // DoMockReceiveButton
            // 
            this.DoMockReceiveButton.Location = new System.Drawing.Point(84, 390);
            this.DoMockReceiveButton.Name = "DoMockReceiveButton";
            this.DoMockReceiveButton.Size = new System.Drawing.Size(207, 31);
            this.DoMockReceiveButton.TabIndex = 4;
            this.DoMockReceiveButton.Text = "模拟收到数据源数据（执行）";
            this.DoMockReceiveButton.UseVisualStyleBackColor = true;
            this.DoMockReceiveButton.Click += new System.EventHandler(this.DoMockReceiveButton_Click);
            // 
            // DoMockDisconnectButton
            // 
            this.DoMockDisconnectButton.Location = new System.Drawing.Point(84, 428);
            this.DoMockDisconnectButton.Name = "DoMockDisconnectButton";
            this.DoMockDisconnectButton.Size = new System.Drawing.Size(207, 31);
            this.DoMockDisconnectButton.TabIndex = 5;
            this.DoMockDisconnectButton.Text = "模拟与数据源断开（执行）";
            this.DoMockDisconnectButton.UseVisualStyleBackColor = true;
            this.DoMockDisconnectButton.Visible = false;
            this.DoMockDisconnectButton.Click += new System.EventHandler(this.DoMockDisconnectButton_Click);
            // 
            // MockSerialDataConnectorView
            // 
            this.ClientSize = new System.Drawing.Size(303, 474);
            this.Controls.Add(this.DoMockDisconnectButton);
            this.Controls.Add(this.DoMockReceiveButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MockSerialDataConnectorView";
            this.Text = "模拟串口数据连接器";
            this.Load += new System.EventHandler(this.MockSerialDataConnectorView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void MockSerialDataConnectorView_Load(object sender, EventArgs e)
        {
            var mockReceiveList = new List<KeyValuePair<string, byte[]>>();
            mockReceiveList.Add(new KeyValuePair<string, byte[]>(
                "登录",new byte[]{0xA0, 0x07,0x3D,0x03,0x10,0x01,0x00,0x00,0x00,0x58,0xFF}));
            mockReceiveList.Add(new KeyValuePair<string, byte[]>(
                "一条登录（前后都有错包）", new byte[] { 0x01,0x05, 0xA0, 0x07, 0x3D, 0x03, 0x10, 0x01, 0x00, 0x00, 0x00, 0x58, 0xFF, 0x55,0x46 }));
            mockReceiveList.Add(new KeyValuePair<string, byte[]>(
                "两条登录（内容不同）", new byte[] { 0xA0, 0x07, 0x3D, 0x03, 0x10, 0x01, 0x00, 0x00, 0x00, 0x58, 0xFF, 0xA0, 0x07, 0x3D, 0x03, 0x10, 0x03, 0x00, 0x00, 0x00, 0x5A, 0xFF }));
            MockReceiveListBox.DataSource = mockReceiveList;
            MockReceiveListBox.DisplayMember = "Key";

            DoMockReceiveButton.Enabled = Properties.Settings.Default.EnableMock;
        }

        private void MockReceiveListBox_Click(object sender, EventArgs e)
        {
            if (MockReceiveListBox.SelectedIndex < 0)
                return;

            var item = MockReceiveListBox.SelectedItem;
            var data = ((KeyValuePair<string, byte[]>) item).Value;
            MockReceiveContentTextBox.Text = data.ToHexString();
        }

        /// <summary>
        /// 模拟收到数据源数据（执行）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoMockReceiveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MockReceiveContentTextBox.Text))
            {
                MessageBox.Show("模拟数据为空，可以从列表中选择，或自定义发送内容");
                return;
            }
            try
            {
                var data = MockReceiveContentTextBox.Text.ToBytes();
                _DataConnector.MockReceive(data);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("模拟数据格式不正确，无法转换成字节数组");
            }
        }

        private void DoMockDisconnectButton_Click(object sender, EventArgs e)
        {

        }
    }
}
