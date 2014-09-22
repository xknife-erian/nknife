using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusOM.Client
{
    public partial class UserInfoForm : Form
    {
        public UserInfoForm(DataTable industryTable)
        {
            InitializeComponent();


            industryComboBox.DataSource = industryTable;
            industryComboBox.ValueMember = "code";
            industryComboBox.DisplayMember = "name";
        }

        public User UserValue
        {
            get
            {
                User user = new User();
                user.UserId = textBox40.Text;
                user.UserName = textBox26.Text;
                user.UserLinkMan = textBox36.Text;
                user.UserLinkTel = textBox30.Text;
                user.UserFax = textBox34.Text;
                user.UserEmail = textBox28.Text;
                user.UserPostCode = textBox29.Text;
                user.UserAdress = textBox27.Text;
                user.WebURL = textBox1.Text;
                user.WebSite = textBox6.Text;
                user.WebIndustry = industryComboBox.SelectedValue.ToString();
                return user;
            }
            set
            {
                textBox40.Text = value.UserId;
                textBox26.Text = value.UserName;
                textBox36.Text = value.UserLinkMan;
                textBox30.Text = value.UserLinkTel;
                textBox34.Text = value.UserFax;
                textBox28.Text = value.UserEmail;
                textBox29.Text = value.UserPostCode;
                textBox27.Text = value.UserAdress;
                textBox1.Text = value.WebURL;
                textBox6.Text = value.WebSite;
                industryComboBox.SelectedValue = value.WebIndustry;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
