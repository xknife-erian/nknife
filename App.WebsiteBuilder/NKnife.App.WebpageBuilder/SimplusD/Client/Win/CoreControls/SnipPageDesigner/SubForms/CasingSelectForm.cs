using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
    public class CasingSelectForm : Form
    {
        static internal CasingSelectForm Singler = new CasingSelectForm();
        private CasingSelectForm()
        {
            this.SuspendLayout();

            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(0, 0, 255);
            this.MinimumSize = new Size(1, 1);
            this.Opacity = 0.2;

            this.ResumeLayout(false);
        }

        public static void ShowForm(int x, int y, int width, int height)
        {
            Utility.DllImport.SetWindowShow(Singler, null, x, y, width, height);
        }

        public static void HideForm()
        {
            Singler.Hide();
        }
    }
}
