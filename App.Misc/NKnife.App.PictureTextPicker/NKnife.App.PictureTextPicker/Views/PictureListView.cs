using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.App.PictureTextPicker.Common.Base;
using NKnife.Ioc;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class PictureListView :DockContent
    {
        private FlowLayoutPanel _PictureListFlowLayoutPanel;
        private IPictureList _PictureList = DI.Get<IPictureList>();

        public PictureListView()
        {
            InitializeComponent();

            _PictureListFlowLayoutPanel.WrapContents = true;
            _PictureList.PictureListChanged += PictureListChanged;
        }

        private void PictureListChanged(object sender, EventArgs eventArgs)
        {
            _PictureListFlowLayoutPanel.Controls.Clear();
            foreach (var path in _PictureList.FilePathList)
            {
                var picBox = new PictureBox
                {
                    ImageLocation = path, 
                    WaitOnLoad = false,
                    BackColor = Color.Black,
                };

                _PictureListFlowLayoutPanel.Controls.Add(picBox);
            }
            
        }

        protected void InitializeComponent()
        {
            this._PictureListFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // PictureListFlowLayoutPanel
            // 
            this._PictureListFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PictureListFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._PictureListFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._PictureListFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this._PictureListFlowLayoutPanel.Name = "_PictureListFlowLayoutPanel";
            this._PictureListFlowLayoutPanel.Size = new System.Drawing.Size(378, 366);
            this._PictureListFlowLayoutPanel.TabIndex = 0;
            // 
            // PictureListView
            // 
            this.ClientSize = new System.Drawing.Size(378, 366);
            this.Controls.Add(this._PictureListFlowLayoutPanel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PictureListView";
            this.Text = "图片列表";
            this.ResumeLayout(false);

        }
    }
}
