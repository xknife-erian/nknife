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
            this.hRuler1 = new NKnife.Draws.Controls.HRuler();
            this.vRuler1 = new NKnife.Draws.Controls.VRuler();
            this.SuspendLayout();
            // 
            // hRuler1
            // 
            this.hRuler1.CurrentZoom = 1F;
            this.hRuler1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hRuler1.Location = new System.Drawing.Point(0, 457);
            this.hRuler1.Name = "hRuler1";
            this.hRuler1.RulerScaleStyle = NKnife.Draws.Common.RulerStyle.Pixel;
            this.hRuler1.Size = new System.Drawing.Size(620, 23);
            this.hRuler1.TabIndex = 0;
            this.hRuler1.Text = "hRuler1";
            // 
            // vRuler1
            // 
            this.vRuler1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vRuler1.Location = new System.Drawing.Point(620, 0);
            this.vRuler1.Name = "vRuler1";
            this.vRuler1.RulerStyle = NKnife.Draws.Common.RulerStyle.Pixel;
            this.vRuler1.Size = new System.Drawing.Size(20, 480);
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
            this.Name = "DesignBench";
            this.Size = new System.Drawing.Size(640, 480);
            this.ResumeLayout(false);

        }

        #endregion

        private HRuler hRuler1;
        private VRuler vRuler1;

    }
}
