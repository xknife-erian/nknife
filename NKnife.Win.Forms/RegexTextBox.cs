using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NKnife.Win.Forms
{
    /// <summary>
    ///     一个实现用正则校验的TextBox
    /// </summary>
    public class RegexTextBox : TextBox
    {
        private bool _isValidating;
        private string _oldText;
        private Regex _regex;
        private Regex _regexRuntime;
        private int _selectionLength;
        private int _selectionStart;

        public string RegexText
        {
            get => Convert.ToString(_regex);
            set => _regex = new Regex(value, RegexOptions.ExplicitCapture);
        }

        public string RegexTextRuntime
        {
            get => Convert.ToString(_regexRuntime);
            set => _regexRuntime = new Regex(value, RegexOptions.ExplicitCapture);
        }

        public bool IsValidate()
        {
            if (_regex == null) throw new ArgumentException();
            return _regex.IsMatch(Text);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
            if (_isValidating) 
                return;
            //用指定的正则验证
            if (_regexRuntime != null)
            {
                if (_regexRuntime.IsMatch(Text))
                {
                    _oldText = Text;
                    _selectionStart = SelectionStart;
                    _selectionLength = SelectionLength;
                }
                else
                {
                    _isValidating = true;
                    Text = _oldText;
                    _isValidating = false;
                    SelectionStart = _selectionStart;
                    SelectionLength = _selectionLength;
                }
            }

            base.OnTextChanged(e);
        }

        public event EventHandler Changed;

        protected virtual void OnCheckChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            _selectionStart = SelectionStart;
            _selectionLength = SelectionLength;
            base.OnKeyDown(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _selectionStart = SelectionStart;
            _selectionLength = SelectionLength;
            base.OnMouseDown(e);
        }
    }
}