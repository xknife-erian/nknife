using NKnife.Draws.Common;
using NKnife.Draws.Controls;

namespace NKnife.Draws
{
    partial class DesignBench
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._StartLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this._EndLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.hRuler1 = new HRuler();
            this.vRuler1 = new VRuler();
            this._StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _StatusStrip
            // 
            this._StatusStrip.BackColor = System.Drawing.SystemColors.Control;
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._StartLabel,
            this.toolStripStatusLabel2,
            this._EndLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 458);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(640, 22);
            this._StatusStrip.TabIndex = 2;
            this._StatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel1.Text = "Start";
            // 
            // _StartLabel
            // 
            this._StartLabel.AutoSize = false;
            this._StartLabel.Name = "_StartLabel";
            this._StartLabel.Size = new System.Drawing.Size(100, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(30, 17);
            this.toolStripStatusLabel2.Text = "End";
            // 
            // _EndLabel
            // 
            this._EndLabel.AutoSize = false;
            this._EndLabel.Name = "_EndLabel";
            this._EndLabel.Size = new System.Drawing.Size(100, 17);
            // 
            // hRuler1
            // 
            this.hRuler1.CurrentZoom = 1F;
            this.hRuler1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hRuler1.Location = new System.Drawing.Point(0, 435);
            this.hRuler1.Name = "hRuler1";
            this.hRuler1.RulerScaleStyle = RulerStyle.Pixel;
            this.hRuler1.Size = new System.Drawing.Size(620, 23);
            this.hRuler1.TabIndex = 0;
            this.hRuler1.Text = "hRuler1";
            // 
            // vRuler1
            // 
            this.vRuler1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vRuler1.Location = new System.Drawing.Point(620, 0);
            this.vRuler1.Name = "vRuler1";
            this.vRuler1.RulerStyle = RulerStyle.Pixel;
            this.vRuler1.Size = new System.Drawing.Size(20, 458);
            this.vRuler1.TabIndex = 1;
            this.vRuler1.Text = "vRuler1";
            // 
            // DesignBench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.hRuler1);
            this.Controls.Add(this.vRuler1);
            this.Controls.Add(this._StatusStrip);
            this.Name = "DesignBench";
            this.Size = new System.Drawing.Size(640, 480);
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HRuler hRuler1;
        private VRuler vRuler1;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _StartLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel _EndLabel;

    }
}
