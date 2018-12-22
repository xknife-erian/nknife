using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.Extensions;
using NKnife.IoC;
using NKnife.Kits.SerialKnife.Mock;
using SerialKnife.Interfaces;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.SerialKnife.Views
{
    public class MockSerialDataConnectorView:DockContent
    {
        private MockSerialDataConnector _dataConnector = (MockSerialDataConnector) Di.Get<ISerialConnector>("Mock");

        #region 初始化
        private System.Windows.Forms.ListBox _mockReceiveListBox;
        private System.Windows.Forms.GroupBox _groupBox1;
        private System.Windows.Forms.TextBox _mockReceiveContentTextBox;
        private System.Windows.Forms.GroupBox _groupBox2;
        private System.Windows.Forms.Button _doMockReceiveButton;
        private System.Windows.Forms.Button _doMockDisconnectButton;

        public MockSerialDataConnectorView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this._mockReceiveListBox = new System.Windows.Forms.ListBox();
            this._groupBox1 = new System.Windows.Forms.GroupBox();
            this._mockReceiveContentTextBox = new System.Windows.Forms.TextBox();
            this._groupBox2 = new System.Windows.Forms.GroupBox();
            this._doMockReceiveButton = new System.Windows.Forms.Button();
            this._doMockDisconnectButton = new System.Windows.Forms.Button();
            this._groupBox1.SuspendLayout();
            this._groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MockReceiveListBox
            // 
            this._mockReceiveListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mockReceiveListBox.FormattingEnabled = true;
            this._mockReceiveListBox.ItemHeight = 12;
            this._mockReceiveListBox.Location = new System.Drawing.Point(3, 17);
            this._mockReceiveListBox.Name = "_mockReceiveListBox";
            this._mockReceiveListBox.Size = new System.Drawing.Size(297, 217);
            this._mockReceiveListBox.TabIndex = 0;
            this._mockReceiveListBox.Click += new System.EventHandler(this.MockReceiveListBox_Click);
            // 
            // groupBox1
            // 
            this._groupBox1.Controls.Add(this._mockReceiveListBox);
            this._groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this._groupBox1.Location = new System.Drawing.Point(0, 0);
            this._groupBox1.Name = "_groupBox1";
            this._groupBox1.Size = new System.Drawing.Size(303, 237);
            this._groupBox1.TabIndex = 1;
            this._groupBox1.TabStop = false;
            this._groupBox1.Text = "预定义模拟收到数据";
            // 
            // MockReceiveContentTextBox
            // 
            this._mockReceiveContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mockReceiveContentTextBox.Location = new System.Drawing.Point(3, 17);
            this._mockReceiveContentTextBox.Multiline = true;
            this._mockReceiveContentTextBox.Name = "_mockReceiveContentTextBox";
            this._mockReceiveContentTextBox.Size = new System.Drawing.Size(297, 121);
            this._mockReceiveContentTextBox.TabIndex = 2;
            // 
            // groupBox2
            // 
            this._groupBox2.Controls.Add(this._mockReceiveContentTextBox);
            this._groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this._groupBox2.Location = new System.Drawing.Point(0, 237);
            this._groupBox2.Name = "_groupBox2";
            this._groupBox2.Size = new System.Drawing.Size(303, 141);
            this._groupBox2.TabIndex = 3;
            this._groupBox2.TabStop = false;
            this._groupBox2.Text = "数据内容";
            // 
            // DoMockReceiveButton
            // 
            this._doMockReceiveButton.Location = new System.Drawing.Point(84, 390);
            this._doMockReceiveButton.Name = "_doMockReceiveButton";
            this._doMockReceiveButton.Size = new System.Drawing.Size(207, 31);
            this._doMockReceiveButton.TabIndex = 4;
            this._doMockReceiveButton.Text = "模拟收到数据源数据（执行）";
            this._doMockReceiveButton.UseVisualStyleBackColor = true;
            this._doMockReceiveButton.Click += new System.EventHandler(this.DoMockReceiveButton_Click);
            // 
            // DoMockDisconnectButton
            // 
            this._doMockDisconnectButton.Location = new System.Drawing.Point(84, 428);
            this._doMockDisconnectButton.Name = "_doMockDisconnectButton";
            this._doMockDisconnectButton.Size = new System.Drawing.Size(207, 31);
            this._doMockDisconnectButton.TabIndex = 5;
            this._doMockDisconnectButton.Text = "模拟与数据源断开（执行）";
            this._doMockDisconnectButton.UseVisualStyleBackColor = true;
            this._doMockDisconnectButton.Visible = false;
            this._doMockDisconnectButton.Click += new System.EventHandler(this.DoMockDisconnectButton_Click);
            // 
            // MockSerialDataConnectorView
            // 
            this.ClientSize = new System.Drawing.Size(303, 474);
            this.Controls.Add(this._doMockDisconnectButton);
            this.Controls.Add(this._doMockReceiveButton);
            this.Controls.Add(this._groupBox2);
            this.Controls.Add(this._groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MockSerialDataConnectorView";
            this.Text = "模拟串口数据连接器";
            this.Load += new System.EventHandler(this.MockSerialDataConnectorView_Load);
            this._groupBox1.ResumeLayout(false);
            this._groupBox2.ResumeLayout(false);
            this._groupBox2.PerformLayout();
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
            _mockReceiveListBox.DataSource = mockReceiveList;
            _mockReceiveListBox.DisplayMember = "Key";

            _doMockReceiveButton.Enabled = Properties.Settings.Default.EnableMock;
        }

        private void MockReceiveListBox_Click(object sender, EventArgs e)
        {
            if (_mockReceiveListBox.SelectedIndex < 0)
                return;

            var item = _mockReceiveListBox.SelectedItem;
            var data = ((KeyValuePair<string, byte[]>) item).Value;
            _mockReceiveContentTextBox.Text = data.ToHexString();
        }

        /// <summary>
        /// 模拟收到数据源数据（执行）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoMockReceiveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_mockReceiveContentTextBox.Text))
            {
                MessageBox.Show("模拟数据为空，可以从列表中选择，或自定义发送内容");
                return;
            }
            try
            {
                var data = _mockReceiveContentTextBox.Text.ToBytes();
                _dataConnector.MockReceive(data);
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
