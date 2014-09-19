using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Remoting;
using System.Data;
using Jeelu.SimplusOM.Client.Win;
using System.Xml;
using System.IO;

namespace Jeelu.SimplusOM.Client
{
    public class LoginForm : Form
    {
        private Button CancelButton;
        private Button LoginButtton;
        private System.Windows.Forms.Button button1;
        private Label label1;
        private TextBox userNameTextBox;
        private Label label2;
        private TextBox passWordTextBox;
        private Label label3;
        private TextBox AgentIDTextBox;
        private CheckBox SaveLoginInfo;

        private static LoginForm _loginForm;
        public static LoginForm MainLoginForm
        {
            get { return _loginForm; }
        }
    
        public LoginForm()
        {
            this.Text = "客户端";
            InitializeComponent();
            this.Text = "登录 :"+DateTime.Today.ToShortDateString();

            dataAgent = DataAgentFactory.GetDataAgent();

            DateTime dt1 = DateTime.Parse("2008-08-08");
            DateTime dt2 = DateTime.Today;
            TimeSpan t = dt1- dt2;
            SaveLoginInfo.Text += "--距离奥运开幕还有" + t.Days + "天";
            SaveLoginInfo.ForeColor = Color.Green;

            if (File.Exists(@"c:\om.xml"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"c:\om.xml");
                XmlElement docEle = doc.DocumentElement;
                AgentIDTextBox.Text = docEle.GetAttribute("agentName");
                userNameTextBox.Text = docEle.GetAttribute("userName");
            }
        }

        private void InitializeComponent()
        {
            this.LoginButtton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passWordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AgentIDTextBox = new System.Windows.Forms.TextBox();
            this.SaveLoginInfo = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LoginButtton
            // 
            this.LoginButtton.Location = new System.Drawing.Point(123, 175);
            this.LoginButtton.Name = "LoginButtton";
            this.LoginButtton.Size = new System.Drawing.Size(71, 23);
            this.LoginButtton.TabIndex = 3;
            this.LoginButtton.Text = "登录(&L)";
            this.LoginButtton.UseVisualStyleBackColor = true;
            this.LoginButtton.Click += new System.EventHandler(this.LoginButtton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(206, 175);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(71, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "取消(&C)";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(105, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "用户名：";
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(123, 85);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(154, 21);
            this.userNameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码：";
            // 
            // passWordTextBox
            // 
            this.passWordTextBox.Location = new System.Drawing.Point(123, 112);
            this.passWordTextBox.Name = "passWordTextBox";
            this.passWordTextBox.PasswordChar = '*';
            this.passWordTextBox.Size = new System.Drawing.Size(154, 21);
            this.passWordTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "代理商ID：";
            // 
            // AgentIDTextBox
            // 
            this.AgentIDTextBox.Location = new System.Drawing.Point(123, 58);
            this.AgentIDTextBox.Name = "AgentIDTextBox";
            this.AgentIDTextBox.Size = new System.Drawing.Size(154, 21);
            this.AgentIDTextBox.TabIndex = 0;
            this.AgentIDTextBox.Text = "Jeelu";
            // 
            // SaveLoginInfo
            // 
            this.SaveLoginInfo.AutoSize = true;
            this.SaveLoginInfo.Checked = true;
            this.SaveLoginInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SaveLoginInfo.Location = new System.Drawing.Point(27, 152);
            this.SaveLoginInfo.Name = "SaveLoginInfo";
            this.SaveLoginInfo.Size = new System.Drawing.Size(98, 17);
            this.SaveLoginInfo.TabIndex = 4;
            this.SaveLoginInfo.Text = "保存登录信息";
            this.SaveLoginInfo.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.LoginButtton;
            this.ClientSize = new System.Drawing.Size(337, 246);
            this.ControlBox = false;
            this.Controls.Add(this.SaveLoginInfo);
            this.Controls.Add(this.passWordTextBox);
            this.Controls.Add(this.AgentIDTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.LoginButtton);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录 :2008-4-22";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        DataAgent dataAgent = null;
        DataSet selfInfoDataSet = null;
        DataSet baseInfoDataSet = null;

        public DataSet BaseInfoDataSet
        {
            get { return baseInfoDataSet; }
            set { baseInfoDataSet = value; }
        }

        public DataSet SelfInfoDataSet
        {
            get { return selfInfoDataSet; }
            set { selfInfoDataSet = value; }
        }

        private void LoginButtton_Click(object sender, EventArgs e)
        {
            baseInfoDataSet =OMWorkBench.DeserializeDataSet(dataAgent.GetBaseInfo());
            selfInfoDataSet = dataAgent.Login(AgentIDTextBox.Text,userNameTextBox.Text, passWordTextBox.Text);
            if (selfInfoDataSet != null)
            {
                this.DialogResult = DialogResult.OK;

                if (SaveLoginInfo.Checked)
                {
                    XmlDocument doc = new XmlDocument();
                    XmlElement docEle = doc.CreateElement("docEle");
                    docEle.SetAttribute("agentName", AgentIDTextBox.Text);
                    docEle.SetAttribute("userName", userNameTextBox.Text);
                    docEle.SetAttribute("passWord", passWordTextBox.Text);
                    doc.AppendChild(docEle);
                    doc.Save(@"c:\om.xml");
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("登录失败！");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
