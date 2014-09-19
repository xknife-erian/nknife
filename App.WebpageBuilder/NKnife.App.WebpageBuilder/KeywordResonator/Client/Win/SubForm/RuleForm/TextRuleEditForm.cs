using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.KeywordResonator.Client
{
    public partial class TextRuleEditForm : Form
    {
        /// <summary>
        /// 是否为新建规则
        /// </summary>
        private bool _isNew;
        private string _startLabel;
        private string _endLabel;
        /// <summary>
        /// 截取字符的开始标签
        /// </summary>
        public string StartLabel 
        {
            get
            {
                return _startLabel;
            }
            set 
            {
                _startLabel = value;
            }
        }
        /// <summary>
        /// 截取字符的结束标签
        /// </summary>
        public string EndLabel
        {
            get
            {
                return _endLabel;
            }
            set
            {
                _endLabel = value;
            }
        }


        public TextRuleEditForm(bool isNew)
        {
            _isNew = isNew;
            InitializeComponent();
        }

        private void TextRuleEditForm_Load(object sender, EventArgs e)
        {
            if (!_isNew)
            {
                this.startLabelTextBox.Text = _startLabel;
                this.endLabelTextBox.Text = _endLabel;
            }
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            _startLabel = this.startLabelTextBox.Text;
            _endLabel = this.endLabelTextBox.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            _startLabel = "";
            _endLabel = "";
            this.DialogResult = DialogResult.Cancel;
        }


     
    }
}
