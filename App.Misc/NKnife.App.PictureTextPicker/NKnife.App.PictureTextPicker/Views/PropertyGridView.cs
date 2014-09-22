using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class PropertyGridView : DockContent
    {
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    
        private void InitializeComponent()
        {
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(330, 340);
            this.propertyGrid1.TabIndex = 0;
            // 
            // PropertyGridView
            // 
            this.ClientSize = new System.Drawing.Size(330, 340);
            this.Controls.Add(this.propertyGrid1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PropertyGridView";
            this.ResumeLayout(false);

        }
    }
}
