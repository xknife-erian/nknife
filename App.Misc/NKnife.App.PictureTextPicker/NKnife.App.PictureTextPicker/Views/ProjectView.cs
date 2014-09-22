using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class ProjectView :DockContent
    {
        private System.Windows.Forms.TreeView _TreeView1;

        public ProjectView()
        {
            InitializeComponent();
        }

        protected void InitializeComponent()
        {
            this._TreeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this._TreeView1.Location = new System.Drawing.Point(67, 133);
            this._TreeView1.Name = "_TreeView1";
            this._TreeView1.Size = new System.Drawing.Size(169, 186);
            this._TreeView1.TabIndex = 0;
            // 
            // ProjectView
            // 
            this.ClientSize = new System.Drawing.Size(378, 366);
            this.Controls.Add(this._TreeView1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ProjectView";
            this.ResumeLayout(false);
        }
    }
}
