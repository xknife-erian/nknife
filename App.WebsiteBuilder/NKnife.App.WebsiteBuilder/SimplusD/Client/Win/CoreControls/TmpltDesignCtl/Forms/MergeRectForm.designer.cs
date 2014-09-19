using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    partial class MergeRectForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.OKBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.radioButtonFirst = new System.Windows.Forms.RadioButton();
            this.radioButtonSenond = new System.Windows.Forms.RadioButton();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.selectPanel = new System.Windows.Forms.UserControl();
            this.labelSelect = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKBtn
            // 
            this.OKBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.Location = new System.Drawing.Point(121, 139);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(51, 25);
            this.OKBtn.TabIndex = 0;
            this.OKBtn.Text = "确 定";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(178, 139);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(59, 25);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "取 消";
            // 
            // radioButtonFirst
            // 
            this.radioButtonFirst.AutoSize = true;
            this.radioButtonFirst.Location = new System.Drawing.Point(16, 24);
            this.radioButtonFirst.Name = "radioButtonFirst";
            this.radioButtonFirst.Size = new System.Drawing.Size(95, 16);
            this.radioButtonFirst.TabIndex = 2;
            this.radioButtonFirst.TabStop = true;
            this.radioButtonFirst.Text = "radioButton1";
            this.radioButtonFirst.UseVisualStyleBackColor = true;
            // 
            // radioButtonSenond
            // 
            this.radioButtonSenond.AutoSize = true;
            this.radioButtonSenond.Location = new System.Drawing.Point(16, 47);
            this.radioButtonSenond.Name = "radioButtonSenond";
            this.radioButtonSenond.Size = new System.Drawing.Size(95, 16);
            this.radioButtonSenond.TabIndex = 3;
            this.radioButtonSenond.TabStop = true;
            this.radioButtonSenond.Text = "radioButton2";
            this.radioButtonSenond.UseVisualStyleBackColor = true;
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.radioButtonFirst);
            this.groupBox.Controls.Add(this.radioButtonSenond);
            this.groupBox.Location = new System.Drawing.Point(5, 1);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(241, 78);
            this.groupBox.TabIndex = 4;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "选择保留的区域";
            // 
            // selectPanel
            // 
            this.selectPanel.BackColor = System.Drawing.SystemColors.Info;
            this.selectPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.selectPanel.Location = new System.Drawing.Point(12, 25);
            this.selectPanel.Name = "selectPanel";
            this.selectPanel.Size = new System.Drawing.Size(225, 100);
            this.selectPanel.TabIndex = 0;
            // 
            // labelSelect
            // 
            this.labelSelect.AutoSize = true;
            this.labelSelect.Location = new System.Drawing.Point(12, 9);
            this.labelSelect.Name = "labelSelect";
            this.labelSelect.Size = new System.Drawing.Size(40, 13);
            this.labelSelect.TabIndex = 2;
            this.labelSelect.Text = "Select:";
            // 
            // MergeRectForm
            // 
            this.AcceptButton = this.OKBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(249, 173);
            this.Controls.Add(this.labelSelect);
            this.Controls.Add(this.selectPanel);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.cancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MergeRectForm";
            this.Text = "选择要保留的区域";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button cancelBtn;
        
        private RadioButton radioButtonFirst;
        private RadioButton radioButtonSenond;
        private GroupBox groupBox;
        private UserControl selectPanel;
        private Label labelSelect;
    }
}