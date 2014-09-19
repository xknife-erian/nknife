using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Jeelu.Win
{
    public partial class ColorSelectorButton : Button
    {
        private Color _value;
        public Color Value
        {
            get { return _value; }
            set
            {
                _value = value;
                _colorSelector.Value = value;
                this.Image = Utility.Form.CreatImageForColorButton(this.Value);

                ///触发事件
                OnValueChanged(EventArgs.Empty);
            }
        }

        private ColorSelector _colorSelector = new ColorSelector();
        private Form colorPanel = new Form();

        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        public ColorSelectorButton()
        {
            InitializeComponent();

            colorPanel.BackColor = SystemColors.ControlLight;
            colorPanel.FormBorderStyle = FormBorderStyle.None;
            colorPanel.ShowInTaskbar = false;
            colorPanel.Size = new Size(230, 170);
            _colorSelector.Dock = DockStyle.Fill;
            colorPanel.Controls.Add(_colorSelector);
            colorPanel.Deactivate += new EventHandler(colorPanel_Deactivate);
            this.Image = Utility.Form.CreatImageForColorButton(this.Value);
        }

        void colorPanel_Deactivate(object sender, EventArgs e)
        {
            Value = _colorSelector.Value;
            SetWindowHide(colorPanel);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;

            ///画颜色块
            DrawColorBlock(g);

            base.OnPaint(pe);
        }

        void DrawColorBlock(Graphics g)
        {
            int offset = 4;
            if (this.Height < this.Width)
            {
                if (this.Height > 20)
                    offset = this.Height / 5;
            }
            else
            {
                if (this.Width > 20)
                    offset = this.Width / 5;
            }
            Rectangle r = new Rectangle(offset, offset,
                this.Width - offset - offset - 1, this.Height - offset - offset - 1);

            SolidBrush colorBrush = new SolidBrush(Value);
            g.FillRectangle(colorBrush, r);
            Pen blackPen = new Pen(Brushes.Black);
            g.DrawRectangle(blackPen, r);
        }

        protected override void OnClick(EventArgs e)
        {
            Point mc = this.Parent.PointToScreen(
                this.Location);
            int screenWidth = Screen.GetWorkingArea(this).Width;
            int screenHeight = Screen.GetWorkingArea(this).Height;

            if (mc.X < screenWidth / 2 && mc.Y < screenHeight / 2)//4像限
            {
                int xx, yy;
                xx = mc.X + 2;
                yy = mc.Y + this.Height - 1;
                this.colorPanel.Location = new Point(xx, yy);
                SetWindowShow(colorPanel, null, xx, yy, colorPanel.Width, colorPanel.Height);
            }
            else if (mc.X < screenWidth / 2 && mc.Y > screenHeight / 2)//3像限
            {
                int xx, yy;
                xx = mc.X + 2;
                yy = mc.Y - colorPanel.Height + 1;
                this.colorPanel.Location = new Point(xx, yy);
                SetWindowShow(colorPanel, null, xx, yy, colorPanel.Width, colorPanel.Height);
            }
            else if (mc.X > screenWidth / 2 && mc.Y > screenHeight / 2)//2像限
            {
                int xx, yy;
                xx = mc.X + this.Width - colorPanel.Width - 1;
                yy = mc.Y - colorPanel.Height + 1;
                this.colorPanel.Location = new Point(xx, yy);
                SetWindowShow(colorPanel, null, xx, yy, colorPanel.Width, colorPanel.Height);
            }
            else if (mc.X > screenWidth / 2 && mc.Y < screenHeight / 2)//1像限
            {
                int xx, yy;
                xx = mc.X + this.Width - colorPanel.Width - 1;
                yy = mc.Y + this.Height - 1;
                this.colorPanel.Location = new Point(xx, yy);
                SetWindowShow(colorPanel, null, xx, yy, colorPanel.Width, colorPanel.Height);
            }

            colorPanel.Show();
            colorPanel.Activate();
            colorPanel.Select();

            base.OnClick(e);
        }

        #region 防止焦点切换闪烁
        [DllImport("user32.dll")]
        private static extern void SetWindowPos(IntPtr formHandle, IntPtr hWndInsertAfter, int x, int y, int width, int height, int args);
        const int SWP_NOSIZE = 0x0001;
        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_NOREDRAW = 0x0008;
        const int SWP_NOACTIVATE = 0x0010;
        const int SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        const int SWP_SHOWWINDOW = 0x0040;
        const int SWP_HIDEWINDOW = 0x0080;
        const int SWP_NOCOPYBITS = 0x0100;
        const int SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        const int SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */

        public static void SetWindowHide(Form form)
        {
            SetWindowPos(form.Handle, (IntPtr)0, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_HIDEWINDOW | SWP_NOSENDCHANGING);
        }
        public static void SetWindowShow(Form form, Form formAfter, int x, int y, int width, int height)
        {
            SetWindowPos(form.Handle, (formAfter == null ? (IntPtr)0 : formAfter.Handle), x, y, width, height, SWP_SHOWWINDOW | SWP_FRAMECHANGED);
        }
        #endregion
    }
}
