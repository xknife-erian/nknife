using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Jeelu.SimplusD.Client.Win
{
    public partial class MediaUserControl : UserControl
    {
        public MediaUserControl()
        {
            InitializeComponent();
            pathtextBox.Enabled = false;
            GetCHSTextforFlashPropertyPanel getchs = new GetCHSTextforFlashPropertyPanel();
            widthLabel.Text = ResourceService.GetResourceText("flash.label.width");
            heightLabel.Text = ResourceService.GetResourceText("flash.label.height");
            pathLabel.Text = ResourceService.GetResourceText("flash.label.path");
            useclassLabel.Text = ResourceService.GetResourceText("flash.label.useclass");
            vspacelabel.Text = ResourceService.GetResourceText("flash.label.vspace");
            hspacelabel.Text = ResourceService.GetResourceText("flash.label.hspace");
            qualitylabel.Text = ResourceService.GetResourceText("flash.label.quality");
            scalelabel.Text = ResourceService.GetResourceText("flash.label.scale");
            bglabel.Text = ResourceService.GetResourceText("flash.label.bgcolor");
            alignlabel.Text = ResourceService.GetResourceText("flash.label.align");
            loopcheckBox.Text = ResourceService.GetResourceText("flash.checkboxtext.loop");
            autoplaycheckBox.Text = ResourceService.GetResourceText("flash.checkboxtext.autoplay");

            string[] Quality = getchs.FlashQuality;
            string[] calign = getchs.Align;
            string[] Unit = getchs.Unit;
            string[] Scale = getchs.Scale;

            foreach (string Q in Quality)
                qualitycomboBox.Items.Add(Q);
            foreach (string a in calign)
                aligncomboBox.Items.Add(a);
            foreach (string sc in Scale)
                scalecomboBox.Items.Add(sc);
            foreach (string u in Unit)
            {
                flashWidUintComboBox.Items.Add(u);
                flashHeigUintComboBox.Items.Add(u);
            }
        }

        private void MediaUserControl_Paint(object sender, PaintEventArgs e)
        {
            GeneralMethods.drawLineForProPanel(this, Color.Blue, 1, DashStyle.Solid, Width);
        }

        #region 属性
        public string MediaScale
        {
            get { return scalecomboBox.Text; }
            set
            {
                switch (value)
                {
                    case "noborder": scalecomboBox.SelectedIndex = 1; break;
                    case "exactfit": scalecomboBox.SelectedIndex = 2; break;
                    case "": scalecomboBox.SelectedIndex = 0; break;
                }
            }
        }
        public string MediaQuality
        {
            get { return qualitycomboBox.Text; }
            set
            {
                switch (value)
                {
                    case "autolow": qualitycomboBox.SelectedIndex = 3; break;
                    case "": qualitycomboBox.SelectedIndex = 2; break;
                    case "low": qualitycomboBox.SelectedIndex = 1; break;
                    case "high":
                    case "hight": qualitycomboBox.SelectedIndex = 0; break;
                }
            }
        }
        public string MediaAlign
        {
            get { return aligncomboBox.Text; }
            set
            {
                switch (value)
                {
                    case "": aligncomboBox.SelectedIndex = 0; break;
                    case "baseline": aligncomboBox.SelectedIndex = 1; break;
                    case "top": aligncomboBox.SelectedIndex = 2; break;
                    case "middle": aligncomboBox.SelectedIndex = 3; break;
                    case "bottom": aligncomboBox.SelectedIndex = 4; break;
                    case "textTop": aligncomboBox.SelectedIndex = 5; break;
                    case "absMiddle": aligncomboBox.SelectedIndex = 6; break;
                    case "absBottom": aligncomboBox.SelectedIndex = 7; break;
                    case "left": aligncomboBox.SelectedIndex = 8; break;
                    case "right": aligncomboBox.SelectedIndex = 9; break;
                }
            }
        }
        public string MediaClass
        {
            get { return useclasscomboBox.Text; }
            set { useclasscomboBox.Text = value; }
        }
        public bool MediaAutoplay
        {
            get { return autoplaycheckBox.Checked; }
            set { autoplaycheckBox.Checked = value; }
        }
        public bool MediaLoop
        {
            get { return loopcheckBox.Checked; }
            set { loopcheckBox.Checked = value; }
        }
        public string MediaPath
        {
            get { return pathtextBox.Text; }
            set { pathtextBox.Text = value; }
        }
        public string MediaHspace
        {
            get { return hspacetextBox.Text; }
            set { hspacetextBox.Text = value; }
        }
        public string MediaVspace
        {
            get { return vspacetextBox.Text; }
            set { vspacetextBox.Text = value; }
        }
        public string MediaHeight
        {
            get { return heightTextBox.Text; }
            set { heightTextBox.Text = value; }
        }
        public string MediaWidth
        {
            get { return widthTextBox.Text; }
            set { widthTextBox.Text = value; }
        }

        public string MediaHeightUnit
        {
            get { return flashHeigUintComboBox.Text; }
            set { flashHeigUintComboBox.SelectedIndex = Convert.ToInt32(value); }
        }
        public string MediaWidthUnit
        {
            get { return flashWidUintComboBox.Text; }
            set
            {
                flashWidUintComboBox.SelectedIndex = Convert.ToInt32(value);
            }
        }
        #endregion
    }
}
