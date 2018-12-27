using NKnife.ChannelKnife.Controls;

namespace NKnife.ChannelKnife.Views.Controls
{
    partial class ChannelDataListView
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._ListView = new NotFlickerListView();
            this.SuspendLayout();
            // 
            // _ListView
            // 
            this._ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ListView.Font = new System.Drawing.Font("Arial", 8.25F);
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.Location = new System.Drawing.Point(0, 0);
            this._ListView.MultiSelect = false;
            this._ListView.Name = "_ListView";
            this._ListView.ShowItemToolTips = true;
            this._ListView.Size = new System.Drawing.Size(652, 372);
            this._ListView.TabIndex = 0;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            // 
            // SerialDataListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ListView);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "SerialDataListView";
            this.Size = new System.Drawing.Size(652, 372);
            this.ResumeLayout(false);
        }

        #endregion

        private NotFlickerListView _ListView;
    }
}
