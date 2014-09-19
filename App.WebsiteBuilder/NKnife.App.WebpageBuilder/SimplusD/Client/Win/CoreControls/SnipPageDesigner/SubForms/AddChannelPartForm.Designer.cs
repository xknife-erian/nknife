namespace Jeelu.SimplusD.Client.Win
{
    partial class AddChannelPartForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点3");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点4");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点7");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点8");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点9");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点10");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("节点11");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            this.treeViewChannels = new System.Windows.Forms.TreeView();
            this.labelSelectChannel = new System.Windows.Forms.Label();
            this.checkBoxUsedSeparator = new System.Windows.Forms.CheckBox();
            this.textBoxSeparator = new System.Windows.Forms.TextBox();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxWidth = new System.Windows.Forms.CheckBox();
            this.cssFieldUnitWidth = new Jeelu.Win.CssFieldUnit();
            this.SuspendLayout();
            // 
            // treeViewChannels
            // 
            this.treeViewChannels.CheckBoxes = true;
            this.treeViewChannels.Location = new System.Drawing.Point(12, 29);
            this.treeViewChannels.Name = "treeViewChannels";
            treeNode1.Name = "节点2";
            treeNode1.Text = "节点2";
            treeNode2.Name = "节点3";
            treeNode2.Text = "节点3";
            treeNode3.Name = "节点4";
            treeNode3.Text = "节点4";
            treeNode4.Name = "节点5";
            treeNode4.Text = "节点5";
            treeNode5.Name = "节点6";
            treeNode5.Text = "节点6";
            treeNode6.Name = "节点7";
            treeNode6.Text = "节点7";
            treeNode7.Name = "节点8";
            treeNode7.Text = "节点8";
            treeNode8.Name = "节点9";
            treeNode8.Text = "节点9";
            treeNode9.Name = "节点10";
            treeNode9.Text = "节点10";
            treeNode10.Name = "节点11";
            treeNode10.Text = "节点11";
            treeNode11.Name = "节点0";
            treeNode11.Text = "节点0";
            this.treeViewChannels.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11});
            this.treeViewChannels.ShowNodeToolTips = true;
            this.treeViewChannels.Size = new System.Drawing.Size(175, 200);
            this.treeViewChannels.TabIndex = 1;
            this.treeViewChannels.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewChannels_NodeMouseDoubleClick);
            this.treeViewChannels.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewChannels_AfterCheck);
            // 
            // labelSelectChannel
            // 
            this.labelSelectChannel.AutoSize = true;
            this.labelSelectChannel.Location = new System.Drawing.Point(12, 13);
            this.labelSelectChannel.Name = "labelSelectChannel";
            this.labelSelectChannel.Size = new System.Drawing.Size(83, 13);
            this.labelSelectChannel.TabIndex = 2;
            this.labelSelectChannel.Text = "selectChannels:";
            // 
            // checkBoxUsedSeparator
            // 
            this.checkBoxUsedSeparator.AutoSize = true;
            this.checkBoxUsedSeparator.Enabled = false;
            this.checkBoxUsedSeparator.Location = new System.Drawing.Point(12, 235);
            this.checkBoxUsedSeparator.Name = "checkBoxUsedSeparator";
            this.checkBoxUsedSeparator.Size = new System.Drawing.Size(99, 17);
            this.checkBoxUsedSeparator.TabIndex = 0;
            this.checkBoxUsedSeparator.Text = "usingSeparator";
            this.checkBoxUsedSeparator.UseVisualStyleBackColor = true;
            this.checkBoxUsedSeparator.CheckedChanged += new System.EventHandler(this.usedSeparator_CheckedChanged);
            // 
            // textBoxSeparator
            // 
            this.textBoxSeparator.Enabled = false;
            this.textBoxSeparator.Location = new System.Drawing.Point(120, 233);
            this.textBoxSeparator.Name = "textBoxSeparator";
            this.textBoxSeparator.Size = new System.Drawing.Size(67, 21);
            this.textBoxSeparator.TabIndex = 0;
            this.textBoxSeparator.Text = "|";
            this.textBoxSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxSeparator.TextChanged += new System.EventHandler(this.textBoxSeparator_TextChanged);
            // 
            // buttonEnter
            // 
            this.buttonEnter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonEnter.Location = new System.Drawing.Point(81, 310);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(50, 23);
            this.buttonEnter.TabIndex = 3;
            this.buttonEnter.Text = "enter";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(137, 310);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(50, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxWidth
            // 
            this.checkBoxWidth.AutoSize = true;
            this.checkBoxWidth.Checked = true;
            this.checkBoxWidth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWidth.Location = new System.Drawing.Point(12, 258);
            this.checkBoxWidth.Name = "checkBoxWidth";
            this.checkBoxWidth.Size = new System.Drawing.Size(134, 17);
            this.checkBoxWidth.TabIndex = 5;
            this.checkBoxWidth.Text = "选中的频道宽度相同";
            this.checkBoxWidth.UseVisualStyleBackColor = true;
            this.checkBoxWidth.CheckedChanged += new System.EventHandler(this.checkBoxWidth_CheckedChanged);
            // 
            // cssFieldUnitWidth
            // 
            this.cssFieldUnitWidth.FieldUnitType = Jeelu.Win.CssFieldUnitType.Part;
            this.cssFieldUnitWidth.Location = new System.Drawing.Point(15, 281);
            this.cssFieldUnitWidth.Name = "cssFieldUnitWidth";
            this.cssFieldUnitWidth.Size = new System.Drawing.Size(172, 23);
            this.cssFieldUnitWidth.TabIndex = 13;
            this.cssFieldUnitWidth.Value = "";
            this.cssFieldUnitWidth.ValueChanged += new System.EventHandler(this.cssFieldUnitWidth_ValueChanged);
            // 
            // EditChannelPartForm
            // 
            this.AcceptButton = this.buttonEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 337);
            this.Controls.Add(this.cssFieldUnitWidth);
            this.Controls.Add(this.checkBoxWidth);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.checkBoxUsedSeparator);
            this.Controls.Add(this.textBoxSeparator);
            this.Controls.Add(this.labelSelectChannel);
            this.Controls.Add(this.treeViewChannels);
            this.Name = "EditChannelPartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditChannelPartForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewChannels;
        private System.Windows.Forms.Label labelSelectChannel;
        private System.Windows.Forms.CheckBox checkBoxUsedSeparator;
        private System.Windows.Forms.TextBox textBoxSeparator;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxWidth;
        private Jeelu.Win.CssFieldUnit cssFieldUnitWidth;
    }
}