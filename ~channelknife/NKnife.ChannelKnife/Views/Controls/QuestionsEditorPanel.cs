using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using NKnife.ChannelKnife.ViewModel;
using NKnife.ChannelKnife.ViewModel.Common;

namespace NKnife.ChannelKnife.Views.Controls
{
    public sealed partial class QuestionsEditorPanel : UserControl
    {
        private static readonly ILog _logger = LogManager.GetLogger<QuestionsEditorPanel>();

        private bool _CurrentIsHex;

        private bool _IsSingleAsk;
        private QuestionsEditorPanelViewModel _ViewData;

        public QuestionsEditorPanel()
        {
            InitializeComponent();
            _SingleQuestionTextbox.BackColor = Color.PapayaWhip;
            ControlEventManage();
        }

        public QuestionsEditorPanelViewModel ViewData
        {
            get { return _ViewData; }
            set
            {
                if (value == null)
                    return;
                _ViewData = value;
                _ViewData.PropertyChanged += (sender, args) =>
                {
                    switch (args.PropertyName)
                    {
                        case nameof(_ViewData.SerialEnable):
                        {
                            if (!_SingleQuestionTextbox.IsEmptyText())
                                _SingleAskButton.Enabled = true;
                            break;
                        }
                    }
                };
            }
        }

        private void ControlEventManage()
        {
            _SingleQuestionTextbox.TextChanged +=
                (sender, args) => { _SingleAskButton.Enabled = (!_SingleQuestionTextbox.IsEmptyText()) && ViewData.SerialEnable; };
            _IsHexDisplayCheckBox.CheckedChanged += (sender, args) =>
            {
                _CurrentIsHex = _IsHexDisplayCheckBox.Checked;
                var text = _SingleQuestionTextbox.Text;
                if (!string.IsNullOrWhiteSpace(text))
                {
                    try
                    {
                        _SingleQuestionTextbox.Text = !_CurrentIsHex
                            ? Encoding.Default.GetString(text.ToBytes())
                            : Encoding.Default.GetBytes(text).ToHexString();
                    }
                    catch (Exception e)
                    {
                        _logger.Warn($"转换不成功:{text}", e);
                    }
                }
                _SingleQuestionTextbox.BackColor = !_CurrentIsHex ? Color.PapayaWhip : Color.LightCyan;
            };

            #region Single

            _IsSingleLoopCheckBox.CheckedChanged += (sender, args) =>
            {
                ViewData.SingleQuestions.IsLoop = _IsSingleLoopCheckBox.Checked;
                ViewData.SingleQuestions.Time = (uint) _LoopTimeNumericUpDown.Value;
            };
            _LoopTimeNumericUpDown.ValueChanged += (sender, args) => ViewData.SingleQuestions.Time = (uint) _LoopTimeNumericUpDown.Value;
            _SingleQuestionTextbox.TextChanged += (sender, args) =>
            {
                if (!_SingleQuestionTextbox.IsEmptyText())
                {
                    byte[] bs;
                    if (_CurrentIsHex)
                    {
                        try
                        {
                            bs = _SingleQuestionTextbox.Text.ToBytes();
                        }
                        catch (Exception)
                        {
                            bs = new byte[0];
                            MessageBox.Show(this, "请按照16进制的格式书写，位与位之间以空格间隔。\r\n例如：打算发送ABC，填写“41 42 43”。", "格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        bs = Encoding.Default.GetBytes(_SingleQuestionTextbox.Text);
                    }
                    if (bs != null && bs.Length > 0)
                    {
                        ViewData.SingleQuestions.Content = bs;
                    }
                }
            };
            _SingleAskButton.Click += (sender, args) =>
            {
                if (!_IsSingleLoopCheckBox.Checked)
                {
                    OnAsked(new AskEventArgs(ViewData));
                }
                else
                {
                    if (!_IsSingleAsk)
                    {
                        _MultitermPage.Enabled = false;
                        _UserPage.Enabled = false;
                        _IsSingleLoopCheckBox.Enabled = false;
                        _LoopTimeNumericUpDown.Enabled = false;
                        _SingleAskButton.Text = "停止";
                        _IsSingleAsk = true;
                        OnAsked(new AskEventArgs(ViewData));
                    }
                    else
                    {
                        _MultitermPage.Enabled = true;
                        _UserPage.Enabled = true;
                        _IsSingleLoopCheckBox.Enabled = true;
                        _LoopTimeNumericUpDown.Enabled = true;
                        _SingleAskButton.Text = "发送";
                        _IsSingleAsk = false;
                        OnEndAsked();
                    }
                }
            };

            #endregion
        }

        public event EventHandler EndAsked;

        private void OnEndAsked()
        {
            EndAsked?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<AskEventArgs> Asked;

        private void OnAsked(AskEventArgs e)
        {
            Asked?.Invoke(this, e);
        }
    }
}