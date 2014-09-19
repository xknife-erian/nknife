using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Jeelu.Win
{
    public class ValidateTextBox : TextBox
    {
        string _oldText;
        int _selectionStart;
        int _selectionLength;
        bool _isValidating;

        private Regex _regex;
        public string RegexText
        {
            get { return Convert.ToString(_regex); }
            set { _regex = new Regex(value, RegexOptions.ExplicitCapture); }
        }

        private Regex _regexRuntime;
        public string RegexTextRuntime
        {
            get { return Convert.ToString(_regexRuntime); }
            set { _regexRuntime = new Regex(value, RegexOptions.ExplicitCapture); }
        }

        public ValidateTextBox()
        {
        }

        public bool IsValidate()
        {
            if (_regex == null)
            {
                throw new ArgumentNullException("RegexText", "没有初始化RegexText");
            }
            return _regex.IsMatch(Text);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
            if (_isValidating)
            {
                return;
            }
            ///用指定的正则验证
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
            if (Changed != null)
            {
                Changed(this, e);
            }
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
