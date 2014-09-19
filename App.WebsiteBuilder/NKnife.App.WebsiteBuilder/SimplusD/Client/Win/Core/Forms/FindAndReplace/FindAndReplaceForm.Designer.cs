namespace Jeelu.SimplusD.Client.Win
{
    partial class FindAndReplaceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindAndReplaceForm));
            this.toolStripSearchAndReplace = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonReplace = new System.Windows.Forms.ToolStripButton();
            this.labelSearchContent = new System.Windows.Forms.Label();
            this.textBoxSearchContent = new System.Windows.Forms.TextBox();
            this.textBoxReplaceContent = new System.Windows.Forms.TextBox();
            this.labelReplaceContent = new System.Windows.Forms.Label();
            this.labelSearchScope = new System.Windows.Forms.Label();
            this.comboBoxSearchScope = new System.Windows.Forms.ComboBox();
            this.buttonSearchOption = new System.Windows.Forms.Button();
            this.labelSearchOption = new System.Windows.Forms.Label();
            this.LineSearchOption = new Jeelu.Win.SingleLine();
            this.groupBoxSearchOption = new System.Windows.Forms.GroupBox();
            this.comboBoxSearchType = new System.Windows.Forms.ComboBox();
            this.checkBoxUsing = new System.Windows.Forms.CheckBox();
            this.checkBoxUpWard = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchWholeWord = new System.Windows.Forms.CheckBox();
            this.checkBoxMatchCase = new System.Windows.Forms.CheckBox();
            this.buttonSearchNext = new System.Windows.Forms.Button();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.buttonReplaceAll = new System.Windows.Forms.Button();
            this.buttonSearchAll = new System.Windows.Forms.Button();
            this.comboBoxSearchTarget = new System.Windows.Forms.ComboBox();
            this.labelSearchTarget = new System.Windows.Forms.Label();
            this.buttenUsing = new System.Windows.Forms.Button();
            this.toolStripSearchAndReplace.SuspendLayout();
            this.groupBoxSearchOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSearchAndReplace
            // 
            this.toolStripSearchAndReplace.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripSearchAndReplace.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSearch,
            this.toolStripSeparator1,
            this.toolStripButtonReplace});
            this.toolStripSearchAndReplace.Location = new System.Drawing.Point(0, 0);
            this.toolStripSearchAndReplace.Name = "toolStripSearchAndReplace";
            this.toolStripSearchAndReplace.Size = new System.Drawing.Size(269, 25);
            this.toolStripSearchAndReplace.TabIndex = 0;
            this.toolStripSearchAndReplace.Text = "toolStrip1";
            // 
            // toolStripButtonSearch
            // 
            this.toolStripButtonSearch.Checked = true;
            this.toolStripButtonSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSearch.Image")));
            this.toolStripButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearch.Name = "toolStripButtonSearch";
            this.toolStripButtonSearch.Size = new System.Drawing.Size(73, 22);
            this.toolStripButtonSearch.Text = "快速查找";
            this.toolStripButtonSearch.Click += new System.EventHandler(this.toolStripButtonSearch_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonReplace
            // 
            this.toolStripButtonReplace.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonReplace.Image")));
            this.toolStripButtonReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReplace.Name = "toolStripButtonReplace";
            this.toolStripButtonReplace.Size = new System.Drawing.Size(73, 22);
            this.toolStripButtonReplace.Text = "快速替换";
            this.toolStripButtonReplace.Click += new System.EventHandler(this.toolStripButtonReplace_Click);
            // 
            // labelSearchContent
            // 
            this.labelSearchContent.AutoSize = true;
            this.labelSearchContent.Location = new System.Drawing.Point(12, 31);
            this.labelSearchContent.Name = "labelSearchContent";
            this.labelSearchContent.Size = new System.Drawing.Size(78, 13);
            this.labelSearchContent.TabIndex = 2;
            this.labelSearchContent.Text = "searchContent";
            // 
            // textBoxSearchContent
            // 
            this.textBoxSearchContent.Location = new System.Drawing.Point(12, 47);
            this.textBoxSearchContent.Name = "textBoxSearchContent";
            this.textBoxSearchContent.Size = new System.Drawing.Size(219, 21);
            this.textBoxSearchContent.TabIndex = 3;
            this.textBoxSearchContent.TextChanged += new System.EventHandler(this.textBoxSearchContent_TextChanged);
            // 
            // textBoxReplaceContent
            // 
            this.textBoxReplaceContent.Location = new System.Drawing.Point(12, 87);
            this.textBoxReplaceContent.Name = "textBoxReplaceContent";
            this.textBoxReplaceContent.Size = new System.Drawing.Size(245, 21);
            this.textBoxReplaceContent.TabIndex = 5;
            this.textBoxReplaceContent.Visible = false;
            this.textBoxReplaceContent.TextChanged += new System.EventHandler(this.textBoxReplaceContent_TextChanged);
            // 
            // labelReplaceContent
            // 
            this.labelReplaceContent.AutoSize = true;
            this.labelReplaceContent.Location = new System.Drawing.Point(12, 71);
            this.labelReplaceContent.Name = "labelReplaceContent";
            this.labelReplaceContent.Size = new System.Drawing.Size(81, 13);
            this.labelReplaceContent.TabIndex = 4;
            this.labelReplaceContent.Text = "replaceContent";
            this.labelReplaceContent.Visible = false;
            // 
            // labelSearchScope
            // 
            this.labelSearchScope.AutoSize = true;
            this.labelSearchScope.Location = new System.Drawing.Point(12, 111);
            this.labelSearchScope.Name = "labelSearchScope";
            this.labelSearchScope.Size = new System.Drawing.Size(68, 13);
            this.labelSearchScope.TabIndex = 6;
            this.labelSearchScope.Text = "searchScope";
            // 
            // comboBoxSearchScope
            // 
            this.comboBoxSearchScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchScope.FormattingEnabled = true;
            this.comboBoxSearchScope.Location = new System.Drawing.Point(12, 127);
            this.comboBoxSearchScope.Name = "comboBoxSearchScope";
            this.comboBoxSearchScope.Size = new System.Drawing.Size(245, 21);
            this.comboBoxSearchScope.TabIndex = 7;
            this.comboBoxSearchScope.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchScope_SelectedIndexChanged);
            // 
            // buttonSearchOption
            // 
            this.buttonSearchOption.Location = new System.Drawing.Point(10, 197);
            this.buttonSearchOption.Name = "buttonSearchOption";
            this.buttonSearchOption.Size = new System.Drawing.Size(15, 15);
            this.buttonSearchOption.TabIndex = 8;
            this.buttonSearchOption.UseVisualStyleBackColor = true;
            this.buttonSearchOption.Click += new System.EventHandler(this.buttonSearchOption_Click);
            // 
            // labelSearchOption
            // 
            this.labelSearchOption.AutoSize = true;
            this.labelSearchOption.Font = new System.Drawing.Font("宋体", 9F);
            this.labelSearchOption.Location = new System.Drawing.Point(30, 198);
            this.labelSearchOption.Name = "labelSearchOption";
            this.labelSearchOption.Size = new System.Drawing.Size(77, 12);
            this.labelSearchOption.TabIndex = 9;
            this.labelSearchOption.Text = "searchOption";
            this.labelSearchOption.Visible = false;
            // 
            // LineSearchOption
            // 
            this.LineSearchOption.LineStyle = Jeelu.Win.LineStyle.Standard;
            this.LineSearchOption.Location = new System.Drawing.Point(110, 203);
            this.LineSearchOption.Name = "LineSearchOption";
            this.LineSearchOption.Size = new System.Drawing.Size(145, 2);
            this.LineSearchOption.TabIndex = 10;
            this.LineSearchOption.TabStop = false;
            this.LineSearchOption.Visible = false;
            // 
            // groupBoxSearchOption
            // 
            this.groupBoxSearchOption.Controls.Add(this.comboBoxSearchType);
            this.groupBoxSearchOption.Controls.Add(this.checkBoxUsing);
            this.groupBoxSearchOption.Controls.Add(this.checkBoxUpWard);
            this.groupBoxSearchOption.Controls.Add(this.checkBoxMatchWholeWord);
            this.groupBoxSearchOption.Controls.Add(this.checkBoxMatchCase);
            this.groupBoxSearchOption.Location = new System.Drawing.Point(10, 197);
            this.groupBoxSearchOption.Name = "groupBoxSearchOption";
            this.groupBoxSearchOption.Size = new System.Drawing.Size(245, 123);
            this.groupBoxSearchOption.TabIndex = 11;
            this.groupBoxSearchOption.TabStop = false;
            this.groupBoxSearchOption.Text = "searchOption";
            // 
            // comboBoxSearchType
            // 
            this.comboBoxSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchType.Enabled = false;
            this.comboBoxSearchType.FormattingEnabled = true;
            this.comboBoxSearchType.Location = new System.Drawing.Point(17, 91);
            this.comboBoxSearchType.Name = "comboBoxSearchType";
            this.comboBoxSearchType.Size = new System.Drawing.Size(219, 21);
            this.comboBoxSearchType.TabIndex = 4;
            this.comboBoxSearchType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchType_SelectedIndexChanged);
            // 
            // checkBoxUsing
            // 
            this.checkBoxUsing.AutoSize = true;
            this.checkBoxUsing.Location = new System.Drawing.Point(6, 72);
            this.checkBoxUsing.Name = "checkBoxUsing";
            this.checkBoxUsing.Size = new System.Drawing.Size(52, 17);
            this.checkBoxUsing.TabIndex = 3;
            this.checkBoxUsing.Text = "Using";
            this.checkBoxUsing.UseVisualStyleBackColor = true;
            this.checkBoxUsing.CheckedChanged += new System.EventHandler(this.checkBoxUsing_CheckedChanged);
            // 
            // checkBoxUpWard
            // 
            this.checkBoxUpWard.AutoSize = true;
            this.checkBoxUpWard.Location = new System.Drawing.Point(6, 55);
            this.checkBoxUpWard.Name = "checkBoxUpWard";
            this.checkBoxUpWard.Size = new System.Drawing.Size(65, 17);
            this.checkBoxUpWard.TabIndex = 2;
            this.checkBoxUpWard.Text = "UpWard";
            this.checkBoxUpWard.UseVisualStyleBackColor = true;
            this.checkBoxUpWard.CheckedChanged += new System.EventHandler(this.checkBoxUpWard_CheckedChanged);
            // 
            // checkBoxMatchWholeWord
            // 
            this.checkBoxMatchWholeWord.AutoSize = true;
            this.checkBoxMatchWholeWord.Location = new System.Drawing.Point(6, 38);
            this.checkBoxMatchWholeWord.Name = "checkBoxMatchWholeWord";
            this.checkBoxMatchWholeWord.Size = new System.Drawing.Size(111, 17);
            this.checkBoxMatchWholeWord.TabIndex = 1;
            this.checkBoxMatchWholeWord.Text = "MatchWholeWord";
            this.checkBoxMatchWholeWord.UseVisualStyleBackColor = true;
            this.checkBoxMatchWholeWord.CheckedChanged += new System.EventHandler(this.checkBoxMatchWholeWord_CheckedChanged);
            // 
            // checkBoxMatchCase
            // 
            this.checkBoxMatchCase.AutoSize = true;
            this.checkBoxMatchCase.Location = new System.Drawing.Point(6, 21);
            this.checkBoxMatchCase.Name = "checkBoxMatchCase";
            this.checkBoxMatchCase.Size = new System.Drawing.Size(79, 17);
            this.checkBoxMatchCase.TabIndex = 0;
            this.checkBoxMatchCase.Text = "MatchCase";
            this.checkBoxMatchCase.UseVisualStyleBackColor = true;
            this.checkBoxMatchCase.CheckedChanged += new System.EventHandler(this.checkBoxMatchCase_CheckedChanged);
            // 
            // buttonSearchNext
            // 
            this.buttonSearchNext.Location = new System.Drawing.Point(65, 326);
            this.buttonSearchNext.Name = "buttonSearchNext";
            this.buttonSearchNext.Size = new System.Drawing.Size(93, 23);
            this.buttonSearchNext.TabIndex = 12;
            this.buttonSearchNext.Text = "searchNext";
            this.buttonSearchNext.UseVisualStyleBackColor = true;
            this.buttonSearchNext.Click += new System.EventHandler(this.buttonSearchNext_Click);
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(164, 326);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(93, 23);
            this.buttonReplace.TabIndex = 13;
            this.buttonReplace.Text = "replace";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // buttonReplaceAll
            // 
            this.buttonReplaceAll.Location = new System.Drawing.Point(164, 355);
            this.buttonReplaceAll.Name = "buttonReplaceAll";
            this.buttonReplaceAll.Size = new System.Drawing.Size(93, 23);
            this.buttonReplaceAll.TabIndex = 14;
            this.buttonReplaceAll.Text = "replaceAll";
            this.buttonReplaceAll.UseVisualStyleBackColor = true;
            this.buttonReplaceAll.Click += new System.EventHandler(this.buttonReplaceAll_Click);
            // 
            // buttonSearchAll
            // 
            this.buttonSearchAll.Location = new System.Drawing.Point(164, 326);
            this.buttonSearchAll.Name = "buttonSearchAll";
            this.buttonSearchAll.Size = new System.Drawing.Size(93, 23);
            this.buttonSearchAll.TabIndex = 15;
            this.buttonSearchAll.Text = "searchAll";
            this.buttonSearchAll.UseVisualStyleBackColor = true;
            this.buttonSearchAll.Click += new System.EventHandler(this.buttonSearchAll_Click);
            // 
            // comboBoxSearchTarget
            // 
            this.comboBoxSearchTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchTarget.FormattingEnabled = true;
            this.comboBoxSearchTarget.Location = new System.Drawing.Point(12, 168);
            this.comboBoxSearchTarget.Name = "comboBoxSearchTarget";
            this.comboBoxSearchTarget.Size = new System.Drawing.Size(245, 21);
            this.comboBoxSearchTarget.TabIndex = 17;
            this.comboBoxSearchTarget.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchTarget_SelectedIndexChanged);
            // 
            // labelSearchTarget
            // 
            this.labelSearchTarget.AutoSize = true;
            this.labelSearchTarget.Location = new System.Drawing.Point(12, 152);
            this.labelSearchTarget.Name = "labelSearchTarget";
            this.labelSearchTarget.Size = new System.Drawing.Size(71, 13);
            this.labelSearchTarget.TabIndex = 16;
            this.labelSearchTarget.Text = "searchTarget";
            // 
            // buttenUsing
            // 
            this.buttenUsing.Enabled = false;
            this.buttenUsing.Location = new System.Drawing.Point(237, 46);
            this.buttenUsing.Name = "buttenUsing";
            this.buttenUsing.Size = new System.Drawing.Size(20, 23);
            this.buttenUsing.TabIndex = 18;
            this.buttenUsing.Text = "正";
            this.buttenUsing.UseVisualStyleBackColor = true;
            // 
            // FindAndReplaceForm
            // 
            this.AcceptButton = this.buttonSearchNext;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(269, 384);
            this.Controls.Add(this.buttenUsing);
            this.Controls.Add(this.buttonSearchOption);
            this.Controls.Add(this.comboBoxSearchTarget);
            this.Controls.Add(this.labelSearchTarget);
            this.Controls.Add(this.buttonSearchAll);
            this.Controls.Add(this.buttonReplaceAll);
            this.Controls.Add(this.LineSearchOption);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.buttonSearchNext);
            this.Controls.Add(this.labelSearchOption);
            this.Controls.Add(this.comboBoxSearchScope);
            this.Controls.Add(this.labelSearchScope);
            this.Controls.Add(this.textBoxReplaceContent);
            this.Controls.Add(this.labelReplaceContent);
            this.Controls.Add(this.textBoxSearchContent);
            this.Controls.Add(this.labelSearchContent);
            this.Controls.Add(this.toolStripSearchAndReplace);
            this.Controls.Add(this.groupBoxSearchOption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "FindAndReplaceForm";
            this.Text = "FindAndReplaceForm";
            this.Load += new System.EventHandler(this.FindAndReplaceForm_Load);
            this.toolStripSearchAndReplace.ResumeLayout(false);
            this.toolStripSearchAndReplace.PerformLayout();
            this.groupBoxSearchOption.ResumeLayout(false);
            this.groupBoxSearchOption.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripSearchAndReplace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label labelSearchContent;
        private System.Windows.Forms.TextBox textBoxSearchContent;
        private System.Windows.Forms.TextBox textBoxReplaceContent;
        private System.Windows.Forms.Label labelReplaceContent;
        private System.Windows.Forms.Label labelSearchScope;
        private System.Windows.Forms.ComboBox comboBoxSearchScope;
        private System.Windows.Forms.Button buttonSearchOption;
        private System.Windows.Forms.Label labelSearchOption;
        private Jeelu.Win.SingleLine LineSearchOption;
        private System.Windows.Forms.GroupBox groupBoxSearchOption;
        private System.Windows.Forms.CheckBox checkBoxMatchCase;
        private System.Windows.Forms.CheckBox checkBoxMatchWholeWord;
        private System.Windows.Forms.CheckBox checkBoxUsing;
        private System.Windows.Forms.CheckBox checkBoxUpWard;
        private System.Windows.Forms.ComboBox comboBoxSearchType;
        private System.Windows.Forms.Button buttonSearchNext;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Button buttonReplaceAll;
        private System.Windows.Forms.Button buttonSearchAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonReplace;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearch;
        private System.Windows.Forms.ComboBox comboBoxSearchTarget;
        private System.Windows.Forms.Label labelSearchTarget;
        private System.Windows.Forms.Button buttenUsing;
    }
}