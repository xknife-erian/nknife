using System;
using System.Drawing;
using System.Windows.Forms;

namespace NKnife.Win.Forms.IPAddressControl
{
    internal class DotControl : Control
    {
        #region Constructors

        public DotControl()
        {
            Text = ".";

            BackColor = SystemColors.Window;
            Size = MinimumSize;
            TabStop = false;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        #endregion // Constructors

        #region Public Methods

        public override string ToString()
        {
            return Text;
        }

        #endregion // Public Methods

        #region Public Properties

        public override Size MinimumSize
        {
            get
            {
                var g = Graphics.FromHwnd(Handle);

                var minimumSize = TextRenderer.MeasureText(g,
                    Text, Font, Size,
                    _textFormatFlags);

                g.Dispose();

                return minimumSize;
            }
        }

        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;
                Invalidate();
            }
        }

        #endregion // Public Properties

        #region Protected Methods

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Size = MinimumSize;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var backColor = BackColor;

            if (!_backColorChanged)
                if (!Enabled || ReadOnly)
                    backColor = SystemColors.Control;

            var textColor = ForeColor;

            if (!Enabled)
                textColor = SystemColors.GrayText;
            else if (ReadOnly)
                if (!_backColorChanged)
                    textColor = SystemColors.WindowText;

            using (var backgroundBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, ClientRectangle);
            }

            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle,
                textColor, _textFormatFlags);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            BackColor = Parent.BackColor;
            _backColorChanged = true;
        }

        protected override void OnParentForeColorChanged(EventArgs e)
        {
            base.OnParentForeColorChanged(e);
            ForeColor = Parent.ForeColor;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Size = MinimumSize;
        }

        #endregion // Protected Methods

        #region Private Data

        private bool _backColorChanged;
        private bool _readOnly;

        private readonly TextFormatFlags _textFormatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPrefix |
                                                            TextFormatFlags.SingleLine | TextFormatFlags.NoPadding;

        #endregion // Private Data
    }
}