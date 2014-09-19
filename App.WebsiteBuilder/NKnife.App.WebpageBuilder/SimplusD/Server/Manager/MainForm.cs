using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Jeelu.SimplusD.Server;
using System.Net;
using System.Net.Sockets;
namespace Jeelu.SimplusD.Server.Manager
{
    public partial class ManageForm : Form
    {
        public ManageForm()
        {
            InitializeComponent();
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        protected override void OnLoad(EventArgs e)
        {
            this.ReShow();
            base.OnLoad(e);
        }

    

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.ReShow();

        }

        private void ReShow()
        {
            int iCount = 0;
            listUser.Items.Clear();

            foreach (KeyValuePair<TcpClient, string> keyvalue in ServerCore.DicUserClient)
            {
                string file = "";
                ServerCore.DicFileClient.TryGetValue(keyvalue.Key, out file);
                ListViewItem item = new ListViewItem(new string[] { keyvalue.Value, DisposeUserMethod.GetClientIp(keyvalue.Key), file });
                listUser.Items.Add(item);
                iCount++;
            }
            this._onLineNumberLabel.Text = "共有" + iCount + "人在线";
        }
    }
}
