using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Jeelu.Win
{
    /// <summary>
    /// 扩展的颜色选择框，默认选择Web的216种安全色,Button类型
    /// </summary>
    public class ColorGeneralButton : Button
    {
        Color _myColor = Color.Empty;
        /// <summary>
        /// 获取或设置用户选择的颜色
        /// </summary>
        public Color MyColor
        {
            get { return _myColor; }
            set
            {
                if (_myColor != value)
                {
                    _myColor = value;
                    _colorArgbString = ColorTranslator.ToHtml(value);
                    this.Invalidate();
                    OnMyColorChanged(EventArgs.Empty);
                }
            }
        }

        public override string Text
        {
            get
            {
                return ColorTranslator.ToHtml(_myColor);
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value == "Color [Empty]")
                {
                    MyColor = Color.White;
                }
                else
                {
                    MyColor = ColorTranslator.FromHtml(value);
                }
            }
        }

        private string _colorArgbString = "";
        /// <summary>
        /// 获取或设置用户选择的颜色RGB
        /// </summary>
        public string ColorArgbString
        {
            get { return _colorArgbString; }
            set { _myColor = ColorTranslator.FromHtml(value); }
        }

        public event EventHandler MyColorChanged;
        protected virtual void OnMyColorChanged(EventArgs e)
        {
            if (MyColorChanged != null)
            {
                MyColorChanged(this, e);
            }
        }

        MyPanel4Form colorPanel;
        public ColorGeneralButton()
        {
            this.colorPanel = new MyPanel4Form(_myColor);
            this.colorPanel.Size = new Size(253, 180);
            this.colorPanel.Visible = false;
            this.colorPanel.ColorSelected += delegate
            {
                this.MyColor = this.colorPanel.MyColor;
            };
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
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
            Graphics g = pevent.Graphics;
            Rectangle r = new Rectangle(offset, offset,
                this.Width - offset - offset - 1, this.Height - offset - offset - 1);

            SolidBrush colorBrush = new SolidBrush(_myColor);
            g.FillRectangle(colorBrush, r);
            Pen blackPen = new Pen(Brushes.Black);
            g.DrawRectangle(blackPen, r);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Point mc = this.Parent.PointToScreen(
                this.Bounds.Location);
            int screenWidth = Screen.GetWorkingArea(this).Width;
            int screenHeight = Screen.GetWorkingArea(this).Height;

            if (mc.X < screenWidth / 2 && mc.Y < screenHeight / 2)//4像限
            {
                int xx, yy;
                xx = mc.X + 2;
                yy = mc.Y + this.Height - 1;
                this.colorPanel.Location = new Point(xx, yy);
                Utility.DllImport.SetWindowShow(colorPanel, null, xx, yy, colorPanel.Width, colorPanel.Height);
            }
            else if (mc.X < screenWidth / 2 && mc.Y > screenHeight / 2)//3像限
            {
                int xx, yy;
                xx = mc.X + 2;
                yy = mc.Y - colorPanel.Height + 1;
                this.colorPanel.Location = new Point(xx, yy);
                Utility.DllImport.SetWindowShow(colorPanel, null, xx, yy, colorPanel.Width, colorPanel.Height);
            }
            else if (mc.X > screenWidth / 2 && mc.Y > screenHeight / 2)//2像限
            {
                int xx, yy;
                xx = mc.X + this.Width - colorPanel.Width - 1;
                yy = mc.Y - colorPanel.Height + 1;
                this.colorPanel.Location = new Point(xx, yy);
                Utility.DllImport.SetWindowShow(colorPanel, null, xx, yy, colorPanel.Width, colorPanel.Height);
            }
            else if (mc.X > screenWidth / 2 && mc.Y < screenHeight / 2)//1像限
            {
                int xx, yy;
                xx = mc.X + this.Width - colorPanel.Width - 1;
                yy = mc.Y + this.Height - 1;
                this.colorPanel.Location = new Point(xx, yy);
                Utility.DllImport.SetWindowShow(colorPanel, null, xx, yy, colorPanel.Width, colorPanel.Height);
            }

            colorPanel.Show();
            colorPanel.Activate();
            colorPanel.Select();

            if (_myColor != Color.Empty)
                colorPanel.GetBtnColor(_myColor);
        }

    } 
    
    /// <summary>
    /// 扩展的颜色选择框，默认选择Web的216种安全色,ToolStripSplitButton型
    /// </summary>
    public class ColorToolStripButton : ToolStripSplitButton
    {
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

        Color _myColor = Color.Empty;
        /// <summary>
        /// 用户选择的颜色
        /// </summary>
        public Color MyColor
        {
            get { return _myColor; }
            set
            {
                if (_myColor != value)
                {
                    _myColor = value;
                    this.Image = Utility.Form.CreatImageForColorButton(_myColor);//this.MyColor);
                    this.Invalidate();

                    OnMyColorChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler MyColorChanged;
        protected virtual void OnMyColorChanged(EventArgs e)
        {
            if (MyColorChanged != null)
            {
                MyColorChanged(this, e);
            }
        }

        public ColorToolStripButton()
        {
            this.colorPanel = new MyPanel4Form(_myColor);
            this.colorPanel.Size = new Size(253, 180);
            this.colorPanel.Visible = false;
            this.colorPanel.ColorSelected += delegate
            {
                this.MyColor = this.colorPanel.MyColor;
                this.Image = Utility.Form.CreatImageForColorButton(this.MyColor);
            };

            this.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.ImageTransparentColor = Color.Magenta;
            this.Size = new Size(23, 22);

            this.Image = Utility.Form.CreatImageForColorButton(this.MyColor);
            this.Text = this.MyColor.ToString();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
        }

        MyPanel4Form colorPanel;
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Point mc = this.Parent.PointToScreen(
                this.Bounds.Location);
            int screenWidth = Screen.GetWorkingArea(this.Parent).Width;
            int screenHeight = Screen.GetWorkingArea(this.Parent).Height;

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

            if (_myColor != Color.Empty)
                colorPanel.GetBtnColor(_myColor);
        }
    }

    /// <summary>
    /// Button点击后Show出的Panel，继承自Form
    /// </summary>
    class MyPanel4Form : Form
    {
        Panel _currPanel;
        TextBox _currTextBox;
        Button _btnWindowsColorDialog;
        ColorDialog _winColorDialog;

        #region 画颜色正方形的一些变量
        string[] _color216 ={ "00", "33", "66", "99", "CC", "FF" };
        string[] _importColor ={ "FF0000", "00FF00", "0000FF", "FFFF00", "00FFFF", "FF00FF" };

        /// <summary>
        /// 矩形的个数
        /// </summary>
        int t = 0;
        /// <summary>
        /// 颜色小正方形的边长
        /// </summary>
        int w = 11;

        int x = 0;
        int y = 25;
        #endregion

        public MyPanel4Form(Color sColor)
        {
            if (sColor.Name == "0")
                sColor = Color.White;

            _currPanel = new Panel();
            _currPanel.Location = new Point(w, w);
            _currPanel.Size = new Size(66, 20);
            _currPanel.BorderStyle = BorderStyle.FixedSingle;
            _currPanel.BackColor = sColor;
            this.Controls.Add(_currPanel);

            _currTextBox = new TextBox();
            _currTextBox.Size = new Size(70, 20);
            _currTextBox.Text = ColorTranslator.ToHtml(sColor);
            _currTextBox.Location = new Point(96 + w, w);
            _currTextBox.TextAlign = HorizontalAlignment.Center;
            _currTextBox.TabIndex = 0;
            _currTextBox.KeyDown += new KeyEventHandler(_currTextBox_KeyDown);
            this.Controls.Add(_currTextBox);

            _btnWindowsColorDialog = new Button();
            _btnWindowsColorDialog.Size = new Size(20, 20);
            _btnWindowsColorDialog.FlatStyle = FlatStyle.Popup;
            _btnWindowsColorDialog.Location = new Point(222, w);
            _btnWindowsColorDialog.Image = null;
            this.Controls.Add(_btnWindowsColorDialog);
            _btnWindowsColorDialog.Click += new EventHandler(ShowWinColorDialog);

            this.SetStyle(ControlStyles.Selectable, true);//设置ColorPanel可接收焦点
            this.BackColor = SystemColors.ControlLight;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Font = new Font("Courier New", 9F);
        }

        void _currTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {
                    MyColor = ColorTranslator.FromHtml(_currTextBox.Text);
                }
                catch { }
            }
        }

        void ShowWinColorDialog(object sender, EventArgs e)
        {
            this.Hide();
            _winColorDialog = new ColorDialog();
            _winColorDialog.AllowFullOpen = true;
            _winColorDialog.FullOpen = true;
            _winColorDialog.ShowHelp = true;
            if (_winColorDialog.ShowDialog() == DialogResult.OK)
            {
                MyColor = _winColorDialog.Color;
                OnColorSelected(EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            Rectangle r = new Rectangle(new Point(0, 0), new Size(this.Size.Width - 1, this.Size.Height - 1));
            g.DrawRectangle(pen, r);

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    x = w; y += w;
                    drawColorR(g, new Point(x, y), t, Color.Black);

                    if (i == 0)
                        drawColorR(g, new Point(x, y), t, GetColor(_color216[j] + _color216[j] + _color216[j]));
                    else
                        drawColorR(g, new Point(x, y), t, GetColor(_importColor[j]));

                    drawColorR(g, new Point(x, y), t, Color.Black);

                    for (int k = 0; k < 3; k++)
                    {
                        for (int m = 0; m < 6; m++)
                        {
                            drawColorR(g, new Point(x, y), t, GetColor(_color216[k + i * 3] + _color216[m] + _color216[j]));
                        }
                    }//一行结束
                }
            }
            x = 0; y = 25;
        }

        /// <summary>
        /// 记录每个矩形的位置与颜色值
        /// </summary>
        Dictionary<List<int>, Color> dic = new Dictionary<List<int>, Color>();
        /// <summary>
        /// 画颜色矩形
        /// </summary>
        void drawColorR(Graphics g, Point point, int t, Color color)
        {
            Pen pen = new Pen(Color.Black, 1);
            Brush brush = new SolidBrush(color);
            Rectangle r = new Rectangle(point, new Size(w, w));
            g.FillRectangle(brush, r);
            g.DrawRectangle(pen, r);
            x += w;
            t++;

            List<int> itemList = new List<int>();
            itemList.Add(point.X);
            itemList.Add(point.Y);
            itemList.Add(point.X + w);
            itemList.Add(point.Y + w);

            dic.Add(itemList, color);
        }

        static string ColorToRGB(Color color)
        {
            if (color == Color.Empty)
            {
                return "";
            }
            return "#" + color.ToArgb().ToString("x").Remove(0, 2);
        }

        static Color GetColor(String colorstring)
        {
            if (string.IsNullOrEmpty(colorstring))
            {
                return Color.Empty;
            }
            return Color.FromArgb(
                Convert.ToInt32(colorstring.Substring(0, 2), 16),
                Convert.ToInt32(colorstring.Substring(2, 2), 16),
                Convert.ToInt32(colorstring.Substring(4, 2), 16));
        }

        /// <summary>
        /// 储存上次鼠标所在的矩形位置
        /// </summary>
        KeyValuePair<List<int>, Color> prv = new KeyValuePair<List<int>, Color>();
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            if (x > 11 && x < 242 && y > 36 && y < 169)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;

            foreach (KeyValuePair<List<int>, Color> kv in dic)
            {
                if (prv.Key == null)
                    prv = kv;
                if (x > kv.Key[0] && x < kv.Key[2] && y > kv.Key[1] && y < kv.Key[3])
                {
                    Graphics g = CreateGraphics();
                    Pen pen = new Pen(Color.Yellow, 1);
                    Pen bPen = new Pen(Color.Black, 1);
                    Rectangle r = new Rectangle(
                        new Point(kv.Key[0], kv.Key[1]),
                        new Size(w, w));
                    if (prv.Key != null)//将上次鼠标所在的矩形外框颜色还原
                    {
                        Rectangle tmpR = new Rectangle(
                            new Point(prv.Key[0], prv.Key[1]),
                            new Size(w, w));
                        g.DrawRectangle(bPen, tmpR);
                    }

                    this._currPanel.BackColor = kv.Value;
                    this._currTextBox.Text = ColorToRGB(kv.Value).ToUpper();
                    this._currTextBox.Focus();
                    this._currTextBox.SelectAll();

                    g.DrawRectangle(pen, r);
                    prv = kv;//记录鼠标上次所在矩形位置
                }
            }
        }

        Color _myColor;
        public Color MyColor
        {
            get { return _myColor; }
            set { _myColor = value; }
        }

        public void GetBtnColor(Color bColor)
        {
            _myColor = bColor;
            _currPanel.BackColor = _myColor;
            _currTextBox.Text = ColorToRGB(_myColor).ToUpper();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _myColor = this._currPanel.BackColor;
            this.Hide();
            OnColorSelected(EventArgs.Empty);
        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Hide();
            OnColorSelected(EventArgs.Empty);
        }
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.Hide();
            OnColorSelected(EventArgs.Empty);
        }
        protected override void OnLoad(EventArgs e)
        {
            this._currTextBox.Focus();
            this._currTextBox.SelectAll();

            base.OnLoad(e);
        }

        public event EventHandler ColorSelected;
        protected void OnColorSelected(EventArgs e)
        {
            if (ColorSelected != null)
            {
                ColorSelected(this, e);
            }
        }
    }
}

