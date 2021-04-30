using System;
using System.Linq;
using System.Windows.Forms;
using NKnife.Util;
using NKnife.Win.Forms;

namespace NKnife.Win.Forms
{
    public partial class TextArrayDialog : SimpleForm
    {
        public TextArrayDialog()
        {
            InitializeComponent();
            _ListView.SelectedIndexChanged += ListView_SelectedIndexChanged;
            _CurrentItemValueTextBox.TextChanged += CurrentItemValueTextBox_TextChanged;
        }

        private void CurrentItemValueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_CurrentItemValueTextBox.Text) && _ListView.SelectedItems.Count > 0)
                _AgreeButton.Enabled = true;
            else
                _AgreeButton.Enabled = false;
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ListView.SelectedItems.Count > 0)
            {
                _DeleteButton.Enabled = true;
                var item = _ListView.SelectedItems[0];
                _CurrentItemValueTextBox.Text = item.SubItems[1].Text;
                var index = _ListView.SelectedIndices[0];
                if (_ListView.Items.Count <= 1)
                {
                    _UpButton.Enabled = false;
                    _DownButton.Enabled = false;
                }
                else
                {
                    if (index == 0)
                    {
                        _UpButton.Enabled = false;
                        _DownButton.Enabled = true;
                    }
                    else if (index == _ListView.Items.Count - 1)
                    {
                        _UpButton.Enabled = true;
                        _DownButton.Enabled = false;
                    }
                    else
                    {
                        _UpButton.Enabled = true;
                        _DownButton.Enabled = true;
                    }
                }
            }
            else
            {
                _CurrentItemValueTextBox.Clear();
                _DeleteButton.Enabled = false;
                _UpButton.Enabled = false;
                _DownButton.Enabled = false;
                _AgreeButton.Enabled = false;
            }
        }

        public string[] TextArray
        {
            get => (from ListViewItem item in _ListView.Items select item.SubItems[1].Text).ToArray();
            set
            {
                if (!UtilCollection.IsNullOrEmpty(value))
                {
                    _ListView.Items.Clear();
                    foreach (var text in value)
                    {
                        AddTextItem(text);
                    }
                }
            }
        }

        private ListViewItem AddTextItem(string text)
        {
            var item = new ListViewItem();
            item.Text = GetIndex();
            item.SubItems.Add(text);
            _ListView.Items.Add(item);
            return item;
        }

        private string GetIndex()
        {
            return (_ListView.Items.Count + 1).ToString();
        }

        private void UpdateIndex()
        {
            for (int i = 0; i < _ListView.Items.Count; i++)
                _ListView.Items[i].Text = (i + 1).ToString();
        }

        private void _AcceptButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void _AddButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem it in _ListView.Items)
                it.Selected = false;
            var item = AddTextItem($"新数据{GetIndex()}");
            item.Selected = true;
            _ListView.Focus();
        }

        private void _DeleteButton_Click(object sender, EventArgs e)
        {
            _CurrentItemValueTextBox.Clear();
            foreach (int index in _ListView.SelectedIndices)
                _ListView.Items.RemoveAt(index);
            UpdateIndex();
        }

        private void _UpButton_Click(object sender, EventArgs e)
        {
            var index = _ListView.SelectedIndices[0];
            var text = _ListView.SelectedItems[0].SubItems[1].Text;
            _ListView.Items.RemoveAt(index);
            var item = new ListViewItem();
            item.Text = GetIndex();
            item.SubItems.Add(text);
            _ListView.Items.Insert(index - 1, item);
            UpdateIndex();
            item.Selected = true;
            _ListView.Focus();
        }

        private void _DownButton_Click(object sender, EventArgs e)
        {
            var index = _ListView.SelectedIndices[0];
            var text = _ListView.SelectedItems[0].SubItems[1].Text;
            _ListView.Items.RemoveAt(index);
            var item = new ListViewItem();
            item.Text = GetIndex();
            item.SubItems.Add(text);
            _ListView.Items.Insert(index + 1, item);
            UpdateIndex();
            item.Selected = true;
            _ListView.Focus();
        }

        private void _AgreeButton_Click(object sender, EventArgs e)
        {
            _ListView.SelectedItems[0].SubItems[1].Text = _CurrentItemValueTextBox.Text;
            _ListView.Focus();
        }
    }
}
