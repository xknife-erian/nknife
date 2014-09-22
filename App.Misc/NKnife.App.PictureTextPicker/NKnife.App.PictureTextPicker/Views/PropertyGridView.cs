using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class PropertyGridView : DockContent
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PropertyGridView
            // 
            this.ClientSize = new System.Drawing.Size(330, 340);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PropertyGridView";
            this.ResumeLayout(false);

        }
    }
}
