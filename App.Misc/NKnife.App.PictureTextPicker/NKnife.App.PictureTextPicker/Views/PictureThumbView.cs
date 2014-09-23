using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class PictureThumbView : DockContent
    {
    
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PictureThumbView
            // 
            this.ClientSize = new System.Drawing.Size(351, 361);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PictureThumbView";
            this.Text = "图片资源管理器";
            this.ResumeLayout(false);

        }
    }
}
