using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class ProductImage : UserControl
    {
        protected string picPath;
        protected string ImageId;
        ProductImageData data = new ProductImageData();
        public ProductImageData Value
        {
            get
            {
                Save();
                return data;
            }
            set
            {
               data=value;
            }
        }
        public bool IsBigImageTag { get; set; }
        public ProductImage()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddSourceForm();
        }

        private void AddSourceForm()
        {
            if (IsBigImageTag)
                this.btnAddImage.Text = "添加大图";
            else
                this.btnAddImage.Text = "添加小图";
            ShowAddImage();
        }
        private void ShowAddImage()
        {
            if (!string.IsNullOrEmpty(data.ImageId))
            {
                ImageId = data.ImageId;
                if (!string.IsNullOrEmpty(ImageId))
                    this.conPicShow.Image = System.Drawing.Image.FromFile(AddImageToControl(data.ImageId));
            }
   
        }
        private string AddImageToControl(string id)
        {
            if (string.IsNullOrEmpty(id))
                return string.Empty;
            string picPath = SiteResourceService.ParseFormatId("${srs_" + id + "}", true);
            return picPath;
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            ImageId = SiteResourceService.SelectResource(MediaFileType.Pic,Service.Workbench.MainForm);
            if (!string.IsNullOrEmpty(ImageId))
                this.conPicShow.Image = System.Drawing.Image.FromFile(AddImageToControl(ImageId));
        }
        private void Save()
        {
            if (!string.IsNullOrEmpty(ImageId))
            {
                data.ImageId = ImageId;
            }
        }
        public event EventHandler Changed;
        protected virtual void OnCheckChanged(EventArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }
        private void conPicShow_Paint(object sender, PaintEventArgs e)
        {
            OnCheckChanged(EventArgs.Empty);
        }
    }
}
