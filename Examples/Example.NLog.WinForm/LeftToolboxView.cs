using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.NLog.WinForm.Example
{
    public partial class LeftToolboxView : DockContent
    {
        private readonly LeftToolboxViewViewModel _viewModel;

        public LeftToolboxView()
        {
            _viewModel = new LeftToolboxViewViewModel();
            InitializeComponent();
            SetButtonState(true);
            SetStopButtonState(false);
        }

        private void SetButtonState(bool enable)
        {
            _Func10Button.Enabled = enable;
            _Func11Button.Enabled = enable;
            _Func12Button.Enabled = enable;
            _Func1Button.Enabled = enable;
            _Func2Button.Enabled = enable;
            _GroupCountBox.Enabled = enable;
        }

        private void SetStopButtonState(bool enable)
        {
            _Stop1Button.Enabled = enable;
            _Stop2Button.Enabled = enable;
        }

        private void _Func10Button_Click(object sender, EventArgs e)
        {
            _viewModel.BuildSingle();
        }

        private void _Func11Button_Click(object sender, EventArgs e)
        {
            _viewModel.BuildOneGroup();
        }

        private void _Func12Button_Click(object sender, EventArgs e)
        {
            _viewModel.BuildGroups((int)_GroupCountBox.Value);
        }

        private void _Func1Button_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("测试时，因速度较快，请将所有日志窗体仅保留一个，其余关闭后再启动。\r\n是否已经关闭？", "准备", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.No)
                return;
            SetButtonState(false);
            SetStopButtonState(false);
            _Stop1Button.Enabled = true;
            _Stop1Button.Focus();
            _viewModel.BuildLoop1Millisecond1Log();
        }

        private void _Stop1Button_Click(object sender, EventArgs e)
        {
            SetButtonState(true);
            SetStopButtonState(false);
            _Func1Button.Focus();
            _viewModel.StopLoop1Millisecond1Log();
        }

        private void _Func2Button_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("测试时，因速度较快，请将所有日志窗体仅保留一个，其余关闭后再启动。\r\n是否已经关闭？", "准备", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (r == DialogResult.No)
                return;
            SetButtonState(false);
            SetStopButtonState(false);
            _Stop2Button.Enabled = true;
            _Stop2Button.Focus();
            _viewModel.BuildLoop1Millisecond20GroupLog();
        }

        private void _Stop2Button_Click(object sender, EventArgs e)
        {
            SetButtonState(true);
            SetStopButtonState(false);
            _Func2Button.Focus();
            _viewModel.StopLoop1Millisecond20GroupLog();
        }

    }
}
