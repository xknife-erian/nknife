using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using NKnife.App.PictureTextPicker.Common.Base;
using NKnife.App.PictureTextPicker.Common.Base.Res;
using NKnife.Ioc;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.App.PictureTextPicker.Views
{
    public class PropertyGridView : DockContent
    {
        private System.Windows.Forms.PropertyGrid _DocumentPropertyGrid;
        private readonly IPictureList _PictureList = DI.Get<IPictureList>();
        public PropertyGridView()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(OwnResources.icon_property.GetHicon());
            _PictureList.PictureSelected += (o, s) =>
            {
                _DocumentPropertyGrid.SelectedObject = _PictureList.ActiveDocument;
            };
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }


        private void InitializeComponent()
        {
            this._DocumentPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // DocumentPropertyGrid
            // 
            this._DocumentPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._DocumentPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this._DocumentPropertyGrid.Name = "_DocumentPropertyGrid";
            this._DocumentPropertyGrid.Size = new System.Drawing.Size(330, 381);
            this._DocumentPropertyGrid.TabIndex = 0;
            // 
            // PropertyGridView
            // 
            this.ClientSize = new System.Drawing.Size(330, 381);
            this.Controls.Add(this._DocumentPropertyGrid);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "PropertyGridView";
            this.Text = "属性";
            this.ResumeLayout(false);

        }
    }
}
