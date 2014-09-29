using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class ProcessResultView : DockContent
    {
        public ProcessResultView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ProcessResultView
            // 
            this.ClientSize = new System.Drawing.Size(351, 361);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "ProcessResultView";
            this.Text = "数据列表";
            this.ResumeLayout(false);

        }
    }
}
