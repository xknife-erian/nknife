using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Kits.StarterKit.Forms
{
    public partial class ThreadTimerTestForm : DockContent
    {
        private System.Threading.Timer _timer;

        public ThreadTimerTestForm()
        {
            InitializeComponent();
            _ClearLogButton.Click += (sender, args) => _ListBox.Items.Clear();
            _StartButton.Click += _StartButton_Click;
            _ChangeButton.Click += _ChangeButton_Click;
            _StopBbutton.Click += _StopBbutton_Click;
        }

        private void _StopBbutton_Click(object sender, EventArgs e)
        {
            _timer.Dispose();
            _StartButton.Enabled = true;
            _ChangeButton.Enabled = false;
            _StopBbutton.Enabled = false;
            _CheckBox.Enabled = true;
            _count = 0;
        }

        private void _ChangeButton_Click(object sender, EventArgs e)
        {
            var durTime = (int) _DuTimeNumericUpDown.Value;
            var period = (int) _PeriodNumericUpDown.Value;
            _timer.Change(durTime, period);
        }

        private void _StartButton_Click(object sender, EventArgs e)
        {
            var time = (int) _BaseNumericUpDown.Value;
            _timer = new System.Threading.Timer(CallBack, null, 0, time);
            _StartButton.Enabled = false;
            _ChangeButton.Enabled = true;
            _StopBbutton.Enabled = true;
            _CheckBox.Enabled = false;
        }

        private int _count = 0;
        private bool _isSmall = true;

        private void CallBack(object state)
        {
            _count++;
            Write($"######-{_count}");
            if (_CheckBox.Checked && _count % 5 == 0)
            {
                var time = (int) _BaseNumericUpDown.Value;
                if (_isSmall)
                    time = time / 4;
                _isSmall = !_isSmall;
                _timer.Change(0, time);
            }
            for (int i = 0; i < 5; i++)//测试当Change时，是否还会继续执行以下的代码
            {
                Write($"*-({i})-");
                Thread.Sleep(10);
            }
        }

        public void Write(string info)
        {
            _ListBox.ThreadSafeInvoke(() => { _ListBox.Items.Insert(0, $"{DateTime.Now:mm:ss.fff}  > {info}"); });
        }

        #region Overrides of Form

        /// <summary>引发 <see cref="E:System.Windows.Forms.Form.Closing" /> 事件。</summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.ComponentModel.CancelEventArgs" />。</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            _timer?.Dispose();
            base.OnClosing(e);
        }

        #endregion
    }
}