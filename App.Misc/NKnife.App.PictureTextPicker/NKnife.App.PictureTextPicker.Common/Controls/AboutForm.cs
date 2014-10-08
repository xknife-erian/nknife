using System;
using System.Windows.Forms;
using NKnife.App.PictureTextPicker.Common.Base.Res;
using NKnife.Interface;
using NKnife.IoC;

namespace NKnife.App.PictureTextPicker.Common.Controls
{
    public sealed partial class AboutForm : Form
    {
        private readonly IAbout _About = DI.Get<IAbout>();

        public AboutForm()
        {
            InitializeComponent();

            _LogoPictureBox.Image = OwnResources.giraffe;
            Text = String.Format("关于 {0}", _About.AssemblyTitle);
            labelProductName.Text = _About.AssemblyProduct;
            labelVersion.Text = String.Format("版本 {0}", _About.AssemblyVersion);
            labelCopyright.Text = _About.AssemblyCopyright;
            labelCompanyName.Text = _About.AssemblyCompany;
            textBoxDescription.Text = _About.AssemblyDescription;
        }
    }
}