using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Jeelu.SimplusD.Client.Win
{
    public class DrawDropFollowForm : Form
    {
        readonly public static DrawDropFollowForm Singler = new DrawDropFollowForm();

        static private Color _borderColor = Color.FromArgb(100, 100, 100);
        static public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

        static private float _borderWidth = 1f;
        static public float BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }

        static Pen _borderPen;

        static DrawDropFollowForm()
        {
            _borderPen = new Pen(BorderColor, _borderWidth);
            _borderPen.DashStyle = DashStyle.Dot;
        }
        private DrawDropFollowForm()
        {
            ///设置一些初始值
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.AllowTransparency = true;
            this.AllowDrop = true;
            //this.Opacity = 0.8;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_borderPen, new Rectangle(0, 0, this.Width - 1, this.Height - 1));

            base.OnPaint(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.Hide();

            base.OnMouseUp(e);
        }

        static public void ShowForm(int x, int y, int width, int height)
        {
            Utility.DllImport.SetWindowShow(Singler, null, x, y, width, height);
        }
        static public void HideForm()
        {
            Singler.Hide();
        }
    }
}
