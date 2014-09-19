using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "服务器";
            RemotingServer.Start();

            Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(30);

            timer1.Start();

            ExeMySQLCommand cmd = new ExeMySQLCommand();
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            //cmd.CreateMonthRetrnData(year, month);
            //cmd.CreateSeasonRetrnData(year, month);
            //cmd.CreateCustomRetrnData();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Show();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(30);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Day == '1')
            {
                ExeMySQLCommand cmd = new ExeMySQLCommand();
                int year = DateTime.Today.Year;
                int month = DateTime.Today.Month;
                cmd.CreateMonthRetrnData(year, month);
            }
        }

        
    }
}
