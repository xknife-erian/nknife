using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace NKnife.Win.Forms.IPAddressControl
{
    internal enum Direction
    {
        Forward,
        Reverse
    }

    internal enum Selection
    {
        None,
        All
    }

    internal enum Action
    {
        None,
        Trim,
        Home,
        End
    }

    internal class CedeFocusEventArgs : EventArgs
    {
        public int FieldIndex { get; set; }

        public Action Action { get; set; }

        public Direction Direction { get; set; }

        public Selection Selection { get; set; }
    }

    internal class TextChangedEventArgs : EventArgs
    {
        public int FieldIndex { get; set; }

        public string Text { get; set; }
    }

    internal class FieldControl : TextBox
    {
        #region Constructors

        public FieldControl()
        {
            BorderStyle = BorderStyle.None;
            MaxLength = 3;
            Size = MinimumSize;
            TabStop = false;
            TextAlign = HorizontalAlignment.Center;
        }

        #endregion //Constructors

        #region Public Constants

        public const byte MinimumValue = 0;
        public const byte MaximumValue = 255;

        #endregion // Public Constants

        #region Public Events

        public event EventHandler<CedeFocusEventArgs> CedeFocusEvent;
        public event EventHandler<TextChangedEventArgs> TextChangedEvent;

        #endregion // Public Events

        #region Public Properties

        public bool Blank => TextLength == 0;

        public int FieldIndex { get; set; } = -1;

        public override Size MinimumSize
        {
            get
            {
                var g = Graphics.FromHwnd(Handle);

                var minimumSize = TextRenderer.MeasureText(g,
                    "255", Font, Size,
                    _textFormatFlags);

                g.Dispose();

                return minimumSize;
            }
        }

        public byte RangeLower
        {
            get => _rangeLower;
            set
            {
                if (value < MinimumValue)
                    _rangeLower = MinimumValue;
                else if (value > _rangeUpper)
                    _rangeLower = _rangeUpper;
                else
                    _rangeLower = value;

                if (Value < _rangeLower)
                    Text = _rangeLower.ToString(CultureInfo.InvariantCulture);
            }
        }

        public byte RangeUpper
        {
            get => _rangeUpper;
            set
            {
                if (value < _rangeLower)
                    _rangeUpper = _rangeLower;
                else if (value > MaximumValue)
                    _rangeUpper = MaximumValue;
                else
                    _rangeUpper = value;

                if (Value > _rangeUpper)
                    Text = _rangeUpper.ToString(CultureInfo.InvariantCulture);
            }
        }

        public byte Value
        {
            get
            {
                byte result;

                if (!byte.TryParse(Text, out result))
                    result = RangeLower;

                return result;
            }
        }

        #endregion // Public Properties

        #region Public Methods

        public void TakeFocus(Action action)
        {
            Focus();

            switch (action)
            {
                case Action.Trim:

                    if (TextLength > 0)
                    {
                        var newLength = TextLength - 1;
                        Text = Text.Substring(0, newLength);
                    }

                    SelectionStart = TextLength;

                    return;

                case Action.Home:

                    SelectionStart = 0;
                    SelectionLength = 0;

                    return;

                case Action.End:

                    SelectionStart = TextLength;

                    return;
            }
        }

        public void TakeFocus(Direction direction, Selection selection)
        {
            Focus();

            if (selection == Selection.All)
            {
                SelectionStart = 0;
                SelectionLength = TextLength;
            }
            else
            {
                SelectionStart = direction == Direction.Forward ? 0 : TextLength;
            }
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        #endregion // Public Methods

        #region Protected Methods

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Home:
                    SendCedeFocusEvent(Action.Home);
                    return;

                case Keys.End:
                    SendCedeFocusEvent(Action.End);
                    return;
            }

            if (IsCedeFocusKey(e))
            {
                SendCedeFocusEvent(Direction.Forward, Selection.All);
                e.SuppressKeyPress = true;
            }
            else if (IsForwardKey(e))
            {
                if (e.Control)
                    SendCedeFocusEvent(Direction.Forward, Selection.All);
                else if (SelectionLength == 0 && SelectionStart == TextLength)
                    SendCedeFocusEvent(Direction.Forward, Selection.None);
            }
            else if (IsReverseKey(e))
            {
                if (e.Control)
                    SendCedeFocusEvent(Direction.Reverse, Selection.All);
                else if (SelectionLength == 0 && SelectionStart == 0)
                    SendCedeFocusEvent(Direction.Reverse, Selection.None);
            }
            else if (IsBackspaceKey(e))
            {
                HandleBackspaceKey(e);
            }
            else if (!IsNumericKey(e) &&
                     !IsEditKey(e) &&
                     !IsEnterKey(e))
            {
                e.SuppressKeyPress = true;
            }
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            BackColor = Parent.BackColor;
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

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (!Blank)
            {
                int value;
                if (!int.TryParse(Text, out value))
                {
                    Text = string.Empty;
                }
                else
                {
                    if (value > RangeUpper)
                    {
                        Text = RangeUpper.ToString(CultureInfo.InvariantCulture);
                        SelectionStart = 0;
                    }
                    else if (TextLength == MaxLength && value < RangeLower)
                    {
                        Text = RangeLower.ToString(CultureInfo.InvariantCulture);
                        SelectionStart = 0;
                    }
                    else
                    {
                        var originalLength = TextLength;
                        var newSelectionStart = SelectionStart;

                        Text = value.ToString(CultureInfo.InvariantCulture);

                        if (TextLength < originalLength)
                        {
                            newSelectionStart -= originalLength - TextLength;
                            SelectionStart = Math.Max(0, newSelectionStart);
                        }
                    }
                }
            }

            if (null != TextChangedEvent)
            {
                var args = new TextChangedEventArgs();
                args.FieldIndex = FieldIndex;
                args.Text = Text;
                TextChangedEvent(this, args);
            }

            if (TextLength == MaxLength && Focused && SelectionStart == TextLength)
                SendCedeFocusEvent(Direction.Forward, Selection.All);
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            if (!Blank)
                if (Value < RangeLower)
                    Text = RangeLower.ToString(CultureInfo.InvariantCulture);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x007b: // WM_CONTEXTMENU
                    return;
            }

            base.WndProc(ref m);
        }

        #endregion // Protected Methods

        #region Private Methods

        private void HandleBackspaceKey(KeyEventArgs e)
        {
            if (!ReadOnly && (TextLength == 0 || SelectionStart == 0 && SelectionLength == 0))
            {
                SendCedeFocusEvent(Action.Trim);
                e.SuppressKeyPress = true;
            }
        }

        private static bool IsBackspaceKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
                return true;

            return false;
        }

        private bool IsCedeFocusKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.OemPeriod ||
                e.KeyCode == Keys.Decimal ||
                e.KeyCode == Keys.Space)
                if (TextLength != 0 && SelectionLength == 0 && SelectionStart != 0)
                    return true;

            return false;
        }

        private static bool IsEditKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back ||
                e.KeyCode == Keys.Delete)
                return true;
            if (e.Modifiers == Keys.Control &&
                (e.KeyCode == Keys.C ||
                 e.KeyCode == Keys.V ||
                 e.KeyCode == Keys.X))
                return true;

            return false;
        }

        private static bool IsEnterKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Return)
                return true;

            return false;
        }

        private static bool IsForwardKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Down)
                return true;

            return false;
        }

        private static bool IsNumericKey(KeyEventArgs e)
        {
            if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
                    return false;

            return true;
        }

        private static bool IsReverseKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left ||
                e.KeyCode == Keys.Up)
                return true;

            return false;
        }

        private void SendCedeFocusEvent(Action action)
        {
            if (null != CedeFocusEvent)
            {
                var args = new CedeFocusEventArgs();
                args.FieldIndex = FieldIndex;
                args.Action = action;
                CedeFocusEvent(this, args);
            }
        }

        private void SendCedeFocusEvent(Direction direction, Selection selection)
        {
            if (null != CedeFocusEvent)
            {
                var args = new CedeFocusEventArgs();
                args.FieldIndex = FieldIndex;
                args.Action = Action.None;
                args.Direction = direction;
                args.Selection = selection;
                CedeFocusEvent(this, args);
            }
        }

        #endregion // Private Methods

        #region Private Data

        private byte _rangeLower; // = MinimumValue;  // this is removed for FxCop approval
        private byte _rangeUpper = MaximumValue;

        private readonly TextFormatFlags _textFormatFlags = TextFormatFlags.HorizontalCenter |
                                                            TextFormatFlags.SingleLine | TextFormatFlags.NoPadding;

        #endregion // Private Data
    }
}