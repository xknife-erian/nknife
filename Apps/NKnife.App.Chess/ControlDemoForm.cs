using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Gean.Module.Chess;
using NKnife.Chesses.Common.Record;
using NKnife.Chesses.Common.Record.PGN;
using NKnife.Interface;

namespace Gean.Gui.ChessControl.Demo
{
    public partial class DemoMainForm : Form
    {
        //private FormWindowState IsShangBan { get { return Program.IsShangBan; } }
        #region private
        private string PGNFile { get { return Program.PGNFile_Test_2_Game; } } 
        private string _demoFile = Path.GetDirectoryName(@"..\..\DemoFile\");
        private Board _board = new Board();
        private Records _records = new Records();
        private RecordPlayToolStrip _playStrip = new RecordPlayToolStrip();
        private StepsPanel _stepsPanel = new StepsPanel();
        #endregion

        public DemoMainForm()
        {
            this.InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            this._label.Text = "OK";
            this._board.Dock = DockStyle.Fill;
            this._board.BringToFront();
            this._splitContainer3.Panel1.Controls.Add(_board);
            this._stripContainer.TopToolStripPanel.Controls.Add(_playStrip);
            this._stripContainer.TopToolStripPanel.Controls.Add(_mainMenuStrip);
            this._stepsPanel.Dock = DockStyle.Fill;
            this._stepsPanel.BackColor = Color.Plum;
            this._stepsTabPage.Controls.Add(_stepsPanel);

            this._recordListView.SelectedIndexChanged += new EventHandler(ViewStepCollection);
            this._board.PlayEvent += new Board.PlayEventHandler(WhilePlayed);
        }

        //F4：新建游戏
        private void NewGame(object sender, EventArgs e)
        {
            this._board.LoadSituation();
            this.WhilePlayed(null, null);
        }

        //F5：针对PGN文件进行转换与解析
        private void PGNConvent(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            PgnReader reader = new PgnReader();
            reader.Filename = Path.GetFullPath(Path.Combine(_demoFile, PGNFile)); 
            reader.AddEvents(_records);
            reader.Parse();
            string s = _records[0].ToString();

            this._recordListView.Items.Clear();
            foreach (var item in _records)
            {
                this._recordListView.Add(item);
            }
            this._label.Text = $"[Count: {_records.Count} record].";
            this.Cursor = Cursors.Default;
        }

        //当下棋后发生
        private void WhilePlayed(object sender, Board.PlayEventArgs e)
        {
            this._fenTextBox.Text = _board.Game.ToString() + "\r\n" + _board.Game.Generator() + "\r\n";// +e.GetHashCode();
        }

        //
        private void ViewStepCollection(object sender, EventArgs e)
        {
            if (this._recordListView.SelectedRecord == null || this._recordListView.SelectedRecord.Length == 0) return;
            Record record = this._recordListView.SelectedRecord[0];
            _stepsPanel.Controls.Clear();
            for (int i = 0; i < record.Items.Count; i++)
            {
                if (record.Items[i] is IGenerator)
                {
                    _stepsPanel.Add(record.Items[i] as IGenerator);
                }
            }
        }
    }
}

/* 点击Record的ListView，在树上显示Record
private void SelectedRecord(object sender, EventArgs e)
{
    if (this._recordListView.SelectedRecord == null || this._recordListView.SelectedRecord.Length == 0)
    {
        return;
    }
    _recordTree.Nodes.Clear();
    Record record = this._recordListView.SelectedRecord[0];
    TreeNode node = new TreeNode("Game");

    this.MarkNode(record, node, new StringBuilder());

    _recordTree.BeginUpdate();
    _recordTree.Nodes.Add(node);
    _recordTree.ShowLines = true;
    _recordTree.ExpandAll();
    _recordTree.EndUpdate();
}
private void MarkNode(ITree tree, TreeNode node, StringBuilder text)
{
    foreach (IItem item in tree.Items)
    {
        TreeNode subnode = new TreeNode();
        if (item is Step)
        {
            Step step = (Step)item;
            if (step.GameSide == Enums.GameSide.White)
            {
                text = new StringBuilder();
                text.Append(item.ItemType).Append(": ").Append(step.Generator());
            }
            if (step.GameSide == Enums.GameSide.Black)
            {
                text.Append(' ').Append(step.Generator());
                subnode.Text = text.ToString();
                node.Nodes.Add(subnode);
            }
            if (item is ITree)
            {
                ITree subtree = (ITree)item;
                if (subtree.HasChildren)
                {
                    MarkNode(subtree, subnode, new StringBuilder());
                }
            }
        }
        else
        {
            text = new StringBuilder();
            text.Append(item.ItemType).Append(": ").Append(item.Value);
            subnode.Text = text.ToString();
            node.Nodes.Add(subnode);
        }
    }
}
*/
