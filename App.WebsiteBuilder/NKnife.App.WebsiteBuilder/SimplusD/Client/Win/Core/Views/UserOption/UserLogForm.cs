using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class UserLogForm : UserGeneralForm
    {
        static private UserLogForm _singlerForm;
        static public UserLogForm SinglerForm
        {
            get
            {
                if (_singlerForm == null || _singlerForm.IsDisposed)
                {
                    _singlerForm = new UserLogForm();
                }
                return _singlerForm;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Navigation();

            base.OnLoad(e);
        }

        private void Navigation()
        {
            //http://jeelu.com/sd_user/login.htm
            Navigation(StringParserService.Parse("${res:user.login.url}"));
        }

        private UserLogForm()
            : base()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.Size = new Size(430,350);
        }
    }
}
