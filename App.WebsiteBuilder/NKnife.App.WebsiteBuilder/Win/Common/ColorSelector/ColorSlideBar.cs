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
    internal partial class ColorSlideBar : ColorBaseControl
    {
        float _colorWidthRatio = 0.75f;
        Color _slideBarColor = Color.Black;
        Brush _slideBrush;

        private ColorSelector _ownerColorSelector;
        public ColorSelector OwnerColorSelector
        {
            get { return _ownerColorSelector; }
            set
            {
                _ownerColorSelector = value;

                if (value != null)
                {
                    OwnerColorSelector.BaseValueChanged += new EventHandler(OwnerColorSelector_BaseValueChanged);
                }
            }
        }

        void OwnerColorSelector_BaseValueChanged(object sender, EventArgs e)
        {
            Graphics g = CreateGraphics();
            DrawColorBlock(g);
            g.Dispose();

            OwnerColorSelector.TempValue = SelectedColor;
        }

        int ColorWidth
        {
            get
            {
                return (int)Math.Round(_colorWidthRatio * this.Width);
            }
        }

        int BarWidth
        {
            get
            {
                return (int)Math.Round((1 - _colorWidthRatio) * this.Width);
            }
        }

        /// <summary>
        /// 选择的颜色值
        /// </summary>
        public Color SelectedColor
        {
            get
            {
                if (DesignMode)
                {
                    return Color.Blue;
                }
                return GetColor(_slideBarValue);
            }
        }

        private int _slideBarValue = 50;

        public ColorSlideBar()
        {
            _slideBrush = new SolidBrush(_slideBarColor);
        }

        /// <summary>
        /// 通过滑块的值获取Color值
        /// </summary>
        private Color GetColor(int barValue)
        {
            int my = this.Height / 2;
            if (barValue == my)
            {
                return OwnerColorSelector.BaseValue;
            }
            else if (barValue < my)
            {
                float depth = ((float)barValue) / my;
                return CommonUtility.ApplyWhiteDepth(OwnerColorSelector.BaseValue, depth);
            }
            else
            {
                float depth = ((float)(barValue - my)) / my;
                //MessageBox.Show(string.Format("((float)(barValue - my)) / my:((({0} - {1})) / {1})"
                //    ,barValue,my));
                return CommonUtility.ApplyBlackDepth(OwnerColorSelector.BaseValue, depth);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;

            ///画颜色块
            DrawColorBlock(g);

            ///画滑块
            DrawSlideBar(g, _slideBrush);

            base.OnPaint(pe);
        }

        private void DrawColorBlock(Graphics g)
        {
            for (int y = 0; y < this.Height; y++)
            {
                g.DrawLine(new Pen(GetColor(y)), 0, y, ColorWidth, y);
            }
        }

        private void DrawSlideBar(Graphics g, Brush brush)
        {
            g.FillPolygon(brush, new Point[]{new Point(ColorWidth+1, _slideBarValue),
                new Point(this.Width,_slideBarValue-(this.BarWidth)),
                new Point(this.Width,_slideBarValue+(this.BarWidth))});
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StartMoveBar();

                MoveBar(e.Y);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MoveBar(e.Y);
            }

            base.OnMouseMove(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            ///todo:打算按esc键取消更改，但未生效
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (this.Capture)
            //    {
            //        _ownerColorSelector.BottomColorPad.NewColor = _ownerColorSelector.BottomColorPad.OldColor;
            //        this.Capture = false;
            //        Cursor.Clip = Rectangle.Empty;
            //    }
            //}

            base.OnKeyDown(e);
        }

        bool _moving = false;
        private void StartMoveBar()
        {
            _moving = true;
            Cursor.Hide();
            Cursor.Clip = this.RectangleToScreen(this.ClientRectangle);
        }
        private void MoveBar(int y)
        {
            if (_moving)
            {
                Graphics g = this.CreateGraphics();
                Brush brush = new SolidBrush(this.BackColor);

                DrawSlideBar(g, brush);

                _slideBarValue = y;

                DrawSlideBar(g, _slideBrush);

                ///更新TempValue
                OwnerColorSelector.TempValue = SelectedColor;
            }
        }

        private void EndMoveBar()
        {
            if (!_moving)
            {
                return;
            }
            _moving = false;

            Cursor.Show();
            Cursor.Clip = Rectangle.Empty;

            ///更新Value
            OwnerColorSelector.Value = SelectedColor;
        }

        protected override void OnLostCapture(EventArgs e)
        {
            EndMoveBar();

            base.OnLostCapture(e);
        }
    }
}