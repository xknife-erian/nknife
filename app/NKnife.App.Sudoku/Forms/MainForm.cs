using System;
using System.Drawing;
using System.Windows.Forms;
using NKnife.App.Sudoku.Common;
using NKnife.App.Sudoku.Common.Enum;
using NKnife.App.Sudoku.Controls;
using NKnife.App.Sudoku.Controls.TreeNode;
using NKnife.ShareResources;

namespace NKnife.App.Sudoku.Forms
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
    public partial class MainForm : System.Windows.Forms.Form
	{

        internal SudoExercise _CurrExercise;
        internal SudokuPanel _DoPanel;

		public MainForm()
		{
			InitializeComponent();
		}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _DoPanel = new SudokuPanel(new Size(this._SplitContainer.Panel1.Width, this._SplitContainer.Panel1.Height));
            _DoPanel.Dock = DockStyle.Fill;
            this._SplitContainer.Panel1.Controls.Add(_DoPanel);
        }

        #region 新建题目
        /// <summary>
        /// 新建题目的工具栏事件
        /// </summary>
        private void _NewExerciseToolStripButton_Click(object sender, EventArgs e)
        {
            this.NewExercise();
        }
        /// <summary>
        /// 新建题目的菜单事件
        /// </summary>
        private void _NewExerciseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.NewExercise();
        }
        /// <summary>
        /// 新建题目
        /// </summary>
        private void NewExercise()
        {
            NewExerciseForm form = new NewExerciseForm();
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                this._CurrExercise = form.Exercise;
                this._DoPanel.SetPanelValue(_CurrExercise.SingleExercise);
            }
            form.Dispose();
            form = null;
        }
        #endregion

        #region 打开题库的Xml文件

        private void _OpenDoXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenDoXml();
        }
        private void _OpenDoXmlToolStripButton_Click(object sender, EventArgs e)
        {
            this.OpenDoXml();
        }
        private void OpenDoXml()
        {
            string file = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "打开题库";
            dialog.Filter = FilterString.SimpleSudoku;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.FileName))
                {
                    return;
                }
                file = dialog.FileName;
            }
            SuDoHelper.Initialize(file);
            this.InitializeTree(SuDoHelper.DoXml);
        }

        private void InitializeTree(SuDoXml doXml)
        {
            SudoExercise[] cises = doXml.SelectExercises(SudoDifficulty.Easy);
            foreach (var item in cises)
            {
                ExerciseNode node = new ExerciseNode();
                node.Exercise = item;
                node.Text = item.SingleExercise;
                this._ExerciseTree.Nodes.Add(node);
            }
            
        }

        #endregion


	}
}
