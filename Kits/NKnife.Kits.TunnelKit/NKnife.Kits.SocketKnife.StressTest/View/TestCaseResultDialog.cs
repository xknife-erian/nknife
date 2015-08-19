using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;

namespace NKnife.Kits.SocketKnife.StressTest.View
{
    public partial class TestCaseResultDialog : Form
    {
        private static ILog _logger = LogManager.GetLogger<TestCaseResultDialog>();
        public TestCaseResultDialog(string message)
        {
            InitializeComponent();
            MessageTextBox.Text = message;
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportButton_Click(object sender, EventArgs e)
        {
            _logger.Info(MessageTextBox.Text);
            MessageBox.Show("日志导入程序Log目录下report.log.txt文件");
        }
    }
}
